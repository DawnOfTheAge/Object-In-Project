using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using General.Common;
using General.Log;
using Microsoft.Build.Construction;
using Newtonsoft.Json;
using ObjectInProject.Common;
using ObjectInProject.EditorsInformation;
using ObjectInProject.Search;

namespace ObjectInProject.Gui
{
    public partial class frmMain : Form
    {
        #region Local Constants

        private static string module = MethodBase.GetCurrentMethod().DeclaringType.Name;

        #endregion

        #region Data Members        

        private bool searchCaseSensitive;
        private bool searchNoTests;

        private int m_numberOfFiles;
        private int m_numberOfTestFiles;
        private int m_numberOfProjects;

        private Editors EDITOR_USED;

        private string m_Path;
        private string m_FullFilename;
        private string searchAndDelimiter;
        private string searchOrDelimiter;

        private List<string> directories;
        private List<string> fileTypeFilter;

        private SearchProjects m_Configuration;

        private SearchProject m_ActiveSearchProject;

        private List<EditorInformation> m_VisualStudio;
        
        private List<Editors> m_ActiveEditors;

        private SearchDelegate m_SearchType;

        #endregion

        #region Delegates

        private delegate bool SearchDelegate(string line, List<string> lText, out string result);

        #endregion

        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Close
        
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;
            string result;

            try
            {
                DialogResult dialogResult = MessageBox.Show("Save Configuration?", "Save Configuration On Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveConfiguration(out result);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Warning, Log.LINE());
                MessageBox.Show(ex.Message, "Error Closing Object In Project", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Initialize

        private void frmMain_Load(object sender, EventArgs e)
        {
            string result;
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                Location = Cursor.Position;

                if (!Initialize(out result))
                {
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());
                    MessageBox.Show(result, "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Warning, Log.LINE());
                MessageBox.Show(ex.Message, "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Initialize(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;
            string solutionFileText = string.Empty;

            #endregion

            result = string.Empty;

            try
            {
                #region Initialize Variables

                m_SearchType = ContainsAll;

                directories = new List<string>();
                fileTypeFilter = new List<string>();

                m_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                m_FullFilename = m_Path + "\\" + ObjectInProjectConstants.APPLICATION_NAME + ObjectInProjectConstants.JSON_EXTENSION;

                m_numberOfProjects = 0;
                m_numberOfFiles = 0;
                m_numberOfTestFiles = 0;

                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                Log.LogPath = $"{path}";

                #endregion

                if (!VisualStudiosInstalled.GetVisualStudiosInstalled(out m_VisualStudio, out result))
                {
                    result = string.Format("Visual Studios Detection Failure. {0}.", result);
                    Audit(result, method, AuditSeverity.Information, Log.LINE());
                }

                if (LoadConfiguration(out result))
                {
                    if (!InitializeGui(out result))
                    {
                        return false;
                    }

                    if (m_ActiveSearchProject != null)
                    {
                        switch (m_ActiveSearchProject.Type)
                        {
                            case SearchProjectType.DirectoriesProject:
                                #region All Files

                                foreach (string directory in m_ActiveSearchProject.Workspace)
                                {
                                    if (Directory.Exists(directory))
                                    {
                                        directories.Add(directory);

                                        int numberOfFiles;
                                        int numberOfTestFiles;

                                        string criteriaList = string.IsNullOrEmpty(m_ActiveSearchProject.FileTypeFilter) ? m_ActiveSearchProject.FileTypeFilter : "*.*";

                                        if (FindCriteriaFilesNumbers(directory,
                                                                     criteriaList,
                                                                     out numberOfFiles,
                                                                     out numberOfTestFiles,
                                                                     out result))
                                        {
                                            m_numberOfFiles += numberOfFiles;
                                            m_numberOfTestFiles += numberOfTestFiles;
                                        }
                                    }
                                    else
                                    {
                                        Audit(result, method, AuditSeverity.Information, Log.LINE());
                                        result = string.Format("Path '{0}' Does Not Exist", directory);

                                        return false;
                                    }
                                }

                                #endregion
                                break;

                            case SearchProjectType.SolutionsProject:
                                #region Solutions & Projects

                                Solution solution;

                                foreach (string currentSolutionFile in m_ActiveSearchProject.Workspace)
                                {
                                    if (File.Exists(currentSolutionFile))
                                    {
                                        solutionFileText = File.ReadAllText(currentSolutionFile);

                                        if (!ParseSolutionFileText(currentSolutionFile, solutionFileText, out solution, out result))
                                        {
                                            Audit(result, method, AuditSeverity.Information, Log.LINE());
                                            result = string.Format("Solution File '{0}' Parse Error", currentSolutionFile);

                                            return false;
                                        }
                                        else
                                        {
                                            m_ActiveSearchProject.SolutionsInformation.Add(solution);
                                        }
                                    }
                                }

                                #endregion
                                break;

                            default:
                                message = "Wrong Search Type";

                                Audit(message, method, AuditSeverity.Error, Log.LINE());
                                result = string.Format("Initialization Error. {0}.", message);

                                return false;
                        }
                    }
                }
                else
                {
                    result = string.Format("Initialize Error. {0}.", result);
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                result = string.Format("Initialize Error. {0}.",ex.Message);

                return false;
            }
        }

        #endregion

        #region Parse        

        private bool ParseSolutionFileText(string solutionFilename, string solutionFileText, out Solution solution, out string result)
        {
            #region Data Members

            Project project;

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = "";

            solution = new Solution();

            solution.Name = solutionFilename;

            try
            {
                var _solutionFile = SolutionFile.Parse(solutionFilename);

                foreach (var currentProjectFile in _solutionFile.ProjectsInOrder)
                {
                    if (ParseProjectFile(currentProjectFile.AbsolutePath, out project, out result))
                    {
                        solution.Projects.Add(project);
                        solution.Projects[solution.Projects.Count - 1].Id = solution.Projects.Count;

                        m_numberOfProjects++;
                    }
                    else
                    {
                        Audit(result, method, AuditSeverity.Warning, Log.LINE());
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Audit(e.Message, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool ParseProjectFile(string projectFile, out Project project, out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string projectFileData;
            string projectFilePath;
            string projectFileDataFilename;
            string projectFileExtension;

            int projectFileTextIndex;
            int startCsIndex;
            int endCsIndex;

            #endregion

            result = "";

            List<string> lines;

            project = new Project();

            bool goOn = true;

            projectFileExtension = Path.GetExtension(projectFile);

            try
            {
                if (!string.IsNullOrEmpty(projectFile))
                {
                    projectFileDataFilename = projectFile;

                    if (!File.Exists(projectFileDataFilename))
                    {
                        result = "File '" + projectFileDataFilename + "' does not exist";

                        Audit(result, method, AuditSeverity.Warning, Log.LINE());

                        return false;
                    }

                    Audit("Parsing '" + projectFileDataFilename + "' ...", method, AuditSeverity.Information, Log.LINE());

                    projectFilePath = Path.GetDirectoryName(projectFileDataFilename);

                    project.Name = projectFileDataFilename;

                    projectFileData = File.ReadAllText(projectFileDataFilename);

                    projectFileTextIndex = 0;

                    switch (projectFileExtension.ToLower())
                    {
                        case ObjectInProjectConstants.CS_PROJECT_FILE_EXTENSION:
                            #region C# File

                            while (goOn)
                            {
                                startCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CS_PROPERTY, projectFileTextIndex);
                                if (startCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CS_FILE_EXTENSION, startCsIndex);
                                if (endCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                CsFile csFile = new CsFile();

                                csFile.Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CS_PROPERTY.Length + 1), 
                                                                                                 endCsIndex - startCsIndex - (ObjectInProjectConstants.CS_PROPERTY.Length - ObjectInProjectConstants.CS_FILE_EXTENSION.Length + 1));

                                if (Utils.ReadFileLines(csFile.Name, out lines, out result))
                                {
                                    csFile.Lines = lines;

                                    project.Files.Add(csFile);

                                    m_numberOfFiles++;
                                    if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                    {
                                        m_numberOfTestFiles++;
                                    }
                                }

                                projectFileTextIndex = endCsIndex + 3;

                                if (projectFileTextIndex >= projectFileData.Length)
                                {
                                    goOn = false;
                                }
                            }

                            #endregion
                            break;

                        case ObjectInProjectConstants.CPP_PROJECT_FILE_EXTENSION:
                            #region C++ Files CPP/H

                            while (goOn)
                            {
                                startCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY, projectFileTextIndex);
                                if (startCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION, startCsIndex);
                                if (endCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                CsFile csFile = new CsFile();

                                csFile.Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length + 1), 
                                                                                                 endCsIndex - startCsIndex - (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION.Length + 1));

                                if (Utils.ReadFileLines(csFile.Name, out lines, out result))
                                {
                                    csFile.Lines = lines;

                                    project.Files.Add(csFile);

                                    m_numberOfFiles++;
                                    if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                    {
                                        m_numberOfTestFiles++;
                                    }
                                }

                                projectFileTextIndex = endCsIndex + 3;

                                if (projectFileTextIndex >= projectFileData.Length)
                                {
                                    goOn = false;
                                }
                            }

                            goOn = true;
                            while (goOn)
                            {
                                startCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY, projectFileTextIndex);
                                if (startCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION, startCsIndex);
                                if (endCsIndex == Constants.NONE)
                                {
                                    break;
                                }

                                CsFile csFile = new CsFile();

                                csFile.Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length + 1), 
                                                                                                 endCsIndex - startCsIndex - (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION.Length + 1));

                                if (Utils.ReadFileLines(csFile.Name, out lines, out result))
                                {
                                    csFile.Lines = lines;

                                    project.Files.Add(csFile);

                                    m_numberOfFiles++;
                                    if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                    {
                                        m_numberOfTestFiles++;
                                    }
                                }

                                projectFileTextIndex = endCsIndex + 3;

                                if (projectFileTextIndex >= projectFileData.Length)
                                {
                                    goOn = false;
                                }
                            }

                            #endregion
                            break;

                        default:
                            result = "Wrong Project Type '" + projectFileExtension + "'";

                            return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;
                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        #endregion

        #region Find

        private bool ContainsAll(string line, List<string> lText, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(line))
            {
                result = "Line to search on is null or empty";

                return false;
            }

            if ((lText == null) || (lText.Count == 0))
            {
                result = "Text to search on is null or empty";

                return false;
            }

            return lText.Where(x => !string.IsNullOrEmpty(x)).All(v => line.Contains(v));
        }

        private bool ContainsAtLeastOneOf(string line, List<string> lText, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(line))
            {
                result = "Line to search on is null or empty";

                return false;
            }

            if ((lText == null) || (lText.Count == 0))
            {
                result = "Text to search on is null or empty";

                return false;
            }

            foreach (string item in lText)
            {
                if (line.IndexOf(item) != Constants.NONE)
                {
                    return true;
                }
            }

            result = "No item from list was found in '" + line + "'";

            return false;
        }

        #endregion

        #region GUI

        private void frmMain_Resize(object sender, EventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                btnFind.Location = new Point(10, 50);
                txtFind.Location = new Point(btnFind.Left + btnFind.Width + 20, 55);
                lvResults.Location = new Point(10, btnFind.Top + btnFind.Height + 20);
                chkCaseSensitive.Location = new Point(txtFind.Left, txtFind.Top + txtFind.Height + 5);
                lblFileTypeFilters.Location = new Point(chkCaseSensitive.Left + chkCaseSensitive.Width + 10, chkCaseSensitive.Top);
                cboFileTypeFilters.Location = new Point(lblFileTypeFilters.Left+ lblFileTypeFilters.Width + 5, lblFileTypeFilters.Top - 2);

                lvResults.Height = Height - lvResults.Top - 90;

                txtFind.Width = (Width / 3) * 2;
                lvResults.Width = Width - lvResults.Left - 50;

                btnLoadSearchedItemsFromFile.Location = new Point(txtFind.Left + txtFind.Width + 5, 50);
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Warning, Log.LINE());
                MessageBox.Show(ex.Message, "Error Resizing Form", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #region Main menu

        private void mnuClearResults_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            txtNumberOfHits.Text = "0";
            pbFiles.Value = 0;
        }

        private void mnuClearSearchHistory_Click(object sender, EventArgs e)
        {
            txtFind.Items.Clear();
            txtFind.Text = "";
        }

        private void mnuShowSearchProjectsTree_Click(object sender, EventArgs e)
        {
            string result;

            if (!GetActiveEditors(m_VisualStudio, out m_ActiveEditors, out result))
            {
                MessageBox.Show(result, "Active Editors Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            frmSearchProjectsTree searchProjectsTree = new frmSearchProjectsTree(m_Configuration, m_ActiveEditors);
            searchProjectsTree.Message += SearchProjectsTree_Message;
            searchProjectsTree.Show();
        }

        private bool SearchProjectsTree_Message(object message, out object reply, out string result)
        {
            result = string.Empty;

            reply = null;

            try
            {
                if (message == null)
                {
                    result = "Message Is Null";

                    return false;
                }

                MessageObject messageObject = (MessageObject)message;

                switch (messageObject.Action)
                {
                    case IoAction.Keep:
                    case IoAction.KeepAndSave:
                        if (messageObject.Data == null)
                        {
                            result = $"Message Data Is Null";

                            return false;
                        }

                        SearchProjects fromTreeSearchProjects = (SearchProjects)(messageObject.Data);

                        m_Configuration = fromTreeSearchProjects;

                        if (!InitializeStatusStrip(out result))
                        {
                            return false;
                        }

                        if (messageObject.Action == IoAction.KeepAndSave)
                        {
                            if (!SaveConfiguration(fromTreeSearchProjects, out result))
                            {
                                return false;
                            }
                        }
                        return true;

                    case IoAction.Reload:
                        if (!LoadConfiguration(out result))
                        {
                            return false;
                        }

                        reply = m_Configuration;
                        return true;

                    default:
                        result = $"Wrong Action[{messageObject.Action}]";
                        return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        
        private bool InitializeGui(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = string.Empty;

            try
            {
                #region Check Existing Configuration & Search Jobs

                if ((m_Configuration == null) || (m_Configuration.SearchProjectsList == null))
                {
                    result = string.Format("Configuration Error. No Configuration Defined.");
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());

                    return false;
                }

                #endregion

                frmMain_Resize(null, null);

                if (!InitializeStatusStrip(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool InitializeStatusStrip(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = string.Empty;

            try
            {
                #region Search Types

                cboSearchType.DropDownItems.Add("AND", null, SelectSearchType_Click);
                cboSearchType.DropDownItems.Add("OR", null, SelectSearchType_Click);
                txtSearchType.Text = cboSearchType.DropDownItems[0].Text;

                #endregion

                #region Search Jobs Load & Show And Define Active Search job

                cboActiveSearchJob.DropDownItems.Clear();
                txtActiveSearchJob.Text = string.Empty;
                foreach (SearchProject searchJob in m_Configuration.SearchProjectsList)
                {
                    cboActiveSearchJob.DropDownItems.Add(searchJob.Name, null, ChangeSearchJob_Click);
                }

                if ((m_Configuration.ActiveSearchProjectIndex != Constants.NONE) &&
                    (m_Configuration.ActiveSearchProjectRealIndex < m_Configuration.SearchProjectsList.Count))
                {
                    m_ActiveSearchProject = m_Configuration.SearchProjectsList[m_Configuration.ActiveSearchProjectRealIndex];

                    searchCaseSensitive = m_ActiveSearchProject.CaseSensitive;
                    searchNoTests = m_ActiveSearchProject.NoTests;
                    searchAndDelimiter = m_ActiveSearchProject.SearchAndDelimiter;
                    searchOrDelimiter = m_ActiveSearchProject.SearchOrDelimiter;

                    txtActiveSearchJob.Text = m_ActiveSearchProject.Name;
                }
                else
                {
                    m_ActiveSearchProject = null;

                    result = string.Format("Configuration Error. No Active Search Project Defined.");
                    Audit(result, method, AuditSeverity.Error, Log.LINE());

                    //return false;
                }

                #endregion

                #region Case Sensitive

                if (m_ActiveSearchProject != null)
                {
                    chkCaseSensitive.Checked = m_ActiveSearchProject.CaseSensitive;
                }
                else
                {
                    chkCaseSensitive.Checked = false;
                }

                #endregion

                #region Editor

                if (!BuildListView(out result))
                {
                    result = string.Format("Build List View Error. {0}.", result);
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());

                    return false;
                }

                if ((m_ActiveSearchProject != null) && (!string.IsNullOrEmpty(m_ActiveSearchProject.Editor)))
                {
                    EDITOR_USED = Utils.EditorToEnum(m_ActiveSearchProject.Editor);
                }
                else
                {
                    EDITOR_USED = Editors.Notepad;
                }

                if (!EnsureEditorActive(out result))
                {
                    result = string.Format("Editor Definition Error. {0}.", result);
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());

                    return false;
                }

                #endregion

                #region Load File Type Filters And Show Chosen Filter

                if (m_ActiveSearchProject != null)
                {
                    if (m_ActiveSearchProject.Type != SearchProjectType.DirectoriesProject)
                    {
                        cboFileTypeFilters.Text = m_ActiveSearchProject.FileTypeFilter;
                    }
                }
                else
                {
                    cboFileTypeFilters.Text = "*.*";
                }

                #endregion

                #region Projects & Files Lables

                if (m_ActiveSearchProject != null)
                {
                    txtNumberOfProjects.Visible = (m_ActiveSearchProject.Type == SearchProjectType.SolutionsProject) ? true : false;
                    lblNumberOfProjects.Visible = (m_ActiveSearchProject.Type == SearchProjectType.SolutionsProject) ? true : false;
                }
                else
                {
                    txtNumberOfProjects.Visible = false;
                    lblNumberOfProjects.Visible = false;
                }

                txtNumberOfProjects.Text = m_numberOfProjects.ToString();
                txtNumberOfFiles.Text = m_numberOfFiles.ToString();
                txtNumberOfTestFiles.Text = m_numberOfTestFiles.ToString();

                #endregion

                #region Colors

                txtNumberOfProjects.BackColor = Color.Blue;
                txtNumberOfProjects.ForeColor = Color.White;

                txtNumberOfTestFiles.BackColor = Color.Cyan;
                txtNumberOfTestFiles.ForeColor = Color.White;

                txtNumberOfFiles.BackColor = Color.DarkCyan;
                txtNumberOfFiles.ForeColor = Color.White;

                txtNumberOfHits.BackColor = Color.Coral;
                txtNumberOfHits.ForeColor = Color.White;

                txtActiveSearchJob.BackColor = Color.Blue;
                txtActiveSearchJob.ForeColor = Color.White;

                txtSearchType.BackColor = Color.Green;
                txtSearchType.ForeColor = Color.White;

                #endregion

                if (m_ActiveSearchProject != null)
                {
                    pbFiles.Maximum = (m_ActiveSearchProject.NoTests) ? (m_numberOfFiles - m_numberOfTestFiles) : m_numberOfFiles;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private void ChangeSearchJob_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string result;

            #endregion

            try
            {
                ToolStripMenuItem selectedSearchJob = (ToolStripMenuItem)sender;

                txtActiveSearchJob.Text = selectedSearchJob.Text;

                if (!m_Configuration.SetAsActive(txtActiveSearchJob.Text, out result))
                {
                    Audit(result, method, AuditSeverity.Warning, Log.LINE());
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
            }
        }

        private void SelectFileTypeFilter_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                ToolStripMenuItem selectedSearchJob = (ToolStripMenuItem)sender;

                cboFileTypeFilters.Text = selectedSearchJob.Text;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE()); ;
            }
        }

        private void SelectSearchType_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                ToolStripMenuItem selectedSearchJob = (ToolStripMenuItem)sender;

                txtSearchType.Text = selectedSearchJob.Text;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE()); ;
            }
        }
        
        private bool BuildListView(out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            result = string.Empty;

            try
            {
                lvResults.View = View.Details;
                lvResults.GridLines = true;
                lvResults.FullRowSelect = true;

                if (m_ActiveSearchProject != null)
                {
                    lvResults.Columns.Clear();
                    switch (m_ActiveSearchProject.Type)
                    {
                        case SearchProjectType.SolutionsProject:
                            lvResults.Columns.Add("Solution", 200);
                            lvResults.Columns.Add("Project", 400);
                            lvResults.Columns.Add("File", 400);
                            lvResults.Columns.Add("Line", 50);
                            lvResults.Columns.Add("Full File name", 0);
                            break;

                        case SearchProjectType.DirectoriesProject:
                            lvResults.Columns.Add("Directory", 400);
                            lvResults.Columns.Add("File", 400);
                            lvResults.Columns.Add("Line", 50);
                            lvResults.Columns.Add("Full File name", 0);
                            break;

                        default:
                            MessageBox.Show($"Wrong Search Type '{m_ActiveSearchProject.Type}'", 
                                            "GUI Error", 
                                            MessageBoxButtons.OK, 
                                            MessageBoxIcon.Error);
                            return false;
                    }
                }
                else
                {
                    MessageBox.Show("No Active Search Project Defined", 
                                    "GUI Warning", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Build Listview Failure. " + ex.Message, "GUI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
         
        private void btnFind_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string result;
            string message;

            List<SearchResult> searchResults;

            #endregion

            try
            {
                if (m_ActiveSearchProject == null)
                {
                    result = "No Active Search Project Defined";

                    Audit(result, method, AuditSeverity.Information, Log.LINE());
                    MessageBox.Show(result, "Finding Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                txtNumberOfHits.Text = "0";
                lvResults.Items.Clear();

                if (!string.IsNullOrEmpty(txtFind.Text))
                {
                    if ((string.IsNullOrEmpty(searchAndDelimiter)) || (string.IsNullOrWhiteSpace(searchAndDelimiter)))
                    {
                        //  no delimiter for AND/OR search

                        if (!SearchUtils.FindToken(txtFind.Text, m_ActiveSearchProject, out searchResults, out result))
                        {
                            Audit(result, method, AuditSeverity.Error, Log.LINE());
                            MessageBox.Show(result, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //  search with search AND/OR delimiter

                        string[] delimiter = new string[1];
                        delimiter[0] = searchAndDelimiter;

                        string[] lsText = (txtFind.Text).Split(delimiter, System.StringSplitOptions.RemoveEmptyEntries);

                        List<string> lText = new List<string>();

                        foreach (string lsTextItem in lsText)
                        {
                            lText.Add(lsTextItem);
                        }

                        if (!SearchUtils.FindTokens(lText, m_ActiveSearchProject, out searchResults, out result))
                        {
                            Audit(result, method, AuditSeverity.Error, Log.LINE());
                            MessageBox.Show(result, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if ((searchResults != null) && (searchResults.Count > 0))
                    { 
                        foreach (SearchResult searchResult in searchResults)
                        {
                            string[] arr;

                            ListViewItem itm;

                            switch (m_ActiveSearchProject.Type)
                            {
                                case SearchProjectType.DirectoriesProject:
                                    arr = new string[4];

                                    arr[0] = searchResult.Directoty;
                                    arr[1] = searchResult.File;
                                    arr[2] = searchResult.Line.ToString();
                                    arr[3] = searchResult.FullPath;
                                    break;

                                case SearchProjectType.SolutionsProject:
                                    arr = new string[5];
                                        
                                    arr[0] = searchResult.Solution;
                                    arr[1] = searchResult.Project;
                                    arr[2] = searchResult.File;
                                    arr[3] = searchResult.Line.ToString();
                                    arr[4] = searchResult.FullPath;
                                    break;

                                default:
                                    return;
                            }

                            itm = new ListViewItem(arr);
                            lvResults.Items.Add(itm);
                        }

                        txtNumberOfHits.Text = searchResults.Count.ToString();
                    }
                    else
                    {
                        message = "No Results";

                        Audit(message, method, AuditSeverity.Information, Log.LINE());
                        MessageBox.Show(message, "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    message = "No Text To Search";

                    Audit(message, method, AuditSeverity.Information, Log.LINE());
                    MessageBox.Show(message, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string message;

            #endregion

            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (lvResults.SelectedItems != null)
                    {
                        int sourceFilePathIndex;
                        int lineNumberIndex;

                        switch (m_ActiveSearchProject.Type)
                        {
                            case SearchProjectType.DirectoriesProject:
                                sourceFilePathIndex = 3;
                                lineNumberIndex = 2;
                                break;

                            case SearchProjectType.SolutionsProject:
                                sourceFilePathIndex = 4;
                                lineNumberIndex = 3;
                                break;

                            default:
                                return;
                        }

                        string sourceFilePath = lvResults.SelectedItems[0].SubItems[sourceFilePathIndex].Text;

                        int lineNumber = (int.TryParse(lvResults.SelectedItems[0].SubItems[lineNumberIndex].Text, out lineNumber)) ? lineNumber : Constants.NONE;

                        if (!File.Exists(sourceFilePath))
                        {
                            message = "Source File '" + sourceFilePath + "' Does Not Exist";

                            Audit(message, method, AuditSeverity.Error, Log.LINE());
                            MessageBox.Show(message, "Failed Openning File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        OpenFileAtLine(sourceFilePath, lineNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Failed Openning File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);

                txtFind.Items.Add(txtFind.Text);
            }
        }

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            if (cboSearchType.Text == "AND")
            {
                m_SearchType = ContainsAll;
            }
            else
            {
                m_SearchType = ContainsAtLeastOneOf;
            }
        }

        private void btnLoadSearchedItemsFromFile_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string result;

            List<string> lines;

            int i;

            #endregion

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = m_Path;
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!Utils.ReadFileLines(openFileDialog.FileName, out lines, out result))
                        {
                            Audit(result, method, AuditSeverity.Error, Log.LINE());
                            MessageBox.Show(result, "Failed Loading List Of Searched Items", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        if (lines == null)
                        {
                            Audit(result, method, AuditSeverity.Error, Log.LINE());
                            MessageBox.Show(result, "List Of Searched Items Is Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        string searchedItems = string.Empty;
                        for (i = 0; i < (lines.Count - 1); i++)
                        {
                            searchedItems += lines[i] + searchAndDelimiter;
                        }

                        searchedItems += lines[i];

                        txtFind.Text = searchedItems;
                    }
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Load Searched Items From File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                if (m_ActiveSearchProject != null)
                {
                    m_ActiveSearchProject.CaseSensitive = chkCaseSensitive.Checked;
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(ex.Message, "Case Sensitive Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion

        #region Utils

        private void OpenFileAtLine(string file, int line)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string result;

            #endregion

            try
            {
                switch (EDITOR_USED)
                {
                    case Editors.Notepad:
                        if (!OpenWithNotepad(file, line, out result))
                        {
                            Audit(result, method, AuditSeverity.Warning, Log.LINE());
                            MessageBox.Show(result, "Failed Openning File '" + file + "' With " + Utils.EditorToString(EDITOR_USED), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;

                    case Editors.NotepadPlusPlus:
                        if (!OpenWithNotepadPlusPlus(file, line, out result))
                        {
                            Audit(result, method, AuditSeverity.Warning, Log.LINE());
                            MessageBox.Show(result, "Failed Openning File '" + file + "' With " + Utils.EditorToString(EDITOR_USED), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;

                    case Editors.VisualStudio2005:
                    case Editors.VisualStudio2010:
                    case Editors.VisualStudio2013:
                    case Editors.VisualStudio2012:
                    case Editors.VisualStudio2017:
                        if (!OpenWithVisualStudio(file, line, out result))
                        {
                            Audit(result, method, AuditSeverity.Warning, Log.LINE());
                            MessageBox.Show(result, "Failed Openning File '" + file +"' With " + Utils.EditorToString(EDITOR_USED), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Audit(e.Message, method, AuditSeverity.Error, Log.LINE());
                MessageBox.Show(e.Message);
            }
        }

        private bool OpenWithVisualStudio(string file, int line, out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            object visualStudio;
            object ops;
            object window;
            object selection;

            #endregion

            result = "";

            try
            {
                switch (EDITOR_USED)
                {
                    case Editors.VisualStudio2005:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.9.0");
                        break;

                    case Editors.VisualStudio2010:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.10.0");
                        break;

                    case Editors.VisualStudio2012:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.11.0");
                        break;

                    case Editors.VisualStudio2013:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.12.0");
                        break;

                    case Editors.VisualStudio2015:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.14.0");
                        break;

                    case Editors.VisualStudio2017:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    default:
                        result = "Unknown editor type";
                        return false;
                }

                ops = visualStudio.GetType().InvokeMember("ItemOperations", BindingFlags.GetProperty, null, visualStudio, null);
                window = ops.GetType().InvokeMember("OpenFile", BindingFlags.InvokeMethod, null, ops, new object[] { file });
                selection = window.GetType().InvokeMember("Selection", BindingFlags.GetProperty, null, window, null);
                selection.GetType().InvokeMember("GotoLine", BindingFlags.InvokeMethod, null, selection, new object[] { line, true });

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool OpenWithNotepad(string file, int line, out string result)
        {
            #region Data Members            

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = "";

            try
            {
                Process.Start("notepad.exe", file);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool OpenWithNotepadPlusPlus(string file, int line, out string result)
        {
            #region Data Members            

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = "";

            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat("\"{0}\" -n{1}", file, line);
                Process.Start("notepad++", sb.ToString());

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool FindCriteriaFilesNumbers(string path,      
                                              string criteriaList, 
                                              out int numberOfMatchingFiles, 
                                              out int numberOfMatchingTestFiles, 
                                              out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            result = string.Empty;

            numberOfMatchingFiles = 0;
            numberOfMatchingTestFiles = 0;

            try
            {
                string[] criteriaSplitList = criteriaList.Split(';');

                foreach (string criteria in criteriaSplitList)
                {
                    string[] matchingFiles = Directory.GetFiles(path, criteria, SearchOption.AllDirectories);

                    if (matchingFiles != null)
                    {
                        numberOfMatchingFiles += matchingFiles.Length;

                        foreach (string fileName in matchingFiles)
                        {
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                if (fileName.ToLower().Contains("test"))
                                {
                                    numberOfMatchingTestFiles++;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool EnsureEditorActive(out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            result = string.Empty;

            try
            {
                if (EDITOR_USED == Editors.Unknown)
                {
                    EDITOR_USED = Editors.Notepad;
                }

                if ((EDITOR_USED == Editors.Notepad) || (EDITOR_USED == Editors.NotepadPlusPlus))
                {
                    return true;
                }                

                Process[] pname = Process.GetProcessesByName("devenv");

                foreach (Process process in pname)
                {
                    string runningVisualStudio = process.MainModule.FileVersionInfo.FileDescription;
                    string editorUsed = Utils.EditorToString(EDITOR_USED);

                    if (runningVisualStudio.IndexOf(editorUsed) != Constants.NONE)
                    {
                        result = string.Format("Running Visual Studio[{0}] Is Not The Editor Used[{1}]", runningVisualStudio, editorUsed);

                        return false;
                    }
                }

                if (!OpenUsedVisualStudio(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Audit(e.Message, method, AuditSeverity.Error, Log.LINE());
                result = string.Format("Failed Ensuring Editor '{0}' Active. {1}.", Utils.EditorToString(EDITOR_USED), e.Message);

                return false;
            }
        }

        private bool OpenUsedVisualStudio(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string batchFilename;
            string batchFileOutput;
            string visualStudioExePath;
            string message;

            bool openFailed;

            #endregion

            openFailed = false;
            result = string.Empty;

            try
            {
                batchFilename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ObjectInProjectConstants.BATCH_FILE_NAME;

                if (File.Exists(batchFilename))
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.FileName = batchFilename;
                    startInfo.Arguments = EDITOR_USED.ToString().Substring(12);

                    process.StartInfo = startInfo;
                    process.Start();

                    batchFileOutput = process.StandardOutput.ReadToEnd();

                    batchFileOutput = batchFileOutput.Replace("\r", String.Empty);
                    batchFileOutput = batchFileOutput.Replace("\t", String.Empty);
                    batchFileOutput = batchFileOutput.Replace("\n", String.Empty);

                    visualStudioExePath = batchFileOutput + ObjectInProjectConstants.DEVENV_PATH_SUFFIX;

                    if (!File.Exists(visualStudioExePath))
                    {
                        message = "Visual Studio Execution File Does Not Exist";

                        Audit(message, method, AuditSeverity.Error, Log.LINE());
                        result = string.Format("Failed Starting '{0}'. {1}.", Utils.EditorToString(EDITOR_USED), message);

                        openFailed = true;

                        return false;
                    }

                    Process.Start(visualStudioExePath);

                    process.WaitForExit();
                }
                else
                {
                    message = "'" + batchFilename + "' does not exist";

                    Audit(message, method, AuditSeverity.Error, Log.LINE());
                    result = string.Format("Failed Starting '{0}'. {1}.", Utils.EditorToString(EDITOR_USED), message);

                    openFailed = true;

                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed Openning '" + Utils.EditorToString(EDITOR_USED) + "'", MessageBoxButtons.OK, MessageBoxIcon.Error);

                openFailed = true;

                return false;
            }
            finally
            {
                if (openFailed)
                {
                    EDITOR_USED = Editors.Notepad;
                }
            }
        }

        private bool GetActiveEditors(List<EditorInformation> editorsInformation, out List<Editors> activeEditors, out string result)
        {
            result = string.Empty;

            activeEditors = null;

            try
            {
                if ((editorsInformation == null) || (editorsInformation.Count == 0))
                {
                    result = "No Active Editors";

                    return false;
                }

                activeEditors = new List<Editors>();
                foreach (EditorInformation editor in editorsInformation)
                {
                    activeEditors.Add(editor.Editor);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Configuration

        private bool LoadConfiguration(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;
            string json;
            
            #endregion

            result = string.Empty;

            try
            {
                if (!File.Exists(m_FullFilename))
                {
                    result = "'" + m_FullFilename + "' Does Not Exist";

                    Audit(result, method, AuditSeverity.Information, Log.LINE());

                    return false;
                }

                json = File.ReadAllText(m_FullFilename);
                m_Configuration = JsonConvert.DeserializeObject<SearchProjects>(json); 

                return (m_Configuration != null);
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool SaveConfiguration(out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                return SaveConfiguration(m_Configuration, out result);
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
            }
        }

        private bool SaveConfiguration(SearchProjects searchProjects, out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = "";

            try
            {
                if (searchProjects == null)
                {
                    Audit("Configuration Is Null", method, AuditSeverity.Warning, Log.LINE());

                    return false;
                }

                string json = JsonConvert.SerializeObject(searchProjects);
                File.WriteAllText(m_FullFilename, json);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, AuditSeverity.Error, Log.LINE());

                return false;
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
