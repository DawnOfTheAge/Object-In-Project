using ObjectInProject.Search;
using ObjectInProject.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ObjectInProject.Tests.SolutionFilesList
{
    public partial class frmMain : Form
    {
        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Location = Cursor.Position;

            txtFilePath.Text = //@"C:\Temp\Example Projects For Search\CS Example Project For Search\Solution\Solution.sln";
                               @"D:\GitDev\MOPS_WEB\Applications\OpticalAnalysisService\OpticalAnalysisService.sln";
        }

        #endregion

        #region Gui

        private void BtnFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    openFileDialog.Filter = "Project Files (*.sln)|*.sln";
                    openFileDialog.Title = "Open Solution File";

                    DialogResult result = openFileDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        txtFilePath.Text = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> files;

                if (string.IsNullOrEmpty(txtFilePath.Text))
                {
                    MessageBox.Show($"Solution File Not Given", "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!File.Exists(txtFilePath.Text))
                {
                    MessageBox.Show($"Solution File '{txtFilePath.Text}' Does Not Exist", "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!ParseFile.ParseSolutionFile(txtFilePath.Text, out List<string> projectFiles, out string result))
                {
                    MessageBox.Show(result, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if ((projectFiles == null) || (projectFiles.Count == 0))
                {
                    MessageBox.Show($"No Project Files In Solution '{txtFilePath.Text}'", "Fill Files Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                files = new List<string>();
                foreach (string projectFile in projectFiles)
                {
                    if (File.Exists(projectFile))
                    {
                        if (!ParseFile.ParseProjectFile(projectFile, out projectFiles, out result))
                        {
                            MessageBox.Show(result, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        files.AddRange(projectFiles);
                    }                    
                }

                if (files.Count > 0)
                {
                    if (!FillFiles(files, out result))
                    {
                        MessageBox.Show(result, "Fill Files Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool FillFiles(List<string> files, out string result)
        {
            result = string.Empty;

            try
            {
                lblNumberOfFiles.Text = "Files - ";

                if ((files == null) || (files.Count == 0))
                {
                    result = "Files List Is Null Or Empty";

                    return false;
                }

                dgvFiles.Rows.Clear();
                foreach (string file in files)
                {
                    dgvFiles.Rows.Add(file);
                }

                dgvFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                lblNumberOfFiles.Text = $"{lblNumberOfFiles.Text}{files.Count}";

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill Files Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }        

        #endregion
    }
}
