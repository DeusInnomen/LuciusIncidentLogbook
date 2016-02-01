using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class FrmAddPlayer : Form
    {
        public string SelectedID
        { 
            get { return cmbPlayers.SelectedIndex >= 0 ? ((PlayerRecord)cmbPlayers.SelectedItem).ID : null; }
        }

        public Factions SelectedFaction
        {
            get
            {
                return cmbFaction.SelectedIndex >= 0
                           ? ((Factions) Enum.Parse(typeof (Factions), cmbFaction.SelectedItem.ToString(), true))
                           : Factions.Undeclared;
            }
        }

        public FrmAddPlayer(Dictionary<string, int> players )
        {
            InitializeComponent();
            foreach (var player in Config.Settings.Players)
            {
                if (players.ContainsKey(player.ID)) continue;
                cmbPlayers.Items.Add(player);
            }
            foreach (var faction in Enum.GetNames(typeof (Factions)))
                cmbFaction.Items.Add(faction);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(cmbPlayers.SelectedIndex >= 0)
                DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmbPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlayers.SelectedIndex == -1) return;
            var player = Config.Settings.GetPlayer(((PlayerRecord) cmbPlayers.SelectedItem).ID);
            cmbFaction.SelectedIndex = cmbFaction.FindString(player.Faction.ToString());
        }
    }
}
