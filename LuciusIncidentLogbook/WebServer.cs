using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using KitchenGeeks.Properties;

namespace KitchenGeeks
{
    /// <summary>
    ///     Contains the current status of the web server when it changes, e.g. errors occur.
    /// </summary>
    public class WebStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     If true, the server is currently active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     The message being passed from the server.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     If true, an error occurred which raised this event.
        /// </summary>
        public bool Error { get; set; }
    }

    /// <summary>
    ///     Contains the results from a Match as sent in from a mobile device.
    /// </summary>
    public class MatchUpdateFromWebEventArgs : EventArgs
    {
        /// <summary>
        ///     The name of the Tournament being updated.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        ///     The ID for the first player in the match.
        /// </summary>
        public string Player1ID { get; set; }

        /// <summary>
        ///     The Victory Points for the first player in the match.
        /// </summary>
        public int Player1Vp { get; set; }

        /// <summary>
        ///     If true, the first player Forfeited.
        /// </summary>
        public bool Player1Forfeit { get; set; }

        /// <summary>
        ///     The ID for the second player in the match.
        /// </summary>
        public string Player2ID { get; set; }

        /// <summary>
        ///     The Victory Points for the second player in the match.
        /// </summary>
        public int Player2Vp { get; set; }

        /// <summary>
        ///     If true, the second player Forfeited.
        /// </summary>
        public bool Player2Forfeit { get; set; }
    }

    public delegate void WebStatusChangedHandler(object sender, WebStatusChangedEventArgs e);

    public delegate void MatchUpdateFromWebHandler(object sender, MatchUpdateFromWebEventArgs e);

    /// <summary>
    ///     A lightweight web server designed for remote management of active tournaments via mobile devices.
    /// </summary>
    public class WebServer : IDisposable
    {
        private readonly Dictionary<string, DateTime> _authenticated = new Dictionary<string, DateTime>();
        private Thread Worker { get; set; }
        private HttpListener Listener { get; set; }
        private bool ShutdownServer { get; set; }
        private bool Disposed { get; set; }

        /// <summary>
        ///     Declares a new instance of the WebServer class.
        /// </summary>
        public WebServer()
        {
            Listener = new HttpListener();
            Active = false;
            try
            {
                Worker = new Thread(Listen)
                    {
                        IsBackground = false,
                        Name = "Logbook Web Server Thread",
                        Priority = ThreadPriority.BelowNormal
                    };
                Worker.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The web server had an error occur while initializing: " + ex.Message,
                                "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     If true, the server is actively listening for connections.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     Releases the resources used by the WebServer object, including ordering the server to shutdown
        ///     if necessary.
        /// </summary>
        public void Dispose()
        {
            if (Listener != null)
            {
                ShutdownServer = true;
                Worker.Join(5000);
                Listener.Close();
                Listener = null;
            }
            Disposed = true;
        }

        /// <summary>
        ///     Raised when a match update is received from a web client.
        /// </summary>
        public event MatchUpdateFromWebHandler MatchUpdateFromWeb;

        /// <summary>
        ///     Raised when the status of the server has changed.
        /// </summary>
        public event WebStatusChangedHandler WebStatusChanged;

        private void Listen()
        {
            while (!ShutdownServer)
            {
                try
                {
                    // Check for anybody whose authentication has expired.
                    Dictionary<string, DateTime>.Enumerator en = _authenticated.GetEnumerator();
                    while (en.MoveNext())
                    {
                        if (en.Current.Value < DateTime.Now)
                        {
                            _authenticated.Remove(en.Current.Key);
                            en = _authenticated.GetEnumerator();
                        }
                    }
                    if (Active)
                    {
                        Listener.Prefixes.Clear();
                        Listener.Prefixes.Add("http://*:" + Config.Settings.WebServerPort + "/");
                        Listener.Start();
                        while (!ShutdownServer && Active)
                        {
                            IAsyncResult result = Listener.BeginGetContext(ListenerCallback, Listener);
                            while (!ShutdownServer && !result.IsCompleted)
                            {
                                Thread.Sleep(500);
                                if (!Active || ShutdownServer)
                                {
                                    result.AsyncWaitHandle.Close();
                                    break;
                                }
                            }
                        }
                        if (!Disposed && Listener.IsListening) Listener.Stop();
                    }
                    else
                        Thread.Sleep(500);
                }
                catch (ObjectDisposedException)
                {
                    // Just restart the listener.
                    Listener = new HttpListener();
                }
                catch (HttpListenerException ex)
                {
                    var message = "The web server had an error occur: " + ex.Message;
                    if (ex.ErrorCode == 5)
                        message = "Unable to open the web server's port. You may need administrative rights to do so.";
                    if (ex.ErrorCode == 183)
                        message = "Port " + Config.Settings.WebServerPort +
                                  " is in use. Please choose another port before starting the server again, or ensure " + 
                                  "the port is available.";

                    if (WebStatusChanged != null)
                    {
                        var e = new WebStatusChangedEventArgs
                        {
                            Active = false,
                            Error = true,
                            Message = message
                        };
                        WebStatusChanged(this, e);
                    }
                    else
                        MessageBox.Show(message, "Unexpected Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!Disposed && Listener.IsListening) Listener.Stop();
                    Active = false;
                }
                catch (Exception ex)
                {
                    if (WebStatusChanged != null)
                    {
                        var e = new WebStatusChangedEventArgs
                            {
                                Active = false,
                                Error = true,
                                Message = ex.Message
                            };
                        WebStatusChanged(this, e);
                    }
                    else
                        MessageBox.Show("The web server had an error occur: " + ex.Message, "Unexpected Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!Disposed && Listener.IsListening) Listener.Stop();
                    Active = false;
                }
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            // Short circuits the callback if we're disposing. Saves us a lot of hassle with error handling.
            if (Disposed) return;

            try
            {
                var myListener = (HttpListener) result.AsyncState;
                HttpListenerContext context = myListener.EndGetContext(result);
                HttpListenerRequest request = context.Request;
                string content;
                using (var sr = new StreamReader(request.InputStream))
                    content = sr.ReadToEnd();
                string originalUrl = request.RawUrl;
                string url = originalUrl;
                if (url.Contains("?"))
                {
                    content = url.Substring(url.IndexOf("?") + 1);
                    url = url.Substring(0, url.IndexOf("?"));
                }
                if (request.RemoteEndPoint == null) return;
                var remoteIP = request.RemoteEndPoint.Address.ToString();
                var isAuthenticated = Config.Settings.WebServerPassword.Length > 0 &&
                                       _authenticated.ContainsKey(remoteIP);

                var parameters = new Dictionary<string, string>();
                if (content.Contains("="))
                {
                    string[] values = content.Split(new[] {"&"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in values)
                    {
                        if (value.Contains("="))
                        {
                            string key = value.Substring(0, value.IndexOf("="));
                            string innerValue = "";
                            if (key.Length + 1 != value.Length)
                                innerValue = value.Substring(value.IndexOf("=") + 1);
                            if (!parameters.ContainsKey(key)) parameters.Add(key, innerValue);
                        }
                        else if (!parameters.ContainsKey(value)) parameters.Add(value, "");
                    }
                }
                HttpListenerResponse response = context.Response;

                switch (url.ToLower())
                {
                    case "/Style.css":
                        {
                            string html = Resources.Style;
                            byte[] buffer = Encoding.UTF8.GetBytes(html);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;
                    case "/login.htm":
                        {
                            string html;
                            if (isAuthenticated)
                            {
                                html = Resources.Authenticated;
                                html = html.Replace("<!-- url -->", parameters.ContainsKey("target") ? 
                                    HttpUtility.UrlDecode(parameters["target"]) : "/");
                            }
                            else if (parameters.ContainsKey("target"))
                            {
                                html = GetAuthenticationPage(parameters["target"]);
                            }
                            else if (parameters.ContainsKey("Password") &&
                                     parameters["Password"] == Config.Settings.WebServerPassword)
                            {
                                _authenticated.Add(remoteIP, DateTime.Now.AddDays(1));
                                html = Resources.Authenticated;
                                html = html.Replace("<!-- url -->", parameters.ContainsKey("Target") ? 
                                    HttpUtility.UrlDecode(parameters["Target"]) : "/");
                            }
                            else
                            {
                                html = Resources.RetryAuthenticate;
                                html = html.Replace("<!-- url -->", parameters.ContainsKey("Target") ? 
                                    parameters["Target"] : "/");
                            }
                            byte[] buffer = Encoding.UTF8.GetBytes(html);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;

                    case "/favicon.ico":
                        {
                            response.Headers.Add("Content-Type", "image/x-icon");
                            Icon favIcon = Resources.LuciusIcon;
                            var ms = new MemoryStream();
                            favIcon.Save(ms);
                            response.ContentLength64 = ms.Length;
                            ms.Seek(0, SeekOrigin.Begin);
                            var buffer = new byte[ms.Length];
                            ms.Read(buffer, 0, buffer.Length);
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;

                    case "/apple-touch-icon.png":
                    case "/apple-touch-icon-precomposed.png":
                        {
                            response.Headers.Add("Content-Type", "image/png");
                            Image appleIcon = Resources.AppleTouchIcon;
                            var ms = new MemoryStream();
                            appleIcon.Save(ms, ImageFormat.Png);
                            response.ContentLength64 = ms.Length;
                            ms.Seek(0, SeekOrigin.Begin);
                            var buffer = new byte[ms.Length];
                            ms.Read(buffer, 0, buffer.Length);
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;

                    case "/tournament.htm":
                        {
                            string name = "";
                            if (parameters.ContainsKey("name")) name = HttpUtility.UrlDecode(parameters["name"]);
                            if (string.IsNullOrEmpty(name))
                            {
                                url = "/";
                                break;
                            }

                            name = name.Replace("%20", " ");
                            var tournament = Config.Settings.GetTournament(name);
                            if (tournament == null)
                            {
                                url = "/";
                                break;
                            }

                            var html = Resources.ChooseRound;
                            html = html.Replace("<!-- name -->", tournament.Name + " -- Round " +
                                                                 tournament.Rounds.Count + " of " +
                                                                 tournament.TotalRounds);
                            var message = isAuthenticated ? 
                                    "\t\tChoose the Round to manage from the list below:<br />\r\n" +
                                    "\t\t(Unscored rounds are listed first, followed by rounds with scores.)<br />" :
                                    "\t\tBelow is a list of the Matches this round and their end scores, if recorded.<br />";
                            html = html.Replace("<!-- message -->", message);

                            var roundsScored = "";
                            var roundsOpen = "";

                            var id = 0;
                            foreach (var match in tournament.Rounds[tournament.Rounds.Count - 1].Matches)
                            {
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                if (match.Players.Count == 2)
                                {
                                    var player2 = Config.Settings.GetPlayer(match.Players[1]);
                                    if (isAuthenticated)
                                    {
                                        var tag = "\t\t<a href=\"Round.htm?name=" +
                                                     HttpUtility.UrlEncode(tournament.Name) + "&id=" +
                                                     id + "\"><p class=\"roundbordered\"><br />Match #" +
                                                     (id + 1) +
                                                     "<br />" + player1.Name + " VS " + player2.Name + "<br />";
                                        if (match.Results.Count > 0)
                                            roundsScored += tag + "SCORED -> " +
                                                            match.Results[player1.ID].VictoryPoints +
                                                            " VPs VS " +
                                                            match.Results[player2.ID].VictoryPoints +
                                                            " VPs <- SCORED<br /><br /></p></a>\r\n";
                                        else
                                            roundsOpen += tag + "0 VPs VS 0 VPs<br /><br /></p></a>\r\n";
                                    }
                                    else
                                    {
                                        roundsScored += "\t\t<p class=\"round\"><br />Match #" + (id + 1) +
                                                        "<br />" + player1.Name + " VS " + player2.Name + "<br />";
                                        if (match.Results.Count > 0)
                                            roundsScored += "SCORED -> " +
                                                            match.Results[player1.ID].VictoryPoints +
                                                            " VPs VS " +
                                                            match.Results[player2.ID].VictoryPoints +
                                                            " VPs <- SCORED<br /></p>\r\n";
                                        else
                                            roundsScored += "0 VPs VS 0 VPs<br /></p>\r\n";
                                    }
                                }
                                else
                                    roundsScored += "\t\t<p class=\"round\"><br />" + player1.Name +
                                                    " -- Bye Round<br /><br /></p>\r\n";
                                id++;
                            }

                            if (isAuthenticated)
                            {
                                string scores = "<p class=\"round\">Rounds with Scores<br /></p><br />" + roundsScored;
                                if (roundsOpen.Length > 0)
                                    scores = "<p class=\"round\">Open Rounds<br /></p><br />" + roundsOpen +
                                             "<br /><br />\r\n" + scores;
                                html = html.Replace("<!-- entries -->", scores);
                            }
                            else
                            {
                                roundsScored += "\r\n\t\t<a href=\"/Login.htm?target=" + originalUrl +
                                                "\"><p class=\"name\"><br />Log In to Edit Rounds<br /><br /></p></a>\r\n";
                                html = html.Replace("<!-- entries -->", roundsScored);
                            }

                            byte[] buffer = Encoding.UTF8.GetBytes(html);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;

                    case "/round.htm":
                        {
                            string html;
                            if (!isAuthenticated)
                                html = GetAuthenticationPage(originalUrl);
                            else
                            {
                                var name = "";
                                if (parameters.ContainsKey("name")) name = HttpUtility.UrlDecode(parameters["name"]);
                                var id = -1;
                                if (parameters.ContainsKey("id")) int.TryParse(parameters["id"], out id);

                                if (string.IsNullOrEmpty(name) || id <= -1)
                                {
                                    url = "/";
                                    break;
                                }
                                var tournament = Config.Settings.GetTournament(name);
                                if (tournament == null)
                                {
                                    url = "/";
                                    break;
                                }
                                if (id >= tournament.Rounds[tournament.Rounds.Count - 1].Matches.Count)
                                {
                                    url = "/";
                                    break;
                                }

                                var match = tournament.Rounds[tournament.Rounds.Count - 1].Matches[id];
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                var player2 = Config.Settings.GetPlayer(match.Players[1]);

                                html = Resources.EditRound;
                                html = html.Replace("<!-- error -->", "");
                                html = html.Replace("<!-- player1 -->", player1.Name);
                                html = html.Replace("<!-- player1VP -->", match.Results.ContainsKey(player1.ID) ? 
                                    match.Results[player1.ID].VictoryPoints.ToString() : "0");
                                html = html.Replace("<!-- player2 -->", player2.Name);
                                html = html.Replace("<!-- player2VP -->", match.Results.ContainsKey(player2.ID) ? 
                                    match.Results[player2.ID].VictoryPoints.ToString() : "0");
                                html = html.Replace("<!-- name -->", HttpUtility.UrlEncode(tournament.Name));
                                html = html.Replace("<!-- player1ID -->", player1.ID);
                                html = html.Replace("<!-- player2ID -->", player2.ID);
                                html = html.Replace("<!-- ID -->", id.ToString());
                            }
                            var buffer = Encoding.UTF8.GetBytes(html);
                            response.ContentLength64 = buffer.Length;
                            response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                        break;

                    case "/submit.htm":
                        {
                            string mode = "";
                            if (parameters.ContainsKey("Mode")) mode = parameters["Mode"];
                            if (mode == "UpdateScores")
                            {
                                string html;
                                if (!isAuthenticated)
                                    html = GetAuthenticationPage(originalUrl);
                                else
                                {
                                    var name =  HttpUtility.UrlDecode(parameters["Name"]);
                                    name = name.Replace("+", " ");
                                    var player1ID = parameters["Player1ID"];
                                    var player2ID = parameters["Player2ID"];
                                    var player1Vp = parameters["Player1VP"];
                                    var player2Vp = parameters["Player2VP"];

                                    var e = new MatchUpdateFromWebEventArgs
                                        {
                                            TournamentName = name,
                                            Player1ID = player1ID,
                                            Player2ID = player2ID
                                        };

                                    if (player1Vp.ToUpper() != "F")
                                    {
                                        int vp;
                                        if (!int.TryParse(player1Vp, out vp))
                                        {
                                            url = "/";
                                            break;
                                        }
                                        e.Player1Vp = vp;
                                    }
                                    else
                                        e.Player1Forfeit = true;

                                    if (player2Vp.ToUpper() != "F")
                                    {
                                        int vp;
                                        if (!int.TryParse(player2Vp, out vp))
                                        {
                                            url = "/";
                                            break;
                                        }
                                        e.Player2Vp = vp;
                                    }
                                    else
                                        e.Player2Forfeit = true;

                                    if (MatchUpdateFromWeb != null)
                                        MatchUpdateFromWeb(this, e);

                                    html = Resources.Submitted;
                                    html = html.Replace("<!-- url -->", "Tournament.htm?name=" + parameters["Name"]);
                                    const string message = "Scores have been successfully submitted. Redirecting back to " +
                                                           "the tournament page in a few seconds...";
                                    html = html.Replace("<!-- message -->", message);
                                }
                                var buffer = Encoding.UTF8.GetBytes(html);
                                response.ContentLength64 = buffer.Length;
                                response.OutputStream.Write(buffer, 0, buffer.Length);
                            }
                            else
                                url = "/";
                        }
                        break;

                    default:
                        url = "/";
                        break;
                }

                // We may fall through to here if we got a bad URL.
                if (url.ToLower() == "/")
                {
                    var html = Resources.ChooseTournament;
                    var tournamentHTML = "";
                    foreach (var tournament in Config.Settings.Tournaments)
                        if (!tournament.Completed && tournament.Rounds.Count > 0)
                        {
                            var currentRound = tournament.Rounds.Count;
                            if (tournament.Rounds[currentRound - 1].Completed)
                            {
                                tournamentHTML += "\t\t<a href=\"Tournament.htm?name=" +
                                                  HttpUtility.UrlEncode(tournament.Name) + "\">" + "<p><br />" +
                                                  tournament.Name + " - Round " + currentRound +
                                                  " Completed<br /><br /></p>\r\n";
                            }
                            else
                            {
                                tournamentHTML += "\t\t<a href=\"Tournament.htm?name=" +
                                                  HttpUtility.UrlEncode(tournament.Name) + "\">" + "<p><br />" +
                                                  tournament.Name + " - Round " + currentRound +
                                                  " Active<br /><br /></p>\r\n";
                            }
                        }

                    if (tournamentHTML.Length == 0)
                        tournamentHTML = "\t\tThere are no active tournaments to record scores for!<br />";
                    html = html.Replace("<!-- entries -->", tournamentHTML);

                    var buffer = Encoding.UTF8.GetBytes(html);
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }

                response.OutputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                // Code 995 occurs when the HttpListener is stopped. We can ignore this error.
                if (ex is HttpListenerException && ((HttpListenerException) ex).ErrorCode == 995)
                    return;

                if (WebStatusChanged == null) return;
                var e = new WebStatusChangedEventArgs
                    {
                        Active = true,
                        Error = true,
                        Message = ex.Message
                    };
                WebStatusChanged(this, e);
            }
        }

        private static string GetAuthenticationPage(string targetUrl)
        {
            var html = Resources.Authenticate;
            html = html.Replace("<!-- target -->", targetUrl);
            return html;
        }

        /// <summary>
        ///     Tells the server to shut down entirely.
        /// </summary>
        public void Shutdown()
        {
            ShutdownServer = true;
        }
    }
}