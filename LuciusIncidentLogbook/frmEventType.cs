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
    /// <summary>
    /// The type of Event chosen.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// The type has not been chosen.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// A traditionally single day event where players are matched up against each other based on victory
        /// conditions and play a fixed number of rounds.
        /// </summary>
        Tournament = 1,
        /// <summary>
        /// A single or multiple day event where players play as many matches as they can in order to accumulate
        /// victories, achievements or both.
        /// </summary>
        League = 2
    }

    public partial class frmEventType : Form
    {
        public frmEventType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the type of Event chosen by the user.
        /// </summary>
        public EventType Value
        {
            get
            {
                if (rdoTournament.Checked)
                    return EventType.Tournament;
                else
                    return EventType.League;
            }
        }

        private void rdoTournament_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTournament.Checked)
            {
                lblDetails.Text = "A Tournament is a traditionally single day event where players are matched up " +
                    "against each other based on victory conditions and play a fixed number of rounds.";
            }
            else
            {
                lblDetails.Text = "A League is a single or multiple day event where players play as many matches " +
                    "as they can in order to accumulate victories, achievements or both.";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
