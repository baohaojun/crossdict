namespace HiBaiduAlbumDownloader
{
    partial class AlbumDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumDownloader));
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelDestDir = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.buttonSelectDir = new System.Windows.Forms.Button();
            this.textBoxDestDir = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.linkPage = new System.Windows.Forms.LinkLabel();
            this.DownloadingProgress = new System.Windows.Forms.ProgressBar();
            this.labelDownloading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelUserName
            // 
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.BackColor = System.Drawing.SystemColors.Control;
            this.labelUserName.Name = "labelUserName";
            // 
            // labelDestDir
            // 
            resources.ApplyResources(this.labelDestDir, "labelDestDir");
            this.labelDestDir.Name = "labelDestDir";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.Name = "textBoxUserName";
            // 
            // buttonSelectDir
            // 
            resources.ApplyResources(this.buttonSelectDir, "buttonSelectDir");
            this.buttonSelectDir.Name = "buttonSelectDir";
            this.buttonSelectDir.UseVisualStyleBackColor = true;
            this.buttonSelectDir.Click += new System.EventHandler(this.buttonSelectDir_Click);
            // 
            // textBoxDestDir
            // 
            resources.ApplyResources(this.textBoxDestDir, "textBoxDestDir");
            this.textBoxDestDir.Name = "textBoxDestDir";
            this.textBoxDestDir.ReadOnly = true;
            // 
            // linkPage
            // 
            resources.ApplyResources(this.linkPage, "linkPage");
            this.linkPage.Name = "linkPage";
            this.linkPage.TabStop = true;
            this.linkPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPage_LinkClicked);
            // 
            // DownloadingProgress
            // 
            resources.ApplyResources(this.DownloadingProgress, "DownloadingProgress");
            this.DownloadingProgress.Name = "DownloadingProgress";
            // 
            // labelDownloading
            // 
            resources.ApplyResources(this.labelDownloading, "labelDownloading");
            this.labelDownloading.Name = "labelDownloading";
            // 
            // AlbumDownloader
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDownloading);
            this.Controls.Add(this.DownloadingProgress);
            this.Controls.Add(this.linkPage);
            this.Controls.Add(this.textBoxDestDir);
            this.Controls.Add(this.buttonSelectDir);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelDestDir);
            this.Controls.Add(this.labelUserName);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "AlbumDownloader";
            this.Load += new System.EventHandler(this.AlbumDownloader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelDestDir;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button buttonSelectDir;
        private System.Windows.Forms.TextBox textBoxDestDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel linkPage;
        private System.Windows.Forms.ProgressBar DownloadingProgress;
        private System.Windows.Forms.Label labelDownloading;
    }
}

