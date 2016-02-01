namespace KitchenGeeks
{
    partial class frmTournamentRound
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTournamentRound));
            this.pnlMatches = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMatches = new System.Windows.Forms.Label();
            this.mnuOptions = new System.Windows.Forms.ToolStrip();
            this.btnTimer = new System.Windows.Forms.ToolStripButton();
            this.btnLockAllMatches = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFinalize = new System.Windows.Forms.ToolStripButton();
            this.lblMatchesLeft = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mnuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMatches
            // 
            this.pnlMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMatches.AutoScroll = true;
            this.pnlMatches.Location = new System.Drawing.Point(0, 80);
            this.pnlMatches.Name = "pnlMatches";
            this.pnlMatches.Size = new System.Drawing.Size(599, 337);
            this.pnlMatches.TabIndex = 3;
            // 
            // lblMatches
            // 
            this.lblMatches.AutoSize = true;
            this.lblMatches.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatches.Location = new System.Drawing.Point(12, 40);
            this.lblMatches.Name = "lblMatches";
            this.lblMatches.Size = new System.Drawing.Size(290, 25);
            this.lblMatches.TabIndex = 1;
            this.lblMatches.Text = "Open Matches Remaining:";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTimer,
            this.btnLockAllMatches,
            this.toolStripSeparator1,
            this.btnFinalize});
            this.mnuOptions.Location = new System.Drawing.Point(0, 0);
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(599, 25);
            this.mnuOptions.TabIndex = 0;
            this.mnuOptions.Text = "toolStrip1";
            // 
            // btnTimer
            // 
            this.btnTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnTimer.Image")));
            this.btnTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(178, 22);
            this.btnTimer.Text = "Create Timer For This Round";
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // btnLockAllMatches
            // 
            this.btnLockAllMatches.Image = ((System.Drawing.Image)(resources.GetObject("btnLockAllMatches.Image")));
            this.btnLockAllMatches.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLockAllMatches.Name = "btnLockAllMatches";
            this.btnLockAllMatches.Size = new System.Drawing.Size(177, 22);
            this.btnLockAllMatches.Text = "Lock All Remaining Matches";
            this.btnLockAllMatches.Click += new System.EventHandler(this.btnLockAllMatches_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Image = ((System.Drawing.Image)(resources.GetObject("btnFinalize.Image")));
            this.btnFinalize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(129, 22);
            this.btnFinalize.Text = "Finalize This Round";
            this.btnFinalize.Visible = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // lblMatchesLeft
            // 
            this.lblMatchesLeft.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchesLeft.Location = new System.Drawing.Point(304, 40);
            this.lblMatchesLeft.Name = "lblMatchesLeft";
            this.lblMatchesLeft.Size = new System.Drawing.Size(72, 25);
            this.lblMatchesLeft.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(382, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "Double Click a Match to Lock or Unlock Its Scores.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // frmTournamentRound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 419);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMatchesLeft);
            this.Controls.Add(this.mnuOptions);
            this.Controls.Add(this.lblMatches);
            this.Controls.Add(this.pnlMatches);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(538, 426);
            this.Name = "frmTournamentRound";
            this.Text = "Tournament Round";
            this.mnuOptions.ResumeLayout(false);
            this.mnuOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel pnlMatches;
        private System.Windows.Forms.Label lblMatches;
        private System.Windows.Forms.ToolStrip mnuOptions;
        private System.Windows.Forms.ToolStripButton btnTimer;
        private System.Windows.Forms.ToolStripButton btnLockAllMatches;
        private System.Windows.Forms.Label lblMatchesLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFinalize;
        private System.Windows.Forms.Label label2;
    }
}