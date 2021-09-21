using ObjectInProject.Common;
using ObjectInProject.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectInProject.Tests.FilesList
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

            txtTokens.Text = "yosef,ad";

            dgvFiles.Rows.Add(@"C:\Temp\Search File Test.txt");
            dgvFiles.Rows.Add(@"C:\Temp\Search File Test 1.txt");
            dgvFiles.Rows.Add(@"C:\Temp\Search File Test 2.txt");

            dgvFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        #endregion

        #region Gui

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<FileOrigin> files = new List<FileOrigin>();

                for (int i = 0; i < dgvFiles.Rows.Count; i++)
                {
                    files.Add(new FileOrigin(string.Empty, dgvFiles.Rows[i].Cells[0].Value.ToString()));
                }

                string result;

                List<string> lTokens = txtTokens.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                SearchedFilesList searchedFilesList;

                if (!SearchUtils.SearchInListOfFiles(files,
                                                     lTokens,
                                                     (rbAnd.Checked) ? SearchLogic.And : SearchLogic.Or,
                                                     chkCaseSensitive.Checked,
                                                     out searchedFilesList,
                                                     out result))

                {
                    MessageBox.Show(result, $"Failed Searching Files List. '{result}'", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                dgvResults.Rows.Clear();

                if ((searchedFilesList == null) || (searchedFilesList.Files == null) || (searchedFilesList.Files.Count == 0))
                {
                    MessageBox.Show(result, $"No Results To Files List Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                
                List<ExtendedSearchedLine> lines;
                if (!searchedFilesList.GetLines(out lines, out result))
                {
                    return;
                }

                foreach (ExtendedSearchedLine line in lines)
                {
                    dgvResults.Rows.Add(new string[] { line.Name, line.LineNumber.ToString(), line.Line, line.Path });

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
