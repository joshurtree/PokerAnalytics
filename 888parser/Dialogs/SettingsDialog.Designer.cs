namespace PokerAnalytics.Dialogs
{
    partial class SettingsDialog
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
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.clearDatabase = new System.Windows.Forms.Button();
            this.PokerSites = new System.Windows.Forms.ListBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.ModifyButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Location = new System.Drawing.Point(15, 141);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(166, 17);
            this.autoUpdate.TabIndex = 4;
            this.autoUpdate.Text = "Automaticly update on startup";
            this.autoUpdate.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(373, 12);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 5;
            this.ok.Text = "&Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(373, 55);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "&Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // clearDatabase
            // 
            this.clearDatabase.Location = new System.Drawing.Point(15, 177);
            this.clearDatabase.Name = "clearDatabase";
            this.clearDatabase.Size = new System.Drawing.Size(117, 23);
            this.clearDatabase.TabIndex = 7;
            this.clearDatabase.Text = "Clear Database";
            this.clearDatabase.UseVisualStyleBackColor = true;
            this.clearDatabase.Click += new System.EventHandler(this.clearDatabase_Click);
            // 
            // PokerSites
            // 
            this.PokerSites.FormattingEnabled = true;
            this.PokerSites.Location = new System.Drawing.Point(15, 12);
            this.PokerSites.Name = "PokerSites";
            this.PokerSites.Size = new System.Drawing.Size(195, 121);
            this.PokerSites.TabIndex = 8;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(216, 22);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ModifyButton
            // 
            this.ModifyButton.Enabled = false;
            this.ModifyButton.Location = new System.Drawing.Point(216, 64);
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(75, 23);
            this.ModifyButton.TabIndex = 10;
            this.ModifyButton.Text = "&Modify";
            this.ModifyButton.UseVisualStyleBackColor = true;
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Enabled = false;
            this.RemoveButton.Location = new System.Drawing.Point(216, 110);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 11;
            this.RemoveButton.Text = "&Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 238);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.ModifyButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.PokerSites);
            this.Controls.Add(this.clearDatabase);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.autoUpdate);
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.VisibleChanged += new System.EventHandler(this.Settings_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button clearDatabase;
        private System.Windows.Forms.ListBox PokerSites;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button ModifyButton;
        private System.Windows.Forms.Button RemoveButton;
    }
}

