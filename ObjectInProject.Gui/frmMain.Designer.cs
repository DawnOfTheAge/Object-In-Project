namespace ObjectInProject.Gui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearResults = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearSearchHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFind = new System.Windows.Forms.Button();
            this.lvResults = new System.Windows.Forms.ListView();
            this.txtFind = new System.Windows.Forms.ComboBox();
            this.btnLoadSearchedItemsFromFile = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.cboActiveSearchJob = new System.Windows.Forms.ToolStripDropDownButton();
            this.txtActiveSearchJob = new System.Windows.Forms.ToolStripStatusLabel();
            this.cboSearchType = new System.Windows.Forms.ToolStripDropDownButton();
            this.txtSearchType = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumberOfProjects = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNumberOfProjects = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumberOfFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNumberOfFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumberOfTestFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNumberOfTestFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumberOfHits = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNumberOfHits = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbFiles = new System.Windows.Forms.ToolStripProgressBar();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.cboFileTypeFilters = new System.Windows.Forms.ComboBox();
            this.lblFileTypeFilters = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClear,
            this.mnuSearchProjects,
            this.mnuExit});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1133, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuClear
            // 
            this.mnuClear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearResults,
            this.mnuClearSearchHistory});
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(46, 20);
            this.mnuClear.Text = "Clear";
            // 
            // mnuClearResults
            // 
            this.mnuClearResults.Name = "mnuClearResults";
            this.mnuClearResults.Size = new System.Drawing.Size(180, 22);
            this.mnuClearResults.Text = "Clear Results";
            this.mnuClearResults.Click += new System.EventHandler(this.mnuClearResults_Click);
            // 
            // mnuClearSearchHistory
            // 
            this.mnuClearSearchHistory.Name = "mnuClearSearchHistory";
            this.mnuClearSearchHistory.Size = new System.Drawing.Size(180, 22);
            this.mnuClearSearchHistory.Text = "Clear Search History";
            this.mnuClearSearchHistory.Click += new System.EventHandler(this.mnuClearSearchHistory_Click);
            // 
            // mnuSearchProjects
            // 
            this.mnuSearchProjects.Name = "mnuSearchProjects";
            this.mnuSearchProjects.Size = new System.Drawing.Size(107, 20);
            this.mnuSearchProjects.Text = "Projects Explorer";
            this.mnuSearchProjects.Click += new System.EventHandler(this.mnuShowSearchProjectsTree_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(38, 20);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources.Refresh;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(832, 26);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(60, 60);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lvResults
            // 
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(12, 95);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(1109, 415);
            this.lvResults.TabIndex = 6;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvResults_MouseDoubleClick);
            // 
            // txtFind
            // 
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtFind.FormattingEnabled = true;
            this.txtFind.Location = new System.Drawing.Point(12, 47);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(814, 21);
            this.txtFind.TabIndex = 18;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // btnLoadSearchedItemsFromFile
            // 
            this.btnLoadSearchedItemsFromFile.BackColor = System.Drawing.SystemColors.Window;
            this.btnLoadSearchedItemsFromFile.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources._3Dots;
            this.btnLoadSearchedItemsFromFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadSearchedItemsFromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSearchedItemsFromFile.Location = new System.Drawing.Point(964, 38);
            this.btnLoadSearchedItemsFromFile.Name = "btnLoadSearchedItemsFromFile";
            this.btnLoadSearchedItemsFromFile.Size = new System.Drawing.Size(40, 33);
            this.btnLoadSearchedItemsFromFile.TabIndex = 23;
            this.btnLoadSearchedItemsFromFile.UseVisualStyleBackColor = false;
            this.btnLoadSearchedItemsFromFile.Click += new System.EventHandler(this.btnLoadSearchedItemsFromFile_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboActiveSearchJob,
            this.txtActiveSearchJob,
            this.cboSearchType,
            this.txtSearchType,
            this.lblNumberOfProjects,
            this.txtNumberOfProjects,
            this.lblNumberOfFiles,
            this.txtNumberOfFiles,
            this.lblNumberOfTestFiles,
            this.txtNumberOfTestFiles,
            this.lblNumberOfHits,
            this.txtNumberOfHits,
            this.lblFiles,
            this.pbFiles});
            this.statusStrip.Location = new System.Drawing.Point(0, 636);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1133, 24);
            this.statusStrip.TabIndex = 28;
            this.statusStrip.Text = "statusStrip1";
            // 
            // cboActiveSearchJob
            // 
            this.cboActiveSearchJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cboActiveSearchJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cboActiveSearchJob.Image = ((System.Drawing.Image)(resources.GetObject("cboActiveSearchJob.Image")));
            this.cboActiveSearchJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cboActiveSearchJob.Name = "cboActiveSearchJob";
            this.cboActiveSearchJob.Size = new System.Drawing.Size(119, 22);
            this.cboActiveSearchJob.Text = "Active Search Job";
            // 
            // txtActiveSearchJob
            // 
            this.txtActiveSearchJob.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtActiveSearchJob.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtActiveSearchJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtActiveSearchJob.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtActiveSearchJob.Name = "txtActiveSearchJob";
            this.txtActiveSearchJob.Size = new System.Drawing.Size(4, 19);
            // 
            // cboSearchType
            // 
            this.cboSearchType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cboSearchType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cboSearchType.Image = ((System.Drawing.Image)(resources.GetObject("cboSearchType.Image")));
            this.cboSearchType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(87, 22);
            this.cboSearchType.Text = "Search Type";
            // 
            // txtSearchType
            // 
            this.txtSearchType.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtSearchType.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtSearchType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtSearchType.Name = "txtSearchType";
            this.txtSearchType.Size = new System.Drawing.Size(4, 19);
            // 
            // lblNumberOfProjects
            // 
            this.lblNumberOfProjects.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblNumberOfProjects.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblNumberOfProjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfProjects.Name = "lblNumberOfProjects";
            this.lblNumberOfProjects.Size = new System.Drawing.Size(122, 19);
            this.lblNumberOfProjects.Text = "Number Of Projects";
            // 
            // txtNumberOfProjects
            // 
            this.txtNumberOfProjects.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtNumberOfProjects.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtNumberOfProjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtNumberOfProjects.Name = "txtNumberOfProjects";
            this.txtNumberOfProjects.Size = new System.Drawing.Size(18, 19);
            this.txtNumberOfProjects.Text = "0";
            // 
            // lblNumberOfFiles
            // 
            this.lblNumberOfFiles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblNumberOfFiles.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblNumberOfFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfFiles.Name = "lblNumberOfFiles";
            this.lblNumberOfFiles.Size = new System.Drawing.Size(101, 19);
            this.lblNumberOfFiles.Text = "Number Of Files";
            // 
            // txtNumberOfFiles
            // 
            this.txtNumberOfFiles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtNumberOfFiles.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtNumberOfFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtNumberOfFiles.Name = "txtNumberOfFiles";
            this.txtNumberOfFiles.Size = new System.Drawing.Size(18, 19);
            this.txtNumberOfFiles.Text = "0";
            // 
            // lblNumberOfTestFiles
            // 
            this.lblNumberOfTestFiles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblNumberOfTestFiles.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblNumberOfTestFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfTestFiles.Name = "lblNumberOfTestFiles";
            this.lblNumberOfTestFiles.Size = new System.Drawing.Size(127, 19);
            this.lblNumberOfTestFiles.Text = "Number Of Test Files";
            // 
            // txtNumberOfTestFiles
            // 
            this.txtNumberOfTestFiles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtNumberOfTestFiles.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtNumberOfTestFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtNumberOfTestFiles.Name = "txtNumberOfTestFiles";
            this.txtNumberOfTestFiles.Size = new System.Drawing.Size(18, 19);
            this.txtNumberOfTestFiles.Text = "0";
            // 
            // lblNumberOfHits
            // 
            this.lblNumberOfHits.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblNumberOfHits.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblNumberOfHits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNumberOfHits.Name = "lblNumberOfHits";
            this.lblNumberOfHits.Size = new System.Drawing.Size(99, 19);
            this.lblNumberOfHits.Text = "Number Of Hits";
            // 
            // txtNumberOfHits
            // 
            this.txtNumberOfHits.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.txtNumberOfHits.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.txtNumberOfHits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtNumberOfHits.Name = "txtNumberOfHits";
            this.txtNumberOfHits.Size = new System.Drawing.Size(18, 19);
            this.txtNumberOfHits.Text = "0";
            // 
            // lblFiles
            // 
            this.lblFiles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblFiles.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(59, 19);
            this.lblFiles.Text = "Progress";
            // 
            // pbFiles
            // 
            this.pbFiles.Name = "pbFiles";
            this.pbFiles.Size = new System.Drawing.Size(100, 18);
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(12, 72);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.chkCaseSensitive.TabIndex = 29;
            this.chkCaseSensitive.Text = "Case Sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            this.chkCaseSensitive.CheckedChanged += new System.EventHandler(this.chkCaseSensitive_CheckedChanged);
            // 
            // cboFileTypeFilters
            // 
            this.cboFileTypeFilters.FormattingEnabled = true;
            this.cboFileTypeFilters.Location = new System.Drawing.Point(200, 70);
            this.cboFileTypeFilters.Name = "cboFileTypeFilters";
            this.cboFileTypeFilters.Size = new System.Drawing.Size(244, 21);
            this.cboFileTypeFilters.TabIndex = 30;
            // 
            // lblFileTypeFilters
            // 
            this.lblFileTypeFilters.AutoSize = true;
            this.lblFileTypeFilters.Location = new System.Drawing.Point(114, 73);
            this.lblFileTypeFilters.Name = "lblFileTypeFilters";
            this.lblFileTypeFilters.Size = new System.Drawing.Size(80, 13);
            this.lblFileTypeFilters.TabIndex = 31;
            this.lblFileTypeFilters.Text = "File Type Filters";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources.Clear;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(898, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 60);
            this.btnClear.TabIndex = 32;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 660);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblFileTypeFilters);
            this.Controls.Add(this.cboFileTypeFilters);
            this.Controls.Add(this.chkCaseSensitive);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnLoadSearchedItemsFromFile);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "Object In Solution Browser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchProjects;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
        private System.Windows.Forms.ComboBox txtFind;
        private System.Windows.Forms.ToolStripMenuItem mnuClearResults;
        private System.Windows.Forms.ToolStripMenuItem mnuClearSearchHistory;
        private System.Windows.Forms.Button btnLoadSearchedItemsFromFile;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripDropDownButton cboActiveSearchJob;
        private System.Windows.Forms.ToolStripStatusLabel txtActiveSearchJob;
        private System.Windows.Forms.ToolStripStatusLabel lblFiles;
        private System.Windows.Forms.ToolStripProgressBar pbFiles;
        private System.Windows.Forms.ToolStripStatusLabel lblNumberOfProjects;
        private System.Windows.Forms.ToolStripStatusLabel txtNumberOfProjects;
        private System.Windows.Forms.ToolStripStatusLabel lblNumberOfFiles;
        private System.Windows.Forms.ToolStripStatusLabel txtNumberOfFiles;
        private System.Windows.Forms.ToolStripStatusLabel lblNumberOfTestFiles;
        private System.Windows.Forms.ToolStripStatusLabel txtNumberOfTestFiles;
        private System.Windows.Forms.ToolStripStatusLabel lblNumberOfHits;
        private System.Windows.Forms.ToolStripStatusLabel txtNumberOfHits;
        private System.Windows.Forms.ToolStripDropDownButton cboSearchType;
        private System.Windows.Forms.ToolStripStatusLabel txtSearchType;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.ComboBox cboFileTypeFilters;
        private System.Windows.Forms.Label lblFileTypeFilters;
        private System.Windows.Forms.Button btnClear;
    }
}

