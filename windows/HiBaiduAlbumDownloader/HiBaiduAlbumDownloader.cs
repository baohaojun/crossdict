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
using System.Net;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace HiBaiduAlbumDownloader
{
    class HiBaiduAlbumDownloader
    {
        private ImagePersistencer persistencer;
        public string username;
        private CookieContainer cookieContainer;

        public delegate void dDownloadProgress(long total, long current);
        public event dDownloadProgress onDownLoadProgress;

        public delegate void dChangeInfo(string info);
        public event dChangeInfo onInfoChange;

        public HiBaiduAlbumDownloader(string username)
        {
            this.persistencer = null;
            username = HttpUtility.UrlDecode(username, Encoding.GetEncoding("GBK"));
            this.username = HttpUtility.UrlEncode(username, Encoding.GetEncoding("GBK")).ToUpper();
            MessageBox.Show(this.username);
            this.cookieContainer = new CookieContainer();
            
        }

        public bool setPersistence(ImagePersistencer p)
        {
            this.persistencer = p;
            return true;
        }

        public List<string> getAllAlbums()
        {
            Regex albumpattern = new Regex("imgarr\\[len\\]={purl:\"/" + this.username + "/album/(.+?)\",", RegexOptions.IgnoreCase);
            List<string> albumlist = new List<string>();
            string baseurl = "http://hi.baidu.com/" + this.username + "/album/index/";
            int idx = 0;
            bool cont = true;
            while (cont)
            {
                cont = false;
                string url = baseurl + idx;
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.CookieContainer = this.cookieContainer;
                WebResponse wr = req.GetResponse();
                Stream resp = wr.GetResponseStream();
                StreamReader reader = new StreamReader(resp);
                string page = reader.ReadToEnd();
                MatchCollection mc = albumpattern.Matches(page);
                for (int i = 0; i < mc.Count; i++)
                {
                    cont = true;
                    albumlist.Add(mc[i].Groups[1].Value);
                }
                idx++;
            }
            return albumlist;
        }

        public bool getImages(string albumname)
        {
            Regex imagepattern = new Regex("imgarr\\[len\\]={purl:\"/" + this.username + "/album/item/(.+?).html\",", RegexOptions.IgnoreCase);
            List<string> imageid = new List<string>();
            int idx = 0;
            bool cont = true;
            while (cont)
            {
                cont = false;
                string albumurl = "http://hi.baidu.com/" + this.username + "/album/" + albumname + "/index/" + idx;
                idx++;
                //MessageBox.Show(albumurl);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(albumurl);
                req.Method = "GET";
                req.CookieContainer = this.cookieContainer;
                req.Accept = "*/*";
                req.Referer = "http://hi.baidu.com/" + this.username + "/blog";
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1;OfficeLiveConnector.1.2)";
                WebResponse wr = req.GetResponse();
                Stream resp = wr.GetResponseStream();
                StreamReader reader = new StreamReader(resp);
                string page = reader.ReadToEnd();
                Regex nextpatten = new Regex("/" + this.username + "/album/.+?/index/" + idx, RegexOptions.IgnoreCase);
                if (nextpatten.IsMatch(page))
                {
                    cont = true;
                }
                MatchCollection mc = imagepattern.Matches(page);
                for (int i = 0; i < mc.Count; i++)
                {
                    imageid.Add(mc[i].Groups[1].Value);
                }
            }
            int curr = 0;
            foreach (string image in imageid)
            {
                if (onDownLoadProgress != null)
                {
                    onDownLoadProgress(imageid.Count, curr);
                }
                curr++;
                Thread.Sleep(1000);
                bool retry = true;
                while (retry)
                {
                    retry = false;
                    string imageurl = "http://hiphotos.baidu.com/" + this.username + "/pic/item/" + image + ".jpg";
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(imageurl);
                    req.Method = "GET";
                    req.CookieContainer = this.cookieContainer;
                    req.Accept = "*/*";
                    req.Referer = "http://hi.baidu.com/" + this.username + "/blog";
                    req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1;OfficeLiveConnector.1.2)";
                    WebResponse wr = req.GetResponse();
                    int size = (int)wr.ContentLength;
                    Stream resp = wr.GetResponseStream();
                    MemoryStream mem = new MemoryStream(); 
                    BinaryReader br = new BinaryReader(resp);
                    byte[] buf = new byte[1024];
                    int r = 0;
                    while (size > 0)
                    {
                        r = br.Read(buf, 0, 1024);
                        size -= r;
                        if (r > 0)
                        {
                            mem.Write(buf, 0, r);
                        }
                    }
                    byte[] picbytes = mem.ToArray();
                    this.persistencer.persistence(albumname, image + ".jpg", picbytes);
                }
            }

            return true;
        }

        public bool getAll()
        {
            List<string> albumlist = this.getAllAlbums();
            MessageBox.Show("Album Num." + albumlist.Count);
            if (onInfoChange != null)
            {
                onInfoChange("Album Num." +albumlist.Count);
            }
            foreach (string album in albumlist)
            {
                if (onInfoChange != null)
                {
                    onInfoChange("Downloading " + HttpUtility.UrlDecode(album, Encoding.GetEncoding("GBK")));
                }
                this.getImages(album);
            }
            if (onDownLoadProgress != null)
            {
                onDownLoadProgress(0, 0);
            }
            if (onInfoChange != null)
            {
                onInfoChange("Download Finished");
            }
            return true;
        }

        public void check()
        {
        }
    }
}
