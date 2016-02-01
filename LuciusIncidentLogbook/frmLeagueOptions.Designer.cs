namespace KitchenGeeks
{
    partial class frmLeagueOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeagueOptions));
            this.label4 = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lstAchievements = new System.Windows.Forms.ListView();
            this.colAchievement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMaxTimes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPoints = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPointsPerEarn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNeedAll = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuAchievementList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewAchievementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelctedAchievementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedAchievementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkContinuousAdd = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtForumURL = new System.Windows.Forms.TextBox();
            this.mnuAchievementList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Start Date:";
            // 
            // dtStartDate
            // 
            this.dtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtStartDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDate.Location = new System.Drawing.Point(494, 12);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(102, 20);
            this.dtStartDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(75, 34);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(208, 20);
            this.txtLocation.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(75, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(208, 20);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(430, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Date:";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtEndDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEndDate.Location = new System.Drawing.Point(494, 38);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(102, 20);
            this.dtEndDate.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(440, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(521, 358);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstAchievements
            // 
            this.lstAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAchievements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAchievement,
            this.colCategory,
            this.colMaxTimes,
            this.colPoints,
            this.colPointsPerEarn,
            this.colNeedAll});
            this.lstAchievements.ContextMenuStrip = this.mnuAchievementList;
            this.lstAchievements.FullRowSelect = true;
            this.lstAchievements.GridLines = true;
            this.lstAchievements.Location = new System.Drawing.Point(8, 123);
            this.lstAchievements.MultiSelect = false;
            this.lstAchievements.Name = "lstAchievements";
            this.lstAchievements.Size = new System.Drawing.Size(591, 156);
            this.lstAchievements.TabIndex = 12;
            this.lstAchievements.UseCompatibleStateImageBehavior = false;
            this.lstAchievements.View = System.Windows.Forms.View.Details;
            this.lstAchievements.DoubleClick += new System.EventHandler(this.lstAchievements_DoubleClick);
            // 
            // colAchievement
            // 
            this.colAchievement.Text = "Achievement";
            this.colAchievement.Width = 245;
            // 
            // colCategory
            // 
            this.colCategory.Text = "Category";
            this.colCategory.Width = 102;
            // 
            // colMaxTimes
            // 
            this.colMaxTimes.Text = "Limit";
            this.colMaxTimes.Width = 36;
            // 
            // colPoints
            // 
            this.colPoints.Text = "Points";
            this.colPoints.Width = 43;
            // 
            // colPointsPerEarn
            // 
            this.colPointsPerEarn.Text = "Points Each?";
            this.colPointsPerEarn.Width = 75;
            // 
            // colNeedAll
            // 
            this.colNeedAll.Text = "Need All?";
            // 
            // mnuAchievementList
            // 
            this.mnuAchievementList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewAchievementToolStripMenuItem,
            this.editSelctedAchievementToolStripMenuItem,
            this.deleteSelectedAchievementToolStripMenuItem});
            this.mnuAchievementList.Name = "mnuAchievementList";
            this.mnuAchievementList.Size = new System.Drawing.Size(228, 70);
            this.mnuAchievementList.Opening += new System.ComponentModel.CancelEventHandler(this.mnuAchievementList_Opening);
            // 
            // addNewAchievementToolStripMenuItem
            // 
            this.addNewAchievementToolStripMenuItem.Name = "addNewAchievementToolStripMenuItem";
            this.addNewAchievementToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.addNewAchievementToolStripMenuItem.Text = "&Add New Achievement";
            this.addNewAchievementToolStripMenuItem.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // editSelctedAchievementToolStripMenuItem
            // 
            this.editSelctedAchievementToolStripMenuItem.Name = "editSelctedAchievementToolStripMenuItem";
            this.editSelctedAchievementToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.editSelctedAchievementToolStripMenuItem.Text = "&Edit Selcted Achievement";
            this.editSelctedAchievementToolStripMenuItem.Click += new System.EventHandler(this.lstAchievements_DoubleClick);
            // 
            // deleteSelectedAchievementToolStripMenuItem
            // 
            this.deleteSelectedAchievementToolStripMenuItem.Name = "deleteSelectedAchievementToolStripMenuItem";
            this.deleteSelectedAchievementToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.deleteSelectedAchievementToolStripMenuItem.Text = "&Delete Selected Achievement";
            this.deleteSelectedAchievementToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedAchievementToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Format:";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(75, 86);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(150, 21);
            this.cmbFormat.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(5, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(594, 99);
            this.label6.TabIndex = 13;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(494, 94);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add Achievement";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkContinuousAdd
            // 
            this.chkContinuousAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkContinuousAdd.AutoSize = true;
            this.chkContinuousAdd.Location = new System.Drawing.Point(387, 98);
            this.chkContinuousAdd.Name = "chkContinuousAdd";
            this.chkContinuousAdd.Size = new System.Drawing.Size(101, 17);
            this.chkContinuousAdd.TabIndex = 10;
            this.chkContinuousAdd.Text = "Continuous Add";
            this.chkContinuousAdd.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Forum URL:";
            // 
            // txtForumURL
            // 
            this.txtForumURL.Location = new System.Drawing.Point(75, 60);
            this.txtForumURL.Name = "txtForumURL";
            this.txtForumURL.Size = new System.Drawing.Size(208, 20);
            this.txtForumURL.TabIndex = 17;
            // 
            // frmLeagueOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 393);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtForumURL);
            this.Controls.Add(this.chkContinuousAdd);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.lstAchievements);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label6);
            this.MinimumSize = new System.Drawing.Size(624, 413);
            this.Name = "frmLeagueOptions";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "League Options";
            this.mnuAchievementList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListView lstAchievements;
        private System.Windows.Forms.ColumnHeader colAchievement;
        private System.Windows.Forms.ColumnHeader colMaxTimes;
        private System.Windows.Forms.ColumnHeader colPoints;
        private System.Windows.Forms.ColumnHeader colPointsPerEarn;
        private System.Windows.Forms.ColumnHeader colNeedAll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip mnuAchievementList;
        private System.Windows.Forms.ToolStripMenuItem addNewAchievementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelctedAchievementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedAchievementToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkContinuousAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtForumURL;
    }
}