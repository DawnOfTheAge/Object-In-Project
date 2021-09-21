using ObjectInProject.Common;
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

namespace ObjectInProject.Tests.File
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

            chkCaseSensitive.Checked = false;

            rbAnd.Checked = true;
            rbOr.Checked = false;

            txtTokens.Text = "ttl , ad";
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
                    DialogResult result = openFileDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                    {
                        txtFilePath.Text = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvResults.Rows.Clear();

                if (!System.IO.File.Exists(txtFilePath.Text))
                {
                    MessageBox.Show(txtFilePath.Text, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                string result;                

                List<string> lTokens = txtTokens.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                SearchedFile searchedFile;

                if (!SearchUtils.SearchInFile(new FileOrigin(string.Empty, txtFilePath.Text),
                                              lTokens,
                                              (rbAnd.Checked) ? SearchLogic.And : SearchLogic.Or,
                                              chkCaseSensitive.Checked,
                                              out searchedFile,
                                              out result))

                {
                    MessageBox.Show(result, $"Failed Searching File '{txtFilePath.Text}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if ((searchedFile == null) || (searchedFile.Lines == null) || (searchedFile.Lines.Count == 0))
                {
                    MessageBox.Show(result, $"No Results To File '{txtFilePath.Text}' Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                
                foreach (SearchedLine line in searchedFile.Lines)
                {
                    dgvResults.Rows.Add(new string[] { line.LineNumber.ToString(), line.Line });

                    dgvResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvResults.Rows.Clear();
        }

        #endregion
    }
}
