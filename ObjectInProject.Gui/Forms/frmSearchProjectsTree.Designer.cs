namespace ObjectInProject.Gui
{
    partial class frmSearchProjectsTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchProjectsTree));
            this.tvSearchProjects = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvSearchProjects
            // 
            this.tvSearchProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSearchProjects.Location = new System.Drawing.Point(0, 0);
            this.tvSearchProjects.Name = "tvSearchProjects";
            this.tvSearchProjects.Size = new System.Drawing.Size(800, 450);
            this.tvSearchProjects.TabIndex = 0;
            // 
            // frmSearchProjectsTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tvSearchProjects);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSearchProjectsTree";
            this.Text = "Search Projects";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSearchProjectsTree_FormClosed);
            this.Load += new System.EventHandler(this.FrmSolutionsTree_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvSearchProjects;
    }
}