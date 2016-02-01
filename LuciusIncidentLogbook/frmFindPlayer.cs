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
    public partial class frmFindPlayer : Form
    {
        private int SortColumn = 0;
        private bool SortAscend = true;

        public int SelectedID
        {
            get
            {
                if (lstResults.SelectedItems.Count > 0)
                    return Convert.ToInt32(lstResults.SelectedItems[0].Text);
                else
                    return -1;
            }
        }

        public frmFindPlayer()
        {
            InitializeComponent();
            lstResults.Columns[0].ImageIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            lstResults.BeginUpdate();
            foreach (PlayerRecord record in Config.Settings.Players)
            {
                bool found = false;
                if (txtFirstName.TextLength > 0 && record.FirstName.ToLower().Contains(txtFirstName.Text.ToLower()))
                    found = true;
                if (txtLastName.TextLength > 0 && record.LastName.ToLower().Contains(txtLastName.Text.ToLower()))
                    found = true;
                if (txtEmail.TextLength > 0 && record.Email.ToLower().Contains(txtEmail.Text.ToLower()))
                    found = true;
                if (txtForumName.TextLength > 0 && record.ForumName.ToLower().Contains(txtForumName.Text.ToLower()))
                    found = true;
                if (txtHometown.TextLength > 0 && record.Hometown.ToLower().Contains(txtHometown.Text.ToLower()))
                    found = true;

                if (found)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = record.ID.ToString();
                    item.SubItems.Add(record.FirstName);
                    item.SubItems.Add(record.LastName);
                    lstResults.Items.Add(item);
                }
            }
            lstResults.Sort();
            lstResults.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtForumName.Text = "";
            txtHometown.Text = "";
            txtFirstName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lstResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstResults.SelectedItems.Count > 0)
                this.DialogResult = DialogResult.OK;
        }

        private void lstResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Math.Abs(SortColumn) == Math.Abs(e.Column))
            {
                SortAscend = !SortAscend;
                lstResults.Columns[e.Column].ImageIndex = SortAscend ? 0 : 1;
            }
            else
            {
                lstResults.Columns[SortColumn].ImageIndex = -1;
                lstResults.Columns[SortColumn].TextAlign = lstResults.Columns[SortColumn].TextAlign;
                SortAscend = true;
                SortColumn = e.Column;
                lstResults.Columns[e.Column].ImageIndex = 0;
            }

            lstResults.BeginUpdate();
            lstResults.ListViewItemSorter = new ListViewItemComparer(e.Column, SortAscend);
            lstResults.Sort();
            lstResults.EndUpdate();
        }

    }
}
