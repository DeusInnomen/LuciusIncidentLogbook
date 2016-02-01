using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class FrmExportLeagueResults : Form
    {
        private string TournamentName { get; set; }

        public FrmExportLeagueResults(string name)
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
                else //if (rdoHTML.Checked)
                {
                    dialog.DefaultExt = "htm";
                    dialog.Filter = "HTML Files (*.htm)|*.csv|All Files (*.*)|*.*";
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
                ExportAsCSV(tournament, txtFilename.Text);
            else if (rdoBBCode.Checked)
                ExportAsBbCode(tournament, txtFilename.Text);
            else //if (rdoHTML.Checked)
                ExportAsHTML(tournament, txtFilename.Text);

            if (rdoWriteClipboard.Checked)
                MessageBox.Show("The data has been written to the clipboard.", "Export Completed", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        private void ExportAsHTML(Tournament tournament, string filename = null)
        {
            string data = "";
            if (chkShowName.Checked) data += "<h2>" + tournament.Name + "</h2>\r\n";
            data += "<table>\r\n";

            string scoreLabels;
            if (tournament.Format == EventFormat.Domination)
                scoreLabels = "<th>TPs</th><th>Diff</th><th>VPs</th>";
            else if (tournament.Format == EventFormat.Disparity)
                scoreLabels = "<th>Diff</th><th>VPs</th><th>TPs</th>";
            else //if( tournament.Format == EventFormat.Victory)
                scoreLabels = "<th>VPs</th><th>TPs</th><th>Diff</th>";

            var factionLabel = "";
            if (chkShowFactions.Checked) factionLabel = "<th>Faction</th>";

            if (rdoExportPlayer.Checked)
                data += "<tr><th>Round</th><th>Player Name</th>" +factionLabel + scoreLabels + "<th>Opponent</th></tr>\r\n";
            else if (rdoExportTable.Checked)
                data += "<tr><th>Round</th><th>Table</th><th>Player 1</th>" + factionLabel + scoreLabels +
                        "<th>Player 2</th><th>" + factionLabel + scoreLabels + "</th></tr>\r\n";
            else
                data += "<tr><th>Rank</th><th>Player Name</th>" + factionLabel + scoreLabels + "</tr>\r\n";

            if (rdoExportResults.Checked)
            {
                var results = tournament.PlayerIDs.Select(tournament.GetPlayerResults).ToList();
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

                        switch (tournament.Format)
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
                var rank = 1;
                foreach (var result in results)
                {
                    if (chkExcludeForfeits.Checked && result.Forfeited) continue;
                    var player = Config.Settings.GetPlayer(result.PlayerID);
                    var rankString = result.Forfeited ? "F" : rank++.ToString();
                    data += "<tr><td>" + rankString + "</td><td>" + player.Name + "</td>";
                    if (chkShowFactions.Checked) data += "<td>" + player.Faction + "</td>";
                    if (tournament.Format == EventFormat.Domination)
                        data += "<td>" + result.TournamentPoints + "</td><td>" +
                                result.Differential + "</td><td>" +
                                result.VictoryPoints + "</td>";
                    else if (tournament.Format == EventFormat.Disparity)
                        data += "<td>" + result.Differential + "</td><td>" +
                                result.VictoryPoints + "</td><td>" +
                                result.TournamentPoints + "</td>";
                    else //if(tournament.Format == EventFormat.Victory)
                        data += "<td>" + result.VictoryPoints + "</td><td>" +
                                result.TournamentPoints + "</td><td>" +
                                result.Differential + "</td>";
                    data += "</tr>\r\n";
                }
            }
            else
            {
                var roundNumber = 1;
                foreach (var round in tournament.Rounds)
                {
                    if (rdoExportTable.Checked)
                    {
                        var matchNumber = 1;
                        foreach (var match in round.Matches)
                        {
                            var player1 = Config.Settings.GetPlayer(match.Players[0]);
                            var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                            data += "<tr><td>" + roundNumber + "</td><td>" + matchNumber++ + "</td><td>" + player1.Name +
                                    "</td>";
                            if (chkShowFactions.Checked) data += "<td>" + player1.Faction + "</td>";
                            if (tournament.Format == EventFormat.Domination)
                                data += "<td>" + match.Results[player1.ID].TournamentPoints + "</td><td>" +
                                        match.Results[player1.ID].Differential + "</td><td>" +
                                        match.Results[player1.ID].VictoryPoints + "</td>";
                            else if (tournament.Format == EventFormat.Disparity)
                                data += "<td>" + match.Results[player1.ID].Differential + "</td><td>" +
                                        match.Results[player1.ID].VictoryPoints + "</td><td>" +
                                        match.Results[player1.ID].TournamentPoints + "</td>";
                            else //if(tournament.Format == EventFormat.Victory)
                                data += "<td>" + match.Results[player1.ID].VictoryPoints + "</td><td>" +
                                        match.Results[player1.ID].TournamentPoints + "</td><td>" +
                                        match.Results[player1.ID].Differential + "</td>";
                            if (player2 != null)
                            {
                                data += ("<td>" + player2.Name + "</td>");
                                if (chkShowFactions.Checked) data += "<td>" + player2.Faction + "</td>";
                                if (tournament.Format == EventFormat.Domination)
                                    data += "<td>" + match.Results[player2.ID].TournamentPoints + "</td><td>" +
                                            match.Results[player2.ID].Differential + "</td><td>" +
                                            match.Results[player2.ID].VictoryPoints + "</td>";
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += "<td>" + match.Results[player2.ID].Differential + "</td><td>" +
                                            match.Results[player2.ID].VictoryPoints + "</td><td>" +
                                            match.Results[player2.ID].TournamentPoints + "</td>";
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += "<td>" + match.Results[player2.ID].VictoryPoints + "</td><td>" +
                                            match.Results[player2.ID].TournamentPoints + "</td><td>" +
                                            match.Results[player2.ID].Differential + "</td>";
                            }
                            else
                                data += ("<td>BYE ROUND</td><td></td><td></td><td></td>");
                            data += "</tr>\r\n";
                        }
                    }
                    else
                    {
                        if (!rdoOrderTable.Checked)
                        {
                            var players = new List<string>(tournament.Players.Keys);
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
                            foreach (var id in players)
                            {
                                var player = Config.Settings.GetPlayer(id);
                                foreach (var match in round.Matches.Where(match => match.Players.Contains(player.ID)))
                                {
                                    data += "<tr><td>" + roundNumber + "</td><td>" + player.Name + "</td>";
                                    if (chkShowFactions.Checked) data += "<td>" + player.Faction + "</td>";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += "<td>" + match.Results[id].TournamentPoints + "</td><td>" +
                                                match.Results[id].Differential + "</td><td>" +
                                                match.Results[id].VictoryPoints + "</td>";
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += "<td>" + match.Results[id].Differential + "</td><td>" +
                                                match.Results[id].VictoryPoints + "</td><td>" +
                                                match.Results[id].TournamentPoints + "</td>";
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += "<td>" + match.Results[id].VictoryPoints + "</td><td>" +
                                                match.Results[id].TournamentPoints + "</td><td>" +
                                                match.Results[id].Differential + "</td>";
                                    if (match.Players.Count > 1)
                                    {
                                        var id2 = match.Players[0] == id ? match.Players[1] : match.Players[0];
                                        var player2 = Config.Settings.GetPlayer(id2);
                                        data += "<td>" + player2.Name + "</td>";
                                    }
                                    else
                                        data += "<td>BYE ROUND</td>";
                                    data += "</tr>\r\n";
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
                                data += "<tr><td>" + roundNumber + "</td><td>" + player1.Name + "</td>";
                                if (chkShowFactions.Checked) data += "<td>" + player1.Faction + "</td>";
                                if (tournament.Format == EventFormat.Domination)
                                    data += "<td>" + match.Results[player1.ID].TournamentPoints + "</td><td>" +
                                            match.Results[player1.ID].Differential + "</td><td>" +
                                            match.Results[player1.ID].VictoryPoints + "</td>";
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += "<td>" + match.Results[player1.ID].Differential + "</td><td>" +
                                            match.Results[player1.ID].VictoryPoints + "</td><td>" +
                                            match.Results[player1.ID].TournamentPoints + "</td>";
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += "<td>" + match.Results[player1.ID].VictoryPoints + "</td><td>" +
                                            match.Results[player1.ID].TournamentPoints + "</td><td>" +
                                            match.Results[player1.ID].Differential + "</td>";
                                if (player2 != null)
                                {
                                    data += "<td>" + player2.Name + "</td>";
                                    if (chkShowFactions.Checked) data += "<td>" + player2.Faction + "</td>";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += "<td>" + match.Results[player2.ID].TournamentPoints + "</td><td>" +
                                                match.Results[player2.ID].Differential + "</td><td>" +
                                                match.Results[player2.ID].VictoryPoints + "</td>";
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += "<td>" + match.Results[player2.ID].Differential + "</td><td>" +
                                                match.Results[player2.ID].VictoryPoints + "</td><td>" +
                                                match.Results[player2.ID].TournamentPoints + "</td>";
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += "<td>" + match.Results[player2.ID].VictoryPoints + "</td><td>" +
                                                match.Results[player2.ID].TournamentPoints + "</td><td>" +
                                                match.Results[player2.ID].Differential + "</td>";
                                }
                                else
                                    data += "<td>BYE ROUND</td>";
                                data += "</tr>\r\n";
                            }
                        }
                    }
                    roundNumber++;
                }
            }
            data += "</table>\r\n";

            if (rdoWriteFile.Checked)
                using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write,
                                                                FileShare.None)))
                    sw.Write(data);
            else
                Clipboard.SetText(data, TextDataFormat.Text);
        }

        private void ExportAsBbCode(Tournament tournament, string filename = null)
        {
            string data = "";
            if (chkShowName.Checked) data += "[B][SIZE=+1]" + tournament.Name + "[/SIZE][/B]\r\n\r\n";
            data += "[TABLE]\r\n";

            string scoreLabels;
            if (tournament.Format == EventFormat.Domination)
                scoreLabels = "[TD][B]TPs[/B][/TD][TD][B]Diff[/B][/TD][TD][B]VPs[/B][/TD]";
            else if (tournament.Format == EventFormat.Disparity)
                scoreLabels = "[TD][B]Diff[/B][/TD][TD][B]VPs[/B][/TD][TD][B]TPs[/B][/TD]";
            else //if( tournament.Format == EventFormat.Victory)
                scoreLabels = "[TD][B]VPs[/B][/TD][TD][B]TPs[/B][/TD][TD][B]Diff[/B][/TD]";

            var factionLabel = "";
            if (chkShowFactions.Checked) factionLabel = "[TD][B]Faction[/B][/TD]";

            if (rdoExportPlayer.Checked)
                data += "[TR][TD][B]Round[/B][/TD][TD][B]Player[/B][/TD]" + factionLabel + scoreLabels +
                        "[TD][B]Opponent[/B][/TD][/TR]\r\n";
            else if (rdoExportTable.Checked)
                data += "[TR][TD][B]Round[/B][/TD][TD][B]Table[/B][/TD][TD][B]Player 1[/B][/TD]" + factionLabel + scoreLabels +
                        "[TD][B]Player 2[/B][/TD]" + factionLabel + scoreLabels + "[/TR]\r\n";
            else
                data += "[TR][TD][B]Rank[/B][/TD][TD][B]Player Name[/B][/TD]" + factionLabel + scoreLabels + "[/TR]\r\n";

            if (rdoExportResults.Checked)
            {
                var results = tournament.PlayerIDs.Select(tournament.GetPlayerResults).ToList();
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

                    switch (tournament.Format)
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
                var rank = 1;
                foreach (var result in results)
                {
                    if (chkExcludeForfeits.Checked && result.Forfeited) continue;
                    var player = Config.Settings.GetPlayer(result.PlayerID);
                    var rankString = result.Forfeited ? "F" : rank++.ToString();
                    data += "[TR][TD]" + rankString + "[/TD][TD]" + player.Name + "[/TD]";
                    if (chkShowFactions.Checked) data += "[TD]" + player.Faction + "[/TD]";
                    if (tournament.Format == EventFormat.Domination)
                        data += "[TD]" + result.TournamentPoints + "[/TD][TD]" +
                                result.Differential + "[/TD][TD]" +
                                result.VictoryPoints + "[/TD]";
                    else if (tournament.Format == EventFormat.Disparity)
                        data += "[TD]" + result.Differential + "[/TD][TD]" +
                                result.VictoryPoints + "[/TD][TD]" +
                                result.TournamentPoints + "[/TD]";
                    else //if(tournament.Format == EventFormat.Victory)
                        data += "[TD]" + result.VictoryPoints + "[/TD][TD]" +
                                result.TournamentPoints + "[/TD][TD]" +
                                result.Differential + "[/TD]";
                    data += "[/TR]\r\n";
                }
            }
            else
            {
                var roundNumber = 1;
                foreach (var round in tournament.Rounds)
                {
                    if (rdoExportTable.Checked)
                    {
                        var matchNumber = 1;
                        foreach (var match in round.Matches)
                        {
                            var player1 = Config.Settings.GetPlayer(match.Players[0]);
                            var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                            data += "[TR][TD]" + roundNumber + "[/TD][TD]" + matchNumber++ + "[/TD][TD]" + player1.Name +
                                    "[/TD]";
                            if (chkShowFactions.Checked) data += "[TD]" + player1.Faction + "[/TD]";
                            if (tournament.Format == EventFormat.Domination)
                                data += "[TD]" + match.Results[player1.ID].TournamentPoints + "[/TD][TD]" +
                                        match.Results[player1.ID].Differential + "[/TD][TD]" +
                                        match.Results[player1.ID].VictoryPoints + "[/TD]";
                            else if (tournament.Format == EventFormat.Disparity)
                                data += "[TD]" + match.Results[player1.ID].Differential + "[/TD][TD]" +
                                        match.Results[player1.ID].VictoryPoints + "[/TD][TD]" +
                                        match.Results[player1.ID].TournamentPoints + "[/TD]";
                            else //if(tournament.Format == EventFormat.Victory)
                                data += "[TD]" + match.Results[player1.ID].VictoryPoints + "[/TD][TD]" +
                                        match.Results[player1.ID].TournamentPoints + "[/TD][TD]" +
                                        match.Results[player1.ID].Differential + "[/TD]";
                            if (player2 != null)
                            {
                                data += "[TD]" + player2.Name + "[/TD]";
                                if (chkShowFactions.Checked) data += "[TD]" + player2.Faction + "[/TD]";
                                if (tournament.Format == EventFormat.Domination)
                                    data += "[TD]" + match.Results[player2.ID].TournamentPoints + "[/TD][TD]" +
                                            match.Results[player2.ID].Differential + "[/TD][TD]" +
                                            match.Results[player2.ID].VictoryPoints + "[/TD]";
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += "[TD]" + match.Results[player2.ID].Differential + "[/TD][TD]" +
                                            match.Results[player2.ID].VictoryPoints + "[/TD][TD]" +
                                            match.Results[player2.ID].TournamentPoints + "[/TD]";
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += "[TD]" + match.Results[player2.ID].VictoryPoints + "[/TD][TD]" +
                                            match.Results[player2.ID].TournamentPoints + "[/TD][TD]" +
                                            match.Results[player2.ID].Differential + "[/TD]";
                            }
                            else
                                data += ("[TD]BYE ROUND[/TD][TD][/TD][TD][/TD][TD][/TD]");
                            data += "[/TR]\r\n";
                        }
                    }
                    else
                    {
                        if (!rdoOrderTable.Checked)
                        {
                            var players = new List<string>(tournament.Players.Keys);
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
                            foreach (var id in players)
                            {
                                var player = Config.Settings.GetPlayer(id);
                                foreach (var match in round.Matches.Where(match => match.Players.Contains(player.ID)))
                                {
                                    data += "[TR][TD]" + roundNumber + "[/TD][TD]" + player.Name + "[/TD]";
                                    if (chkShowFactions.Checked) data += "[TD]" + player.Faction + "[/TD]";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += "[TD]" + match.Results[id].TournamentPoints + "[/TD][TD]" +
                                                match.Results[id].Differential + "[/TD][TD]" +
                                                match.Results[id].VictoryPoints + "[/TD]";
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += "[TD]" + match.Results[id].Differential + "[/TD][TD]" +
                                                match.Results[id].VictoryPoints + "[/TD][TD]" +
                                                match.Results[id].TournamentPoints + "[/TD]";
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += "[TD]" + match.Results[id].VictoryPoints + "[/TD][TD]" +
                                                match.Results[id].TournamentPoints + "[/TD][TD]" +
                                                match.Results[id].Differential + "[/TD]";
                                    if (match.Players.Count > 1)
                                    {
                                        var id2 = match.Players[0] == id ? match.Players[1] : match.Players[0];
                                        var player2 = Config.Settings.GetPlayer(id2);
                                        data += "[TD]" + player2.Name + "[/TD]";
                                    }
                                    else
                                        data += "[TD]BYE ROUND[/TD]";
                                    data += "[/TR]\r\n";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var match in round.Matches)
                            {
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                                data += "[TR][TD]" + roundNumber + "[/TD][TD]" + player1.Name + "[/TD]";
                                if (chkShowFactions.Checked) data += "[TD]" + player1.Faction + "[/TD]";
                                if (tournament.Format == EventFormat.Domination)
                                    data += "[TD]" + match.Results[player1.ID].TournamentPoints + "[/TD][TD]" +
                                            match.Results[player1.ID].Differential + "[/TD][TD]" +
                                            match.Results[player1.ID].VictoryPoints + "[/TD]";
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += "[TD]" + match.Results[player1.ID].Differential + "[/TD][TD]" +
                                            match.Results[player1.ID].VictoryPoints + "[/TD][TD]" +
                                            match.Results[player1.ID].TournamentPoints + "[/TD]";
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += "[TD]" + match.Results[player1.ID].VictoryPoints + "[/TD][TD]" +
                                            match.Results[player1.ID].TournamentPoints + "[/TD][TD]" +
                                            match.Results[player1.ID].Differential + "[/TD]";
                                if (player2 != null)
                                {
                                    data += "[TD]" + player2.Name + "[/TD]";
                                    if (chkShowFactions.Checked) data += "[TD]" + player2.Faction + "[/TD]";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += "[TD]" + match.Results[player2.ID].TournamentPoints + "[/TD][TD]" +
                                                match.Results[player2.ID].Differential + "[/TD][TD]" +
                                                match.Results[player2.ID].VictoryPoints + "[/TD]";
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += "[TD]" + match.Results[player2.ID].Differential + "[/TD][TD]" +
                                                match.Results[player2.ID].VictoryPoints + "[/TD][TD]" +
                                                match.Results[player2.ID].TournamentPoints + "[/TD]";
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += "[TD]" + match.Results[player2.ID].VictoryPoints + "[/TD][TD]" +
                                                match.Results[player2.ID].TournamentPoints + "[/TD][TD]" +
                                                match.Results[player2.ID].Differential + "[/TD]";
                                }
                                else
                                    data += "[TD]BYE ROUND[/TD]";
                                data += "[/TR]\r\n";
                            }
                        }
                    }
                    roundNumber++;
                }
            }

            data += "[/TABLE]\r\n";

            if (rdoWriteFile.Checked)
                using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write,
                                                                FileShare.None)))
                    sw.Write(data);
            else
                Clipboard.SetText(data, TextDataFormat.Text);
        }

        private void ExportAsCSV(Tournament tournament, string filename = null)
        {
            string scoreLabels;
            if (tournament.Format == EventFormat.Domination)
                scoreLabels = "\"TPs\",\"Diff\",\"VPs\"";
            else if (tournament.Format == EventFormat.Disparity)
                scoreLabels = "\"Diff\",\"VPs\",\"TPs\"";
            else //if( tournament.Format == EventFormat.Victory)
                scoreLabels = "\"VPs\",\"TPs\",\"Diff\"";

            var factionLabel = "";
            if (chkShowFactions.Checked) factionLabel = ",\"Faction\"";

            string data;
            if (rdoExportPlayer.Checked)
                data = "\"Round\",\"Player\"," + factionLabel + scoreLabels + ",\"Opponent\"\r\n";
            else if (rdoExportTable.Checked)
                data = "\"Round\",\"Table\",\"Player 1\"," + factionLabel + scoreLabels + ",\"Player 2\"," +
                    factionLabel + scoreLabels + "\r\n";
            else
                data = "\"Rank\",\"Player Name\"," + factionLabel + scoreLabels + "\r\n";

            if (chkShowName.Checked)
                data += "\"" + tournament.Name + "\"\r\n";

            if (rdoExportResults.Checked)
            {
                var results = tournament.PlayerIDs.Select(tournament.GetPlayerResults).ToList();
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

                    switch (tournament.Format)
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
                var rank = 1;
                foreach (var result in results)
                {
                    if (chkExcludeForfeits.Checked && result.Forfeited) continue;
                    var player = Config.Settings.GetPlayer(result.PlayerID);
                    var rankString = result.Forfeited ? "\"F\"" : rank++.ToString();
                    data += rankString + ",\"" + player.Name + "\",";
                    if (chkShowFactions.Checked) data += "\"" + player.Faction + "\",";
                    if (tournament.Format == EventFormat.Domination)
                        data += result.TournamentPoints + "," +
                                result.Differential + "," +
                                result.VictoryPoints;
                    else if (tournament.Format == EventFormat.Disparity)
                        data += result.Differential + "," +
                                result.VictoryPoints + "," +
                                result.TournamentPoints;
                    else //if(tournament.Format == EventFormat.Victory)
                        data += result.VictoryPoints + "," +
                                result.TournamentPoints + "," +
                                result.Differential;
                    data += "\r\n";
                }
            }
            else
            {
                var roundNumber = 1;
                foreach (var round in tournament.Rounds)
                {
                    if (rdoExportTable.Checked)
                    {
                        var matchNumber = 1;
                        foreach (var match in round.Matches)
                        {
                            var player1 = Config.Settings.GetPlayer(match.Players[0]);
                            var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;

                            data += roundNumber + "," + matchNumber++ + ",\"" + player1.Name + "\",";
                            if (chkShowFactions.Checked) data += "\"" + player1.Faction + "\",";
                            if (tournament.Format == EventFormat.Domination)
                                data += match.Results[player1.ID].TournamentPoints + "," +
                                        match.Results[player1.ID].Differential + "," +
                                        match.Results[player1.ID].VictoryPoints;
                            else if (tournament.Format == EventFormat.Disparity)
                                data += match.Results[player1.ID].Differential + "," +
                                        match.Results[player1.ID].VictoryPoints + "," +
                                        match.Results[player1.ID].TournamentPoints;
                            else //if(tournament.Format == EventFormat.Victory)
                                data += match.Results[player1.ID].VictoryPoints + "," +
                                        match.Results[player1.ID].TournamentPoints + "," +
                                        match.Results[player1.ID].Differential;
                            if (player2 != null)
                            {
                                data += ",\"" + player2.Name + "\",";
                                if (chkShowFactions.Checked) data += "\"" + player2.Faction + "\",";
                                if (tournament.Format == EventFormat.Domination)
                                    data += match.Results[player2.ID].TournamentPoints + "," +
                                            match.Results[player2.ID].Differential + "," +
                                            match.Results[player2.ID].VictoryPoints + "\r\n";
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += match.Results[player2.ID].Differential + "," +
                                            match.Results[player2.ID].VictoryPoints + "," +
                                            match.Results[player2.ID].TournamentPoints + "\r\n";
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += match.Results[player2.ID].VictoryPoints + "," +
                                            match.Results[player2.ID].TournamentPoints + "," +
                                            match.Results[player2.ID].Differential + "\r\n";
                            }
                            else
                                data += ",\"BYE ROUND\",0,0,0\r\n";
                        }
                    }
                    else
                    {
                        if (!rdoOrderTable.Checked)
                        {
                            var players = new List<string>(tournament.Players.Keys);
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
                            foreach (var id in players)
                            {
                                var player = Config.Settings.GetPlayer(id);
                                foreach (var match in round.Matches.Where(match => match.Players.Contains(player.ID)))
                                {
                                    data += roundNumber + ",\"" + player.Name + "\",";
                                    if (chkShowFactions.Checked) data += "\"" + player.Faction + "\",";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += match.Results[id].TournamentPoints + "," +
                                                match.Results[id].Differential + "," +
                                                match.Results[id].VictoryPoints;
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += match.Results[id].Differential + "," +
                                                match.Results[id].VictoryPoints + "," +
                                                match.Results[id].TournamentPoints;
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += match.Results[id].VictoryPoints + "," +
                                                match.Results[id].TournamentPoints + "," +
                                                match.Results[id].Differential;
                                    if (match.Players.Count > 1)
                                    {
                                        var id2 = match.Players[0] == id ? match.Players[1] : match.Players[0];
                                        var player2 = Config.Settings.GetPlayer(id2);
                                        data += ",\"" + player2.Name + "\"\r\n";
                                    }
                                    else
                                        data += ",\"BYE ROUND\"\r\n";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var match in round.Matches)
                            {
                                var player1 = Config.Settings.GetPlayer(match.Players[0]);
                                var player2 = match.Players.Count > 1 ? Config.Settings.GetPlayer(match.Players[1]) : null;
                                data += roundNumber + ",\"" + player1.Name + "\",";
                                if (chkShowFactions.Checked) data += "\"" + player1.Faction + "\",";
                                if (tournament.Format == EventFormat.Domination)
                                    data += match.Results[player1.ID].TournamentPoints + "," +
                                            match.Results[player1.ID].Differential + "," +
                                            match.Results[player1.ID].VictoryPoints;
                                else if (tournament.Format == EventFormat.Disparity)
                                    data += match.Results[player1.ID].Differential + "," +
                                            match.Results[player1.ID].VictoryPoints + "," +
                                            match.Results[player1.ID].TournamentPoints;
                                else //if(tournament.Format == EventFormat.Victory)
                                    data += match.Results[player1.ID].VictoryPoints + "," +
                                            match.Results[player1.ID].TournamentPoints + "," +
                                            match.Results[player1.ID].Differential;
                                if (player2 != null)
                                {
                                    data += ",\"" + player2.Name + "\",";
                                    if (chkShowFactions.Checked) data += "\"" + player2.Faction + "\",";
                                    if (tournament.Format == EventFormat.Domination)
                                        data += match.Results[player2.ID].TournamentPoints + "," +
                                                match.Results[player2.ID].Differential + "," +
                                                match.Results[player2.ID].VictoryPoints;
                                    else if (tournament.Format == EventFormat.Disparity)
                                        data += match.Results[player2.ID].Differential + "," +
                                                match.Results[player2.ID].VictoryPoints + "," +
                                                match.Results[player2.ID].TournamentPoints;
                                    else //if(tournament.Format == EventFormat.Victory)
                                        data += match.Results[player2.ID].VictoryPoints + "," +
                                                match.Results[player2.ID].TournamentPoints + "," +
                                                match.Results[player2.ID].Differential;
                                }
                                else
                                    data += ",\"BYE ROUND\",0,0,0";
                                data += "\r\n";
                            }
                        }
                    }
                    roundNumber++;
                }
            }

            if (rdoWriteFile.Checked)
                using (var sw = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write,
                                                                FileShare.None)))
                    sw.Write(data);
            else
                Clipboard.SetText(data, TextDataFormat.Text);
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
