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
    public partial class frmTournamentRound : Form
    {
        /// <summary>
        /// The name of the Tournament associated with this Round.
        /// </summary>
        public string TournamentName
        {
            get
            {
                return ThisRound.TournamentName;
            }
        }

        /// <summary>
        /// Returns the TournamentRound being managed by this form.
        /// </summary>
        public TournamentRound Round
        { 
            get
            {
                // Update the TournamentMatch to have the values we expect.
                foreach (ctlTournamentMatch match in pnlMatches.Controls)
                {
                    foreach (TournamentMatch record in ThisRound.Matches)
                    {
                        if (match.Match.Players[0] == record.Players[0])
                        {
                            if (match.ScoresLocked) match.Match.CalculateResults();
                            record.Results = match.Match.Results;
                            break;
                        }
                    }
                }
                return ThisRound;
            } 
        }

        public int RoundNumber { get; private set; }

        private TournamentRound ThisRound;
        private frmTimer MyTimer = null;
        private bool RevisionMode { get; set; }

        /// <summary>
        /// Creates a new instance of the frmTournamentRound form.
        /// </summary>
        /// <param name="round">The TournamentRound to display on this form.</param>
        /// <param name="roundNumber">The round number to display on this form.</param>
        /// <param name="revisionMode">If true, the round is in Revision Mode.</param>
        public frmTournamentRound(TournamentRound round, int roundNumber, bool revisionMode = false)
        {
            InitializeComponent();

            ThisRound = round;
            RoundNumber = roundNumber;
            RevisionMode = revisionMode;

            Text = round.TournamentName + " -- Round " + roundNumber;
            int matchNumber = Config.Settings.GetTournament(round.TournamentName).TableNumbering == TableNumbering.Even ? 2 : 1;
            int openMatches = 0;
            foreach (TournamentMatch match in ThisRound.Matches)
            {
                var matchControl = new ctlTournamentMatch(match, matchNumber);
                matchNumber += Config.Settings.GetTournament(round.TournamentName).TableNumbering == TableNumbering.Normal ? 1 : 2; 
                matchControl.MatchLockChanged += MatchControl_MatchLockChanged;
                if (match.Results.Count > 0) matchControl.LockScores();
                if (!matchControl.ScoresLocked) openMatches++;
                pnlMatches.Controls.Add(matchControl);
            }
            if (RevisionMode)
            {
                lblMatches.Text = "Viewing and Editing Round " + roundNumber;
                mnuOptions.Visible = false;
            }
            else
                lblMatchesLeft.Text = openMatches.ToString();
        }

        private void UpdateForm()
        {
            if (!RevisionMode)
            {
                int openMatches = 0;
                foreach (ctlTournamentMatch match in pnlMatches.Controls)
                    if (!match.ScoresLocked) openMatches++;

                lblMatchesLeft.Text = openMatches.ToString();
                btnFinalize.Visible = (openMatches == 0);
            }

            // Capture the current state of the Tournament Round.
            Config.Settings.GetTournament(TournamentName).Rounds[
                Config.Settings.GetTournament(TournamentName).Rounds.Count - 1] = ThisRound;
            Config.Settings.SaveEvents();
        }

        private void MatchControl_MatchLockChanged(object sender, MatchLockChangedEventArgs e)
        {
            UpdateForm();
        }

        private void btnLockAllMatches_Click(object sender, EventArgs e)
        {
            foreach (ctlTournamentMatch match in pnlMatches.Controls)
                if (!match.ScoresLocked) match.LockScores();
            UpdateForm();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (MyTimer == null)
            {
                // It's possible the timer was opened previously. Go look for it.
                foreach (Form form in this.MdiParent.MdiChildren)
                {
                    if (form is frmTimer)
                    {
                        frmTimer timer = (frmTimer)form;
                        if (timer.Text == "Timer: " + ThisRound.TournamentName + " -- Time Remaining")
                        {
                            MyTimer = timer;
                            MyTimer.WindowState = FormWindowState.Normal;
                            MyTimer.Focus();
                            return;
                        }
                    }
                }

                MyTimer = new frmTimer(ThisRound.TournamentName + " -- Time Remaining", ThisRound.Length);
                MyTimer.FormClosed += MyTimer_FormClosed;
                MyTimer.Show();
            }
            else
            {
                MyTimer.WindowState = FormWindowState.Normal;
                MyTimer.Focus();
            }
        }

        void MyTimer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyTimer = null;
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you ready to record the scores for this round?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            ThisRound.FinalizeRound();
            this.Close();
        }

        public void UpdateMatch(ctlTournamentMatch match, string player1VP, string player2VP)
        {
            foreach (ctlTournamentMatch matchCtl in pnlMatches.Controls)
            {
                if (matchCtl.Player1ID == match.Player1ID && matchCtl.Player2ID == match.Player2ID)
                {
                    if (player1VP != "F")
                        matchCtl.Player1VictoryPoints = Convert.ToInt32(player1VP);
                    else
                        matchCtl.Player1Forfeit = true;
                    if (player2VP != "F")
                        matchCtl.Player2VictoryPoints = Convert.ToInt32(player2VP);
                    else
                        matchCtl.Player2Forfeit = true;
                    matchCtl.LockScores();
                    UpdateForm();
                    break;
                }
            }
        }
    }
}
