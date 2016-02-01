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
    public partial class frmTournamentOptions : Form
    {
        private Tournament _tournament = new Tournament();
        public Tournament Tournament { get { return _tournament; } }
        private bool IsNew;

        public frmTournamentOptions(Tournament ToEdit = null, bool isNew = false)
        {
            InitializeComponent();

            if (ToEdit != null) _tournament = ToEdit;
            IsNew = isNew;

            txtName.Text = Tournament.Name;
            txtLocation.Text = Tournament.Location;
            txtForumURL.Text = Tournament.ForumURL;
            dtDate.Value = Tournament.Date;
            numStones.Value = Tournament.Soulstones;
            numRounds.Value = Tournament.TotalRounds;
            numLength.Value = Tournament.RoundTimeLimit;
            chkAvoidSameRegion.Checked = Tournament.TryToAvoidSameRegionMatches;
            chkAvoidSameFaction.Checked = Tournament.TryToAvoidSameFactionMatches;

            cmbType.SelectedItem = Tournament.IsBrawl ? "Brawl" : "Scrap";

            // Populate combo boxes.
            FillComboBoxes();

            cmbFormat.SelectedItem = Tournament.Format.ToString();
            cmbCrew.SelectedItem = Tournament.Crews.ToString();
            cmbStrategy.SelectedItem = Tournament.Strategy.ToString();
            cmbSchemes.SelectedItem = Tournament.Schemes.ToString();
            cmbNumbering.SelectedIndex = (int)Tournament.TableNumbering;
        }

        private void FillComboBoxes()
        {
            var currentFormat = (string)cmbFormat.SelectedItem;
            var currentCrew = (string)cmbCrew.SelectedItem;
            var currentStrategy = (string)cmbStrategy.SelectedItem;
            var currentScheme = (string)cmbSchemes.SelectedItem;
            cmbFormat.Items.Clear();
            cmbCrew.Items.Clear();
            cmbStrategy.Items.Clear();
            cmbSchemes.Items.Clear();

            foreach (string format in Enum.GetNames(typeof(EventFormat)))
                cmbFormat.Items.Add(format);
            foreach (string crew in Enum.GetNames(typeof(EventCrews)))
                cmbCrew.Items.Add(crew);
            foreach (string strategy in Enum.GetNames(typeof(EventStrategy)))
                cmbStrategy.Items.Add(strategy);
            foreach (string scheme in Enum.GetNames(typeof(EventSchemes)))
                cmbSchemes.Items.Add(scheme);

            if (currentFormat != null && cmbFormat.Items.Contains(currentFormat))
                cmbFormat.SelectedItem = currentFormat;
            else
                cmbFormat.SelectedIndex = 0;
            if (currentCrew != null && cmbCrew.Items.Contains(currentCrew))
                cmbCrew.SelectedItem = currentCrew;
            else
                cmbCrew.SelectedIndex = 0;
            if (currentStrategy != null && cmbStrategy.Items.Contains(currentStrategy))
                cmbStrategy.SelectedItem = currentStrategy;
            else
                cmbStrategy.SelectedIndex = 0;
            if (currentScheme != null && cmbSchemes.Items.Contains(currentScheme))
                cmbSchemes.SelectedItem = currentScheme;
            else
                cmbSchemes.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.TextLength == 0)
            {
                MessageBox.Show("You must provide a name for the Tournament at a minimum.", "Validation Error",
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

            Tournament.Name = txtName.Text;
            Tournament.Location = txtLocation.Text;
            Tournament.Date = dtDate.Value;
            Tournament.ForumURL = txtForumURL.Text;
            Tournament.Soulstones = (int)numStones.Value;
            Tournament.TotalRounds = (int)numRounds.Value;
            Tournament.RoundTimeLimit = (int)numLength.Value;
            Tournament.TryToAvoidSameRegionMatches = chkAvoidSameRegion.Checked;
            Tournament.TryToAvoidSameFactionMatches = chkAvoidSameFaction.Checked;
            Tournament.IsBrawl = ((string)cmbType.SelectedItem == "Brawl");
            Tournament.Format = (EventFormat)Enum.Parse(typeof(EventFormat), (string)cmbFormat.SelectedItem);
            Tournament.Crews = (EventCrews)Enum.Parse(typeof(EventCrews), (string)cmbCrew.SelectedItem);
            Tournament.Strategy = (EventStrategy)Enum.Parse(typeof(EventStrategy), (string)cmbStrategy.SelectedItem);
            Tournament.Schemes = (EventSchemes)Enum.Parse(typeof(EventSchemes), (string)cmbSchemes.SelectedItem);
            Tournament.TableNumbering = (TableNumbering) cmbNumbering.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }
    }
}
