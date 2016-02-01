using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace KitchenGeeks
{
    public partial class frmExportOptions : Form
    {
        private string EventName = null;
        private EventType EventType = EventType.Unknown;

        public frmExportOptions(string name, EventType eventType)
        {
            InitializeComponent();

            txtName.Text = name;
            EventName = name;
            EventType = eventType;
        }

        private void btnGetPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the folder to write the exported Event to...";
                dialog.ShowNewFolderButton = true;
                dialog.SelectedPath = txtPath.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                    txtPath.Text = dialog.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtPath.Text))
            {
                if (MessageBox.Show("The target folder does not exist. Create it now?", "Folder Missing", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
                try
                {
                    Directory.CreateDirectory(txtPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create folder \"" + txtPath.Text + "\": " + ex.Message +
                        "Please create the folder manually before pressing OK again.", "Failed to Create Folder",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlNode exportNode = xml.AppendChild(xml.CreateElement("Export"));

            string target = "";
            if (EventType == KitchenGeeks.EventType.Tournament)
            {
                target = Path.Combine(txtPath.Text, Config.ScrubFilename(EventName + ".tournament.export"));
                Tournament record = Config.Settings.GetTournament(EventName);

                if (!chkExportNone.Checked && record.Players.Count > 0)
                {
                    XmlNode playersNode = exportNode.AppendChild(xml.CreateElement("Players"));
                    foreach (string ID in record.Players.Keys)
                        playersNode.AppendChild(Config.Settings.GetPlayer(ID).ToXML(xml));
                }

                XmlNode tournamentNode = exportNode.AppendChild(record.ToXml(xml));
                if (!chkExportAll.Checked && record.Rounds.Count > 0)
                {
                    XmlNodeList roundNodes = tournamentNode.SelectNodes("Rounds/Round");
                    foreach (XmlNode roundNode in roundNodes)
                        roundNode.ParentNode.RemoveChild(roundNode);
                }
            }
            else
            {
                target = Path.Combine(txtPath.Text, Config.ScrubFilename(EventName + ".league.export"));
                League record = Config.Settings.GetLeague(EventName);

                if (!chkExportNone.Checked && record.Players.Count > 0)
                {
                    XmlNode playersNode = exportNode.AppendChild(xml.CreateElement("Players"));
                    foreach (string ID in record.Players)
                        playersNode.AppendChild(Config.Settings.GetPlayer(ID).ToXML(xml));
                }

                XmlNode leagueNode = exportNode.AppendChild(record.ToXML(xml));
                if (!chkExportAll.Checked && record.MatchesPlayed.Count > 0)
                {
                    XmlNodeList resultNodes = leagueNode.SelectNodes("Results/Result");
                    foreach (XmlNode resultNode in resultNodes)
                        resultNode.ParentNode.RemoveChild(resultNode);
                }
            }

            if (File.Exists(target))
                if (MessageBox.Show("The file \"" + target + "\" already exists, overwrite?", "File Already Exists",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                    return;

            XmlTextWriter writer = new XmlTextWriter(new FileStream(target, FileMode.Create), null);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 1;
            writer.IndentChar = '\t';

            xml.WriteTo(writer);
            writer.Flush();
            writer.Close();


            MessageBox.Show("Your event was successfully exported to " + target + "!", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
