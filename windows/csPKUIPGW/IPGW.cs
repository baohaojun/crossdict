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
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading;

namespace csPKUIPGW
{
    public partial class IPGW : Form
    {
        public IPGW()
        {
            InitializeComponent();
        }

        private string magic = "|;kiDrqvfi7d$v0p5Fg72Vwbv2;|";

        private void IPGW_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\csIPGW");
                Object username = key.GetValue("username");
                this.textUserName.Text = username.ToString();
                Object password = key.GetValue("password");
                this.textPassword.Text = password.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void textUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string username = this.textUserName.Text;
            string password = this.textPassword.Text;
            Thread  th=new Thread(new ThreadStart(this.LoginITS));
            th.Start();
            SavePassword();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void textPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void SavePassword()
        {
            string username = this.textUserName.Text;
            string password = this.textPassword.Text;
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\csIPGW",true);
                key.SetValue("username", username);
                key.SetValue("password", password);
            }
            catch (Exception)
            {
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                    RegistryKey ipgw = key.CreateSubKey("csIPGW");
                    ipgw.SetValue("username", username);
                    ipgw.SetValue("password", password);
                }
                catch (Exception message)
                {
                    MessageBox.Show(message.ToString());
                }
                
            }
        }

        private void LoginITS()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/cas/login");
            req.Method = "GET";
            req.CookieContainer = cookieContainer;
            WebResponse wr = req.GetResponse();
            Stream resp = wr.GetResponseStream();
            StreamReader reader = new StreamReader(resp, Encoding.UTF8);
            string page = reader.ReadToEnd();
            Regex ltpattern = new Regex("name=\"lt\" value=\"([^\"]+)\"");
            string lt = ltpattern.Match(page).Groups[1].Value;
            string username = HttpUtility.UrlEncode(this.textUserName.Text);
            string password = HttpUtility.UrlEncode(this.textPassword.Text);
            string formatString = "lt={0}&username1={1}&password={2}&_currentStateId={3}&_eventId={4}&username={5}";
            string u = HttpUtility.UrlEncode(username + this.magic + password + this.magic + '2');
            string postString = string.Format(formatString, lt, username, password, "viewLoginForm", "submit", u);
            byte[] postData = Encoding.ASCII.GetBytes(postString);
            req = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/cas/login");
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.CookieContainer = cookieContainer;
            req.ContentLength = postData.Length;
            Stream outputStream = req.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();
            resp = req.GetResponse().GetResponseStream();
            reader = new StreamReader(resp, Encoding.UTF8);
            req = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/netportal/");
            req.Method = "GET";
            req.CookieContainer = cookieContainer;
            resp = req.GetResponse().GetResponseStream();
            reader = new StreamReader(resp, Encoding.UTF8);
            reader.ReadToEnd();
            req = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/netportal/ipgwcloseall");
            req.Method = "GET";
            req.CookieContainer = cookieContainer;
            resp = req.GetResponse().GetResponseStream();
            reader = new StreamReader(resp, Encoding.UTF8);
            reader.ReadToEnd();
            req = (HttpWebRequest)HttpWebRequest.Create("https://its.pku.edu.cn/netportal/ipgwopen");
            req.Method = "GET";
            req.CookieContainer = cookieContainer;
            resp = req.GetResponse().GetResponseStream();
            reader = new StreamReader(resp, Encoding.UTF8);
            Regex client = new Regex("<!--IPGWCLIENT_START (.*) IPGWCLIENT_END-->");
            string result = reader.ReadToEnd();
            MessageBox.Show(client.Match(result).Groups[1].Value);
        }
    }
}
