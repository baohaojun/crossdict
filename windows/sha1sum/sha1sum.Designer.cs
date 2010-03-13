namespace sha1sum
{
    partial class sha1sum
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
            this.labelFile = new System.Windows.Forms.Label();
            this.textFile = new System.Windows.Forms.TextBox();
            this.labelSha1sum = new System.Windows.Forms.Label();
            this.textSha1sum = new System.Windows.Forms.TextBox();
            this.SelectFile = new System.Windows.Forms.Button();
            this.Exist = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFile.Location = new System.Drawing.Point(12, 11);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(34, 20);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "File";
            // 
            // textFile
            // 
            this.textFile.Location = new System.Drawing.Point(61, 11);
            this.textFile.Name = "textFile";
            this.textFile.ReadOnly = true;
            this.textFile.Size = new System.Drawing.Size(294, 20);
            this.textFile.TabIndex = 1;
            // 
            // labelSha1sum
            // 
            this.labelSha1sum.AutoSize = true;
            this.labelSha1sum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSha1sum.Location = new System.Drawing.Point(2, 45);
            this.labelSha1sum.Name = "labelSha1sum";
            this.labelSha1sum.Size = new System.Drawing.Size(74, 20);
            this.labelSha1sum.TabIndex = 2;
            this.labelSha1sum.Text = "sha1sum";
            // 
            // textSha1sum
            // 
            this.textSha1sum.Location = new System.Drawing.Point(76, 47);
            this.textSha1sum.Name = "textSha1sum";
            this.textSha1sum.ReadOnly = true;
            this.textSha1sum.Size = new System.Drawing.Size(279, 20);
            this.textSha1sum.TabIndex = 3;
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(41, 79);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(70, 22);
            this.SelectFile.TabIndex = 4;
            this.SelectFile.Text = "SelectFile";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // Exist
            // 
            this.Exist.Location = new System.Drawing.Point(216, 79);
            this.Exist.Name = "Exist";
            this.Exist.Size = new System.Drawing.Size(76, 21);
            this.Exist.TabIndex = 5;
            this.Exist.Text = "Exist";
            this.Exist.UseVisualStyleBackColor = true;
            this.Exist.Click += new System.EventHandler(this.Exist_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // sha1sum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 106);
            this.Controls.Add(this.Exist);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.textSha1sum);
            this.Controls.Add(this.labelSha1sum);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.labelFile);
            this.MinimizeBox = false;
            this.Name = "sha1sum";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "sha1sum";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textFile;
        private System.Windows.Forms.Label labelSha1sum;
        private System.Windows.Forms.TextBox textSha1sum;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button Exist;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

