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
    public partial class frmViewMatches : Form
    {
        /// <summary>
        /// The name of the Tournament that this Round belongs to.
        /// </summary>
        public string TournamentName { get { return _TournamentName; } }

        private string _TournamentName = null;
        private int roundIndex = -1;

        private TreeNode selected1 = null;
        private TreeNode selected2 = null;

        public frmViewMatches(string name, int round = -1)
        {
            InitializeComponent();
            _TournamentName = name;
            roundIndex = round;
            if (roundIndex == -1) roundIndex = Config.Settings.GetTournament(TournamentName).Rounds.Count - 1;
            this.Text = name + " -- Viewing Round " + (roundIndex + 1).ToString() + " Matches";
            PopulateTree();
        }

        private void PopulateTree()
        {
            treeMatches.BeginUpdate();
            treeMatches.Nodes.Clear();

            int matchNum = 1;
            foreach (TournamentMatch match in Config.Settings.GetTournament(TournamentName).Rounds[roundIndex].Matches)
            {
                TreeNode matchNode = new TreeNode("Match #" + matchNum.ToString());
                matchNode.Name = matchNum.ToString();
                foreach (string id in match.Players)
                {
                    PlayerRecord player = Config.Settings.GetPlayer(id);
                    TreeNode playerNode = new TreeNode(player.Name);
                    playerNode.Name = id.ToString();
                    matchNode.Nodes.Add(playerNode);
                }
                treeMatches.Nodes.Add(matchNode);
                matchNum++;
            }
            treeMatches.ExpandAll();            
            treeMatches.EndUpdate();
            btnSwap.Enabled = false;
            selected1 = null;
            selected2 = null;
        }

        private void treeMatches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Only allow two Nodes to be checked at a time, unselecting the oldest one if another gets selected.
            if (e.Node.Checked)
            {
                if (selected1 == null)
                    selected1 = e.Node;
                else if (selected2 == null)
                    selected2 = e.Node;
                else
                {
                    selected1.Checked = false;
                    selected1 = selected2;
                    selected2 = e.Node;
                }
            }
            else if (selected1 == e.Node)
                selected1 = null;
            else if (selected2 == e.Node)
                selected2 = null;

            btnSwap.Enabled = (selected1 != null && selected2 != null);
        }

        private void treeMatches_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // Only allow player names to be checked.
            if (e.Node.Parent == null) e.Cancel = true;
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Swap " + selected1.Text + " with " + selected2.Text + "?", "Confirmation",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            string player1ID = selected1.Name;
            string player2ID = selected2.Name;

            Config.Settings.GetTournament(TournamentName).SwapPlayers(player1ID, player2ID, roundIndex);
            Config.Settings.SaveEvents();

            PopulateTree();
        }

        private void treeMatches_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}
