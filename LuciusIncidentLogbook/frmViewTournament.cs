using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace KitchenGeeks
{
    public partial class frmViewTournament : Form
    {
        private frmViewMatches MatchesForm { get; set; }
        private frmTournamentRound RoundForm { get; set; }
        private frmTournamentRound EditRoundForm { get; set; }

        public frmViewTournament(string name)
        {
            InitializeComponent();
            TournamentName = name;
            Text = TournamentName + " -- View Tournament";

            // Set up the three score fields based on their priority, as determined by the Format.
            var tpColumn = new ColumnHeader { Text = "TPs", Name = "TP", Width = 40 };
            var vpColumn = new ColumnHeader { Text = "VPs", Name = "VP", Width = 40 };
            var diffColumn = new ColumnHeader { Text = "Diff", Name = "Diff", Width = 40 };

            switch (Config.Settings.GetTournament(TournamentName).Format)
            {
                case EventFormat.Domination:
                    lstResults.Columns.Add(tpColumn);
                    lstResults.Columns.Add(diffColumn);
                    lstResults.Columns.Add(vpColumn);
                    break;

                case EventFormat.Disparity:
                    lstResults.Columns.Add(diffColumn);
                    lstResults.Columns.Add(vpColumn);
                    lstResults.Columns.Add(tpColumn);
                    break;

                case EventFormat.Victory:
                    lstResults.Columns.Add(vpColumn);
                    lstResults.Columns.Add(tpColumn);
                    lstResults.Columns.Add(diffColumn);
                    break;
            }

            lstResults.ListViewItemSorter =
                new TournamentRankItemComparer(Config.Settings.GetTournament(TournamentName).Format);

            FillPlayers();
            UpdateScores();
            SetRoundButton();
            lstResults.Width = Width - 223;

            chkConfirmNewRound.Checked = Config.Settings.ConfirmNewRounds;

            // Gets around a weird problem where the Items list only refreshes between method calls.
            // ReSharper disable ObjectCreationAsStatement
            new Timer(InitialRanks, null, 1, Timeout.Infinite);
            // ReSharper restore ObjectCreationAsStatement
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public string TournamentName { get; private set; }

        private void InitialRanks(object sender)
        {
            lstResults.Invoke(new MethodInvoker(SetRanks));
        }

        private void FillPlayers()
        {
            lstResults.BeginUpdate();
            lstResults.Items.Clear();
            var tournament = Config.Settings.GetTournament(TournamentName);
            foreach (var playerData in tournament.Players)
            {
                var id = playerData.Key;
                var playerNum = playerData.Value;
                var player = Config.Settings.GetPlayer(id);
                var current = tournament.GetPlayerResults(id);
                var item = new ListViewItem { Name = id, Text = "" };
                item.SubItems.Add(playerNum.ToString());
                item.SubItems.Add(player.Name);
                item.SubItems.Add(tournament.PlayerFaction[id].ToString());
                var tp = new ListViewItem.ListViewSubItem(item, current.TournamentPoints.ToString()) { Name = "TP" };
                var vp = new ListViewItem.ListViewSubItem(item, current.VictoryPoints.ToString()) { Name = "VP" };
                var diff = new ListViewItem.ListViewSubItem(item, current.Differential.ToString()) { Name = "Diff" };
                switch (tournament.Format)
                {
                    case EventFormat.Domination:
                        item.SubItems.Add(tp);
                        item.SubItems.Add(diff);
                        item.SubItems.Add(vp);
                        break;
                    case EventFormat.Disparity:
                        item.SubItems.Add(diff);
                        item.SubItems.Add(vp);
                        item.SubItems.Add(tp);
                        break;
                    case EventFormat.Victory:
                        item.SubItems.Add(vp);
                        item.SubItems.Add(tp);
                        item.SubItems.Add(diff);
                        break;
                }
                lstResults.Items.Add(item);
            }
            lstResults.Sort();
            lstResults.EndUpdate();
        }

        private void UpdateScores()
        {
            lstResults.BeginUpdate();
            foreach (ListViewItem item in lstResults.Items)
            {
                string id = item.Name;
                var current = Config.Settings.GetTournament(TournamentName).GetPlayerResults(id);
                item.SubItems["TP"].Text = current.TournamentPoints.ToString();
                item.SubItems["VP"].Text = current.VictoryPoints.ToString();
                item.SubItems["Diff"].Text = current.Differential.ToString();
                item.Text = current.Forfeited ? "F" : "";
            }
            lstResults.Sort();
            lstResults.EndUpdate();
            SetRanks();
        }

        private void SetRanks()
        {
            lstResults.BeginUpdate();
            int rank = 0;
            int displayRank = 0;
            for (int index = 0; index < lstResults.Items.Count; index++)
            {
                if (lstResults.Items[index].Text != "F")
                {
                    rank++;
                    if (!TiedWithPrevious(index))
                        if (Config.Settings.SkipTiedRanks)
                            displayRank = rank;
                        else
                            displayRank++;
                    lstResults.Items[index].Text = displayRank.ToString();
                    if (Config.Settings.GetTournament(TournamentName).Rounds.Count > 0 &&
                        Config.Settings.GetTournament(TournamentName).Rounds[0].Completed)
                        if (chkHighlight.Checked && rank <= (int)numTopRows.Value)
                            lstResults.Items[index].BackColor = Color.LightGreen;
                        else
                            lstResults.Items[index].BackColor = Color.White;
                }
                else
                    lstResults.Items[index].BackColor = Color.LightCoral;
            }
            lstResults.EndUpdate();
        }

        private void chkHighlight_CheckedChanged(object sender, EventArgs e)
        {
            numTopRows.Enabled = chkHighlight.Checked;
            SetRanks();
        }

        private void SetRoundButton()
        {
            if (Config.Settings.GetTournament(TournamentName).Rounds.Count == 0)
            {
                btnRound.Text = "Begin Tournament";
                lblCurrentRound.Text = "None of " + Config.Settings.GetTournament(TournamentName).TotalRounds.ToString();
                btnViewPairings.Enabled = false;
                btnEditRounds.Enabled = false;
                btnCancelRound.Enabled = false;
                return;
            }
            int currentRound = Config.Settings.GetTournament(TournamentName).Rounds.Count;
            if (!Config.Settings.GetTournament(TournamentName).Rounds[currentRound - 1].Completed)
            {
                lblCurrentRound.Text = currentRound.ToString() + " of " +
                                       Config.Settings.GetTournament(TournamentName).TotalRounds.ToString() + " Active";
                btnRound.Text = "Manage Current Round";
                btnViewPairings.Enabled = true;
                btnEditRounds.Enabled = currentRound > 1;
                btnCancelRound.Enabled = true;
            }
            else if (currentRound != Config.Settings.GetTournament(TournamentName).TotalRounds)
            {
                lblCurrentRound.Text = currentRound.ToString() + " of " +
                                       Config.Settings.GetTournament(TournamentName).TotalRounds.ToString() + " Done";
                btnRound.Text = "Start Next Round";
                btnViewPairings.Enabled = false;
                btnEditRounds.Enabled = true;
                btnCancelRound.Enabled = false;
            }
            else
            {
                lblCurrentRound.Text = "Tournament Complete!";
                btnRound.Text = "Rounds Locked";
                btnRound.Enabled = false;
                btnViewPairings.Enabled = false;
                btnEditRounds.Enabled = true;
            }
        }

        private void btnRound_Click(object sender, EventArgs e)
        {
            if (RoundForm != null)
            {
                RoundForm.WindowState = FormWindowState.Normal;
                RoundForm.Focus();
                return;
            }

            TournamentRound round;
            int roundNum = Config.Settings.GetTournament(TournamentName).Rounds.Count;
            if (roundNum == 0 ||
                Config.Settings.GetTournament(TournamentName).Rounds[roundNum - 1].Completed)
            {
                round = Config.Settings.GetTournament(TournamentName).GenerateNextRound();
                Config.Settings.SaveEvents();
                roundNum++;
                if (!chkOpenRound.Checked)
                {
                    SetRoundButton();
                    return;
                }
            }
            else
                round = Config.Settings.GetTournament(TournamentName).Rounds[roundNum - 1];

            RoundForm = new frmTournamentRound(round, roundNum, false) { MdiParent = MdiParent };
            RoundForm.FormClosing += RoundForm_FormClosing;
            RoundForm.FormClosed += RoundForm_FormClosed;
            RoundForm.Show();
            SetRoundButton();
        }

        private void RoundForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RoundForm = null;
            UpdateScores();
            SetRoundButton();
        }

        private void RoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            // Capture the current state of the Tournament Round.
            Config.Settings.GetTournament(TournamentName).Rounds[
                Config.Settings.GetTournament(TournamentName).Rounds.Count - 1] = RoundForm.Round;
            Config.Settings.SaveEvents();
        }

        private void frmViewTournament_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RoundForm != null)
                if (MessageBox.Show("Closing this form will also close the currently open Round for this tournament. " +
                                    "The scores will be saved in their current state. Continue?", "Confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                    DialogResult.Yes)
                    RoundForm.Close();
                else
                    e.Cancel = true;

            if (MatchesForm != null) MatchesForm.Close();
        }

        private void btnViewPairings_Click(object sender, EventArgs e)
        {
            if (MatchesForm != null)
            {
                MatchesForm.WindowState = FormWindowState.Normal;
                MatchesForm.Focus();
                return;
            }

            MatchesForm = new frmViewMatches(TournamentName);
            MatchesForm.FormClosed += MatchesForm_FormClosed;
            MatchesForm.MdiParent = MdiParent;
            MatchesForm.Show();
        }

        private void MatchesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MatchesForm = null;
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            if (ParentForm != null)
                foreach (Form childForm in ParentForm.MdiChildren)
                {
                    if (childForm is frmNotes)
                    {
                        if ((string)childForm.Tag == TournamentName)
                        {
                            childForm.WindowState = FormWindowState.Normal;
                            childForm.Focus();
                            return;
                        }
                    }
                }

            var notes = new frmNotes(TournamentName) { MdiParent = ParentForm };
            notes.Show();
        }

        private bool TiedWithPrevious(int index)
        {
            if (index == 0) return false;

            ListViewItem a = lstResults.Items[index];
            ListViewItem b = lstResults.Items[index - 1];
            int pointsa = a.SubItems.ContainsKey("Points") ? Convert.ToInt32(a.SubItems["Points"].Text) : 0;
            int quantitya = a.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(a.SubItems["Quantity"].Text) : 0;
            int pa = a.SubItems.ContainsKey("TP") ? Convert.ToInt32(a.SubItems["TP"].Text) : 0;
            int vPa = a.SubItems.ContainsKey("VP") ? Convert.ToInt32(a.SubItems["VP"].Text) : 0;
            int diffa = a.SubItems.ContainsKey("Diff") ? Convert.ToInt32(a.SubItems["Diff"].Text) : 0;
            int pointsb = b.SubItems.ContainsKey("Points") ? Convert.ToInt32(b.SubItems["Points"].Text) : 0;
            int quantityb = b.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(b.SubItems["Quantity"].Text) : 0;
            int pb = b.SubItems.ContainsKey("TP") ? Convert.ToInt32(b.SubItems["TP"].Text) : 0;
            int vPb = b.SubItems.ContainsKey("VP") ? Convert.ToInt32(b.SubItems["VP"].Text) : 0;
            int diffb = b.SubItems.ContainsKey("Diff") ? Convert.ToInt32(b.SubItems["Diff"].Text) : 0;

            return (pointsa == pointsb && quantitya == quantityb && pa == pb && vPa == vPb && diffa == diffb);
        }

        private void frmViewTournament_SizeChanged(object sender, EventArgs e)
        {
            lstResults.Width = Width - 223;
        }

        private void mnuPlayers_Opening(object sender, CancelEventArgs e)
        {
            if (lstResults.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            forfeitPlayerToolStripMenuItem.Enabled = (lstResults.SelectedItems[0].Text != "F" &&
                                                      !Config.Settings.GetTournament(TournamentName).Completed);
            removeForfeitToolStripMenuItem.Enabled = (lstResults.SelectedItems[0].Text == "F" &&
                                                      !Config.Settings.GetTournament(TournamentName).Completed);
        }

        private void forfeitPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = lstResults.SelectedItems[0].Name;
            var tournament = Config.Settings.GetTournament(TournamentName);
            var roundNum = tournament.Rounds.Count - 1;
            if (tournament.Rounds[roundNum].Completed)
            {
                tournament.RemovedPlayers.Add(id);
            }
            else
            {
                foreach (var match in tournament.Rounds[roundNum].Matches)
                {
                    if (!match.Players.Contains(id)) continue;
                    // If he was the buy round, this is easy.
                    if (match.Players.Count == 1)
                    {
                        tournament.Rounds[roundNum].Matches.Remove(match);
                        tournament.RemovedPlayers.Add(id);
                    }
                    else
                    {
                        if (match.Results.ContainsKey(id))
                            match.Results.Clear();
                        var opponentID = match.Players[0] == id ? match.Players[1] : match.Players[0];
                        var resultA = new MatchResult(id) { Forfeited = true };
                        var resultB = new MatchResult(opponentID) { Bye = true };
                        match.Results.Add(id, resultA);
                        match.Results.Add(opponentID, resultB);
                        match.CalculateResults();
                    }
                    break;
                }
            }
            Config.Settings.SaveEvents();
            UpdateScores();
        }

        private void removeForfeitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = lstResults.SelectedItems[0].Name;
            var tournament = Config.Settings.GetTournament(TournamentName);
            var roundNum = tournament.Rounds.Count;
            if (roundNum == 0)
            {
                tournament.Players.Remove(id);
                FillPlayers();
            }
            else if (tournament.Rounds[roundNum - 1].Completed)
            {
                foreach (var match in tournament.Rounds[roundNum - 1].Matches.Where(match => match.Players.Contains(id)))
                {
                    // If he was the buy round, this is easy.
                    if (match.Players.Count == 1)
                    {
                        tournament.Rounds[roundNum - 1].Matches.Remove(match);
                        tournament.RemovedPlayers.Add(id);
                    }
                    else
                    {
                        match.Results[id].Forfeited = false;
                    }
                    break;
                }
            }

            if (tournament.RemovedPlayers.Contains(id))
                tournament.RemovedPlayers.Remove(id);


            Config.Settings.SaveEvents();
            UpdateScores();
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            var tournament = Config.Settings.GetTournament(TournamentName);
            using (var playerSelect = new FrmAddPlayer(tournament.Players))
            {
                if (playerSelect.ShowDialog() == DialogResult.OK)
                {
                    var reopenRoundForm = false;
                    if (RoundForm != null)
                    {
                        RoundForm.Close();
                        reopenRoundForm = true;
                    }
                    var id = playerSelect.SelectedID;
                    var faction = playerSelect.SelectedFaction;
                    tournament.AddPlayer(id, faction);

                    int currentRound = Config.Settings.GetTournament(TournamentName).Rounds.Count;
                    if (currentRound > 0)
                    {
                        if (tournament.Players.Count%2 == 1)
                        {
                            var byeMatch = new TournamentMatch(TournamentName);
                            byeMatch.Players.Add(id);
                            byeMatch.Crews.Add(id, new PlayerCrew());
                            var byeResult = new MatchResult(id) {Bye = true};
                            byeMatch.Results.Add(id, byeResult);

                            if (!Config.Settings.GetTournament(TournamentName).Rounds[currentRound - 1].Completed)
                                Config.Settings.GetTournament(TournamentName).Rounds[currentRound - 1].Matches.Add(
                                    byeMatch);
                        }
                        else
                        {
                            foreach (
                                var match in Config.Settings.GetTournament(TournamentName).Rounds[currentRound - 1].
                                    Matches.Where(match => match.ByeRound))
                            {
                                if (!Config.Settings.GetTournament(TournamentName).Rounds[currentRound - 1].Completed)
                                {
                                    match.Results.Clear();
                                    match.Players.Add(id);
                                    match.Crews.Add(id, new PlayerCrew());
                                    break;
                                }
                            }
                        }
                    }
                    Config.Settings.SaveEvents();
                    FillPlayers();
                    UpdateScores();

                    if (reopenRoundForm) btnRound_Click(sender, e);
                }
            }
        }

        private void btnExportResults_Click(object sender, EventArgs e)
        {
            using (var dialog = new FrmExportTournamentResults(TournamentName))
                dialog.ShowDialog();
        }

        private void btnEditRounds_Click(object sender, EventArgs e)
        {
            if (EditRoundForm != null)
            {
                EditRoundForm.WindowState = FormWindowState.Normal;
                EditRoundForm.Focus();
                return;
            }

            var tournament = Config.Settings.GetTournament(TournamentName);
            var completedRounds = tournament.Rounds.Count;
            if (!tournament.Rounds[completedRounds - 1].Completed) completedRounds--;
            if (completedRounds == 0) return;

            int roundNum;
            using (var getRoundNum = new frmGetRoundToEdit(completedRounds))
            {
                if (getRoundNum.ShowDialog() == DialogResult.Cancel) return;
                roundNum = getRoundNum.SelectedRound;
            }

            var round = Config.Settings.GetTournament(TournamentName).Rounds[roundNum - 1];

            EditRoundForm = new frmTournamentRound(round, roundNum, true) { MdiParent = MdiParent };
            EditRoundForm.FormClosing += EditRoundForm_FormClosing;
            EditRoundForm.FormClosed += EditRoundForm_FormClosed;
            EditRoundForm.Show();
            SetRoundButton();
        }


        private void EditRoundForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditRoundForm = null;
            UpdateScores();
        }

        private void EditRoundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Capture the current state of the Tournament Round.
            Config.Settings.GetTournament(TournamentName).Rounds[EditRoundForm.RoundNumber - 1] = EditRoundForm.Round;
            Config.Settings.SaveEvents();
        }

        private void changeFactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = lstResults.SelectedItems[0].Name;
            var tournament = Config.Settings.GetTournament(TournamentName);
            using(var factionForm = new frmChangeFaction(tournament.PlayerFaction[id]))
            {
                if(factionForm.ShowDialog() == DialogResult.OK)
                {
                    tournament.PlayerFaction[id] = factionForm.SelectedFaction;
                    Config.Settings.SaveEvents();
                    lstResults.Items[lstResults.SelectedIndices[0]].SubItems[3].Text = factionForm.SelectedFaction.ToString();
                }
            }
        }

        private void btnCancelRound_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Cancelling this round will delete any results. Matchmaking will start over and the pairings " +
                "may change between the current round and the new round. Are you sure you wish to Cancel?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (RoundForm != null)
                    RoundForm.Close();
                var tournament = Config.Settings.GetTournament(TournamentName);
                tournament.Rounds.RemoveAt(tournament.Rounds.Count - 1);
                Config.Settings.SaveEvents();
                SetRoundButton();
            }
        }

        private void replacePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tournament = Config.Settings.GetTournament(TournamentName);
            using (var playerSelect = new FrmAddPlayer(tournament.Players))
            {
                if (playerSelect.ShowDialog() == DialogResult.OK)
                {
                    var reopenRoundForm = false;
                    if (RoundForm != null)
                    {
                        RoundForm.Close();
                        reopenRoundForm = true;
                    }
                    var originalID = lstResults.SelectedItems[0].Name;
                    var number = tournament.Players[originalID];
                    var id = playerSelect.SelectedID;
                    var faction = playerSelect.SelectedFaction;
                    tournament.Players.Remove(originalID);
                    tournament.PlayerFaction.Remove(originalID);
                    tournament.AddPlayer(id, faction, number);

                    if (tournament.RemovedPlayers.Contains(originalID))
                        tournament.RemovedPlayers[tournament.RemovedPlayers.IndexOf(originalID)] = id;
                    foreach(var round in tournament.Rounds)
                    {
                        var match = round.Matches.Where(m => m.Players.Contains(originalID)).FirstOrDefault();
                        if (match != null)
                        {
                            match.Players[match.Players.IndexOf(originalID)] = id;
                            if (match.Results.ContainsKey(originalID))
                            {
                                var result = match.Results[originalID];
                                match.Results.Remove(originalID);
                                result.PlayerID = id;
                                match.Results.Add(id, result);                                
                            }
                        }
                    }
                    Config.Settings.SaveEvents();
                    FillPlayers();
                    UpdateScores();
                    if (reopenRoundForm) btnRound_Click(sender, e);
                }
            }
        }
    }

    public class TournamentRankItemComparer : IComparer
    {
        private EventFormat Format { get; set; }

        public TournamentRankItemComparer(EventFormat tournamentFormat)
        {
            Format = tournamentFormat;
        }

        public int Compare(object x, object y)
        {
            var a = (ListViewItem)x;
            var b = (ListViewItem)y;

            // Bubble Forfeits to the bottom.
            if (a.Text == "F" && b.Text == "F")
                return a.SubItems[1].Text.CompareTo(b.SubItems[1].Text);
            if (a.Text == "F")
                return 1;
            if (b.Text == "F")
                return -1;

            var tpA = Convert.ToInt32(a.SubItems["TP"].Text);
            var vpA = Convert.ToInt32(a.SubItems["VP"].Text);
            var diffA = Convert.ToInt32(a.SubItems["Diff"].Text);
            var tpB = Convert.ToInt32(b.SubItems["TP"].Text);
            var vpB = Convert.ToInt32(b.SubItems["VP"].Text);
            var diffB = Convert.ToInt32(b.SubItems["Diff"].Text);

            switch (Format)
            {
                case EventFormat.Domination:
                    if (tpA != tpB)
                        return tpB.CompareTo(tpA);
                    return diffA != diffB ? diffB.CompareTo(diffA) : vpB.CompareTo(vpA);

                case EventFormat.Disparity:
                    if (diffA != diffB)
                        return diffB.CompareTo(diffA);
                    return vpA != vpB ? vpB.CompareTo(vpA) : tpB.CompareTo(tpA);

                case EventFormat.Victory:
                    if (vpA != vpB)
                        return vpB.CompareTo(vpA);
                    return tpA != tpB ? tpB.CompareTo(tpA) : diffB.CompareTo(diffA);
            }

            // Worst case, compare the names.
            return a.SubItems[1].Text.CompareTo(b.SubItems[1].Text);
        }
    }
}