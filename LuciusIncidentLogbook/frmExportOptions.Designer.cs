namespace KitchenGeeks
{
    partial class frmExportOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportOptions));
            this.grpExportPlayers = new System.Windows.Forms.GroupBox();
            this.chkExportNone = new System.Windows.Forms.RadioButton();
            this.chkExportPlayersOnly = new System.Windows.Forms.RadioButton();
            this.chkExportAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnGetPath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpExportPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpExportPlayers
            // 
            this.grpExportPlayers.Controls.Add(this.chkExportNone);
            this.grpExportPlayers.Controls.Add(this.chkExportPlayersOnly);
            this.grpExportPlayers.Controls.Add(this.chkExportAll);
            this.grpExportPlayers.Location = new System.Drawing.Point(12, 65);
            this.grpExportPlayers.Name = "grpExportPlayers";
            this.grpExportPlayers.Size = new System.Drawing.Size(191, 91);
            this.grpExportPlayers.TabIndex = 0;
            this.grpExportPlayers.TabStop = false;
            this.grpExportPlayers.Text = "How to Handle Players and Results:";
            // 
            // chkExportNone
            // 
            this.chkExportNone.AutoSize = true;
            this.chkExportNone.Checked = true;
            this.chkExportNone.Location = new System.Drawing.Point(6, 65);
            this.chkExportNone.Name = "chkExportNone";
            this.chkExportNone.Size = new System.Drawing.Size(179, 17);
            this.chkExportNone.TabIndex = 2;
            this.chkExportNone.TabStop = true;
            this.chkExportNone.Text = "Do Not Export Players or Results";
            this.chkExportNone.UseVisualStyleBackColor = true;
            // 
            // chkExportPlayersOnly
            // 
            this.chkExportPlayersOnly.AutoSize = true;
            this.chkExportPlayersOnly.Location = new System.Drawing.Point(6, 42);
            this.chkExportPlayersOnly.Name = "chkExportPlayersOnly";
            this.chkExportPlayersOnly.Size = new System.Drawing.Size(174, 17);
            this.chkExportPlayersOnly.TabIndex = 1;
            this.chkExportPlayersOnly.Text = "Export Players Only, No Results";
            this.chkExportPlayersOnly.UseVisualStyleBackColor = true;
            // 
            // chkExportAll
            // 
            this.chkExportAll.AutoSize = true;
            this.chkExportAll.Location = new System.Drawing.Point(6, 19);
            this.chkExportAll.Name = "chkExportAll";
            this.chkExportAll.Size = new System.Drawing.Size(165, 17);
            this.chkExportAll.TabIndex = 0;
            this.chkExportAll.Text = "Export All Players and Results";
            this.chkExportAll.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(490, 53);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location to Export To:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(212, 120);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(257, 20);
            this.txtPath.TabIndex = 3;
            // 
            // btnGetPath
            // 
            this.btnGetPath.Location = new System.Drawing.Point(475, 117);
            this.btnGetPath.Name = "btnGetPath";
            this.btnGetPath.Size = new System.Drawing.Size(24, 23);
            this.btnGetPath.TabIndex = 21;
            this.btnGetPath.Text = "...";
            this.btnGetPath.UseVisualStyleBackColor = true;
            this.btnGetPath.Click += new System.EventHandler(this.btnGetPath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Event to Export:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(212, 81);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(287, 20);
            this.txtName.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(343, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(424, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmExportOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(511, 185);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGetPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpExportPlayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmExportOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Event Options";
            this.grpExportPlayers.ResumeLayout(false);
            this.grpExportPlayers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpExportPlayers;
        private System.Windows.Forms.RadioButton chkExportNone;
        private System.Windows.Forms.RadioButton chkExportPlayersOnly;
        private System.Windows.Forms.RadioButton chkExportAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnGetPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}