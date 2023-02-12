using ObjectInProject.Common;
using ObjectInProject.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ObjectInProject.Tests.Line
{
    public partial class FrmMain : Form
    {
        #region Constructor

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Location = Cursor.Position;

            chkCaseSensitive.Checked = false;

            rbAnd.Checked = true;
            rbOr.Checked = false;

            txtLine.Text = "Yosef Had A Little Lamb";
            txtTokens.Text = "ttl , ad";
        }

        #endregion

        #region Gui

        private void BtnIsIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLine.Text))
                {
                    MessageBox.Show("No Line To Search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrEmpty(txtTokens.Text))
                {
                    MessageBox.Show("No Tokens To Search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return;
                }

                string tokens = txtTokens.Text.Replace(" ", string.Empty);

                List<string> lTokens = tokens.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

                bool exists = SearchLine.SearchInLine(txtLine.Text,
                                                       lTokens,
                                                       (rbAnd.Checked) ? SearchLogic.And : SearchLogic.Or,
                                                       chkCaseSensitive.Checked,
                                                       out var result);

                string message = exists ? "Yes" : "No";

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
