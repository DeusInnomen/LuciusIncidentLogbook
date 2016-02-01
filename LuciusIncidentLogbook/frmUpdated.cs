using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace KitchenGeeks
{
    public partial class frmUpdated : Form
    {
        public frmUpdated(Version oldVersion)
        {
            InitializeComponent();

            if (oldVersion == null)
            {
                this.Text = "Change Log";
                lblMessage.Text = "Below is the complete Change Log:";
            }
            try
            {
                Dictionary<Version, string> notes = new Dictionary<Version, string>();
                List<Version> versions = new List<Version>();

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(Properties.Resources.ChangeLog);

                XmlNodeList updateNodes = xml.SelectNodes("Updates/Update");                
                if(updateNodes != null)
                    foreach (XmlNode updateNode in updateNodes)
                    {
                        Version updateVersion = new Version(updateNode.Attributes["Version"].Value);
                        if (oldVersion == null || updateVersion > oldVersion && updateVersion <= Program.Version)
                        {
                            versions.Add(updateVersion);
                            notes.Add(updateVersion, updateNode.InnerText.Trim());
                        }
                    }
                            
                versions.Sort(new Comparison<Version>(delegate(Version a, Version b)
                    {
                        return b.CompareTo(a);
                    }));
                foreach (Version version in versions)
                    txtLog.Text += "v" + version.Major.ToString() + "." + version.Minor.ToString() + "." +
                        version.Build.ToString() + "\r\n" + notes[version] + "\r\n\r\n";
                txtLog.SelectionLength = 0;
                txtLog.SelectionStart = 0;
            }
            catch
            {
                txtLog.Text = "An error occurred trying to read \"ChangeLog.xml\". The change log is unavailable.";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
