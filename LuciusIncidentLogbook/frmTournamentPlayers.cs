using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class FrmTournamentPlayers : Form
    {
        /// <summary>
        /// The player IDs enrolled in this tournament.
        /// </summary>
        private Dictionary<string, int> Enrolled
        {
            get
            {
                var list = new Dictionary<string, int>();
                foreach (ListViewItem item in lstEnrolled.Items)
                {
                    var num = 0;
                    if (item.SubItems[2].Text == "New")
                    {
                        num = MaxNumber + 1;
                        while (HasNumber(num)) num++;
                        myArgs.Enrolled.Add(item.Name, num);
                    }
                    else
                        num = Convert.ToInt32(item.SubItems[2].Text);
                    list.Add(item.Name, num);
                }
                return list;
            }
        }

        /// <summary>
        /// The factions chosen by the players in this tournament.
        /// </summary>
        private Dictionary<string, Factions> ChosenFactions
        {
            get
            {
                var list = new Dictionary<string, Factions>();
                foreach (ListViewItem item in lstEnrolled.Items)
                    list.Add(item.Name, (Factions)Enum.Parse(typeof(Factions), item.SubItems[3].Text, true));
                return list;
            }
        }

        private int MaxNumber
        {
            get
            {
                return myArgs.Enrolled.Count > 0 ? myArgs.Enrolled.Max(item => item.Value) : 0;
            }
        }

        private bool HasNumber(int num)
        {
            return myArgs.Enrolled.Any(item => item.Value == num);
        }

        /// <summary>
        /// The name of the Tournament being edited.
        /// </summary>
        public string TournamentName
        {
            get
            {
                return myArgs.Name;
            }
        }

        private int EnrolledSortColumn = 0;
        private bool EnrolledSortAscend = true;
        private int AvailableSortColumn = 0;
        private bool AvailableSortAscend = true;

        private TournamentPlayersEventArgs myArgs;

        public FrmTournamentPlayers(TournamentPlayersEventArgs e)
        {
            InitializeComponent();
            myArgs = e;

            Text = e.Name + " -- Enroll Players";
            lstAvailable.BeginUpdate();
            lstEnrolled.BeginUpdate();
            lstAvailable.Columns[0].ImageIndex = 0;
            lstEnrolled.Columns[0].ImageIndex = 0;

            if (Enrolled != null)
                foreach (var id in e.Enrolled)
                {
                    var player = Config.Settings.GetPlayer(id.Key);
                    var item = new ListViewItem { Name = player.ID, Text = player.Name };
                    item.SubItems.Add(player.Hometown);
                    item.SubItems.Add(id.Value.ToString());
                    item.SubItems.Add(e.ChosenFactions[player.ID].ToString());
                    lstEnrolled.Items.Add(item);
                }

            foreach (PlayerRecord player in Config.Settings.Players)
            {
                if (lstEnrolled.Items.ContainsKey(player.ID)) continue;
                var item = new ListViewItem { Name = player.ID, Text = player.Name };
                item.SubItems.Add(player.Hometown);
                lstAvailable.Items.Add(item);

            }

            lblTotal.Text = "Total Players Enrolled: " + lstEnrolled.Items.Count.ToString();
            lstAvailable.Sort();
            lstEnrolled.Sort();
            lstAvailable.EndUpdate();
            lstEnrolled.EndUpdate();
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void frmTournamentPlayers_SizeChanged(object sender, EventArgs e)
        {
            lstAvailable.Width = btnToEnrolled.Left - 20;
            lstEnrolled.Left = btnToEnrolled.Right + 8;
            lstEnrolled.Width = lstAvailable.Width;
            lblEnrolled.Left = lstEnrolled.Left - 3;
        }

        private void lstAvailable_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Math.Abs(AvailableSortColumn) == Math.Abs(e.Column))
            {
                AvailableSortAscend = !AvailableSortAscend;
                lstAvailable.Columns[e.Column].ImageIndex = AvailableSortAscend ? 0 : 1;
            }
            else
            {
                lstAvailable.Columns[AvailableSortColumn].ImageIndex = -1;
                lstAvailable.Columns[AvailableSortColumn].TextAlign = lstAvailable.Columns[AvailableSortColumn].TextAlign;
                AvailableSortAscend = true;
                AvailableSortColumn = e.Column;
                lstAvailable.Columns[e.Column].ImageIndex = 0;
            }

            lstAvailable.BeginUpdate();
            lstAvailable.ListViewItemSorter = new ListViewItemComparer(e.Column, AvailableSortAscend);
            lstAvailable.Sort();
            lstAvailable.EndUpdate();
        }

        private void lstEnrolled_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Math.Abs(EnrolledSortColumn) == Math.Abs(e.Column))
            {
                EnrolledSortAscend = !EnrolledSortAscend;
                lstEnrolled.Columns[e.Column].ImageIndex = EnrolledSortAscend ? 0 : 1;
            }
            else
            {
                lstEnrolled.Columns[EnrolledSortColumn].ImageIndex = -1;
                lstEnrolled.Columns[EnrolledSortColumn].TextAlign = lstEnrolled.Columns[EnrolledSortColumn].TextAlign;
                EnrolledSortAscend = true;
                EnrolledSortColumn = e.Column;
                lstEnrolled.Columns[e.Column].ImageIndex = 0;
            }

            lstEnrolled.BeginUpdate();
            lstEnrolled.ListViewItemSorter = new ListViewItemComparer(e.Column, EnrolledSortAscend);
            lstEnrolled.Sort();
            lstEnrolled.EndUpdate();
        }

        private void lstAvailable_DoubleClick(object sender, EventArgs e)
        {
            lstAvailable.BeginUpdate();
            lstEnrolled.BeginUpdate();
            foreach (ListViewItem item in lstAvailable.SelectedItems)
            {
                var player = Config.Settings.GetPlayer(item.Name);
                var faction = item.SubItems.Count == 4
                                  ? (Factions) Enum.Parse(typeof (Factions), item.SubItems[3].Text, true)
                                  : player.Faction;
                using (var getFaction = new frmGetFaction(faction, player.Name))
                {
                    if (getFaction.ShowDialog() == DialogResult.Cancel) continue;
                    faction = getFaction.SelectedFaction;
                }
                lstAvailable.Items.Remove(item);
                if (item.SubItems.Count == 2)
                {
                    item.SubItems.Add("New");
                    item.SubItems.Add("");
                }
                item.SubItems[3].Text = faction.ToString();
                lstEnrolled.Items.Add(item);
            }

            lstAvailable.Sort();
            lstEnrolled.Sort();
            lstAvailable.EndUpdate();
            lstEnrolled.EndUpdate();

            lblTotal.Text = "Total Players Enrolled: " + lstEnrolled.Items.Count.ToString();
        }

        private void lstEnrolled_DoubleClick(object sender, EventArgs e)
        {
            lstAvailable.BeginUpdate();
            lstEnrolled.BeginUpdate();
            foreach (ListViewItem item in lstEnrolled.SelectedItems)
            {
                lstEnrolled.Items.Remove(item);
                lstAvailable.Items.Add(item);
            }

            lstAvailable.Sort();
            lstEnrolled.Sort();
            lstAvailable.EndUpdate();
            lstEnrolled.EndUpdate();

            lblTotal.Text = "Total Players Enrolled: " + lstEnrolled.Items.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            myArgs.Enrolled = Enrolled;
            myArgs.ChosenFactions = ChosenFactions;
            myArgs.Cancel = false;
            this.Close();
        }

        private void frmTournamentPlayers_FormClosing(object sender, FormClosingEventArgs e)
        {
            myArgs.Callback(this, myArgs);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            using (var dialog = new frmCreatePlayer())
            {
                if (dialog.ShowDialog() == DialogResult.Cancel) return;

                var player = new PlayerRecord {FirstName = dialog.FirstName, LastName = dialog.LastName};
                if (dialog.AdditionalInformation)
                {
                    player.ForumName = dialog.ForumName;
                    player.Email = dialog.Email;
                    player.Hometown = dialog.Hometown;
                    player.Region = dialog.PlayerRegion;
                    player.Faction = dialog.Faction;
                }

                Config.Settings.Players.Add(player);
                Config.Settings.SavePlayers();

                var id = GetPlayerID(player.Name);
                var item = new ListViewItem { Name = id, Text = player.Name };
                item.SubItems.Add(player.Hometown);
                item.SubItems.Add("New");
                item.SubItems.Add(player.Faction.ToString());
                lstEnrolled.Items.Add(item);

                lblTotal.Text = "Total Players Enrolled: " + lstEnrolled.Items.Count.ToString();
            }
        }

        private string GetPlayerID(string name)
        {
            return (from record in Config.Settings.Players where record.Name.ToLower() == name.ToLower() select record.ID).FirstOrDefault();
        }
    }
}
