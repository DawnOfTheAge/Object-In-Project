namespace ObjectInProject.Gui
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearResults = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClearSearchHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearchProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFind = new System.Windows.Forms.Button();
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvAudit = new System.Windows.Forms.DataGridView();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeverity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelL1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelL2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelL32 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel321 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelL31 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelL311 = new System.Windows.Forms.TableLayoutPanel();
            this.mnuMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.tableLayoutPanelL1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.tableLayoutPanelL2.SuspendLayout();
            this.tableLayoutPanelL32.SuspendLayout();
            this.tableLayoutPanel321.SuspendLayout();
            this.tableLayoutPanelL31.SuspendLayout();
            this.tableLayoutPanelL311.SuspendLayout();
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
            this.mnuClearResults.Click += new System.EventHandler(this.MnuClearResults_Click);
            // 
            // mnuClearSearchHistory
            // 
            this.mnuClearSearchHistory.Name = "mnuClearSearchHistory";
            this.mnuClearSearchHistory.Size = new System.Drawing.Size(180, 22);
            this.mnuClearSearchHistory.Text = "Clear Search History";
            this.mnuClearSearchHistory.Click += new System.EventHandler(this.MnuClearSearchHistory_Click);
            // 
            // mnuSearchProjects
            // 
            this.mnuSearchProjects.Name = "mnuSearchProjects";
            this.mnuSearchProjects.Size = new System.Drawing.Size(107, 20);
            this.mnuSearchProjects.Text = "Projects Explorer";
            this.mnuSearchProjects.Click += new System.EventHandler(this.MnuShowSearchProjectsTree_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(38, 20);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources.Refresh;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(3, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(60, 60);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // txtFind
            // 
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtFind.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtFind.FormattingEnabled = true;
            this.txtFind.Location = new System.Drawing.Point(3, 26);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(645, 21);
            this.txtFind.TabIndex = 18;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtFind_KeyDown);
            // 
            // btnLoadSearchedItemsFromFile
            // 
            this.btnLoadSearchedItemsFromFile.BackColor = System.Drawing.SystemColors.Window;
            this.btnLoadSearchedItemsFromFile.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources._3Dots;
            this.btnLoadSearchedItemsFromFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadSearchedItemsFromFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadSearchedItemsFromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadSearchedItemsFromFile.Location = new System.Drawing.Point(47, 34);
            this.btnLoadSearchedItemsFromFile.Name = "btnLoadSearchedItemsFromFile";
            this.btnLoadSearchedItemsFromFile.Size = new System.Drawing.Size(38, 25);
            this.btnLoadSearchedItemsFromFile.TabIndex = 23;
            this.btnLoadSearchedItemsFromFile.UseVisualStyleBackColor = false;
            this.btnLoadSearchedItemsFromFile.Click += new System.EventHandler(this.BtnLoadSearchedItemsFromFile_Click);
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
            this.chkCaseSensitive.Location = new System.Drawing.Point(3, 3);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.chkCaseSensitive.TabIndex = 29;
            this.chkCaseSensitive.Text = "Case Sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            this.chkCaseSensitive.CheckedChanged += new System.EventHandler(this.ChkCaseSensitive_CheckedChanged);
            // 
            // cboFileTypeFilters
            // 
            this.cboFileTypeFilters.FormattingEnabled = true;
            this.cboFileTypeFilters.Location = new System.Drawing.Point(433, 3);
            this.cboFileTypeFilters.Name = "cboFileTypeFilters";
            this.cboFileTypeFilters.Size = new System.Drawing.Size(209, 21);
            this.cboFileTypeFilters.TabIndex = 30;
            // 
            // lblFileTypeFilters
            // 
            this.lblFileTypeFilters.AutoSize = true;
            this.lblFileTypeFilters.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileTypeFilters.Location = new System.Drawing.Point(347, 0);
            this.lblFileTypeFilters.Name = "lblFileTypeFilters";
            this.lblFileTypeFilters.Size = new System.Drawing.Size(80, 44);
            this.lblFileTypeFilters.TabIndex = 31;
            this.lblFileTypeFilters.Text = "File Type Filters";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::ObjectInProject.Gui.Properties.Resources.Clear;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(165, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 60);
            this.btnClear.TabIndex = 32;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanelL1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvAudit);
            this.splitContainer.Size = new System.Drawing.Size(1133, 612);
            this.splitContainer.SplitterDistance = 444;
            this.splitContainer.TabIndex = 33;
            // 
            // dgvAudit
            // 
            this.dgvAudit.AllowUserToAddRows = false;
            this.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.colSeverity,
            this.colModule,
            this.colMethod,
            this.colLine,
            this.colMessage});
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.Location = new System.Drawing.Point(0, 0);
            this.dgvAudit.Name = "dgvAudit";
            this.dgvAudit.Size = new System.Drawing.Size(1133, 164);
            this.dgvAudit.TabIndex = 1;
            // 
            // colDateTime
            // 
            this.colDateTime.HeaderText = "Date/Time";
            this.colDateTime.Name = "colDateTime";
            // 
            // colSeverity
            // 
            this.colSeverity.HeaderText = "Severity";
            this.colSeverity.Name = "colSeverity";
            // 
            // colModule
            // 
            this.colModule.HeaderText = "Module";
            this.colModule.Name = "colModule";
            // 
            // colMethod
            // 
            this.colMethod.HeaderText = "Method";
            this.colMethod.Name = "colMethod";
            // 
            // colLine
            // 
            this.colLine.HeaderText = "Line";
            this.colLine.Name = "colLine";
            // 
            // colMessage
            // 
            this.colMessage.HeaderText = "Message";
            this.colMessage.Name = "colMessage";
            // 
            // tableLayoutPanelL1
            // 
            this.tableLayoutPanelL1.ColumnCount = 1;
            this.tableLayoutPanelL1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelL1.Controls.Add(this.dgvResults, 0, 1);
            this.tableLayoutPanelL1.Controls.Add(this.tableLayoutPanelL2, 0, 0);
            this.tableLayoutPanelL1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelL1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelL1.Name = "tableLayoutPanelL1";
            this.tableLayoutPanelL1.RowCount = 2;
            this.tableLayoutPanelL1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.35211F));
            this.tableLayoutPanelL1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.64789F));
            this.tableLayoutPanelL1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelL1.Size = new System.Drawing.Size(1133, 444);
            this.tableLayoutPanelL1.TabIndex = 33;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 115);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(1127, 326);
            this.dgvResults.TabIndex = 0;
            // 
            // tableLayoutPanelL2
            // 
            this.tableLayoutPanelL2.ColumnCount = 2;
            this.tableLayoutPanelL2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.33333F));
            this.tableLayoutPanelL2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanelL2.Controls.Add(this.tableLayoutPanelL32, 1, 0);
            this.tableLayoutPanelL2.Controls.Add(this.tableLayoutPanelL31, 0, 0);
            this.tableLayoutPanelL2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelL2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelL2.Name = "tableLayoutPanelL2";
            this.tableLayoutPanelL2.RowCount = 1;
            this.tableLayoutPanelL2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelL2.Size = new System.Drawing.Size(1127, 106);
            this.tableLayoutPanelL2.TabIndex = 1;
            // 
            // tableLayoutPanelL32
            // 
            this.tableLayoutPanelL32.ColumnCount = 5;
            this.tableLayoutPanelL32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelL32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelL32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelL32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelL32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelL32.Controls.Add(this.btnFind, 0, 0);
            this.tableLayoutPanelL32.Controls.Add(this.btnClear, 2, 0);
            this.tableLayoutPanelL32.Controls.Add(this.tableLayoutPanel321, 4, 0);
            this.tableLayoutPanelL32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelL32.Location = new System.Drawing.Point(660, 3);
            this.tableLayoutPanelL32.Name = "tableLayoutPanelL32";
            this.tableLayoutPanelL32.RowCount = 1;
            this.tableLayoutPanelL32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelL32.Size = new System.Drawing.Size(464, 100);
            this.tableLayoutPanelL32.TabIndex = 0;
            // 
            // tableLayoutPanel321
            // 
            this.tableLayoutPanel321.ColumnCount = 3;
            this.tableLayoutPanel321.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.Controls.Add(this.btnLoadSearchedItemsFromFile, 1, 1);
            this.tableLayoutPanel321.Location = new System.Drawing.Point(327, 3);
            this.tableLayoutPanel321.Name = "tableLayoutPanel321";
            this.tableLayoutPanel321.RowCount = 3;
            this.tableLayoutPanel321.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel321.Size = new System.Drawing.Size(134, 94);
            this.tableLayoutPanel321.TabIndex = 33;
            // 
            // tableLayoutPanelL31
            // 
            this.tableLayoutPanelL31.ColumnCount = 1;
            this.tableLayoutPanelL31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelL31.Controls.Add(this.txtFind, 0, 0);
            this.tableLayoutPanelL31.Controls.Add(this.tableLayoutPanelL311, 0, 1);
            this.tableLayoutPanelL31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelL31.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelL31.Name = "tableLayoutPanelL31";
            this.tableLayoutPanelL31.RowCount = 2;
            this.tableLayoutPanelL31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelL31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelL31.Size = new System.Drawing.Size(651, 100);
            this.tableLayoutPanelL31.TabIndex = 1;
            // 
            // tableLayoutPanelL311
            // 
            this.tableLayoutPanelL311.ColumnCount = 3;
            this.tableLayoutPanelL311.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelL311.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelL311.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelL311.Controls.Add(this.chkCaseSensitive, 0, 0);
            this.tableLayoutPanelL311.Controls.Add(this.lblFileTypeFilters, 1, 0);
            this.tableLayoutPanelL311.Controls.Add(this.cboFileTypeFilters, 2, 0);
            this.tableLayoutPanelL311.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelL311.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanelL311.Name = "tableLayoutPanelL311";
            this.tableLayoutPanelL311.RowCount = 1;
            this.tableLayoutPanelL311.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelL311.Size = new System.Drawing.Size(645, 44);
            this.tableLayoutPanelL311.TabIndex = 19;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 660);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "FrmMain";
            this.Text = "Object In Solution Browser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.tableLayoutPanelL1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.tableLayoutPanelL2.ResumeLayout(false);
            this.tableLayoutPanelL32.ResumeLayout(false);
            this.tableLayoutPanel321.ResumeLayout(false);
            this.tableLayoutPanelL31.ResumeLayout(false);
            this.tableLayoutPanelL311.ResumeLayout(false);
            this.tableLayoutPanelL311.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuSearchProjects;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Button btnFind;
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
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeverity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelL1;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelL2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelL32;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel321;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelL31;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelL311;
    }
}

