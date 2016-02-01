namespace KitchenGeeks
{
    partial class FrmTournamentPlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTournamentPlayers));
            this.lstAvailable = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SortImages = new System.Windows.Forms.ImageList(this.components);
            this.lstEnrolled = new System.Windows.Forms.ListView();
            this.colEnrolledName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnrolledLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnrolledID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnToEnrolled = new System.Windows.Forms.Button();
            this.btnFromEnrolled = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEnrolled = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.colEnrolledFaction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.lstAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colLocation});
            this.lstAvailable.FullRowSelect = true;
            this.lstAvailable.GridLines = true;
            this.lstAvailable.HideSelection = false;
            this.lstAvailable.Location = new System.Drawing.Point(12, 27);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(240, 317);
            this.lstAvailable.SmallImageList = this.SortImages;
            this.lstAvailable.TabIndex = 1;
            this.lstAvailable.UseCompatibleStateImageBehavior = false;
            this.lstAvailable.View = System.Windows.Forms.View.Details;
            this.lstAvailable.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstAvailable_ColumnClick);
            this.lstAvailable.DoubleClick += new System.EventHandler(this.lstAvailable_DoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 121;
            // 
            // colLocation
            // 
            this.colLocation.Text = "Location";
            this.colLocation.Width = 91;
            // 
            // SortImages
            // 
            this.SortImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SortImages.ImageStream")));
            this.SortImages.TransparentColor = System.Drawing.Color.Transparent;
            this.SortImages.Images.SetKeyName(0, "Down.png");
            this.SortImages.Images.SetKeyName(1, "Up.png");
            // 
            // lstEnrolled
            // 
            this.lstEnrolled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEnrolled.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEnrolledName,
            this.colEnrolledLocation,
            this.colEnrolledID,
            this.colEnrolledFaction});
            this.lstEnrolled.FullRowSelect = true;
            this.lstEnrolled.GridLines = true;
            this.lstEnrolled.HideSelection = false;
            this.lstEnrolled.Location = new System.Drawing.Point(303, 27);
            this.lstEnrolled.Name = "lstEnrolled";
            this.lstEnrolled.Size = new System.Drawing.Size(401, 262);
            this.lstEnrolled.SmallImageList = this.SortImages;
            this.lstEnrolled.TabIndex = 3;
            this.lstEnrolled.UseCompatibleStateImageBehavior = false;
            this.lstEnrolled.View = System.Windows.Forms.View.Details;
            this.lstEnrolled.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstEnrolled_ColumnClick);
            this.lstEnrolled.DoubleClick += new System.EventHandler(this.lstEnrolled_DoubleClick);
            // 
            // colEnrolledName
            // 
            this.colEnrolledName.Text = "Name";
            this.colEnrolledName.Width = 121;
            // 
            // colEnrolledLocation
            // 
            this.colEnrolledLocation.Text = "Location";
            this.colEnrolledLocation.Width = 91;
            // 
            // colEnrolledID
            // 
            this.colEnrolledID.Text = "ID";
            this.colEnrolledID.Width = 35;
            // 
            // btnToEnrolled
            // 
            this.btnToEnrolled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnToEnrolled.Image = global::KitchenGeeks.Properties.Resources.RightArrow;
            this.btnToEnrolled.Location = new System.Drawing.Point(258, 181);
            this.btnToEnrolled.Name = "btnToEnrolled";
            this.btnToEnrolled.Size = new System.Drawing.Size(35, 27);
            this.btnToEnrolled.TabIndex = 5;
            this.btnToEnrolled.UseVisualStyleBackColor = true;
            this.btnToEnrolled.Click += new System.EventHandler(this.lstAvailable_DoubleClick);
            // 
            // btnFromEnrolled
            // 
            this.btnFromEnrolled.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFromEnrolled.Image = global::KitchenGeeks.Properties.Resources.LeftArrow;
            this.btnFromEnrolled.Location = new System.Drawing.Point(258, 112);
            this.btnFromEnrolled.Name = "btnFromEnrolled";
            this.btnFromEnrolled.Size = new System.Drawing.Size(35, 27);
            this.btnFromEnrolled.TabIndex = 4;
            this.btnFromEnrolled.UseVisualStyleBackColor = true;
            this.btnFromEnrolled.Click += new System.EventHandler(this.lstEnrolled_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(548, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(629, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Players";
            // 
            // lblEnrolled
            // 
            this.lblEnrolled.AutoSize = true;
            this.lblEnrolled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrolled.Location = new System.Drawing.Point(300, 8);
            this.lblEnrolled.Name = "lblEnrolled";
            this.lblEnrolled.Size = new System.Drawing.Size(123, 16);
            this.lblEnrolled.TabIndex = 2;
            this.lblEnrolled.Text = "Enrolled Players";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(525, 292);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(179, 16);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total Players Enrolled: 0";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(412, 321);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(103, 23);
            this.btnAddNew.TabIndex = 9;
            this.btnAddNew.Text = "Add New Player";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // colEnrolledFaction
            // 
            this.colEnrolledFaction.Text = "Faction";
            this.colEnrolledFaction.Width = 124;
            // 
            // FrmTournamentPlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(720, 356);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblEnrolled);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnToEnrolled);
            this.Controls.Add(this.btnFromEnrolled);
            this.Controls.Add(this.lstEnrolled);
            this.Controls.Add(this.lstAvailable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(575, 378);
            this.Name = "FrmTournamentPlayers";
            this.Text = "Manage Tournament Players";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTournamentPlayers_FormClosing);
            this.SizeChanged += new System.EventHandler(this.frmTournamentPlayers_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstAvailable;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ListView lstEnrolled;
        private System.Windows.Forms.ColumnHeader colEnrolledName;
        private System.Windows.Forms.ColumnHeader colEnrolledLocation;
        private System.Windows.Forms.Button btnFromEnrolled;
        private System.Windows.Forms.Button btnToEnrolled;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ImageList SortImages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEnrolled;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ColumnHeader colEnrolledID;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ColumnHeader colEnrolledFaction;

    }
}