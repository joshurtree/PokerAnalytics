namespace PokerAnalytics.Dialogs
{
    partial class SiteSetup
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
            this.HandHistoryLocation = new System.Windows.Forms.TextBox();
            this.handBrowseButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TournamentSummaryLocation = new System.Windows.Forms.TextBox();
            this.tournamentBrowseButton = new System.Windows.Forms.Button();
            this.PokerSites = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // HandHistoryLocation
            // 
            this.HandHistoryLocation.Location = new System.Drawing.Point(39, 234);
            this.HandHistoryLocation.Name = "HandHistoryLocation";
            this.HandHistoryLocation.Size = new System.Drawing.Size(224, 20);
            this.HandHistoryLocation.TabIndex = 1;
            // 
            // handBrowseButton
            // 
            this.handBrowseButton.Location = new System.Drawing.Point(303, 234);
            this.handBrowseButton.Name = "handBrowseButton";
            this.handBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.handBrowseButton.TabIndex = 2;
            this.handBrowseButton.Text = "Browse...";
            this.handBrowseButton.UseVisualStyleBackColor = true;
            this.handBrowseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(303, 12);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "&Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(303, 54);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "&Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hand files location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Site";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tournament summaries location";
            // 
            // TournamentSummaryLocation
            // 
            this.TournamentSummaryLocation.Location = new System.Drawing.Point(39, 292);
            this.TournamentSummaryLocation.Name = "TournamentSummaryLocation";
            this.TournamentSummaryLocation.Size = new System.Drawing.Size(224, 20);
            this.TournamentSummaryLocation.TabIndex = 9;
            // 
            // tournamentBrowseButton
            // 
            this.tournamentBrowseButton.Location = new System.Drawing.Point(303, 292);
            this.tournamentBrowseButton.Name = "tournamentBrowseButton";
            this.tournamentBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.tournamentBrowseButton.TabIndex = 10;
            this.tournamentBrowseButton.Text = "Browse...";
            this.tournamentBrowseButton.UseVisualStyleBackColor = true;
            this.tournamentBrowseButton.Click += new System.EventHandler(this.tournamentBrowseButton_Click);
            // 
            // PokerSites
            // 
            this.PokerSites.FormattingEnabled = true;
            this.PokerSites.Location = new System.Drawing.Point(39, 28);
            this.PokerSites.Name = "PokerSites";
            this.PokerSites.Size = new System.Drawing.Size(224, 160);
            this.PokerSites.TabIndex = 11;
            this.PokerSites.SelectedIndexChanged += new System.EventHandler(this.PokerSites_SelectedIndexChanged);
            // 
            // SiteSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 354);
            this.Controls.Add(this.PokerSites);
            this.Controls.Add(this.tournamentBrowseButton);
            this.Controls.Add(this.TournamentSummaryLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.handBrowseButton);
            this.Controls.Add(this.HandHistoryLocation);
            this.Name = "SiteSetup";
            this.Text = "SiteSetup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button handBrowseButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox HandHistoryLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button tournamentBrowseButton;
        public System.Windows.Forms.TextBox TournamentSummaryLocation;
        public System.Windows.Forms.ListBox PokerSites;
    }
}