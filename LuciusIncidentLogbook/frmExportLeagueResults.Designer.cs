namespace KitchenGeeks
{
    partial class FrmExportLeagueResults
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetExport = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpMethod = new System.Windows.Forms.GroupBox();
            this.rdoExportResults = new System.Windows.Forms.RadioButton();
            this.rdoExportTable = new System.Windows.Forms.RadioButton();
            this.rdoExportPlayer = new System.Windows.Forms.RadioButton();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.rdoOrderTable = new System.Windows.Forms.RadioButton();
            this.rdoOrderFirst = new System.Windows.Forms.RadioButton();
            this.rdoOrderLast = new System.Windows.Forms.RadioButton();
            this.grpFormat = new System.Windows.Forms.GroupBox();
            this.rdoHTML = new System.Windows.Forms.RadioButton();
            this.rdoBBCode = new System.Windows.Forms.RadioButton();
            this.rdoCSV = new System.Windows.Forms.RadioButton();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.rdoWriteClipboard = new System.Windows.Forms.RadioButton();
            this.rdoWriteFile = new System.Windows.Forms.RadioButton();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkShowName = new System.Windows.Forms.CheckBox();
            this.chkShowFactions = new System.Windows.Forms.CheckBox();
            this.chkExcludeForfeits = new System.Windows.Forms.CheckBox();
            this.grpMethod.SuspendLayout();
            this.grpOrder.SuspendLayout();
            this.grpFormat.SuspendLayout();
            this.grpType.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Export Results to:";
            // 
            // btnGetExport
            // 
            this.btnGetExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetExport.Location = new System.Drawing.Point(396, 57);
            this.btnGetExport.Name = "btnGetExport";
            this.btnGetExport.Size = new System.Drawing.Size(24, 23);
            this.btnGetExport.TabIndex = 8;
            this.btnGetExport.Text = "...";
            this.btnGetExport.UseVisualStyleBackColor = true;
            this.btnGetExport.Click += new System.EventHandler(this.btnGetExport_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.Location = new System.Drawing.Point(108, 59);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(282, 20);
            this.txtFilename.TabIndex = 7;
            this.txtFilename.TextChanged += new System.EventHandler(this.CheckForReady);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(268, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(349, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(412, 45);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "This will export the results of the \"%name%\" tournament either to the clipboard o" +
    "r to a Comma Separated Value (CSV) file.";
            // 
            // grpMethod
            // 
            this.grpMethod.Controls.Add(this.rdoExportResults);
            this.grpMethod.Controls.Add(this.rdoExportTable);
            this.grpMethod.Controls.Add(this.rdoExportPlayer);
            this.grpMethod.Location = new System.Drawing.Point(12, 89);
            this.grpMethod.Name = "grpMethod";
            this.grpMethod.Size = new System.Drawing.Size(149, 132);
            this.grpMethod.TabIndex = 12;
            this.grpMethod.TabStop = false;
            this.grpMethod.Text = "Export Method";
            // 
            // rdoExportResults
            // 
            this.rdoExportResults.Location = new System.Drawing.Point(6, 97);
            this.rdoExportResults.Name = "rdoExportResults";
            this.rdoExportResults.Size = new System.Drawing.Size(130, 24);
            this.rdoExportResults.TabIndex = 2;
            this.rdoExportResults.Text = "Overall Event Results";
            this.rdoExportResults.UseVisualStyleBackColor = true;
            this.rdoExportResults.CheckedChanged += new System.EventHandler(this.rdoExportResults_CheckedChanged);
            // 
            // rdoExportTable
            // 
            this.rdoExportTable.Location = new System.Drawing.Point(6, 56);
            this.rdoExportTable.Name = "rdoExportTable";
            this.rdoExportTable.Size = new System.Drawing.Size(140, 35);
            this.rdoExportTable.TabIndex = 1;
            this.rdoExportTable.Text = "Export Results of Each Table Per Round";
            this.rdoExportTable.UseVisualStyleBackColor = true;
            this.rdoExportTable.CheckedChanged += new System.EventHandler(this.rdoExportTable_CheckedChanged);
            // 
            // rdoExportPlayer
            // 
            this.rdoExportPlayer.Checked = true;
            this.rdoExportPlayer.Location = new System.Drawing.Point(6, 19);
            this.rdoExportPlayer.Name = "rdoExportPlayer";
            this.rdoExportPlayer.Size = new System.Drawing.Size(140, 39);
            this.rdoExportPlayer.TabIndex = 0;
            this.rdoExportPlayer.TabStop = true;
            this.rdoExportPlayer.Text = "Export Individual Player Results per Round";
            this.rdoExportPlayer.UseVisualStyleBackColor = true;
            // 
            // grpOrder
            // 
            this.grpOrder.Controls.Add(this.rdoOrderTable);
            this.grpOrder.Controls.Add(this.rdoOrderFirst);
            this.grpOrder.Controls.Add(this.rdoOrderLast);
            this.grpOrder.Location = new System.Drawing.Point(12, 227);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(149, 87);
            this.grpOrder.TabIndex = 13;
            this.grpOrder.TabStop = false;
            this.grpOrder.Text = "Sortation";
            // 
            // rdoOrderTable
            // 
            this.rdoOrderTable.AutoSize = true;
            this.rdoOrderTable.Location = new System.Drawing.Point(6, 65);
            this.rdoOrderTable.Name = "rdoOrderTable";
            this.rdoOrderTable.Size = new System.Drawing.Size(135, 17);
            this.rdoOrderTable.TabIndex = 2;
            this.rdoOrderTable.Text = "Order by Table Number";
            this.rdoOrderTable.UseVisualStyleBackColor = true;
            // 
            // rdoOrderFirst
            // 
            this.rdoOrderFirst.AutoSize = true;
            this.rdoOrderFirst.Location = new System.Drawing.Point(6, 42);
            this.rdoOrderFirst.Name = "rdoOrderFirst";
            this.rdoOrderFirst.Size = new System.Drawing.Size(118, 17);
            this.rdoOrderFirst.TabIndex = 1;
            this.rdoOrderFirst.Text = "Order by First Name";
            this.rdoOrderFirst.UseVisualStyleBackColor = true;
            // 
            // rdoOrderLast
            // 
            this.rdoOrderLast.AutoSize = true;
            this.rdoOrderLast.Checked = true;
            this.rdoOrderLast.Location = new System.Drawing.Point(6, 19);
            this.rdoOrderLast.Name = "rdoOrderLast";
            this.rdoOrderLast.Size = new System.Drawing.Size(119, 17);
            this.rdoOrderLast.TabIndex = 0;
            this.rdoOrderLast.TabStop = true;
            this.rdoOrderLast.Text = "Order by Last Name";
            this.rdoOrderLast.UseVisualStyleBackColor = true;
            // 
            // grpFormat
            // 
            this.grpFormat.Controls.Add(this.rdoHTML);
            this.grpFormat.Controls.Add(this.rdoBBCode);
            this.grpFormat.Controls.Add(this.rdoCSV);
            this.grpFormat.Location = new System.Drawing.Point(167, 89);
            this.grpFormat.Name = "grpFormat";
            this.grpFormat.Size = new System.Drawing.Size(126, 91);
            this.grpFormat.TabIndex = 14;
            this.grpFormat.TabStop = false;
            this.grpFormat.Text = "Output Format";
            // 
            // rdoHTML
            // 
            this.rdoHTML.AutoSize = true;
            this.rdoHTML.Location = new System.Drawing.Point(6, 65);
            this.rdoHTML.Name = "rdoHTML";
            this.rdoHTML.Size = new System.Drawing.Size(55, 17);
            this.rdoHTML.TabIndex = 2;
            this.rdoHTML.Text = "HTML";
            this.rdoHTML.UseVisualStyleBackColor = true;
            // 
            // rdoBBCode
            // 
            this.rdoBBCode.AutoSize = true;
            this.rdoBBCode.Location = new System.Drawing.Point(6, 42);
            this.rdoBBCode.Name = "rdoBBCode";
            this.rdoBBCode.Size = new System.Drawing.Size(105, 17);
            this.rdoBBCode.TabIndex = 1;
            this.rdoBBCode.Text = "BB Code (Forum)";
            this.rdoBBCode.UseVisualStyleBackColor = true;
            // 
            // rdoCSV
            // 
            this.rdoCSV.AutoSize = true;
            this.rdoCSV.Checked = true;
            this.rdoCSV.Location = new System.Drawing.Point(6, 19);
            this.rdoCSV.Name = "rdoCSV";
            this.rdoCSV.Size = new System.Drawing.Size(46, 17);
            this.rdoCSV.TabIndex = 0;
            this.rdoCSV.TabStop = true;
            this.rdoCSV.Text = "CSV";
            this.rdoCSV.UseVisualStyleBackColor = true;
            this.rdoCSV.CheckedChanged += new System.EventHandler(this.rdoCSV_CheckedChanged);
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rdoWriteClipboard);
            this.grpType.Controls.Add(this.rdoWriteFile);
            this.grpType.Location = new System.Drawing.Point(299, 89);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(121, 66);
            this.grpType.TabIndex = 15;
            this.grpType.TabStop = false;
            this.grpType.Text = "Output Type";
            // 
            // rdoWriteClipboard
            // 
            this.rdoWriteClipboard.AutoSize = true;
            this.rdoWriteClipboard.Location = new System.Drawing.Point(6, 42);
            this.rdoWriteClipboard.Name = "rdoWriteClipboard";
            this.rdoWriteClipboard.Size = new System.Drawing.Size(109, 17);
            this.rdoWriteClipboard.TabIndex = 1;
            this.rdoWriteClipboard.Text = "Write to Clipboard";
            this.rdoWriteClipboard.UseVisualStyleBackColor = true;
            // 
            // rdoWriteFile
            // 
            this.rdoWriteFile.AutoSize = true;
            this.rdoWriteFile.Checked = true;
            this.rdoWriteFile.Location = new System.Drawing.Point(6, 19);
            this.rdoWriteFile.Name = "rdoWriteFile";
            this.rdoWriteFile.Size = new System.Drawing.Size(81, 17);
            this.rdoWriteFile.TabIndex = 0;
            this.rdoWriteFile.TabStop = true;
            this.rdoWriteFile.Text = "Write to File";
            this.rdoWriteFile.UseVisualStyleBackColor = true;
            this.rdoWriteFile.CheckedChanged += new System.EventHandler(this.CheckForReady);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkExcludeForfeits);
            this.grpOptions.Controls.Add(this.chkShowFactions);
            this.grpOptions.Controls.Add(this.chkShowName);
            this.grpOptions.Location = new System.Drawing.Point(167, 183);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(253, 100);
            this.grpOptions.TabIndex = 16;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Additional Export Options";
            // 
            // chkShowName
            // 
            this.chkShowName.AutoSize = true;
            this.chkShowName.Enabled = false;
            this.chkShowName.Location = new System.Drawing.Point(6, 19);
            this.chkShowName.Name = "chkShowName";
            this.chkShowName.Size = new System.Drawing.Size(178, 17);
            this.chkShowName.TabIndex = 0;
            this.chkShowName.Text = "Show Tournament Name at Top";
            this.chkShowName.UseVisualStyleBackColor = true;
            // 
            // chkShowFactions
            // 
            this.chkShowFactions.AutoSize = true;
            this.chkShowFactions.Location = new System.Drawing.Point(6, 42);
            this.chkShowFactions.Name = "chkShowFactions";
            this.chkShowFactions.Size = new System.Drawing.Size(143, 17);
            this.chkShowFactions.TabIndex = 1;
            this.chkShowFactions.Text = "Show Faction Selections";
            this.chkShowFactions.UseVisualStyleBackColor = true;
            // 
            // chkExcludeForfeits
            // 
            this.chkExcludeForfeits.AutoSize = true;
            this.chkExcludeForfeits.Location = new System.Drawing.Point(6, 65);
            this.chkExcludeForfeits.Name = "chkExcludeForfeits";
            this.chkExcludeForfeits.Size = new System.Drawing.Size(145, 17);
            this.chkExcludeForfeits.TabIndex = 2;
            this.chkExcludeForfeits.Text = "Exclude Forfeited Players";
            this.chkExcludeForfeits.UseVisualStyleBackColor = true;
            // 
            // FrmExportResults
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(436, 329);
            this.ControlBox = false;
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.grpFormat);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.grpMethod);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGetExport);
            this.Controls.Add(this.txtFilename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmExportResults";
            this.Text = "Output Tournament Results...";
            this.grpMethod.ResumeLayout(false);
            this.grpOrder.ResumeLayout(false);
            this.grpOrder.PerformLayout();
            this.grpFormat.ResumeLayout(false);
            this.grpFormat.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetExport;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpMethod;
        private System.Windows.Forms.RadioButton rdoExportTable;
        private System.Windows.Forms.RadioButton rdoExportPlayer;
        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.RadioButton rdoOrderTable;
        private System.Windows.Forms.RadioButton rdoOrderFirst;
        private System.Windows.Forms.RadioButton rdoOrderLast;
        private System.Windows.Forms.GroupBox grpFormat;
        private System.Windows.Forms.RadioButton rdoHTML;
        private System.Windows.Forms.RadioButton rdoBBCode;
        private System.Windows.Forms.RadioButton rdoCSV;
        private System.Windows.Forms.RadioButton rdoExportResults;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton rdoWriteClipboard;
        private System.Windows.Forms.RadioButton rdoWriteFile;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkExcludeForfeits;
        private System.Windows.Forms.CheckBox chkShowFactions;
        private System.Windows.Forms.CheckBox chkShowName;
    }
}