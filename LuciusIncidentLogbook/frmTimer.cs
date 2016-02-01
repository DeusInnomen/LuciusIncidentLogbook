using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KitchenGeeks
{
    public partial class frmTimer : Form
    {
        private System.Threading.Timer MyTimer = null;
        private DateTime Target;
        private TimeSpan Remaining;
        private int OriginalMinutes;
        private SoundPlayer alarm = new SoundPlayer();

        public frmTimer(string name, int minutes)
        {
            InitializeComponent();
            this.Text = "Timer: " + name;

            OriginalMinutes = minutes;
            Remaining = new TimeSpan(0, minutes, 0);
            lblTime.Text = OriginalMinutes.ToString("00") + ":00";

            MyTimer = new System.Threading.Timer(new System.Threading.TimerCallback(Tick), null, Timeout.Infinite, 250);
            alarm.Stream = Properties.Resources.Alarm;
        }

        private void Tick(object sender)
        {
            Remaining = Target.Subtract(DateTime.Now);

            // Ensures that we don't display a negative number.
            if (Remaining.TotalSeconds <= 0) Remaining = new TimeSpan(0, 0, 0);
            try
            {
                lblTime.Invoke(new MethodInvoker(delegate
                    {
                        lblTime.Text = Math.Floor(Remaining.TotalMinutes).ToString("00") + ":" + Remaining.Seconds.ToString("00");
                    }));
                if (Math.Floor(Remaining.TotalSeconds) == 0)
                {
                    MyTimer.Change(Timeout.Infinite, 250);
                    btnToggle.Invoke(new MethodInvoker(delegate
                        {
                            btnToggle.Text = mnuPlaySound.Checked ? "Stop" : "Reset";
                        }));
                    if (mnuPlaySound.Checked)
                        alarm.PlayLooping();

                }
            }
            catch
            {
                // Errors should only really happen when the form or entire app is closing.
                MyTimer.Dispose();
            }
        }

        private void resetClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTimer.Change(Timeout.Infinite, 250);

            btnToggle.Text = "Start";
            Remaining = new TimeSpan(0, OriginalMinutes, 0);
            lblTime.Text = OriginalMinutes.ToString("00") + ":00";
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (btnToggle.Text == "Start")
            {
                Target = DateTime.Now.Add(Remaining);
                MyTimer.Change(250, 250);
                btnToggle.Text = "Pause";
            }
            else if (btnToggle.Text == "Reset" || btnToggle.Text == "Stop")
            {
                alarm.Stop(); // Just in case it's playing.
                btnToggle.Text = "Start";
                Remaining = new TimeSpan(0, OriginalMinutes, 0);
                lblTime.Text = OriginalMinutes.ToString("00") + ":00";
            }
            else
            {
                MyTimer.Change(Timeout.Infinite, 250);
                btnToggle.Text = "Start";
            }
        }

        private void frmTimer_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyTimer.Change(Timeout.Infinite, 250);
            MyTimer.Dispose();
        }

        private void frmTimer_SizeChanged(object sender, EventArgs e)
        {
            lblTime.Font = new Font("Verdana", Height / 5);
        }
    }
}
