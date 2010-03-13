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
using System.IO;
using System.Security.Cryptography;

namespace md5sum
{
    public partial class FileMd5 : Form
    {
        private string filename;
        private string md5sum;

        public delegate void dChangeInfo(string info);
        public event dChangeInfo onInfoChange;

        public FileMd5()
        {
            InitializeComponent();
            this.filename = "";
            this.md5sum = "";
            this.openFileDialog = new OpenFileDialog();
        }

        private void FileMd5_Load(object sender, EventArgs e)
        {

        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filename = this.openFileDialog.FileName;
                textFileName.Text = this.filename;
                textMd5sum.Text = "Calculating";

                MethodInvoker mi = new MethodInvoker(this.Md5sum);
                mi.BeginInvoke(null, null);

            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Md5sum()
        {
            this.onInfoChange += new dChangeInfo(changeinfo);
            try
            {
                FileStream fs = System.IO.File.OpenRead(this.filename);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(fs);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sBuilder.Append(result[i].ToString("X2"));
                }
                this.md5sum = sBuilder.ToString();
                //this.md5sum = BitConverter.ToString(result);
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
            }
            if (onInfoChange != null)
            {
                onInfoChange(this.md5sum);
            }
        }

        private void changeinfo(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new dChangeInfo(changeinfo), new object[] { info });
            }
            else
            {
                this.textMd5sum.Text = info;
            }
        }
    }
}
