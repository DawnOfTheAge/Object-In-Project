using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General.Common;
using General.Log;
using ObjectInProject.Common;

namespace ObjectInProject.Gui
{
    public partial class frmCreateUpdateSearchProject : Form
    {
        #region Local Constants

        private static string module = MethodBase.GetCurrentMethod().DeclaringType.Name;

        #endregion

        #region Events

        public event CreateUpdateSearchProjectReply Reply;

        #endregion

        #region Data Members

        private List<Editors> activeEditors;

        private ContextMenu removeMenu;

        private SearchProject searchProject;

        private CrudAction crudAction;
        
        private SearchProjectType projectType;

        private string fileTypeFilter;

        #endregion

        #region Constructor

        public frmCreateUpdateSearchProject(CrudAction inCrudAction, SearchProjectType inProjectType, List<Editors> inActiveEditors)
        {
            InitializeComponent();

            crudAction = inCrudAction;
            projectType = inProjectType;
            activeEditors = inActiveEditors;
        }

        public frmCreateUpdateSearchProject(CrudAction inCrudAction, SearchProject inSearchProject, List<Editors> inActiveEditors)
        {
            InitializeComponent();

            crudAction = inCrudAction;
            searchProject = inSearchProject;
            projectType = searchProject.Type;
            fileTypeFilter = searchProject.FileTypeFilter;
            activeEditors = inActiveEditors;
        }

        #endregion

        #region Startup

        private void frmCreateUpdateSearchProject_Load(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            #endregion

            try
            {
                Location = Cursor.Position;

                Text = $"{crudAction} {Utils.GetEnumDescription(projectType)}";

                switch (projectType)
                {
                    case SearchProjectType.DirectoriesProject:
                        rbDirectoriesProjectSearch.Checked = true;
                        tabSettings.TabPages.Remove(tabPageSolutionsList);

                        removeMenu = new ContextMenu();
                        removeMenu.MenuItems.Add("Remove", mnuRemoveDirectory);
                        lstDirectories.ContextMenu = removeMenu;
                        break;

                    case SearchProjectType.SolutionsProject:
                        rbSolutionsProjectSearch.Checked = true;
                        tabSettings.TabPages.Remove(tabPageDirectoriesList);

                        removeMenu = new ContextMenu();
                        removeMenu.MenuItems.Add("Remove", mnuRemoveSolution);
                        lstSolutions.ContextMenu = removeMenu;
                        break;

                    default:
                        MessageBox.Show($"Wrong Search Project Type [{projectType}]", 
                                        "Failed On Settings Startup", 
                                        MessageBoxButtons.OK, 
                                        MessageBoxIcon.Error);
                        return;
                }

                rbSolutionsProjectSearch.Enabled = false;
                rbDirectoriesProjectSearch.Enabled = false;

                if ((activeEditors == null) || (activeEditors.Count == 0))
                {
                    Audit("No Active Editors", method, AuditSeverity.Warning, Log.LINE());
                    MessageBox.Show("No Active Editors", "Failed On Settings Startup", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                foreach (Editors editor in activeEditors)
                {
                    cboEditor.Items.Add(editor.ToString());
                }

                switch (crudAction)
                {
                    case CrudAction.Create:
                        searchProject = new SearchProject();
                        searchProject.Type = projectType;

                        cboEditor.Text = cboEditor.Items[0].ToString();

                        rbAnd.Checked = true;

                        chkCaseSensitiveSearch.Checked = false;
                        chkNoTest.Checked = false;

                        txtFileTypeFilters.Text = $"{ObjectInProjectConstants.CS_FILE_EXTENSION};";
                        break;

                    case CrudAction.Update:
                        txtSearchProjectName.Text = searchProject.Name;

                        int index = GetEditorIndexInList(searchProject.Editor, out var result);
                        cboEditor.Text = cboEditor.Items[index].ToString();

                        rbAnd.Checked = (searchProject.Logic == SearchLogic.And);

                        chkCaseSensitiveSearch.Checked = searchProject.CaseSensitive;
                        chkNoTest.Checked = searchProject.NoTests;

                        txtFileTypeFilters.Text = string.Join(";", searchProject.FileTypeFilter);

                        if ((searchProject.Workspace != null) && (searchProject.Workspace.Count > 0))
                        {
                            foreach (string pathOrSolution in searchProject.Workspace)
                            {
                                if (searchProject.Type == SearchProjectType.DirectoriesProject)
                                {
                                    lstDirectories.Items.Add(pathOrSolution);
                                }

                                if (searchProject.Type == SearchProjectType.SolutionsProject)
                                {
                                    lstSolutions.Items.Add(pathOrSolution);
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(message, "Failed On Settings Startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Reply 

        private bool OnReply()
        {
            string result;

            if (Reply != null)
            {
                if (!Reply(crudAction, searchProject, out result))
                {
                    MessageBox.Show($"Send Reply Failure. {result}", "Failed Adding Search Project", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                return true;
            }

            return false;
        }

        private void btnSaveSearchProject_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            DialogResult dialogResult;

            #endregion

            try
            {
                searchProject.Name = txtSearchProjectName.Text;
                searchProject.FileTypeFilter = txtFileTypeFilters.Text;
                searchProject.CaseSensitive = chkCaseSensitiveSearch.Checked;
                searchProject.NoTests = chkNoTest.Checked;
                searchProject.Logic = rbAnd.Checked ? SearchLogic.And : SearchLogic.Or;

                if ((searchProject.Workspace == null) || (searchProject.Workspace.Count == 0))
                {
                    dialogResult = MessageBox.Show("Search Project '" + searchProject.Name + "' Has No Workspace. Continue Anyway?",
                                                   "Save Search Project",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes)
                    {
                        return;
                    }
                }

                dialogResult = MessageBox.Show("Keep Search Project '" + searchProject.Name + "'?",
                                               "Save Search Project",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (OnReply())
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(message, "Failed Adding Search Job", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Search Logic

        private void rbAndLogicSearch_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("Search Logic Error"))
                //{
                //    Audit("Serach Type Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.Logic = (rbAnd.Checked) ? SearchLogic.And : SearchLogic.Or;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Search Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void rbOrLogicSearch_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("Search Logic Error"))
                //{
                //    Audit("Serach Type Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.Logic = (rbOr.Checked) ? SearchLogic.Or : SearchLogic.And;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Search Logic Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Search Types

        private void rbSolutionsProjectsSearch_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("Search Type Error"))
                //{
                //    Audit("Serach Type Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.Type = (rbSolutionsProjectSearch.Checked) ? SearchProjectType.SolutionsProject : SearchProjectType.DirectoriesProject;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Search Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void rbAllFilesSearch_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("Search Type Error"))
                //{
                //    Audit("Serach Type Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.Type = (rbDirectoriesProjectSearch.Checked) ? SearchProjectType.DirectoriesProject : SearchProjectType.SolutionsProject;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Search Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Discard files with 'test'

        private void chkNoTest_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("No Test Error"))
                //{
                //    Audit("No Test Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.NoTests = chkNoTest.Checked;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "No Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Case Sensitive

        private void chkCaseSensitiveSearch_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                //if (!ValidateCurrentSearchJob("Case Sensitive Error"))
                //{
                //    Audit("Case Sensitive Error", method, AuditSeverity.Error, Log.LINE());

                //    return;
                //}

                searchProject.CaseSensitive = chkCaseSensitiveSearch.Checked;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Case Sensitive Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Editor

        private void cboEditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                searchProject.Editor = cboEditor.Text;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Editor Pick Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private int GetEditorIndexInList(string editorString, out string result)
        {
            result = string.Empty;

            try
            {
                Editors editor =  Utils.EditorToEnum(editorString);

                int index = cboEditor.Items.IndexOf(editor.ToString());
                if (index == Constants.NONE)
                {
                    index = cboEditor.Items.IndexOf(Editors.Notepad);
                }

                return index;
            }
            catch (Exception e)
            {
                result = e.Message;

                return cboEditor.Items.IndexOf(Editors.Notepad);
            }
        }        

        #endregion

        #region Solutions

        private void btnAddSolution_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            OpenFileDialog ofd = new OpenFileDialog();

            #endregion

            try
            {
                ofd.Filter = "Solution Files (*.sln)|*.sln";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!searchProject.Workspace.Contains(ofd.FileName))
                    {
                        searchProject.Workspace.Add(ofd.FileName);
                    }

                    lstSolutions.Items.Clear();

                    foreach (string line in searchProject.Workspace)
                    {
                        lstSolutions.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(message, "Failed Adding Solution", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuRemoveSolution(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            #endregion

            try
            {
                if ((lstSolutions.Items == null) || (lstSolutions.Items.Count <= 0))
                {
                    return;
                }

                DialogResult dr = MessageBox.Show("Remove?", "Remove Solution From Solutions List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (lstSolutions.SelectedIndex != Constants.NONE)
                    {
                        lstSolutions.Items.RemoveAt(lstSolutions.SelectedIndex);
                    }

                    searchProject.Workspace = new List<string>();

                    foreach (string item in lstSolutions.Items)
                    {
                        searchProject.Workspace.Add(item);
                    }                    
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(message, "Failed Removing Solution From Solutions List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Directories

        private void btnAddDirectory_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            #endregion

            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (!searchProject.Workspace.Contains(fbd.SelectedPath))
                    {
                        searchProject.Workspace.Add(fbd.SelectedPath);
                    }

                    lstDirectories.Items.Clear();

                    foreach (string line in searchProject.Workspace)
                    {
                        lstDirectories.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(message, "Failed Adding Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuRemoveDirectory(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            #endregion

            try
            {
                if ((lstDirectories.Items == null) || (lstDirectories.Items.Count <= 0))
                {
                    return;
                }

                DialogResult dr = MessageBox.Show("Remove?", "Remove Directory From Directories List", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (lstDirectories.SelectedIndex != Constants.NONE)
                    {
                        lstDirectories.Items.RemoveAt(lstDirectories.SelectedIndex);
                    }

                    searchProject.Workspace = new List<string>();

                    foreach (string item in lstDirectories.Items)
                    {
                        searchProject.Workspace.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;

                Audit(message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Failed Removing Directory From Directories List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Audit

        private static void Audit(string message, string method, AuditSeverity auditSeverity, int line)
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string fileName = Log.FILE();

            Log.Audit(message, fileName, assemblyName, module, method, auditSeverity, line);
        }

        #endregion
    }
}
