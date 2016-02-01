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
    public partial class frmGetRoundToEdit : Form
    {
        public int SelectedRound
        {
            get { return cmbRound.SelectedIndex + 1; }
        }

        public frmGetRoundToEdit(int rounds)
        {
            InitializeComponent();
            for (var round = 1; round <= rounds; round++)
                cmbRound.Items.Add("Round " + round);
            cmbRound.SelectedIndex = rounds - 1;
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
