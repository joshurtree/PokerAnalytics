namespace PokerAnalytics.Dialogs
{
    partial class ParserProgressForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.parsingWorker = new System.ComponentModel.BackgroundWorker();
            this.cancel = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(40, 58);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 23);
            this.progressBar.TabIndex = 0;
            // 
            // parsingWorker
            // 
            this.parsingWorker.WorkerReportsProgress = true;
            this.parsingWorker.WorkerSupportsCancellation = true;
            this.parsingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.parsingWorker_DoWork);
            this.parsingWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.parsingWorker_ProgressChanged);
            this.parsingWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.parsingWorker_RunWorkerCompleted);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(289, 102);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(236, 25);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(209, 13);
            this.progressLabel.TabIndex = 2;
            this.progressLabel.Text = "0 hands and 0 tournaments parsed so far...";
            // 
            // ParserProgressForm
            // 
            this.ClientSize = new System.Drawing.Size(659, 164);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParserProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Parsing hand histories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker parsingWorker;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label progressLabel;
    }
}