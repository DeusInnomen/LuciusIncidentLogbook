using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace KitchenGeeks
{
    public class Config
    {
        private static Config _instance;
        private static WebServer _server;
        private static readonly Random Rng = new Random();

        /// <summary>
        ///     Declares a new instance of the Config class.
        /// </summary>
        private Config()
        {
            Players = new List<PlayerRecord>();
            Tournaments = new List<Tournament>();
            Leagues = new List<League>();

            SeparateEventFiles = false;
            ArchivePath = ".\\Archives";
            ConfirmNewRounds = true;
            OpenAfterPlayersAdded = false;
            AssignTablesRandomly = false;
            AutoEnableWebServer = false;
            WebServerPassword = "password";
            WebServerPort = 9000;
            AutoCheckForUpdates = true;
            BackupsToKeep = 10;
            BackupPath = ".\\Backups";
            Notes = "";
            SkipTiedRanks = true;
            ForumName = "";
            DisableReportIfSent = true;

            SMTPFromName = "";
            SMTPFromAddress = "";
            SMTPServer = "";
            SMTPPort = 25;
            SMTPUsername = "";
            SMTPPassword = "";
        }

        /// <summary>
        ///     Gets the static instance of the application's configuration.
        /// </summary>
        public static Config Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Config();
                    _instance.Load();
                }
                return _instance;
            }
        }

        /// <summary>
        ///     Gets the static instance of the Web Server.
        /// </summary>
        public static WebServer Server
        {
            get { return _server ?? (_server = new WebServer()); }
        }

        /// <summary>
        ///     Represents the shared Random object used by the application.
        /// </summary>
        public static Random Random
        {
            get { return Rng; }
        }

        /// <summary>
        ///     Provides basic encryption functions.
        /// </summary>
        private static readonly SimpleAES Encryption = new SimpleAES();

        /// <summary>
        ///     The PlayerRecords available to the application.
        /// </summary>
        public List<PlayerRecord> Players { get; private set; }

        /// <summary>
        ///     The Tournaments available to the application.
        /// </summary>
        public List<Tournament> Tournaments { get; private set; }

        /// <summary>
        ///     The Leagues available to the application.
        /// </summary>
        public List<League> Leagues { get; private set; }

        /// <summary>
        ///     If true, Events will be written to separate files instead of a single Events.dat file.
        /// </summary>
        public bool SeparateEventFiles { get; set; }

        /// <summary>
        ///     The folder to write archived Events to.
        /// </summary>
        public string ArchivePath { get; set; }

        /// <summary>
        ///     If true, by default the "Confirm Before Starting New Rounds" box is checked.
        /// </summary>
        public bool ConfirmNewRounds { get; set; }

        /// <summary>
        ///     If true, a tournament is opened after players have been added to it.
        /// </summary>
        public bool OpenAfterPlayersAdded { get; set; }

        /// <summary>
        ///     If true, Rounds after the first have their tables assigned randomly. Otherwise, assignments are
        ///     by rank. (Table 1 = Top 2 Players, etc.)
        /// </summary>
        public bool AssignTablesRandomly { get; set; }

        /// <summary>
        ///     If true, the built-in web server will be automatically started when the application starts up.
        /// </summary>
        public bool AutoEnableWebServer { get; set; }

        /// <summary>
        ///     The password required to interface with the Logbook via a mobile device's browser.
        /// </summary>
        public string WebServerPassword { get; set; }

        /// <summary>
        ///     The port that the web server listens on for requests.
        /// </summary>
        public int WebServerPort { get; set; }

        /// <summary>
        ///     If true, the application will automatically check for any updates when it starts.
        /// </summary>
        public bool AutoCheckForUpdates { get; set; }

        /// <summary>
        ///     The number of backups of the settings files to keep. 0 disables this feature.
        /// </summary>
        public int BackupsToKeep { get; set; }

        /// <summary>
        ///     The location to keep backup copies.
        /// </summary>
        public string BackupPath { get; set; }

        /// <summary>
        ///     Notes being kept independent of the Events.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     If true, ranks following a tie are skipped: 1, 2, 2, 4, 5 instead of 1, 2, 2, 3, 4.
        /// </summary>
        public bool SkipTiedRanks { get; set; }

        /// <summary>
        ///     The Henchman's forum name (for Event reports).
        /// </summary>
        public string ForumName { get; set; }

        /// <summary>
        ///     If true, the "Send Event Report" buttons will be disabled if a report was already previously sent.
        /// </summary>
        public bool DisableReportIfSent { get; set; }

        /// <summary>
        ///     The name of the person sending the email.
        /// </summary>
        public string SMTPFromName { get; set; }

        /// <summary>
        ///     The address that email will be sent from.
        /// </summary>
        public string SMTPFromAddress { get; set; }

        /// <summary>
        ///     The name of the SMTP server to send through.
        /// </summary>
        public string SMTPServer { get; set; }

        /// <summary>
        ///     The port number the SMTP server is running on.
        /// </summary>
        public int SMTPPort { get; set; }

        /// <summary>
        ///     The username to use for sending email, if any.
        /// </summary>
        public string SMTPUsername { get; set; }

        /// <summary>
        ///     The decrypted password to use for sending email, if any.
        /// </summary>
        public string SMTPPassword { get; set; }

        /// <summary>
        ///     Load all available data from the drive.
        /// </summary>
        public void Load()
        {
            Players.Clear();
            Tournaments.Clear();
            Leagues.Clear();

            var xml = new XmlDocument();
            if (File.Exists(Path.Combine(Program.BasePath, "LuciusIncidentLogbook.dat")))
            {
                xml.Load(Path.Combine(Program.BasePath, "LuciusIncidentLogbook.dat"));
                var baseNode = xml.SelectSingleNode("//Settings");

                if (baseNode != null)
                {
                    var node = baseNode.SelectSingleNode("SeparateTournamentFiles");
                    if (node != null)
                        SeparateEventFiles = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("ArchivePath");
                    if (node != null)
                        ArchivePath = node.InnerText;
                    node = baseNode.SelectSingleNode("ConfirmNewRounds");
                    if (node != null)
                        ConfirmNewRounds = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("OpenAfterPlayersAdded");
                    if (node != null)
                        OpenAfterPlayersAdded = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("AssignTablesRandomly");
                    if (node != null)
                        AssignTablesRandomly = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("AutoEnableWebServer");
                    if (node != null)
                        AutoEnableWebServer = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("WebServerPassword");
                    if (node != null)
                        WebServerPassword = node.InnerText;
                    node = baseNode.SelectSingleNode("WebServerPort");
                    if (node != null)
                        WebServerPort = Convert.ToInt32(node.InnerText);
                    node = baseNode.SelectSingleNode("AutoCheckForUpdates");
                    if (node != null)
                        AutoCheckForUpdates = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("BackupsToKeep");
                    if (node != null)
                        BackupsToKeep = Convert.ToInt32(node.InnerText);
                    node = baseNode.SelectSingleNode("BackupPath");
                    if (node != null)
                        BackupPath = node.InnerText;
                    node = baseNode.SelectSingleNode("Notes");
                    if (node != null)
                        Notes = node.InnerText;
                    node = baseNode.SelectSingleNode("SkipTiedRanks");
                    if (node != null)
                        SkipTiedRanks = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("ForumName");
                    if (node != null)
                        ForumName = node.InnerText;
                    node = baseNode.SelectSingleNode("DisableReportIfSent");
                    if (node != null)
                        DisableReportIfSent = Convert.ToBoolean(node.InnerText);
                    node = baseNode.SelectSingleNode("Email/FromName");
                    if (node != null)
                        SMTPFromName = node.InnerText;
                    node = baseNode.SelectSingleNode("Email/FromAddress");
                    if (node != null)
                        SMTPFromAddress = node.InnerText;
                    node = baseNode.SelectSingleNode("Email/Server");
                    if (node != null)
                        SMTPServer = node.InnerText;
                    node = baseNode.SelectSingleNode("Email/Port");
                    if (node != null)
                        SMTPPort = Convert.ToInt32(node.InnerText);
                    node = baseNode.SelectSingleNode("Email/Username");
                    if (node != null)
                        SMTPUsername = node.InnerText;
                    node = baseNode.SelectSingleNode("Email/Password");
                    if (node != null)
                        SMTPPassword = Encryption.DecryptString(node.InnerText);
                }
            }
            if (!Directory.Exists(ArchivePath)) Directory.CreateDirectory(ArchivePath);
            if (!Directory.Exists(BackupPath)) Directory.CreateDirectory(BackupPath);

            if (File.Exists(Path.Combine(Program.BasePath, "Players.dat")))
            {
                xml.Load(Path.Combine(Program.BasePath, "Players.dat"));
                XmlNode baseNode = xml.SelectSingleNode("//Players");
                if (baseNode != null)
                {
                    XmlNodeList playerNodes = xml.SelectNodes("//Players/Player");
                    if (playerNodes != null)
                    {
                        foreach (XmlNode playerNode in playerNodes)
                        {
                            var record = new PlayerRecord(playerNode);
                            Players.Add(record);
                        }
                    }
                }
            }

            var needsSave = false;
            if (SeparateEventFiles)
            {
                string targetPath = Path.Combine(Program.BasePath, "Events");
                if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
                string[] tournamentFiles = Directory.GetFiles(targetPath, "*.tournament.dat",
                                                              SearchOption.TopDirectoryOnly);
                foreach (string tournamentFile in tournamentFiles)
                {
                    try
                    {
                        xml.Load(tournamentFile);
                        XmlNode tournamentNode = xml.SelectSingleNode("//Tournament");
                        if (tournamentNode != null)
                        {
                            var tournament = new Tournament(tournamentNode);
                            Tournaments.Add(tournament);
                            if (tournament.NeedsSave) needsSave = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected error while parsing tournaments in '" + tournamentFile +
                                        ": " + ex.Message, "Exception", MessageBoxButtons.OK);
                    }
                }

                string[] leagueFiles = Directory.GetFiles(targetPath, "*.league.dat",
                                                          SearchOption.TopDirectoryOnly);
                foreach (string leagueFile in leagueFiles)
                {
                    try
                    {
                        xml.Load(leagueFile);
                        XmlNode leagueNode = xml.SelectSingleNode("//League");
                        if (leagueNode != null)
                        {
                            var league = new League(leagueNode);
                            Leagues.Add(league);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected error while parsing leagues in '" + leagueFile +
                                        ": " + ex.Message, "Exception", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                if (File.Exists(Path.Combine(Program.BasePath, "Events.dat")))
                {
                    xml.Load(Path.Combine(Program.BasePath, "Events.dat"));
                    XmlNodeList tournamentNodes = xml.SelectNodes("//Events/Tournaments/Tournament");
                    if (tournamentNodes != null)
                        foreach (XmlNode tournamentNode in tournamentNodes)
                        {
                            var tournament = new Tournament(tournamentNode);
                            Tournaments.Add(tournament);
                            if (tournament.NeedsSave) needsSave = true;
                        }

                    XmlNodeList leagueNodes = xml.SelectNodes("//Events/Leagues/League");
                    if (leagueNodes != null)
                        foreach (XmlNode leagueNode in leagueNodes)
                            Leagues.Add(new League(leagueNode));
                }
            }
            if (needsSave) SaveEvents();
        }

        /// <summary>
        ///     Save all settings and data to the drive.
        /// </summary>
        /// <returns>True if successfully written.</returns>
        public bool Save()
        {
            bool success = SaveSettings();
            if (!SavePlayers()) success = false;
            if (!SaveEvents()) success = false;

            return success;
        }

        /// <summary>
        ///     Save Application data to the drive.
        /// </summary>
        /// <returns>True if successfully written.</returns>
        public bool SaveSettings()
        {
            ArchiveFile(Path.Combine(Program.BasePath, "LuciusIncidentLogbook.dat"));
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode baseNode = xml.AppendChild(xml.CreateElement("Settings"));
            baseNode.AppendChild(xml.CreateElement("SeparateTournamentFiles")).InnerText = SeparateEventFiles.ToString();
            baseNode.AppendChild(xml.CreateElement("ArchivePath")).InnerText = ArchivePath;
            baseNode.AppendChild(xml.CreateElement("ConfirmNewRounds")).InnerText = ConfirmNewRounds.ToString();
            baseNode.AppendChild(xml.CreateElement("OpenAfterPlayersAdded")).InnerText =
                OpenAfterPlayersAdded.ToString();
            baseNode.AppendChild(xml.CreateElement("AssignTablesRandomly")).InnerText = AssignTablesRandomly.ToString();
            baseNode.AppendChild(xml.CreateElement("AutoEnableWebServer")).InnerText = AutoEnableWebServer.ToString();
            baseNode.AppendChild(xml.CreateElement("WebServerPassword")).InnerText = WebServerPassword;
            baseNode.AppendChild(xml.CreateElement("WebServerPort")).InnerText = WebServerPort.ToString();
            baseNode.AppendChild(xml.CreateElement("AutoCheckForUpdates")).InnerText = AutoCheckForUpdates.ToString();
            baseNode.AppendChild(xml.CreateElement("BackupsToKeep")).InnerText = BackupsToKeep.ToString();
            baseNode.AppendChild(xml.CreateElement("BackupPath")).InnerText = BackupPath;
            baseNode.AppendChild(xml.CreateElement("Notes")).AppendChild(xml.CreateCDataSection(Notes));
            baseNode.AppendChild(xml.CreateElement("SkipTiedRanks")).InnerText = SkipTiedRanks.ToString();
            baseNode.AppendChild(xml.CreateElement("ForumName")).InnerText = ForumName;
            baseNode.AppendChild(xml.CreateElement("DisableReportIfSent")).InnerText = DisableReportIfSent.ToString();

            XmlNode emailNode = baseNode.AppendChild(xml.CreateElement("Email"));
            emailNode.AppendChild(xml.CreateElement("FromName")).InnerText = SMTPFromName;
            emailNode.AppendChild(xml.CreateElement("FromAddress")).InnerText = SMTPFromAddress;
            emailNode.AppendChild(xml.CreateElement("Server")).InnerText = SMTPServer;
            emailNode.AppendChild(xml.CreateElement("Port")).InnerText = SMTPPort.ToString();
            if (SMTPUsername.Length > 0)
                emailNode.AppendChild(xml.CreateElement("Username")).InnerText = SMTPUsername;
            if (SMTPPassword.Length > 0)
                emailNode.AppendChild(xml.CreateElement("Password")).InnerText = Encryption.EncryptToString(SMTPPassword);

            var writer = new XmlTextWriter(new FileStream(Path.Combine(Program.BasePath,
                                                                       "LuciusIncidentLogbook.dat"), FileMode.Create),
                                           null) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = '\t' };

            xml.WriteTo(writer);
            writer.Flush();
            writer.Close();
            return true;
        }

        /// <summary>
        ///     Save Player data to the drive.
        /// </summary>
        /// <returns>True if successfully written.</returns>
        public bool SavePlayers()
        {
            ArchiveFile(Path.Combine(Program.BasePath, "Players.dat"));
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));

            XmlNode playersNode = xml.AppendChild(xml.CreateElement("Players"));
            foreach (PlayerRecord record in Players)
            {
                playersNode.AppendChild(record.ToXML(xml));
            }

            var writer = new XmlTextWriter(new FileStream(Path.Combine(Program.BasePath,
                                                                       "Players.dat"), FileMode.Create), null)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };

            xml.WriteTo(writer);
            writer.Flush();
            writer.Close();
            return true;
        }

        /// <summary>
        ///     Save Event data to the drive.
        /// </summary>
        /// <returns>True if successfully written.</returns>
        public bool SaveEvents()
        {
            if (SeparateEventFiles)
            {
                string targetPath = Path.Combine(Program.BasePath, "Events");
                if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);

                foreach (Tournament record in Tournaments)
                {
                    string targetFile = ScrubFilename(record.Name + ".tournament.dat");
                    ArchiveFile(Path.Combine(targetPath, targetFile));
                    var xml = new XmlDocument();
                    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
                    xml.AppendChild(record.ToXml(xml));

                    var writer = new XmlTextWriter(new FileStream(Path.Combine(targetPath, targetFile),
                                                                  FileMode.Create), null)
                        {
                            Formatting = Formatting.Indented,
                            Indentation = 1,
                            IndentChar = '\t'
                        };

                    xml.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                }

                foreach (League record in Leagues)
                {
                    string targetFile = ScrubFilename(record.Name + ".league.dat");
                    ArchiveFile(Path.Combine(targetPath, targetFile));
                    var xml = new XmlDocument();
                    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
                    xml.AppendChild(record.ToXML(xml));

                    var writer = new XmlTextWriter(new FileStream(Path.Combine(targetPath, targetFile),
                                                                  FileMode.Create), null)
                        {
                            Formatting = Formatting.Indented,
                            Indentation = 1,
                            IndentChar = '\t'
                        };

                    xml.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                }
            }
            else
            {
                ArchiveFile(Path.Combine(Program.BasePath, "Events.dat"));
                var xml = new XmlDocument();
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
                XmlNode eventsNode = xml.AppendChild(xml.CreateElement("Events"));

                XmlNode tournamentsNode = eventsNode.AppendChild(xml.CreateElement("Tournaments"));
                foreach (Tournament record in Tournaments)
                    tournamentsNode.AppendChild(record.ToXml(xml));

                XmlNode leaguesNode = eventsNode.AppendChild(xml.CreateElement("Leagues"));
                foreach (League record in Leagues)
                    leaguesNode.AppendChild(record.ToXML(xml));

                var writer = new XmlTextWriter(new FileStream(Path.Combine(Program.BasePath,
                                                                           "Events.dat"), FileMode.Create), null)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 1,
                        IndentChar = '\t'
                    };

                xml.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }
            return true;
        }

        private void ArchiveFile(string filename)
        {
            var copies = new List<string>(Directory.GetFiles(BackupPath, Path.GetFileNameWithoutExtension(filename) +
                                                                         "*" + Path.GetExtension(filename)));
            if (copies.Count >= BackupsToKeep)
            {
                copies.Sort(delegate(string a, string b)
                    {
                        string fileDateA =
                            Path.GetFileNameWithoutExtension(a)
                                .Substring(Path.GetFileNameWithoutExtension(filename).Length, 14);
                        string fileDateB =
                            Path.GetFileNameWithoutExtension(b)
                                .Substring(Path.GetFileNameWithoutExtension(filename).Length, 14);
                        DateTime dateA = DateTime.ParseExact(fileDateA, "yyyyMMddHHmmss", null);
                        DateTime dateB = DateTime.ParseExact(fileDateB, "yyyyMMddHHmmss", null);
                        return dateA.CompareTo(dateB);
                    });
                for (int index = 0; index < (copies.Count - BackupsToKeep + 1); index++)
                    File.Delete(copies[index]);
            }
            var backupName = Path.Combine(BackupPath, Path.GetFileNameWithoutExtension(filename) +
                                                      DateTime.Now.ToString("yyyyMMddHHmmss") +
                                                      Path.GetExtension(filename));
            if (BackupsToKeep > 0 && File.Exists(filename) && !File.Exists(backupName))
                File.Copy(filename, backupName);
        }

        /// <summary>
        ///     Makes the filename provided valid by swapping invalid characters for underscores (_).
        /// </summary>
        /// <param name="filename">The filename to scrub.</param>
        /// <returns>The scrubbed filename.</returns>
        public static string ScrubFilename(string filename)
        {
            var invalid = new[] { "\\", "/", ":", "*", "?", "\"", "<", ">", "|", "!" };
            return invalid.Aggregate(filename, (current, invalidChar) => current.Replace(invalidChar, "_"));
        }

        /// <summary>
        ///     Looks up a PlayerRecord object based on its ID.
        /// </summary>
        /// <param name="id">The ID value to look up.</param>
        /// <returns>The PlayerRecord matching the ID, or null if not found.</returns>
        public PlayerRecord GetPlayer(string id)
        {
            return Players.FirstOrDefault(player => player.ID == id);
        }

        /// <summary>
        ///     Looks up a Tournament object based on its name.
        /// </summary>
        /// <param name="name">The name of the Tournament to look up.</param>
        /// <returns>The Tournament bearing the name, or null if not found.</returns>
        public Tournament GetTournament(string name)
        {
            return Tournaments.FirstOrDefault(tournament => tournament.Name == name);
        }

        /// <summary>
        ///     Deletes the tournament from the configuration.
        /// </summary>
        /// <param name="name">The name of the Tournament to delete.</param>
        /// <returns>True if the request was successful.</returns>
        public void DeleteTournament(string name)
        {
            Tournament tournament = GetTournament(name);
            if (tournament == null) return;
            Tournaments.Remove(tournament);

            if (SeparateEventFiles)
            {
                string targetPath = Path.Combine(Program.BasePath, "Events");
                string targetFile = ScrubFilename(tournament.Name + ".tournament.dat");
                File.Delete(Path.Combine(targetPath, targetFile));
            }
            else
                SaveEvents();
        }

        /// <summary>
        ///     Saves a copy of the named Tournament to the Archive folder.
        /// </summary>
        /// <param name="name">The name of the Tournament to archive.</param>
        /// <returns>True if the request was successful.</returns>
        public void ArchiveTournament(string name)
        {
            Tournament tournament = GetTournament(name);
            if (tournament == null) return;

            string targetFile = ScrubFilename(tournament.Name + ".tournament.dat");
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            xml.AppendChild(tournament.ToXml(xml));

            var writer = new XmlTextWriter(new FileStream(Path.Combine(ArchivePath, targetFile),
                                                          FileMode.Create), null)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };

            xml.WriteTo(writer);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        ///     Looks up a League object based on its name.
        /// </summary>
        /// <param name="name">The name of the League to look up.</param>
        /// <returns>The League bearing the name, or null if not found.</returns>
        public League GetLeague(string name)
        {
            return Leagues.FirstOrDefault(league => league.Name == name);
        }

        /// <summary>
        ///     Deletes the League from the configuration.
        /// </summary>
        /// <param name="name">The name of the League to delete.</param>
        /// <returns>True if the request was successful.</returns>
        public void DeleteLeague(string name)
        {
            League league = GetLeague(name);
            if (league == null) return;
            Leagues.Remove(league);

            if (SeparateEventFiles)
            {
                string targetPath = Path.Combine(Program.BasePath, "Events");
                string targetFile = ScrubFilename(league.Name + ".league.dat");
                File.Delete(Path.Combine(targetPath, targetFile));
            }
            else
                SaveEvents();
        }

        /// <summary>
        ///     Saves a copy of the named League to the Archive folder.
        /// </summary>
        /// <param name="name">The name of the League to archive.</param>
        /// <returns>True if the request was successful.</returns>
        public void ArchiveLeague(string name)
        {
            League league = GetLeague(name);
            if (league == null) return;

            string targetFile = ScrubFilename(league.Name + ".league.dat");
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            xml.AppendChild(league.ToXML(xml));

            var writer = new XmlTextWriter(new FileStream(Path.Combine(ArchivePath, targetFile),
                                                          FileMode.Create), null)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                };

            xml.WriteTo(writer);
            writer.Flush();
            writer.Close();
        }
    }


    public class ListViewItemComparer : IComparer
    {
        private readonly bool _asc;
        private readonly int _col;

        public ListViewItemComparer(int column, bool ascending)
        {
            _col = column;
            _asc = ascending;
        }

        public int Compare(object x, object y)
        {
            int returnVal;

            int tmpValue;
            if (int.TryParse(((ListViewItem)x).SubItems[_col].Text, out tmpValue) &&
                int.TryParse(((ListViewItem)y).SubItems[_col].Text, out tmpValue))
                returnVal = Convert.ToInt32(((ListViewItem)x).SubItems[_col].Text).CompareTo(
                    Convert.ToInt32(((ListViewItem)y).SubItems[_col].Text));
            else
                returnVal = ((ListViewItem)x).SubItems[_col].Text.CompareTo(
                    ((ListViewItem)y).SubItems[_col].Text);
            return returnVal * (_asc ? 1 : -1);
        }
    }

    public enum TableNumbering
    {
        Normal = 0,
        Odd = 1,
        Even = 2
    }
}