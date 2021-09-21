using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectInProject.Common;
using ObjectInProject.EditorsInformation;
using System.IO;
using System.Reflection;

namespace ObjectInProject.Tests.Editors
{
    public partial class frmMain : Form
    {
        #region Data Members

        private List<EditorInformation> editors;

        #endregion

        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Location = Cursor.Position;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Gui

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                string result;
                
                if (!VisualStudiosInstalled.GetVisualStudiosInstalled(out editors, out result))
                {
                    MessageBox.Show(result, "Get Visual Studios Installed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if ((editors != null) && (editors.Count > 0))
                {
                    dgvFiles.Rows.Clear();
                    foreach (EditorInformation editor in editors)
                    {
                        dgvFiles.Rows.Add(editor.Editor);
                    }
                }

                dgvFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Find Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNotepadExists_Click(object sender, EventArgs e)
        {
            string message;

            if (!VisualStudiosInstalled.NotepadPlusPlusExists())
            {
                message = $"'{VisualStudiosInstalled.NOTEPAD_STRING}' Does Not Exist";
            }
            else
            {
                message = $"'{VisualStudiosInstalled.NOTEPAD_STRING}' Exists";
            }

            MessageBox.Show(message);
        }

        private void btnNotepadPlusPlusExists_Click(object sender, EventArgs e)
        {
            string message;

            if (!VisualStudiosInstalled.NotepadPlusPlusExists())
            {
                message = $"'{VisualStudiosInstalled.NOTEPAD_PLUS_PLUS_STRING}' Does Not Exist";
            }
            else
            {
                message = $"'{VisualStudiosInstalled.NOTEPAD_PLUS_PLUS_STRING}' Exists";
            }

            MessageBox.Show(message);
        }

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
                MessageBox.Show(ex.Message, "File Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dgvFiles.SelectedRows.Count == 0) || dgvFiles.SelectedRows[0].Index == ObjectInProjectConstants.NONE)
                {
                    MessageBox.Show("No Editor Selected", "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                Common.Editors editor;

                editor = Utils.EditorToEnum(dgvFiles.Rows[dgvFiles.SelectedRows[0].Index].Cells[0].Value.ToString());

                if (!VisualStudiosInstalled.OpenFileAtLine(txtFilePath.Text, (int)nudLine.Value, editor, out string result))
                {
                    MessageBox.Show($"Failed Opening File [{txtFilePath.Text}] At Line [{nudLine.Value}] With Editor [{editor}]", 
                                    "Open File Error", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        #endregion
    }
}
