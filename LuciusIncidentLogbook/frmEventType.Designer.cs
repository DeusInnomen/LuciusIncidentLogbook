namespace KitchenGeeks
{
    partial class frmEventType
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
            this.rdoTournament = new System.Windows.Forms.RadioButton();
            this.rdoLeague = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDetails = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rdoTournament
            // 
            this.rdoTournament.AutoSize = true;
            this.rdoTournament.Checked = true;
            this.rdoTournament.Location = new System.Drawing.Point(12, 12);
            this.rdoTournament.Name = "rdoTournament";
            this.rdoTournament.Size = new System.Drawing.Size(82, 17);
            this.rdoTournament.TabIndex = 0;
            this.rdoTournament.TabStop = true;
            this.rdoTournament.Text = "Tournament";
            this.rdoTournament.UseVisualStyleBackColor = true;
            this.rdoTournament.CheckedChanged += new System.EventHandler(this.rdoTournament_CheckedChanged);
            // 
            // rdoLeague
            // 
            this.rdoLeague.AutoSize = true;
            this.rdoLeague.Location = new System.Drawing.Point(173, 12);
            this.rdoLeague.Name = "rdoLeague";
            this.rdoLeague.Size = new System.Drawing.Size(61, 17);
            this.rdoLeague.TabIndex = 1;
            this.rdoLeague.Text = "League";
            this.rdoLeague.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(78, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(159, 97);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDetails
            // 
            this.lblDetails.Location = new System.Drawing.Point(9, 41);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(225, 53);
            this.lblDetails.TabIndex = 2;
            this.lblDetails.Text = "A traditionally single day event where players are matched up against each other " +
    "based on victory conditions and play a fixed number of rounds.";
            // 
            // frmEventType
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(246, 127);
            this.ControlBox = false;
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rdoLeague);
            this.Controls.Add(this.rdoTournament);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmEventType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select the type of Event to create...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoTournament;
        private System.Windows.Forms.RadioButton rdoLeague;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDetails;
    }
}