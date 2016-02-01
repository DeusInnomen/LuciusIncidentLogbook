namespace KitchenGeeks
{
    partial class FrmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManager));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAllPlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAllTournamentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewTournamentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.createTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipTheDeveloperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMessageLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewChangeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnManagePlayers = new System.Windows.Forms.ToolStripButton();
            this.btnManageEvents = new System.Windows.Forms.ToolStripButton();
            this.btnOpenNotes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCreateTimer = new System.Windows.Forms.ToolStripButton();
            this.btnNewEvent = new System.Windows.Forms.ToolStripButton();
            this.btnAddPlayer = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnWebServerToggle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblWebStatus = new System.Windows.Forms.ToolStripLabel();
            this.mnuSettings = new System.Windows.Forms.ToolStripButton();
            this.mnuMain.SuspendLayout();
            this.mnuTools.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.manageToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.MdiWindowListItem = this.windowsToolStripMenuItem;
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(883, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(116, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewAllPlayersToolStripMenuItem,
            this.viewAllTournamentsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.addNewPlayerToolStripMenuItem,
            this.createNewTournamentToolStripMenuItem,
            this.toolStripMenuItem3,
            this.createTimerToolStripMenuItem});
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.manageToolStripMenuItem.Text = "&Manage";
            // 
            // viewAllPlayersToolStripMenuItem
            // 
            this.viewAllPlayersToolStripMenuItem.Name = "viewAllPlayersToolStripMenuItem";
            this.viewAllPlayersToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewAllPlayersToolStripMenuItem.Text = "View All &Players";
            this.viewAllPlayersToolStripMenuItem.Click += new System.EventHandler(this.btnManagePlayers_Click);
            // 
            // viewAllTournamentsToolStripMenuItem
            // 
            this.viewAllTournamentsToolStripMenuItem.Name = "viewAllTournamentsToolStripMenuItem";
            this.viewAllTournamentsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewAllTournamentsToolStripMenuItem.Text = "View All &Tournaments";
            this.viewAllTournamentsToolStripMenuItem.Click += new System.EventHandler(this.btnManageTournaments_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(201, 6);
            // 
            // addNewPlayerToolStripMenuItem
            // 
            this.addNewPlayerToolStripMenuItem.Name = "addNewPlayerToolStripMenuItem";
            this.addNewPlayerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.addNewPlayerToolStripMenuItem.Text = "Add &New Player";
            this.addNewPlayerToolStripMenuItem.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // createNewTournamentToolStripMenuItem
            // 
            this.createNewTournamentToolStripMenuItem.Name = "createNewTournamentToolStripMenuItem";
            this.createNewTournamentToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.createNewTournamentToolStripMenuItem.Text = "&Create New Tournament";
            this.createNewTournamentToolStripMenuItem.Click += new System.EventHandler(this.btnNewTournament_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(201, 6);
            // 
            // createTimerToolStripMenuItem
            // 
            this.createTimerToolStripMenuItem.Name = "createTimerToolStripMenuItem";
            this.createTimerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.createTimerToolStripMenuItem.Text = "Create T&imer";
            this.createTimerToolStripMenuItem.Click += new System.EventHandler(this.btnCreateTimer_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.windowsToolStripMenuItem.Text = "&Windows";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.sendFeedbackToolStripMenuItem,
            this.tipTheDeveloperToolStripMenuItem,
            this.showMessageLogToolStripMenuItem,
            this.toolStripMenuItem1,
            this.viewChangeLogToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // sendFeedbackToolStripMenuItem
            // 
            this.sendFeedbackToolStripMenuItem.Name = "sendFeedbackToolStripMenuItem";
            this.sendFeedbackToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.sendFeedbackToolStripMenuItem.Text = "Send &Feedback";
            this.sendFeedbackToolStripMenuItem.Click += new System.EventHandler(this.sendFeedbackToolStripMenuItem_Click);
            // 
            // tipTheDeveloperToolStripMenuItem
            // 
            this.tipTheDeveloperToolStripMenuItem.Name = "tipTheDeveloperToolStripMenuItem";
            this.tipTheDeveloperToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.tipTheDeveloperToolStripMenuItem.Text = "&Tip the Developer";
            this.tipTheDeveloperToolStripMenuItem.Click += new System.EventHandler(this.tipTheDeveloperToolStripMenuItem_Click);
            // 
            // showMessageLogToolStripMenuItem
            // 
            this.showMessageLogToolStripMenuItem.Name = "showMessageLogToolStripMenuItem";
            this.showMessageLogToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showMessageLogToolStripMenuItem.Text = "Show &Error Messages";
            this.showMessageLogToolStripMenuItem.Click += new System.EventHandler(this.showMessageLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // viewChangeLogToolStripMenuItem
            // 
            this.viewChangeLogToolStripMenuItem.Name = "viewChangeLogToolStripMenuItem";
            this.viewChangeLogToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.viewChangeLogToolStripMenuItem.Text = "View &Change Log";
            this.viewChangeLogToolStripMenuItem.Click += new System.EventHandler(this.viewChangeLogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.btnManagePlayers,
            this.btnManageEvents,
            this.btnOpenNotes,
            this.mnuSettings,
            this.toolStripSeparator1,
            this.btnCreateTimer,
            this.btnNewEvent,
            this.btnAddPlayer});
            this.mnuTools.Location = new System.Drawing.Point(0, 24);
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(883, 25);
            this.mnuTools.TabIndex = 1;
            this.mnuTools.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Tools:";
            // 
            // btnManagePlayers
            // 
            this.btnManagePlayers.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePlayers.Image")));
            this.btnManagePlayers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManagePlayers.Name = "btnManagePlayers";
            this.btnManagePlayers.Size = new System.Drawing.Size(110, 22);
            this.btnManagePlayers.Text = "Manage Players";
            this.btnManagePlayers.Click += new System.EventHandler(this.btnManagePlayers_Click);
            // 
            // btnManageEvents
            // 
            this.btnManageEvents.Image = ((System.Drawing.Image)(resources.GetObject("btnManageEvents.Image")));
            this.btnManageEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManageEvents.Name = "btnManageEvents";
            this.btnManageEvents.Size = new System.Drawing.Size(107, 22);
            this.btnManageEvents.Text = "Manage Events";
            this.btnManageEvents.Click += new System.EventHandler(this.btnManageTournaments_Click);
            // 
            // btnOpenNotes
            // 
            this.btnOpenNotes.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenNotes.Image")));
            this.btnOpenNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenNotes.Name = "btnOpenNotes";
            this.btnOpenNotes.Size = new System.Drawing.Size(137, 22);
            this.btnOpenNotes.Text = "Open Notes Window";
            this.btnOpenNotes.Click += new System.EventHandler(this.btnOpenNotes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCreateTimer
            // 
            this.btnCreateTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateTimer.Image")));
            this.btnCreateTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateTimer.Name = "btnCreateTimer";
            this.btnCreateTimer.Size = new System.Drawing.Size(95, 22);
            this.btnCreateTimer.Text = "Create Timer";
            this.btnCreateTimer.Click += new System.EventHandler(this.btnCreateTimer_Click);
            // 
            // btnNewEvent
            // 
            this.btnNewEvent.Image = ((System.Drawing.Image)(resources.GetObject("btnNewEvent.Image")));
            this.btnNewEvent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewEvent.Name = "btnNewEvent";
            this.btnNewEvent.Size = new System.Drawing.Size(120, 22);
            this.btnNewEvent.Text = "Create New Event";
            this.btnNewEvent.Click += new System.EventHandler(this.btnNewTournament_Click);
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPlayer.Image")));
            this.btnAddPlayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(111, 22);
            this.btnAddPlayer.Text = "Add New Player";
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.btnWebServerToggle,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.lblWebStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 49);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(883, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel2.Text = "Web Server:";
            // 
            // btnWebServerToggle
            // 
            this.btnWebServerToggle.Image = ((System.Drawing.Image)(resources.GetObject("btnWebServerToggle.Image")));
            this.btnWebServerToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWebServerToggle.Name = "btnWebServerToggle";
            this.btnWebServerToggle.Size = new System.Drawing.Size(140, 22);
            this.btnWebServerToggle.Text = "Toggle Server On/Off";
            this.btnWebServerToggle.Click += new System.EventHandler(this.btnWebServerToggle_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel3.Text = "Status:";
            // 
            // lblWebStatus
            // 
            this.lblWebStatus.Name = "lblWebStatus";
            this.lblWebStatus.Size = new System.Drawing.Size(43, 22);
            this.lblWebStatus.Text = "Offline";
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuSettings.Image")));
            this.mnuSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(69, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 627);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mnuTools);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "FrmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lucius\'s Incident Logbook";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManager_FormClosing);
            this.Load += new System.EventHandler(this.frmManager_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.mnuTools.ResumeLayout(false);
            this.mnuTools.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStrip mnuTools;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnManagePlayers;
        private System.Windows.Forms.ToolStripButton btnManageEvents;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCreateTimer;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipTheDeveloperToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendFeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewChangeLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnNewEvent;
        private System.Windows.Forms.ToolStripButton btnAddPlayer;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAllPlayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAllTournamentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem addNewPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewTournamentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem createTimerToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton btnWebServerToggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lblWebStatus;
        private System.Windows.Forms.ToolStripMenuItem showMessageLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnOpenNotes;
        private System.Windows.Forms.ToolStripButton mnuSettings;
    }
}

