using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace KitchenGeeks
{
    public partial class FrmViewLeague : Form
    {
        public FrmViewLeague(string name)
        {
            InitializeComponent();

            League = Config.Settings.GetLeague(name);
            Text = League.Name + " -- View League";

            // Set up the three score fields based on their priority, as determined by the Format.
            var numColumn = new ColumnHeader();
            numColumn.Text = "# Earned";
            numColumn.Name = "Quantity";
            numColumn.Width = 60;
            var pointsColumn = new ColumnHeader();
            pointsColumn.Text = "Points";
            pointsColumn.Name = "Points";
            pointsColumn.Width = 45;

            if (League.Achievements.Count > 0)
            {
                lstResults.Columns.Add(numColumn);
                lstResults.Columns.Add(pointsColumn);
            }

            var tpColumn = new ColumnHeader();
            tpColumn.Text = "TPs";
            tpColumn.Name = "TP";
            tpColumn.Width = 40;
            var vpColumn = new ColumnHeader();
            vpColumn.Text = "VPs";
            vpColumn.Name = "VP";
            vpColumn.Width = 40;
            var diffColumn = new ColumnHeader();
            diffColumn.Text = "Diff";
            diffColumn.Name = "Diff";
            diffColumn.Width = 40;

            switch (League.Format)
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

            foreach (string ID in League.Players)
                AddPlayer(ID);

            // Only enable the ranking options if we're following both achievements and match results.
            if (League.Achievements.Count > 0 && League.Format != EventFormat.None)
            {
                grpRankBy.Enabled = true;
                rdoAchievement.Checked = true;
                lstResults.ListViewItemSorter = new LeagueRankItemComparer();
            }
            else if (League.Achievements.Count > 0)
            {
                grpRankBy.Enabled = false;
                rdoAchievement.Checked = true;
                lstResults.ListViewItemSorter = new LeagueRankItemComparer();
            }
            else
            {
                grpRankBy.Enabled = false;
                rdoMatch.Checked = true;
                lstResults.ListViewItemSorter = new LeagueRankItemComparer(League.Format);
            }

            lstResults.Sort();

            // Gets around a weird bug where the Items list only refreshes between method calls.
            var initialRank = new Timer(InitialRanks, null, 1, Timeout.Infinite);
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public League League { get; set; }

        private void InitialRanks(object sender)
        {
            lstResults.Invoke(new MethodInvoker(delegate { SetRanks(); }));
        }

        private void AddPlayer(string ID)
        {
            PlayerRecord player = Config.Settings.GetPlayer(ID);
            var item = new ListViewItem();
            item.Text = "";
            item.Name = ID;
            item.SubItems.Add(player.Name);
            LeagueResults results = League.GetPlayerResults(ID);
            if (League.Achievements.Count > 0)
            {
                var Quantity = new ListViewItem.ListViewSubItem(item, results.QuantityEarned().ToString());
                Quantity.Name = "Quantity";
                item.SubItems.Add(Quantity);
                var Points = new ListViewItem.ListViewSubItem(item, results.PointsEarned().ToString());
                Points.Name = "Points";
                item.SubItems.Add(Points);
            }
            if (League.Format != EventFormat.None)
            {
                var TP = new ListViewItem.ListViewSubItem(item, results.MatchResults.TournamentPoints.ToString());
                TP.Name = "TP";
                var VP = new ListViewItem.ListViewSubItem(item, results.MatchResults.VictoryPoints.ToString());
                VP.Name = "VP";
                var Diff = new ListViewItem.ListViewSubItem(item, results.MatchResults.Differential.ToString());
                Diff.Name = "Diff";
                switch (League.Format)
                {
                    case EventFormat.Domination:
                        item.SubItems.Add(TP);
                        item.SubItems.Add(Diff);
                        item.SubItems.Add(VP);
                        break;
                    case EventFormat.Disparity:
                        item.SubItems.Add(Diff);
                        item.SubItems.Add(VP);
                        item.SubItems.Add(TP);
                        break;
                    case EventFormat.Victory:
                        item.SubItems.Add(VP);
                        item.SubItems.Add(TP);
                        item.SubItems.Add(Diff);
                        break;
                }
            }
            lstResults.Items.Add(item);
        }

        private void UpdateScores()
        {
            lstResults.BeginUpdate();

            foreach (string ID in League.Players)
            {
                if (lstResults.Items.IndexOfKey(ID) == -1)
                    AddPlayer(ID);

                ListViewItem item = lstResults.Items[ID];
                item.Text = "";
                item.Name = ID;

                LeagueResults current = League.GetPlayerResults(ID);
                if (League.Achievements.Count > 0)
                {
                    item.SubItems["Quantity"].Text = current.QuantityEarned().ToString();
                    item.SubItems["Points"].Text = current.PointsEarned().ToString();
                }
                if (League.Format != EventFormat.None)
                {
                    item.SubItems["TP"].Text = current.MatchResults.TournamentPoints.ToString();
                    item.SubItems["VP"].Text = current.MatchResults.VictoryPoints.ToString();
                    item.SubItems["Diff"].Text = current.MatchResults.Differential.ToString();
                }
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
                rank++;
                if (!TiedWithPrevious(index))
                    if (Config.Settings.SkipTiedRanks)
                        displayRank = rank;
                    else
                        displayRank++;
                lstResults.Items[index].Text = displayRank.ToString();
                if (chkHighlight.Checked && rank <= (int) numTopRows.Value)
                    lstResults.Items[index].BackColor = Color.LightGreen;
                else
                    lstResults.Items[index].BackColor = Color.White;
            }
            lstResults.EndUpdate();
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in ParentForm.MdiChildren)
            {
                if (childForm is frmNotes)
                {
                    if ((string) childForm.Tag == League.Name)
                    {
                        childForm.WindowState = FormWindowState.Normal;
                        childForm.Focus();
                        return;
                    }
                }
            }

            var notes = new frmNotes(League.Name);
            notes.MdiParent = ParentForm;
            notes.Show();
        }

        private void rdoAchievement_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAchievement.Checked)
                lstResults.ListViewItemSorter = new LeagueRankItemComparer();
            else
                lstResults.ListViewItemSorter = new LeagueRankItemComparer(League.Format);
            lstResults.Sort();
            SetRanks();
        }

        private bool TiedWithPrevious(int index)
        {
            if (index == 0) return false;

            ListViewItem a = lstResults.Items[index];
            ListViewItem b = lstResults.Items[index - 1];
            int Pointsa = a.SubItems.ContainsKey("Points") ? Convert.ToInt32(a.SubItems["Points"].Text) : 0;
            int Quantitya = a.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(a.SubItems["Quantity"].Text) : 0;
            int TPa = a.SubItems.ContainsKey("TP") ? Convert.ToInt32(a.SubItems["TP"].Text) : 0;
            int VPa = a.SubItems.ContainsKey("VP") ? Convert.ToInt32(a.SubItems["VP"].Text) : 0;
            int Diffa = a.SubItems.ContainsKey("Diff") ? Convert.ToInt32(a.SubItems["Diff"].Text) : 0;
            int Pointsb = b.SubItems.ContainsKey("Points") ? Convert.ToInt32(b.SubItems["Points"].Text) : 0;
            int Quantityb = b.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(b.SubItems["Quantity"].Text) : 0;
            int TPb = b.SubItems.ContainsKey("TP") ? Convert.ToInt32(b.SubItems["TP"].Text) : 0;
            int VPb = b.SubItems.ContainsKey("VP") ? Convert.ToInt32(b.SubItems["VP"].Text) : 0;
            int Diffb = b.SubItems.ContainsKey("Diff") ? Convert.ToInt32(b.SubItems["Diff"].Text) : 0;

            return (Pointsa == Pointsb && Quantitya == Quantityb && TPa == TPb && VPa == VPb && Diffa == Diffb);
        }

        private void btnEnterResults_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form is frmEnterLeagueResults)
                {
                    var thatForm = (frmEnterLeagueResults) form;
                    if ((string) thatForm.Tag == League.Name)
                    {
                        thatForm.WindowState = FormWindowState.Normal;
                        thatForm.Focus();
                        return;
                    }
                }
            }

            var EnterForm = new frmEnterLeagueResults(League);
            EnterForm.LeagueResultsEnteredCallback = LeagueResultsEntered;
            EnterForm.MdiParent = MdiParent;
            EnterForm.Show();
        }

        public void LeagueResultsEntered(object sender, LeagueResultsEventArgs e)
        {
            League.MatchesPlayed.Add(e.MatchResult);
            Config.Settings.SaveEvents();
            lstResults.Invoke(new MethodInvoker(delegate { UpdateScores(); }));
        }

        private void frmViewLeague_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Unhook the update method if necessary.
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form is frmEnterLeagueResults)
                {
                    var thatForm = (frmEnterLeagueResults) form;
                    if ((string) thatForm.Tag == League.Name)
                    {
                        thatForm.LeagueResultsEnteredCallback = null;
                        return;
                    }
                }
            }
        }

        private void chkHighlight_CheckedChanged(object sender, EventArgs e)
        {
            numTopRows.Enabled = chkHighlight.Checked;
            SetRanks();
        }

        private void frmViewLeague_SizeChanged(object sender, EventArgs e)
        {
            lstResults.Width = Width - 255;
        }

        private void btnExportResults_Click(object sender, EventArgs e)
        {
            using (var dialog = new FrmExportLeagueResults(League.Name))
                dialog.ShowDialog();
        }
    }

    public class LeagueRankItemComparer : IComparer
    {
        private readonly bool RankByPoints;
        private readonly EventFormat format;

        public LeagueRankItemComparer()
        {
            format = EventFormat.None;
            RankByPoints = true;
        }

        public LeagueRankItemComparer(EventFormat tournamentFormat)
        {
            format = tournamentFormat;
            RankByPoints = false;
        }

        public int Compare(object x, object y)
        {
            var a = (ListViewItem) x;
            var b = (ListViewItem) y;

            int Pointsa = a.SubItems.ContainsKey("Points") ? Convert.ToInt32(a.SubItems["Points"].Text) : 0;
            int Quantitya = a.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(a.SubItems["Quantity"].Text) : 0;
            int TPa = a.SubItems.ContainsKey("TP") ? Convert.ToInt32(a.SubItems["TP"].Text) : 0;
            int VPa = a.SubItems.ContainsKey("VP") ? Convert.ToInt32(a.SubItems["VP"].Text) : 0;
            int Diffa = a.SubItems.ContainsKey("Diff") ? Convert.ToInt32(a.SubItems["Diff"].Text) : 0;
            int Pointsb = b.SubItems.ContainsKey("Points") ? Convert.ToInt32(b.SubItems["Points"].Text) : 0;
            int Quantityb = b.SubItems.ContainsKey("Quantity") ? Convert.ToInt32(b.SubItems["Quantity"].Text) : 0;
            int TPb = b.SubItems.ContainsKey("TP") ? Convert.ToInt32(b.SubItems["TP"].Text) : 0;
            int VPb = b.SubItems.ContainsKey("VP") ? Convert.ToInt32(b.SubItems["VP"].Text) : 0;
            int Diffb = b.SubItems.ContainsKey("Diff") ? Convert.ToInt32(b.SubItems["Diff"].Text) : 0;

            if (RankByPoints)
            {
                if (Pointsa != Pointsb)
                    return Pointsb.CompareTo(Pointsa);
                else if (Quantitya != Quantityb)
                    return Quantityb.CompareTo(Quantitya);
                else
                {
                    // Try to tie break with the match results.
                    switch (format)
                    {
                        case EventFormat.Domination:
                            if (TPa != TPb)
                                return TPb.CompareTo(TPa);
                            else if (Diffa != Diffb)
                                return Diffb.CompareTo(Diffa);
                            else if (VPa != VPb)
                                return VPb.CompareTo(VPa);
                            break;

                        case EventFormat.Disparity:
                            if (Diffa != Diffb)
                                return Diffb.CompareTo(Diffa);
                            else if (VPa != VPb)
                                return VPb.CompareTo(VPa);
                            else if (TPa != TPb)
                                return TPb.CompareTo(TPa);
                            break;

                        case EventFormat.Victory:
                            if (VPa != VPb)
                                return VPb.CompareTo(VPa);
                            else if (TPa != TPb)
                                return TPb.CompareTo(TPa);
                            else if (Diffa != Diffb)
                                return Diffb.CompareTo(Diffa);
                            break;
                    }
                }
            }
            else
            {
                switch (format)
                {
                    case EventFormat.Domination:
                        if (TPa != TPb)
                            return TPb.CompareTo(TPa);
                        else if (Diffa != Diffb)
                            return Diffb.CompareTo(Diffa);
                        else if (VPa != VPb)
                            return VPb.CompareTo(VPa);
                        break;

                    case EventFormat.Disparity:
                        if (Diffa != Diffb)
                            return Diffb.CompareTo(Diffa);
                        else if (VPa != VPb)
                            return VPb.CompareTo(VPa);
                        else if (TPa != TPb)
                            return TPb.CompareTo(TPa);
                        break;

                    case EventFormat.Victory:
                        if (VPa != VPb)
                            return VPb.CompareTo(VPa);
                        else if (TPa != TPb)
                            return TPb.CompareTo(TPa);
                        else if (Diffa != Diffb)
                            return Diffb.CompareTo(Diffa);
                        break;
                }

                // Try to tie break with Achievement points.
                if (Pointsa != Pointsb)
                    return Pointsb.CompareTo(Pointsa);
                else if (Quantitya != Quantityb)
                    return Quantityb.CompareTo(Quantitya);
            }

            // Worst case, compare the names.
            return a.SubItems[1].Text.CompareTo(b.SubItems[1].Text);
        }
    }

    public delegate void LeagueResultsEnteredHandler(object sender, LeagueResultsEventArgs e);

    public class LeagueResultsEventArgs : EventArgs
    {
        public LeagueResultsEventArgs(string ID)
        {
            PlayerID = ID;
            Achievements = new List<Achievement>();
            MatchResult = new MatchResult(ID);
        }

        public string PlayerID { get; set; }
        public List<Achievement> Achievements { get; set; }
        public MatchResult MatchResult { get; set; }
    }
}