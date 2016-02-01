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
    public partial class frmChangeFaction : Form
    {
        public Factions SelectedFaction
        {
            get
            {
                return cmbFaction.SelectedIndex >= 0
                           ? ((Factions)Enum.Parse(typeof(Factions), cmbFaction.SelectedItem.ToString(), true))
                           : Factions.Undeclared;
            }
        }

        public frmChangeFaction(Factions currentFaction)
        {
            InitializeComponent();
            foreach (var faction in Enum.GetNames(typeof(Factions)))
                cmbFaction.Items.Add(faction);
            cmbFaction.SelectedIndex = cmbFaction.FindString(currentFaction.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
