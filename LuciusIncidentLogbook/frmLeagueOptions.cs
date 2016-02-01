using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class frmLeagueOptions : Form
    {
        private League _League = new League();
        public League League { get { return _League; } }
        private bool IsNew;

        public frmLeagueOptions(League ToEdit = null, bool isNew = false)
        {
            InitializeComponent();

            if (ToEdit != null) _League = ToEdit;
            IsNew = isNew;

            txtName.Text = League.Name;
            txtLocation.Text = League.Location;
            txtForumURL.Text = League.ForumURL;
            dtStartDate.MinDate = League.StartDate > DateTime.Now ? DateTime.Now : League.StartDate;
            dtStartDate.Value = League.StartDate;
            dtEndDate.MinDate = League.EndDate > DateTime.Now ? DateTime.Now : League.EndDate;
            dtEndDate.Value = League.EndDate;

            lstAchievements.ListViewItemSorter = new AchievementListComparer();
            lstAchievements.BeginUpdate();
            foreach (Achievement achievement in League.Achievements)
            {
                ListViewItem item = new ListViewItem();
                item.Name = achievement.ToString();
                item.Text = achievement.Name;
                item.Tag = achievement;
                item.SubItems.Add(achievement.Category);
                item.SubItems.Add(achievement.MaxAllowed.ToString());
                item.SubItems.Add(achievement.Points.ToString());
                item.SubItems.Add(achievement.PointsEarnedEachTime ? "Yes" : "No");
                item.SubItems.Add(achievement.MustEarnAllToGetPoints ? "Yes" : "No");
                lstAchievements.Items.Add(item);
            }
            lstAchievements.Sort();
            lstAchievements.EndUpdate();

            FillComboBoxes();
            cmbFormat.SelectedItem = League.Format.ToString();
        }

        private void FillComboBoxes()
        {
            string currentFormat = (string)cmbFormat.SelectedItem;
            cmbFormat.Items.Clear();

            foreach (string format in Enum.GetNames(typeof(EventFormat)))
                cmbFormat.Items.Add(format);

            if (currentFormat != null && cmbFormat.Items.Contains(currentFormat))
                cmbFormat.SelectedItem = currentFormat;
            else
                cmbFormat.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<string> categories = new List<string>();
            foreach (ListViewItem item in lstAchievements.Items)
                if (!categories.Contains(item.SubItems[1].Text)) categories.Add(item.SubItems[1].Text);
            categories.Sort();

            Achievement achievement = null;
            while (true)
            {
                using (frmEditAchievement dialog = new frmEditAchievement(categories, achievement))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        achievement = dialog.Achievement;

                        // Make sure we don't already have it.
                        bool found = false;
                        foreach (Achievement a in League.Achievements)
                        {
                            if (a.Name.ToLower() == dialog.Achievement.Name.ToLower())
                            {
                                MessageBox.Show("You already have an achievement of that name. Either edit the existing one or use a different name.",
                                    "Achievement Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                found = true;
                                break;
                            }
                        }

                        if (found)
                            continue;
                        else
                        {                            
                            _League.Achievements.Add(achievement.Clone());

                            // If we just added a new category, put it at the top of the list for convenience sake.
                            if (!categories.Contains(dialog.Achievement.Category)) categories.Insert(0, dialog.Achievement.Category);

                            var item = new ListViewItem
                                {
                                    Name = achievement.ToString(),
                                    Text = achievement.Name,
                                    Tag = achievement
                                };
                            item.SubItems.Add(achievement.Category);
                            item.SubItems.Add(achievement.MaxAllowed.ToString());
                            item.SubItems.Add(achievement.Points.ToString());
                            item.SubItems.Add(achievement.PointsEarnedEachTime ? "Yes" : "No");
                            item.SubItems.Add(achievement.MustEarnAllToGetPoints ? "Yes" : "No");
                            item.Selected = true;

                            lstAchievements.Items.Add(item);
                            lstAchievements.Sort();

                            // If we're looping, reset everything but the category to let the user reenter fast.
                            achievement = new Achievement()
                                {
                                    Category = "",
                                    Earned = 0,
                                    MaxAllowed = 1,
                                    MustEarnAllToGetPoints = false,
                                    Name = "",
                                    Points = 1,
                                    PointsEarnedEachTime = true
                                };
                        }
                    }
                    else
                        return;
                }
                if (!chkContinuousAdd.Checked) return;
            }
        }

        private void lstAchievements_DoubleClick(object sender, EventArgs e)
        {
            if (lstAchievements.SelectedItems.Count > 0)
            {
                ListViewItem selected = lstAchievements.SelectedItems[0];
                Achievement currentAchievement = (Achievement)selected.Tag;

                List<string> categories = new List<string>();
                foreach (ListViewItem item in lstAchievements.Items)
                    if (!categories.Contains(currentAchievement.Category)) categories.Add(currentAchievement.Category);
                using (frmEditAchievement dialog = new frmEditAchievement(categories, currentAchievement))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // See if this achievement was earned before being edited.
                        bool answered = false;
                        foreach (MatchResult match in _League.MatchesPlayed)
                        {
                            foreach (Achievement a in match.Achievements)
                            {
                                if (a.Name.ToLower() == currentAchievement.Name.ToLower())
                                {
                                    if (!answered)
                                    {
                                        if (MessageBox.Show("This achievement has already been earned by at least one player. Existing achievements " +
                                            "will be updated with the new information. Continue?", "Achievement In Use", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                            return;
                                        answered = true;
                                    }
                                    a.Name = dialog.Achievement.Name;
                                    a.Category = dialog.Achievement.Category;
                                    a.MaxAllowed = dialog.Achievement.MaxAllowed;
                                    if (a.Earned > dialog.Achievement.MaxAllowed) a.Earned = dialog.Achievement.MaxAllowed;
                                    a.Points = dialog.Achievement.Points;
                                    a.PointsEarnedEachTime = dialog.Achievement.PointsEarnedEachTime;
                                    a.MustEarnAllToGetPoints = dialog.Achievement.MustEarnAllToGetPoints;
                                }
                            }
                        }

                        foreach (Achievement a in _League.Achievements)
                        {
                            if (a.Name.ToLower() == selected.Text.ToLower())
                            {
                                a.Name = dialog.Achievement.Name;
                                a.Category = dialog.Achievement.Category;
                                a.MaxAllowed = dialog.Achievement.MaxAllowed;
                                a.Points = dialog.Achievement.Points;
                                a.PointsEarnedEachTime = dialog.Achievement.PointsEarnedEachTime;
                                a.MustEarnAllToGetPoints = dialog.Achievement.MustEarnAllToGetPoints;
                                break;
                            }
                        }

                        selected.Name = dialog.Achievement.ToString();
                        selected.Text = dialog.Achievement.Name;
                        selected.Tag = dialog.Achievement;
                        selected.SubItems[1].Text = dialog.Achievement.Category;
                        selected.SubItems[2].Text = dialog.Achievement.MaxAllowed.ToString();
                        selected.SubItems[3].Text = dialog.Achievement.Points.ToString();
                        selected.SubItems[4].Text = dialog.Achievement.PointsEarnedEachTime ? "Yes" : "No";
                        selected.SubItems[5].Text = dialog.Achievement.MustEarnAllToGetPoints ? "Yes" : "No";
                    }
                }
            }
        }

        private void deleteSelectedAchievementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstAchievements.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you wish to delete this Achievement?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // Make sure this achievement hasn't already been earned!
                    DialogResult decision = DialogResult.None;

                    foreach (MatchResult match in _League.MatchesPlayed)
                    {
                        foreach (Achievement achievement in match.Achievements)
                        {
                            if (achievement.Name.ToLower() == lstAchievements.SelectedItems[0].Text.ToLower())
                            {
                                if (decision == DialogResult.None)
                                {
                                    decision = MessageBox.Show("This achievement has already been earned by at least one player! Remove it from " +
                                        "all players? If No, they will continue to have earned the points for the Achievement", "Achievement In Use",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                                    if (decision == DialogResult.Cancel) return;

                                }
                                if (decision == DialogResult.Yes)
                                {
                                    foreach (Achievement a in _League.Achievements)
                                    {
                                        if (a.Name.ToLower() == achievement.Name.ToLower())
                                        {
                                            _League.Achievements.Remove(a);
                                            break;
                                        }
                                    }
                                    _League.Achievements.Remove(achievement);
                                    break;
                                }
                            }
                        }
                    }
                    lstAchievements.Items.Remove(lstAchievements.SelectedItems[0]);
                }
            }
        }

        private void mnuAchievementList_Opening(object sender, CancelEventArgs e)
        {
            editSelctedAchievementToolStripMenuItem.Enabled = lstAchievements.SelectedItems.Count > 0;
            deleteSelectedAchievementToolStripMenuItem.Enabled = lstAchievements.SelectedItems.Count > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.TextLength == 0)
            {
                MessageBox.Show("You must provide a name for the League at a minimum.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(IsNew)
            {
                Tournament tempTournament = Config.Settings.GetTournament(txtName.Text);
                League tempLeague = Config.Settings.GetLeague(txtName.Text);
                if (tempTournament != null || tempLeague != null)
                {
                    MessageBox.Show("There is already an Event named \"" + txtName.Text + "\", please choose a new " +
                        " name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (txtForumURL.TextLength > 0)
            {
                if (!txtForumURL.Text.ToLower().StartsWith("http://") && !txtForumURL.Text.ToLower().StartsWith("https://"))
                    txtForumURL.Text = "http://" + txtForumURL.Text;
                Uri temp = null;
                if (!Uri.TryCreate(txtForumURL.Text, UriKind.RelativeOrAbsolute, out temp))
                {
                    MessageBox.Show("\"" + txtForumURL.Text + "\" is not a valid URL.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _League.Name = txtName.Text;
            _League.Location = txtLocation.Text;
            _League.ForumURL = txtForumURL.Text;
            _League.Format = (EventFormat)Enum.Parse(typeof(EventFormat), (string)cmbFormat.SelectedItem);
            _League.StartDate = dtStartDate.Value;
            _League.EndDate = dtEndDate.Value;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }

    public class AchievementListComparer : IComparer
    {
        public AchievementListComparer() { }

        public int Compare(object x, object y)
        {
            string categoryA = ((ListViewItem)x).SubItems[1].Text;
            string categoryB = ((ListViewItem)y).SubItems[1].Text;
            string nameA = ((ListViewItem)x).Text;
            string nameB = ((ListViewItem)y).Text;

            if (categoryA.ToLower() != categoryB.ToLower())
                return categoryA.CompareTo(categoryB);
            else
                return nameA.CompareTo(nameB);
        }
    }
}
