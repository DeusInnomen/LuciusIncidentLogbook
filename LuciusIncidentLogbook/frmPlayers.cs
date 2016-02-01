using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace KitchenGeeks
{
    public partial class frmPlayers : Form
    {
        private int SortColumn = 0;
        private bool SortAscend = true;

        public frmPlayers()
        {
            InitializeComponent();
            RefreshList();
            foreach (string faction in Enum.GetNames(typeof(Factions)))
                cmbFaction.Items.Add(faction);
        }

        /// <summary>
        /// Refreshes the list of players.
        /// </summary>
        public void RefreshList()
        {            
            lstPlayers.BeginUpdate();
            lstPlayers.Columns[0].ImageIndex = 0;

            lstPlayers.Items.Clear();
            foreach (PlayerRecord record in Config.Settings.Players)
            {
                ListViewItem item = new ListViewItem();
                item.Name = record.ID;
                item.Text = record.FirstName;
                item.SubItems.Add(record.LastName);
                item.SubItems.Add(record.Region);
                lstPlayers.Items.Add(item);

                if (!cmbRegion.Items.Contains(record.Region))
                    cmbRegion.Items.Add(record.Region);
            }
            lstPlayers.ListViewItemSorter = new ListViewItemComparer(0, true);
            lstPlayers.Sort();
            lstPlayers.EndUpdate();

        }

        private void lstPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Math.Abs(SortColumn) == Math.Abs(e.Column))
            {
                SortAscend = !SortAscend;
                lstPlayers.Columns[e.Column].ImageIndex = SortAscend ? 0 : 1;
            }
            else
            {
                lstPlayers.Columns[SortColumn].ImageIndex = -1;
                lstPlayers.Columns[SortColumn].TextAlign = lstPlayers.Columns[SortColumn].TextAlign;
                SortAscend = true;
                SortColumn = e.Column;
                lstPlayers.Columns[e.Column].ImageIndex = 0;
            }

            lstPlayers.BeginUpdate();
            lstPlayers.ListViewItemSorter = new ListViewItemComparer(e.Column, SortAscend);
            lstPlayers.Sort();
            lstPlayers.EndUpdate();
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            using (var dialog = new frmCreatePlayer())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
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

                    ListViewItem item = new ListViewItem();
                    item.Name = record.ID;
                    item.Text = record.FirstName;
                    item.SubItems.Add(record.LastName);
                    item.SubItems.Add(record.Region);

                    lstPlayers.BeginUpdate();
                    lstPlayers.Items.Add(item);
                    item.Selected = true;
                    lstPlayers.Sort();
                    lstPlayers.EndUpdate();
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            using (frmFindPlayer dialog = new frmFindPlayer())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (ListViewItem item in lstPlayers.Items)
                    {
                        if (item.Text == dialog.SelectedID.ToString())
                        {
                            item.Selected = true;
                            return;
                        }
                    }
                }
            }
        }

        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItems.Count > 0)
            {
                string id = lstPlayers.SelectedItems[0].Name;
                foreach (PlayerRecord record in Config.Settings.Players)
                {
                    if (record.ID == id)
                    {
                        txtID.Text = record.ID.ToString();
                        txtFirstName.Text = record.FirstName;
                        txtLastName.Text = record.LastName;
                        txtEmail.Text = record.Email;
                        txtForumName.Text = record.ForumName;
                        txtHometown.Text = record.Hometown;
                        cmbRegion.Text = record.Region;
                        cmbFaction.SelectedItem = record.Faction.ToString();

                        txtFirstName.Enabled = true;
                        txtLastName.Enabled = true;
                        txtEmail.Enabled = true;
                        txtForumName.Enabled = true;
                        txtHometown.Enabled = true;
                        cmbRegion.Enabled = true;
                        cmbFaction.Enabled = true;
                        btnRevert.Enabled = true;
                        btnSave.Enabled = true;
                        btnShowRecords.Enabled = true;
                        return;
                    }
                }
            }
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtForumName.Text = "";
            txtHometown.Text = "";
            cmbRegion.Text = "";
            cmbFaction.Text = "";

            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            txtForumName.Enabled = false;
            txtHometown.Enabled = false;
            cmbRegion.Enabled = false;
            cmbFaction.Enabled = false;
            btnRevert.Enabled = false;
            btnSave.Enabled = false;
            btnShowRecords.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (PlayerRecord record in Config.Settings.Players)
            {
                if (record.ID == txtID.Text)
                {
                    record.FirstName = txtFirstName.Text;
                    record.LastName = txtLastName.Text;
                    record.Email = txtEmail.Text;
                    record.ForumName = txtForumName.Text;
                    record.Hometown = txtHometown.Text;
                    record.Region = cmbRegion.Text != null ? (string)cmbRegion.Text : "";
                    record.Faction = (Factions)Enum.Parse(typeof(Factions), cmbFaction.Text);
                    Config.Settings.SavePlayers();

                    if (record.Region.Length > 0 && !cmbRegion.Items.Contains(record.Region))
                        cmbRegion.Items.Add(record.Region);

                    foreach (ListViewItem item in lstPlayers.Items)
                    {
                        if (item.Text == txtID.Text)
                        {
                            item.SubItems[1].Text = txtFirstName.Text;
                            item.SubItems[2].Text = txtLastName.Text;
                            lstPlayers.Sort();
                            break;
                        }
                    }
                    return;
                }
            }
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            foreach (PlayerRecord record in Config.Settings.Players)
            {
                if (record.ID == txtID.Text)
                {
                    txtID.Text = record.ID.ToString();
                    txtFirstName.Text = record.FirstName;
                    txtLastName.Text = record.LastName;
                    txtEmail.Text = record.Email;
                    txtForumName.Text = record.ForumName;
                    txtHometown.Text = record.Hometown;
                    cmbRegion.Text = record.Region;
                    return;
                }
            }
        }

        private void btnShowRecords_Click(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedItems.Count == 0) return;

            string PlayerID = txtID.Text;
            foreach (Form form in this.MdiParent.MdiChildren)
            {
                if (form is frmPlayerResults)
                    if (((frmPlayerResults)form).PlayerID == PlayerID)
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.Focus();
                        return;
                    }
            }

            frmPlayerResults results = new frmPlayerResults(PlayerID);
            results.MdiParent = this.MdiParent;
            results.Show();
        }
    }

}
