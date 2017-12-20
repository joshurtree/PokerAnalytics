namespace PokerAnalytics.Dialogs
{
    partial class PrizeDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.placesUpDown = new System.Windows.Forms.NumericUpDown();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.prizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.placesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.placesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of places";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prize";
            // 
            // placesUpDown
            // 
            this.placesUpDown.Location = new System.Drawing.Point(19, 126);
            this.placesUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.placesUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.placesUpDown.Name = "placesUpDown";
            this.placesUpDown.Size = new System.Drawing.Size(113, 20);
            this.placesUpDown.TabIndex = 3;
            this.placesUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(188, 16);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 4;
            this.ok.Text = "&Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(188, 45);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 5;
            this.cancel.Text = "&Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // prizeUpDown
            // 
            this.prizeUpDown.DecimalPlaces = 2;
            this.prizeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.prizeUpDown.Location = new System.Drawing.Point(19, 45);
            this.prizeUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.prizeUpDown.Name = "prizeUpDown";
            this.prizeUpDown.Size = new System.Drawing.Size(113, 20);
            this.prizeUpDown.TabIndex = 6;
            // 
            // placesLabel
            // 
            this.placesLabel.AutoSize = true;
            this.placesLabel.Location = new System.Drawing.Point(185, 128);
            this.placesLabel.Name = "placesLabel";
            this.placesLabel.Size = new System.Drawing.Size(0, 13);
            this.placesLabel.TabIndex = 7;
            // 
            // PrizeDialog
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.placesLabel);
            this.Controls.Add(this.prizeUpDown);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.placesUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrizeDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New prize";
            this.Load += new System.EventHandler(this.PrizeDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.placesUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prizeUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown placesUpDown;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.NumericUpDown prizeUpDown;
        private System.Windows.Forms.Label placesLabel;
    }
}