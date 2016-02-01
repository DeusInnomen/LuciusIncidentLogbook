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
    public partial class frmCreatePlayer : Form
    {
        public string FirstName
        {
            get
            {
                if (txtName.Text.IndexOf(" ") != -1)
                    return txtName.Text.Substring(0, txtName.Text.IndexOf(" "));
                else
                    return txtName.Text;
            }
        }

        public string LastName
        {
            get
            {
                if (txtName.Text.IndexOf(" ") != -1)
                    return txtName.Text.Substring(txtName.Text.IndexOf(" ") + 1);
                else
                    return "";
            }
        }

        public bool AdditionalInformation { get { return (this.Height == 269); } }
        public string ForumName { get { return txtForumName.Text; } }
        public string Email { get { return txtEmail.Text; } }
        public string Hometown { get { return txtHometown.Text; } }
        public string PlayerRegion { get { return cmbRegion.Text; } }
        public Factions Faction { get { return (Factions)Enum.Parse(typeof(Factions), cmbFaction.Text); } }

        public frmCreatePlayer(string name = null)
        {
            InitializeComponent();
            if (name != null) txtName.Text = name;
            foreach (string faction in Enum.GetNames(typeof(Factions)))
                cmbFaction.Items.Add(faction);
            foreach (PlayerRecord record in Config.Settings.Players)
                if (record.Region.Trim().Length > 0 && !cmbRegion.Items.Contains(record.Region))
                    cmbRegion.Items.Add(record.Region);
            cmbFaction.Text = "Undeclared";
            ShowMore(false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ShowMore(bool visible)
        {
            if (visible)
            {
                this.Height = 269;
                btnResize.Text = "Show Less";
            }
            else
            {
                this.Height = 99;
                btnResize.Text = "Show More";
            }
            lblAdditional.Visible = visible;
            lblForumName.Visible = visible;
            txtForumName.Visible = visible;
            txtForumName.Enabled = visible;
            txtEmail.Enabled = visible;
            txtHometown.Enabled = visible;
            cmbRegion.Enabled = visible;
            cmbFaction.Enabled = visible;
            txtName.Focus();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            ShowMore(this.Height == 99);
        }
    }
}
