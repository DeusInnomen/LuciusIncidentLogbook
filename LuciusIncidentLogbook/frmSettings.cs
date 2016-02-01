using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            chkSeparateFiles.Checked = Config.Settings.SeparateEventFiles;
            chkConfirmDefault.Checked = Config.Settings.ConfirmNewRounds;
            chkOpenAfterPlayersAdded.Checked = Config.Settings.OpenAfterPlayersAdded;
            chkRandomTables.Checked = Config.Settings.AssignTablesRandomly;
            txtArchive.Text = Config.Settings.ArchivePath;
            numBackups.Value = Config.Settings.BackupsToKeep;
            txtBackupPath.Text = Config.Settings.BackupPath;
            chkAutoStartWeb.Checked = Config.Settings.AutoEnableWebServer;
            numWebPort.Value = Config.Settings.WebServerPort;
            txtWebPassword.Text = Config.Settings.WebServerPassword;
            chkAutoCheck.Checked = Config.Settings.AutoCheckForUpdates;
            chkSkipTiedRanks.Checked = Config.Settings.SkipTiedRanks;
            txtForumName.Text = Config.Settings.ForumName;
            chkDisableReportAfterSend.Checked = Config.Settings.DisableReportIfSent;

            txtSMTPFromName.Text = Config.Settings.SMTPFromName;
            txtSMTPFromEmail.Text = Config.Settings.SMTPFromAddress;
            txtSMTPServer.Text = Config.Settings.SMTPServer;
            numSMTPPort.Value = Config.Settings.SMTPPort;
            if (Config.Settings.SMTPUsername.Length > 0)
            {
                chkSMTPAuth.Checked = true;
                txtSMTPUser.Text = Config.Settings.SMTPUsername;
                txtSMTPPassword.Text = Config.Settings.SMTPPassword;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtArchive.Text))
            {
                if (MessageBox.Show("The Archive Directory does not exist. Create now?", "Folder Missing",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;

                try
                {
                    Directory.CreateDirectory(txtArchive.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create the Archive Directory: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (txtSMTPFromEmail.TextLength > 0 && !IsValidEmail(txtSMTPFromEmail.Text))
            {
                MessageBox.Show("\"" + txtSMTPFromEmail.Text + "\" is not a valid email address.", "Validation Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkSMTPAuth.Checked && (txtSMTPUser.TextLength == 0 || txtSMTPPassword.TextLength == 0))
            {
                MessageBox.Show("You must enter an username and password for sending email, or uncheck \"Authentication " +
                    "Required\".", "Validation Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool resaveTournaments = (Config.Settings.SeparateEventFiles != chkSeparateFiles.Checked);
            Config.Settings.SeparateEventFiles = chkSeparateFiles.Checked;
            Config.Settings.ConfirmNewRounds = chkConfirmDefault.Checked;
            Config.Settings.OpenAfterPlayersAdded = chkOpenAfterPlayersAdded.Checked;
            Config.Settings.AssignTablesRandomly = chkRandomTables.Checked;
            Config.Settings.ArchivePath = txtArchive.Text;
            Config.Settings.BackupsToKeep = Convert.ToInt32(numBackups.Value);
            Config.Settings.BackupPath = txtBackupPath.Text;
            Config.Settings.AutoEnableWebServer = chkAutoStartWeb.Checked;
            Config.Settings.WebServerPassword = txtWebPassword.Text;
            Config.Settings.WebServerPort = Convert.ToInt32(numWebPort.Value);
            Config.Settings.AutoCheckForUpdates = chkAutoCheck.Checked;
            Config.Settings.SkipTiedRanks = chkSkipTiedRanks.Checked;
            Config.Settings.ForumName = txtForumName.Text;
            Config.Settings.DisableReportIfSent = chkDisableReportAfterSend.Checked;
            Config.Settings.SMTPFromName = txtSMTPFromName.Text;
            Config.Settings.SMTPFromAddress = txtSMTPFromEmail.Text;
            Config.Settings.SMTPServer = txtSMTPServer.Text;
            Config.Settings.SMTPPort = (int)numSMTPPort.Value;
            if (chkSMTPAuth.Checked)
            {
                Config.Settings.SMTPUsername = txtSMTPUser.Text;
                Config.Settings.SMTPPassword = txtSMTPPassword.Text;
            }
            else
            {
                Config.Settings.SMTPUsername = "";
                Config.Settings.SMTPPassword = "";
            }
            Config.Settings.SaveSettings();

            if (resaveTournaments)
                Config.Settings.SaveEvents();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnGetArchivePath_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the folder to move archived Events to...";
                dialog.ShowNewFolderButton = true;
                dialog.SelectedPath = txtArchive.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    if (Program.BasePath.StartsWith(Path.GetDirectoryName(path)))
                        path = "." + path.Substring(Program.BasePath.Length);
                    txtArchive.Text = path;
                }
            }
        }

        private void btnGetBackupPath_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the folder to write backups to...";
                dialog.ShowNewFolderButton = true;
                dialog.SelectedPath = txtBackupPath.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    if (Program.BasePath.StartsWith(Path.GetDirectoryName(path)))
                        path = "." + path.Substring(Program.BasePath.Length);
                    txtBackupPath.Text = path;
                }
            }
        }

        private void chkSMTPAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtSMTPUser.Enabled = chkSMTPAuth.Checked;
            txtSMTPPassword.Enabled = chkSMTPAuth.Checked;
        }

        private bool IsValidEmail(string address)
        {
            return Regex.IsMatch(address,
               "^(?(\")(\".+?\"@)|(([0-9a-zA-Z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-zA-Z])@))" +
               "(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,6}))$");
        }


        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            if (txtSMTPServer.TextLength == 0 || txtSMTPFromEmail.TextLength == 0 || txtSMTPFromName.TextLength == 0)
            {
                MessageBox.Show("You must enter the From Name, From Email and SMTP Server at a minimum to test or " +
                    "use SMTP.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(txtSMTPFromEmail.Text))
            {
                MessageBox.Show("\"" + txtSMTPFromEmail.Text + "\" is not a valid email address.", "Validation Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SMTPTestResults results = SMTPFunctions.TestSMTP(txtSMTPServer.Text, (int)numSMTPPort.Value,
                chkSMTPAuth.Checked ? txtSMTPUser.Text : null, chkSMTPAuth.Checked ? txtSMTPPassword.Text : null);
            
            string message;
            var icon = MessageBoxIcon.Asterisk;
            if (!results.Connected || !results.Acknowledged)
                icon = MessageBoxIcon.Error;
            else if (chkSMTPAuth.Checked && !results.AuthenticationSucceeded)
                icon = MessageBoxIcon.Error;
            
            if (results.Connected)
            {
                message = "* Connect to the server successfully.\r\n";
                message += results.Acknowledged ? "* Server correctly responded to connection.\r\n" : 
                    "! Server failed to respond to the connection!\r\n";
                if (results.Acknowledged)
                {
                    message += results.AuthenticationRequired ? "* Server allows and may require authentication.\r\n" : 
                        "! Server does not allow authentication.\r\n";
                    if (chkSMTPAuth.Checked)
                        message += results.AuthenticationSucceeded ? "* Successfully authenticated with the server.\r\n" :
                            "! Authentication with the server failed.\r\n";
                }                
            }
            else
            {
                message = "! Failed to connect to the server.\r\n";
            }

            MessageBox.Show(message, "SMTP Test Results", MessageBoxButtons.OK, icon);
        }
    }
}
