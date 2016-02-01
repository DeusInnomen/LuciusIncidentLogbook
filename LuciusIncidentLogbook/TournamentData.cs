using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace KitchenGeeks
{
    /// <summary>
    /// Contains all of the details and results for a single Tournament.
    /// </summary>
    public class Tournament
    {
        // Core Information
        /// <summary>
        /// The name of this Tournament.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The location of the Tournament, usually a convention or game store.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The date the Tournament is being held.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The URL where this event's post is on the official Wyrd forums.
        /// </summary>
        public string ForumURL { get; set; }
        /// <summary>
        /// Notes and information about the Tournament.
        /// </summary>
        public string Notes { get; set; }

        // Settings For Players
        /// <summary>
        /// If true, each player may select up to two Masters.
        /// </summary>
        public bool IsBrawl { get; set; }
        /// <summary>
        /// The scoring format of the tournament.
        /// </summary>
        public EventFormat Format { get; set; }
        /// <summary>
        /// The type of crews the players may hire from.
        /// </summary>
        public EventCrews Crews { get; set; }
        /// <summary>
        /// The type of strategies the players may choose from.
        /// </summary>
        public EventStrategy Strategy { get; set; }
        /// <summary>
        /// Scheme limitations imposed on the players.
        /// </summary>
        public EventSchemes Schemes { get; set; }
        /// <summary>
        /// The number of Soulstones available to players for hiring their crew with.
        /// </summary>
        public int Soulstones { get; set; }

        // Tournament Options
        /// <summary>
        /// The total number of rounds that the tournament will be held. Fixed when using Gaining Grounds.
        /// </summary>
        public int TotalRounds { get; set; }
        /// <summary>
        /// The default length of each round, in minutes. Fixed when using Gaining Grounds.
        /// </summary>
        public int RoundTimeLimit { get; set; }
        /// <summary>
        /// If true, matches will avoid pairing people from the same Region where possible.
        /// </summary>
        public bool TryToAvoidSameRegionMatches { get; set; }
        /// <summary>
        /// If true, matches will avoid pairing people playing the same Faction where possible.
        /// </summary>
        public bool TryToAvoidSameFactionMatches { get; set; }

        /// <summary>
        /// The players involved in the tournament.
        /// </summary>
        public Dictionary<string, int> Players { get; set; }
        /// <summary>
        /// The IDs of the players involved in the tournament.
        /// </summary>
        public List<string> PlayerIDs
        {
            get { return Players.Keys.ToList(); }
        }
        public bool NeedsSave { get; set; }
        /// <summary>
        /// Contains the information and results of the Rounds of the Tournament.
        /// </summary>
        public List<TournamentRound> Rounds { get; private set; }
        /// <summary>
        /// If true, the Tournament is finished and the scores are finalized.
        /// </summary>
        public bool Completed
        {
            get
            {
                return (Rounds.Count == TotalRounds && Rounds[TotalRounds - 1].Completed);
            }
        }
        /// <summary>
        /// Contains a list of players who have left the tournament prematurely. They are excluded from future
        /// matches.
        /// </summary>
        public List<string> RemovedPlayers { get; private set; }
        /// <summary>
        /// If true, the Event Organizer has already sent a Pre-Event Report.
        /// </summary>
        public bool PreEventSent { get; set; }
        /// <summary>
        /// If true, the Event Organizer has already sent a Post-Event Report.
        /// </summary>
        public bool PostEventSent { get; set; }
        /// <summary>
        /// Determines how the tables will be numbered when a match round is generated.
        /// </summary>
        public TableNumbering TableNumbering { get; set; }
        /// <summary>
        /// Contains the faction being played by the players.
        /// </summary>
        public Dictionary<string, Factions> PlayerFaction { get; set; }

        /// <summary>
        /// Get a single Player's current tournament results.
        /// </summary>
        /// <param name="id">The ID of the Player to look up.</param>
        /// <returns>A MatchResult object containing the accumulated results of the Player.</returns>
        public MatchResult GetPlayerResults(string id)
        {
            var results = new MatchResult(id);
            var hasBye = false;
            if (RemovedPlayers.Contains(id))
            {
                results = new MatchResult(id) { Forfeited = true };
            }
            else
            {
                foreach (var round in Rounds)
                {
                    if (!round.Completed) continue;
                    foreach (var match in round.Matches)
                    {
                        foreach (var playerID in match.Players)
                        {
                            if (playerID != id) continue;
                            // Found the player's individual match-up. Update its results just to be sure.
                            match.CalculateResults();

                            if (!match.Results.ContainsKey(id)) break;

                            // Detect a Forfeit and crash out with that result immediately.
                            if (match.Results[id].Forfeited)
                            {
                                results = new MatchResult(id) { Forfeited = true };
                                continue;
                            }

                            if (match.ByeRound)
                            {
                                results.VictoryPoints += 10;
                                results.TournamentPoints += 3;
                                results.Differential += 5;
                            }
                            else
                            {
                                results.VictoryPoints += match.Results[id].VictoryPoints;
                                results.TournamentPoints += match.Results[id].TournamentPoints;
                                results.Differential += match.Results[id].Differential;
                            }
                        }
                    }
                }
            }

            // Players who have a Bye get their skipped round averaged in at the end of the Tournament.
            if (hasBye && Completed)
            {
                results.Bye = true;
                results.VictoryPoints = results.VictoryPoints * Rounds.Count / (Rounds.Count - 1);
                results.TournamentPoints = results.TournamentPoints * Rounds.Count / (Rounds.Count - 1);
                results.Differential = results.Differential * Rounds.Count / (Rounds.Count - 1);
            }

            return results;
        }

        /// <summary>
        /// Declares a new Tournament instance.
        /// </summary>
        public Tournament()
        {
            Name = "";
            Location = "";
            Date = DateTime.Now;
            Notes = "";
            ForumURL = "";

            IsBrawl = false;
            Format = EventFormat.Domination;
            Crews = EventCrews.OpenFaction;
            Strategy = EventStrategy.FixedShared;
            Schemes = EventSchemes.UniqueSchemes;
            Soulstones = 50;
            TotalRounds = 3;
            RoundTimeLimit = 120;
            TryToAvoidSameRegionMatches = true;
            Players = new Dictionary<string, int>();
            Rounds = new List<TournamentRound>();
            RemovedPlayers = new List<string>();
            PreEventSent = false;
            PostEventSent = false;
            TableNumbering = TableNumbering.Normal;
            PlayerFaction = new Dictionary<string, Factions>();
        }

        /// <summary>
        /// Load a Tournament object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public Tournament(XmlNode sourceNode)
            : this()
        {
            XmlNode node = sourceNode.SelectSingleNode("Name");
            if (node != null)
                Name = node.InnerText;
            node = sourceNode.SelectSingleNode("Location");
            if (node != null)
                Location = node.InnerText;
            node = sourceNode.SelectSingleNode("Date");
            if (node != null)
                Date = DateTime.ParseExact(node.InnerText, "yyyyMMddHHmm", null);
            node = sourceNode.SelectSingleNode("Notes");
            if (node != null)
                Notes = node.InnerText;
            node = sourceNode.SelectSingleNode("ForumURL");
            if (node != null)
                ForumURL = node.InnerText;
            node = sourceNode.SelectSingleNode("IsBrawl");
            if (node != null)
                IsBrawl = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("Format");
            if (node != null)
                Format = (EventFormat)Enum.Parse(typeof(EventFormat), node.InnerText);
            node = sourceNode.SelectSingleNode("Strategy");
            if (node != null)
                Strategy = (EventStrategy)Enum.Parse(typeof(EventStrategy), node.InnerText);
            node = sourceNode.SelectSingleNode("Crews");
            if (node != null)
                Crews = (EventCrews)Enum.Parse(typeof(EventCrews), node.InnerText);
            node = sourceNode.SelectSingleNode("Schemes");
            if (node != null)
                Schemes = (EventSchemes)Enum.Parse(typeof(EventSchemes), node.InnerText);
            node = sourceNode.SelectSingleNode("SoulStones");
            if (node != null)
                Soulstones = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("TotalRounds");
            if (node != null)
                TotalRounds = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("RoundTimeLimit");
            if (node != null)
                RoundTimeLimit = Convert.ToInt32(node.InnerText);
            node = sourceNode.SelectSingleNode("TryToAvoidSameRegionMatches");
            if (node != null)
                TryToAvoidSameRegionMatches = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("TryToAvoidSameFactionMatches");
            if (node != null)
                TryToAvoidSameFactionMatches = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("PreEventSent");
            if (node != null)
                PreEventSent = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("PostEventSent");
            if (node != null)
                PostEventSent = Convert.ToBoolean(node.InnerText);
            node = sourceNode.SelectSingleNode("TableNumbering");
            if (node != null)
                TableNumbering = (TableNumbering)Enum.Parse(typeof(TableNumbering), node.InnerText);

            var players = sourceNode.SelectNodes("Players/Player");
            if (players != null)
                foreach (XmlNode player in players)
                {
                    var id = 0;
                    var faction = Factions.Undeclared;
                    if (player.Attributes != null)
                    {
                        var idAttrib = player.Attributes["Number"];
                        if (idAttrib != null)
                            id = Convert.ToInt32(idAttrib.Value);
                        var factionAttrib = player.Attributes["Faction"];
                        if (factionAttrib != null)
                            faction = (Factions)Enum.Parse(typeof(Factions), factionAttrib.Value);
                    }
                    AddPlayer(player.InnerText, faction, id);
                    if (id == 0) NeedsSave = true;
                }

            var rounds = sourceNode.SelectNodes("Rounds/Round");
            if (rounds != null)
                foreach (XmlNode round in rounds)
                    Rounds.Add(new TournamentRound(round, Name));

            var removedPlayers = sourceNode.SelectNodes("Removed/Player");
            if (removedPlayers != null)
                foreach (XmlNode player in removedPlayers)
                    RemovedPlayers.Add(player.InnerText);
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXml(XmlDocument xml)
        {
            XmlNode baseNode = xml.CreateElement("Tournament");
            baseNode.AppendChild(xml.CreateElement("Name")).InnerText = Name;
            baseNode.AppendChild(xml.CreateElement("Location")).InnerText = Location;
            baseNode.AppendChild(xml.CreateElement("Date")).InnerText = Date.ToString("yyyyMMddHHmm");
            baseNode.AppendChild(xml.CreateElement("Notes")).AppendChild(xml.CreateCDataSection(Notes));
            baseNode.AppendChild(xml.CreateElement("ForumURL")).InnerText = ForumURL;

            baseNode.AppendChild(xml.CreateElement("IsBrawl")).InnerText = IsBrawl.ToString();
            baseNode.AppendChild(xml.CreateElement("Format")).InnerText = Format.ToString();
            baseNode.AppendChild(xml.CreateElement("Strategy")).InnerText = Strategy.ToString();
            baseNode.AppendChild(xml.CreateElement("Crews")).InnerText = Crews.ToString();
            baseNode.AppendChild(xml.CreateElement("Schemes")).InnerText = Schemes.ToString();
            baseNode.AppendChild(xml.CreateElement("SoulStones")).InnerText = Soulstones.ToString();

            baseNode.AppendChild(xml.CreateElement("TotalRounds")).InnerText = TotalRounds.ToString();
            baseNode.AppendChild(xml.CreateElement("RoundTimeLimit")).InnerText = RoundTimeLimit.ToString();
            baseNode.AppendChild(xml.CreateElement("TryToAvoidSameRegionMatches")).InnerText =
                TryToAvoidSameRegionMatches.ToString();
            baseNode.AppendChild(xml.CreateElement("TryToAvoidSameFactionMatches")).InnerText =
                TryToAvoidSameFactionMatches.ToString();
            baseNode.AppendChild(xml.CreateElement("PreEventSent")).InnerText = PreEventSent.ToString();
            baseNode.AppendChild(xml.CreateElement("PostEventSent")).InnerText = PostEventSent.ToString();
            baseNode.AppendChild(xml.CreateElement("TableNumbering")).InnerText = TableNumbering.ToString();

            var playersNode = baseNode.AppendChild(xml.CreateElement("Players"));
            foreach (var player in Players)
            {
                var playerNode = playersNode.AppendChild(xml.CreateElement("Player"));
                playerNode.InnerText = player.Key;
                playerNode.Attributes.Append(xml.CreateAttribute("Number")).Value = player.Value.ToString();
                playerNode.Attributes.Append(xml.CreateAttribute("Faction")).Value = PlayerFaction[player.Key].ToString();
            }

            var roundsNode = baseNode.AppendChild(xml.CreateElement("Rounds"));
            foreach (var round in Rounds)
                roundsNode.AppendChild(round.ToXML(xml));

            var removedPlayersNode = baseNode.AppendChild(xml.CreateElement("Removed"));
            foreach (var id in RemovedPlayers)
                removedPlayersNode.AppendChild(xml.CreateElement("Player")).InnerText = id;

            return baseNode;
        }

        /// <summary>
        /// Add a player to the Player list, assigning a player number if needed.
        /// </summary>
        /// <param name="id">The player's unique ID.</param>
        /// <param name="faction">The player's chosen faction.</param>
        /// <param name="playerNumber">The player number, if known.</param>
        public void AddPlayer(string id, Factions faction, int playerNumber = 0)
        {
            if (Players.ContainsKey(id)) return;
            if (playerNumber == 0)
            {
                playerNumber = Players.Count > 0 ? Players.Values.Max() + 1 : 1;
                while (Players.ContainsValue(playerNumber))
                    playerNumber++;
            }
            Players.Add(id, playerNumber);
            PlayerFaction.Add(id, faction);
        }

        /// <summary>
        /// Generates pairings based on the tournament settings and round being created. The Round will
        /// automatically be added to the Rounds property.
        /// </summary>
        /// <returns>The TournamentRound that was generated.</returns>
        public TournamentRound GenerateNextRound()
        {
            var round = new TournamentRound(Name)
                {
                    Length = RoundTimeLimit,
                    Matches = Rounds.Count == 0 ? GenerateFirstRound() : GenerateFollowUpRound()
                };

            Rounds.Add(round);
            return round;
        }

        private List<TournamentMatch> GenerateFirstRound()
        {
            var records = Players.Keys.Select(id => Config.Settings.GetPlayer(id)).ToList();
            var order = new List<int>();
            for (int index = 1; index <= records.Count; index++)
                order.Add(index);
            RandomizeList(ref order);

            var pairings = order.Select(index => records[index - 1].ID).ToList();

            // If we're trying to avoid matches against people from the same region or faction, we'll need to 
            // go through and randomly swap players until we go through the whole list and satisfy the
            // constraints. If we don't have any matchmaking requirements, this will breeze through without changes.
            for (int index = 0; index < order.Count - 2; index += 2)
            {
                var player1 = Config.Settings.GetPlayer(pairings[index]);
                var player2 = Config.Settings.GetPlayer(pairings[index + 1]);

                var targetID = -1;
                var mustSwap = false;
                if (TryToAvoidSameRegionMatches && player1.Region.Trim().Length != 0 ||
                    player2.Region.Trim().Length != 0)
                    if (player1.Region.ToLower() == player2.Region.ToLower())
                        mustSwap = true;
                if (TryToAvoidSameFactionMatches && PlayerFaction[player1.ID] != Factions.Undeclared ||
                    PlayerFaction[player2.ID] != Factions.Undeclared)
                    if (PlayerFaction[player1.ID] == PlayerFaction[player2.ID])
                        mustSwap = true;
                if (mustSwap) targetID = FindValidSwapTarget(pairings, player1.ID, player2.ID);

                if (targetID != -1)
                {
                    string value = pairings[index];
                    pairings[index] = pairings[targetID];
                    pairings[targetID] = value;
                }
            }

            // Create the pairings and return them.
            return GenerateMatchPairs(pairings);
        }

        private List<TournamentMatch> GenerateFollowUpRound()
        {
            var results = new List<MatchResult>();

            // Only grab players who haven't forfeited yet.
            foreach (string id in PlayerIDs)
            {
                if (RemovedPlayers.Contains(id)) continue;
                var result = GetPlayerResults(id);
                if (!result.Forfeited)
                    results.Add(result);
            }

            // Sort the list based on the appropriate scoring format.
            results.Sort(new MatchComparer(Format));

            // Ensure players have not already played each other.
            for (var index = 0; index < results.Count - 2; index += 2)
            {
                var player1 = Config.Settings.GetPlayer(results[index].PlayerID);
                var player2 = Config.Settings.GetPlayer(results[index + 1].PlayerID);

                var targetID = -1;
                var mustSwap = false;
                foreach (var round in Rounds)
                {
                    if (round.Matches.Any(match => match.Players.Contains(player1.ID) && match.Players.Contains(player2.ID)))
                        mustSwap = true;
                    if (mustSwap) break;
                }

                if (mustSwap) targetID = FindNewPartner(results, player1.ID, player2.ID);

                if (targetID == -1) continue;
                var value = results[index];
                results[index] = results[targetID];
                results[targetID] = value;
            }

            // The TO may want tables to be random and not rank-based.
            if (Config.Settings.AssignTablesRandomly)
            {
                // Shuffle the players randomly based on their pairs.
                var randomOrder = new List<int>();
                for (var index = 0; index < results.Count; index += 2)
                    randomOrder.Add(index);
                RandomizeList(ref randomOrder);

                var newResults = new List<MatchResult>();
                for (var index = 0; index < randomOrder.Count; index += 1)
                {
                    // Skip the Bye round.
                    if (results.Count % 2 == 1 && randomOrder[index] + 1 == results.Count) continue;
                    newResults.Add(results[randomOrder[index]]);
                    newResults.Add(results[randomOrder[index] + 1]);
                }

                // Add the Bye round if present.
                if (results.Count % 2 == 1)
                    newResults.Add(results[results.Count - 1]);

                results = newResults;
            }

            // Generate the list of players in order and return the matches.
            var order = results.Select(result => result.PlayerID).ToList();
            return GenerateMatchPairs(order);
        }

        private List<TournamentMatch> GenerateMatchPairs(List<string> order)
        {
            var matches = new List<TournamentMatch>();

            // If we have an even number of matchups, we're good to go.
            if (order.Count % 2 == 0)
            {
                for (var index = 0; index < order.Count; index += 2)
                {
                    var match = new TournamentMatch(Name);
                    match.Players.Add(order[index]);
                    match.Players.Add(order[index + 1]);
                    matches.Add(match);
                }
            }
            else
            {
                // Find the last player who has not yet had a Bye round. If it's not the last player, move them
                // to the end of the list.
                var index = order.Count - 1;
                while (true)
                {
                    if (!GetPlayerResults(order[index]).Bye) break;
                    index--;
                }
                if (index != order.Count - 1)
                {
                    var toMove = order[index];
                    order.RemoveAt(index);
                    order.Add(toMove);
                }

                // Go through the normal matches.
                for (index = 0; index < order.Count - 1; index += 2)
                {
                    var match = new TournamentMatch(Name);
                    match.Players.Add(order[index]);
                    match.Players.Add(order[index + 1]);
                    matches.Add(match);
                }

                // Add the Bye round match.
                var byeID = order[order.Count - 1];
                var byeMatch = new TournamentMatch(Name);
                byeMatch.Players.Add(byeID);
                byeMatch.Crews.Add(byeID, new PlayerCrew());
                var byeResult = new MatchResult(byeID) { Bye = true };
                byeMatch.Results.Add(byeID, byeResult);
                matches.Add(byeMatch);
            }

            return matches;
        }

        private void RandomizeList(ref List<int> values)
        {
            // We'll randomize the list five times for sufficient noise.
            for (var loop = 1; loop <= 5; loop++)
            {
                var n = values.Count;
                while (n > 1)
                {
                    var index = Config.Random.Next(n);
                    n--;
                    var temp = values[n];
                    values[n] = values[index];
                    values[index] = temp;
                }
            }
        }

        /// <summary>
        /// Find the fromID player a new partner who they are able to play against, based on matchmaking restrictions.
        /// </summary>
        /// <param name="pairings">The List of player IDs as they're currently sorted.</param>
        /// <param name="fromID">The ID of the player being moved.</param>
        /// <param name="partnerID">The ID of the player being moved's partner.</param>
        /// <returns>The index from the values List to swap the player with, or -1 if no match was possible.</returns>
        private int FindValidSwapTarget(List<string> pairings, string fromID, string partnerID)
        {
            PlayerRecord fromPlayer = Config.Settings.GetPlayer(fromID);
            PlayerRecord fromPartner = Config.Settings.GetPlayer(partnerID);
            for (int index = 0; index < pairings.Count - 2; index += 2)
            {
                if (pairings[index] == fromID || pairings[index + 1] == fromID) continue;
                PlayerRecord toPlayerA = Config.Settings.GetPlayer(pairings[index]);
                PlayerRecord toPlayerB = Config.Settings.GetPlayer(pairings[index + 1]);

                // Check to see if the player cannot play either of the target opponents.
                if (TryToAvoidSameFactionMatches && PlayerFaction[fromPlayer.ID] != Factions.Undeclared &&
                    PlayerFaction[fromPlayer.ID] == PlayerFaction[toPlayerA.ID] &&
                    PlayerFaction[fromPlayer.ID] == PlayerFaction[toPlayerB.ID])
                    continue;
                if (TryToAvoidSameRegionMatches && fromPlayer.Region.Trim().Length > 0 &&
                    fromPlayer.Region.ToLower() == toPlayerA.Region.ToLower() &&
                    fromPlayer.Region.ToLower() == toPlayerB.Region.ToLower())
                    continue;

                // Check to see if the player's opponent cannot play either of the target opponents.
                if (TryToAvoidSameFactionMatches && PlayerFaction[fromPartner.ID] != Factions.Undeclared &&
                    PlayerFaction[fromPartner.ID] == PlayerFaction[toPlayerA.ID] &&
                    PlayerFaction[fromPartner.ID] == PlayerFaction[toPlayerB.ID])
                    continue;
                if (TryToAvoidSameRegionMatches && fromPartner.Region.Trim().Length > 0 &&
                    fromPartner.Region.ToLower() == toPlayerA.Region.ToLower() &&
                    fromPartner.Region.ToLower() == toPlayerB.Region.ToLower())
                    continue;

                // First, can we swap the player for Player A?
                bool canMove = true;
                if (TryToAvoidSameFactionMatches)
                    if ((PlayerFaction[fromPlayer.ID] != Factions.Undeclared && PlayerFaction[fromPlayer.ID] == PlayerFaction[toPlayerB.ID]) ||
                        (PlayerFaction[fromPartner.ID] != Factions.Undeclared && PlayerFaction[fromPartner.ID] == PlayerFaction[toPlayerA.ID]))
                        canMove = false;
                if (TryToAvoidSameRegionMatches)
                    if ((fromPlayer.Region.Length != 0 && fromPlayer.Region.ToLower() == toPlayerB.Region.ToLower()) ||
                        (fromPartner.Region.Length != 0 && fromPartner.Region.ToLower() == toPlayerA.Region.ToLower()))
                        canMove = false;
                if (canMove) return index;

                // Swap the player for Player B?
                canMove = true;
                if (TryToAvoidSameFactionMatches)
                    if ((PlayerFaction[fromPlayer.ID] != Factions.Undeclared && PlayerFaction[fromPlayer.ID] == PlayerFaction[toPlayerA.ID]) ||
                        (PlayerFaction[fromPartner.ID] != Factions.Undeclared && PlayerFaction[fromPartner.ID] == PlayerFaction[toPlayerB.ID]))
                        canMove = false;
                if (TryToAvoidSameRegionMatches)
                    if ((fromPlayer.Region.Length != 0 && fromPlayer.Region.ToLower() == toPlayerA.Region.ToLower()) ||
                        (fromPartner.Region.Length != 0 && fromPartner.Region.ToLower() == toPlayerB.Region.ToLower()))
                        canMove = false;
                if (canMove) return index + 1;
            }

            return -1;
        }

        /// <summary>
        /// Find the fromID player a new partner who they have not played yet.
        /// </summary>
        /// <param name="pairings">The List of MatchResult objects containing the current results for the players.</param>
        /// <param name="fromID">The ID of the player being moved.</param>
        /// <param name="partnerID">The ID of the player being moved's partner.</param>
        /// <returns>The index from the values List to swap the player with, or -1 if no match was possible.</returns>
        private int FindNewPartner(List<MatchResult> pairings, string fromID, string partnerID)
        {
            PlayerRecord fromPlayer = Config.Settings.GetPlayer(fromID);
            int foundIndex = -1;
            int playerIndex = -1;
            for (int index = 0; index < pairings.Count - 2; index += 2)
            {
                // We only look down the list past the players.
                if (foundIndex == -1)
                {
                    if (pairings[index].PlayerID == fromID)
                    {
                        foundIndex = index;
                        playerIndex = index;
                    }
                    else if (pairings[index + 1].PlayerID == fromID)
                    {
                        foundIndex = index;
                        playerIndex = index + 1;
                    }
                    continue;
                }

                // First, see if we can swap with the first player.
                if (!HavePlayedEachOther(fromID, pairings[index].PlayerID) &&
                    !HavePlayedEachOther(partnerID, pairings[index].PlayerID))
                    return index;

                // Next, see if we can swap with the second player.
                if (!HavePlayedEachOther(fromID, pairings[index + 1].PlayerID) &&
                    !HavePlayedEachOther(partnerID, pairings[index + 1].PlayerID))
                    return index + 1;
            }

            // If there's a Bye round player, and this player hasn't had a Bye round yet, see if they can be swapped.
            if (pairings.Count % 2 == 1 && !pairings[playerIndex].Bye)
            {
                if (!HavePlayedEachOther(fromID, pairings[pairings.Count - 1].PlayerID) &&
                    !HavePlayedEachOther(partnerID, pairings[pairings.Count - 1].PlayerID))
                    return pairings.Count - 1;
            }

            // If that doesn't work out (usually because it's the last table), look in the other direction.
            for (int index = foundIndex; index >= 0; index -= 2)
            {
                // First, see if we can swap with the first player.
                if (!HavePlayedEachOther(fromID, pairings[index].PlayerID) &&
                    !HavePlayedEachOther(partnerID, pairings[index].PlayerID))
                    return index;

                // Next, see if we can swap with the second player.
                if (!HavePlayedEachOther(fromID, pairings[index + 1].PlayerID) &&
                    !HavePlayedEachOther(partnerID, pairings[index + 1].PlayerID))
                    return index + 1;
            }

            // This is unlikely, but it's POSSIBLE in a small tournament they're stuck with their current seat.
            return -1;
        }

        private bool HavePlayedEachOther(string idA, string idB)
        {
            foreach (TournamentRound round in Rounds)
            {
                foreach (TournamentMatch match in round.Matches)
                    if (match.Players.Contains(idA) && match.Players.Contains(idB))
                        return true;
            }
            return false;
        }

        /// <summary>
        /// Swap two players between their respective matches in a Round.
        /// </summary>
        /// <param name="player1ID">The ID of the first player to swap.</param>
        /// <param name="player2ID">The ID of the second player to swap.</param>
        /// <param name="roundIndex">The 0-based index of the Round to manage. Defaults to the latest round
        /// if not provided.</param>
        /// <returns>True if the swap was successful.</returns>
        public bool SwapPlayers(string player1ID, string player2ID, int roundIndex = -1)
        {
            // Sanity check.
            if (roundIndex == -1)
                roundIndex = Rounds.Count - 1;
            else if (roundIndex < 0 || roundIndex >= Rounds.Count)
                return false;

            // Find the two matches we'll be swapping between.
            int match1Index = -1;
            for (match1Index = 0; match1Index < Rounds[roundIndex].Matches.Count; match1Index++)
                if (Rounds[roundIndex].Matches[match1Index].Players.Contains(player1ID)) break;
            if (match1Index == Rounds[roundIndex].Matches.Count) return false;

            int match2Index = -1;
            for (match2Index = 0; match2Index < Rounds[roundIndex].Matches.Count; match2Index++)
                if (Rounds[roundIndex].Matches[match2Index].Players.Contains(player2ID)) break;
            if (match2Index == Rounds[roundIndex].Matches.Count) return false;

            // Swap the players, clearing any existing results.
            Rounds[roundIndex].Matches[match1Index].Players.Remove(player1ID);
            Rounds[roundIndex].Matches[match1Index].Results.Clear();
            Rounds[roundIndex].Matches[match2Index].Players.Remove(player2ID);
            Rounds[roundIndex].Matches[match2Index].Results.Clear();
            Rounds[roundIndex].Matches[match2Index].Players.Add(player1ID);
            Rounds[roundIndex].Matches[match1Index].Players.Add(player2ID);

            // If either of the matches we swapped into are Bye rounds, make sure to put that result in.
            if (Rounds[roundIndex].Matches[match1Index].Players.Count == 1)
            {
                MatchResult byeResult = new MatchResult(player2ID);
                byeResult.Bye = true;
                Rounds[roundIndex].Matches[match1Index].Results.Add(player2ID, byeResult);
            }
            else if (Rounds[roundIndex].Matches[match2Index].Players.Count == 1)
            {
                MatchResult byeResult = new MatchResult(player1ID);
                byeResult.Bye = true;
                Rounds[roundIndex].Matches[match2Index].Results.Add(player1ID, byeResult);
            }

            return true;
        }

        private class MatchComparer : IComparer<MatchResult>
        {
            private EventFormat Format;

            public MatchComparer(EventFormat format)
            {
                Format = format;
            }

            public int Compare(MatchResult x, MatchResult y)
            {
                switch (Format)
                {
                    case EventFormat.Domination:
                        if (x.TournamentPoints != y.TournamentPoints)
                            return y.TournamentPoints.CompareTo(x.TournamentPoints);
                        else if (x.Differential != y.Differential)
                            return y.Differential.CompareTo(x.Differential);
                        else
                            return y.VictoryPoints.CompareTo(x.VictoryPoints);

                    case EventFormat.Disparity:
                        if (x.Differential != y.Differential)
                            return y.Differential.CompareTo(x.Differential);
                        else if (x.VictoryPoints != y.VictoryPoints)
                            return y.VictoryPoints.CompareTo(x.VictoryPoints);
                        else
                            return y.TournamentPoints.CompareTo(x.TournamentPoints);

                    case EventFormat.Victory:
                        if (x.VictoryPoints != y.VictoryPoints)
                            return y.VictoryPoints.CompareTo(x.VictoryPoints);
                        else if (x.TournamentPoints != y.TournamentPoints)
                            return y.TournamentPoints.CompareTo(x.TournamentPoints);
                        else
                            return y.Differential.CompareTo(x.Differential);

                    default:
                        return 0;
                }
            }
        }

    }

    /// <summary>
    /// Represents a single round in the Tournament.
    /// </summary>
    public class TournamentRound
    {
        /// <summary>
        /// The name of the Tournament this Match is from.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// The Matches for this Tournament Round.
        /// </summary>
        public List<TournamentMatch> Matches { get; set; }
        /// <summary>
        /// The length of the round, in minutes.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// If true, the Round is considered complete for calculation reasons.
        /// </summary>
        public bool Completed { get; private set; }

        /// <summary>
        /// Declares a new TournamentRound instance.
        /// </summary>
        public TournamentRound(string Name)
        {
            Matches = new List<TournamentMatch>();
            Length = 70;
            TournamentName = Name;
        }

        /// <summary>
        /// Load a TournamentRound object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public TournamentRound(XmlNode sourceNode, string Name)
            : this(Name)
        {

            XmlNode node = sourceNode.SelectSingleNode("Length");
            if (node != null)
                Length = Convert.ToInt32(node.InnerText);
            XmlNodeList matches = sourceNode.SelectNodes("Matches/Match");
            Completed = true;
            if (matches != null)
                foreach (XmlNode match in matches)
                {
                    Matches.Add(new TournamentMatch(match, TournamentName));
                    if (Matches[Matches.Count - 1].Results.Count == 0) Completed = false;
                }

        }

        /// <summary>
        /// Go through each of the Matches, calculate their final scores and set the Round as Completed.
        /// </summary>
        public void FinalizeRound()
        {
            foreach (TournamentMatch match in Matches)
                match.CalculateResults();
            Completed = true;
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode roundNode = xml.CreateElement("Round");
            roundNode.AppendChild(xml.CreateElement("Length")).InnerText = Length.ToString();
            XmlNode matchesNode = roundNode.AppendChild(xml.CreateElement("Matches"));
            foreach (TournamentMatch match in Matches)
                matchesNode.AppendChild(match.ToXML(xml));

            return roundNode;
        }
    }

    /// <summary>
    /// Represents a single Match-up between two or more Players in a Tournament Round.
    /// </summary>
    public class TournamentMatch
    {
        /// <summary>
        /// The Player IDs involved in this match.
        /// </summary>
        public List<string> Players { get; set; }
        /// <summary>
        /// The Crews used by the Players in theis match, one record per Player and keyed by Player ID.
        /// </summary>
        public Dictionary<string, PlayerCrew> Crews { get; set; }
        /// <summary>
        /// The results of this Tournament Match, one record per Player and keyed by Player ID.
        /// </summary>
        public Dictionary<string, MatchResult> Results { get; set; }
        /// <summary>
        /// The name of the Tournament this Match is from.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// If true, this Match was a Bye round for a Player.
        /// </summary>
        public bool ByeRound
        {
            get
            {
                if (Results.Count == 1)
                    return (Results[Players[0]].Bye == true);
                else
                    return false;
            }
        }

        /// <summary>
        /// Declares a new TournamentMatch instance.
        /// </summary>
        public TournamentMatch(string Name)
        {
            Players = new List<string>();
            Crews = new Dictionary<string, PlayerCrew>();
            Results = new Dictionary<string, MatchResult>();
            TournamentName = Name;
        }

        /// <summary>
        /// Load a TournamentMatch object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public TournamentMatch(XmlNode sourceNode, string Name)
            : this(Name)
        {
            XmlNodeList nodes = sourceNode.SelectNodes("Players/Player");
            if (nodes != null)
            {
                foreach (XmlNode player in nodes)
                {
                    string id = player.Attributes["ID"].Value;
                    Players.Add(id);
                    XmlNode crew = player.SelectSingleNode("Crew");
                    if (crew != null)
                        Crews.Add(id, new PlayerCrew(crew));
                    XmlNode result = player.SelectSingleNode("Result");
                    if (result != null)
                        Results.Add(id, new MatchResult(result));
                }
            }
        }

        /// <summary>
        /// If there are two Results records, updates them and assigns their Tournament Points and Differential values.
        /// </summary>
        public void CalculateResults()
        {
            if (Results.Count == 2)
            {
                string Player1 = Players[0];
                string Player2 = Players[1];
                int Player1VP = Results[Player1].VictoryPoints;
                int Player2VP = Results[Player2].VictoryPoints;
                Results[Player1].Differential = Player1VP - Player2VP;
                Results[Player2].Differential = Player2VP - Player1VP;
                if (Player1VP == Player2VP)
                {
                    Results[Player1].TournamentPoints = 1;
                    Results[Player2].TournamentPoints = 1;
                }
                else if (Player1VP > Player2VP)
                {
                    Results[Player1].TournamentPoints = 3;
                    Results[Player2].TournamentPoints = 0;
                }
                else
                {
                    Results[Player1].TournamentPoints = 0;
                    Results[Player2].TournamentPoints = 3;
                }
            }
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode matchNode = xml.CreateElement("Match");
            XmlNode playersNode = matchNode.AppendChild(xml.CreateElement("Players"));
            foreach (string id in Players)
            {
                XmlNode playerNode = playersNode.AppendChild(xml.CreateElement("Player"));
                playerNode.Attributes.Append(xml.CreateAttribute("ID")).Value = id.ToString();
                if (Crews.ContainsKey(id))
                    playerNode.AppendChild(Crews[id].ToXML(xml));
                if (Results.ContainsKey(id))
                    playerNode.AppendChild(Results[id].ToXML(xml));
            }

            return matchNode;
        }
    }

    /// <summary>
    /// Represents the results of a single player's tournament match.
    /// </summary>
    public class MatchResult
    {
        /// <summary>
        /// The ID of the player who scored this result.
        /// </summary>
        public string PlayerID { get; set; }
        /// <summary>
        /// The number of Victory Points earned from the Strategy and Schemes.
        /// </summary>
        public int VictoryPoints { get; set; }
        /// <summary>
        /// The number of Tournament Points earned: 3 for a win, 1 for a tie, 0 for a loss.
        /// </summary>
        public int TournamentPoints { get; set; }
        /// <summary>
        /// The difference between this player's Victory Points and their opponent's Victory Points.
        /// </summary>
        public int Differential { get; set; }
        /// <summary>
        /// If true, the player received a Bye for this round. Players may receive no more than one Bye per tournament.
        /// </summary>
        public bool Bye { get; set; }
        /// <summary>
        /// If true, the player forfeited the match and/or tournament.
        /// </summary>
        public bool Forfeited { get; set; }
        /// <summary>
        /// The Date this result was recorded.
        /// </summary>
        public DateTime DatePlayed { get; set; }
        /// <summary>
        /// The Achievements earned by the player, if any.
        /// </summary>
        public List<Achievement> Achievements { get; set; }

        /// <summary>
        /// Declares a new MatchResult instance.
        /// </summary>
        /// <param name="id">The ID of the Player who scored this result.</param>
        public MatchResult(string id)
        {
            PlayerID = id;
            VictoryPoints = 0;
            TournamentPoints = 0;
            Differential = 0;
            Bye = false;
            Forfeited = false;
            DatePlayed = DateTime.MinValue;
            Achievements = new List<Achievement>();
        }

        /// <summary>
        /// Load a MatchResult object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public MatchResult(XmlNode sourceNode)
            : this("")
        {
            XmlAttribute attrib = sourceNode.Attributes["ID"];
            if (attrib != null)
                PlayerID = attrib.Value;
            attrib = sourceNode.Attributes["VP"];
            if (attrib != null)
                VictoryPoints = Convert.ToInt32(attrib.Value);
            attrib = sourceNode.Attributes["TP"];
            if (attrib != null)
                TournamentPoints = Convert.ToInt32(attrib.Value);
            attrib = sourceNode.Attributes["Diff"];
            if (attrib != null)
                Differential = Convert.ToInt32(attrib.Value);
            Bye = (sourceNode.Attributes["Bye"] != null);
            Forfeited = (sourceNode.Attributes["Forfeited"] != null);

            DateTime played = DateTime.MinValue;
            XmlNode node = sourceNode.SelectSingleNode("DatePlayed");
            if (node != null)
                if (DateTime.TryParseExact(node.InnerText, "yyyyMMdd", null,
                    System.Globalization.DateTimeStyles.AssumeLocal, out played))
                    DatePlayed = played;

            XmlNodeList achievements = sourceNode.SelectNodes("Achievements/Achievement");
            foreach (XmlNode achievement in achievements)
                Achievements.Add(new Achievement(achievement));
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode resultNode = xml.CreateElement("Result");
            resultNode.Attributes.Append(xml.CreateAttribute("ID")).Value = PlayerID;
            resultNode.Attributes.Append(xml.CreateAttribute("VP")).Value = VictoryPoints.ToString();
            resultNode.Attributes.Append(xml.CreateAttribute("TP")).Value = TournamentPoints.ToString();
            resultNode.Attributes.Append(xml.CreateAttribute("Diff")).Value = Differential.ToString();
            if (Bye) resultNode.Attributes.Append(xml.CreateAttribute("Bye")).Value = "True";
            if (Forfeited) resultNode.Attributes.Append(xml.CreateAttribute("Forfeited")).Value = "True";
            if (DatePlayed != DateTime.MinValue)
                resultNode.AppendChild(xml.CreateElement("DatePlayed")).InnerText = DatePlayed.ToString("yyyyMMdd");
            if (Achievements.Count > 0)
            {
                XmlNode achievementsNode = resultNode.AppendChild(xml.CreateElement("Achievements"));
                foreach (Achievement achievement in Achievements)
                    achievementsNode.AppendChild(achievement.ToXML(xml));
            }
            return resultNode;
        }
    }

    /// <summary>
    /// Represents the details of a player's Crew for a single tournament match.
    /// </summary>
    public class PlayerCrew
    {
        /// <summary>
        /// The player's chosen Faction.
        /// </summary>
        public Factions Faction { get; set; }
        /// <summary>
        /// The Masters of the crew (one for Scraps, two for Brawls).
        /// </summary>
        public List<string> Masters { get; set; }
        /// <summary>
        /// Any Avatars for the Masters chosen.
        /// </summary>
        public List<string> Avatars { get; set; }
        /// <summary>
        /// Any non-Master units in the crew.
        /// </summary>
        public List<string> Minions { get; set; }
        /// <summary>
        /// The Strategy the player chose or was assigned.
        /// </summary>
        public string Strategy { get; set; }
        /// <summary>
        /// A List of the Schemes the player took.
        /// </summary>
        public List<string> Schemes { get; set; }

        /// <summary>
        /// Declares a new PlayerCrew instance.
        /// </summary>
        public PlayerCrew()
        {
            Faction = Factions.Undeclared;
            Masters = new List<string>();
            Avatars = new List<string>();
            Minions = new List<string>();
            Strategy = "";
            Schemes = new List<string>();
        }

        /// <summary>
        /// Load a PlayerCrew object using the provided XmlNode object.
        /// </summary>
        /// <param name="sourceNode">The XmlNode object containing the data for this instance.</param>
        public PlayerCrew(XmlNode sourceNode)
            : this()
        {
            XmlNode node = sourceNode.SelectSingleNode("Faction");
            if (node != null)
                Faction = (Factions)Enum.Parse(typeof(Factions), node.InnerText);
            XmlNodeList nodes = sourceNode.SelectNodes("Masters");
            if (nodes != null)
                foreach (XmlNode master in nodes)
                    Masters.Add(master.InnerText);
            nodes = sourceNode.SelectNodes("Avatars");
            if (nodes != null)
                foreach (XmlNode avatar in nodes)
                    Avatars.Add(avatar.InnerText);
            nodes = sourceNode.SelectNodes("Minions");
            if (nodes != null)
                foreach (XmlNode minion in nodes)
                    Minions.Add(minion.InnerText);
            node = sourceNode.SelectSingleNode("Strategy");
            if (node != null)
                Strategy = node.InnerText;
            nodes = sourceNode.SelectNodes("Schemes");
            if (nodes != null)
                foreach (XmlNode scheme in nodes)
                    Schemes.Add(scheme.InnerText);
        }

        /// <summary>
        /// Generate a XmlNode object that represents this object for saving to disk.
        /// </summary>
        /// <param name="xml">The XmlDocument object to use for creating the XmlNode.</param>
        /// <returns>An XmlNode object that contains the data for this object.</returns>
        public XmlNode ToXML(XmlDocument xml)
        {
            XmlNode playerNode = xml.CreateElement("Crew");
            playerNode.AppendChild(xml.CreateElement("Faction")).InnerText = Faction.ToString();
            XmlNode mastersNode = playerNode.AppendChild(xml.CreateElement("Masters"));
            foreach (string master in Masters)
                mastersNode.AppendChild(xml.CreateElement("Master")).InnerText = master;
            XmlNode avatarsNode = playerNode.AppendChild(xml.CreateElement("Avatars"));
            foreach (string avatar in Avatars)
                avatarsNode.AppendChild(xml.CreateElement("Avatar")).InnerText = avatar;
            XmlNode minionsNode = playerNode.AppendChild(xml.CreateElement("Minions"));
            foreach (string minion in Minions)
                minionsNode.AppendChild(xml.CreateElement("Minion")).InnerText = minion;
            playerNode.AppendChild(xml.CreateElement("Strategy")).InnerText = Strategy;
            XmlNode schemesNode = playerNode.AppendChild(xml.CreateElement("Schemes"));
            foreach (string scheme in schemesNode)
                schemesNode.AppendChild(xml.CreateElement("Scheme")).InnerText = scheme;

            return playerNode;
        }
    }

    /// <summary>
    /// The Factions available to players.
    /// </summary>
    public enum Factions
    {
        /// <summary>
        /// The player has not declared a faction.
        /// </summary>
        Undeclared = 0,
        /// <summary>
        /// The player is playing the Guild.
        /// </summary>
        Guild = 1,
        /// <summary>
        /// The player is playing the Arcanists.
        /// </summary>
        Arcanists = 2,
        /// <summary>
        /// The player is playing the Resurrectionists.
        /// </summary>
        Resurrectionists = 3,
        /// <summary>
        /// The player is playing the Neverborn.
        /// </summary>
        Neverborn = 4,
        /// <summary>
        /// The player is playing the Outcasts.
        /// </summary>
        Outcasts = 5,
        /// <summary>
        /// The player is playing the Ten Thunders.
        /// </summary>
        TenThunders = 6,
        /// <summary>
        /// The player is playing the Gremlins.
        /// </summary>
        Gremlins = 7
    }

    /// <summary>
    /// The format by which the event is scored.
    /// </summary>
    public enum EventFormat
    {
        /// <summary>
        /// This Event does not score points for the actual matches. (Incompatible with Tournaments.)
        /// </summary>
        None = 0,
        /// <summary>
        /// Tournament Points as the pairing format and/or primary victory condition, 
        /// followed by Victory Point Differential then Total Victory Points.
        /// </summary>
        Domination = 1,
        /// <summary>
        /// Victory Point Differential as the pairing format and/or primary victory condition, 
        /// followed by Total Victory Points then Tournament Points.
        /// </summary>
        Disparity = 2,
        /// <summary>
        /// Total Victory Points as the pairing format and/or primary victory condition, 
        /// followed by Tournament Points then Victory Point Differential.
        /// </summary>
        Victory = 3
    }

    /// <summary>
    /// The Strategy that will be available to the players in the event.
    /// </summary>
    public enum EventStrategy
    {
        /// <summary>
        /// Players flip from the Core Encounter Chart, using the Shared version if they have the same Strategy.
        /// </summary>
        RandomCore = 1,
        /// <summary>
        /// One player flips from the Core Encounter Chart, then both players use the Shared version of the Strategy.
        /// </summary>
        RandomCoreShared = 2,
        /// <summary>
        /// Players flip from the Individual Strategies Chart, using the Shared version if they have the same Strategy.
        /// </summary>
        RandomExpanded = 3,
        /// <summary>
        /// One player flips from the Shared Encounter Chart, then both players use the Shared version of the Strategy.
        /// </summary>
        RandomExpandedShared = 4,
        /// <summary>
        /// Players will use a Shared Strategy declared by the Event Organizer.
        /// </summary>
        FixedShared = 5,
        /// <summary>
        /// Players will be using a Story Encounter.
        /// </summary>
        StoryEncounter = 6
    }

    /// <summary>
    /// The factions and crews available to the players in the event.
    /// </summary>
    public enum EventCrews
    {
        /// <summary>
        /// Players may hire from any Faction they wish at the beginning of each round.
        /// </summary>
        OpenFaction = 1,
        /// <summary>
        /// Players choose a single Faction, then hire their crews at the beginning of each round from that Faction.
        /// </summary>
        SingleFaction = 2,
        /// <summary>
        /// Players choose a single Faction and then provide a list of non-Master models valued equal to the encounter size plus
        /// 20 Soulstones to the Event Organizer in advance, then use that list to hire from during each round. Mercenaries
        /// always cost one additional Soulstone, regardless of master-based discounts.
        /// </summary>
        LimitedFaction = 3,
        /// <summary>
        /// Crews are premade in advance of the Event, and players may choose from any of them available crews.
        /// </summary>
        OpenFixedCrews = 4,
        /// <summary>
        /// Crews are premade in advance of the Event, and players declare a single Faction and then may choose from 
        /// any availables crews of that Faction.
        /// </summary>
        SingleFixedCrews = 5
    }

    /// <summary>
    /// How Schemes may be limited in the Event.
    /// </summary>
    public enum EventSchemes
    {
        /// <summary>
        /// Schemes are considered Unique and cannot be reused throughout the Event.
        /// </summary>
        UniqueSchemes = 1,
        /// <summary>
        /// All Schemes are available without limitations.
        /// </summary>
        AllSchemes = 2,
        /// <summary>
        /// Schemes are fixed in advance by the Event Organizer.
        /// </summary>
        FixedSchemes = 3
    }
}
