/**
Copyright 2009 http://code.google.com/p/toolkits/. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:
  * Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.
  * Neither the name of http://code.google.com/p/toolkits/ nor the names of its
    contributors may be used to endorse or promote products derived
    from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Web;

namespace HiBaiduAlbumDownloader
{
    public partial class AlbumDownloader : Form
    {
        public AlbumDownloader()
        {
            InitializeComponent();
        }

        private void AlbumDownloader_Load(object sender, EventArgs e)
        {
            this.textBoxUserName.Text = "";
            this.textBoxDestDir.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSelectDir_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDestDir.Text = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //TODO: check username and dirname
            MethodInvoker mi = new MethodInvoker(this.DownloadImages);
            mi.BeginInvoke(null, null);
        }

        private void linkPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/toolkits/wiki/HiBaiduAlbumDownloader");
        }

        private void DownloadImages()
        {
            HiBaiduAlbumDownloader dler = new HiBaiduAlbumDownloader(this.textBoxUserName.Text);
            DiskImagePersistencer p = new DiskImagePersistencer(dler, this.textBoxDestDir.Text);
            p.init();
            dler.setPersistence(p);
            dler.onDownLoadProgress += new HiBaiduAlbumDownloader.dDownloadProgress(downloading);
            dler.onInfoChange += new HiBaiduAlbumDownloader.dChangeInfo(changeinfo); 
            dler.getAll();
            MessageBox.Show("Download OK");
        }

        private void downloading(long total, long current)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HiBaiduAlbumDownloader.dDownloadProgress(downloading), new object[]{ total, current });
            }
            else {
                this.DownloadingProgress.Maximum = (int)total;
                this.DownloadingProgress.Value = (int)current;
            }
        }

        private void changeinfo(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HiBaiduAlbumDownloader.dChangeInfo(changeinfo), new object[] {info});
            }
            else
            {
                this.labelDownloading.Text = info;
            }
        }
    }
}
