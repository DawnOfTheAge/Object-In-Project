
namespace ObjectInProject.Tests.Editors
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
            this.btnFind = new System.Windows.Forms.Button();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNotepadExists = new System.Windows.Forms.Button();
            this.btnNotepadPlusPlusExists = new System.Windows.Forms.Button();
            this.lblNotepadExists = new System.Windows.Forms.Label();
            this.lblNotepadPlusPlusExists = new System.Windows.Forms.Label();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.nudLine = new System.Windows.Forms.NumericUpDown();
            this.btnOpenFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLine)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(12, 136);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(555, 23);
            this.btnFind.TabIndex = 48;
            this.btnFind.Text = "Find Editors";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFile});
            this.dgvFiles.Location = new System.Drawing.Point(12, 12);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.Size = new System.Drawing.Size(555, 118);
            this.dgvFiles.TabIndex = 47;
            // 
            // colFile
            // 
            this.colFile.HeaderText = "Editor";
            this.colFile.Name = "colFile";
            // 
            // btnNotepadExists
            // 
            this.btnNotepadExists.Location = new System.Drawing.Point(80, 174);
            this.btnNotepadExists.Name = "btnNotepadExists";
            this.btnNotepadExists.Size = new System.Drawing.Size(67, 23);
            this.btnNotepadExists.TabIndex = 49;
            this.btnNotepadExists.Text = "Exists?";
            this.btnNotepadExists.UseVisualStyleBackColor = true;
            this.btnNotepadExists.Click += new System.EventHandler(this.btnNotepadExists_Click);
            // 
            // btnNotepadPlusPlusExists
            // 
            this.btnNotepadPlusPlusExists.Location = new System.Drawing.Point(80, 198);
            this.btnNotepadPlusPlusExists.Name = "btnNotepadPlusPlusExists";
            this.btnNotepadPlusPlusExists.Size = new System.Drawing.Size(67, 23);
            this.btnNotepadPlusPlusExists.TabIndex = 50;
            this.btnNotepadPlusPlusExists.Text = "Exists?";
            this.btnNotepadPlusPlusExists.UseVisualStyleBackColor = true;
            this.btnNotepadPlusPlusExists.Click += new System.EventHandler(this.btnNotepadPlusPlusExists_Click);
            // 
            // lblNotepadExists
            // 
            this.lblNotepadExists.AutoSize = true;
            this.lblNotepadExists.Location = new System.Drawing.Point(12, 179);
            this.lblNotepadExists.Name = "lblNotepadExists";
            this.lblNotepadExists.Size = new System.Drawing.Size(48, 13);
            this.lblNotepadExists.TabIndex = 51;
            this.lblNotepadExists.Text = "Notepad";
            // 
            // lblNotepadPlusPlusExists
            // 
            this.lblNotepadPlusPlusExists.AutoSize = true;
            this.lblNotepadPlusPlusExists.Location = new System.Drawing.Point(12, 203);
            this.lblNotepadPlusPlusExists.Name = "lblNotepadPlusPlusExists";
            this.lblNotepadPlusPlusExists.Size = new System.Drawing.Size(60, 13);
            this.lblNotepadPlusPlusExists.TabIndex = 52;
            this.lblNotepadPlusPlusExists.Text = "Notepad++";
            // 
            // btnFilePath
            // 
            this.btnFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilePath.Location = new System.Drawing.Point(442, 237);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(38, 39);
            this.btnFilePath.TabIndex = 55;
            this.btnFilePath.Text = "...";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(80, 247);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(356, 20);
            this.txtFilePath.TabIndex = 54;
            this.txtFilePath.Text = "C:\\Temp\\Search File Test.txt";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 250);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(48, 13);
            this.lblFilePath.TabIndex = 53;
            this.lblFilePath.Text = "File Path";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(12, 298);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(27, 13);
            this.lblLine.TabIndex = 56;
            this.lblLine.Text = "Line";
            // 
            // nudLine
            // 
            this.nudLine.Location = new System.Drawing.Point(80, 296);
            this.nudLine.Name = "nudLine";
            this.nudLine.Size = new System.Drawing.Size(67, 20);
            this.nudLine.TabIndex = 57;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(15, 330);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(552, 23);
            this.btnOpenFile.TabIndex = 58;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 365);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.nudLine);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.btnFilePath);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblNotepadPlusPlusExists);
            this.Controls.Add(this.lblNotepadExists);
            this.Controls.Add(this.btnNotepadPlusPlusExists);
            this.Controls.Add(this.btnNotepadExists);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.dgvFiles);
            this.Name = "frmMain";
            this.Text = "Get Existing Editors Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.Button btnNotepadExists;
        private System.Windows.Forms.Button btnNotepadPlusPlusExists;
        private System.Windows.Forms.Label lblNotepadExists;
        private System.Windows.Forms.Label lblNotepadPlusPlusExists;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.NumericUpDown nudLine;
        private System.Windows.Forms.Button btnOpenFile;
    }
}

