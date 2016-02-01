using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class frmPlayerResults : Form
    {
        public string PlayerID { get; set; }

        public frmPlayerResults(string id)
        {
            InitializeComponent();
            PlayerID = id;

            PlayerRecord record = Config.Settings.GetPlayer(PlayerID);
            this.Text = record.Name + " -- Player Record";
            lblName.Text = record.Name;

            MatchResult overallResults = new MatchResult(PlayerID);

            lstEvents.BeginUpdate();
            foreach (Tournament tournament in Config.Settings.Tournaments)
            {
                if (!tournament.Players.ContainsKey(PlayerID)) continue;
                MatchResult thisResults = tournament.GetPlayerResults(PlayerID);
                var item = new ListViewItem
                    {
                        UseItemStyleForSubItems = false,
                        Text = tournament.Date.ToShortDateString()
                    };
                item.SubItems.Add(tournament.Name);
                item.SubItems.Add("Tournament");
                item.SubItems.Add(tournament.Location);
                var TPs = new ListViewItem.ListViewSubItem();
                var VPs = new ListViewItem.ListViewSubItem();
                var Diff = new ListViewItem.ListViewSubItem();
                if (thisResults.Forfeited)
                {
                    TPs.Text = "F";
                    TPs.BackColor = Color.Crimson;
                    VPs.Text = "F";
                    VPs.BackColor = Color.Crimson;
                    Diff.Text = "F";
                    Diff.BackColor = Color.Crimson;
                }
                else
                {
                    overallResults.TournamentPoints += thisResults.TournamentPoints;
                    overallResults.VictoryPoints += thisResults.VictoryPoints;
                    overallResults.Differential += thisResults.Differential;

                    TPs.Text = thisResults.TournamentPoints.ToString();
                    if (tournament.Format == EventFormat.Domination)
                        TPs.BackColor = Color.LightGreen;
                    VPs.Text = thisResults.VictoryPoints.ToString();
                    if (tournament.Format == EventFormat.Victory)
                        VPs.BackColor = Color.LightGreen;
                    Diff.Text = thisResults.Differential.ToString();
                    if (tournament.Format == EventFormat.Disparity)
                        Diff.BackColor = Color.LightGreen;
                }
                item.SubItems.Add(TPs);
                item.SubItems.Add(VPs);
                item.SubItems.Add(Diff);
                lstEvents.Items.Add(item);
            }
            int overallPoints = 0;
            foreach (League league in Config.Settings.Leagues)
            {
                if (league.Players.Contains(PlayerID))
                {
                    LeagueResults thisResults = league.GetPlayerResults(PlayerID);
                    ListViewItem item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    item.Text = league.EndDate.ToShortDateString();
                    item.SubItems.Add(league.Name);
                    item.SubItems.Add("League");
                    item.SubItems.Add(league.Location);
                    ListViewItem.ListViewSubItem TPs = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem VPs = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem Diff = new ListViewItem.ListViewSubItem();

                    overallPoints += thisResults.PointsEarned();
                    overallResults.Achievements.AddRange(thisResults.Achievements);
                    overallResults.TournamentPoints += thisResults.MatchResults.TournamentPoints;
                    overallResults.VictoryPoints += thisResults.MatchResults.VictoryPoints;
                    overallResults.Differential += thisResults.MatchResults.Differential;

                    TPs.Text = thisResults.MatchResults.TournamentPoints.ToString();
                    if (league.Format == EventFormat.Domination)
                        TPs.BackColor = Color.LightGreen;
                    VPs.Text = thisResults.MatchResults.VictoryPoints.ToString();
                    if (league.Format == EventFormat.Victory)
                        VPs.BackColor = Color.LightGreen;
                    Diff.Text = thisResults.MatchResults.Differential.ToString();
                    if (league.Format == EventFormat.Disparity)
                        Diff.BackColor = Color.LightGreen;

                    item.SubItems.Add(TPs);
                    item.SubItems.Add(VPs);
                    item.SubItems.Add(Diff);
                    lstEvents.Items.Add(item);
                }
            }
            lstEvents.EndUpdate();

            lblRecord.Text = overallResults.TournamentPoints.ToString() + " TP/ " +
                overallResults.VictoryPoints.ToString() + " VP/ " + (overallResults.Differential > 0 ? "+" : "") +
                overallResults.Differential.ToString() + " Diff\r\n" + overallResults.Achievements.Count.ToString() + 
                " Achievements, " + overallPoints.ToString() + " Points Across " + lstEvents.Items.Count.ToString() +
                " Event" + (lstEvents.Items.Count == 1 ? "" : "s");
        }

        private void lstTournaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSelected.Items.Clear();
            if (lstEvents.SelectedItems.Count == 0)
                return;

            string name = lstEvents.SelectedItems[0].SubItems[1].Text;
            if (lstEvents.SelectedItems[0].SubItems[2].Text == "Tournament")
                ShowTournament(name);
            else
                ShowLeague(name);
        }

        private void ShowTournament(string name)
        {
            Tournament tournament = Config.Settings.GetTournament(name);

            lstSelected.BeginUpdate();
            if ((string)lstSelected.Tag != "Tournament")
            {
                lstSelected.Columns.Clear();
                lstSelected.Columns.Add("Round", 45);
                lstSelected.Columns.Add("TPs", 40);
                lstSelected.Columns.Add("VPs", 40);
                lstSelected.Columns.Add("Diff", 40);
                lstSelected.Columns.Add("Opponent", 235);
                lstSelected.Columns.Add("TPs", 40);
                lstSelected.Columns.Add("VPs", 40);
                lstSelected.Columns.Add("Diff", 40);
                lstSelected.Tag = "Tournament";
            }

            int roundNum = 1;
            foreach (TournamentRound round in tournament.Rounds)
            {
                foreach (TournamentMatch match in round.Matches)
                {
                    if (match.Players.Contains(PlayerID) && match.Results.Count > 0)
                    {
                        MatchResult myResult = match.Results[PlayerID];
                        ListViewItem item = new ListViewItem();
                        item.UseItemStyleForSubItems = false;
                        item.Text = roundNum.ToString();
                        if (myResult.Bye)
                        {
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("Bye Round");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                        }
                        else
                        {
                            string theirID = null;
                            if (match.Players[0] != PlayerID)
                                theirID = match.Players[0];
                            else
                                theirID = match.Players[1];

                            PlayerRecord opponent = Config.Settings.GetPlayer(theirID);
                            MatchResult theirResult = match.Results[theirID];

                            if (myResult.Forfeited)
                            {
                                item.SubItems.Add("F");
                                item.SubItems.Add("F");
                                item.SubItems.Add("F");
                                item.SubItems[1].BackColor = Color.Crimson;
                                item.SubItems[2].BackColor = Color.Crimson;
                                item.SubItems[3].BackColor = Color.Crimson;
                            }
                            else if (theirResult.Forfeited)
                            {
                                item.SubItems.Add("3");
                                item.SubItems.Add("8");
                                item.SubItems.Add("+8");
                            }
                            else
                            {
                                item.SubItems.Add(myResult.TournamentPoints.ToString());
                                item.SubItems.Add(myResult.VictoryPoints.ToString());
                                item.SubItems.Add((myResult.Differential > 0 ? "+" : "") + myResult.Differential.ToString());
                            }
                            item.SubItems.Add(opponent.Name);
                            if (myResult.Forfeited)
                            {
                                item.SubItems.Add("3");
                                item.SubItems.Add("8");
                                item.SubItems.Add("+8");
                            }
                            else if (theirResult.Forfeited)
                            {
                                item.SubItems.Add("F");
                                item.SubItems.Add("F");
                                item.SubItems.Add("F");
                                item.SubItems[1].BackColor = Color.Crimson;
                                item.SubItems[2].BackColor = Color.Crimson;
                                item.SubItems[3].BackColor = Color.Crimson;
                            }
                            else
                            {
                                item.SubItems.Add(theirResult.TournamentPoints.ToString());
                                item.SubItems.Add(theirResult.VictoryPoints.ToString());
                                item.SubItems.Add((theirResult.Differential > 0 ? "+" : "") + theirResult.Differential.ToString());
                            }
                        }
                        lstSelected.Items.Add(item);
                    }
                }
                roundNum++;
            }
            lstSelected.EndUpdate();
        }

        private void ShowLeague(string name)
        {
            League league = Config.Settings.GetLeague(name);

            lstSelected.BeginUpdate();
            if ((string)lstSelected.Tag != "League")
            {
                lstSelected.Columns.Clear();
                lstSelected.Columns.Add("Match", 45);
                lstSelected.Columns.Add("# Earned", 60);
                lstSelected.Columns.Add("Points", 60);
                lstSelected.Columns.Add("TPs", 40);
                lstSelected.Columns.Add("VPs", 40);
                lstSelected.Columns.Add("Diff", 40);
                lstSelected.Tag = "League";
            }

            int matchNum = 0;
            foreach (MatchResult result in league.MatchesPlayed)
            {
                if (result.PlayerID == PlayerID)
                {
                    matchNum++;
                    int points = 0;
                    foreach (Achievement achievement in result.Achievements)
                    {
                        if (achievement.MustEarnAllToGetPoints)
                        {
                            if (achievement.Earned == achievement.MaxAllowed)
                                if (achievement.PointsEarnedEachTime)
                                    points += (achievement.Points * achievement.Earned);
                                else
                                    points += achievement.Points;
                        }
                        else
                        {
                            if (achievement.PointsEarnedEachTime)
                                points += (achievement.Points * achievement.Earned);
                            else if (achievement.Earned > 0)
                                points += achievement.Points;
                        }
                    }

                    ListViewItem item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    item.Text = matchNum.ToString();
                    item.SubItems.Add(result.Achievements.Count.ToString());
                    item.SubItems.Add(points.ToString());
                    item.SubItems.Add(result.TournamentPoints.ToString());
                    item.SubItems.Add(result.VictoryPoints.ToString());
                    item.SubItems.Add((result.Differential > 0 ? "+" : "") + result.Differential.ToString());

                    lstSelected.Items.Add(item);
                }
            }
            lstSelected.EndUpdate();
        }
    }
}
