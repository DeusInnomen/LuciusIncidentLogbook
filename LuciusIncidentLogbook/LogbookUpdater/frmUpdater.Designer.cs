namespace LogbookUpdater
{
    partial class frmUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdater));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkPassive = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtWebVersion = new System.Windows.Forms.TextBox();
            this.txtLocalVersion = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(238, 28);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkPassive
            // 
            this.chkPassive.AutoSize = true;
            this.chkPassive.Location = new System.Drawing.Point(339, 17);
            this.chkPassive.Name = "chkPassive";
            this.chkPassive.Size = new System.Drawing.Size(108, 17);
            this.chkPassive.TabIndex = 14;
            this.chkPassive.Text = "Use Passive FTP";
            this.chkPassive.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(238, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update!";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtWebVersion
            // 
            this.txtWebVersion.Location = new System.Drawing.Point(132, 28);
            this.txtWebVersion.Name = "txtWebVersion";
            this.txtWebVersion.ReadOnly = true;
            this.txtWebVersion.Size = new System.Drawing.Size(100, 20);
            this.txtWebVersion.TabIndex = 12;
            // 
            // txtLocalVersion
            // 
            this.txtLocalVersion.Location = new System.Drawing.Point(132, 4);
            this.txtLocalVersion.Name = "txtLocalVersion";
            this.txtLocalVersion.ReadOnly = true;
            this.txtLocalVersion.Size = new System.Drawing.Size(100, 20);
            this.txtLocalVersion.TabIndex = 11;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 31);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(108, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Current Web Version:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Local Version Number:";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(0, 54);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(652, 361);
            this.txtLog.TabIndex = 8;
            this.txtLog.Text = "";
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 417);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkPassive);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtWebVersion);
            this.Controls.Add(this.txtLocalVersion);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpdater";
            this.Text = "Lucius Incident Logbook Web Updater";
            this.Load += new System.EventHandler(this.frmUpdater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.CheckBox chkPassive;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.TextBox txtWebVersion;
        internal System.Windows.Forms.TextBox txtLocalVersion;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.RichTextBox txtLog;
    }
}

