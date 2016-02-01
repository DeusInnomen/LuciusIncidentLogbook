namespace KitchenGeeks
{
    partial class ctlTournamentMatch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVP1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVP2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMatch = new System.Windows.Forms.Label();
            this.lblRecord1 = new System.Windows.Forms.Label();
            this.lblRecord2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer1.Location = new System.Drawing.Point(9, 9);
            this.lblPlayer1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(267, 86);
            this.lblPlayer1.TabIndex = 0;
            this.lblPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlayer1.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer2.Location = new System.Drawing.Point(396, 9);
            this.lblPlayer2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(267, 86);
            this.lblPlayer2.TabIndex = 10;
            this.lblPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPlayer2.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(285, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 63);
            this.label3.TabIndex = 11;
            this.label3.Text = "VS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // txtVP1
            // 
            this.txtVP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVP1.Location = new System.Drawing.Point(213, 123);
            this.txtVP1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVP1.MaxLength = 2;
            this.txtVP1.Name = "txtVP1";
            this.txtVP1.Size = new System.Drawing.Size(46, 35);
            this.txtVP1.TabIndex = 12;
            this.txtVP1.Text = "0";
            this.txtVP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVP1.Enter += new System.EventHandler(this.txtVP_Enter);
            this.txtVP1.Leave += new System.EventHandler(this.txtVP_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(270, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 29);
            this.label5.TabIndex = 15;
            this.label5.Text = ":Earned VP:";
            this.label5.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // txtVP2
            // 
            this.txtVP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVP2.Location = new System.Drawing.Point(408, 123);
            this.txtVP2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVP2.MaxLength = 2;
            this.txtVP2.Name = "txtVP2";
            this.txtVP2.Size = new System.Drawing.Size(46, 35);
            this.txtVP2.TabIndex = 14;
            this.txtVP2.Text = "0";
            this.txtVP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVP2.Enter += new System.EventHandler(this.txtVP_Enter);
            this.txtVP2.Leave += new System.EventHandler(this.txtVP_Leave);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(474, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 51);
            this.label4.TabIndex = 16;
            this.label4.Text = "Double Click to lock or unlock the scores.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label4.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // lblMatch
            // 
            this.lblMatch.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatch.Location = new System.Drawing.Point(3, 114);
            this.lblMatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMatch.Name = "lblMatch";
            this.lblMatch.Size = new System.Drawing.Size(208, 51);
            this.lblMatch.TabIndex = 17;
            this.lblMatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMatch.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // lblRecord1
            // 
            this.lblRecord1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord1.Location = new System.Drawing.Point(4, 83);
            this.lblRecord1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecord1.Name = "lblRecord1";
            this.lblRecord1.Size = new System.Drawing.Size(272, 31);
            this.lblRecord1.TabIndex = 18;
            this.lblRecord1.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // lblRecord2
            // 
            this.lblRecord2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord2.Location = new System.Drawing.Point(396, 83);
            this.lblRecord2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecord2.Name = "lblRecord2";
            this.lblRecord2.Size = new System.Drawing.Size(272, 31);
            this.lblRecord2.TabIndex = 19;
            this.lblRecord2.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(286, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 62);
            this.label1.TabIndex = 20;
            this.label1.Text = "Use \"F\" for Forfeits and \"B\" for Bye.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            // 
            // ctlTournamentMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRecord2);
            this.Controls.Add(this.lblRecord1);
            this.Controls.Add(this.lblMatch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtVP2);
            this.Controls.Add(this.txtVP1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPlayer2);
            this.Controls.Add(this.lblPlayer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctlTournamentMatch";
            this.Size = new System.Drawing.Size(669, 169);
            this.DoubleClick += new System.EventHandler(this.ctlTournamentMatch_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVP1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVP2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMatch;
        private System.Windows.Forms.Label lblRecord1;
        private System.Windows.Forms.Label lblRecord2;
        private System.Windows.Forms.Label label1;
    }
}
