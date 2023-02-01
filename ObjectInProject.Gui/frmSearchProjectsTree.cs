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
using ObjectInProject.Utils;

namespace ObjectInProject.Gui
{
    public partial class frmSearchProjectsTree : Form
    {
        #region Local Constants

        private static string module = MethodBase.GetCurrentMethod().DeclaringType.Name;

        #endregion

        #region Events

        public event EventMessage Message;

        #endregion

        #region Data Members 

        private List<Editors> activeEditors;
            
        private SearchProjects searchProjects;

        private TreeNode tnRoot;
        private TreeNode tnDirectoriesSearchProjects;
        private TreeNode tnSolutionsSearchProjects;

        private ContextMenu rootContextMenu;
        private ContextMenu solutionsSearchProjectContextMenu;
        private ContextMenu directoriesSearchProjectContextMenu;
        private ContextMenu solutionsUpdateDeleteSearchProjectContextMenu;
        private ContextMenu directoriesUpdateDeleteSearchProjectContextMenu;

        #endregion

        #region Constructor

        public frmSearchProjectsTree(SearchProjects inSearchProjects, List<Editors> inActiveEditors)
        {
            InitializeComponent();

            searchProjects = inSearchProjects;
            activeEditors = inActiveEditors;
        }

        #endregion

        #region Startup & Close

        private void frmSolutionsTree_Load(object sender, EventArgs e)
        {
            string result;

            try
            {
                Location = Cursor.Position;

                if (!CreateContextMenus(out result))
                {
                    MessageBox.Show(result, "Create Context Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (searchProjects == null)
                {
                    searchProjects = new SearchProjects();
                }

                if (!CreateTree(out result))
                {
                    MessageBox.Show(result, "Load Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSearchProjectsTree_FormClosed(object sender, FormClosedEventArgs e)
        {
            string result;

            try
            {
                SearchProjects inSearchProjects;
                if (!OnMessage((new MessageObject(IoAction.Keep, searchProjects)), out inSearchProjects, out result))
                {
                    MessageBox.Show(result, "Form Closed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Closed Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CreateTree(out string result)
        {
            result = string.Empty;

            try
            {
                tvSearchProjects.Nodes.Clear();

                tnRoot = tvSearchProjects.Nodes.Add("Search Projects");
                tnRoot.ContextMenu = rootContextMenu;

                tnSolutionsSearchProjects = tvSearchProjects.Nodes[0].Nodes.Add("Solutions Projects");
                tnSolutionsSearchProjects.Tag = new TagObject(TagObjectType.Main, SearchProjectType.SolutionsProject);
                tnSolutionsSearchProjects.ContextMenu = solutionsSearchProjectContextMenu;

                tnDirectoriesSearchProjects = tvSearchProjects.Nodes[0].Nodes.Add("Directories Projects");
                tnDirectoriesSearchProjects.Tag = new TagObject(TagObjectType.Main, SearchProjectType.DirectoriesProject);
                tnDirectoriesSearchProjects.ContextMenu = directoriesSearchProjectContextMenu;

                if ((searchProjects != null) && (searchProjects.SearchProjectsList != null))
                {
                    foreach (SearchProject searchProject in searchProjects.SearchProjectsList)
                    {
                        bool isActive = searchProject.Index == (searchProjects.ActiveSearchProjectIndex);
                        switch (searchProject.Type)
                        {
                            case SearchProjectType.DirectoriesProject:
                                if (!AddDirectorySearchProjects(searchProject.Name, isActive, searchProject.Workspace, out result))
                                {
                                }
                                break;

                            case SearchProjectType.SolutionsProject:
                                if (!AddSolutionsSearchProjects(searchProject.Name, isActive, searchProject.Workspace, out result))
                                {
                                }
                                break;

                            default:
                                result = $"Wrong Projet Type [{searchProject.Type}] For Search Project '{searchProject.Name}'";

                                MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }
                    } 
                }

                tnRoot.ExpandAll();

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion

        #region Context Menu Stuff

        private bool CreateContextMenus(out string result)
        {
            result = string.Empty;

            try
            {
                if (!CreateRootContextMenu(out result))
                {
                    return false;
                }

                if (!CreateSolutionsSearchProjectContextMenu(out result))
                {
                    return false;
                }

                if (!CreateDirectoriesSearchProjectContextMenu(out result))
                {
                    return false;
                }

                if (!CreateSolutionsUpdateDeleteSearchProjectContextMenu(out result))
                {
                    return false;
                }

                if (!CreateDirectoriesUpdateDeleteSearchProjectContextMenu(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Context Menus Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool CreateRootContextMenu(out string result)
        {
            result = string.Empty;

            try
            {
                rootContextMenu = new ContextMenu();
                rootContextMenu.Tag = null;
                rootContextMenu.MenuItems.Add("Reload Search Projects", ReloadSearchProjectsHandler);
                rootContextMenu.MenuItems.Add("Save Search Projects", SaveSearchProjectsHandler);
                rootContextMenu.MenuItems.Add("-");
                rootContextMenu.MenuItems.Add("Delete All Search Projects", DeleteAllSearchProjectsHandler);
                rootContextMenu.MenuItems.Add("-");
                rootContextMenu.MenuItems.Add("Unset Active Search Project", UnsetActiveSearchProjectHandler);
                
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Context Menus Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool CreateSolutionsSearchProjectContextMenu(out string result)
        {
            result = string.Empty;

            try
            {                
                solutionsSearchProjectContextMenu = new ContextMenu();
                solutionsSearchProjectContextMenu.Tag = SearchProjectType.SolutionsProject;

                solutionsSearchProjectContextMenu.MenuItems.Add("Add Solutions Search Project", CreateSearchProjectHandler);
                solutionsSearchProjectContextMenu.MenuItems.Add("Delete All Solutions Search Projects", DeleteAllSearchProjectsHandler);
                
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Solutions Search Project Context Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool CreateDirectoriesSearchProjectContextMenu(out string result)
        {
            result = string.Empty;

            try
            {                
                directoriesSearchProjectContextMenu = new ContextMenu();
                directoriesSearchProjectContextMenu.Tag = SearchProjectType.DirectoriesProject;

                directoriesSearchProjectContextMenu.MenuItems.Add("Add Directories Search Project", CreateSearchProjectHandler);
                directoriesSearchProjectContextMenu.MenuItems.Add("Delete All Directories Search Projects", DeleteAllSearchProjectsHandler);
                
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Directories Search Project Context Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool CreateSolutionsUpdateDeleteSearchProjectContextMenu(out string result)
        {
            result = string.Empty;

            try
            {                
                solutionsUpdateDeleteSearchProjectContextMenu = new ContextMenu();
                solutionsUpdateDeleteSearchProjectContextMenu.Tag = SearchProjectType.SolutionsProject;

                solutionsUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Set Search Project As Active", SetSearchProjectAsActiveHandler);
                solutionsUpdateDeleteSearchProjectContextMenu.MenuItems.Add("-");
                solutionsUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Update Solutions Search Project", UpdateSearchProjectHandler);
                solutionsUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Delete Solutions Search Project", DeleteSearchProjectHandler);
                
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Solutions Update\\Delete Search Project Context Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool CreateDirectoriesUpdateDeleteSearchProjectContextMenu(out string result)
        {
            result = string.Empty;

            try
            {                
                directoriesUpdateDeleteSearchProjectContextMenu = new ContextMenu();
                directoriesUpdateDeleteSearchProjectContextMenu.Tag = SearchProjectType.DirectoriesProject;

                directoriesUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Set Search Project As Active", SetSearchProjectAsActiveHandler);
                directoriesUpdateDeleteSearchProjectContextMenu.MenuItems.Add("-");
                directoriesUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Update Directories Search Project", UpdateSearchProjectHandler);
                directoriesUpdateDeleteSearchProjectContextMenu.MenuItems.Add("Delete Directories Search Project", DeleteSearchProjectHandler);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                MessageBox.Show(result, "Create Directories Update\\Delete Search Project Context Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        #region Context Menus Handlers

        private void SetSearchProjectAsActiveHandler(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = tvSearchProjects.SelectedNode;
                string searchProjectName = tn.Text;
                string result;

                if (!searchProjects.SetAsActive(searchProjectName, out result))
                {
                    MessageBox.Show(result, $"Set As Active Search Project Failure '{searchProjectName}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                SearchProjects inSearchProjects;

                if (!OnMessage((new MessageObject(IoAction.Keep, searchProjects)), out inSearchProjects, out result))
                {
                    MessageBox.Show(result, "Save Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!CreateTree(out result))
                {
                    MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Set Search Project As Active Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSearchProjectsHandler(object sender, EventArgs e)
        {
            try
            {
                string result;

                SearchProjects inSearchProjects;

                if (!OnMessage((new MessageObject(IoAction.KeepAndSave, searchProjects)), out inSearchProjects, out result))
                {
                    MessageBox.Show(result, "Save Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadSearchProjectsHandler(object sender, EventArgs e)
        {
            try
            {
                string result;

                SearchProjects inSearchProjects;

                if (!OnMessage((new MessageObject(IoAction.Reload)), out inSearchProjects, out result))
                {
                    MessageBox.Show(result, "Reload Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                searchProjects = inSearchProjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reload Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateSearchProjectHandler(object sender, EventArgs e)
        {
            SearchProjectType projectType;

            try
            {
                MenuItem mi = (MenuItem)sender;
                ContextMenu cm = (ContextMenu)(mi.Parent);
                projectType = (SearchProjectType)(cm.Tag);

                frmCreateUpdateSearchProject createUpdateSearchProject = new frmCreateUpdateSearchProject(CrudAction.Create, 
                                                                                                          projectType,
                                                                                                          activeEditors);
                createUpdateSearchProject.Reply += CreateUpdateSearchProject_Reply;
                createUpdateSearchProject.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Search Project Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSearchProjectHandler(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)sender;
                ContextMenu cm = (ContextMenu)(mi.Parent);
                SearchProjectType projectType = (SearchProjectType)(cm.Tag);

                TreeNode tn = tvSearchProjects.SelectedNode;
                string searchProjectName = tn.Text;
                string result;

                SearchProject searchProject;
                if (!searchProjects.GetByName(searchProjectName, out searchProject, out result))
                {
                    MessageBox.Show(result, $"Update Search Project Failure '{searchProjectName}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                frmCreateUpdateSearchProject updateSearchProject = new frmCreateUpdateSearchProject(CrudAction.Update, 
                                                                                                    searchProject,
                                                                                                    activeEditors);
                updateSearchProject.Reply += CreateUpdateSearchProject_Reply;
                updateSearchProject.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Search Project Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSearchProjectHandler(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = (MenuItem)sender;
                ContextMenu cm = (ContextMenu)(mi.Parent);
                SearchProjectType projectType = (SearchProjectType)(cm.Tag);

                TreeNode tn = tvSearchProjects.SelectedNode;
                string searchProjectName = tn.Text;
                string result;

                DialogResult dialogResult = MessageBox.Show("Delete Search Project '" + searchProjectName + "'?",
                                                            "Delete Search Project",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }

                if (!searchProjects.Delete(searchProjectName, out result))
                {
                    MessageBox.Show(result, $"Delete Search Project Failure '{searchProjectName}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!CreateTree(out result))
                {
                    MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Search Project Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteAllSearchProjectsHandler(object sender, EventArgs e)
        {
            string result;

            try
            {
                MenuItem mi = (MenuItem)sender;
                ContextMenu cm = (ContextMenu)(mi.Parent);

                SearchProjectType projectType = SearchProjectType.Unknown;

                if ((cm.Tag != null) && (typeof(SearchProjectType) == (cm.Tag).GetType()))
                {
                    projectType = (SearchProjectType)(cm.Tag);
                }

                string deleteQuestionString;
                switch (projectType)
                {
                    case SearchProjectType.DirectoriesProject:
                    case SearchProjectType.SolutionsProject:
                        deleteQuestionString = $"Delete All {GeneralUtils.GetEnumDescription(projectType)}s";
                        break;

                    default:
                        deleteQuestionString = "Delete All Search Projects";
                        break;
                }

                DialogResult dialogResult = MessageBox.Show($"{deleteQuestionString}?",
                                                            "Delete Search Projects",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }

                if (!searchProjects.DeleteAll(projectType, out result))
                {
                    MessageBox.Show(result, $"{deleteQuestionString} Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!CreateTree(out result))
                {
                    MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete All Search Projects Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnsetActiveSearchProjectHandler(object sender, EventArgs e)
        {
            string result;

            try
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure?",
                                                            "Unset Active Search Project",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }

                searchProjects.ActiveSearchProjectIndex = Constants.NONE;
                searchProjects.ActiveSearchProjectRealIndex = Constants.NONE;

                SearchProjects inSearchProjects;
                if (!OnMessage((new MessageObject(IoAction.Keep, searchProjects)), out inSearchProjects, out result))
                {
                    MessageBox.Show(result, "Form Closed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!CreateTree(out result))
                {
                    MessageBox.Show(result, "Create Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unset Active Search Project Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region CrUd Reply

        private bool CreateUpdateSearchProject_Reply(CrudAction crudAction, SearchProject searchProject, out string result)
        {
            result = string.Empty;

            try
            {
                if (searchProject != null)
                {
                    int searchProjectIndex;

                    switch (crudAction)
                    {
                        case CrudAction.Create:
                            if (!searchProjects.Add(searchProject, out searchProjectIndex, out result))
                            {
                                return false;
                            }
                            break;
                        
                        case CrudAction.Update:
                            if (!searchProjects.Update(searchProject, out result))
                            {
                                return false;
                            }
                            break;
                        
                        default:
                            result = $"Wrong CRUD Action [{crudAction.ToString()}]";
                            return false;
                    }

                    SearchProjects inSearchProjects;
                    if (!OnMessage((new MessageObject(IoAction.Keep, searchProjects)), out inSearchProjects, out result))
                    {
                        return false;
                    }

                    if (!CreateTree(out result))
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    result = "Search Project Is Null";

                    return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        private bool AddSolutionsSearchProjects(string name, bool isActive, List<string> solutions, out string result)
        {
            TreeNode tnSolution;
            TreeNode tnSearchProject;

            result = string.Empty;

            try
            {
                tnSearchProject = tnSolutionsSearchProjects.Nodes.Add(name);
                if (isActive)
                {
                    tnSearchProject.BackColor = Color.Aquamarine;
                }
                tnSearchProject.Tag = new TagObject(TagObjectType.SolutionsSearchPrject);
                tnSearchProject.ContextMenu = solutionsUpdateDeleteSearchProjectContextMenu;

                if (solutions != null)
                {
                    for (int i = 0; i < solutions.Count; i++)
                    {
                        string currentSolution = solutions[i];

                        tnSolution = tnSearchProject.Nodes.Add(currentSolution);
                    }

                    tvSearchProjects.Nodes[0].Expand();
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool AddDirectorySearchProjects(string name, bool isActive, List<string> directories, out string result)
        {
            TreeNode tnDirectory;
            TreeNode tnSearchProject;

            result = string.Empty;

            try
            {
                tnSearchProject = tnDirectoriesSearchProjects.Nodes.Add(name);
                if (isActive)
                {
                    tnSearchProject.BackColor = Color.Aquamarine;
                }
                tnSearchProject.Tag = new TagObject(TagObjectType.DirectoriesSearchPrject);
                tnSearchProject.ContextMenu = directoriesUpdateDeleteSearchProjectContextMenu;

                if (directories != null)
                {
                    for (int i = 0; i < directories.Count; i++)
                    {
                        string currentDirectory = directories[i];

                        tnDirectory = tnSearchProject.Nodes.Add(currentDirectory);
                    }

                    tvSearchProjects.Nodes[0].Expand();
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool OnMessage(MessageObject message, out SearchProjects searchProjects, out string result)
        {
            result = string.Empty;

            searchProjects = null;

            try
            {
                if (Message != null)
                {
                    object objectSearchProjects;
                    if (!Message(message, out objectSearchProjects, out result))
                    {
                        MessageBox.Show(result, "Send Message Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

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
