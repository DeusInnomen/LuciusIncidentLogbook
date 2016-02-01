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
    public partial class frmLog : Form
    {
        public frmLog()
        {
            InitializeComponent();
        }

        public void WriteLog(string Message)
        {
            txtLog.Invoke(new MethodInvoker(delegate
                {
                    txtLog.Text += "[" + DateTime.Now.ToString("HH:mm") + "] " + Message + "\r\n";
                    if(btnScrollToEnd.Checked)
                    {
                        txtLog.SelectionStart = txtLog.TextLength - 1;
                        txtLog.SelectionLength = 0;
                        txtLog.ScrollToCaret();
                    }
                }));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Invoke(new MethodInvoker(delegate { txtLog.Text = ""; }));   
        }
    }
}
