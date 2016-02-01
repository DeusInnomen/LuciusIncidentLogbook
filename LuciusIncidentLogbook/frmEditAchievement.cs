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
    public partial class frmEditAchievement : Form
    {
        public Achievement Achievement
        {
            get
            {
                Achievement achievement = new Achievement();
                achievement.Name = txtName.Text;
                achievement.Category = (string) cmbCategory.Text;
                achievement.Points = (int)numPoints.Value;
                achievement.MaxAllowed = (int)numLimit.Value;
                achievement.PointsEarnedEachTime = chkPointsEachTime.Checked;
                achievement.MustEarnAllToGetPoints = chkMustEarnAll.Checked;
                return achievement;
            }
        }

        public frmEditAchievement(List<string> categories, Achievement achievement = null)
        {
            InitializeComponent();

            if (achievement != null)
            {
                txtName.Text = achievement.Name;
                cmbCategory.Text = achievement.Category;
                numPoints.Value = achievement.Points;
                numLimit.Value = achievement.MaxAllowed;
                chkPointsEachTime.Checked = achievement.PointsEarnedEachTime;
                chkMustEarnAll.Checked = achievement.MustEarnAllToGetPoints;
            }
            cmbCategory.Items.AddRange(categories.ToArray());
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
