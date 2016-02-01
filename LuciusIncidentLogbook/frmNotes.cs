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
    public partial class frmNotes : Form
    {
        private string EventName = null;
        private string OriginalText = "";

        public frmNotes(string name)
        {
            InitializeComponent();
            if (name != null)
            {
                EventName = name;
                this.Tag = EventName;

                Tournament tournament = Config.Settings.GetTournament(EventName);
                if (tournament != null)
                {
                    this.Text = "Tournament \"" + EventName + "\" Notes";
                    txtNotes.Text = tournament.Notes;
                    OriginalText = tournament.Notes;
                    return;
                }
                League league = Config.Settings.GetLeague(EventName);
                if (league != null)
                {
                    this.Text = "League \"" + EventName + "\" Notes";
                    txtNotes.Text = league.Notes;
                    OriginalText = league.Notes;
                    return;
                }
            }

            this.Text = "General Notes";
            this.Tag = "General Notes";
            txtNotes.Text = Config.Settings.Notes;
            OriginalText = Config.Settings.Notes;
        }

        private void frmNotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OriginalText == txtNotes.Text) return;

            if (EventName != null)
            {
                Tournament tournament = Config.Settings.GetTournament(EventName);
                if (tournament != null)
                {
                    tournament.Notes = txtNotes.Text;
                    Config.Settings.SaveEvents();
                    return;
                }
                League league = Config.Settings.GetLeague(EventName);
                if (league != null)
                {
                    league.Notes = txtNotes.Text;
                    Config.Settings.SaveEvents();
                    return;
                }
            }
            else
            {
                Config.Settings.Notes = txtNotes.Text;
                Config.Settings.SaveSettings();
            }
        }
    }
}
