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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;

namespace sosodadaCheater
{
    class MockLogin
    {
        public delegate void updateInfo(string curr, string info);
        public event updateInfo onInfoUpdate;

        private string email;
        private string password;
        private CookieContainer cookieContainer;
        private string host = "http://www.sosodada.com";
		private string loginurl = "/ULogin.aspx";
        private string infourl = "/Print_Member/DefaultInfo.aspx";

        public MockLogin(string e, string p)
        {
            this.email = e;
            this.password = p;
            this.cookieContainer = new CookieContainer();
        }

        public string Login()
        {
            string formatString = "__VIEWSTATE={0}&__EVENTVALIDATION={1}&ctl00$ContentPlaceHolder1$txtUserName={2}&ctl00$ContentPlaceHolder1$txtUserPwd={3}&ctl00$ContentPlaceHolder1$imgbtnLogin.x={4}&ctl00$ContentPlaceHolder1$imgbtnLogin.y={5}&__EVENTARGUMENT={6}&__EVENTTARGET={7}";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.host + this.loginurl);
            req.Method = "GET";
            req.CookieContainer = this.cookieContainer;
            WebResponse wr = req.GetResponse();
            Stream resp = wr.GetResponseStream();
            StreamReader reader = new StreamReader(resp);
            string page = reader.ReadToEnd();
            Regex viewstate = new Regex("id=\"__VIEWSTATE\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection view_mc = viewstate.Matches(page);
            Regex valid = new Regex("id=\"__EVENTVALIDATION\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection valid_mc = valid.Matches(page);

            string postString = string.Format(formatString,
                HttpUtility.UrlEncode(view_mc[0].Groups[1].Value),
                HttpUtility.UrlEncode(valid_mc[0].Groups[1].Value),
                HttpUtility.UrlEncode(this.email),
                HttpUtility.UrlEncode(this.password),
                "15", "13", "", "");
            //*
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            req = (HttpWebRequest)HttpWebRequest.Create(this.host + this.loginurl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.CookieContainer = this.cookieContainer;
            req.ContentLength = postData.Length;
            Stream outputStream = req.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();
            resp = req.GetResponse().GetResponseStream();
            reader = new StreamReader(resp, Encoding.UTF8);
            return reader.ReadToEnd();
          //*/
        }

        public string SkipQuestions(string content)
        {
            Regex viewstate = new Regex("id=\"__VIEWSTATE\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection view_mc = viewstate.Matches(content);
            Regex valid = new Regex("id=\"__EVENTVALIDATION\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection valid_mc = valid.Matches(content);
            Regex urlpattern = new Regex("method=\"post\" action=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection action_mc = urlpattern.Matches(content);
            string action = action_mc[0].Groups[1].Value;

            string formatString = "__VIEWSTATE={0}&__EVENTVALIDATION={1}&imgbtnNext.x={2}&imgbtnNext.y={3}";
            string postString = string.Format(formatString,
                HttpUtility.UrlEncode(view_mc[0].Groups[1].Value),
                HttpUtility.UrlEncode(valid_mc[0].Groups[1].Value),
                "28", "18");

            byte[] postData = Encoding.UTF8.GetBytes(postString);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.host + "/" + action);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.CookieContainer = this.cookieContainer;
            req.ContentLength = postData.Length;
            Stream outputStream = req.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();
            Stream resp = req.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(resp, Encoding.UTF8);
            return reader.ReadToEnd();

        }

        public string GetScore(string content)
        {
            Regex viewstate = new Regex("id=\"__VIEWSTATE\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection view_mc = viewstate.Matches(content);
            Regex valid = new Regex("id=\"__EVENTVALIDATION\" value=\"([^\"]+)\"", RegexOptions.IgnoreCase);
            MatchCollection valid_mc = valid.Matches(content);

            string formatString = "__VIEWSTATE={0}&__EVENTVALIDATION={1}&__EVENTARGUMENT={2}&__EVENTTARGET={3}&ctl00$ContentPlaceHolder1$btnGetScores={4}";
            string postString = string.Format(formatString,
                HttpUtility.UrlEncode(view_mc[0].Groups[1].Value),
                HttpUtility.UrlEncode(valid_mc[0].Groups[1].Value),
                "", "",
                "%E9%A2%86%E5%8F%96%E7%A7%AF%E5%88%86");

            byte[] postData = Encoding.UTF8.GetBytes(postString);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.host + this.infourl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.CookieContainer = this.cookieContainer;
            req.ContentLength = postData.Length;
            Stream outputStream = req.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();
            Stream resp = req.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(resp, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public void ShowScore(string i, string content)
        {
            string curr = "";
            string info = "";

            Regex currpattern = new Regex("<span id=\".*?\"><i><font face=\".*?\" color=\".*?\" size=\".*?\">(.*?)</font></i></span>", RegexOptions.IgnoreCase);
            MatchCollection curr_mc = currpattern.Matches(content);
            Regex infopattern = new Regex("<div id=\"ctl00_ContentPlaceHolder1_GetScores\" style=\"width: 332px; text-align: center;\">(.*?)</div>", RegexOptions.IgnoreCase);
            MatchCollection info_mc = infopattern.Matches(content);
            curr = curr_mc[0].Groups[1].Value;
            info = info_mc[0].Groups[1].Value;
            if (onInfoUpdate != null)
            {
                onInfoUpdate("("+i+"), "+curr, info);
            }
        
        }

        public static int testMain()
        {
            MockLogin log = new MockLogin("", "");
            string content = log.Login();
            content = log.SkipQuestions(content);
            content = log.GetScore(content);
            Console.Write(content);
            return 0;
        }
    }
}
