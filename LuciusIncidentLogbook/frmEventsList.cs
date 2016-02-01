using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using System.Xml;

namespace KitchenGeeks
{
    public partial class frmEventsList : Form
    {
        private readonly List<FrmViewLeague> ActiveLeagueWindows = new List<FrmViewLeague>();
        private readonly List<FrmTournamentPlayers> ActivePlayersWindows = new List<FrmTournamentPlayers>();
        private readonly List<frmViewTournament> ActiveTournamentWindows = new List<frmViewTournament>();
        private bool SortAscend = true;
        private int SortColumn;

        public frmEventsList()
        {
            InitializeComponent();
            lstEvents.Columns[0].ImageIndex = 0;
            FilterList();
            dtFilterDate.Value = DateTime.Now;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FilterList();
        }

        /// <summary>
        ///     Filters and refreshes the list of Events.
        /// </summary>
        public void FilterList()
        {
            // Make a reference that's midnight today.
            var pastReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            lstEvents.BeginUpdate();
            lstEvents.Items.Clear();
            foreach (Tournament tournament in Config.Settings.Tournaments)
            {
                // Check filters.
                if (chkHidePast.Checked && tournament.Completed && tournament.Date < pastReference) continue;
                if (rdoFilterLocation.Checked &&
                    !tournament.Location.ToLower().Contains(txtFilterLocation.Text.ToLower())) continue;
                if (rdoFilterDate.Checked &&
                    dtFilterDate.Value.ToString("yyyyMMdd") != tournament.Date.ToString("yyyyMMdd")) continue;

                var item = new ListViewItem();
                item.Name = tournament.Name;
                item.Text = tournament.Name;
                item.Tag = "Tournament";
                item.SubItems.Add("Tournament");
                item.SubItems.Add(tournament.Location);
                item.SubItems.Add(tournament.Date.ToShortDateString());
                item.SubItems.Add(tournament.Players.Count.ToString());
                if (tournament.Rounds.Count == 0)
                    item.SubItems.Add("Not Started");
                else if (tournament.Rounds.Count == tournament.TotalRounds &&
                         tournament.Rounds[tournament.TotalRounds - 1].Completed)
                    item.SubItems.Add("Finished");
                else
                {
                    int roundNum = tournament.Rounds.Count;
                    if (tournament.Rounds[roundNum - 1].Completed)
                        item.SubItems.Add("Round " + roundNum.ToString() + " of " +
                                          tournament.TotalRounds.ToString() + " Done");
                    else
                        item.SubItems.Add("Round " + roundNum.ToString() + " of " +
                                          tournament.TotalRounds.ToString() + " Active");
                }

                lstEvents.Items.Add(item);
            }
            foreach (League league in Config.Settings.Leagues)
            {
                // Check filters.
                if (chkHidePast.Checked && league.EndDate < pastReference) continue;
                if (rdoFilterLocation.Checked && !league.Location.ToLower().Contains(txtFilterLocation.Text.ToLower()))
                    continue;
                if (rdoFilterDate.Checked && dtFilterDate.Value >= league.StartDate &&
                    dtFilterDate.Value <= league.EndDate) continue;

                var item = new ListViewItem();
                item.Name = league.Name;
                item.Text = league.Name;
                item.Tag = "League";
                item.SubItems.Add("League");
                item.SubItems.Add(league.Location);
                item.SubItems.Add(league.StartDate.ToShortDateString());
                item.SubItems.Add(league.Players.Count.ToString());
                if (league.MatchesPlayed.Count == 0)
                    item.SubItems.Add("Not Started");
                else if (league.EndDate < pastReference)
                    item.SubItems.Add("Finished");
                else
                {
                    item.SubItems.Add(league.MatchesPlayed.Count.ToString() + " Result" +
                                      (league.MatchesPlayed.Count == 1 ? "" : "s") + " Entered");
                }

                lstEvents.Items.Add(item);
            }

            lstEvents.Sort();
            if (lstEvents.Columns.Count > 0)
            {
                lstEvents.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lstEvents.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            lstEvents.EndUpdate();
        }

        private void lstEvents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (SortColumn == e.Column)
            {
                SortAscend = !SortAscend;
                lstEvents.Columns[e.Column].ImageIndex = SortAscend ? 0 : 1;
            }
            else
            {
                lstEvents.Columns[SortColumn].ImageIndex = -1;
                lstEvents.Columns[SortColumn].TextAlign = lstEvents.Columns[SortColumn].TextAlign;
                SortAscend = true;
                SortColumn = e.Column;
                lstEvents.Columns[e.Column].ImageIndex = 0;
            }

            lstEvents.BeginUpdate();
            lstEvents.ListViewItemSorter = new ListViewItemComparer(e.Column, SortAscend);
            lstEvents.Sort();
            lstEvents.EndUpdate();
        }

        private void btnNewEvent_Click(object sender, EventArgs e)
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

                            League checkLeague = Config.Settings.GetLeague(dialog.Name);
                            Tournament checkTournament = Config.Settings.GetTournament(dialog.Name);
                            if (checkTournament != null || checkLeague != null)
                                if (MessageBox.Show("An event with the name \"" + tournament.Name + "\" already " +
                                                    "exists. Event names must be unique.", "Validation Error",
                                                    MessageBoxButtons.RetryCancel,
                                                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) ==
                                    DialogResult.Cancel)
                                    return;
                                else
                                    continue;

                            Config.Settings.Tournaments.Add(tournament);
                            Config.Settings.SaveEvents();

                            lstEvents.BeginUpdate();

                            var item = new ListViewItem();
                            item.Name = tournament.Name;
                            item.Text = tournament.Name;
                            item.SubItems.Add("Tournament");
                            item.SubItems.Add(tournament.Location);
                            item.SubItems.Add(tournament.Date.ToShortDateString());
                            item.SubItems.Add(tournament.Players.Count.ToString());
                            item.SubItems.Add("Not Started");
                            lstEvents.Items.Add(item);

                            lstEvents.Sort();
                            lstEvents.EndUpdate();

                            // Show the Add Players screen if wanted.
                            if (!btnShowAddPlayers.Checked) return;
                            var args = new TournamentPlayersEventArgs(tournament.Name, tournament.Players, tournament.PlayerFaction,
                                                                      PlayersAdded);

                            var addPlayersDialog = new FrmTournamentPlayers(args);
                            addPlayersDialog.MdiParent = MdiParent;
                            addPlayersDialog.Show();
                            ActivePlayersWindows.Add(addPlayersDialog);

                            return;
                        }
                        else
                            return;
                    }
                }
            }
            else if (selectedType == EventType.League)
            {
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

                            lstEvents.BeginUpdate();

                            var item = new ListViewItem();
                            item.Name = league.Name;
                            item.Text = league.Name;
                            item.SubItems.Add("League");
                            item.SubItems.Add(league.Location);
                            item.SubItems.Add(league.StartDate.ToShortDateString());
                            item.SubItems.Add(league.Players.Count.ToString());
                            item.SubItems.Add("Not Started");
                            lstEvents.Items.Add(item);

                            lstEvents.Sort();
                            lstEvents.EndUpdate();

                            return;
                        }
                        else
                            return;
                    }
                }
            }
        }

        private void editOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
            {
                Tournament tournament = Config.Settings.GetTournament(lstEvents.SelectedItems[0].Text);
                using (var dialog = new frmTournamentOptions(tournament, false))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        int index = Config.Settings.Tournaments.IndexOf(tournament);
                        Config.Settings.Tournaments[index] = dialog.Tournament;
                        Config.Settings.SaveEvents();

                        lstEvents.BeginUpdate();

                        lstEvents.SelectedItems[0].Text = tournament.Name;
                        lstEvents.SelectedItems[0].SubItems[2].Text = tournament.Location;
                        lstEvents.SelectedItems[0].SubItems[3].Text = tournament.Date.ToShortDateString();
                        lstEvents.SelectedItems[0].SubItems[4].Text = tournament.Players.Count.ToString();
                        if (tournament.Rounds.Count == 0)
                            lstEvents.SelectedItems[0].SubItems[5].Text = "Not Started";
                        else if (tournament.Rounds.Count == tournament.TotalRounds &&
                                 tournament.Rounds[tournament.TotalRounds - 1].Completed)
                            lstEvents.SelectedItems[0].SubItems[5].Text = "Finished";
                        else
                        {
                            int roundNum = tournament.Rounds.Count;
                            if (tournament.Rounds[roundNum - 1].Completed)
                                lstEvents.SelectedItems[0].SubItems[5].Text = "Round " + roundNum.ToString() +
                                                                              " of " +
                                                                              tournament.TotalRounds.ToString() +
                                                                              " Done";
                            else
                                lstEvents.SelectedItems[0].SubItems[5].Text = "Round " + roundNum.ToString() +
                                                                              " of " +
                                                                              tournament.TotalRounds.ToString() +
                                                                              " Active";
                        }

                        lstEvents.Sort();
                        lstEvents.EndUpdate();
                    }
                }
            }
            else
            {
                var pastReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                League league = Config.Settings.GetLeague(lstEvents.SelectedItems[0].Text);
                using (var dialog = new frmLeagueOptions(league, false))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        int index = Config.Settings.Leagues.IndexOf(league);
                        Config.Settings.Leagues[index] = dialog.League;
                        Config.Settings.SaveEvents();

                        lstEvents.BeginUpdate();
                        lstEvents.SelectedItems[0].Text = league.Name;
                        lstEvents.SelectedItems[0].SubItems[2].Text = league.Location;
                        lstEvents.SelectedItems[0].SubItems[3].Text = league.StartDate.ToShortDateString();
                        lstEvents.SelectedItems[0].SubItems[4].Text = league.Players.Count.ToString();
                        if (league.MatchesPlayed.Count == 0)
                            lstEvents.SelectedItems[0].SubItems[5].Text = "Not Started";
                        else if (league.EndDate < pastReference)
                            lstEvents.SelectedItems[0].SubItems[5].Text = "Finished";
                        else
                        {
                            lstEvents.SelectedItems[0].SubItems[5].Text = league.MatchesPlayed.Count.ToString() +
                                                                          " Result" +
                                                                          (league.MatchesPlayed.Count == 1 ? "" : "s") +
                                                                          " Entered";
                        }

                        lstEvents.Sort();
                        lstEvents.EndUpdate();
                    }
                }
            }
        }

        private void mnuEventsOptions_Opening(object sender, CancelEventArgs e)
        {
            if (lstEvents.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
            {
                mnuInputPlayers.Visible = true;
                mnuRecordMatch.Visible = false;
                exportEventResultsToolStripMenuItem.Enabled = true;

                var tournament = Config.Settings.GetTournament(lstEvents.SelectedItems[0].Text);
                mnuInputPlayers.Enabled = (!tournament.Completed);
                editOptionsToolStripMenuItem.Enabled = (!tournament.Completed);
                mnuOpenForum.Enabled = tournament.ForumURL.Length > 0;
                if (Config.Settings.DisableReportIfSent)
                {
                    sendPreEventReportToolStripMenuItem.Enabled = !tournament.PreEventSent;
                    sendPostEventReportToolStripMenuItem.Enabled = !tournament.PostEventSent;
                }
            }
            else
            {
                var pastReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                mnuInputPlayers.Visible = false;
                mnuRecordMatch.Visible = true;
                exportEventResultsToolStripMenuItem.Enabled = false;

                League league = Config.Settings.GetLeague(lstEvents.SelectedItems[0].Text);
                mnuOpenForum.Enabled = league.ForumURL.Length > 0;
                if (Config.Settings.DisableReportIfSent)
                {
                    sendPreEventReportToolStripMenuItem.Enabled = !league.PreEventSent;
                    sendPostEventReportToolStripMenuItem.Enabled = !league.PostEventSent;
                }
            }
        }

        private void inputPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tournament tournament = Config.Settings.GetTournament(lstEvents.SelectedItems[0].Text);
            var args = new TournamentPlayersEventArgs(tournament.Name, tournament.Players, tournament.PlayerFaction,
                                                      PlayersAdded);
            foreach (FrmTournamentPlayers active in ActivePlayersWindows)
            {
                if (active.TournamentName == tournament.Name)
                {
                    active.WindowState = FormWindowState.Normal;
                    active.Focus();
                    return;
                }
            }

            var dialog = new FrmTournamentPlayers(args);
            dialog.MdiParent = MdiParent;
            dialog.Show();
            ActivePlayersWindows.Add(dialog);
        }

        private void PlayersAdded(object sender, TournamentPlayersEventArgs e)
        {
            lstEvents.BeginInvoke(new MethodInvoker(delegate
                {
                    if (!e.Cancel)
                    {
                        Tournament tournament = Config.Settings.GetTournament(e.Name);
                        int index = Config.Settings.Tournaments.IndexOf(tournament);

                        Config.Settings.Tournaments[index].Players = e.Enrolled;
                        Config.Settings.Tournaments[index].PlayerFaction = e.ChosenFactions;
                        Config.Settings.SaveEvents();

                        if (lstEvents.Items.ContainsKey(tournament.Name))
                            lstEvents.Items[tournament.Name].SubItems[4].Text = e.Enrolled.Count.ToString();

                        if (e.Enrolled.Count > 0 && Config.Settings.OpenAfterPlayersAdded)
                        {
                            foreach (ListViewItem item in lstEvents.Items)
                            {
                                if (item.Text == e.Name)
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                            openEventToolStripMenuItem_Click(this, new EventArgs());
                        }
                    }
                    ActivePlayersWindows.Remove((FrmTournamentPlayers)sender);
                    return;
                }));
        }

        private void openEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItems.Count == 0) return;
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
            {
                Tournament tournament = Config.Settings.GetTournament(lstEvents.SelectedItems[0].Text);

                if (tournament.Players.Count == 0)
                {
                    if (MessageBox.Show(
                        "You have not added any players to this Tournament! You must add players before " +
                        "the Tournament can be opened. Add players now?", "No Players", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        inputPlayersToolStripMenuItem_Click(sender, e);
                    return;
                }


                foreach (frmViewTournament active in ActiveTournamentWindows)
                {
                    if (active.TournamentName == tournament.Name)
                    {
                        active.WindowState = FormWindowState.Normal;
                        active.Focus();
                        return;
                    }
                }

                var dialog = new frmViewTournament(tournament.Name);
                dialog.FormClosed += frmViewTournament_FormClosed;
                dialog.MdiParent = MdiParent;
                dialog.Show();
                ActiveTournamentWindows.Add(dialog);
            }
            else
            {
                League league = Config.Settings.GetLeague(lstEvents.SelectedItems[0].Text);

                foreach (FrmViewLeague active in ActiveLeagueWindows)
                {
                    if (active.League.Name == league.Name)
                    {
                        active.WindowState = FormWindowState.Normal;
                        active.Focus();
                        return;
                    }
                }

                var dialog = new FrmViewLeague(league.Name);

                foreach (Form form in MdiParent.MdiChildren)
                {
                    if (form is frmEnterLeagueResults)
                    {
                        var thatForm = (frmEnterLeagueResults)form;
                        if ((string)thatForm.Tag == league.Name)
                        {
                            thatForm.LeagueResultsEnteredCallback = dialog.LeagueResultsEntered;
                            return;
                        }
                    }
                }

                dialog.FormClosed += frmViewLeague_FormClosed;
                dialog.MdiParent = MdiParent;
                dialog.Show();
                ActiveLeagueWindows.Add(dialog);
            }
        }

        private void frmViewTournament_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActiveTournamentWindows.Remove((frmViewTournament)sender);
            btnExport.Enabled = false;
            FilterList();
        }

        private void frmViewLeague_FormClosed(object sender, FormClosedEventArgs e)
        {
            ActiveLeagueWindows.Remove((FrmViewLeague)sender);
            btnExport.Enabled = false;
            FilterList();
        }

        private void deleteEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to delete this Event? This action cannot be reversed!",
                                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
                Config.Settings.DeleteTournament(lstEvents.SelectedItems[0].Text);
            else
                Config.Settings.DeleteLeague(lstEvents.SelectedItems[0].Text);
            lstEvents.Items.Remove(lstEvents.SelectedItems[0]);
        }

        private void archiveEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = lstEvents.SelectedItems[0].Text;
            string extension = lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament"
                                   ? ".tournament.dat"
                                   : ".league.dat";
            string sourceFile = Config.ScrubFilename(name + extension);
            if (File.Exists(Path.Combine(Config.Settings.ArchivePath, sourceFile)))
                if (MessageBox.Show("This Event was previously archived. Overwrite?", "Confirmation",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                    DialogResult.No)
                    return;

            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
                Config.Settings.ArchiveTournament(name);
            else
                Config.Settings.ArchiveLeague(name);

            if (MessageBox.Show("This Event was successfully archived. Would you like to remove it?", "Confirmation",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                DialogResult.No)
                return;
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
                Config.Settings.DeleteTournament(name);
            else
                Config.Settings.DeleteLeague(name);
            lstEvents.Items.Remove(lstEvents.SelectedItems[0]);
        }

        private void lstEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = lstEvents.SelectedItems.Count > 0;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var targets = new Queue<string>();
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".export";
                dialog.ValidateNames = true;
                dialog.Title = "Select the file or files to Import...";
                dialog.RestoreDirectory = true;
                dialog.Multiselect = true;
                dialog.Filter = "Export Files (*.export)|*.export|All Files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.Cancel) return;

                foreach (string filename in dialog.FileNames)
                    targets.Enqueue(filename);
            }

            while (targets.Count > 0)
            {
                string target = targets.Dequeue();
                var xml = new XmlDocument();
                try
                {
                    xml.Load(target);
                }
                catch
                {
                    MessageBox.Show("An error occurred while trying to import " + Path.GetFileName(target) +
                                    ", it may not be an export or it is corrupted.", "Failed to Import",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    continue;
                }

                XmlNode exportNode = xml.SelectSingleNode("//Export");
                if (exportNode == null)
                {
                    MessageBox.Show("An error occurred while trying to import " + Path.GetFileName(target) +
                                    ", it does not contain an exported Event.", "Failed to Import", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    continue;
                }

                XmlNodeList playerNodes = exportNode.SelectNodes("Players/Player");
                var newPlayers = new List<PlayerRecord>();
                if (playerNodes != null)
                {
                    foreach (XmlNode playerNode in playerNodes)
                    {
                        var record = new PlayerRecord(playerNode);
                        if (Config.Settings.GetPlayer(record.ID) == null)
                            newPlayers.Add(record);
                    }
                }

                XmlNode tournamentNode = exportNode.SelectSingleNode("Tournament");
                XmlNode leagueNode = exportNode.SelectSingleNode("League");
                if (tournamentNode != null)
                {
                    Tournament tournament = null;
                    try
                    {
                        tournament = new Tournament(tournamentNode);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred while trying to import " + Path.GetFileName(target) +
                                        ", found a Tournament record but failed to parse it.", "Failed to Import",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        continue;
                    }
                    Tournament tempTournament = Config.Settings.GetTournament(tournament.Name);
                    bool alreadyExists = false;
                    string prefix = "";
                    while (tempTournament != null)
                    {
                        alreadyExists = true;
                        prefix += "!";
                        tempTournament = Config.Settings.GetTournament(prefix + " " + tournament.Name);
                    }

                    string message = "The following information will be imported:\r\n";
                    message += "Tournament: " + tournament.Name + "\r\n";
                    if (alreadyExists)
                        message += "(A Tournament already exists with this name, adding \"" + prefix +
                                   "\" to the name.)\r\n";
                    if (newPlayers.Count > 0)
                        message += "New Players: " + newPlayers.Count.ToString() + " (out of " +
                                   playerNodes.Count.ToString() + " found.)\r\n";
                    message += "\r\nPress OK to import this Event.";

                    if (MessageBox.Show(message, "Confirm Import", MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Information,
                                        MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        continue;

                    foreach (PlayerRecord record in newPlayers)
                        Config.Settings.Players.Add(record);

                    if (prefix.Length > 0) tournament.Name = prefix + " " + tournament.Name;
                    Config.Settings.Tournaments.Add(tournament);
                    Config.Settings.SaveEvents();

                    lstEvents.BeginUpdate();
                    var item = new ListViewItem();
                    item.Name = tournament.Name;
                    item.Text = tournament.Name;
                    item.SubItems.Add("Tournament");
                    item.SubItems.Add(tournament.Location);
                    item.SubItems.Add(tournament.Date.ToShortDateString());
                    item.SubItems.Add(tournament.Players.Count.ToString());
                    if (tournament.Rounds.Count == 0)
                        item.SubItems.Add("Not Started");
                    else if (tournament.Rounds.Count == tournament.TotalRounds &&
                             tournament.Rounds[tournament.TotalRounds - 1].Completed)
                        item.SubItems.Add("Finished");
                    else
                    {
                        int roundNum = tournament.Rounds.Count;
                        if (tournament.Rounds[roundNum - 1].Completed)
                            item.SubItems.Add("Round " + roundNum.ToString() + " of " +
                                              tournament.TotalRounds.ToString() + " Done");
                        else
                            item.SubItems.Add("Round " + roundNum.ToString() + " of " +
                                              tournament.TotalRounds.ToString() + " Active");
                    }
                    lstEvents.Items.Add(item);
                    lstEvents.Sort();
                    lstEvents.EndUpdate();
                }
                else if (leagueNode != null)
                {
                    var pastReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    League league = null;
                    try
                    {
                        league = new League(leagueNode);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred while trying to import " + Path.GetFileName(target) +
                                        ", found a League record but failed to parse it.", "Failed to Import",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        continue;
                    }
                    League tempLeague = Config.Settings.GetLeague(league.Name);
                    bool alreadyExists = false;
                    string prefix = "";
                    while (tempLeague != null)
                    {
                        alreadyExists = true;
                        prefix += "!";
                        tempLeague = Config.Settings.GetLeague(prefix + " " + league.Name);
                    }

                    string message = "The following information will be imported:\r\n";
                    message += "League: " + league.Name + "\r\n";
                    if (alreadyExists)
                        message += "(A League already exists with this name, adding \"" + prefix +
                                   "\" to the name.)\r\n";
                    if (newPlayers.Count > 0)
                        message += "New Players: " + newPlayers.Count.ToString() + " (out of " +
                                   playerNodes.Count.ToString() + " found.)\r\n";
                    message += "\r\nPress OK to import this Event.";

                    if (MessageBox.Show(message, "Confirm Import", MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Information,
                                        MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        continue;

                    foreach (PlayerRecord record in newPlayers)
                        Config.Settings.Players.Add(record);

                    if (prefix.Length > 0) league.Name = prefix + " " + league.Name;
                    Config.Settings.Leagues.Add(league);
                    Config.Settings.SaveEvents();

                    lstEvents.BeginUpdate();

                    var item = new ListViewItem();
                    item.Name = league.Name;
                    item.Text = league.Name;
                    item.SubItems.Add("League");
                    item.SubItems.Add(league.Location);
                    item.SubItems.Add(league.StartDate.ToShortDateString());
                    item.SubItems.Add(league.Players.Count.ToString());
                    if (league.MatchesPlayed.Count == 0)
                        item.SubItems.Add("Not Started");
                    else if (league.EndDate < pastReference)
                        item.SubItems.Add("Finished");
                    else
                    {
                        item.SubItems.Add(league.MatchesPlayed.Count.ToString() + " Result" +
                                          (league.MatchesPlayed.Count == 1 ? "" : "s") + " Entered");
                    }
                    lstEvents.Items.Add(item);
                    lstEvents.Sort();
                    lstEvents.EndUpdate();
                }
                else
                {
                    MessageBox.Show("An error occurred while trying to import " + Path.GetFileName(target) +
                                    ", the Event information was not found.", "Failed to Import", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    continue;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string name = lstEvents.SelectedItems[0].Text;
            EventType eventType = lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament"
                                      ? EventType.Tournament
                                      : EventType.League;

            using (var dialog = new frmExportOptions(name, eventType))
                dialog.ShowDialog();
        }

        private void notesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItems.Count == 0) return;
            string name = lstEvents.SelectedItems[0].Text;

            foreach (Form childForm in ParentForm.MdiChildren)
            {
                if (childForm is frmNotes)
                {
                    if ((string)childForm.Tag == name)
                    {
                        childForm.WindowState = FormWindowState.Normal;
                        childForm.Focus();
                        return;
                    }
                }
            }

            var notes = new frmNotes(name);
            notes.MdiParent = ParentForm;
            notes.Show();
        }

        private void sendPreEventReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var recipient = new MailAddress("office.vcen@wyrd-games.net", "Henchman Office");
            int stones = 0;
            string message = "";
            string name = lstEvents.SelectedItems[0].Text;
            string eventType = lstEvents.SelectedItems[0].SubItems[1].Text;

            if (eventType == "Tournament")
            {
                Tournament tournament = Config.Settings.GetTournament(name);
                if (tournament.PreEventSent)
                    if (MessageBox.Show("You have already sent a Pre-Event Report for this Event. Are you sure you " +
                                        "want to send the report again?", "Confirmation", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                foreach (var id in tournament.PlayerIDs)
                    if (Config.Settings.GetPlayer(id).Name.ToLower() != Config.Settings.SMTPFromName.ToLower())
                        stones += 2;

                if (stones > 30) stones = 30;

                message += "Real Name: " + Config.Settings.SMTPFromName + "\r\n";
                message += "Forum Name: " + Config.Settings.ForumName + "\r\n";
                message += "Event Type: Tournament\r\n";
                message += "Date and Time of Event: " + tournament.Date.ToString("yyyy-MM-dd hh:mm tt") + "\r\n";
                message += "Location of Event: " + tournament.Location + "\r\n";
                message += "Estimated Soulstones: " + stones.ToString() + " Soulstones\r\n";
                message += "Link to Event on the Wyrd Forums: " + (tournament.ForumURL.Length > 0
                                                                       ? tournament.ForumURL
                                                                       : "[INSERT LINK HERE]") + "\r\n";
            }
            else
            {
                League league = Config.Settings.GetLeague(name);
                if (league.PreEventSent)
                    if (MessageBox.Show("You have already sent a Pre-Event Report for this Event. Are you sure you " +
                                        "want to send the report again?", "Confirmation", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                string leagueType = "";
                if (league.Achievements.Count > 0 && league.Format != EventFormat.None)
                    leagueType = "Achievement and Competitive League";
                else if (league.Achievements.Count > 0)
                    leagueType = "Achievement League";
                else
                    leagueType = "Competitive League";

                //foreach (string ID in league.Players)
                //    if (Config.Settings.GetPlayer(ID).Name.ToLower() != Config.Settings.SMTPFromName.ToLower())
                //        stones += 3;

                //if (stones > 45) stones = 45;
                message += "Real Name: " + Config.Settings.SMTPFromName + "\r\n";
                message += "Forum Name: " + Config.Settings.ForumName + "\r\n";
                message += "Event Type: " + leagueType + "\r\n";
                message += "Dates of Event: " + league.StartDate.ToString("yyyy-MM-dd");
                if (league.StartDate.ToString("yyyyMMdd") == league.EndDate.ToString("yyyyMMdd"))
                    message += " (One Day League)\r\n";
                else
                    message += " through " + league.EndDate.ToString("yyyy-MM-dd") + "\r\n";
                message += "Location of Event: " + league.Location + "\r\n";
                //message += "Estimated Soulstones: " + stones.ToString() + " Soulstones\r\n";
                message += "Estimated Soulstones: [3 Soulstones per Player] (maximum 45)\r\n";
                message += "Link to Event on the Wyrd Forums: " + (league.ForumURL.Length > 0
                                                                       ? league.ForumURL
                                                                       : "[INSERT LINK HERE]") + "\r\n";
            }

            var dialog = new frmSendEmail("Pre Event Report: " + name, "Please verify ALL information is " +
                                                                        "accurate and filled in! Once you have confirmed the details, " +
                                                                        "and added any further notes of your " +
                                                                        "own if desired, press Send.", recipient,
                                          "Pre Event report: " + name, null, message);
            DialogResult result = dialog.ShowDialog();
            dialog.Close();
            if (result == DialogResult.OK)
            {
                if (eventType == "Tournament")
                {
                    Tournament tournament = Config.Settings.GetTournament(name);
                    tournament.PreEventSent = true;
                }
                else
                {
                    League league = Config.Settings.GetLeague(name);
                    league.PreEventSent = true;
                }
                Config.Settings.SaveEvents();
            }
        }

        private void sendPostEventReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var recipient = new MailAddress("office.vcen@wyrd-games.net", "Henchman Office");
            int stones = 0;
            string message = "";
            string name = lstEvents.SelectedItems[0].Text;
            string eventType = lstEvents.SelectedItems[0].SubItems[1].Text;

            if (eventType == "Tournament")
            {
                Tournament tournament = Config.Settings.GetTournament(name);
                if (!tournament.Completed)
                    if (MessageBox.Show("This Tournament is not registered as having completed yet. Are you sure you " +
                                        "wish to send a Post Event report?", "Event Not Completed",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                foreach (var id in tournament.Players.Keys)
                    if (Config.Settings.GetPlayer(id).Name.ToLower() != Config.Settings.SMTPFromName.ToLower())
                        stones += 2;

                if (stones > 30) stones = 30;

                message += "Real Name: " + Config.Settings.SMTPFromName + "\r\n";
                message += "Forum Name: " + Config.Settings.ForumName + "\r\n";
                message += "Event Type: Tournament\r\n";
                message += "Date and Time of Event: " + tournament.Date.ToString("yyyy-MM-dd hh:mm tt") + "\r\n";
                message += "Location of Event: " + tournament.Location + "\r\n";
                message += "Soulstones Earned: " + stones.ToString() + " Soulstones\r\n";
                message += "Link to Event on the Wyrd Forums: " + (tournament.ForumURL.Length > 0
                                                                       ? tournament.ForumURL
                                                                       : "[INSERT LINK HERE]") + "\r\n";
            }
            else
            {
                var pastReference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                League league = Config.Settings.GetLeague(name);
                if (league.EndDate >= pastReference)
                    if (MessageBox.Show("This League's end date has not passed yet. Are you sure you " +
                                        "wish to send a Post Event report?", "Event Not Completed",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                string leagueType = "";
                if (league.Achievements.Count > 0 && league.Format != EventFormat.None)
                    leagueType = "Achievement and Competitive League";
                else if (league.Achievements.Count > 0)
                    leagueType = "Achievement League";
                else
                    leagueType = "Competitive League";

                foreach (string ID in league.Players)
                    if (Config.Settings.GetPlayer(ID).Name.ToLower() != Config.Settings.SMTPFromName.ToLower())
                        stones += 3;

                if (stones > 45) stones = 45;
                message += "Real Name: " + Config.Settings.SMTPFromName + "\r\n";
                message += "Forum Name: " + Config.Settings.ForumName + "\r\n";
                message += "Event Type: " + leagueType + "\r\n";
                message += "Dates of Event: " + league.StartDate.ToString("yyyy-MM-dd");
                if (league.StartDate.ToString("yyyyMMdd") == league.EndDate.ToString("yyyyMMdd"))
                    message += " (One Day League)\r\n";
                else
                    message += " through " + league.EndDate.ToString("yyyy-MM-dd") + "\r\n";
                message += "Location of Event: " + league.Location + "\r\n";
                message += "Soulstones Earned: " + stones.ToString() + " Soulstones\r\n";
                message += "Link to Event on the Wyrd Forums: " + (league.ForumURL.Length > 0
                                                                       ? league.ForumURL
                                                                       : "[INSERT LINK HERE]") + "\r\n";
            }

            var dialog = new frmSendEmail("Post Event Report: " + name, "Please verify ALL information is " +
                                                                        "accurate and filled in! Once you have confirmed the details, " +
                                                                        "and added any further notes of your " +
                                                                        "own if desired, press Send.", recipient,
                                          "Post Event report: " + name, null, message);
            DialogResult result = dialog.ShowDialog();
            dialog.Close();
            if (result == DialogResult.OK)
            {
                if (eventType == "Tournament")
                {
                    Tournament tournament = Config.Settings.GetTournament(name);
                    tournament.PostEventSent = true;
                }
                else
                {
                    League league = Config.Settings.GetLeague(name);
                    league.PostEventSent = true;
                }
                Config.Settings.SaveEvents();
            }
        }

        private void mnuOpenForum_Click(object sender, EventArgs e)
        {
            string url = "";
            string name = lstEvents.SelectedItems[0].Text;
            if (lstEvents.SelectedItems[0].SubItems[1].Text == "Tournament")
                url = Config.Settings.GetTournament(name).ForumURL;
            else
                url = Config.Settings.GetLeague(name).ForumURL;

            Process.Start(url);
        }

        private void mnuRecordMatch_Click(object sender, EventArgs e)
        {
            string name = lstEvents.SelectedItems[0].Text;
            League league = Config.Settings.GetLeague(name);

            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form is frmEnterLeagueResults)
                {
                    var thatForm = (frmEnterLeagueResults)form;
                    if ((string)thatForm.Tag == name)
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.Focus();
                        return;
                    }
                }
            }

            var dialog = new frmEnterLeagueResults(league);
            dialog.MdiParent = MdiParent;
            dialog.Show();
        }

        private void outputResultsToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var name = lstEvents.SelectedItems[0].Text;
            using (var dialog = new FrmExportTournamentResults(name))
                dialog.ShowDialog();
        }

        private void chkHidePast_CheckedChanged(object sender, EventArgs e)
        {
            FilterList();
        }
    }

    public delegate void PlayersAddedHandler(object sender, TournamentPlayersEventArgs e);

    public class TournamentPlayersEventArgs : EventArgs
    {
        public TournamentPlayersEventArgs()
        {
            Name = "";
            Enrolled = new Dictionary<string, int>();
            ChosenFactions = new Dictionary<string, Factions>();
            Callback = null;
            Cancel = true;
        }

        public TournamentPlayersEventArgs(string name, Dictionary<string, int> players, Dictionary<string, Factions> factions, PlayersAddedHandler callback)
            : this()
        {
            Name = name;
            Enrolled = players;
            ChosenFactions = factions;
            Callback = callback;
        }

        public string Name { get; private set; }
        public Dictionary<string, int> Enrolled { get; set; }
        public Dictionary<string, Factions> ChosenFactions { get; set; } 
        public PlayersAddedHandler Callback { get; set; }
        public bool Cancel { get; set; }
    }
}