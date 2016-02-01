using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace KitchenGeeks
{
    /// <summary>
    /// Contains the logic to perform upgrades between versions. In some cases I need to make drastic changes to
    /// the structure of files. This code handles each of the individual updates from one version to the next.
    /// </summary>
    internal class Upgrades
    {
        /// <summary>
        /// Handle upgrades based on the version number of the previously installed copy of the Logbook.
        /// </summary>
        /// <param name="fromVersion">The Version being upgraded from.</param>
        public static void PerformUpgrades(Version fromVersion)
        {
            // Upgrades for 0.1.6
            if (fromVersion <= new Version("0.1.6"))
                Upgrades0160();
            // Upgrades for 0.2.0
            if (fromVersion <= new Version("0.2.0"))
                Upgrades0200();
            // Upgrades for 0.2.1
            if (fromVersion <= new Version("0.2.1"))
                Upgrades0210();
            Config.Settings.Load();
        }

        // Accumulation format is now called Victory.
        private static void Upgrades0210()
        {
            var xml = new XmlDocument();
            if (Config.Settings.SeparateEventFiles)
            {
                var targetPath = Path.Combine(Program.BasePath, "Tournaments");
                if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
                var files = new List<string>(Directory.GetFiles(targetPath, "*.tournament.dat",
                    SearchOption.TopDirectoryOnly));
                files.AddRange(Directory.GetFiles(targetPath, "*.league.dat",
                    SearchOption.TopDirectoryOnly));
                foreach (string filename in files)
                {
                    xml.Load(filename);
                    var oldNodes = xml.SelectNodes("//Events/Tournaments/Tournament/Format[. = 'Accumulation']");
                    if (oldNodes != null)
                        foreach (XmlNode oldNode in oldNodes)
                            oldNode.InnerText = "Victory";
                    oldNodes = xml.SelectNodes("//Events/Leagues/League/Format[. = 'Accumulation']");
                    if (oldNodes != null)
                        foreach (XmlNode oldNode in oldNodes)
                            oldNode.InnerText = "Victory";

                    var writer = new XmlTextWriter(new FileStream(filename, FileMode.Create), null)
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
            else if (File.Exists(Path.Combine(Program.BasePath, "Events.dat")))
            {
                xml.Load(Path.Combine(Program.BasePath, "Events.dat"));
                var oldNodes = xml.SelectNodes("//Events/Tournaments/Tournament/Format[. = 'Accumulation']");
                if (oldNodes != null)
                    foreach (XmlNode oldNode in oldNodes)
                        oldNode.InnerText = "Victory";
                oldNodes = xml.SelectNodes("//Events/Leagues/League/Format[. = 'Accumulation']");
                if (oldNodes != null)
                    foreach (XmlNode oldNode in oldNodes)
                        oldNode.InnerText = "Victory";

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
        }

        /// <summary>
        /// Renames Tournaments.dat to Events.dat, or if using separate Tournament files, renames the Tournaments
        /// folder to Events.
        /// </summary>
        private static void Upgrades0200()
        {
            var xml = new XmlDocument();
            if (!Config.Settings.SeparateEventFiles && !File.Exists(Path.Combine(Program.BasePath, "Events.dat")))
            {
                xml.Load(Path.Combine(Program.BasePath, "Tournaments.dat"));
                var baseNode = xml.SelectSingleNode("//Tournaments");
                if (baseNode != null) xml.RemoveChild(baseNode);
                var eventsNode = xml.AppendChild(xml.CreateElement("Events"));
                if (baseNode != null) eventsNode.AppendChild(baseNode);

                var writer = new XmlTextWriter(new FileStream(Path.Combine(Program.BasePath,
                    "Tournaments.dat"), FileMode.Create), null)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 1,
                        IndentChar = '\t'
                    };

                xml.WriteTo(writer);
                writer.Flush();
                writer.Close();

                File.Move(Path.Combine(Program.BasePath, "Tournaments.dat"),
                    Path.Combine(Program.BasePath, "Events.dat"));
            }
            else if (!Directory.Exists(Path.Combine(Program.BasePath, "Events")))
                Directory.Move(Path.Combine(Program.BasePath, "Tournaments"),
                    Path.Combine(Program.BasePath, "Events"));

        }

        /// <summary>
        /// Converts Players.dat to use GUID IDs instead of integer IDs, then cascades those changes through the
        /// tournaments.
        /// </summary>
        private static void Upgrades0160()
        {
            // If there is no Players.dat file, something's REALLY wrong, abandon ship instead.
            if (!File.Exists(Path.Combine(Program.BasePath, "Players.dat"))) return;

            var newIDs = new Dictionary<int, string>();
            var xml = new XmlDocument();
            xml.Load(Path.Combine(Program.BasePath, "Players.dat"));
            var baseNode = xml.SelectSingleNode("//Players");
            if (baseNode != null)
            {
                // The ID in the file is no longer required, since the player IDs will be unique.
                if (baseNode.Attributes != null)
                {
                    XmlAttribute attrib = baseNode.Attributes["ID"];
                    if (attrib != null)
                        baseNode.Attributes.Remove(attrib);

                    // Go through each Player, find their old ID and replace it with a new GUID-based ID.
                    XmlNodeList playerNodes = xml.SelectNodes("//Players/Player");
                    if (playerNodes != null)
                    {
                        foreach (XmlNode playerNode in playerNodes)
                        {
                            if (playerNode.Attributes != null) attrib = playerNode.Attributes["ID"];
                            if (attrib != null)
                            {
                                int oldID = Convert.ToInt32(attrib.Value);
                                string newID = Guid.NewGuid().ToString();
                                newIDs.Add(oldID, newID);
                                attrib.Value = newID;
                            }
                        }
                    }
                }
            }

            // Write the updated Players.dat file out.
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

            // Now, we need to go through the existing tournaments and replace their player IDs.
            if (Config.Settings.SeparateEventFiles)
            {
                string targetPath = Path.Combine(Program.BasePath, "Tournaments");
                if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
                string[] tournamentFiles = Directory.GetFiles(targetPath, "*.tournament.dat",
                    SearchOption.TopDirectoryOnly);
                foreach (string tournamentFile in tournamentFiles)
                {
                    xml.Load(tournamentFile);
                    XmlNode tournamentNode = xml.SelectSingleNode("//Tournament");
                    if (tournamentNode != null)
                    {
                        XmlNodeList playerNodes = tournamentNode.SelectNodes("Players/Player");
                        if (playerNodes != null)
                            foreach (XmlNode playerNode in playerNodes)
                                playerNode.InnerText = newIDs[Convert.ToInt32(playerNode.InnerText)];

                        playerNodes = tournamentNode.SelectNodes("Rounds/Round/Matches/Match/Players/Player");
                        if (playerNodes != null)
                            foreach (XmlNode playerNode in playerNodes)
                            {
                                if (playerNode.Attributes != null)
                                {
                                    XmlAttribute attrib = playerNode.Attributes["ID"];
                                    if (attrib != null)
                                        attrib.Value = newIDs[Convert.ToInt32(attrib.Value)];
                                }
                            }
                    }

                    writer = new XmlTextWriter(new FileStream(tournamentFile, FileMode.Create), null)
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
                if (File.Exists(Path.Combine(Program.BasePath, "Tournaments.dat")))
                {
                    xml.Load(Path.Combine(Program.BasePath, "Tournaments.dat"));
                    baseNode = xml.SelectSingleNode("//Tournaments");
                    if (baseNode != null)
                    {
                        XmlNodeList tournamentNodes = xml.SelectNodes("//Tournaments/Tournament");
                        if (tournamentNodes != null)
                        {
                            foreach (XmlNode tournamentNode in tournamentNodes)
                            {
                                var playerNodes = tournamentNode.SelectNodes("Players/Player");
                                if (playerNodes != null)
                                    foreach (XmlNode playerNode in playerNodes)
                                        playerNode.InnerText = newIDs[Convert.ToInt32(playerNode.InnerText)];

                                playerNodes = tournamentNode.SelectNodes("Rounds/Round/Matches/Match/Players/Player");
                                if (playerNodes != null)
                                    foreach (XmlNode playerNode in playerNodes)
                                    {
                                        if (playerNode.Attributes != null)
                                        {
                                            var attrib = playerNode.Attributes["ID"];
                                            if (attrib != null)
                                                attrib.Value = newIDs[Convert.ToInt32(attrib.Value)];
                                        }
                                    }
                            }
                        }
                    }

                    writer = new XmlTextWriter(new FileStream(Path.Combine(Program.BasePath,
                        "Tournaments.dat"), FileMode.Create), null)
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
        }
    }
}
