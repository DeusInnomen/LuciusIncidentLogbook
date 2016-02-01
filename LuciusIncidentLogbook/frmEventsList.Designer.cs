namespace KitchenGeeks
{
    partial class frmEventsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEventsList));
            this.lstEvents = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuEventOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenForum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInputPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecordMatch = new System.Windows.Forms.ToolStripMenuItem();
            this.editOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.sendPreEventReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendPostEventReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportEventResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archiveEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SortImages = new System.Windows.Forms.ImageList(this.components);
            this.mnuEvents = new System.Windows.Forms.ToolStrip();
            this.btnNewEvent = new System.Windows.Forms.ToolStripButton();
            this.btnShowAddPlayers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.dtFilterDate = new System.Windows.Forms.DateTimePicker();
            this.grpFilters = new System.Windows.Forms.GroupBox();
            this.chkHidePast = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.rdoFilterDate = new System.Windows.Forms.RadioButton();
            this.txtFilterLocation = new System.Windows.Forms.TextBox();
            this.rdoFilterLocation = new System.Windows.Forms.RadioButton();
            this.rdoShowAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuEventOptions.SuspendLayout();
            this.mnuEvents.SuspendLayout();
            this.grpFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstEvents
            // 
            this.lstEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colLocation,
            this.colDate,
            this.colPlayers,
            this.colStatus});
            this.lstEvents.ContextMenuStrip = this.mnuEventOptions;
            this.lstEvents.FullRowSelect = true;
            this.lstEvents.GridLines = true;
            this.lstEvents.HideSelection = false;
            this.lstEvents.Location = new System.Drawing.Point(18, 152);
            this.lstEvents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstEvents.MultiSelect = false;
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(942, 359);
            this.lstEvents.SmallImageList = this.SortImages;
            this.lstEvents.TabIndex = 2;
            this.lstEvents.UseCompatibleStateImageBehavior = false;
            this.lstEvents.View = System.Windows.Forms.View.Details;
            this.lstEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstEvents_ColumnClick);
            this.lstEvents.SelectedIndexChanged += new System.EventHandler(this.lstEvents_SelectedIndexChanged);
            this.lstEvents.DoubleClick += new System.EventHandler(this.openEventToolStripMenuItem_Click);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 121;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 78;
            // 
            // colLocation
            // 
            this.colLocation.Text = "Location";
            this.colLocation.Width = 119;
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 84;
            // 
            // colPlayers
            // 
            this.colPlayers.Text = "# Players";
            this.colPlayers.Width = 85;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 99;
            // 
            // mnuEventOptions
            // 
            this.mnuEventOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openEventToolStripMenuItem,
            this.mnuOpenForum,
            this.toolStripMenuItem1,
            this.mnuInputPlayers,
            this.mnuRecordMatch,
            this.editOptionsToolStripMenuItem,
            this.notesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.sendPreEventReportToolStripMenuItem,
            this.sendPostEventReportToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exportEventResultsToolStripMenuItem,
            this.archiveEventToolStripMenuItem,
            this.deleteEventToolStripMenuItem,
            this.exportEventToolStripMenuItem});
            this.mnuEventOptions.Name = "contextMenuStrip1";
            this.mnuEventOptions.Size = new System.Drawing.Size(286, 382);
            this.mnuEventOptions.Opening += new System.ComponentModel.CancelEventHandler(this.mnuEventsOptions_Opening);
            // 
            // openEventToolStripMenuItem
            // 
            this.openEventToolStripMenuItem.Name = "openEventToolStripMenuItem";
            this.openEventToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.openEventToolStripMenuItem.Text = "&Open Event";
            this.openEventToolStripMenuItem.Click += new System.EventHandler(this.openEventToolStripMenuItem_Click);
            // 
            // mnuOpenForum
            // 
            this.mnuOpenForum.Name = "mnuOpenForum";
            this.mnuOpenForum.Size = new System.Drawing.Size(285, 30);
            this.mnuOpenForum.Text = "Open Wyrd &Forum Post";
            this.mnuOpenForum.Click += new System.EventHandler(this.mnuOpenForum_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(282, 6);
            // 
            // mnuInputPlayers
            // 
            this.mnuInputPlayers.Name = "mnuInputPlayers";
            this.mnuInputPlayers.Size = new System.Drawing.Size(285, 30);
            this.mnuInputPlayers.Text = "Input &Players";
            this.mnuInputPlayers.Click += new System.EventHandler(this.inputPlayersToolStripMenuItem_Click);
            // 
            // mnuRecordMatch
            // 
            this.mnuRecordMatch.Name = "mnuRecordMatch";
            this.mnuRecordMatch.Size = new System.Drawing.Size(285, 30);
            this.mnuRecordMatch.Text = "&Record Match";
            this.mnuRecordMatch.Click += new System.EventHandler(this.mnuRecordMatch_Click);
            // 
            // editOptionsToolStripMenuItem
            // 
            this.editOptionsToolStripMenuItem.Name = "editOptionsToolStripMenuItem";
            this.editOptionsToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.editOptionsToolStripMenuItem.Text = "&Edit Options";
            this.editOptionsToolStripMenuItem.Click += new System.EventHandler(this.editOptionsToolStripMenuItem_Click);
            // 
            // notesToolStripMenuItem
            // 
            this.notesToolStripMenuItem.Name = "notesToolStripMenuItem";
            this.notesToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.notesToolStripMenuItem.Text = "&Notes";
            this.notesToolStripMenuItem.Click += new System.EventHandler(this.notesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(282, 6);
            // 
            // sendPreEventReportToolStripMenuItem
            // 
            this.sendPreEventReportToolStripMenuItem.Name = "sendPreEventReportToolStripMenuItem";
            this.sendPreEventReportToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.sendPreEventReportToolStripMenuItem.Text = "Send Pre-Event Report...";
            this.sendPreEventReportToolStripMenuItem.Click += new System.EventHandler(this.sendPreEventReportToolStripMenuItem_Click);
            // 
            // sendPostEventReportToolStripMenuItem
            // 
            this.sendPostEventReportToolStripMenuItem.Name = "sendPostEventReportToolStripMenuItem";
            this.sendPostEventReportToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.sendPostEventReportToolStripMenuItem.Text = "Send Post-Event Report...";
            this.sendPostEventReportToolStripMenuItem.Click += new System.EventHandler(this.sendPostEventReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(282, 6);
            // 
            // exportEventResultsToolStripMenuItem
            // 
            this.exportEventResultsToolStripMenuItem.Name = "exportEventResultsToolStripMenuItem";
            this.exportEventResultsToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.exportEventResultsToolStripMenuItem.Text = "Export Event Results...";
            this.exportEventResultsToolStripMenuItem.Click += new System.EventHandler(this.outputResultsToCSVToolStripMenuItem_Click);
            // 
            // archiveEventToolStripMenuItem
            // 
            this.archiveEventToolStripMenuItem.Name = "archiveEventToolStripMenuItem";
            this.archiveEventToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.archiveEventToolStripMenuItem.Text = "&Archive Event";
            this.archiveEventToolStripMenuItem.Click += new System.EventHandler(this.archiveEventToolStripMenuItem_Click);
            // 
            // deleteEventToolStripMenuItem
            // 
            this.deleteEventToolStripMenuItem.Name = "deleteEventToolStripMenuItem";
            this.deleteEventToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.deleteEventToolStripMenuItem.Text = "&Delete Event...";
            this.deleteEventToolStripMenuItem.Click += new System.EventHandler(this.deleteEventsToolStripMenuItem_Click);
            // 
            // exportEventToolStripMenuItem
            // 
            this.exportEventToolStripMenuItem.Name = "exportEventToolStripMenuItem";
            this.exportEventToolStripMenuItem.Size = new System.Drawing.Size(285, 30);
            this.exportEventToolStripMenuItem.Text = "E&xport Event...";
            this.exportEventToolStripMenuItem.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // SortImages
            // 
            this.SortImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SortImages.ImageStream")));
            this.SortImages.TransparentColor = System.Drawing.Color.Transparent;
            this.SortImages.Images.SetKeyName(0, "Down.png");
            this.SortImages.Images.SetKeyName(1, "Up.png");
            // 
            // mnuEvents
            // 
            this.mnuEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewEvent,
            this.btnShowAddPlayers,
            this.toolStripSeparator1,
            this.btnImport,
            this.btnExport});
            this.mnuEvents.Location = new System.Drawing.Point(0, 0);
            this.mnuEvents.Name = "mnuEvents";
            this.mnuEvents.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuEvents.Size = new System.Drawing.Size(980, 32);
            this.mnuEvents.TabIndex = 0;
            this.mnuEvents.Text = "toolStrip1";
            // 
            // btnNewEvent
            // 
            this.btnNewEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnNewEvent.Image")));
            this.btnNewEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewEvent.Name = "btnNewEvent";
            this.btnNewEvent.Size = new System.Drawing.Size(170, 29);
            this.btnNewEvent.Text = "Create New Event";
            this.btnNewEvent.Click += new System.EventHandler(this.btnNewEvent_Click);
            // 
            // btnShowAddPlayers
            // 
            this.btnShowAddPlayers.Checked = true;
            this.btnShowAddPlayers.CheckOnClick = true;
            this.btnShowAddPlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowAddPlayers.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAddPlayers.Image")));
            this.btnShowAddPlayers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAddPlayers.Name = "btnShowAddPlayers";
            this.btnShowAddPlayers.Size = new System.Drawing.Size(376, 29);
            this.btnShowAddPlayers.Text = "Show \"Add Players\" After Creating an Event";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // btnImport
            // 
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(147, 29);
            this.btnImport.Text = "Import Event...";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(143, 29);
            this.btnExport.Text = "Export Event...";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtFilterDate
            // 
            this.dtFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFilterDate.Location = new System.Drawing.Point(460, 28);
            this.dtFilterDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtFilterDate.Name = "dtFilterDate";
            this.dtFilterDate.Size = new System.Drawing.Size(145, 26);
            this.dtFilterDate.TabIndex = 4;
            // 
            // grpFilters
            // 
            this.grpFilters.Controls.Add(this.chkHidePast);
            this.grpFilters.Controls.Add(this.btnUpdate);
            this.grpFilters.Controls.Add(this.rdoFilterDate);
            this.grpFilters.Controls.Add(this.txtFilterLocation);
            this.grpFilters.Controls.Add(this.dtFilterDate);
            this.grpFilters.Controls.Add(this.rdoFilterLocation);
            this.grpFilters.Controls.Add(this.rdoShowAll);
            this.grpFilters.Location = new System.Drawing.Point(18, 43);
            this.grpFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpFilters.Size = new System.Drawing.Size(720, 100);
            this.grpFilters.TabIndex = 1;
            this.grpFilters.TabStop = false;
            this.grpFilters.Text = "Filter Events List...";
            // 
            // chkHidePast
            // 
            this.chkHidePast.AutoSize = true;
            this.chkHidePast.Location = new System.Drawing.Point(9, 65);
            this.chkHidePast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHidePast.Name = "chkHidePast";
            this.chkHidePast.Size = new System.Drawing.Size(297, 24);
            this.chkHidePast.TabIndex = 6;
            this.chkHidePast.Text = "Hide Past Events that are Completed";
            this.chkHidePast.UseVisualStyleBackColor = true;
            this.chkHidePast.CheckedChanged += new System.EventHandler(this.chkHidePast_CheckedChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(622, 25);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 35);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // rdoFilterDate
            // 
            this.rdoFilterDate.AutoSize = true;
            this.rdoFilterDate.Location = new System.Drawing.Point(375, 29);
            this.rdoFilterDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoFilterDate.Name = "rdoFilterDate";
            this.rdoFilterDate.Size = new System.Drawing.Size(73, 24);
            this.rdoFilterDate.TabIndex = 3;
            this.rdoFilterDate.Text = "Date:";
            this.rdoFilterDate.UseVisualStyleBackColor = true;
            // 
            // txtFilterLocation
            // 
            this.txtFilterLocation.Location = new System.Drawing.Point(214, 28);
            this.txtFilterLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterLocation.Name = "txtFilterLocation";
            this.txtFilterLocation.Size = new System.Drawing.Size(150, 26);
            this.txtFilterLocation.TabIndex = 2;
            // 
            // rdoFilterLocation
            // 
            this.rdoFilterLocation.AutoSize = true;
            this.rdoFilterLocation.Location = new System.Drawing.Point(117, 29);
            this.rdoFilterLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoFilterLocation.Name = "rdoFilterLocation";
            this.rdoFilterLocation.Size = new System.Drawing.Size(99, 24);
            this.rdoFilterLocation.TabIndex = 1;
            this.rdoFilterLocation.Text = "Location:";
            this.rdoFilterLocation.UseVisualStyleBackColor = true;
            // 
            // rdoShowAll
            // 
            this.rdoShowAll.AutoSize = true;
            this.rdoShowAll.Checked = true;
            this.rdoShowAll.Location = new System.Drawing.Point(9, 29);
            this.rdoShowAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoShowAll.Name = "rdoShowAll";
            this.rdoShowAll.Size = new System.Drawing.Size(95, 24);
            this.rdoShowAll.TabIndex = 0;
            this.rdoShowAll.TabStop = true;
            this.rdoShowAll.Text = "Show All";
            this.rdoShowAll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 518);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(558, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Right click an Event for Event-specific options. Double click an Event to view it" +
    ".";
            // 
            // frmEventsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 552);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpFilters);
            this.Controls.Add(this.mnuEvents);
            this.Controls.Add(this.lstEvents);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(992, 581);
            this.Name = "frmEventsList";
            this.Text = "Events List";
            this.mnuEventOptions.ResumeLayout(false);
            this.mnuEvents.ResumeLayout(false);
            this.mnuEvents.PerformLayout();
            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstEvents;
        private System.Windows.Forms.ToolStrip mnuEvents;
        private System.Windows.Forms.ToolStripButton btnNewEvent;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colPlayers;
        private System.Windows.Forms.ImageList SortImages;
        private System.Windows.Forms.DateTimePicker dtFilterDate;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RadioButton rdoFilterDate;
        private System.Windows.Forms.TextBox txtFilterLocation;
        private System.Windows.Forms.RadioButton rdoFilterLocation;
        private System.Windows.Forms.RadioButton rdoShowAll;
        private System.Windows.Forms.CheckBox chkHidePast;
        private System.Windows.Forms.ContextMenuStrip mnuEventOptions;
        private System.Windows.Forms.ToolStripMenuItem openEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuInputPlayers;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archiveEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnShowAddPlayers;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ToolStripMenuItem mnuRecordMatch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripMenuItem exportEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sendPreEventReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendPostEventReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenForum;
        private System.Windows.Forms.ToolStripMenuItem exportEventResultsToolStripMenuItem;
    }
}