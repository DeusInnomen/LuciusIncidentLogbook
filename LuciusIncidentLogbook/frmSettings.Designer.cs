namespace KitchenGeeks
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSeparateFiles = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtArchive = new System.Windows.Forms.TextBox();
            this.btnGetArchivePath = new System.Windows.Forms.Button();
            this.chkAutoStartWeb = new System.Windows.Forms.CheckBox();
            this.txtWebPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numWebPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDisableReportAfterSend = new System.Windows.Forms.CheckBox();
            this.chkSkipTiedRanks = new System.Windows.Forms.CheckBox();
            this.chkRandomTables = new System.Windows.Forms.CheckBox();
            this.chkOpenAfterPlayersAdded = new System.Windows.Forms.CheckBox();
            this.chkConfirmDefault = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGetBackupPath = new System.Windows.Forms.Button();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.numBackups = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAutoCheck = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtForumName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.txtSMTPPassword = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSMTPUser = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkSMTPAuth = new System.Windows.Forms.CheckBox();
            this.numSMTPPort = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSMTPFromName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSMTPServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSMTPFromEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWebPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackups)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(647, 319);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(566, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkSeparateFiles
            // 
            this.chkSeparateFiles.AutoSize = true;
            this.chkSeparateFiles.Location = new System.Drawing.Point(6, 19);
            this.chkSeparateFiles.Name = "chkSeparateFiles";
            this.chkSeparateFiles.Size = new System.Drawing.Size(263, 17);
            this.chkSeparateFiles.TabIndex = 0;
            this.chkSeparateFiles.Text = "Save Events to Individual Files Instead of One File";
            this.chkSeparateFiles.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "(If selected, Events are saved to the \"Events\" subfolder.)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Archive Events to:";
            // 
            // txtArchive
            // 
            this.txtArchive.Location = new System.Drawing.Point(101, 82);
            this.txtArchive.Name = "txtArchive";
            this.txtArchive.Size = new System.Drawing.Size(211, 20);
            this.txtArchive.TabIndex = 4;
            // 
            // btnGetArchivePath
            // 
            this.btnGetArchivePath.Location = new System.Drawing.Point(318, 79);
            this.btnGetArchivePath.Name = "btnGetArchivePath";
            this.btnGetArchivePath.Size = new System.Drawing.Size(24, 23);
            this.btnGetArchivePath.TabIndex = 5;
            this.btnGetArchivePath.Text = "...";
            this.btnGetArchivePath.UseVisualStyleBackColor = true;
            this.btnGetArchivePath.Click += new System.EventHandler(this.btnGetArchivePath_Click);
            // 
            // chkAutoStartWeb
            // 
            this.chkAutoStartWeb.AutoSize = true;
            this.chkAutoStartWeb.Location = new System.Drawing.Point(6, 19);
            this.chkAutoStartWeb.Name = "chkAutoStartWeb";
            this.chkAutoStartWeb.Size = new System.Drawing.Size(311, 17);
            this.chkAutoStartWeb.TabIndex = 0;
            this.chkAutoStartWeb.Text = "Automatically Start the Web Server When Application Starts.";
            this.chkAutoStartWeb.UseVisualStyleBackColor = true;
            // 
            // txtWebPassword
            // 
            this.txtWebPassword.Location = new System.Drawing.Point(128, 66);
            this.txtWebPassword.Name = "txtWebPassword";
            this.txtWebPassword.Size = new System.Drawing.Size(187, 20);
            this.txtWebPassword.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Web Server Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(264, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "(To disable, leave blank, but this is not recommended!)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numWebPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkAutoStartWeb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtWebPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(370, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 117);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Web Server Settings:";
            // 
            // numWebPort
            // 
            this.numWebPort.Location = new System.Drawing.Point(101, 40);
            this.numWebPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numWebPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWebPort.Name = "numWebPort";
            this.numWebPort.Size = new System.Drawing.Size(58, 20);
            this.numWebPort.TabIndex = 2;
            this.numWebPort.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Web Server Port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDisableReportAfterSend);
            this.groupBox2.Controls.Add(this.chkSkipTiedRanks);
            this.groupBox2.Controls.Add(this.chkRandomTables);
            this.groupBox2.Controls.Add(this.chkOpenAfterPlayersAdded);
            this.groupBox2.Controls.Add(this.chkConfirmDefault);
            this.groupBox2.Location = new System.Drawing.Point(15, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 159);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tournament Operation Settings";
            // 
            // chkDisableReportAfterSend
            // 
            this.chkDisableReportAfterSend.Location = new System.Drawing.Point(6, 124);
            this.chkDisableReportAfterSend.Name = "chkDisableReportAfterSend";
            this.chkDisableReportAfterSend.Size = new System.Drawing.Size(342, 31);
            this.chkDisableReportAfterSend.TabIndex = 4;
            this.chkDisableReportAfterSend.Text = "Disable the \"Send Event Report\" menu options if the report was already previously" +
    " sent.";
            this.chkDisableReportAfterSend.UseVisualStyleBackColor = true;
            // 
            // chkSkipTiedRanks
            // 
            this.chkSkipTiedRanks.Location = new System.Drawing.Point(6, 88);
            this.chkSkipTiedRanks.Name = "chkSkipTiedRanks";
            this.chkSkipTiedRanks.Size = new System.Drawing.Size(342, 30);
            this.chkSkipTiedRanks.TabIndex = 3;
            this.chkSkipTiedRanks.Text = "Skip tied ranks when assigning. (e.g. 1, 2, 2, 4, 5 instead of 1, 2, 2, 3, 4.)";
            this.chkSkipTiedRanks.UseVisualStyleBackColor = true;
            // 
            // chkRandomTables
            // 
            this.chkRandomTables.AutoSize = true;
            this.chkRandomTables.Location = new System.Drawing.Point(6, 65);
            this.chkRandomTables.Name = "chkRandomTables";
            this.chkRandomTables.Size = new System.Drawing.Size(342, 17);
            this.chkRandomTables.TabIndex = 2;
            this.chkRandomTables.Text = "Assign tables for Rounds after the first randomly, instead of by rank.";
            this.chkRandomTables.UseVisualStyleBackColor = true;
            // 
            // chkOpenAfterPlayersAdded
            // 
            this.chkOpenAfterPlayersAdded.AutoSize = true;
            this.chkOpenAfterPlayersAdded.Location = new System.Drawing.Point(6, 42);
            this.chkOpenAfterPlayersAdded.Name = "chkOpenAfterPlayersAdded";
            this.chkOpenAfterPlayersAdded.Size = new System.Drawing.Size(309, 17);
            this.chkOpenAfterPlayersAdded.TabIndex = 1;
            this.chkOpenAfterPlayersAdded.Text = "After adding players to a tournament, open it up immediately.";
            this.chkOpenAfterPlayersAdded.UseVisualStyleBackColor = true;
            // 
            // chkConfirmDefault
            // 
            this.chkConfirmDefault.AutoSize = true;
            this.chkConfirmDefault.Location = new System.Drawing.Point(6, 19);
            this.chkConfirmDefault.Name = "chkConfirmDefault";
            this.chkConfirmDefault.Size = new System.Drawing.Size(313, 17);
            this.chkConfirmDefault.TabIndex = 0;
            this.chkConfirmDefault.Text = "By default, confirm before starting a new Tournament Round.";
            this.chkConfirmDefault.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnGetBackupPath);
            this.groupBox3.Controls.Add(this.txtBackupPath);
            this.groupBox3.Controls.Add(this.numBackups);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.chkAutoCheck);
            this.groupBox3.Controls.Add(this.chkSeparateFiles);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnGetArchivePath);
            this.groupBox3.Controls.Add(this.txtArchive);
            this.groupBox3.Location = new System.Drawing.Point(15, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 160);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(205, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "(0 disables backups.)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Write Backups to:";
            // 
            // btnGetBackupPath
            // 
            this.btnGetBackupPath.Location = new System.Drawing.Point(318, 131);
            this.btnGetBackupPath.Name = "btnGetBackupPath";
            this.btnGetBackupPath.Size = new System.Drawing.Size(24, 23);
            this.btnGetBackupPath.TabIndex = 11;
            this.btnGetBackupPath.Text = "...";
            this.btnGetBackupPath.UseVisualStyleBackColor = true;
            this.btnGetBackupPath.Click += new System.EventHandler(this.btnGetBackupPath_Click);
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Location = new System.Drawing.Point(101, 134);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.Size = new System.Drawing.Size(211, 20);
            this.txtBackupPath.TabIndex = 10;
            // 
            // numBackups
            // 
            this.numBackups.Location = new System.Drawing.Point(157, 108);
            this.numBackups.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numBackups.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBackups.Name = "numBackups";
            this.numBackups.Size = new System.Drawing.Size(43, 20);
            this.numBackups.TabIndex = 7;
            this.numBackups.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Backups of Settings to Keep:";
            // 
            // chkAutoCheck
            // 
            this.chkAutoCheck.AutoSize = true;
            this.chkAutoCheck.Location = new System.Drawing.Point(6, 59);
            this.chkAutoCheck.Name = "chkAutoCheck";
            this.chkAutoCheck.Size = new System.Drawing.Size(229, 17);
            this.chkAutoCheck.TabIndex = 2;
            this.chkAutoCheck.Text = "Automatically Check for Updates at Startup";
            this.chkAutoCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtForumName);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.btnTestEmail);
            this.groupBox4.Controls.Add(this.txtSMTPPassword);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtSMTPUser);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.chkSMTPAuth);
            this.groupBox4.Controls.Add(this.numSMTPPort);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtSMTPFromName);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtSMTPServer);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtSMTPFromEmail);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(370, 141);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(349, 169);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Email Settings";
            // 
            // txtForumName
            // 
            this.txtForumName.Location = new System.Drawing.Point(80, 45);
            this.txtForumName.Name = "txtForumName";
            this.txtForumName.Size = new System.Drawing.Size(187, 20);
            this.txtForumName.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Forum Name:";
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestEmail.Location = new System.Drawing.Point(290, 27);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(53, 39);
            this.btnTestEmail.TabIndex = 15;
            this.btnTestEmail.Text = "Test SMTP";
            this.btnTestEmail.UseVisualStyleBackColor = true;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // txtSMTPPassword
            // 
            this.txtSMTPPassword.Enabled = false;
            this.txtSMTPPassword.Location = new System.Drawing.Point(243, 140);
            this.txtSMTPPassword.Name = "txtSMTPPassword";
            this.txtSMTPPassword.Size = new System.Drawing.Size(100, 20);
            this.txtSMTPPassword.TabIndex = 14;
            this.txtSMTPPassword.UseSystemPasswordChar = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(186, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Password:";
            // 
            // txtSMTPUser
            // 
            this.txtSMTPUser.Enabled = false;
            this.txtSMTPUser.Location = new System.Drawing.Point(80, 140);
            this.txtSMTPUser.Name = "txtSMTPUser";
            this.txtSMTPUser.Size = new System.Drawing.Size(100, 20);
            this.txtSMTPUser.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Username:";
            // 
            // chkSMTPAuth
            // 
            this.chkSMTPAuth.AutoSize = true;
            this.chkSMTPAuth.Location = new System.Drawing.Point(9, 120);
            this.chkSMTPAuth.Name = "chkSMTPAuth";
            this.chkSMTPAuth.Size = new System.Drawing.Size(143, 17);
            this.chkSMTPAuth.TabIndex = 10;
            this.chkSMTPAuth.Text = "Authentication Required:";
            this.chkSMTPAuth.UseVisualStyleBackColor = true;
            this.chkSMTPAuth.CheckedChanged += new System.EventHandler(this.chkSMTPAuth_CheckedChanged);
            // 
            // numSMTPPort
            // 
            this.numSMTPPort.Location = new System.Drawing.Point(286, 97);
            this.numSMTPPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSMTPPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSMTPPort.Name = "numSMTPPort";
            this.numSMTPPort.Size = new System.Drawing.Size(53, 20);
            this.numSMTPPort.TabIndex = 9;
            this.numSMTPPort.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(251, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Port:";
            // 
            // txtSMTPFromName
            // 
            this.txtSMTPFromName.Location = new System.Drawing.Point(80, 19);
            this.txtSMTPFromName.Name = "txtSMTPFromName";
            this.txtSMTPFromName.Size = new System.Drawing.Size(187, 20);
            this.txtSMTPFromName.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Real Name:";
            // 
            // txtSMTPServer
            // 
            this.txtSMTPServer.Location = new System.Drawing.Point(80, 97);
            this.txtSMTPServer.Name = "txtSMTPServer";
            this.txtSMTPServer.Size = new System.Drawing.Size(165, 20);
            this.txtSMTPServer.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Mail Server:";
            // 
            // txtSMTPFromEmail
            // 
            this.txtSMTPFromEmail.Location = new System.Drawing.Point(80, 71);
            this.txtSMTPFromEmail.Name = "txtSMTPFromEmail";
            this.txtSMTPFromEmail.Size = new System.Drawing.Size(187, 20);
            this.txtSMTPFromEmail.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Your Email:";
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(734, 354);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWebPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackups)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkSeparateFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArchive;
        private System.Windows.Forms.Button btnGetArchivePath;
        private System.Windows.Forms.CheckBox chkAutoStartWeb;
        private System.Windows.Forms.TextBox txtWebPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkRandomTables;
        private System.Windows.Forms.CheckBox chkOpenAfterPlayersAdded;
        private System.Windows.Forms.CheckBox chkConfirmDefault;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numWebPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGetBackupPath;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.NumericUpDown numBackups;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkSkipTiedRanks;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSMTPFromName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSMTPServer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSMTPFromEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnTestEmail;
        private System.Windows.Forms.TextBox txtSMTPPassword;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSMTPUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkSMTPAuth;
        private System.Windows.Forms.NumericUpDown numSMTPPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtForumName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkDisableReportAfterSend;
    }
}