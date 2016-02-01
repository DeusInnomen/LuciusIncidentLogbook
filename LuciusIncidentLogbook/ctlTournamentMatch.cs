using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KitchenGeeks
{
    /// <summary>
    /// A specialized control used to display a TournamentMatch pairing, and allows the Victory Points to be
    /// entered and committed.
    /// </summary>
    public partial class ctlTournamentMatch : UserControl
    {
        public event MatchLockChangedHandler MatchLockChanged;

        /// <summary>
        /// The name of the Tournament associated with this Match.
        /// </summary>
        public string TournamentName
        {
            get
            {
                return ThisMatch.TournamentName;
            }
        }

        /// <summary>
        /// If true, the scores have been entered and locked.
        /// </summary>
        public bool ScoresLocked
        {
            get
            {
                return (txtVP1.Enabled == false || ThisMatch.ByeRound);
            }
        }

        /// <summary>
        /// Player 1's ID.
        /// </summary>
        public string Player1ID
        {
            get
            {
                return ThisMatch.Players[0];
            }
        }

        /// <summary>
        /// Player 2's ID.
        /// </summary>
        public string Player2ID
        {
            get
            {
                if (!ThisMatch.ByeRound)
                    return ThisMatch.Players[1];
                else
                    return null;
            }
        }

        /// <summary>
        /// Player 1's Victory Points.
        /// </summary>
        public int Player1VictoryPoints
        {
            get
            {
                return Convert.ToInt32(txtVP1.Text);
            }
            set
            {
                txtVP1.Text = value.ToString();
            }
        }

        /// <summary>
        /// Player 2's Victory Points.
        /// </summary>
        public int Player2VictoryPoints
        {
            get
            {
                return Convert.ToInt32(txtVP2.Text);
            }
            set
            {
                txtVP2.Text = value.ToString();
            }
        }

        /// <summary>
        /// If true, Player 1 has Forfeitted.
        /// </summary>
        public bool Player1Forfeit
        {
            get
            {
                return txtVP1.Text == "F";
            }
            set
            {
                if (value)
                    txtVP1.Text = "F";
                else
                    txtVP1.Text = "0";
            }
        }

        /// <summary>
        /// If true, Player 2 has Forfeitted.
        /// </summary>
        public bool Player2Forfeit
        {
            get
            {
                return txtVP2.Text == "F";
            }
            set
            {
                if (value)
                    txtVP2.Text = "F";
                else
                    txtVP2.Text = "0";
            }
        }

        /// <summary>
        /// If true, Player 1 has a Bye for this round.
        /// </summary>
        public bool Player1Bye
        {
            get { return txtVP1.Text == "B"; }
            set { txtVP1.Text = value ? "B" : "0"; }
        }

        /// <summary>
        /// If true, Player 2 has a Bye for this round.
        /// </summary>
        public bool Player2Bye
        {
            get { return txtVP2.Text == "B"; }
            set { txtVP2.Text = value ? "B" : "0"; }
        }

        /// <summary>
        /// Returns the TournamentMatch object used to create this control, updated with the MatchResults.
        /// </summary>
        public TournamentMatch Match
        {
            get
            {
                if (!ThisMatch.ByeRound)
                {
                    ThisMatch.Results.Clear();
                    if (ScoresLocked)
                    {
                        MatchResult result1 = new MatchResult(ThisMatch.Players[0]);
                        MatchResult result2 = new MatchResult(ThisMatch.Players[1]);
                        if (txtVP1.Text == "F")
                        {
                            result1.Forfeited = true;
                            result2.VictoryPoints = 10;
                        }
                        else if (txtVP2.Text == "F")
                        {
                            result1.VictoryPoints = 10;
                            result2.Forfeited = true;
                        }
                        else if (txtVP1.Text == "B")
                        {
                            result1.Bye = true;
                            result2.Forfeited = true;
                        }
                        else if (txtVP2.Text == "B")
                        {
                            result1.Forfeited = true;
                            result2.Bye = true;
                        }
                        else
                        {
                            try
                            {
                                result1.VictoryPoints = Convert.ToInt32(txtVP1.Text);
                                result2.VictoryPoints = Convert.ToInt32(txtVP2.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Unable to parse scores. Please double check scores");
                            }
                        }
                        ThisMatch.Results.Add(ThisMatch.Players[0], result1);
                        ThisMatch.Results.Add(ThisMatch.Players[1], result2);
                        ThisMatch.CalculateResults();
                    }
                }
                return ThisMatch;
            }
        }

        private TournamentMatch ThisMatch = null;
        private bool IsForcedBye { get; set; }

        /// <summary>
        /// Creates a new instance of the ctlTournamentMatch control.
        /// </summary>
        /// <param name="match">The TournamentMatch object containing the relevant data for this matchup.</param>
        /// <param name="matchNumber">The match number, for display purposes.</param>
        public ctlTournamentMatch(TournamentMatch match, int matchNumber)
        {
            InitializeComponent();
            ThisMatch = match;
            lblMatch.Text = "Match #" + matchNumber.ToString();
            Tournament tournament = Config.Settings.GetTournament(ThisMatch.TournamentName);

            PlayerRecord player1 = Config.Settings.GetPlayer(ThisMatch.Players[0]);
            lblPlayer1.Text = player1.Name;
            MatchResult currentResult1 = tournament.GetPlayerResults(player1.ID);
            switch (tournament.Format)
            {
                case EventFormat.Victory:
                    lblRecord1.Text = "Record: " +
                        currentResult1.VictoryPoints.ToString() + " VP/" +
                        currentResult1.TournamentPoints.ToString() + " TP/" +
                        (currentResult1.Differential > 0 ? "+" : "") +
                        currentResult1.Differential.ToString() + " DIFF";
                    break;

                case EventFormat.Disparity:
                    lblRecord1.Text = "Record: " +
                        (currentResult1.Differential > 0 ? "+" : "") +
                        currentResult1.Differential.ToString() + " DIFF/" +
                        currentResult1.VictoryPoints.ToString() + " VP/" +
                        currentResult1.TournamentPoints.ToString() + " TP";
                    break;

                case EventFormat.Domination:
                    lblRecord1.Text = "Record: " +
                        currentResult1.TournamentPoints.ToString() + " TP/" +
                        (currentResult1.Differential > 0 ? "+" : "") +
                        currentResult1.Differential.ToString() + " DIFF/" +
                        currentResult1.VictoryPoints.ToString() + " VP";
                    break;
            }

            if (match.Results.ContainsKey(match.Players[0]))
                if (match.Results[match.Players[0]].Forfeited)
                    txtVP1.Text = "F";
                else if (match.Results[match.Players[0]].Bye)
                    txtVP1.Text = "B";
                else
                    txtVP1.Text = match.Results[match.Players[0]].VictoryPoints.ToString();

            // Detect a Bye round automatically.
            if (match.Players.Count == 1)
            {
                IsForcedBye = true;
                lblPlayer2.Text = "BYE ROUND";
                txtVP1.Text = "B";
                lblRecord2.Text = "";
                LockScores();
            }
            else
            {
                IsForcedBye = false;
                PlayerRecord player2 = Config.Settings.GetPlayer(ThisMatch.Players[1]);
                lblPlayer2.Text = player2.Name;
                MatchResult currentResult2 = tournament.GetPlayerResults(player2.ID);
                switch (tournament.Format)
                {
                    case EventFormat.Victory:
                        lblRecord2.Text = "Record: " +
                            currentResult2.VictoryPoints + " VP/" +
                            currentResult2.TournamentPoints + " TP/" +
                            (currentResult2.Differential > 0 ? "+" : "") +
                            currentResult2.Differential + " DIFF";
                        break;

                    case EventFormat.Disparity:
                        lblRecord2.Text = "Record: " +
                            (currentResult2.Differential > 0 ? "+" : "") +
                            currentResult2.Differential + " DIFF/" +
                            currentResult2.VictoryPoints + " VP/" +
                            currentResult2.TournamentPoints + " TP";
                        break;

                    case EventFormat.Domination:
                        lblRecord2.Text = "Record: " +
                            currentResult2.TournamentPoints + " TP/" +
                            (currentResult2.Differential > 0 ? "+" : "") +
                            currentResult2.Differential + " DIFF/" +
                            currentResult2.VictoryPoints + " VP";
                        break;
                }

                if (match.Results.ContainsKey(match.Players[1]))
                    if (match.Results[match.Players[1]].Forfeited)
                        txtVP2.Text = "F";
                    else if (match.Results[match.Players[1]].Bye)
                        txtVP2.Text = "B";
                    else
                        txtVP2.Text = match.Results[match.Players[1]].VictoryPoints.ToString();
            }
        }

        private void ctlTournamentMatch_DoubleClick(object sender, EventArgs e)
        {

            // Verify that all scores are kosher before proceeding
            int p1Score = 0;
            bool p1Success = int.TryParse(txtVP1.Text, out p1Score);
            if (p1Success)
                p1Success = p1Score >= 0 && p1Score <= 12;
            int p2Score = 0;
            bool p2Success = int.TryParse(txtVP2.Text, out p2Score);
            if (p2Success)
                p2Success = p2Score >= 0 && p2Score <= 12;
            p1Success = p1Success || txtVP1.Text.ToLower() == "b" || txtVP1.Text.ToLower() == "f";
            p2Success = p2Success || txtVP2.Text.ToLower() == "b" || txtVP2.Text.ToLower() == "f";

            if (IsForcedBye) return;

            if (p1Success && p2Success)
            {
                txtVP1.Enabled = !txtVP1.Enabled;
                txtVP2.Enabled = !txtVP2.Enabled;

                if (txtVP1.Enabled)
                    this.BackColor = Color.LightGreen;
                else
                    this.BackColor = Color.Crimson;
            }
            else
            {
                MessageBox.Show("Unable to parse scores. Please double check scores");
            }

            if (MatchLockChanged != null && p1Success && p2Success)
                MatchLockChanged(this, new MatchLockChangedEventArgs(!txtVP1.Enabled, Match));
        }

        /// <summary>
        /// Represents a method which will handle the MatchLockChanged event being raised.
        /// </summary>
        /// <param name="sender">The object which invoked the event.</param>
        /// <param name="e">The arguments associated with the event.</param>
        public delegate void MatchLockChangedHandler(object sender, MatchLockChangedEventArgs e);

        /// <summary>
        /// Lock the scores for this Match.
        /// </summary>
        public void LockScores()
        {
            txtVP1.Enabled = false;
            txtVP2.Enabled = false;
            this.BackColor = Color.Crimson;
        }

        private void txtVP_Leave(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.TextLength == 0)
                box.Text = "0";
            int value = 0;
            if (!Int32.TryParse(box.Text, out value) && box.Text != "F" && box.Text != "B")
                box.Text = "0";
        }

        private void txtVP_Enter(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            box.SelectionStart = 0;
            box.SelectionLength = box.TextLength;
        }
    }

    /// <summary>
    /// Contains the data related to the MatchLockChanged event being raised.
    /// </summary>
    public class MatchLockChangedEventArgs : EventArgs
    {
        /// <summary>
        /// If true, the Match scores have been locked and should be considered finalized.
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// The relevant TournamentMatch object that raised this event.
        /// </summary>
        public TournamentMatch Match { get; set; }

        /// <summary>
        /// Creates a new instance of the MatchLockChangedEventArgs object.
        /// </summary>
        /// <param name="locked">If true, the Match scores have been locked and should be considered finalized.</param>
        /// <param name="match">The relevant TournamentMatch object that raised this event.</param>
        public MatchLockChangedEventArgs(bool locked, TournamentMatch match)
        {
            Locked = locked;
            Match = match;
        }
    }
}
