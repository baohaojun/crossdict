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
using System.Threading;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Collections;

namespace FtpList
{
    public class FTPFile
    {
        public static int UNKNOWNTYPE = 0;
        public static int FILETYPE = 1;
        public static int DIRTYPE = 2;
        public static int SYMBOL = 3;

        public string filename = "";
        public int type = UNKNOWNTYPE;
    }

    public class FTPClient
    {
        private string remoteHost, remotePath, remoteUser, remotePass;
        private int remotePort;

        private Socket clientSocket;

        private bool debug = false;
        private bool logined = false;

        private string message;
        private string reply;
        private int retValue, bytes;
        
        private static int BUFFER_SIZE = 512;
        Byte[] buffer = new Byte[BUFFER_SIZE];

        private bool ipv6 = false;
        public Encoding ENCODING = Encoding.UTF8;

        /* The regular expression is from Apache*/
        private static string REGEX = "([bcdelfmpSs-])"
        + "(((r|-)(w|-)([xsStTL-]))((r|-)(w|-)([xsStTL-]))((r|-)(w|-)([xsStTL-])))\\+?\\s+"
        + "(\\d+)\\s+"
        + "(?:(\\S+(?:\\s\\S+)*?)\\s+)?"                // owner name (optional spaces)
        + "(?:(\\S+(?:\\s\\S+)*)\\s+)?"                 // group name (optional spaces)
        + "(\\d+(?:,\\s*\\d+)?)\\s+"

        /*
          numeric or standard format date
        */
        + "((?:\\d+[-/]\\d+[-/]\\d+)|(?:\\S+\\s+\\S+))\\s+"

        /* 
           year (for non-recent standard format) 
           or time (for numeric or recent standard format  
        */
        + "(\\d+(?::\\d+)?)\\s+"

        + "(\\S*)(\\s*.*)";


        Regex entrypattern = new Regex(REGEX, RegexOptions.IgnoreCase);

        public FTPClient(string host, string path, int port, string user, string pass)
        {
            this.remoteHost = host;
            this.remotePath = path;
            this.remotePort = port;
            this.remoteUser = user;
            this.remotePass = pass;
        }

        public void Login()
        {
            try
            {
                IPHostEntry resolvedServer = Dns.GetHostEntry(this.remoteHost);
                IPAddress address = resolvedServer.AddressList[0];

                char[] seperator = { '.' };
                string[] ipsec = address.ToString().Split(seperator);

                Console.WriteLine("ipsec?: {0}", ipsec.Length );

                if (ipsec.Length == 4)
                {
                    this.ipv6 = false;
                }
                else
                {
                    this.ipv6 = true;
                }

                Console.WriteLine("IPv6?: {0}", this.ipv6);

                IPEndPoint ep = new IPEndPoint(address, this.remotePort);
                clientSocket = new
                     Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    clientSocket.Connect(ep);
                }
                catch (Exception ex)
                {
                    throw new IOException("Couldn't connect to remote server." + ex.Message);
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine(
                   "Could not resolve server name: {0}", ex.Message);
            }

            Console.WriteLine("Connecting...");
            
            readReply();

            if (this.retValue != 220)
            {
                Close();
                throw new IOException(this.reply.Substring(4));
            }

            if (this.debug)
                Console.WriteLine("USER " + this.remoteUser);

            sendCommand("USER " + this.remoteUser);

            if (!(this.retValue == 331 || this.retValue == 230))
            {
                cleanup();
                throw new IOException(this.reply.Substring(4));
            }


            if (this.retValue != 230)
            {
                if (this.debug)
                    Console.WriteLine("PASS xxx");

                sendCommand("PASS " + this.remotePass);
                if (!(this.retValue == 230 || this.retValue == 202))
                {
                    this.cleanup();
                    throw new IOException(this.reply.Substring(4));
                }
            }

            this.logined = true;
            Console.WriteLine("Connected to " + this.remoteHost);
            sendCommand("CLNT 0.0.1");
            sendCommand("OPTS UTF8 ON");
            if (this.retValue != 200)
            {
                ENCODING = Encoding.GetEncoding("GBK");
            }
            Console.WriteLine("server encoding: " + ENCODING);

            ChDir(this.remotePath);
        }

        public FTPFile [] getFileList()
        {
            if (!this.logined)
            {
                Login();
            }

            Socket cSocket = createDataSocket();
            
            sendCommand("LIST ");
            
            if (!(retValue == 150 || retValue == 125))
            {
                throw new IOException(reply.Substring(4));
            }

            message = "";

            MemoryStream mem = new MemoryStream(); 

            
            //FileStream log = new FileStream("test.log", FileMode.Create, FileAccess.Write);
            //BufferedStream bs = new BufferedStream(new FileStream("log.log", FileMode.Create, FileAccess.Write));
           // StreamWriter sw = new StreamWriter(bs);
            
            while (true)
            {
                int bytes = cSocket.Receive(buffer, buffer.Length, 0);
                if (bytes > 0)
                {
                    mem.Write(buffer, 0, bytes);
                    //log.Write(buffer, 0, bytes);
                }
                else {
                    break;
                }
            }
            message += ENCODING.GetString(mem.ToArray(), 0, mem.ToArray().Length);
            //sw.WriteLine(message);
            string [] seperator = { "\r\n" };
            string[] mess = message.Split(seperator,StringSplitOptions.RemoveEmptyEntries);


            cSocket.Close();
            readReply();
            if (retValue != 226)
            {
                throw new IOException(reply.Substring(4));
            }

            ArrayList al = new ArrayList ();
            foreach (string s in mess)
            {
              //  sw.Write(s);
                if (s.Trim()!="") {
                    FTPFile f = EntryParser(s);
                    al.Add(f);
                }
            }

           // sw.Close();
           // log.Close();


            FTPFile[] list = new FTPFile[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                list[i] = (FTPFile)al[i];
            }
            return list;
        }

        public void ChDir(string dirName)
        {
            if (dirName.Equals("."))
            {
                return;
            }

            if (!this.logined)
            {
                Login();
            }

            sendCommand("CWD " + dirName);

            if (this.retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
            
            if (this.debug)
            {
                Console.WriteLine("Current directory is " + remotePath);
            }
        }

        public void Close()
        {
            if (clientSocket != null)
            {
                sendCommand("QUIT");
            }
            cleanup();
            Console.WriteLine("Closing...");
        }

        public FTPFile EntryParser(string line)
        {
            MatchCollection mc = this.entrypattern.Matches(line);
            FTPFile file = new FTPFile();
            if (mc.Count > 0)
            {
                string name = mc[0].Groups[21].Value;
                string endtoken = mc[0].Groups[22].Value;
               
                string type = mc[0].Groups[1].Value;
                char t = type[0];
                if (t == 'd')
                {
                    file.type = FTPFile.DIRTYPE;
                }
                else if (t == 'f' || t == '-')
                {
                    file.type = FTPFile.FILETYPE;
                }
                else if (t == 'e' || t == 'l')
                {
                    file.type = FTPFile.SYMBOL;
                }

                if (endtoken.Length == 0)
                {
                    file.filename = name;
                }
                else
                {
                    name += endtoken;
                    if (file.type == FTPFile.SYMBOL)
                    {
                        int end = name.IndexOf(" -> ");
                        // Give up if no link indicator is present
                        if (end == -1)
                        {
                            file.filename = name;
                        }
                        else
                        {
                            file.filename=name.Substring(0, end);
                        }

                    }
                    else
                    {
                        file.filename = name;
                    }
                }
            }
            return file;
        }

        private void readReply()
        {
            message = "";
            reply = readLine();
            retValue = Int32.Parse(reply.Substring(0, 3));
        }

        private string readLine()
        {
            MemoryStream mem = new MemoryStream(); 
            while (true)
            {
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                if (bytes > 0)
                {
                    mem.Write(buffer, 0, bytes);
                }
                if (bytes < buffer.Length)
                {
                    break;
                }
            }

            message += ENCODING.GetString(mem.ToArray(), 0, mem.ToArray().Length);

            char[] seperator = { '\n' };
            string[] mess = message.Split(seperator);
            
            if (message.Length > 2)
            {
                message = mess[mess.Length - 2];
            }
            else
            {
                message = mess[0];
            }

            if (!message.Substring(3, 1).Equals(" "))
            {
                return readLine();
            }
            
            if (this.debug)
            {
                for (int k = 0; k < mess.Length - 1; k++)
                {
                    Console.WriteLine(mess[k]);
                }
            }
            return message;
        }

        private void sendCommand(String command)
        {
            Byte[] cmdBytes =
                 ENCODING.GetBytes((command + "\r\n").ToCharArray());

            //if (this.debug)
            {
                Console.WriteLine("****" + command + "++++");
            }

            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
            readReply();
        }

        private void cleanup()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
            this.logined = false;
        }

        private Socket createDataSocket()
        {
            if (!this.ipv6) // ipv4
            {
                sendCommand("PASV");
                if (this.retValue != 227)
                {
                    throw new IOException(reply.Substring(4));
                }

                int index1 = this.reply.IndexOf('(');
                int index2 = this.reply.IndexOf(')');
                string ipData =
                     reply.Substring(index1 + 1, index2 - index1 - 1);
                int[] parts = new int[6];

                int len = ipData.Length;
                int partCount = 0;
                string buf = "";

                for (int i = 0; i < len && partCount <= 6; i++)
                {
                    char ch = Char.Parse(ipData.Substring(i, 1));
                    if (Char.IsDigit(ch))
                    {
                        buf += ch;
                    }
                    else if (ch != ',')
                    {
                        throw new IOException("Malformed PASV reply: " + this.reply);
                    }

                    if (ch == ',' || i + 1 == len)
                    {
                        try
                        {
                            parts[partCount++] = Int32.Parse(buf);
                            buf = "";
                        }
                        catch (Exception)
                        {
                            throw new IOException("Malformed PASV reply: " + this.reply);
                        }
                    }
                }

                string ipAddress = parts[0] + "." + parts[1] + "." +
                     parts[2] + "." + parts[3];

                int port = (parts[4] << 8) + parts[5];

                Socket s = new
                     Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5000);

                IPEndPoint ep = new
                     IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);

                try
                {
                    s.Connect(ep);
                }
                catch (Exception)
                {
                    throw new IOException("Can't connect to remote server");
                }
                return s;
            }
            else
            {
                sendCommand("EPSV");
                if (this.retValue != 229)
                {
                    throw new IOException(reply.Substring(4));
                }
                int index1 = this.reply.IndexOf('(');
                int index2 = this.reply.IndexOf(')');
                string portData =
                    this.reply.Substring(index1 + 4, index2 - index1 - 1 -4);

                int dataPort = Int32.Parse(portData);

                IPHostEntry resolvedServer = Dns.GetHostEntry(this.remoteHost);
                IPAddress address = resolvedServer.AddressList[0];
                IPEndPoint ep = new
                     IPEndPoint(address, dataPort);
                Socket s = new
                     Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5000);
                try
                {
                    s.Connect(ep);
                }
                catch (Exception)
                {
                    throw new IOException("Can't connect to remote server");
                }
                return s;
            }
        }
    }
}
