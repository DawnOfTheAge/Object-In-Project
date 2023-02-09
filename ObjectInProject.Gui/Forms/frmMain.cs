using Microsoft.Build.Construction;
using Newtonsoft.Json;
using ObjectInProject.Common;
using ObjectInProject.EditorsInformation;
using ObjectInProject.Search;
using ObjectInProject.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ObjectInProject.Gui
{
    public partial class frmMain : Form
    {
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

        private SearchUtils searchUtils;

        private VisualStudiosInstalled visualStudiosInstalled;

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
        
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                DialogResult dialogResult = MessageBox.Show("Save Configuration?", "Save Configuration On Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveConfiguration(out string result);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Warning);
                MessageBox.Show(ex.Message, "Error Closing Object In Project", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #endregion

        #region Initialize

        private void FrmMain_Load(object sender, EventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                Location = Cursor.Position;

                if (!Initialize(out string result))
                {
                    Audit(result, method, LINE(), AuditSeverity.Warning);
                    MessageBox.Show(result, "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Warning);
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
                if (!Prologue(out result))
                { 
                    return false;
                }

                #region Initialize Variables

                m_SearchType = ContainsAll;

                directories = new List<string>();
                fileTypeFilter = new List<string>();

                m_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                m_FullFilename = $@"{m_Path}\{ObjectInProjectConstants.APPLICATION_NAME}{ObjectInProjectConstants.JSON_EXTENSION}";

                m_numberOfProjects = 0;
                m_numberOfFiles = 0;
                m_numberOfTestFiles = 0;

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                #endregion

                searchUtils = new SearchUtils();

                visualStudiosInstalled = new VisualStudiosInstalled();
                if (!visualStudiosInstalled.GetVisualStudiosInstalled(out m_VisualStudio, out result))
                {
                    result = $"Visual Studios Detection Failure. {result}";
                    Audit(result, method, LINE(), AuditSeverity.Information);
                }

                if (LoadConfiguration(out result))
                {
                    if (m_Configuration.AuditSettings == null)
                    {
                        m_Configuration.AuditSettings = new AuditProperties(true);
                    }

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

                                        string criteriaList = string.IsNullOrEmpty(m_ActiveSearchProject.FileTypeFilter) ? m_ActiveSearchProject.FileTypeFilter : "*.*";

                                        if (FindCriteriaFilesNumbers(directory,
                                                                     criteriaList,
                                                                     out int numberOfFiles,
                                                                     out int numberOfTestFiles,
                                                                     out result))
                                        {
                                            m_numberOfFiles += numberOfFiles;
                                            m_numberOfTestFiles += numberOfTestFiles;
                                        }
                                    }
                                    else
                                    {
                                        Audit(result, method, LINE(), AuditSeverity.Information);
                                        result = $"Path '{directory}' Does Not Exist";

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
                                            Audit(result, method, LINE(), AuditSeverity.Information);
                                            result = $"Solution File '{currentSolutionFile}' Parse Error";

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

                                Audit(message, method, LINE(), AuditSeverity.Error);
                                //result = $"Initialization Error. {message}";

                                return false;
                        }
                    }
                }
                else
                {
                    result = $"Initialize Error. {result}";
                    Audit(result, method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                if (!Epilogue(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                result = $"Initialize Error. {ex.Message}";
                
                return false;
            }
        }

        private bool Prologue(out string result)
        {
            result = string.Empty;

            try
            {
                //bool success = EditorUtils.OpenVisualStudio(@"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe",
                //                                            @"C:\Projects\Object In Project Browser\ObjectInProject.Gui\Forms\frmMain.cs", 
                //                                            283, 
                //                                            out result);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool Epilogue(out string result)
        {
            result = string.Empty;

            try
            {
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Parse        

        private bool ParseSolutionFileText(string solutionFilename, string solutionFileText, out Solution solution, out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = string.Empty;

            solution = new Solution
            {
                Name = solutionFilename
            };

            try
            {
                foreach (ProjectInSolution currentProjectFile in SolutionFile.Parse(solutionFilename).ProjectsInOrder)
                {
                    if (ParseProjectFile(currentProjectFile.AbsolutePath, out Project project, out result))
                    {
                        solution.Projects.Add(project);
                        solution.Projects[solution.Projects.Count - 1].Id = solution.Projects.Count;

                        m_numberOfProjects++;
                    }
                    else
                    {
                        Audit(result, method, LINE(), AuditSeverity.Warning);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Audit(e.Message, method, LINE(), AuditSeverity.Error);

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

            result = string.Empty;

            List<string> lines;

            project = new Project();

            bool goOn = true;

            projectFileExtension = Path.GetExtension(projectFile);

            try
            {
                if (string.IsNullOrEmpty(projectFile))
                {
                    result = "Project File Is Null Or Empty";

                    return false;
                }

                if (!projectFileExtension.ToLower().Equals(ObjectInProjectConstants.CS_PROJECT_FILE_EXTENSION) &&
                    !projectFileExtension.ToLower().Equals(ObjectInProjectConstants.CS_PROJECT_FILE_EXTENSION))
                {
                    result = $"'{project}' Not A Project File";

                    return false;
                }

                projectFileDataFilename = projectFile;

                if (!File.Exists(projectFileDataFilename))
                {
                    result = $"File '{projectFileDataFilename}' Does Not Exist";

                    Audit(result, method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                Audit($"Parsing '{projectFileDataFilename}' ...", method, LINE(), AuditSeverity.Information);

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
                            if (startCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CS_FILE_EXTENSION, startCsIndex);
                            if (endCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            CsFile csFile = new CsFile
                            {
                                Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CS_PROPERTY.Length + 1),
                                                                                          endCsIndex - startCsIndex - (ObjectInProjectConstants.CS_PROPERTY.Length - ObjectInProjectConstants.CS_FILE_EXTENSION.Length + 1))
                            };

                            if (FilesUtils.ReadFileLines(csFile.Name, out lines, out result))
                            {
                                csFile.Lines = lines;

                                project.Files.Add(csFile);

                                m_numberOfFiles++;
                                if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != ObjectInProjectConstants.NONE)
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
                            if (startCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION, startCsIndex);
                            if (endCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            CsFile csFile = new CsFile
                            {
                                Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length + 1),
                                                                                             endCsIndex - startCsIndex - (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION.Length + 1))
                            };

                            if (FilesUtils.ReadFileLines(csFile.Name, out lines, out result))
                            {
                                csFile.Lines = lines;

                                project.Files.Add(csFile);

                                m_numberOfFiles++;
                                if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != ObjectInProjectConstants.NONE)
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
                            if (startCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            endCsIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION, startCsIndex);
                            if (endCsIndex == ObjectInProjectConstants.NONE)
                            {
                                break;
                            }

                            CsFile csFile = new CsFile
                            {
                                Name = projectFilePath + "\\" + projectFileData.Substring(startCsIndex + (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length + 1),
                                                                                          endCsIndex - startCsIndex - (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION.Length + 1))
                            };

                            if (FilesUtils.ReadFileLines(csFile.Name, out lines, out result))
                            {
                                csFile.Lines = lines;

                                project.Files.Add(csFile);

                                m_numberOfFiles++;
                                if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != ObjectInProjectConstants.NONE)
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
                        result = $"Wrong Project Type '{projectFileExtension}'";

                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;
                Audit(result, method, LINE(), AuditSeverity.Error);

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
                if (line.IndexOf(item) != ObjectInProjectConstants.NONE)
                {
                    return true;
                }
            }

            result = "No item from list was found in '" + line + "'";

            return false;
        }

        #endregion

        #region GUI

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                lvResults.Height = Height - lvResults.Top - 90;

                txtFind.Width = (Width / 3) * 2;
                lvResults.Width = Width - lvResults.Left - 50;

                txtFind.Location = new Point(10, 50);
                btnFind.Location = new Point(txtFind.Left + txtFind.Width + 10, txtFind.Top -  (btnFind.Height / 3));
                btnClear.Location = new Point(btnFind.Left + btnFind.Width + 10, btnFind.Top);
                btnLoadSearchedItemsFromFile.Location = new Point(btnClear.Left + btnClear.Width + 20, btnClear.Top + (btnLoadSearchedItemsFromFile.Height / 2));
                lvResults.Location = new Point(10, txtFind.Top + txtFind.Height + 30);
                chkCaseSensitive.Location = new Point(txtFind.Left, txtFind.Top + txtFind.Height + 5);
                lblFileTypeFilters.Location = new Point(chkCaseSensitive.Left + chkCaseSensitive.Width + 10, chkCaseSensitive.Top);
                cboFileTypeFilters.Location = new Point(lblFileTypeFilters.Left+ lblFileTypeFilters.Width + 5, lblFileTypeFilters.Top - 2);
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Warning);
                MessageBox.Show(ex.Message, "Error Resizing Form", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        #region Main menu

        private void MnuClearResults_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            txtNumberOfHits.Text = "0";
            pbFiles.Value = 0;
        }

        private void MnuClearSearchHistory_Click(object sender, EventArgs e)
        {
            txtFind.Items.Clear();
            txtFind.Text = "";
        }

        private void MnuShowSearchProjectsTree_Click(object sender, EventArgs e)
        {
            if (!GetActiveEditors(m_VisualStudio, out m_ActiveEditors, out string result))
            {
                MessageBox.Show(result, "Active Editors Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            frmSearchProjectsTree searchProjectsTree = new frmSearchProjectsTree(m_Configuration, m_ActiveEditors);
            searchProjectsTree.Reply += SearchProjectsTree_Reply;
            searchProjectsTree.Show();
        }

        private bool SearchProjectsTree_Reply(object message, out object reply, out string result)
        {
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

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Initializations

        private bool InitializeGui(out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                #region Check Existing Configuration & Search Jobs

                if ((m_Configuration == null) || (m_Configuration.SearchProjectsList == null))
                {
                    result = $"Configuration Error. No Configuration Defined";
                    Audit(result, method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                #endregion

                if (!InitializeStatusStrip(out result))
                {
                    return false;
                }

                FrmMain_Resize(null, null);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        private bool InitializeStatusStrip(out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

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

                if ((m_Configuration.ActiveSearchProjectIndex != ObjectInProjectConstants.NONE) &&
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

                    result = $"Configuration Error. No Active Search Project Defined";
                    Audit(result, method, LINE(), AuditSeverity.Error);

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
                    result = $"Initialize Error. {result}";
                    Audit(result, method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                if ((m_ActiveSearchProject != null) && (!string.IsNullOrEmpty(m_ActiveSearchProject.Editor)))
                {
                    EDITOR_USED = EditorUtils.EditorToEnum(m_ActiveSearchProject.Editor);
                }
                else
                {
                    EDITOR_USED = Editors.Notepad;
                }

                Audit($"Editor Used: {EDITOR_USED}", method, LINE(), AuditSeverity.Information);


                if (!EnsureEditorActive(out result))
                {
                    result = $"Editor Definition Error. {result}.";
                    Audit(result, method, LINE(), AuditSeverity.Warning);

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
                    txtNumberOfProjects.Visible = (m_ActiveSearchProject.Type == SearchProjectType.SolutionsProject);
                    lblNumberOfProjects.Visible = (m_ActiveSearchProject.Type == SearchProjectType.SolutionsProject);
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

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        #endregion

        #region Events

        private void ChangeSearchJob_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                ToolStripMenuItem selectedSearchJob = (ToolStripMenuItem)sender;

                txtActiveSearchJob.Text = selectedSearchJob.Text;

                if (!m_Configuration.SetAsActive(txtActiveSearchJob.Text, out string result))
                {
                    Audit(result, method, LINE(), AuditSeverity.Warning);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
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
                Audit(ex.Message, method, LINE(), AuditSeverity.Error); ;
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
                Audit(ex.Message, method, LINE(), AuditSeverity.Error); ;
            }
        }
        
        private void ChkCaseSensitive_CheckedChanged(object sender, EventArgs e)
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
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                MessageBox.Show(ex.Message, "Case Sensitive Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void TxtFind_KeyDown(object sender, KeyEventArgs e)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnFind_Click(null, null);

                    txtFind.Items.Add(txtFind.Text);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
            }
        }

        private void CboSearchType_SelectedIndexChanged(object sender, EventArgs e)
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

        #endregion

        #region List View

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
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }
         
        private void LvResults_MouseDoubleClick(object sender, MouseEventArgs e)
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

                        int lineNumber = (int.TryParse(lvResults.SelectedItems[0].SubItems[lineNumberIndex].Text, out lineNumber)) ? lineNumber : ObjectInProjectConstants.NONE;

                        if (!File.Exists(sourceFilePath))
                        {
                            message = "Source File '" + sourceFilePath + "' Does Not Exist";

                            Audit(message, method, LINE(), AuditSeverity.Error);
                            MessageBox.Show(message, "Failed Openning File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        OpenFileAtLine(sourceFilePath, lineNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                MessageBox.Show(ex.Message, "Failed Openning File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Buttons
        
        private void BtnFind_Click(object sender, EventArgs e)
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

                    Audit(result, method, LINE(), AuditSeverity.Information);
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

                        if (!searchUtils.FindToken(txtFind.Text, m_ActiveSearchProject, out searchResults, out result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Error);
                            MessageBox.Show(result, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //  search with search AND/OR delimiter

                        string[] delimiter = new string[1];
                        delimiter[0] = searchAndDelimiter;

                        string[] lsText = (txtFind.Text).Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                        List<string> lText = new List<string>();

                        foreach (string lsTextItem in lsText)
                        {
                            lText.Add(lsTextItem);
                        }

                        if (!searchUtils.FindTokens(lText, m_ActiveSearchProject, out searchResults, out result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Error);
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

                        Audit(message, method, LINE(), AuditSeverity.Information);
                        MessageBox.Show(message, "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    message = "No Text To Search";

                    Audit(message, method, LINE(), AuditSeverity.Information);
                    MessageBox.Show(message, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                MessageBox.Show(ex.Message, "Failed Finding", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            try
            {
                txtFind.Text = string.Empty;
                lvResults.Items.Clear();    
            }
            catch (Exception ex)
            {
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                MessageBox.Show(ex.Message, "Clear Searched Results Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadSearchedItemsFromFile_Click(object sender, EventArgs e)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

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
                        if (!FilesUtils.ReadFileLines(openFileDialog.FileName, out List<string> lines, out string result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Error);
                            MessageBox.Show(result, "Failed Loading List Of Searched Items", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        if (lines == null)
                        {
                            Audit(result, method, LINE(), AuditSeverity.Error);
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
                Audit(ex.Message, method, LINE(), AuditSeverity.Error);
                MessageBox.Show(ex.Message, "Load Searched Items From File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Utils

        private void OpenFileAtLine(string file, int line)
        {
            string method = MethodBase.GetCurrentMethod().Name;
            string result;

            try
            {
                switch (EDITOR_USED)
                {
                    case Editors.Notepad:
                        if (!OpenWithNotepad(file, out result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Warning);
                            MessageBox.Show(result, 
                                            $"Failed Openning File '{file}' With {EditorUtils.EditorToString(EDITOR_USED)}", 
                                            MessageBoxButtons.OK, 
                                            MessageBoxIcon.Warning);
                        }
                        break;

                    case Editors.NotepadPlusPlus:
                        if (!OpenWithNotepadPlusPlus(file, line, out result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Warning);
                            MessageBox.Show(result,
                                            $"Failed Openning File '{file}' With {EditorUtils.EditorToString(EDITOR_USED)}",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        break;

                    case Editors.VisualStudio2005:
                    case Editors.VisualStudio2010:
                    case Editors.VisualStudio2013:
                    case Editors.VisualStudio2012:
                    case Editors.VisualStudio2017:
                    case Editors.VisualStudio2019:
                    case Editors.VisualStudio2022:
                        if (!OpenWithVisualStudio(EDITOR_USED, file, line, out result))
                        {
                            Audit(result, method, LINE(), AuditSeverity.Warning);
                            MessageBox.Show(result,
                                            $"Failed Openning File '{file}' With {EditorUtils.EditorToString(EDITOR_USED)}",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Audit(e.Message, method, LINE(), AuditSeverity.Error);
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

            result = string.Empty;

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

                    case Editors.VisualStudio2019:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    case Editors.VisualStudio2022:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    default:
                        result = $"Unknown Editor Type[{EDITOR_USED}]";
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

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        private bool OpenWithVisualStudio(Editors editor, string file, int line, out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            result = string.Empty;
            try
            {
                if (!EditorUtils.VisualStudioPath(editor, out string visualStudioPath, out result))
                {
                    return false;
                }

                if (!File.Exists(visualStudioPath))
                {
                    result = $"'{visualStudioPath}' Does Not Exist";

                    return false;
                }

                if (!EditorUtils.OpenVisualStudio(visualStudioPath, file, line, out result))
                {
                    result = $"Failed Openning {editor}. {result}";

                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        private bool OpenWithNotepad(string file, out string result)
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

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        private bool OpenWithNotepadPlusPlus(string file, int line, out string result)
        {
            #region Data Members            

            string method = MethodBase.GetCurrentMethod().Name;

            #endregion

            result = string.Empty;

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

                Audit(result, method, LINE(), AuditSeverity.Error);

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

                Audit(result, method, LINE(), AuditSeverity.Error);

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
                    ProcessModule processModule = process.MainModule;

                    string fullPath = process.MainModule.FileName;
                    string runningVisualStudio = process.MainModule.FileVersionInfo.FileDescription;
                    string editorUsed = EditorUtils.EditorToString(EDITOR_USED);

                    if (!runningVisualStudio.Contains(editorUsed))
                    {
                        continue;
                    }
                    else
                    {
                        Audit($"Running Visual Studio[{runningVisualStudio}] Editor Used[{editorUsed}]", 
                              method, 
                              LINE(), 
                              AuditSeverity.Information);
                        Audit($"DevEnv.exe Full Path[{fullPath}]",
                              method,
                              LINE(),
                              AuditSeverity.Information);

                        return true;
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
                Audit(e.Message, method, LINE(), AuditSeverity.Error);
                result = $"Failed Ensuring Editor '{EditorUtils.EditorToString(EDITOR_USED)}' Active. {e.Message}";

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
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = batchFilename,
                        Arguments = EDITOR_USED.ToString().Substring(12)
                    };

                    process.StartInfo = startInfo;
                    process.Start();

                    batchFileOutput = process.StandardOutput.ReadToEnd();

                    batchFileOutput = batchFileOutput.Replace("\r", string.Empty);
                    batchFileOutput = batchFileOutput.Replace("\t", string.Empty);
                    batchFileOutput = batchFileOutput.Replace("\n", string.Empty);

                    visualStudioExePath = $"{batchFileOutput}{ObjectInProjectConstants.DEVENV_PATH_SUFFIX}";

                    if (!File.Exists(visualStudioExePath))
                    {
                        message = "Visual Studio Execution File Does Not Exist";

                        Audit(message, method, LINE(), AuditSeverity.Error);
                        result = $"Failed Starting '{EditorUtils.EditorToString(EDITOR_USED)}'. {message}";

                        openFailed = true;

                        return false;
                    }

                    Process.Start(visualStudioExePath);

                    process.WaitForExit();
                }
                else
                {
                    message = $"'{batchFilename}' Does Not Exist";

                    Audit(message, method, LINE(), AuditSeverity.Error);
                    result = $"Failed Starting '{EditorUtils.EditorToString(EDITOR_USED)}'. {message}";

                    openFailed = true;

                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed Openning '" + EditorUtils.EditorToString(EDITOR_USED) + "'", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

                    Audit(result, method, LINE(), AuditSeverity.Information);

                    return false;
                }

                json = File.ReadAllText(m_FullFilename);
                m_Configuration = JsonConvert.DeserializeObject<SearchProjects>(json); 

                return (m_Configuration != null);
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, LINE(), AuditSeverity.Error);

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

                Audit(result, method, LINE(), AuditSeverity.Error);

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
                    Audit("Configuration Is Null", method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                string json = JsonConvert.SerializeObject(searchProjects);
                File.WriteAllText(m_FullFilename, json);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        #endregion

        #region Audit

        private Color SeverityColor(AuditSeverity auditSeverity)
        {
            switch (auditSeverity)
            {
                case AuditSeverity.Information:
                    return Color.SeaShell;

                case AuditSeverity.Important:
                    return Color.Aqua;

                case AuditSeverity.Warning:
                    return Color.Coral;

                case AuditSeverity.Error:
                    return Color.Red;

                case AuditSeverity.Critical:
                    return Color.Purple;

                default:
                    return Color.FromArgb(0, 0, 0, 0);
            }
        }

        private void Settings_Message(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            try
            {
                Audit(message, method, module, line, auditSeverity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH-mm-ss dd-MM-yyyy}]:<{auditSeverity}>:<{module}>:<{method}> {message + ". Error:" + ex.Message}");
            }
        }

        private void Audit(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            string dateTime = DateTime.Now.ToString("HH:mm:ss.fff");

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new AuditMessage(Audit), message, method, module, line, auditSeverity);
                }
                else
                {
                    if (m_Configuration.AuditSettings.AuditOn)
                    {
                        dgvAudit.Rows.Insert(0, new string[] { dateTime, auditSeverity.ToString(), module, method, line.ToString(), message });
                        dgvAudit.Rows[0].DefaultCellStyle.BackColor = SeverityColor(auditSeverity);

                        dgvAudit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                    else
                    {
                        if ((auditSeverity == AuditSeverity.Error) || (auditSeverity == AuditSeverity.Warning))
                        {
                            MessageBox.Show(message,
                                            module,
                                            MessageBoxButtons.OK,
                                            (auditSeverity == AuditSeverity.Error) ?
                                            MessageBoxIcon.Error :
                                            MessageBoxIcon.Warning);
                        }
                    }

                    Console.WriteLine($"[{dateTime}]:<{auditSeverity}>:<{module}>:<{method}:{line}> {message}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"[{dateTime}]:<{auditSeverity}>:<{module}>:<{method}:{line}> {message + ". Audit Error:" + e.Message}");
            }
        }

        private void Audit(string message, string method, int line, AuditSeverity auditSeverity)
        {
            Audit(message, method, "MOPS Config Tool", line, auditSeverity);
        }

        public static int LINE([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }

        #endregion       
    }
}
