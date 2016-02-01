namespace KitchenGeeks
{
    partial class frmTimer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimer));
            this.lblTime = new System.Windows.Forms.Label();
            this.btnToggle = new System.Windows.Forms.Button();
            this.mnuReset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetClockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlaySound = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuReset.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Font = new System.Drawing.Font("Verdana", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(0, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(284, 103);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "70:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnToggle
            // 
            this.btnToggle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnToggle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggle.Location = new System.Drawing.Point(85, 110);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(114, 47);
            this.btnToggle.TabIndex = 1;
            this.btnToggle.Text = "Start";
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // mnuReset
            // 
            this.mnuReset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetClockToolStripMenuItem,
            this.mnuPlaySound});
            this.mnuReset.Name = "mnuReset";
            this.mnuReset.Size = new System.Drawing.Size(199, 48);
            // 
            // resetClockToolStripMenuItem
            // 
            this.resetClockToolStripMenuItem.Name = "resetClockToolStripMenuItem";
            this.resetClockToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.resetClockToolStripMenuItem.Text = "&Reset Clock";
            this.resetClockToolStripMenuItem.Click += new System.EventHandler(this.resetClockToolStripMenuItem_Click);
            // 
            // mnuPlaySound
            // 
            this.mnuPlaySound.Checked = true;
            this.mnuPlaySound.CheckOnClick = true;
            this.mnuPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuPlaySound.Name = "mnuPlaySound";
            this.mnuPlaySound.Size = new System.Drawing.Size(198, 22);
            this.mnuPlaySound.Text = "Play &Sound When Done";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Right Click\r\nfor Options";
            // 
            // frmTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.ContextMenuStrip = this.mnuReset;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnToggle);
            this.Controls.Add(this.lblTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 207);
            this.Name = "frmTimer";
            this.Text = "Timer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimer_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmTimer_SizeChanged);
            this.mnuReset.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.ContextMenuStrip mnuReset;
        private System.Windows.Forms.ToolStripMenuItem resetClockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPlaySound;
        private System.Windows.Forms.Label label1;
    }
}