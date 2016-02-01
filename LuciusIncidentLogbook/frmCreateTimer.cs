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
    public partial class frmCreateTimer : Form
    {
        public string TimerName { get { return txtName.Text; } }
        public int TimerDuration { get { return (int)numMinutes.Value; } }
        public bool DockTimer { get { return chkDock.Checked; } }
        
        public frmCreateTimer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(TimerName.Length > 0)
                this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = TimerName.Length > 0;
        }
    }
}
