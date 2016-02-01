namespace KitchenGeeks
{
    partial class frmViewTournament
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewTournament));
            this.lstResults = new System.Windows.Forms.ListView();
            this.colRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFaction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuPlayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.forfeitPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeForfeitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkHighlight = new System.Windows.Forms.CheckBox();
            this.numTopRows = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRound = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentRound = new System.Windows.Forms.Label();
            this.chkConfirmNewRound = new System.Windows.Forms.CheckBox();
            this.btnViewPairings = new System.Windows.Forms.Button();
            this.chkOpenRound = new System.Windows.Forms.CheckBox();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnAddPlayer = new System.Windows.Forms.Button();
            this.btnExportResults = new System.Windows.Forms.Button();
            this.btnEditRounds = new System.Windows.Forms.Button();
            this.btnCancelRound = new System.Windows.Forms.Button();
            this.replacePlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRows)).BeginInit();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRank,
            this.colNumber,
            this.colName,
            this.colFaction});
            this.lstResults.ContextMenuStrip = this.mnuPlayers;
            this.lstResults.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstResults.FullRowSelect = true;
            this.lstResults.GridLines = true;
            this.lstResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstResults.HideSelection = false;
            this.lstResults.Location = new System.Drawing.Point(0, 0);
            this.lstResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(656, 523);
            this.lstResults.TabIndex = 0;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            // 
            // colRank
            // 
            this.colRank.Text = "Rank";
            this.colRank.Width = 39;
            // 
            // colNumber
            // 
            this.colNumber.Text = "#";
            this.colNumber.Width = 25;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 109;
            // 
            // colFaction
            // 
            this.colFaction.Text = "Faction";
            this.colFaction.Width = 92;
            // 
            // mnuPlayers
            // 
            this.mnuPlayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forfeitPlayerToolStripMenuItem,
            this.removeForfeitToolStripMenuItem,
            this.changeFactionToolStripMenuItem,
            this.replacePlayerToolStripMenuItem});
            this.mnuPlayers.Name = "mnuPlayers";
            this.mnuPlayers.Size = new System.Drawing.Size(207, 157);
            this.mnuPlayers.Opening += new System.ComponentModel.CancelEventHandler(this.mnuPlayers_Opening);
            // 
            // forfeitPlayerToolStripMenuItem
            // 
            this.forfeitPlayerToolStripMenuItem.Name = "forfeitPlayerToolStripMenuItem";
            this.forfeitPlayerToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
            this.forfeitPlayerToolStripMenuItem.Text = "&Forfeit Player";
            this.forfeitPlayerToolStripMenuItem.Click += new System.EventHandler(this.forfeitPlayerToolStripMenuItem_Click);
            // 
            // removeForfeitToolStripMenuItem
            // 
            this.removeForfeitToolStripMenuItem.Name = "removeForfeitToolStripMenuItem";
            this.removeForfeitToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
            this.removeForfeitToolStripMenuItem.Text = "&Remove Forfeit";
            this.removeForfeitToolStripMenuItem.Click += new System.EventHandler(this.removeForfeitToolStripMenuItem_Click);
            // 
            // changeFactionToolStripMenuItem
            // 
            this.changeFactionToolStripMenuItem.Name = "changeFactionToolStripMenuItem";
            this.changeFactionToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
            this.changeFactionToolStripMenuItem.Text = "&Change Faction";
            this.changeFactionToolStripMenuItem.Click += new System.EventHandler(this.changeFactionToolStripMenuItem_Click);
            // 
            // chkHighlight
            // 
            this.chkHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHighlight.AutoSize = true;
            this.chkHighlight.Checked = true;
            this.chkHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighlight.Location = new System.Drawing.Point(674, 18);
            this.chkHighlight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHighlight.Name = "chkHighlight";
            this.chkHighlight.Size = new System.Drawing.Size(128, 24);
            this.chkHighlight.TabIndex = 1;
            this.chkHighlight.Text = "Highlight Top";
            this.chkHighlight.UseVisualStyleBackColor = true;
            this.chkHighlight.CheckedChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
            // 
            // numTopRows
            // 
            this.numTopRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numTopRows.BackColor = System.Drawing.Color.White;
            this.numTopRows.Location = new System.Drawing.Point(798, 17);
            this.numTopRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numTopRows.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numTopRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTopRows.Name = "numTopRows";
            this.numTopRows.Size = new System.Drawing.Size(60, 26);
            this.numTopRows.TabIndex = 2;
            this.numTopRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numTopRows.ValueChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(862, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Players";
            // 
            // btnRound
            // 
            this.btnRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRound.Location = new System.Drawing.Point(717, 198);
            this.btnRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRound.Name = "btnRound";
            this.btnRound.Size = new System.Drawing.Size(194, 35);
            this.btnRound.TabIndex = 4;
            this.btnRound.Text = "Begin Tournament";
            this.btnRound.UseVisualStyleBackColor = true;
            this.btnRound.Click += new System.EventHandler(this.btnRound_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(668, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Round in Progress:";
            // 
            // lblCurrentRound
            // 
            this.lblCurrentRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentRound.AutoSize = true;
            this.lblCurrentRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRound.Location = new System.Drawing.Point(684, 149);
            this.lblCurrentRound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentRound.Name = "lblCurrentRound";
            this.lblCurrentRound.Size = new System.Drawing.Size(72, 29);
            this.lblCurrentRound.TabIndex = 6;
            this.lblCurrentRound.Text = "None";
            // 
            // chkConfirmNewRound
            // 
            this.chkConfirmNewRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkConfirmNewRound.AutoSize = true;
            this.chkConfirmNewRound.Location = new System.Drawing.Point(669, 54);
            this.chkConfirmNewRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkConfirmNewRound.Name = "chkConfirmNewRound";
            this.chkConfirmNewRound.Size = new System.Drawing.Size(297, 24);
            this.chkConfirmNewRound.TabIndex = 7;
            this.chkConfirmNewRound.Text = "Confirm Before Starting New Rounds";
            this.chkConfirmNewRound.UseVisualStyleBackColor = true;
            // 
            // btnViewPairings
            // 
            this.btnViewPairings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewPairings.Location = new System.Drawing.Point(717, 243);
            this.btnViewPairings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnViewPairings.Name = "btnViewPairings";
            this.btnViewPairings.Size = new System.Drawing.Size(194, 35);
            this.btnViewPairings.TabIndex = 8;
            this.btnViewPairings.Text = "View or Edit Pairings";
            this.btnViewPairings.UseVisualStyleBackColor = true;
            this.btnViewPairings.Click += new System.EventHandler(this.btnViewPairings_Click);
            // 
            // chkOpenRound
            // 
            this.chkOpenRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOpenRound.AutoSize = true;
            this.chkOpenRound.Checked = true;
            this.chkOpenRound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenRound.Location = new System.Drawing.Point(672, 85);
            this.chkOpenRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkOpenRound.Name = "chkOpenRound";
            this.chkOpenRound.Size = new System.Drawing.Size(286, 24);
            this.chkOpenRound.TabIndex = 9;
            this.chkOpenRound.Text = "Open Round Window After It Starts";
            this.chkOpenRound.UseVisualStyleBackColor = true;
            // 
            // btnNotes
            // 
            this.btnNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotes.Location = new System.Drawing.Point(717, 378);
            this.btnNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(194, 35);
            this.btnNotes.TabIndex = 10;
            this.btnNotes.Text = "Show Notes";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPlayer.Location = new System.Drawing.Point(717, 423);
            this.btnAddPlayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(194, 35);
            this.btnAddPlayer.TabIndex = 11;
            this.btnAddPlayer.Text = "Add Player";
            this.btnAddPlayer.UseVisualStyleBackColor = true;
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // btnExportResults
            // 
            this.btnExportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportResults.Location = new System.Drawing.Point(717, 468);
            this.btnExportResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(194, 35);
            this.btnExportResults.TabIndex = 12;
            this.btnExportResults.Text = "Export Event Results";
            this.btnExportResults.UseVisualStyleBackColor = true;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // btnEditRounds
            // 
            this.btnEditRounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRounds.Location = new System.Drawing.Point(717, 288);
            this.btnEditRounds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditRounds.Name = "btnEditRounds";
            this.btnEditRounds.Size = new System.Drawing.Size(194, 35);
            this.btnEditRounds.TabIndex = 13;
            this.btnEditRounds.Text = "View/Edit Past Rounds";
            this.btnEditRounds.UseVisualStyleBackColor = true;
            this.btnEditRounds.Click += new System.EventHandler(this.btnEditRounds_Click);
            // 
            // btnCancelRound
            // 
            this.btnCancelRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelRound.Location = new System.Drawing.Point(717, 333);
            this.btnCancelRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelRound.Name = "btnCancelRound";
            this.btnCancelRound.Size = new System.Drawing.Size(194, 35);
            this.btnCancelRound.TabIndex = 14;
            this.btnCancelRound.Text = "Cancel Current Round";
            this.btnCancelRound.UseVisualStyleBackColor = true;
            this.btnCancelRound.Click += new System.EventHandler(this.btnCancelRound_Click);
            // 
            // replacePlayerToolStripMenuItem
            // 
            this.replacePlayerToolStripMenuItem.Name = "replacePlayerToolStripMenuItem";
            this.replacePlayerToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
            this.replacePlayerToolStripMenuItem.Text = "Re&place Player";
            this.replacePlayerToolStripMenuItem.Click += new System.EventHandler(this.replacePlayerToolStripMenuItem_Click);
            // 
            // frmViewTournament
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 523);
            this.Controls.Add(this.btnCancelRound);
            this.Controls.Add(this.btnEditRounds);
            this.Controls.Add(this.btnExportResults);
            this.Controls.Add(this.btnAddPlayer);
            this.Controls.Add(this.btnNotes);
            this.Controls.Add(this.chkOpenRound);
            this.Controls.Add(this.btnViewPairings);
            this.Controls.Add(this.chkConfirmNewRound);
            this.Controls.Add(this.lblCurrentRound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRound);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numTopRows);
            this.Controls.Add(this.chkHighlight);
            this.Controls.Add(this.lstResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(982, 462);
            this.Name = "frmViewTournament";
            this.Text = "View Tournament";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewTournament_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmViewTournament_SizeChanged);
            this.mnuPlayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTopRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colRank;
        private System.Windows.Forms.CheckBox chkHighlight;
        private System.Windows.Forms.NumericUpDown numTopRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrentRound;
        private System.Windows.Forms.ColumnHeader colFaction;
        private System.Windows.Forms.CheckBox chkConfirmNewRound;
        private System.Windows.Forms.Button btnViewPairings;
        private System.Windows.Forms.CheckBox chkOpenRound;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.ContextMenuStrip mnuPlayers;
        private System.Windows.Forms.ToolStripMenuItem forfeitPlayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeForfeitToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colNumber;
        private System.Windows.Forms.Button btnAddPlayer;
        private System.Windows.Forms.Button btnExportResults;
        private System.Windows.Forms.Button btnEditRounds;
        private System.Windows.Forms.ToolStripMenuItem changeFactionToolStripMenuItem;
        private System.Windows.Forms.Button btnCancelRound;
        private System.Windows.Forms.ToolStripMenuItem replacePlayerToolStripMenuItem;
    }
}