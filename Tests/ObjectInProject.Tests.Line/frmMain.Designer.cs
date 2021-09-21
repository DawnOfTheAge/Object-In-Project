
namespace ObjectInProject.Tests.Line
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.rbAnd = new System.Windows.Forms.RadioButton();
            this.rbOr = new System.Windows.Forms.RadioButton();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.btnIsIn = new System.Windows.Forms.Button();
            this.lnlTokens = new System.Windows.Forms.Label();
            this.txtTokens = new System.Windows.Forms.TextBox();
            this.gbSearchLogic = new System.Windows.Forms.GroupBox();
            this.gbSearchLogic.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(132, 96);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.chkCaseSensitive.TabIndex = 0;
            this.chkCaseSensitive.Text = "Case Sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // rbAnd
            // 
            this.rbAnd.AutoSize = true;
            this.rbAnd.Location = new System.Drawing.Point(11, 20);
            this.rbAnd.Name = "rbAnd";
            this.rbAnd.Size = new System.Drawing.Size(44, 17);
            this.rbAnd.TabIndex = 1;
            this.rbAnd.TabStop = true;
            this.rbAnd.Text = "And";
            this.rbAnd.UseVisualStyleBackColor = true;
            // 
            // rbOr
            // 
            this.rbOr.AutoSize = true;
            this.rbOr.Location = new System.Drawing.Point(11, 43);
            this.rbOr.Name = "rbOr";
            this.rbOr.Size = new System.Drawing.Size(36, 17);
            this.rbOr.TabIndex = 2;
            this.rbOr.TabStop = true;
            this.rbOr.Text = "Or";
            this.rbOr.UseVisualStyleBackColor = true;
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(65, 23);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(402, 20);
            this.txtLine.TabIndex = 3;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(16, 26);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(27, 13);
            this.lblLine.TabIndex = 4;
            this.lblLine.Text = "Line";
            // 
            // btnIsIn
            // 
            this.btnIsIn.Location = new System.Drawing.Point(473, 18);
            this.btnIsIn.Name = "btnIsIn";
            this.btnIsIn.Size = new System.Drawing.Size(97, 28);
            this.btnIsIn.TabIndex = 5;
            this.btnIsIn.Text = "Is In?";
            this.btnIsIn.UseVisualStyleBackColor = true;
            this.btnIsIn.Click += new System.EventHandler(this.btnIsIn_Click);
            // 
            // lnlTokens
            // 
            this.lnlTokens.AutoSize = true;
            this.lnlTokens.Location = new System.Drawing.Point(16, 62);
            this.lnlTokens.Name = "lnlTokens";
            this.lnlTokens.Size = new System.Drawing.Size(43, 13);
            this.lnlTokens.TabIndex = 7;
            this.lnlTokens.Text = "Tokens";
            // 
            // txtTokens
            // 
            this.txtTokens.Location = new System.Drawing.Point(65, 59);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.Size = new System.Drawing.Size(402, 20);
            this.txtTokens.TabIndex = 6;
            // 
            // gbSearchLogic
            // 
            this.gbSearchLogic.Controls.Add(this.rbAnd);
            this.gbSearchLogic.Controls.Add(this.rbOr);
            this.gbSearchLogic.Location = new System.Drawing.Point(19, 96);
            this.gbSearchLogic.Name = "gbSearchLogic";
            this.gbSearchLogic.Size = new System.Drawing.Size(84, 70);
            this.gbSearchLogic.TabIndex = 8;
            this.gbSearchLogic.TabStop = false;
            this.gbSearchLogic.Text = "Search Logic";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 181);
            this.Controls.Add(this.gbSearchLogic);
            this.Controls.Add(this.lnlTokens);
            this.Controls.Add(this.txtTokens);
            this.Controls.Add(this.btnIsIn);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.chkCaseSensitive);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmMain";
            this.Text = "Search Line Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbSearchLogic.ResumeLayout(false);
            this.gbSearchLogic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.RadioButton rbAnd;
        private System.Windows.Forms.RadioButton rbOr;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Button btnIsIn;
        private System.Windows.Forms.Label lnlTokens;
        private System.Windows.Forms.TextBox txtTokens;
        private System.Windows.Forms.GroupBox gbSearchLogic;
    }
}

