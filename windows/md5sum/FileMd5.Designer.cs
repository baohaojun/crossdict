namespace md5sum
{
    partial class FileMd5
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
            this.Exit = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SelectFile = new System.Windows.Forms.Button();
            this.File = new System.Windows.Forms.Label();
            this.md5 = new System.Windows.Forms.Label();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.textMd5sum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(160, 69);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(52, 24);
            this.Exit.TabIndex = 0;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(72, 69);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(50, 24);
            this.SelectFile.TabIndex = 1;
            this.SelectFile.Text = "SelectFile";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // File
            // 
            this.File.AutoSize = true;
            this.File.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.File.Location = new System.Drawing.Point(6, 10);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(34, 20);
            this.File.TabIndex = 2;
            this.File.Text = "File";
            // 
            // md5
            // 
            this.md5.AutoSize = true;
            this.md5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.md5.Location = new System.Drawing.Point(6, 41);
            this.md5.Name = "md5";
            this.md5.Size = new System.Drawing.Size(40, 20);
            this.md5.TabIndex = 3;
            this.md5.Text = "md5";
            // 
            // textFileName
            // 
            this.textFileName.Location = new System.Drawing.Point(55, 12);
            this.textFileName.Name = "textFileName";
            this.textFileName.ReadOnly = true;
            this.textFileName.Size = new System.Drawing.Size(210, 20);
            this.textFileName.TabIndex = 4;
            // 
            // textMd5sum
            // 
            this.textMd5sum.Location = new System.Drawing.Point(52, 43);
            this.textMd5sum.Name = "textMd5sum";
            this.textMd5sum.ReadOnly = true;
            this.textMd5sum.Size = new System.Drawing.Size(213, 20);
            this.textMd5sum.TabIndex = 5;
            // 
            // FileMd5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 99);
            this.Controls.Add(this.textMd5sum);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.md5);
            this.Controls.Add(this.File);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.Exit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileMd5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File md5sum";
            this.Load += new System.EventHandler(this.FileMd5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Label File;
        private System.Windows.Forms.Label md5;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.TextBox textMd5sum;
    }
}

