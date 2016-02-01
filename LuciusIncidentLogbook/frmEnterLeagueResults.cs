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
    public partial class frmEnterLeagueResults : Form
    {
        public LeagueResultsEnteredHandler LeagueResultsEnteredCallback { get; set; }
        private League League = null;

        public frmEnterLeagueResults(League league)
        {
            InitializeComponent();

            League = league;
            this.Text = League.Name + " -- Enter League Results";
            this.Tag = League.Name;

            foreach (PlayerRecord player in Config.Settings.Players)
                cmbName.Items.Add(player.Name);

            lstAchievements.BeginUpdate();
            lstAchievements.ListViewItemSorter = new AchievementListComparer();
            foreach (Achievement achievement in League.Achievements)
            {
                ListViewItem item = new ListViewItem();
                item.Text = achievement.Name;
                item.Tag = achievement;
                item.SubItems.Add(achievement.Category);
                item.SubItems.Add("0");
                lstAchievements.Items.Add(item);
            }
            lstAchievements.Sort();
            lstAchievements.EndUpdate();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string ID = GetPlayerID(cmbName.Text);
            if (ID == null)
            {
                if (MessageBox.Show("\"" + cmbName.Text + "\" appears to be a new player. Add them now?", "New Player",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;
                using (var dialog = new frmCreatePlayer(cmbName.Text))
                {
                    if (dialog.ShowDialog() == DialogResult.Cancel) return;

                    var record = new PlayerRecord();
                    record.FirstName = dialog.FirstName;
                    record.LastName = dialog.LastName;
                    if (dialog.AdditionalInformation)
                    {
                        record.ForumName = dialog.ForumName;
                        record.Email = dialog.Email;
                        record.Hometown = dialog.Hometown;
                        record.Region = dialog.PlayerRegion;
                        record.Faction = dialog.Faction;
                    }

                    Config.Settings.Players.Add(record);
                    Config.Settings.SavePlayers();
                    ID = GetPlayerID(cmbName.Text);

                    cmbName.Items.Add(record.Name);
                }
            }

            LeagueResultsEventArgs results = new LeagueResultsEventArgs(ID);

            foreach (ListViewItem item in lstAchievements.Items)
            {
                if (item.SubItems[2].Text != "0")
                {
                    Achievement achievement = ((Achievement)item.Tag).Clone();
                    achievement.Earned = Convert.ToInt32(item.SubItems[2].Text);
                    results.MatchResult.Achievements.Add(achievement);
                }
            }

            int myVPs = Convert.ToInt32(txtVPs.Text);
            int theirVPs = Convert.ToInt32(txtOpponentVPs.Text);

            results.MatchResult.VictoryPoints = myVPs;
            results.MatchResult.TournamentPoints = myVPs > theirVPs ? 3 : myVPs == theirVPs ? 1 : 0;
            results.MatchResult.Differential = myVPs - theirVPs;
            results.MatchResult.DatePlayed = DateTime.Now;

            if (LeagueResultsEnteredCallback != null) LeagueResultsEnteredCallback(this, results);

            if (chkContinuousAdd.Checked)
            {
                foreach (ListViewItem item in lstAchievements.Items)
                    item.SubItems[2].Text = "0";
                txtVPs.Text = "0";
                txtOpponentVPs.Text = "0";
                cmbName.Text = "";
                cmbName.Focus();
            }
            else
                this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetPlayerID(string name)
        {
            foreach (PlayerRecord record in Config.Settings.Players)
                if (record.Name.ToLower() == name.ToLower())
                    return record.ID;

            return null;
        }

        private void lstAchievements_DoubleClick(object sender, EventArgs e)
        {
            if (lstAchievements.SelectedItems.Count > 0)
            {
                Achievement achievement = (Achievement)lstAchievements.SelectedItems[0].Tag;
                var id = GetPlayerID(cmbName.Text);
                var current = 0;
                if (id != null)
                {
                    var player = Config.Settings.GetPlayer(id);
                    var results = League.GetPlayerResults(id);
                    if (results != null)
                    {
                        var thisAchievement = results.Achievements.FirstOrDefault(ach => ach.Name == achievement.Name);
                        if (thisAchievement != null) current = thisAchievement.Earned;
                    }
                }
                int value = Convert.ToInt32(lstAchievements.SelectedItems[0].SubItems[2].Text);
                value++;
                if (value + current > achievement.MaxAllowed) value = 0;
                lstAchievements.SelectedItems[0].SubItems[2].Text = value.ToString();
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAchievements.BeginUpdate();
            foreach (ListViewItem item in lstAchievements.Items)
                item.ForeColor = Color.Black;

            var id = GetPlayerID(cmbName.Text);
            if (id != null)
            {
                var results = League.GetPlayerResults(id);
                foreach (ListViewItem item in lstAchievements.Items)
                {
                    if (results == null) continue;
                    var achievement = (Achievement)item.Tag;
                    var thisAchievement = results.Achievements.FirstOrDefault(ach => ach.Name == achievement.Name);
                    if (thisAchievement != null && thisAchievement.Earned == achievement.MaxAllowed)
                        item.ForeColor = Color.LightGray;
                }
            }

            lstAchievements.EndUpdate();
        }
    }
}
