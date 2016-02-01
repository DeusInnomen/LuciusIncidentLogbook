using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public struct Tags
    {
        public string rowStart, cellStart, cellEnd, rowEnd, docStart, docEnd, headlineStart, headlineEnd, thStart, thEnd;
    }

    public struct MonospacedValues
    {
        public int rankLength, playerNameLength, factionLength, tpsLength, diffLength, vpsLength, roundLength, tableLength, matchLength;
    }

    public struct Scores
    {
        public int diff, vps, tps;
    }

    public partial class FrmExportTournamentResults : Form
    {
        private string TournamentName { get; set; }

        public FrmExportTournamentResults(string name)
        {
            InitializeComponent();
            TournamentName = name;
            var tournament = Config.Settings.GetTournament(name);
            lblTitle.Text = lblTitle.Text.Replace("%name%", tournament.Name);
        }

        private void btnGetExport_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                if (rdoCSV.Checked)
                {
                    dialog.DefaultExt = "csv";
                    dialog.Filter = "Comma Separated Values Files (*.csv)|*.csv|All Files (*.*)|*.*";
                }
                else if (rdoBBCode.Checked)
                {
                    dialog.DefaultExt = "txt";
                    dialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                }
                else if (rdoHTML.Checked)
                {
                    dialog.DefaultExt = "htm";
                    dialog.Filter = "HTML Files (*.htm)|*.csv|All Files (*.*)|*.*";
                }
                else //if (rdoMonospaced.Checked)
                {
                    dialog.DefaultExt = "txt";
                    dialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                }

                dialog.CheckPathExists = true;
                if (txtFilename.TextLength == 0)
                    dialog.FileName = TournamentName + "." + dialog.DefaultExt;
                else
                    dialog.FileName = txtFilename.Text;
                dialog.OverwritePrompt = true;
                dialog.Title = "Enter the path and name of the file to export to:";
                if (dialog.ShowDialog() == DialogResult.OK)
                    txtFilename.Text = dialog.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CheckForReady(object sender, EventArgs e)
        {
            btnOK.Enabled = (rdoWriteFile.Checked && txtFilename.TextLength > 0) || rdoWriteClipboard.Checked;
            txtFilename.Enabled = rdoWriteFile.Checked;
            btnGetExport.Enabled = rdoWriteFile.Checked;
        }

        private void rdoExportTable_CheckedChanged(object sender, EventArgs e)
        {
            grpOrder.Enabled = rdoExportPlayer.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var tournament = Config.Settings.GetTournament(TournamentName);
            if (rdoCSV.Checked)
            {
                Tags tag;
                tag.rowStart = "";
                tag.rowEnd = "";
                tag.cellEnd = ",";
                tag.cellStart = "";
                tag.docEnd = "";
                tag.docStart = "";
                tag.headlineEnd = "";
                tag.headlineStart = "";
                tag.thEnd = ",";
                tag.thStart = "";
                Export(tournament, tag, txtFilename.Text);
            }
            else if (rdoBBCode.Checked)
            {
                Tags tag;
                tag.rowStart = "[TR]";
                tag.rowEnd = "[/TR]";
                tag.cellEnd = "[/TD]";
                tag.cellStart = "[TD]";
                tag.docEnd = "[/TABLE]";
                tag.docStart = "[TABLE]";
                tag.headlineEnd = "[/SIZE][/B]";
                tag.headlineStart = "[B][SIZE=+1]";
                tag.thEnd = "[/B][/TD]";
                tag.thStart = "[TD][B]";
                Export(tournament, tag, txtFilename.Text);
            }
            else if (rdoHTML.Checked)
            {
                Tags tag;
                tag.rowStart = "<TR>";
                tag.rowEnd = "</TR>";
                tag.cellEnd = "</TD>";
                tag.cellStart = "<TD>";
                tag.docEnd = "</TABLE>";
                tag.docStart = "<TABLE>";
                tag.headlineEnd = "</H2>";
                tag.headlineStart = "<H2>";
                tag.thEnd = "</TH>";
                tag.thStart = "<TH>";
                Export(tournament, tag, txtFilename.Text);
            }
            else if (rdoMonospaced.Checked)
            {
                ExportAsMonospaced(tournament, txtFilename.Text);
            }

            if (rdoWriteClipboard.Checked)
                MessageBox.Show("The data has been written to the clipboard.", "Export Completed", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void ExportAsMonospaced(Tournament tournament, string filename = null)
        {
            string data = "";
            int numSpaces = 80 - tournament.Name.Length;
            string padding = new string(' ', (numSpaces / 2));
            if (chkShowName.Checked) data += padding + tournament.Name + "\r\n";
            data += "\r\n";
            int playerNameLength = 0;

            {
                var players = new List<string>(tournament.Players.Keys);
                foreach (var id in players)
                {
                    var player = Config.Settings.GetPlayer(id);
                    if (player.Name.Length > playerNameLength) playerNameLength = player.Name.Length;
                }
            }

            MonospacedValues monoValues;
            monoValues.rankLength = 5;
            monoValues.tpsLength = 5;
            monoValues.vpsLength = 5;
            monoValues.diffLength = 5;
            monoValues.factionLength = 20;
            monoValues.roundLength = 7;
            monoValues.tableLength = 7;
            monoValues.matchLength = 5;
            monoValues.playerNameLength = playerNameLength + 2;

            string scoreLabels;
            if (tournament.Format == EventFormat.Domination)
                scoreLabels = "TPS  Diff VPs  ";
            else if (tournament.Format == EventFormat.Disparity)
                scoreLabels = "Diff VPs  TPs  ";
            else //if( tournament.Format == EventFormat.Victory)
                scoreLabels = "VPs  TPs  Diff ";

            var factionLabel = "";
            if (chkShowFactions.Checked) factionLabel = "Faction".PadRight(monoValues.factionLength);

            if (rdoExportPlayer.Checked)
            {
                data += "Round".PadRight(monoValues.roundLength) + "Player Name".PadRight(monoValues.playerNameLength)
                    + factionLabel + scoreLabels.PadRight(monoValues.diffLength + monoValues.tpsLength + monoValues.vpsLength)
                    + "Opponent".PadRight(monoValues.playerNameLength) + "\r\n";
            }
            else if (rdoExportTable.Checked)
            {
                data += "Round".PadRight(monoValues.roundLength) + "Table".PadRight(monoValues.tableLength)
                        + "Player 1".PadRight(monoValues.playerNameLength) + factionLabel
                        + scoreLabels.PadRight(monoValues.diffLength + monoValues.tpsLength + monoValues.vpsLength) +
                        "Player 2".PadRight(monoValues.playerNameLength) + factionLabel
                        + scoreLabels.PadRight(monoValues.diffLength + monoValues.tpsLength + monoValues.vpsLength)
                        + "\r\n";
            }

            if (rdoExportResults.Checked)
            {
                data += "Rank".PadRight(monoValues.rankLength)
                     + "Player Name".PadRight(monoValues.playerNameLength) + factionLabel + scoreLabels + "\r\n";
                var results = tournament.PlayerIDs.Select(tournament.GetPlayerResults).ToList();
                sortResults(ref results, tournament.Format);
                formatResults(ref data, results, monoValues, tournament);
            }
            else
            {
                var roundNumber = 1;
                foreach (var round in tournament.Rounds)
                {
                    if (rdoExportTable.Checked)
                    {
                        formatTable(ref data, monoValues, round, tournament, roundNumber);
                    }
                    else
                    {
                        if (!rdoOrderTable.Checked)
                        {
                            var players = new List<string>(tournament.Players.Keys);
                            orderPlayers(ref players);
                            foreach (var id in players)
                            {
                                var player = Config.Settings.GetPlayer(id);
                                foreach (var match in round.Matches.Where(match => match.Players.Contains(player.ID)))
                                {
                                    data += roundNumber.ToString().PadRight(monoValues.roundLength)
                                        + player.Name.PadRight(monoValues.playerNameLength);
                                    if (chkShowFactions.Checked) data += tournament.PlayerFaction[player.ID].ToString().PadRight(monoValues.factionLength);
                                    Scores scores;
                                    scores.diff = match.Results[id].Differential;
                                    scores.tps = match.Results[id].TournamentPoints;
                                    scores.vps = match.Results[id].VictoryPoints;
                                    formatScores(ref data, ref monoValues, tournament.Format, scores);

                                    if (match.Players.Count > 1)
                                    {
                                        var id2 = match.Players[0] == id ? match.Players[1] : match.Players[0];
                                        var player2 = Config.Settings.GetPlayer(id2);
                                        data += player2.Name.PadRight(monoValues.playerNameLength);
                                    }
                                    else
                                        data += "BYE ROUND".PadRight(monoValues.playerNameLength);
                                    data += "\r\n";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var match in round.Matches)
                            {
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                var player2 = match.Players.Count > 1
                                                  ? Config.Settings.GetPlayer(match.Players[1])
                                                  : null;
                                data += roundNumber.ToString().PadRight(monoValues.roundLength) + player1.Name.PadRight(monoValues.playerNameLength);
                                if (chkShowFactions.Checked) data += (tournament.PlayerFaction[player1.ID]).ToString().PadRight(monoValues.factionLength);
                                Scores scores;
                                scores.diff = match.Results[player1.ID].Differential;
                                scores.tps = match.Results[player1.ID].TournamentPoints;
                                scores.vps = match.Results[player1.ID].VictoryPoints;
                                formatScores(ref data, ref monoValues, tournament.Format, scores);
                                if (player2 != null)
                                {
                                    data += player2.Name.PadRight(monoValues.playerNameLength);
                                    if (chkShowFactions.Checked) data += (tournament.PlayerFaction[player2.ID]).ToString().PadRight(monoValues.factionLength);
                                    scores.diff = match.Results[player2.ID].Differential;
                                    scores.tps = match.Results[player2.ID].TournamentPoints;
                                    scores.vps = match.Results[player2.ID].VictoryPoints;
                                    formatScores(ref data, ref monoValues, tournament.Format, scores);
                                }
                                else
                                    data += "BYE ROUND";
                                data += "\r\n";
                            }
                        }
                    }
                    roundNumber++;
                }
            }
            data += "\r\n";

            if (rdoWriteFile.Checked)
                using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write,
                                                                FileShare.None)))
                    sw.Write(data);
            else
                Clipboard.SetText(data, TextDataFormat.Text);
        }

        private void Export(Tournament tournament, Tags tag, string filename = null)
        {
            string data = "";
            if (chkShowName.Checked) data += tag.headlineStart + tournament.Name + tag.headlineEnd + "\r\n";
            data += tag.docStart + "\r\n";

            string scoreLabels;
            if (tournament.Format == EventFormat.Domination)
                scoreLabels = tag.thStart + "TPs" + tag.thEnd + tag.thStart + "Diff" + tag.thEnd + tag.thStart 
                    + "VPs" + tag.thEnd;
            else if (tournament.Format == EventFormat.Disparity)
                scoreLabels = tag.thStart + "Diff" + tag.thEnd + tag.thStart + "VPs" + tag.thEnd + tag.thStart
                    + "TPs" + tag.thEnd;
            else //if( tournament.Format == EventFormat.Victory)
                scoreLabels = tag.thStart + "VPs" + tag.thEnd + tag.thStart + "TPs" + tag.thEnd + tag.thStart 
                    + "Diff" + tag.thEnd;

            var factionLabel = "";
            if (chkShowFactions.Checked) factionLabel = tag.thStart + "Faction" + tag.thEnd;

            if (rdoExportPlayer.Checked)
                data += tag.rowStart + tag.thStart + "Round" + tag.thEnd + tag.thStart
                    + "Player Name" + tag.thEnd + factionLabel + scoreLabels + tag.thStart + "Opponent" + tag.thEnd 
                    + tag.rowEnd + "\r\n";
            else if (rdoExportTable.Checked)
                data += tag.rowStart + tag.thStart + "Round" + tag.thEnd + tag.thStart + "Table" + tag.thEnd
                    + tag.thStart + "Player 1" + tag.thEnd + factionLabel + scoreLabels +
                        tag.thStart + "Player 2" + tag.thEnd + factionLabel + scoreLabels + tag.thEnd + tag.rowEnd + "\r\n";
            else
                data += tag.rowStart + tag.thStart + "Rank" + tag.thEnd + tag.thStart + "Player Name" + tag.thEnd
                    + factionLabel + scoreLabels + tag.rowEnd + "\r\n";

            if (rdoExportResults.Checked)
            {
                var results = tournament.PlayerIDs.Select(tournament.GetPlayerResults).ToList();
                sortResults(ref results, tournament.Format);

                formatResults(ref data, results, tag, tournament);
            }
            else
            {
                var roundNumber = 1;
                foreach (var round in tournament.Rounds)
                {
                    if (rdoExportTable.Checked)
                    {
                        formatTable(ref data, tag, round, tournament, roundNumber);
                    }
                    else
                    {
                        if (!rdoOrderTable.Checked)
                        {
                            var players = new List<string>(tournament.Players.Keys);
                            orderPlayers(ref players);
                            foreach (var id in players)
                            {
                                var player = Config.Settings.GetPlayer(id);
                                foreach (var match in round.Matches.Where(match => match.Players.Contains(player.ID)))
                                {
                                    data += tag.rowStart + tag.cellStart + roundNumber + tag.cellEnd + tag.cellStart 
                                        + player.Name + tag.cellEnd;
                                    if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player.ID] + tag.cellEnd;
                                    Scores scores;
                                    scores.diff = match.Results[id].Differential;
                                    scores.tps = match.Results[id].TournamentPoints;
                                    scores.vps = match.Results[id].VictoryPoints;
                                    formatScores(ref data, ref tag, tournament.Format, scores);

                                    if (match.Players.Count > 1)
                                    {
                                        var id2 = match.Players[0] == id ? match.Players[1] : match.Players[0];
                                        var player2 = Config.Settings.GetPlayer(id2);
                                        data += tag.cellStart + player2.Name + tag.cellEnd;
                                    }
                                    else
                                        data += tag.cellStart + "BYE ROUND" + tag.cellEnd;
                                    data += tag.rowEnd + "\r\n";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var match in round.Matches)
                            {
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                var player2 = match.Players.Count > 1
                                                  ? Config.Settings.GetPlayer(match.Players[1])
                                                  : null;
                                data += tag.rowStart + tag.cellStart + roundNumber + tag.cellEnd + tag.cellStart 
                                    + player1.Name + tag.cellEnd;
                                if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player1.ID] 
                                    + tag.cellEnd;

                                Scores scores;
                                scores.diff = match.Results[player1.ID].Differential;
                                scores.tps = match.Results[player1.ID].TournamentPoints;
                                scores.vps = match.Results[player1.ID].VictoryPoints;
                                formatScores(ref data, ref tag, tournament.Format, scores);

                                if (player2 != null)
                                {
                                    data += tag.cellStart + player2.Name + tag.cellEnd;
                                    if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player2.ID] 
                                        + tag.cellEnd;
                                    scores.diff = match.Results[player2.ID].Differential;
                                    scores.tps = match.Results[player2.ID].TournamentPoints;
                                    scores.vps = match.Results[player2.ID].VictoryPoints;
                                    formatScores(ref data, ref tag, tournament.Format, scores);

                                }
                                else
                                    data += tag.cellStart + "BYE ROUND" + tag.cellEnd;
                                data += tag.rowEnd + "\r\n";
                            }
                        }
                    }
                    roundNumber++;
                }
            }
            data += tag.docEnd + "\r\n";

            if (rdoWriteFile.Checked)
                using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write,
                                                                FileShare.None)))
                    sw.Write(data);
            else
                Clipboard.SetText(data, TextDataFormat.Text);
        }

        private void orderPlayers(ref List<string> players)
        {
            if (rdoOrderLast.Checked)
                players.Sort((a, b) =>
                {
                    var player1 = Config.Settings.GetPlayer(a);
                    var player2 = Config.Settings.GetPlayer(b);
                    return player1.LastName != player2.LastName
                               ? string.Compare(player1.LastName, player2.LastName)
                               : string.Compare(player1.FirstName, player2.FirstName);
                });
            else
                players.Sort((a, b) =>
                {
                    var player1 = Config.Settings.GetPlayer(a);
                    var player2 = Config.Settings.GetPlayer(b);
                    return player1.FirstName != player2.FirstName
                               ? string.Compare(player1.FirstName, player2.FirstName)
                               : string.Compare(player1.LastName, player2.LastName);
                });
        }

        void formatTable(ref string data, Tags tag, TournamentRound round, Tournament tournament, int roundNumber)
        {
            var matchNumber = 1;
            foreach (var match in round.Matches)
            {
                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                data += tag.rowStart + tag.cellStart + roundNumber + tag.cellEnd + tag.cellStart + matchNumber++ + tag.cellEnd + tag.cellStart + player1.Name +
                        tag.cellEnd;
                if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player1.ID] + tag.cellEnd;

                Scores scores;
                scores.vps = match.Results[player1.ID].VictoryPoints;
                scores.tps = match.Results[player1.ID].TournamentPoints;
                scores.diff = match.Results[player1.ID].Differential;
                formatScores(ref data, ref tag, tournament.Format, scores);

                if (player2 != null)
                {
                    data += tag.cellStart + player2.Name + tag.cellEnd;
                    if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player2.ID] + tag.cellEnd;
                    scores.vps = match.Results[player2.ID].VictoryPoints;
                    scores.tps = match.Results[player2.ID].TournamentPoints;
                    scores.diff = match.Results[player2.ID].Differential;
                    formatScores(ref data, ref tag, tournament.Format, scores);
                }
                else
                    data += (tag.cellStart + "BYE ROUND" + tag.cellEnd + tag.cellStart + tag.cellEnd + tag.cellStart + tag.cellEnd + tag.cellStart + tag.cellEnd);
                data += tag.rowEnd + "\r\n";
            }
        }

        private void formatTable(ref string data, MonospacedValues monoValues, TournamentRound round, Tournament tournament, int roundNumber)
        {
            var matchNumber = 1;
            foreach (var match in round.Matches)
            {
                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                data += roundNumber.ToString().PadRight(monoValues.roundLength)
                    + matchNumber++.ToString().PadRight(monoValues.tableLength)
                    + player1.Name.PadRight(monoValues.playerNameLength);
                if (chkShowFactions.Checked) data += tournament.PlayerFaction[player1.ID].ToString().PadRight(monoValues.factionLength);

                Scores scores;
                scores.vps = match.Results[player1.ID].VictoryPoints;
                scores.tps = match.Results[player1.ID].TournamentPoints;
                scores.diff = match.Results[player1.ID].Differential;
                formatScores(ref data, ref monoValues, tournament.Format, scores);

                if (player2 != null)
                {
                    data += player2.Name.PadRight(monoValues.playerNameLength);
                    if (chkShowFactions.Checked) data += tournament.PlayerFaction[player2.ID].ToString().PadRight(monoValues.factionLength);
                    scores.vps = match.Results[player2.ID].VictoryPoints;
                    scores.tps = match.Results[player2.ID].TournamentPoints;
                    scores.diff = match.Results[player2.ID].Differential;
                    formatScores(ref data, ref monoValues, tournament.Format, scores);
                }
                else
                    data += ("BYE ROUND");
                data += "\r\n";
            }
        }

        private void formatScores(ref string data, ref Tags tag, EventFormat eventFormat, Scores scores)
        {
            if (eventFormat == EventFormat.Domination)
                data += tag.cellStart + scores.tps + tag.cellEnd + tag.cellStart +
                        scores.diff + tag.cellEnd + tag.cellStart +
                        scores.vps + tag.cellEnd;
            else if (eventFormat == EventFormat.Disparity)
                data += tag.cellStart + scores.diff + tag.cellEnd + tag.cellStart +
                        scores.vps + tag.cellEnd + tag.cellStart +
                        scores.tps + tag.cellEnd;
            else //if(tournament.Format == EventFormat.Victory)
                data += tag.cellStart + scores.vps + tag.cellEnd + tag.cellStart +
                        scores.tps + tag.cellEnd + tag.cellStart +
                        scores.diff + tag.cellEnd;
        }

        private void formatScores(ref string data, ref MonospacedValues monoValues, EventFormat eventFormat, Scores scores)
        {
            if (eventFormat == EventFormat.Domination)
                data += scores.tps.ToString().PadRight(monoValues.tpsLength) +
                        scores.diff.ToString().PadRight(monoValues.diffLength) +
                        scores.vps.ToString().PadRight(monoValues.vpsLength);
            else if (eventFormat == EventFormat.Disparity)
                data += scores.diff.ToString().PadRight(monoValues.diffLength) +
                        scores.vps.ToString().PadRight(monoValues.vpsLength) +
                        scores.tps.ToString().PadRight(monoValues.tpsLength);
            else //if(tournament.Format == EventFormat.Victory)
                data += scores.vps.ToString().PadRight(monoValues.vpsLength) +
                        scores.tps.ToString().PadRight(monoValues.tpsLength) +
                        scores.diff.ToString().PadRight(monoValues.diffLength);
        }

        // Formats the results. Sorry Chris, couldn't think of a more descriptive comment.
        // I suppose I could go into what the arguments are. But then again, I could just leave it be.
        void formatResults(ref string data, List<MatchResult> results, Tags tag, Tournament tournament)
        {
            var rank = 1;
            foreach (var result in results)
            {
                if (chkExcludeForfeits.Checked && result.Forfeited) continue;
                var player = Config.Settings.GetPlayer(result.PlayerID);
                var rankString = result.Forfeited ? "F" : rank++.ToString();
                data += tag.rowStart + tag.cellStart + rankString + tag.cellEnd + tag.cellStart + player.Name + tag.cellEnd;
                if (chkShowFactions.Checked) data += tag.cellStart + tournament.PlayerFaction[player.ID] + tag.cellEnd;
                if (tournament.Format == EventFormat.Domination)
                    data += tag.cellStart + result.TournamentPoints + tag.cellEnd + tag.cellStart +
                            result.Differential + tag.cellEnd + tag.cellStart +
                            result.VictoryPoints + tag.cellEnd;
                else if (tournament.Format == EventFormat.Disparity)
                    data += tag.cellStart + result.Differential + tag.cellEnd + tag.cellStart +
                            result.VictoryPoints + tag.cellEnd + tag.cellStart +
                            result.TournamentPoints + tag.cellEnd;
                else //if(tournament.Format == EventFormat.Victory)
                    data += tag.cellStart + result.VictoryPoints + tag.cellEnd + tag.cellStart +
                            result.TournamentPoints + tag.cellEnd +  tag.cellStart +
                            result.Differential + tag.cellEnd;
                data += tag.rowEnd + "\r\n";
            }
        }

        // This will format results for monospaced results. Probably didn't need to be a function by itself since it is
        // only one use, but remains so that the ExportAs* functions all maintain the same logic.
        void formatResults(ref string data, List<MatchResult> results, MonospacedValues monoValues, Tournament tournament)
        {
            var rank = 1;
            foreach (var result in results)
            {
                if (chkExcludeForfeits.Checked && result.Forfeited) continue;
                var player = Config.Settings.GetPlayer(result.PlayerID);
                var rankString = result.Forfeited ? "F" : rank++.ToString();
                data += rankString.PadRight(monoValues.rankLength) + player.Name.PadRight(monoValues.playerNameLength);
                if (chkShowFactions.Checked) data += (tournament.PlayerFaction[player.ID]).ToString().PadRight(monoValues.factionLength);
                if (tournament.Format == EventFormat.Domination)
                    data += result.TournamentPoints.ToString().PadRight(monoValues.tpsLength) +
                            result.Differential.ToString().PadRight(monoValues.diffLength) +
                            result.VictoryPoints.ToString().PadRight(monoValues.vpsLength);
                else if (tournament.Format == EventFormat.Disparity)
                    data += result.Differential.ToString().PadRight(monoValues.diffLength) +
                            result.VictoryPoints.ToString().PadRight(monoValues.vpsLength) +
                            result.TournamentPoints.ToString().PadRight(monoValues.tpsLength);
                else //if(tournament.Format == EventFormat.Victory)
                    data += result.VictoryPoints.ToString().PadRight(monoValues.vpsLength) +
                            result.TournamentPoints.ToString().PadRight(monoValues.tpsLength) +
                            result.Differential.ToString().PadRight(monoValues.diffLength);
                data += "\r\n";
            }
        }

        static void sortResults(ref List<MatchResult> results, EventFormat eventFormat)
        {
            results.Sort((a, b) =>
            {
                var playerA = Config.Settings.GetPlayer(a.PlayerID);
                var playerB = Config.Settings.GetPlayer(b.PlayerID);

                if (a.Forfeited && b.Forfeited)
                    return playerA.Name.CompareTo(playerB.Name);
                if (a.Forfeited)
                    return 1;
                if (b.Forfeited)
                    return -1;

                switch (eventFormat)
                {
                    case EventFormat.Domination:
                        if (a.TournamentPoints != b.TournamentPoints)
                            return b.TournamentPoints.CompareTo(a.TournamentPoints);
                        return a.Differential != b.Differential ?
                            b.Differential.CompareTo(a.Differential) :
                            b.VictoryPoints.CompareTo(a.VictoryPoints);

                    case EventFormat.Disparity:
                        if (a.Differential != b.Differential)
                            return b.Differential.CompareTo(a.Differential);
                        return a.VictoryPoints != b.VictoryPoints ?
                            b.VictoryPoints.CompareTo(a.VictoryPoints) :
                            b.TournamentPoints.CompareTo(a.TournamentPoints);

                    case EventFormat.Victory:
                        if (a.VictoryPoints != b.VictoryPoints)
                            return b.VictoryPoints.CompareTo(a.VictoryPoints);
                        return a.TournamentPoints != b.TournamentPoints ?
                            b.TournamentPoints.CompareTo(a.TournamentPoints) :
                            b.Differential.CompareTo(a.Differential);
                }

                // Worst case, compare the names.
                return playerA.Name.CompareTo(playerB.Name);

            });
        }
        private void rdoExportResults_CheckedChanged(object sender, EventArgs e)
        {
            grpOrder.Enabled = !rdoExportResults.Checked;
        }

        private void rdoCSV_CheckedChanged(object sender, EventArgs e)
        {
            chkShowName.Enabled = !rdoCSV.Checked;
        }
    }
}