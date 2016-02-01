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
    public partial class frmDownload : Form
    {
        public frmDownload()
        {
            InitializeComponent();
        }

        public void SetProgressMax(int max)
        {
            barProgress.Invoke(new MethodInvoker(delegate { barProgress.Maximum = max; }));
        }

        public void SetProgressValue(int value)
        {
            barProgress.Invoke(new MethodInvoker(delegate { barProgress.Value = value; }));
        }

        public void SetMessage(string text)
        {
            lblInfo.Invoke(new MethodInvoker(delegate { lblInfo.Text = text; }));
        }
    }
}
