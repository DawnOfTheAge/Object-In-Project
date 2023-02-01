
namespace ObjectInProject.Gui
{
    partial class FrmCreateUpdateSearchProject
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
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.txtFileTypeFilters = new System.Windows.Forms.ComboBox();
            this.lblSearchJobIndex = new System.Windows.Forms.Label();
            this.btnSaveSeachProject = new System.Windows.Forms.Button();
            this.gbSearchLogic = new System.Windows.Forms.GroupBox();
            this.rbAnd = new System.Windows.Forms.RadioButton();
            this.rbOr = new System.Windows.Forms.RadioButton();
            this.lblFileTypeFilters = new System.Windows.Forms.Label();
            this.gbSearchType = new System.Windows.Forms.GroupBox();
            this.rbSolutionsProjectSearch = new System.Windows.Forms.RadioButton();
            this.rbDirectoriesProjectSearch = new System.Windows.Forms.RadioButton();
            this.txtSearchProjectName = new System.Windows.Forms.TextBox();
            this.lblSearchProjectName = new System.Windows.Forms.Label();
            this.chkNoTest = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitiveSearch = new System.Windows.Forms.CheckBox();
            this.cboEditor = new System.Windows.Forms.ComboBox();
            this.lblFileEditor = new System.Windows.Forms.Label();
            this.tabPageSolutionsList = new System.Windows.Forms.TabPage();
            this.btnAddSolution = new System.Windows.Forms.Button();
            this.lstSolutions = new System.Windows.Forms.ListBox();
            this.tabPageDirectoriesList = new System.Windows.Forms.TabPage();
            this.lstDirectories = new System.Windows.Forms.ListBox();
            this.btnAddDirectory = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.gbSearchLogic.SuspendLayout();
            this.gbSearchType.SuspendLayout();
            this.tabPageSolutionsList.SuspendLayout();
            this.tabPageDirectoriesList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPageGeneral);
            this.tabSettings.Controls.Add(this.tabPageSolutionsList);
            this.tabSettings.Controls.Add(this.tabPageDirectoriesList);
            this.tabSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSettings.Location = new System.Drawing.Point(0, 0);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(446, 263);
            this.tabSettings.TabIndex = 14;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.txtFileTypeFilters);
            this.tabPageGeneral.Controls.Add(this.lblSearchJobIndex);
            this.tabPageGeneral.Controls.Add(this.btnSaveSeachProject);
            this.tabPageGeneral.Controls.Add(this.gbSearchLogic);
            this.tabPageGeneral.Controls.Add(this.lblFileTypeFilters);
            this.tabPageGeneral.Controls.Add(this.gbSearchType);
            this.tabPageGeneral.Controls.Add(this.txtSearchProjectName);
            this.tabPageGeneral.Controls.Add(this.lblSearchProjectName);
            this.tabPageGeneral.Controls.Add(this.chkNoTest);
            this.tabPageGeneral.Controls.Add(this.chkCaseSensitiveSearch);
            this.tabPageGeneral.Controls.Add(this.cboEditor);
            this.tabPageGeneral.Controls.Add(this.lblFileEditor);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(438, 237);
            this.tabPageGeneral.TabIndex = 2;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // txtFileTypeFilters
            // 
            this.txtFileTypeFilters.FormattingEnabled = true;
            this.txtFileTypeFilters.Location = new System.Drawing.Point(160, 81);
            this.txtFileTypeFilters.Name = "txtFileTypeFilters";
            this.txtFileTypeFilters.Size = new System.Drawing.Size(270, 21);
            this.txtFileTypeFilters.TabIndex = 29;
            // 
            // lblSearchJobIndex
            // 
            this.lblSearchJobIndex.AutoSize = true;
            this.lblSearchJobIndex.Location = new System.Drawing.Point(346, 278);
            this.lblSearchJobIndex.Name = "lblSearchJobIndex";
            this.lblSearchJobIndex.Size = new System.Drawing.Size(0, 13);
            this.lblSearchJobIndex.TabIndex = 28;
            // 
            // btnSaveSeachProject
            // 
            this.btnSaveSeachProject.Location = new System.Drawing.Point(303, 197);
            this.btnSaveSeachProject.Name = "btnSaveSeachProject";
            this.btnSaveSeachProject.Size = new System.Drawing.Size(128, 32);
            this.btnSaveSeachProject.TabIndex = 27;
            this.btnSaveSeachProject.Text = "Save Search Project";
            this.btnSaveSeachProject.UseVisualStyleBackColor = true;
            this.btnSaveSeachProject.Click += new System.EventHandler(this.BtnSaveSearchProject_Click);
            // 
            // gbSearchLogic
            // 
            this.gbSearchLogic.Controls.Add(this.rbAnd);
            this.gbSearchLogic.Controls.Add(this.rbOr);
            this.gbSearchLogic.Location = new System.Drawing.Point(298, 107);
            this.gbSearchLogic.Name = "gbSearchLogic";
            this.gbSearchLogic.Size = new System.Drawing.Size(133, 66);
            this.gbSearchLogic.TabIndex = 26;
            this.gbSearchLogic.TabStop = false;
            this.gbSearchLogic.Text = "Search Logic";
            // 
            // rbAnd
            // 
            this.rbAnd.AutoSize = true;
            this.rbAnd.Location = new System.Drawing.Point(17, 19);
            this.rbAnd.Name = "rbAnd";
            this.rbAnd.Size = new System.Drawing.Size(89, 17);
            this.rbAnd.TabIndex = 21;
            this.rbAnd.TabStop = true;
            this.rbAnd.Text = "\'AND\' Search";
            this.rbAnd.UseVisualStyleBackColor = true;
            // 
            // rbOr
            // 
            this.rbOr.AutoSize = true;
            this.rbOr.Location = new System.Drawing.Point(17, 42);
            this.rbOr.Name = "rbOr";
            this.rbOr.Size = new System.Drawing.Size(82, 17);
            this.rbOr.TabIndex = 22;
            this.rbOr.TabStop = true;
            this.rbOr.Text = "\'OR\' Search";
            this.rbOr.UseVisualStyleBackColor = true;
            // 
            // lblFileTypeFilters
            // 
            this.lblFileTypeFilters.AutoSize = true;
            this.lblFileTypeFilters.Location = new System.Drawing.Point(13, 84);
            this.lblFileTypeFilters.Name = "lblFileTypeFilters";
            this.lblFileTypeFilters.Size = new System.Drawing.Size(80, 13);
            this.lblFileTypeFilters.TabIndex = 24;
            this.lblFileTypeFilters.Text = "File Type Filters";
            // 
            // gbSearchType
            // 
            this.gbSearchType.Controls.Add(this.rbSolutionsProjectSearch);
            this.gbSearchType.Controls.Add(this.rbDirectoriesProjectSearch);
            this.gbSearchType.Location = new System.Drawing.Point(16, 107);
            this.gbSearchType.Name = "gbSearchType";
            this.gbSearchType.Size = new System.Drawing.Size(133, 66);
            this.gbSearchType.TabIndex = 23;
            this.gbSearchType.TabStop = false;
            this.gbSearchType.Text = "Project Type";
            // 
            // rbSolutionsProjectSearch
            // 
            this.rbSolutionsProjectSearch.AutoSize = true;
            this.rbSolutionsProjectSearch.Location = new System.Drawing.Point(17, 19);
            this.rbSolutionsProjectSearch.Name = "rbSolutionsProjectSearch";
            this.rbSolutionsProjectSearch.Size = new System.Drawing.Size(104, 17);
            this.rbSolutionsProjectSearch.TabIndex = 21;
            this.rbSolutionsProjectSearch.TabStop = true;
            this.rbSolutionsProjectSearch.Text = "Solutions Project";
            this.rbSolutionsProjectSearch.UseVisualStyleBackColor = true;
            // 
            // rbDirectoriesProjectSearch
            // 
            this.rbDirectoriesProjectSearch.AutoSize = true;
            this.rbDirectoriesProjectSearch.Location = new System.Drawing.Point(17, 42);
            this.rbDirectoriesProjectSearch.Name = "rbDirectoriesProjectSearch";
            this.rbDirectoriesProjectSearch.Size = new System.Drawing.Size(111, 17);
            this.rbDirectoriesProjectSearch.TabIndex = 22;
            this.rbDirectoriesProjectSearch.TabStop = true;
            this.rbDirectoriesProjectSearch.Text = "Directories Project";
            this.rbDirectoriesProjectSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearchProjectName
            // 
            this.txtSearchProjectName.Location = new System.Drawing.Point(160, 29);
            this.txtSearchProjectName.Name = "txtSearchProjectName";
            this.txtSearchProjectName.Size = new System.Drawing.Size(271, 20);
            this.txtSearchProjectName.TabIndex = 18;
            // 
            // lblSearchProjectName
            // 
            this.lblSearchProjectName.AutoSize = true;
            this.lblSearchProjectName.Location = new System.Drawing.Point(13, 32);
            this.lblSearchProjectName.Name = "lblSearchProjectName";
            this.lblSearchProjectName.Size = new System.Drawing.Size(108, 13);
            this.lblSearchProjectName.TabIndex = 17;
            this.lblSearchProjectName.Text = "Search Project Name";
            // 
            // chkNoTest
            // 
            this.chkNoTest.AutoSize = true;
            this.chkNoTest.Location = new System.Drawing.Point(16, 206);
            this.chkNoTest.Name = "chkNoTest";
            this.chkNoTest.Size = new System.Drawing.Size(154, 17);
            this.chkNoTest.TabIndex = 16;
            this.chkNoTest.Text = "Discard Tests From Search";
            this.chkNoTest.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitiveSearch
            // 
            this.chkCaseSensitiveSearch.AutoSize = true;
            this.chkCaseSensitiveSearch.Location = new System.Drawing.Point(16, 183);
            this.chkCaseSensitiveSearch.Name = "chkCaseSensitiveSearch";
            this.chkCaseSensitiveSearch.Size = new System.Drawing.Size(133, 17);
            this.chkCaseSensitiveSearch.TabIndex = 15;
            this.chkCaseSensitiveSearch.Text = "Case Sensitive Search";
            this.chkCaseSensitiveSearch.UseVisualStyleBackColor = true;
            // 
            // cboEditor
            // 
            this.cboEditor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditor.FormattingEnabled = true;
            this.cboEditor.Location = new System.Drawing.Point(160, 55);
            this.cboEditor.Name = "cboEditor";
            this.cboEditor.Size = new System.Drawing.Size(271, 21);
            this.cboEditor.TabIndex = 14;
            this.cboEditor.SelectedIndexChanged += new System.EventHandler(this.CboEditor_SelectedIndexChanged);
            // 
            // lblFileEditor
            // 
            this.lblFileEditor.AutoSize = true;
            this.lblFileEditor.Location = new System.Drawing.Point(13, 58);
            this.lblFileEditor.Name = "lblFileEditor";
            this.lblFileEditor.Size = new System.Drawing.Size(144, 13);
            this.lblFileEditor.TabIndex = 13;
            this.lblFileEditor.Text = "File Editor For Openning Files";
            // 
            // tabPageSolutionsList
            // 
            this.tabPageSolutionsList.Controls.Add(this.btnAddSolution);
            this.tabPageSolutionsList.Controls.Add(this.lstSolutions);
            this.tabPageSolutionsList.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolutionsList.Name = "tabPageSolutionsList";
            this.tabPageSolutionsList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolutionsList.Size = new System.Drawing.Size(438, 237);
            this.tabPageSolutionsList.TabIndex = 0;
            this.tabPageSolutionsList.Text = "Solutions List";
            this.tabPageSolutionsList.UseVisualStyleBackColor = true;
            // 
            // btnAddSolution
            // 
            this.btnAddSolution.Location = new System.Drawing.Point(6, 198);
            this.btnAddSolution.Name = "btnAddSolution";
            this.btnAddSolution.Size = new System.Drawing.Size(424, 32);
            this.btnAddSolution.TabIndex = 5;
            this.btnAddSolution.Text = "Add Solution";
            this.btnAddSolution.UseVisualStyleBackColor = true;
            this.btnAddSolution.Click += new System.EventHandler(this.BtnAddSolution_Click);
            // 
            // lstSolutions
            // 
            this.lstSolutions.FormattingEnabled = true;
            this.lstSolutions.Location = new System.Drawing.Point(6, 6);
            this.lstSolutions.Name = "lstSolutions";
            this.lstSolutions.Size = new System.Drawing.Size(424, 186);
            this.lstSolutions.TabIndex = 3;
            // 
            // tabPageDirectoriesList
            // 
            this.tabPageDirectoriesList.Controls.Add(this.lstDirectories);
            this.tabPageDirectoriesList.Controls.Add(this.btnAddDirectory);
            this.tabPageDirectoriesList.Location = new System.Drawing.Point(4, 22);
            this.tabPageDirectoriesList.Name = "tabPageDirectoriesList";
            this.tabPageDirectoriesList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDirectoriesList.Size = new System.Drawing.Size(438, 237);
            this.tabPageDirectoriesList.TabIndex = 3;
            this.tabPageDirectoriesList.Text = "Directories List";
            this.tabPageDirectoriesList.UseVisualStyleBackColor = true;
            // 
            // lstDirectories
            // 
            this.lstDirectories.FormattingEnabled = true;
            this.lstDirectories.Location = new System.Drawing.Point(6, 6);
            this.lstDirectories.Name = "lstDirectories";
            this.lstDirectories.Size = new System.Drawing.Size(424, 186);
            this.lstDirectories.TabIndex = 7;
            // 
            // btnAddDirectory
            // 
            this.btnAddDirectory.Location = new System.Drawing.Point(6, 197);
            this.btnAddDirectory.Name = "btnAddDirectory";
            this.btnAddDirectory.Size = new System.Drawing.Size(424, 32);
            this.btnAddDirectory.TabIndex = 6;
            this.btnAddDirectory.Text = "Add Directory";
            this.btnAddDirectory.UseVisualStyleBackColor = true;
            this.btnAddDirectory.Click += new System.EventHandler(this.BtnAddDirectory_Click);
            // 
            // frmCreateUpdateSearchProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 263);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCreateUpdateSearchProject";
            this.Text = "Add Search Project";
            this.Load += new System.EventHandler(this.FrmCreateUpdateSearchProject_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.gbSearchLogic.ResumeLayout(false);
            this.gbSearchLogic.PerformLayout();
            this.gbSearchType.ResumeLayout(false);
            this.gbSearchType.PerformLayout();
            this.tabPageSolutionsList.ResumeLayout(false);
            this.tabPageDirectoriesList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label lblSearchJobIndex;
        private System.Windows.Forms.Button btnSaveSeachProject;
        private System.Windows.Forms.GroupBox gbSearchLogic;
        private System.Windows.Forms.RadioButton rbAnd;
        private System.Windows.Forms.RadioButton rbOr;
        private System.Windows.Forms.Label lblFileTypeFilters;
        private System.Windows.Forms.GroupBox gbSearchType;
        private System.Windows.Forms.RadioButton rbSolutionsProjectSearch;
        private System.Windows.Forms.RadioButton rbDirectoriesProjectSearch;
        private System.Windows.Forms.TextBox txtSearchProjectName;
        private System.Windows.Forms.Label lblSearchProjectName;
        private System.Windows.Forms.CheckBox chkNoTest;
        private System.Windows.Forms.CheckBox chkCaseSensitiveSearch;
        private System.Windows.Forms.ComboBox cboEditor;
        private System.Windows.Forms.Label lblFileEditor;
        private System.Windows.Forms.TabPage tabPageSolutionsList;
        private System.Windows.Forms.Button btnAddSolution;
        private System.Windows.Forms.ListBox lstSolutions;
        private System.Windows.Forms.TabPage tabPageDirectoriesList;
        private System.Windows.Forms.ListBox lstDirectories;
        private System.Windows.Forms.Button btnAddDirectory;
        private System.Windows.Forms.ComboBox txtFileTypeFilters;
    }
}