using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class frmCrash : Form
    {
        private Exception myEx = null;
        private string FileProduced = null;

        public frmCrash(Exception ex)
        {
            InitializeComponent();
            myEx = ex;
        }

        private void frmCrash_Load(object sender, EventArgs e)
        {
            try
            {
                txtError.Text = "[Exception Data]" + "\r\n" +
                    "Message: " + myEx.Message + "\r\n" +
                    "Type: " + myEx.GetType().ToString() + "\r\n\r\n";
                if (myEx.InnerException != null)
                    txtError.Text += "[InnerException Data]" + "\r\n" +
                        "Message: " + myEx.InnerException.Message + "\r\n" +
                        "Type: " + myEx.InnerException.GetType().ToString() + "\r\n\r\n";

                string folder = Path.Combine(Program.BasePath, "Error Reports");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                FileProduced = Path.Combine(folder, DateTime.Now.ToString("yyyyMMddHHmmss") + " Report.dat");
                using (StreamWriter sw = new StreamWriter(new FileStream(FileProduced, FileMode.Create,
                    FileAccess.Write, FileShare.ReadWrite)))
                {
                    sw.WriteLine("[Exception Data]");
                    sw.WriteLine("Message: " + myEx.Message);
                    sw.WriteLine("Type: " + myEx.GetType().ToString());
                    sw.WriteLine();
                    if (myEx.InnerException != null)
                    {
                        sw.WriteLine("[InnerException Data]");
                        sw.WriteLine("Message: " + myEx.InnerException.Message);
                        sw.WriteLine("Type: " + myEx.InnerException.GetType().ToString());
                        sw.WriteLine();
                    }
                    sw.WriteLine("[Stack Trace]");
                    sw.WriteLine(myEx.StackTrace);
                    sw.WriteLine();

                    sw.WriteLine("[Players.dat]");
                    sw.WriteLine(File.ReadAllText("Players.dat"));
                    sw.WriteLine();

                    sw.WriteLine("[Tournaments.dat]");
                    sw.WriteLine(File.ReadAllText("Tournaments.dat"));
                }
                lnkErrorFile.Text = Path.GetFileName(FileProduced);
            }
            catch { }
        }

        private void lnkErrorFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkErrorFile.Text.Length > 0)
                Process.Start("explorer.exe", "/select," + FileProduced);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }
    }
}
