using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Network.FTP
{
    public class FTPClient : IDisposable
    {

        private bool _usePassive = false;
        private Socket client;
        private Socket data;
        private TransferModes _transferMode = TransferModes.Binary;
        private int _timeout = 30;
        private Thread workThread;
        private string _lastLine = null;
        private string _remoteServer = null;
        private bool _isAuthenticated = false;
        private ConnectionState _lastConnectResult = ConnectionState.Disconnected;
        private string _currentDir = null;
        private bool _allowOverwrite = true;
        private int _minPort = IPEndPoint.MinPort;
        private int _maxPort = IPEndPoint.MaxPort;

        private System.DateTime _lastCommandSent = System.DateTime.Now;
        public event FTPMessageHandler LogMessage;

        public bool IsConnected
        {
            get
            {
                if ((client != null))
                {
                    return client.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        public ConnectionState LastConnectionResult
        {
            get { return _lastConnectResult; }
        }

        public string RemoteServer
        {
            get
            {
                if ((client != null))
                {
                    if (_remoteServer == null)
                    {
                        return ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                    }
                    else
                    {
                        return _remoteServer;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public int RemotePort
        {
            get
            {
                if ((client != null))
                {
                    return ((IPEndPoint)client.RemoteEndPoint).Port;
                }
                else
                {
                    return -1;
                }
            }
        }

        public string LastLine
        {
            get { return _lastLine; }
        }

        public TransferModes TransferMode
        {
            get { return _transferMode; }
            set { _transferMode = value; }
        }

        public bool PassiveMode
        {
            get { return _usePassive; }
            set { _usePassive = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string CurrentDirectory
        {
            get { return _currentDir; }
        }

        public bool AllowOverwriteLocal
        {
            get { return _allowOverwrite; }
            set { _allowOverwrite = value; }
        }

        public int MinPort
        {
            get { return _minPort; }
            set
            {
                if (value > MaxPort)
                    throw new ArgumentException("Cannot be greater than the value of MaxPort.", "MinPort");
                if (value < IPEndPoint.MinPort)
                    throw new ArgumentException("Cannot be less than " + IPEndPoint.MinPort + ".", "MinPort");
                _minPort = value;
            }
        }

        public int MaxPort
        {
            get { return _maxPort; }
            set
            {
                if (value > IPEndPoint.MaxPort)
                    throw new ArgumentException("Cannot be greater than " + IPEndPoint.MaxPort + ".", "MaxPort");
                if (value < MinPort)
                    throw new ArgumentException("Cannot be less than the value of MinPort.", "MaxPort");
                _maxPort = value;
            }
        }


        public FTPClient()
        {
        }

        public FTPClient(string server, int port = 21, string username = "anonymous@isp.com", string password = "")
        {
            Connect(server, port, username, password);
        }

        public bool Connect(string server, int port = 21, string username = "anonymous@isp.com", string password = "")
        {
            _lastConnectResult = ConnectionState.Disconnected;
            if (IsConnected)
                Disconnect();
            _isAuthenticated = false;
            try
            {
                IPAddress address = null;
                if (Regex.Match(server, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}").Success)
                {
                    address = IPAddress.Parse(server);
                    _remoteServer = null;
                }
                else
                {
                    IPHostEntry entry = Dns.GetHostEntry(server);
                    address = entry.AddressList[0];
                    _remoteServer = server;
                }
                string user = username;
                if (username == "anonymous@isp.com")
                    user = "Anonymous User";
                if (LogMessage != null)
                {
                    LogMessage(this, new FTPMessageEventArgs("Connecting to server '" + server + "' as user '" + user + "'"));
                }
                IPEndPoint remote = new IPEndPoint(address, port);
                client = new Socket(remote.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(remote);
                string response = ReadLine();
                if (response == null)
                {
                    if (LogMessage != null)
                    {
                        LogMessage(this, new FTPMessageEventArgs("Failed to connect to server, no response."));
                    }
                    Disconnect();
                    return false;
                }
                _lastConnectResult = ConnectionState.Connected;
                if (!response.StartsWith("220"))
                {
                    if (LogMessage != null)
                    {
                        LogMessage(this, new FTPMessageEventArgs("Failed to connect to server, connection rejected."));
                    }
                    Disconnect();
                    return false;
                }
                SendLine("USER " + username);
                response = ReadLine();
                if (!response.StartsWith("331"))
                {
                    if (LogMessage != null)
                    {
                        LogMessage(this, new FTPMessageEventArgs("Failed to connect to server, unknown or invalid user."));
                    }
                    Disconnect();
                    return false;
                }
                SendLine("PASS " + password);
                response = ReadLine();
                if (!response.StartsWith("230"))
                {
                    if (LogMessage != null)
                    {
                        LogMessage(this, new FTPMessageEventArgs("Failed to connect to server, invalid password for user."));
                    }
                    Disconnect();
                    return false;
                }
                _isAuthenticated = true;
                _lastConnectResult = ConnectionState.Authenticated;
                GetCurrentDirectory();
            }
            catch (Exception ex)
            {
                if (LogMessage != null)
                {
                    LogMessage(this, new FTPMessageEventArgs("Failed to connect to server, exception occurred: " + ex.Message));
                }
                if ((client != null))
                    client.Close();
                client = null;
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            if (!IsConnected)
                return;
            if (IsAuthenticated)
            {
                SendLine("QUIT");
                ReadLine();
                // This gets the Goodbye statement into the LastLine property.
            }
            client.Close();
            client = null;
            _isAuthenticated = false;
            _currentDir = null;
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs("Connection to server '" + _remoteServer + "' closed."));
            }
        }

        private void SendLine(string data)
        {
            client.Send(Encoding.ASCII.GetBytes(data + "\r\n"));
            if (data.ToUpper().StartsWith("PASS"))
                data = "PASS ********";
            Trace.WriteLine("SEND > " + data);
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs("SEND > " + data));
            }
            _lastCommandSent = System.DateTime.Now;
        }

        private string ReadLine()
        {
            System.DateTime startStamp = System.DateTime.Now;
            StringBuilder response = new StringBuilder();
            StringBuilder multiLine = new StringBuilder();
            StringBuilder multiLineDebug = new StringBuilder();
            while (System.DateTime.Now < startStamp.AddSeconds(Timeout))
            {
                if (client.Available > 0)
                {
                    string inBuffer = "";
                    startStamp = System.DateTime.Now;
                    while (client.Available > 0 & inBuffer.IndexOf("\r\n") == -1)
                    {
                        byte[] buffer = new byte[1];
                        client.Receive(buffer, 1, SocketFlags.None);
                        inBuffer += Encoding.ASCII.GetString(buffer);
                    }
                    response.Append(inBuffer);
                }
                string message = response.ToString();
                if (message.IndexOf("\r\n") != -1)
                {
                    message = response.ToString(0, message.IndexOf("\r\n"));
                    // A reply with a dash after the code indicates a multi-line message. We continue to receive until we see a full line
                    // with the original code listed again.
                    if (message.Substring(3, 1) == "-")
                    {
                        response = new StringBuilder();
                        multiLineDebug.Append(message + "\r\n");
                        message = message.Trim();
                        if (multiLine.Length == 0)
                        {
                            message = message.Remove(3, 1).Insert(3, " ");
                        }
                        else
                        {
                            message = " " + message.Remove(0, 4);
                        }
                        multiLine.Append(message);
                    }
                    else if (multiLine.Length > 0)
                    {
                        if (multiLine.ToString(0, 3) == message.Substring(0, 3))
                        {
                            multiLineDebug.Append(message);
                            multiLine.Append(message.Substring(3));
                            _lastLine = multiLine.ToString().Trim().Replace("\r\n", "");

                            string debugMsg = multiLineDebug.ToString();
                            if (debugMsg.Substring(debugMsg.Length - 3, 2) == "\r\n")
                                debugMsg = debugMsg.Remove(debugMsg.Length - 3, 2);
                            debugMsg = debugMsg.Replace("\r\n", "\r\n" + "RECV < ");
                            Trace.WriteLine("RECV < " + debugMsg);
                            if (LogMessage != null)
                            {
                                LogMessage(this, new FTPMessageEventArgs("RECV < " + debugMsg));
                            }
                            return LastLine;
                        }
                        else
                        {
                            response = new StringBuilder();
                            multiLineDebug.Append(message);
                            message = message.Trim();
                            multiLine.Append(message);
                        }
                    }
                    else if (message == "\r\n")
                    {
                        // Parse out the line if it came back as just a blank line and we're not in multi-line mode.
                        response = new StringBuilder();
                    }
                    else
                    {
                        _lastLine = message.Replace("\r\n", "").Trim();
                        message = message.Replace("\r\n", "");
                        Trace.WriteLine("RECV < " + message);
                        if (LogMessage != null)
                        {
                            LogMessage(this, new FTPMessageEventArgs("RECV < " + message));
                        }
                        return LastLine;
                    }
                }
                Thread.Sleep(5);
            }

            // Timeout
            return null;
        }

        private void GetCurrentDirectory()
        {
            if (!IsConnected)
                return;
            SendLine("PWD");
            string response = ReadLine();
            if (response == null || !response.StartsWith("257"))
            {
                _currentDir = "Unknown";
            }
            else
            {
                response = response.Substring(response.IndexOf("\"") + 1);
                _currentDir = response.Substring(0, response.IndexOf("\""));
            }
        }

        public bool ChangeDirectory(string directory)
        {
            if (!IsConnected)
                return false;
            SendLine("CWD " + directory);
            string response = ReadLine();
            if (response.StartsWith("250"))
            {
                if (directory.StartsWith("/"))
                {
                    _currentDir = directory;
                }
                else
                {
                    _currentDir += "/" + directory;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public FTPFileInfo[] GetDirectoryList(string directory = "")
        {
            if (!IsConnected)
                return null;
            if (directory.Length > 0)
            {
                if (!ChangeDirectory(directory))
                    return null;
            }
            FTPTransferState dataSocket = PrepareDataSocket();

            SendLine("LIST");
            string response = ReadLine();
            if (response == null || (!response.StartsWith("150") & !response.StartsWith("125")))
                return null;
            dataSocket.Open();
            dataSocket.BeginReceive();

            // Wait for the transfer to complete.
            while (!dataSocket.IsCompleted & System.DateTime.Now < dataSocket.LastReceived.AddSeconds(Timeout))
            {
                Thread.Sleep(500);
            }
            dataSocket.Close();
            Trace.WriteLine("DATA < " + dataSocket.Position + " Bytes Received");
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs(dataSocket.Message.ToString(), true));
            }
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs("DATA < " + dataSocket.Message.Length + " Bytes Received"));
            }

            // Get the next response from the remote server, then continue if it's a 226 or 250 message.
            response = ReadLine();
            if (response == null || ((!response.StartsWith("226") & !response.StartsWith("250")) | !dataSocket.IsCompleted))
                return null;

            string list = dataSocket.Message.ToString();
            ArrayList fileList = new ArrayList();
            if (list.Trim().Length == 0)
                return (FTPFileInfo[])fileList.ToArray(typeof(FTPFileInfo));

            Match files = ParseDirectoryList(list);
            if (files == null)
                throw new ArgumentException("The directory listing received from the server came in an unexpected format! " + "Please report this to ISI Support. Returned data: " + list);
            if (dataSocket.Message.Length > 0)
            {
                while ((!object.ReferenceEquals(files, Match.Empty)))
                {
                    FTPFileInfo file = default(FTPFileInfo);
                    file.Name = files.Groups["name"].Value;
                    file.Name = Unescape(file.Name);
                    if (file.Name.Length > 0)
                    {
                        if (files.Groups["dir"].Value == "d" | files.Groups["dir"].Value == "<DIR>")
                        {
                            file.IsDirectory = true;
                            file.Length = 0;
                        }
                        else if (files.Groups["dir"].Value == "l")
                        {
                            file.IsDirectory = true;
                            file.Length = 0;
                            file.Name = file.Name.Substring(0, file.Name.IndexOf("->") - 1);
                        }
                        else
                        {
                            file.IsDirectory = false;
                            file.Length = Convert.ToInt64(files.Groups["size"].Value);
                        }
                        if ((files.Groups["timestamp"] != null) && files.Groups["timestamp"].Value.Length > 0)
                        {
                            string stamp = files.Groups["timestamp"].Value.Replace("  ", " ");
                            if (Regex.Match(stamp, "[A-Za-z]{3}\\s[0-9]{1,2}\\s[0-9]{2}:[0-9]{2}").Success)
                                file.TimeStamp = System.DateTime.ParseExact(stamp, "MMM d HH:mm", null);
                            if (Regex.Match(stamp, "[A-Za-z]{3}\\s[0-9]{1,2}\\s[0-9]{4}").Success)
                                file.TimeStamp = System.DateTime.ParseExact(stamp, "MMM d yyyy", null);
                            if (Regex.Match(stamp, "[0-9]{2}-[0-9]{2}-[0-9]{2}\\s[0-9]{2}:[0-9]{2}[A-Za-z]{2}").Success)
                                file.TimeStamp = System.DateTime.ParseExact(stamp, "MM-dd-yy hh:mmtt", null);
                        }
                        fileList.Add(file);
                    }

                    files = files.NextMatch();
                }
            }
            return (FTPFileInfo[])fileList.ToArray(typeof(FTPFileInfo));
        }

        private Match ParseDirectoryList(string list)
        {
            // Check for normal FTP directory styles.
            Match m = Regex.Match(list, "(?<timestamp>\\d{2}-\\d{2}-\\d{2}\\s\\s\\d{2}:\\d{2}[AP]M)\\s+(?<size>\\d+)?(?<dir>\\<DIR\\>)?\\s+(?<name>[^\\r\\n]+)", RegexOptions.IgnoreCase);
            if (m.Success)
                return m;
            m = Regex.Match(list, "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:{0,1}\\d{2})\\s+(?<name>[^\\r\\n]+)", RegexOptions.IgnoreCase);
            if (m.Success)
                return m;
            m = Regex.Match(list, "(?<dir>[\\-dl])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:{0,1}\\d{2})\\s+(?<name>[^\\r\\n]+)", RegexOptions.IgnoreCase);
            if (m.Success)
                return m;
            m = Regex.Match(list, "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>[^\\r\\n]+)", RegexOptions.IgnoreCase);
            if (m.Success)
                return m;

            // Check for the buffer box file list style, stripping the first line as it's a row header.
            m = Regex.Match(list, "(?!Filename[\\s]+Records)^[\\s]*(?<name>[^\\s]+)[\\s]+(?<size>[^\\r\\n\\s]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (m.Success)
                return m;
            return null;
        }

        private string Unescape(string s)
        {
            int index = s.IndexOf("\\");
            while (index != -1)
            {
                string remainder = "";
                if (index + 1 < s.Length - 1)
                    remainder = s.Substring(index + 2);
                string c = "";
                switch (s.Substring(index + 1, 1))
                {
                    case "s":
                        c = " ";
                        break;
                    case "t":
                        c = "\t";
                        break;
                    case "n":
                        c = "\n";
                        break;
                    case "r":
                        c = "\r";
                        break;
                    default:
                        c = s.Substring(index + 1, 1);
                        break;
                }
                s = s.Substring(0, index) + c + remainder;
                index = s.IndexOf("\\", index + 1);
            }
            return s;
        }

        public bool DownloadFile(string remoteFilename, string localFilename)
        {
            if (!IsConnected)
                return false;
            // Make sure the localFilename has a period in its filename. If we try to write to an extensionless filename, that's required.
            if (localFilename.IndexOf(".", localFilename.LastIndexOf("\\")) == -1)
            {
                localFilename += ".";
            }

            FTPTransferState dataSocket = PrepareDataSocket();
            if (TransferMode == TransferModes.ASCII)
            {
                SendLine("TYPE A");
            }
            else
            {
                SendLine("TYPE I");
            }
            string response = ReadLine();

            string sourceFilename = Path.GetTempFileName();
            dataSocket.LocalFile = new FileStream(sourceFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

            SendLine("RETR " + remoteFilename);
            response = ReadLine();
            if (response == null || (!response.StartsWith("150") & !response.StartsWith("125")))
                return false;
            dataSocket.Open();
            dataSocket.BeginReceive();

            // Wait for the transfer to complete.
            while (!dataSocket.IsCompleted & System.DateTime.Now < dataSocket.LastReceived.AddSeconds(Timeout))
            {
                Thread.Sleep(500);
            }
            dataSocket.Close();
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs("DATA < " + dataSocket.Position + " Bytes Received"));
            }

            // Get the next response from the remote server, then continue if it's a 226 or 250 message.
            response = ReadLine();
            if (response == null || ((!response.StartsWith("226") & !response.StartsWith("250")) | !dataSocket.IsCompleted))
                return false;
            if (File.Exists(localFilename))
            {
                if (AllowOverwriteLocal)
                {
                    File.Delete(localFilename);
                }
                else
                {
                    int suffix = 2;
                    string localTemp = localFilename.Insert(localFilename.LastIndexOf("."), "(" + suffix + ")");
                    while (File.Exists(localTemp))
                    {
                        suffix += 1;
                        localTemp = localFilename.Insert(localFilename.LastIndexOf("."), "(" + suffix + ")");
                    }
                    localFilename = localTemp;
                }
            }
            File.Move(sourceFilename, localFilename);
            return true;
        }

        public bool UploadFile(string remoteFilename, string localFilename)
        {
            if (!IsConnected)
                return false;
            FTPTransferState dataSocket = PrepareDataSocket();
            if (TransferMode == TransferModes.ASCII)
            {
                SendLine("TYPE A");
            }
            else
            {
                SendLine("TYPE I");
            }
            string response = ReadLine();

            dataSocket.LocalFile = new FileStream(localFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            SendLine("STOR " + remoteFilename);
            response = ReadLine();
            if (response == null || (!response.StartsWith("150") & !response.StartsWith("125")))
                return false;
            dataSocket.Open();
            dataSocket.BeginSend();

            // Wait for the transfer to complete.
            while (!dataSocket.IsCompleted & System.DateTime.Now < dataSocket.LastReceived.AddSeconds(Timeout))
            {
                Thread.Sleep(500);
            }
            dataSocket.Close();
            if (LogMessage != null)
            {
                LogMessage(this, new FTPMessageEventArgs("DATA > " + dataSocket.Position + " Bytes Sent"));
            }

            // Parse the final respnose from the server, or report an error if it happened.
            response = ReadLine();
            if (response == null || ((!response.StartsWith("226") & !response.StartsWith("250")) | !dataSocket.IsCompleted))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteFile(string filename)
        {
            if (!IsConnected)
                return false;
            SendLine("DELE " + filename);
            string response = ReadLine();
            return (response != null) && response.StartsWith("250");
        }

        public bool RenameFile(string oldFilename, string newFilename)
        {
            if (!IsConnected)
                return false;
            SendLine("RNFR " + oldFilename);
            string response = ReadLine();
            if (response == null || !response.StartsWith("350"))
                return false;
            SendLine("RNTO " + newFilename);
            response = ReadLine();
            return (response != null) && response.StartsWith("250");
        }

        private FTPTransferState PrepareDataSocket()
        {
            FTPTransferState dataSocket = null;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (PassiveMode)
            {
                SendLine("PASV");
                string response = ReadLine();
                if (response == null || !response.StartsWith("227"))
                    return null;
                Match m = Regex.Match(response, "[0-9]{1,3},[0-9]{1,3},[0-9]{1,3},[0-9]{1,3},[0-9]{1,3},[0-9]{1,3}");
                string ipData = m.Groups[0].Value;
                int last = ipData.LastIndexOf(",");
                int split = last - (last - ipData.LastIndexOf(",", last - 1));
                string portData = ipData.Substring(split + 1);
                int remotePort = Convert.ToInt32(portData.Substring(0, portData.IndexOf(",")).PadLeft(2, "0"[0]), 16) + 
                    Convert.ToInt32(portData.Substring(portData.IndexOf(",") + 1).PadLeft(2, "0"[0]), 16);
                string remoteIP = ipData.Substring(0, split).Replace(",", ".");
                IPEndPoint dataEP = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
                dataSocket = new FTPTransferState(dataEP, true);
            }
            else
            {
                IPEndPoint localEP = null;
                if (MinPort == IPEndPoint.MinPort & MaxPort == IPEndPoint.MaxPort)
                {
                    localEP = new IPEndPoint(IPAddress.Any, 0);
                }
                else
                {
                    Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // Pick random port numbers in the range and attempt to bind.
                    int tries = MaxPort - MinPort + 1;
                    while (tries > 0)
                    {
                        try
                        {
                            Random r = new Random();
                            localEP = new IPEndPoint(IPAddress.Any, r.Next(MinPort, MaxPort));
                            tempSocket.Bind(localEP);
                            tempSocket.Close();
                            break; // TODO: might not be correct. Was : Exit While
                        }
                        catch
                        {
                            tries -= 1;
                            if (tries == 0)
                                throw new IOException("Unable to find an available port between " + MinPort + " and " + MaxPort + "!");
                        }
                    }
                }
                dataSocket = new FTPTransferState(localEP, false);
                localEP = dataSocket.DataEP;
                IPAddress address = ((IPEndPoint)client.LocalEndPoint).Address;
                string ip = address.ToString().Replace(".", ",");
                string portString = Convert.ToString(localEP.Port, 16).PadLeft(4, "0"[0]);
                string portCommand = "PORT " + ip + "," + Convert.ToInt32(portString.Substring(0, 2), 16) + "," + Convert.ToInt32(portString.Substring(2, 2), 16);
                SendLine(portCommand);
                string response = ReadLine();
                if (response == null || !response.StartsWith("200"))
                {
                    dataSocket.Close();
                    return null;
                }
            }
            return dataSocket;
        }

        private bool BindLocalEndPoint(Socket localSocket)
        {
            // If our min and max values are the defaults, bind to any available port.
            if (MinPort == IPEndPoint.MinPort & MaxPort == IPEndPoint.MaxPort)
            {
                localSocket.Bind(new IPEndPoint(IPAddress.Any, 0));
                return true;
            }

            // Pick random port numbers in the range and attempt to bind.
            int tries = MaxPort - MinPort + 1;
            while (tries > 0)
            {
                try
                {
                    Random r = new Random();
                    IPEndPoint localEP = new IPEndPoint(IPAddress.Any, r.Next(MinPort, MaxPort));
                    localSocket.Bind(localEP);
                    return true;
                }
                catch 
                {
                    tries -= 1;
                    if (tries == 0)
                        throw new IOException("Unable to find an available port between " + MinPort + " and " + MaxPort + "!");
                }
            }
            return false;
        }

        private class FTPTransferState
        {
            public byte[] Buffer = new byte[BufferSize + 1];
            public const int BufferSize = 512;
            public long Position = 0;
            public StringBuilder Message = new StringBuilder();
            public Socket DataSocket;
            public IPEndPoint DataEP;
            public IPEndPoint RemoteEP;
            public FileStream LocalFile;
            public bool IsPassive;
            public bool IsCompleted = false;
            public System.DateTime LastReceived = System.DateTime.Now;
            public bool ErrorOccured = false;
            private Socket ListenSocket;
            private bool Receiving = true;

            private Thread WorkThread;
            public FTPTransferState(IPEndPoint endPoint, bool passive)
            {
                IsPassive = passive;
                if (IsPassive)
                {
                    DataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    DataSocket.Connect(endPoint);
                    RemoteEP = endPoint;
                    DataEP = (IPEndPoint)DataSocket.LocalEndPoint;
                }
                else
                {
                    ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ListenSocket.Bind(endPoint);
                    ListenSocket.Listen(1);
                    DataEP = (IPEndPoint)ListenSocket.LocalEndPoint;
                }
            }

            public bool Open()
            {
                if (!IsPassive)
                {
                    DataSocket = ListenSocket.Accept();
                    RemoteEP = (IPEndPoint)DataSocket.RemoteEndPoint;
                    ListenSocket.Close();
                }
                return (DataSocket != null);
            }

            public void BeginReceive()
            {
                WorkThread = new Thread(new ThreadStart(WorkerThread));
                WorkThread.IsBackground = true;
                WorkThread.Name = "FTP Client Data Socket";
                WorkThread.Start();
            }

            public void BeginSend()
            {
                Receiving = false;
                BeginReceive();
            }

            private void WorkerThread()
            {
                if (Receiving)
                {
                    DataSocket.BeginReceive(Buffer, 0, FTPTransferState.BufferSize, SocketFlags.None, new AsyncCallback(Data_Receive), this);
                }
                else
                {
                    int bytesRead = LocalFile.Read(Buffer, 0, FTPTransferState.BufferSize);
                    DataSocket.BeginSend(Buffer, 0, bytesRead, SocketFlags.None, new AsyncCallback(Data_Send), this);
                }

                while (!IsCompleted)
                {
                    Thread.Sleep(10);
                }
            }

            private void Data_Receive(IAsyncResult result)
            {
                FTPTransferState state = (FTPTransferState)result.AsyncState;
                try
                {
                    int bytesReceived = state.DataSocket.EndReceive(result);
                    if (bytesReceived == 0)
                    {
                        state.DataSocket.Close();
                        if ((state.LocalFile != null))
                        {
                            state.LocalFile.Flush();
                            state.LocalFile.Close();
                        }
                        state.IsCompleted = true;
                        return;
                    }
                    else
                    {
                        state.Position += bytesReceived;
                    }
                    state.LastReceived = System.DateTime.Now;
                    if (state.LocalFile == null)
                    {
                        state.Message.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesReceived));
                        Trace.Write(Encoding.ASCII.GetString(state.Buffer, 0, bytesReceived));
                    }
                    else
                    {
                        state.LocalFile.Write(state.Buffer, 0, bytesReceived);
                    }
                    if (state.DataSocket.Connected)
                    {
                        state.DataSocket.BeginReceive(state.Buffer, 0, FTPTransferState.BufferSize, SocketFlags.None, new AsyncCallback(Data_Receive), state);
                    }
                    else
                    {
                        state.DataSocket.Close();
                        if ((state.LocalFile != null))
                        {
                            state.LocalFile.Flush();
                            state.LocalFile.Close();
                        }
                        state.IsCompleted = true;
                    }
                }
                catch
                {
                    if ((state.DataSocket != null))
                        state.DataSocket.Close();
                    if ((state.LocalFile != null))
                    {
                        state.LocalFile.Flush();
                        state.LocalFile.Close();
                    }
                    state.ErrorOccured = true;
                    state.IsCompleted = true;
                }
            }

            private void Data_Send(IAsyncResult result)
            {
                FTPTransferState state = (FTPTransferState)result.AsyncState;
                try
                {
                    if (state.DataSocket.Connected)
                    {
                        int bytesSent = state.DataSocket.EndSend(result);
                        state.Position += bytesSent;
                    }
                    else
                    {
                        state.DataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        state.DataSocket.Connect(state.RemoteEP);
                    }
                    state.LastReceived = System.DateTime.Now;
                    if (state.Position >= state.LocalFile.Length)
                    {
                        state.DataSocket.Close();
                        state.IsCompleted = true;
                        return;
                    }
                    if (state.DataSocket.Connected)
                    {
                        int bytesRead = state.LocalFile.Read(state.Buffer, 0, FTPTransferState.BufferSize);
                        state.DataSocket.BeginSend(state.Buffer, 0, bytesRead, SocketFlags.None, new AsyncCallback(Data_Send), state);
                    }
                    else
                    {
                        state.DataSocket.Close();
                        state.LocalFile.Close();
                        state.ErrorOccured = true;
                        state.IsCompleted = true;
                    }
                }
                catch
                {
                    if ((state.DataSocket != null))
                        state.DataSocket.Close();
                    if ((state.LocalFile != null))
                        state.LocalFile.Close();
                    state.ErrorOccured = true;
                    state.IsCompleted = true;
                }
            }

            public void Close()
            {
                if ((DataSocket != null))
                {
                    DataSocket.Close();
                    DataSocket = null;
                }
                if ((LocalFile != null))
                {
                    if (Receiving & (LocalFile.CanWrite | LocalFile.CanRead))
                        LocalFile.Flush();
                    LocalFile.Close();
                }
            }
        }

        public void Dispose()
        {
            if (IsConnected)
                Disconnect();
        }

    }

    public enum TransferModes
    {
        ASCII = 1,
        Binary = 2
    }

    public struct FTPFileInfo
    {
        public string Name;
        public long Length;
        public bool IsDirectory;
        public System.DateTime TimeStamp;
    }

    public enum ConnectionState
    {
        Disconnected = 0,
        Connected = 1,
        Authenticated = 2
    }

    public delegate void FTPMessageHandler(object sender, FTPMessageEventArgs e);

    public class FTPMessageEventArgs : EventArgs
    {
        private string _message;

        private bool _isData;
        public string Message
        {
            get { return _message; }
        }

        public bool IsData
        {
            get { return _isData; }
        }

        public FTPMessageEventArgs(string message, bool isData = false)
        {
            _message = message;
            _isData = isData;
        }
    }

}
