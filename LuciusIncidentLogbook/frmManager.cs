using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class FrmManager : Form
    {
        private frmLog LogWindow { get; set; }
        private frmDownload StatusForm { get; set; }
        private bool _downloaded;
        private WebClient _web;

        public FrmManager()
        {
            InitializeComponent();
            Text += " v" + Program.VersionString;
            Config.Server.MatchUpdateFromWeb += Server_MatchUpdateFromWeb;
            Config.Server.WebStatusChanged += Server_WebStatusChanged;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private string LocalIPAddress()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return null;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            var firstOrDefault = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return firstOrDefault != null ? firstOrDefault.ToString() : "127.0.0.1";
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnManagePlayers_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                if (child is frmPlayers)
                {
                    child.WindowState = FormWindowState.Normal;
                    child.BringToFront();
                    child.Focus();
                    return;
                }
            }

            var ChildForm = new frmPlayers();
            ChildForm.MdiParent = this;
            ChildForm.Show();
        }

        private void btnManageTournaments_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                if (child is frmEventsList)
                {
                    child.WindowState = FormWindowState.Normal;
                    child.BringToFront();
                    child.Focus();
                    return;
                }
            }

            var ChildForm = new frmEventsList();
            ChildForm.MdiParent = this;
            ChildForm.Show();
        }

        private void btnCreateTimer_Click(object sender, EventArgs e)
        {
            using (var dialog = new frmCreateTimer())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var timer = new frmTimer(dialog.TimerName, dialog.TimerDuration);
                    if (dialog.DockTimer) timer.MdiParent = this;
                    timer.Show();
                }
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _web = new WebClient();
            _web.Proxy = WebRequest.GetSystemWebProxy();
            _web.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            _web.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            StatusForm = new frmDownload();
            StatusForm.Show();
            StatusForm.SetMessage("Checking version number...");
            Application.DoEvents();

            string webVersionText = "";
            while (true)
            {
                try
                {
                    webVersionText =
                        _web.DownloadString("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/.version");
                    break;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Unable to check for the latest version: " + ex.Message,
                                        "Error During Update Check",
                                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        StatusForm.Close();
                        StatusForm = null;
                        return;
                    }
                }
            }

            var webVersion = new Version(webVersionText + ".0");
            if (LogWindow != null)
                LogWindow.WriteLog("[Updater] Local version = v" + Program.VersionString + ", Web version = v" +
                                   webVersionText);

            if (webVersion <= Program.Version)
            {
                StatusForm.Close();
                StatusForm = null;
                MessageBox.Show("You have the latest version!", "Up To Date", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("A newer version is available: v" + webVersionText + " Update now?", "Update Available",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                DialogResult.No)
            {
                StatusForm.Close();
                StatusForm = null;
                return;
            }

            StatusForm.SetMessage("Initializing...");
            _downloaded = false;
            _web.DownloadProgressChanged += web_DownloadProgressChanged;
            _web.DownloadFileCompleted += web_DownloadFileCompleted;
            _web.DownloadFileAsync(
                new Uri("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/LuciusIncidentLogbook.exe"),
                Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LuciusIncidentLogbook.Update.exe"));
            while (!_downloaded)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }

            StatusForm.SetProgressValue(100);
            StatusForm.SetProgressMax(100);
            StatusForm.SetMessage("Download Completed!");
            Application.DoEvents();
            Thread.Sleep(1000);
            StatusForm.Close();
            StatusForm = null;
            _web = null;

            Process.Start(
                Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LuciusIncidentLogbook.Update.exe"),
                "/update " + Process.GetCurrentProcess().Id.ToString() + " " + Program.VersionString);
            Application.Exit();
        }

        private void web_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _downloaded = true;
        }

        private void web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int current = Convert.ToInt32(e.BytesReceived);
            int total = Convert.ToInt32(e.TotalBytesToReceive);
            StatusForm.SetMessage(current.ToString() + " / " + total.ToString() + " Bytes");
            StatusForm.SetProgressMax(total);
            StatusForm.SetProgressValue(current);
            Application.DoEvents();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new frmAbout();
            about.ShowDialog();
            about.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FrmSettings();
            dialog.ShowDialog();
            dialog.Close();
        }

        private void tipTheDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new frmDonate();
            dialog.ShowDialog();
            dialog.Close();
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new frmSendEmail("Feedback Form", "This form will send an email to the developer " +
                                                           "directly. Please list as much details as you can when filling out the form. (Be sure to have " +
                                                           "configured SMTP settings first.)",
                                          new MailAddress("chris@kitchengeeks.net", "Chris Dundon"),
                                          "Suggestions for the Logbook", "Logbook Feedback");
            dialog.ShowDialog();
            dialog.Close();
        }

        private void viewChangeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new frmUpdated(null);
            dialog.ShowDialog();
            dialog.Close();
        }

        private void btnNewTournament_Click(object sender, EventArgs e)
        {
            var selectedType = EventType.Unknown;
            using (var typeDialog = new frmEventType())
            {
                if (typeDialog.ShowDialog() == DialogResult.Cancel) return;
                selectedType = typeDialog.Value;
            }

            if (selectedType == EventType.Tournament)
            {
                Tournament tournament = null;
                while (true)
                {
                    using (var dialog = new frmTournamentOptions(tournament))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            tournament = dialog.Tournament;

                            Tournament checkTournament = Config.Settings.GetTournament(tournament.Name);
                            if (checkTournament != null)
                                if (MessageBox.Show("A tournament with the name \"" + tournament.Name + "\" already " +
                                                    "exists. Tournament names must be unique.", "Validation Error",
                                                    MessageBoxButtons.RetryCancel,
                                                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) ==
                                    DialogResult.Cancel)
                                    return;
                                else
                                    continue;

                            Config.Settings.Tournaments.Add(tournament);
                            Config.Settings.SaveEvents();

                            foreach (Form form in MdiChildren)
                                if (form is frmEventsList)
                                {
                                    ((frmEventsList) form).FilterList();
                                    break;
                                }

                            return;
                        }
                        else
                            return;
                    }
                }
            }
            else
            {
            }
            League league = null;
            while (true)
            {
                using (var dialog = new frmLeagueOptions(league))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        league = dialog.League;
                        League checkLeague = Config.Settings.GetLeague(dialog.Name);
                        Tournament checkTournament = Config.Settings.GetTournament(dialog.Name);
                        if (checkTournament != null || checkLeague != null)
                            if (MessageBox.Show("An event with the name \"" + league.Name + "\" already " +
                                                "exists. Event names must be unique.", "Validation Error",
                                                MessageBoxButtons.RetryCancel,
                                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) ==
                                DialogResult.Cancel)
                                return;
                            else
                                continue;

                        Config.Settings.Leagues.Add(league);
                        Config.Settings.SaveEvents();
                        return;
                    }
                    else
                        return;
                }
            }
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            using (var dialog = new frmCreatePlayer())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var record = new PlayerRecord();
                    record.FirstName = dialog.FirstName;
                    record.LastName = dialog.LastName;
                    if (dialog.AdditionalInformation)
                    {
                        record.ForumName = dialog.ForumName;
                        record.Email = dialog.Email;
                        record.Hometown = dialog.Hometown;
                        record.Region = dialog.PlayerRegion;
                        record.Faction = dialog.Faction;
                    }

                    Config.Settings.Players.Add(record);
                    Config.Settings.SavePlayers();

                    foreach (Form form in MdiChildren)
                        if (form is frmPlayers)
                        {
                            ((frmPlayers) form).RefreshList();
                            break;
                        }
                }
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            if (Config.Settings.AutoEnableWebServer)
            {
                string IP = LocalIPAddress();
                if (Config.Server.Active)
                {
                    Config.Server.Active = false;
                    if (IP == null)
                        lblWebStatus.Text = "Offline -- No Network Connection";
                    else
                        lblWebStatus.Text = "Offline";
                }
                else
                {
                    if (IP == null)
                        lblWebStatus.Text = "Offline -- No Network Connection";
                    else
                    {
                        Config.Server.Active = true;
                        lblWebStatus.Text = "Listening at " + IP + " on Port " +
                                            Config.Settings.WebServerPort.ToString();
                    }
                }
            }

            if (Config.Settings.AutoCheckForUpdates)
            {
                var t = new Thread(AutoCheckForUpdate);
                t.IsBackground = true;
                t.Name = "Logbook Update Check";
                t.Start();
            }
        }

        private void AutoCheckForUpdate()
        {
            Version webVersion = null;
            string webVersionText = "";
            try
            {
                _web = new WebClient();
                _web.Proxy = WebRequest.GetSystemWebProxy();
                _web.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                _web.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                while (true)
                {
                    try
                    {
                        webVersionText =
                            _web.DownloadString("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/.version");
                        break;
                    }
                    catch
                    {
                        return;
                    }
                }

                webVersion = new Version(webVersionText + ".0");
                if (LogWindow != null)
                    LogWindow.WriteLog("[Updater] Local version = v" + Program.VersionString + ", Web version = v" +
                                       webVersionText);
            }
            catch
            {
                return;
            }

            if (webVersion <= Program.Version)
            {
                _web = null;
                return;
            }

            if (MessageBox.Show("A newer version is available: v" + webVersionText + " Update now?", "Update Available",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                DialogResult.No)
                return;

            try
            {
                StatusForm = new frmDownload();
                StatusForm.Show();
                StatusForm.SetMessage("Initializing...");
                _downloaded = false;
                _web.DownloadProgressChanged += web_DownloadProgressChanged;
                _web.DownloadFileCompleted += web_DownloadFileCompleted;
                _web.DownloadFileAsync(
                    new Uri("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/LuciusIncidentLogbook.exe"),
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LuciusIncidentLogbook.Update.exe"));
                while (!_downloaded)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }

                StatusForm.SetProgressValue(100);
                StatusForm.SetProgressMax(100);
                StatusForm.SetMessage("Download Completed!");
                Application.DoEvents();
                Thread.Sleep(1000);

                Process.Start(
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LuciusIncidentLogbook.Update.exe"),
                    "/update " + Process.GetCurrentProcess().Id.ToString() + " " + Program.VersionString);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while downloading the update: " + ex.Message + " You may retry " +
                                "from the Help menu.", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                StatusForm.Close();
                StatusForm = null;
                _web = null;
            }
        }

        private void Server_MatchUpdateFromWeb(object sender, MatchUpdateFromWebEventArgs e)
        {
            Tournament tournament = Config.Settings.GetTournament(e.TournamentName);
            foreach (TournamentMatch match in tournament.Rounds[tournament.Rounds.Count - 1].Matches)
            {
                if (match.Players.Contains(e.Player1ID) && match.Players.Contains(e.Player2ID))
                {
                    PlayerRecord record1 = Config.Settings.GetPlayer(e.Player1ID);
                    PlayerRecord record2 = Config.Settings.GetPlayer(e.Player2ID);
                    var result1 = new MatchResult(e.Player1ID);
                    var result2 = new MatchResult(e.Player2ID);
                    if (e.Player1Forfeit)
                    {
                        result1.Forfeited = true;
                        result2.VictoryPoints = 8;
                    }
                    else if (e.Player2Forfeit)
                    {
                        result1.VictoryPoints = 8;
                        result2.Forfeited = true;
                    }
                    else
                    {
                        result1.VictoryPoints = e.Player1Vp;
                        result2.VictoryPoints = e.Player2Vp;
                    }
                    match.Results.Clear();
                    match.Results.Add(e.Player1ID, result1);
                    match.Results.Add(e.Player2ID, result2);
                    match.CalculateResults();

                    Config.Settings.SaveEvents();

                    if (LogWindow != null)
                        LogWindow.WriteLog("[Web Server] Recorded a match score in " + e.TournamentName + ": " +
                                           record1.Name + " = " +
                                           (e.Player1Forfeit ? "Forfeited" : e.Player1Vp.ToString()) +
                                           " VP" + (e.Player1Vp == 1 ? "" : "s") + ", " +
                                           record2.Name + " = " +
                                           (e.Player2Forfeit ? "Forfeited" : e.Player2Vp.ToString()) +
                                           " VP" + (e.Player2Vp == 1 ? "" : "s") + ".");

                    foreach (Form form in MdiChildren)
                        if (form is frmTournamentRound)
                        {
                            var roundForm = (frmTournamentRound) form;
                            if (roundForm.TournamentName == e.TournamentName)
                            {
                                foreach (ctlTournamentMatch matchCtl in roundForm.pnlMatches.Controls)
                                {
                                    if (matchCtl.Player1ID == e.Player1ID && matchCtl.Player2ID == e.Player2ID)
                                    {
                                        roundForm.Invoke(new MethodInvoker(delegate
                                            {
                                                roundForm.UpdateMatch(matchCtl,
                                                                      e.Player1Forfeit ? "F" : e.Player1Vp.ToString(),
                                                                      e.Player2Forfeit ? "F" : e.Player2Vp.ToString());
                                            }));
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                }
            }
        }

        private void btnWebServerToggle_Click(object sender, EventArgs e)
        {
            var ip = LocalIPAddress();
            if (Config.Server.Active)
            {
                if (LogWindow != null)
                    LogWindow.WriteLog("Web Server Stopped Listening.");

                Config.Server.Active = false;
                lblWebStatus.Text = ip == null ? "Offline -- No Network Connection" : "Offline";
            }
            else
            {
                if (ip == null)
                {
                    MessageBox.Show("Cannot start the server until the network is online.", "Network Offine",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblWebStatus.Text = "Offline -- No Network Connection";
                }
                else
                {
                    if (LogWindow != null)
                        LogWindow.WriteLog("Web Server Started Listening.");

                    Config.Server.Active = true;
                    lblWebStatus.Text = "Listening at " + ip + " on Port " + Config.Settings.WebServerPort.ToString();
                }
            }
        }

        private void Server_WebStatusChanged(object sender, WebStatusChangedEventArgs e)
        {
            if (!e.Active)
            {
                if (!lblWebStatus.Text.StartsWith("Listening") && LogWindow != null)
                    LogWindow.WriteLog("Web Server Stopped Listening.");

                string IP = LocalIPAddress();
                if (IP == null)
                    lblWebStatus.Text = "Offline -- No Network Connection";
                else
                    lblWebStatus.Text = "Offline";
            }

            if (e.Message.Length > 0 && LogWindow != null)
                LogWindow.WriteLog("[WebServer] " + (e.Error ? "Error: " : "") + e.Message);
            else
                MessageBox.Show(e.Message, "Web Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Server.Shutdown();
        }

        private void showMessageLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LogWindow == null)
            {
                LogWindow = new frmLog();
                LogWindow.FormClosed += LogWindow_FormClosed;
                LogWindow.MdiParent = this;
                LogWindow.Show();
            }
            else
            {
                LogWindow.WindowState = FormWindowState.Normal;
                LogWindow.Focus();
            }
        }

        private void LogWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogWindow = null;
        }

        private void btnOpenNotes_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                if (childForm is frmNotes)
                {
                    if ((string) childForm.Tag == "General Notes")
                    {
                        childForm.WindowState = FormWindowState.Normal;
                        childForm.Focus();
                        return;
                    }
                }
            }

            var notes = new frmNotes(null);
            notes.MdiParent = this;
            notes.Show();
        }
    }
}