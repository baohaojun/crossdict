/**
Copyright 2010 http://code.google.com/p/toolkits/. All rights reserved.

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
using System.IO;
using System.Net;
using System.Text;

namespace FtpList
{
    class FtpList
    {
        static void Traverse(FTPClient client, StreamWriter sw, string dir, int depth)
        {
            try
            {
                client.ChDir(dir);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            FTPFile[] lists = client.getFileList();
            for (int i = 0; i < lists.Length; i++)
            {
                FTPFile f = lists[i];
                Byte[] enc = client.ENCODING.GetBytes(f.filename);
                byte[] def = Encoding.Convert(client.ENCODING, Encoding.Default, enc);
                string result = Encoding.Default.GetString(def);

                for (int j = 0; j < depth; j++)
                {
                    Console.Write("--");
                    sw.Write("--");
                }
                Console.WriteLine(result);
                sw.WriteLine(result);
                
                if (f.type == FTPFile.DIRTYPE)
                {
                    if (!(f.filename.Equals(".") || f.filename.Equals("..")))
                    {
                        Traverse(client, sw, result, depth + 1);
                    }
                }
            }
            client.ChDir("..");
        }

        static void Traverse(FTPClient client, string dir, int depth)
        {
            try
            {
                client.ChDir(dir);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            FTPFile[] lists = client.getFileList();
            for (int i = 0; i < lists.Length; i++)
            {
                FTPFile f = lists[i];
                Byte[] enc = client.ENCODING.GetBytes(f.filename);
                byte[] def = Encoding.Convert(client.ENCODING, Encoding.Default, enc);
                string result = Encoding.Default.GetString(def);

                for (int j = 0; j < depth; j++)
                {
                    Console.Write("--");

                }
                Console.WriteLine(result);

                if (f.type == FTPFile.DIRTYPE)
                {
                    if (!(f.filename.Equals(".") || f.filename.Equals("..")))
                    {
                        Traverse(client, result, depth + 1);
                    }
                }
            }
            client.ChDir("..");
        }


        static void Main(string[] args)
        {
            /*
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://movie.ipv6.scau.edu.cn/");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("anonymous", "toolkits@googlecode.com");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

            reader.Close();
            response.Close();
             * */
            string user = "";
            string pass = "";
            string host = "";
            int port = 21;

            if (args.Length < 3)
            {
                Console.WriteLine("USAGE:");
                Console.WriteLine("      ftplist.exe host[:port] username password");
            }
            else
            {
                host = args[0];
                user = args[1];
                pass = args[2];

                char[] seperator = { ':' };
                string[] mess = host.Split(seperator);
                if (mess.Length == 2)
                {
                    host = mess[0];
                    port = Int32.Parse(mess[1]);
                }

                //**
                FTPClient client = new FTPClient(host, "/", port, user, pass);
                Traverse(client, "/", 0);
                //*/

            }

            /*
            FTPClient client = new FTPClient("movie.ipv6.scau.edu.cn", "/", 21, "anonymous", "toolkits@googlecode.com");
            //FTPClient client = new FTPClient("consummate.3322.org", "/", 2121, "HSC", "HSC");
            //FTPClient client = new FTPClient("162.105.69.120", "/_public/upload/", 21, "sms", "sms");
            BufferedStream bs = new BufferedStream(new FileStream("console.log", FileMode.Create, FileAccess.Write));
            StreamWriter sw = new StreamWriter(bs);
            try
            {
                Traverse(client, sw, "/", 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sw.Close();
                client.Close();
            }
            //*/
        }
    }
}
