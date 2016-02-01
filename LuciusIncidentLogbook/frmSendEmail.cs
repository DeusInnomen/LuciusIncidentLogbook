using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class frmSendEmail : Form
    {
        private string Prefix = "";
        private MailAddress Recipient = null;

        public frmSendEmail(string title, string labelMessage, MailAddress recipient, string subject,
            string subjectPrefix = null, string body = null)
        {
            InitializeComponent();
            this.Text = title;
            lblMessage.Text = labelMessage;
            Recipient = recipient;
            txtSubject.Text = subject;
            if (subjectPrefix != null) Prefix = "[" + subjectPrefix + "] ";
            if (body != null) txtMessage.Text = body;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = (txtMessage.TextLength > 0);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (Config.Settings.SMTPFromAddress.Length == 0 || Config.Settings.SMTPServer.Length == 0)
            {
                if (MessageBox.Show("You have not properly set up SMTP settings yet, please do so now before " +
                    "sending feedback.", "SMTP Not Configured", MessageBoxButtons.OKCancel, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1) == DialogResult.Cancel) return;
                FrmSettings dialog = new FrmSettings();
                dialog.ShowDialog();
                dialog.Close();
                if (Config.Settings.SMTPFromAddress.Length == 0 || Config.Settings.SMTPServer.Length == 0)
                    return;
            }

            if (MessageBox.Show("Are you sure you're ready to send this message?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            string subject = Prefix + (txtSubject.Text.Trim().Length > 0 ? txtSubject.Text.Trim() :
                "General Feedback");
            if(SMTPFunctions.SendMessage(Recipient, subject, txtMessage.Text))            
                this.DialogResult = DialogResult.OK;
        }

        public class SmtpClientEx : SmtpClient
        {
            private void SetClient(string client)
            {
                typeof(SmtpClient).GetField("clientDomain", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, client);
            }

            public SmtpClientEx()
                : base() { }
            public SmtpClientEx(string client)
                : base()
            {
                SetClient(client);
            }
            public SmtpClientEx(string host, string client)
                : base(host)
            {
                SetClient(client);
            }
            public SmtpClientEx(string host, int port, string client)
                : base(host, port)
            {
                SetClient(client);
            }
        }
    }
}
