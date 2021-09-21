using ObjectInProject.Search;
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

namespace ObjectInProject.Tests.ProjectFilesList
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

            txtFilePath.Text = @"C:\Temp\Example Projects For Search\CS Example Project For Search\Solution\ClassLibrary\ClassLibrary.csproj";
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
                    openFileDialog.Filter = "Project Files (*.*proj)|*.*proj";
                    openFileDialog.Title = "Open Project File";

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

                List<string> files;

                if (!string.IsNullOrEmpty(txtFilePath.Text))
                {
                    MessageBox.Show($"Project File Not Given", "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!File.Exists(txtFilePath.Text))
                {
                    MessageBox.Show($"Project File '{txtFilePath.Text}' Does Not Exist", "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;    
                }

                if (!ParseFile.ParseProjectFile(txtFilePath.Text, out files, out result))
                {
                    MessageBox.Show(result, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (!FillFiles(files, out result))
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

        private bool FillFiles(List<string> files, out string result)
        {
            result = string.Empty;

            try
            {
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
