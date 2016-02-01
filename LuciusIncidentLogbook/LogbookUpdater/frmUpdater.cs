using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Network.FTP;

namespace LogbookUpdater
{
    public partial class frmUpdater : Form
    {
        private FTPClient ftp = new FTPClient();
        private string ApplicationPath
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath);
            }
        }

        public frmUpdater()
        {
            InitializeComponent();
            ftp.LogMessage += ftp_LogMessage;
        }

        private void frmUpdater_Load(object sender, EventArgs e)
        {
            CheckVersions();
        }

        private void CheckVersions()
        {
            WebClient web = new WebClient();
            string webVersion = "";
            UpdateLog("Checking version number on the website...");
            while (true)
            {
                try
                {
                    webVersion = web.DownloadString("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/.version");
                    break;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Unable to check for latest version: " + ex.Message,
                        "Error During Update Check", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        this.Close();
                        return;
                    }
                }
            }

            UpdateLog("Web Version = v" + webVersion);
            txtWebVersion.Text = webVersion;
            string target = Path.Combine(ApplicationPath, "LuciusIncidentLogbook.exe");
            if (!File.Exists(target))
            {
                btnUpdate.Enabled = false;
                UpdateLog("Local Version Missing! Please copy LuciusIncidentLogbook.exe into the Updater's directory and press Refresh.");
                txtLocalVersion.Text = "Not Present";
                return;
            }

            btnUpdate.Enabled = true;
            Version fileVersion = Assembly.LoadFile(target).GetName().Version;
            string localVersion = fileVersion.Major.ToString() + "." + fileVersion.Minor.ToString() + "." +
                fileVersion.Build.ToString();

            UpdateLog("Local Version = v" + localVersion);
            txtLocalVersion.Text = localVersion;
            UpdateLog("");

        }

        private void UpdateLog(string data)
        {
            txtLog.Invoke(new MethodInvoker(delegate
            {
                txtLog.Text += data + "\r\n";
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.SelectionLength = 0;
                txtLog.ScrollToCaret();
                Application.DoEvents();
            }));
        }

        void ftp_LogMessage(object sender, FTPMessageEventArgs e)
        {
            if (!e.IsData)
                UpdateLog(e.Message);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CheckVersions();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtLocalVersion.Text == txtWebVersion.Text)
            {
                if (MessageBox.Show("Versions are the same, are you sure you wish to update again?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                Version fileVersion = Assembly.LoadFile(Path.Combine(ApplicationPath, "LuciusIncidentLogbook.exe")).GetName().Version;
                string myVersion = fileVersion.Major.ToString() + "." + fileVersion.Minor.ToString() + "." +
                    fileVersion.Build.ToString();
                File.WriteAllText(Path.Combine(ApplicationPath, ".version"), myVersion);

                ftp.PassiveMode = chkPassive.Checked;
                ftp.Connect("kaeris.kitchengeeks.net", 21, "nighthawk", "Innomen97");
                if (!ftp.IsConnected)
                    return;
                ftp.ChangeDirectory("/var/www/kitchengeeks.net/www/Malifaux/LuciusIncidentLogbook");
                ftp.UploadFile("LuciusIncidentLogbook.exe", Path.Combine(ApplicationPath, "LuciusIncidentLogbook.exe"));
                ftp.UploadFile(".version", Path.Combine(ApplicationPath, ".version"));
                ftp.Disconnect();

                WebClient web = new WebClient();
                string webVersion = "";
                UpdateLog("Checking version number on the website...");
                while (true)
                {
                    try
                    {
                        webVersion = web.DownloadString("http://www.kitchengeeks.net/Malifaux/LuciusIncidentLogbook/.version");
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("Unable to check for latest version: " + ex.Message, "Error During Update Check", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
                        {
                            this.Close();
                            return;
                        }
                    }
                }
                UpdateLog("Web Version = v" + webVersion);
                txtWebVersion.Text = webVersion;
            }
            catch (Exception ex)
            {
                UpdateLog("Error: (" + ex.GetType().ToString() + ") " + ex.Message);
                if (ftp.IsConnected)
                    ftp.Disconnect();
            }
            finally
            {
                Cursor = Cursors.Default;
                File.Delete(Path.Combine(ApplicationPath, ".version"));
            }
        }
    }
}
