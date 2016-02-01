namespace KitchenGeeks
{
    partial class FrmViewLeague
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewLeague));
            this.lstResults = new System.Windows.Forms.ListView();
            this.colRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.numTopRows = new System.Windows.Forms.NumericUpDown();
            this.chkHighlight = new System.Windows.Forms.CheckBox();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnEnterResults = new System.Windows.Forms.Button();
            this.grpRankBy = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoMatch = new System.Windows.Forms.RadioButton();
            this.rdoAchievement = new System.Windows.Forms.RadioButton();
            this.btnExportResults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTopRows)).BeginInit();
            this.grpRankBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRank,
            this.colName});
            this.lstResults.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstResults.FullRowSelect = true;
            this.lstResults.GridLines = true;
            this.lstResults.Location = new System.Drawing.Point(0, 0);
            this.lstResults.MultiSelect = false;
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(392, 313);
            this.lstResults.TabIndex = 0;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            this.lstResults.View = System.Windows.Forms.View.Details;
            // 
            // colRank
            // 
            this.colRank.Text = "Rank";
            this.colRank.Width = 40;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 102;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Players";
            // 
            // numTopRows
            // 
            this.numTopRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numTopRows.BackColor = System.Drawing.Color.White;
            this.numTopRows.Location = new System.Drawing.Point(486, 7);
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
            this.numTopRows.Size = new System.Drawing.Size(40, 20);
            this.numTopRows.TabIndex = 5;
            this.numTopRows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numTopRows.ValueChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
            // 
            // chkHighlight
            // 
            this.chkHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHighlight.AutoSize = true;
            this.chkHighlight.Checked = true;
            this.chkHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighlight.Location = new System.Drawing.Point(399, 8);
            this.chkHighlight.Name = "chkHighlight";
            this.chkHighlight.Size = new System.Drawing.Size(89, 17);
            this.chkHighlight.TabIndex = 4;
            this.chkHighlight.Text = "Highlight Top";
            this.chkHighlight.UseVisualStyleBackColor = true;
            this.chkHighlight.CheckedChanged += new System.EventHandler(this.chkHighlight_CheckedChanged);
            // 
            // btnNotes
            // 
            this.btnNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotes.Location = new System.Drawing.Point(447, 153);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(129, 23);
            this.btnNotes.TabIndex = 11;
            this.btnNotes.Text = "Show Notes";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnEnterResults
            // 
            this.btnEnterResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnterResults.Location = new System.Drawing.Point(447, 124);
            this.btnEnterResults.Name = "btnEnterResults";
            this.btnEnterResults.Size = new System.Drawing.Size(129, 23);
            this.btnEnterResults.TabIndex = 12;
            this.btnEnterResults.Text = "Enter Results";
            this.btnEnterResults.UseVisualStyleBackColor = true;
            this.btnEnterResults.Click += new System.EventHandler(this.btnEnterResults_Click);
            // 
            // grpRankBy
            // 
            this.grpRankBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRankBy.Controls.Add(this.label2);
            this.grpRankBy.Controls.Add(this.rdoMatch);
            this.grpRankBy.Controls.Add(this.rdoAchievement);
            this.grpRankBy.Location = new System.Drawing.Point(399, 33);
            this.grpRankBy.Name = "grpRankBy";
            this.grpRankBy.Size = new System.Drawing.Size(225, 70);
            this.grpRankBy.TabIndex = 13;
            this.grpRankBy.TabStop = false;
            this.grpRankBy.Text = "Rank By...";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ties will be broken by the other method, followed by declaring they\'re the same r" +
    "ank.";
            // 
            // rdoMatch
            // 
            this.rdoMatch.AutoSize = true;
            this.rdoMatch.Location = new System.Drawing.Point(131, 19);
            this.rdoMatch.Name = "rdoMatch";
            this.rdoMatch.Size = new System.Drawing.Size(93, 17);
            this.rdoMatch.TabIndex = 1;
            this.rdoMatch.Text = "Match Results";
            this.rdoMatch.UseVisualStyleBackColor = true;
            // 
            // rdoAchievement
            // 
            this.rdoAchievement.AutoSize = true;
            this.rdoAchievement.Checked = true;
            this.rdoAchievement.Location = new System.Drawing.Point(6, 19);
            this.rdoAchievement.Name = "rdoAchievement";
            this.rdoAchievement.Size = new System.Drawing.Size(119, 17);
            this.rdoAchievement.TabIndex = 0;
            this.rdoAchievement.TabStop = true;
            this.rdoAchievement.Text = "Achievement Points";
            this.rdoAchievement.UseVisualStyleBackColor = true;
            this.rdoAchievement.CheckedChanged += new System.EventHandler(this.rdoAchievement_CheckedChanged);
            // 
            // btnExportResults
            // 
            this.btnExportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportResults.Location = new System.Drawing.Point(447, 182);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(129, 23);
            this.btnExportResults.TabIndex = 14;
            this.btnExportResults.Text = "Export Event Results";
            this.btnExportResults.UseVisualStyleBackColor = true;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // FrmViewLeague
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 313);
            this.Controls.Add(this.btnExportResults);
            this.Controls.Add(this.grpRankBy);
            this.Controls.Add(this.btnEnterResults);
            this.Controls.Add(this.btnNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numTopRows);
            this.Controls.Add(this.chkHighlight);
            this.Controls.Add(this.lstResults);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(647, 265);
            this.Name = "FrmViewLeague";
            this.Text = "View League";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewLeague_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmViewLeague_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numTopRows)).EndInit();
            this.grpRankBy.ResumeLayout(false);
            this.grpRankBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colRank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTopRows;
        private System.Windows.Forms.CheckBox chkHighlight;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnEnterResults;
        private System.Windows.Forms.GroupBox grpRankBy;
        private System.Windows.Forms.RadioButton rdoMatch;
        private System.Windows.Forms.RadioButton rdoAchievement;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportResults;
    }
}