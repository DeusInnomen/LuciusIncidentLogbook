namespace KitchenGeeks
{
    partial class frmViewMatches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewMatches));
            this.treeMatches = new System.Windows.Forms.TreeView();
            this.btnSwap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeMatches
            // 
            this.treeMatches.CheckBoxes = true;
            this.treeMatches.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeMatches.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeMatches.HideSelection = false;
            this.treeMatches.Location = new System.Drawing.Point(0, 0);
            this.treeMatches.Name = "treeMatches";
            this.treeMatches.ShowPlusMinus = false;
            this.treeMatches.ShowRootLines = false;
            this.treeMatches.Size = new System.Drawing.Size(297, 296);
            this.treeMatches.TabIndex = 0;
            this.treeMatches.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeMatches_BeforeCheck);
            this.treeMatches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeMatches_AfterCheck);
            this.treeMatches.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeMatches_BeforeCollapse);
            // 
            // btnSwap
            // 
            this.btnSwap.Enabled = false;
            this.btnSwap.Location = new System.Drawing.Point(312, 12);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(148, 23);
            this.btnSwap.TabIndex = 1;
            this.btnSwap.Text = "Swap Selected Players";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // frmViewMatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 296);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.treeMatches);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(489, 755);
            this.MinimumSize = new System.Drawing.Size(489, 334);
            this.Name = "frmViewMatches";
            this.Text = "View Round Matches";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeMatches;
        private System.Windows.Forms.Button btnSwap;
    }
}