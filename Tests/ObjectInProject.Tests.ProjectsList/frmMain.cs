using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectInProject.Search;

namespace ObjectInProject.Tests.ProjectsList
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            Location = Cursor.Position;
        }

        #endregion

        #region Gui

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    openFileDialog.Filter = "Solution Files (*.sln)|*.sln";
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                string result;

                List<string> projectFiles;

                if (!ParseFile.ParseSolutionFile(txtFilePath.Text, out projectFiles, out result))
                {
                    MessageBox.Show(result, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!FillProjects(projectFiles, out result))
                {
                    MessageBox.Show(result, "Fill Projects Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool FillProjects(List<string> projectFiles, out string result)
        {
            result = string.Empty;

            try
            {
                if ((projectFiles == null) || (projectFiles.Count == 0))
                {
                    result = "Project Files List Is Null Or Empty";

                    return false;
                }

                dgvFiles.Rows.Clear();
                foreach (string projectFile in projectFiles)
                {
                    dgvFiles.Rows.Add(projectFile);
                }

                dgvFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fill Projects Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion
    }
}
