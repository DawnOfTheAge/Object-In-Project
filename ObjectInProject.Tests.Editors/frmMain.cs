using ObjectInProject.Common;
using ObjectInProject.EditorsInformation;
using ObjectInProject.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ObjectInProject.Tests.Editors
{
    public partial class FrmMain : Form
    {
        #region Data Members

        private List<EditorInformation> editors;

        private VisualStudiosInstalled visualStudiosInstalled;

        #endregion

        #region Constructor

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Location = Cursor.Position;     
                
                visualStudiosInstalled = new VisualStudiosInstalled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Gui

        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (!visualStudiosInstalled.GetVisualStudiosInstalled(out editors, out string result))
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

        private void BtnNotepadExists_Click(object sender, EventArgs e)
        {
            string message;

            if (!visualStudiosInstalled.NotepadPlusPlusExists())
            {
                message = $"'{EditorsInformationConstants.NOTEPAD_STRING}' Does Not Exist";
            }
            else
            {
                message = $"'{EditorsInformationConstants.NOTEPAD_STRING}' Exists";
            }

            MessageBox.Show(message);
        }

        private void BtnNotepadPlusPlusExists_Click(object sender, EventArgs e)
        {
            string message;

            if (!visualStudiosInstalled.NotepadPlusPlusExists())
            {
                message = $"'{EditorsInformationConstants.NOTEPAD_PLUS_PLUS_STRING}' Does Not Exist";
            }
            else
            {
                message = $"'{EditorsInformationConstants.NOTEPAD_PLUS_PLUS_STRING}' Exists";
            }

            MessageBox.Show(message);
        }

        private void BtnFilePath_Click(object sender, EventArgs e)
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

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dgvFiles.SelectedRows.Count == 0) || dgvFiles.SelectedRows[0].Index == ObjectInProjectConstants.NONE)
                {
                    MessageBox.Show("No Editor Selected", "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                Common.Editors editor;

                editor = EditorUtils.EditorToEnum(dgvFiles.Rows[dgvFiles.SelectedRows[0].Index].Cells[0].Value.ToString());

                if (!visualStudiosInstalled.OpenFileAtLine(txtFilePath.Text, (int)nudLine.Value, editor, out string result))
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
