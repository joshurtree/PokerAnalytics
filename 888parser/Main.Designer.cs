namespace PokerAnalytics
{
    partial class Main
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
            this.hands = new System.Windows.Forms.TabControl();
            this.overview = new System.Windows.Forms.TabPage();
            this.overviewListView = new System.Windows.Forms.ListView();
            this.handTab = new System.Windows.Forms.TabPage();
            this.HandView = new System.Windows.Forms.TreeView();
            this.statistics = new System.Windows.Forms.TabPage();
            this.statisticsList = new System.Windows.Forms.ListView();
            this.rangeTab = new System.Windows.Forms.TabPage();
            this.tournamentTab = new System.Windows.Forms.TabPage();
            this.ExpectedValue = new System.Windows.Forms.Label();
            this.EditPrize = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CurrentStackUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.StartingStackUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.RemovePrize = new System.Windows.Forms.Button();
            this.AddPrize = new System.Windows.Forms.Button();
            this.playerNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.prizesList = new System.Windows.Forms.ListBox();
            this.evaluationTab = new System.Windows.Forms.TabPage();
            this.HandEvalLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.rangeControl1 = new PokerAnalytics.RangeControl();
            this.MainRange = new PokerAnalytics.RangeControl();
            this.hands.SuspendLayout();
            this.overview.SuspendLayout();
            this.handTab.SuspendLayout();
            this.statistics.SuspendLayout();
            this.rangeTab.SuspendLayout();
            this.tournamentTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentStackUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingStackUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumUpDown)).BeginInit();
            this.evaluationTab.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hands
            // 
            this.hands.Controls.Add(this.overview);
            this.hands.Controls.Add(this.handTab);
            this.hands.Controls.Add(this.statistics);
            this.hands.Controls.Add(this.rangeTab);
            this.hands.Controls.Add(this.tournamentTab);
            this.hands.Controls.Add(this.evaluationTab);
            this.hands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hands.Location = new System.Drawing.Point(0, 24);
            this.hands.Name = "hands";
            this.hands.SelectedIndex = 0;
            this.hands.Size = new System.Drawing.Size(721, 418);
            this.hands.TabIndex = 0;
            // 
            // overview
            // 
            this.overview.Controls.Add(this.overviewListView);
            this.overview.Location = new System.Drawing.Point(4, 22);
            this.overview.Name = "overview";
            this.overview.Padding = new System.Windows.Forms.Padding(3);
            this.overview.Size = new System.Drawing.Size(713, 392);
            this.overview.TabIndex = 3;
            this.overview.Text = "Overview";
            this.overview.UseVisualStyleBackColor = true;
            // 
            // overviewListView
            // 
            this.overviewListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.overviewListView.Location = new System.Drawing.Point(3, 3);
            this.overviewListView.MultiSelect = false;
            this.overviewListView.Name = "overviewListView";
            this.overviewListView.Size = new System.Drawing.Size(707, 386);
            this.overviewListView.TabIndex = 0;
            this.overviewListView.UseCompatibleStateImageBehavior = false;
            this.overviewListView.View = System.Windows.Forms.View.Details;
            // 
            // handTab
            // 
            this.handTab.Controls.Add(this.HandView);
            this.handTab.Location = new System.Drawing.Point(4, 22);
            this.handTab.Name = "handTab";
            this.handTab.Padding = new System.Windows.Forms.Padding(3);
            this.handTab.Size = new System.Drawing.Size(713, 392);
            this.handTab.TabIndex = 0;
            this.handTab.Text = "Hands";
            this.handTab.UseVisualStyleBackColor = true;
            // 
            // HandView
            // 
            this.HandView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HandView.Location = new System.Drawing.Point(3, 0);
            this.HandView.Name = "HandView";
            this.HandView.Size = new System.Drawing.Size(705, 365);
            this.HandView.TabIndex = 0;
            // 
            // statistics
            // 
            this.statistics.Controls.Add(this.statisticsList);
            this.statistics.Location = new System.Drawing.Point(4, 22);
            this.statistics.Name = "statistics";
            this.statistics.Padding = new System.Windows.Forms.Padding(3);
            this.statistics.Size = new System.Drawing.Size(713, 392);
            this.statistics.TabIndex = 2;
            this.statistics.Text = "Statistics";
            this.statistics.UseVisualStyleBackColor = true;
            // 
            // statisticsList
            // 
            this.statisticsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.statisticsList.Location = new System.Drawing.Point(3, 3);
            this.statisticsList.MultiSelect = false;
            this.statisticsList.Name = "statisticsList";
            this.statisticsList.ShowItemToolTips = true;
            this.statisticsList.Size = new System.Drawing.Size(707, 386);
            this.statisticsList.TabIndex = 0;
            this.statisticsList.UseCompatibleStateImageBehavior = false;
            this.statisticsList.View = System.Windows.Forms.View.Details;
            // 
            // rangeTab
            // 
            this.rangeTab.Controls.Add(this.MainRange);
            this.rangeTab.Location = new System.Drawing.Point(4, 22);
            this.rangeTab.Name = "rangeTab";
            this.rangeTab.Padding = new System.Windows.Forms.Padding(3);
            this.rangeTab.Size = new System.Drawing.Size(713, 392);
            this.rangeTab.TabIndex = 4;
            this.rangeTab.Text = "Ranges";
            this.rangeTab.UseVisualStyleBackColor = true;
            // 
            // tournamentTab
            // 
            this.tournamentTab.Controls.Add(this.ExpectedValue);
            this.tournamentTab.Controls.Add(this.EditPrize);
            this.tournamentTab.Controls.Add(this.LoadButton);
            this.tournamentTab.Controls.Add(this.button3);
            this.tournamentTab.Controls.Add(this.button2);
            this.tournamentTab.Controls.Add(this.CurrentStackUpDown);
            this.tournamentTab.Controls.Add(this.label4);
            this.tournamentTab.Controls.Add(this.StartingStackUpDown);
            this.tournamentTab.Controls.Add(this.label3);
            this.tournamentTab.Controls.Add(this.RemovePrize);
            this.tournamentTab.Controls.Add(this.AddPrize);
            this.tournamentTab.Controls.Add(this.playerNumUpDown);
            this.tournamentTab.Controls.Add(this.label2);
            this.tournamentTab.Controls.Add(this.prizesList);
            this.tournamentTab.Location = new System.Drawing.Point(4, 22);
            this.tournamentTab.Name = "tournamentTab";
            this.tournamentTab.Padding = new System.Windows.Forms.Padding(3);
            this.tournamentTab.Size = new System.Drawing.Size(713, 392);
            this.tournamentTab.TabIndex = 5;
            this.tournamentTab.Text = "Tournaments";
            this.tournamentTab.UseVisualStyleBackColor = true;
            // 
            // ExpectedValue
            // 
            this.ExpectedValue.AutoSize = true;
            this.ExpectedValue.Location = new System.Drawing.Point(55, 296);
            this.ExpectedValue.Name = "ExpectedValue";
            this.ExpectedValue.Size = new System.Drawing.Size(150, 13);
            this.ExpectedValue.TabIndex = 13;
            this.ExpectedValue.Text = "Expected tournament Value: 0";
            // 
            // EditPrize
            // 
            this.EditPrize.Location = new System.Drawing.Point(168, 107);
            this.EditPrize.Name = "EditPrize";
            this.EditPrize.Size = new System.Drawing.Size(75, 45);
            this.EditPrize.TabIndex = 12;
            this.EditPrize.Text = "Edit Prize";
            this.EditPrize.UseVisualStyleBackColor = true;
            this.EditPrize.Click += new System.EventHandler(this.EditPrize_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(630, 51);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 11;
            this.LoadButton.Text = "&Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(630, 80);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "&Clear";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(630, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "&Save";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CurrentStackUpDown
            // 
            this.CurrentStackUpDown.Location = new System.Drawing.Point(320, 159);
            this.CurrentStackUpDown.Name = "CurrentStackUpDown";
            this.CurrentStackUpDown.Size = new System.Drawing.Size(120, 20);
            this.CurrentStackUpDown.TabIndex = 8;
            this.CurrentStackUpDown.ValueChanged += new System.EventHandler(this.CurrentStackUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current stack";
            // 
            // StartingStackUpDown
            // 
            this.StartingStackUpDown.Location = new System.Drawing.Point(464, 71);
            this.StartingStackUpDown.Name = "StartingStackUpDown";
            this.StartingStackUpDown.Size = new System.Drawing.Size(120, 20);
            this.StartingStackUpDown.TabIndex = 6;
            this.StartingStackUpDown.ValueChanged += new System.EventHandler(this.StartingStackUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(461, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Starting stack";
            // 
            // RemovePrize
            // 
            this.RemovePrize.Location = new System.Drawing.Point(168, 176);
            this.RemovePrize.Name = "RemovePrize";
            this.RemovePrize.Size = new System.Drawing.Size(75, 38);
            this.RemovePrize.TabIndex = 4;
            this.RemovePrize.Text = "&Revove prize";
            this.RemovePrize.UseVisualStyleBackColor = true;
            this.RemovePrize.Click += new System.EventHandler(this.RemovePrize_Click);
            // 
            // AddPrize
            // 
            this.AddPrize.Location = new System.Drawing.Point(168, 44);
            this.AddPrize.Name = "AddPrize";
            this.AddPrize.Size = new System.Drawing.Size(75, 37);
            this.AddPrize.TabIndex = 3;
            this.AddPrize.Text = "&Add prize";
            this.AddPrize.UseVisualStyleBackColor = true;
            this.AddPrize.Click += new System.EventHandler(this.AddPrize_Click);
            // 
            // playerNumUpDown
            // 
            this.playerNumUpDown.Location = new System.Drawing.Point(320, 71);
            this.playerNumUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.playerNumUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerNumUpDown.Name = "playerNumUpDown";
            this.playerNumUpDown.Size = new System.Drawing.Size(120, 20);
            this.playerNumUpDown.TabIndex = 2;
            this.playerNumUpDown.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.playerNumUpDown.ValueChanged += new System.EventHandler(this.playerNumUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of players";
            // 
            // prizesList
            // 
            this.prizesList.FormattingEnabled = true;
            this.prizesList.Location = new System.Drawing.Point(8, 32);
            this.prizesList.Name = "prizesList";
            this.prizesList.Size = new System.Drawing.Size(120, 225);
            this.prizesList.TabIndex = 0;
            // 
            // evaluationTab
            // 
            this.evaluationTab.Controls.Add(this.HandEvalLayout);
            this.evaluationTab.Location = new System.Drawing.Point(4, 22);
            this.evaluationTab.Name = "evaluationTab";
            this.evaluationTab.Size = new System.Drawing.Size(713, 392);
            this.evaluationTab.TabIndex = 6;
            this.evaluationTab.Text = "Hand Evaluation";
            this.evaluationTab.UseVisualStyleBackColor = true;
            // 
            // HandEvalLayout
            // 
            this.HandEvalLayout.ColumnCount = 5;
            this.HandEvalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.014F));
            this.HandEvalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.98599F));
            this.HandEvalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HandEvalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HandEvalLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.HandEvalLayout.Location = new System.Drawing.Point(0, 0);
            this.HandEvalLayout.Name = "HandEvalLayout";
            this.HandEvalLayout.RowCount = 10;
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HandEvalLayout.Size = new System.Drawing.Size(418, 271);
            this.HandEvalLayout.TabIndex = 0;
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateMenuItem,
            this.settingsMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // updateMenuItem
            // 
            this.updateMenuItem.Name = "updateMenuItem";
            this.updateMenuItem.Size = new System.Drawing.Size(134, 22);
            this.updateMenuItem.Text = "&Update";
            this.updateMenuItem.Click += new System.EventHandler(this.updateMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(134, 22);
            this.settingsMenuItem.Text = "&Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // viewsToolStripMenuItem
            // 
            this.viewsToolStripMenuItem.Name = "viewsToolStripMenuItem";
            this.viewsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.viewsToolStripMenuItem.Text = "Views";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.viewsToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(721, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // rangeControl1
            // 
            this.rangeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rangeControl1.Location = new System.Drawing.Point(3, 3);
            this.rangeControl1.Name = "rangeControl1";
            this.rangeControl1.Size = new System.Drawing.Size(707, 386);
            this.rangeControl1.TabIndex = 0;
            this.rangeControl1.ViewName = null;
            // 
            // MainRange
            // 
            this.MainRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainRange.Location = new System.Drawing.Point(3, 3);
            this.MainRange.Name = "MainRange";
            this.MainRange.Size = new System.Drawing.Size(707, 386);
            this.MainRange.TabIndex = 0;
            this.MainRange.ViewName = null;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 442);
            this.Controls.Add(this.hands);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Main";
            this.Text = "Analyse Poker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.hands.ResumeLayout(false);
            this.overview.ResumeLayout(false);
            this.handTab.ResumeLayout(false);
            this.statistics.ResumeLayout(false);
            this.rangeTab.ResumeLayout(false);
            this.tournamentTab.ResumeLayout(false);
            this.tournamentTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentStackUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingStackUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumUpDown)).EndInit();
            this.evaluationTab.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl hands;
        private System.Windows.Forms.TabPage handTab;
        private System.Windows.Forms.TabPage statistics;
        private System.Windows.Forms.ListView statisticsList;
        private System.Windows.Forms.TabPage overview;
        private System.Windows.Forms.ListView overviewListView;
        private System.Windows.Forms.TabPage rangeTab;
        private System.Windows.Forms.TabPage tournamentTab;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown CurrentStackUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown StartingStackUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RemovePrize;
        private System.Windows.Forms.Button AddPrize;
        private System.Windows.Forms.NumericUpDown playerNumUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox prizesList;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button EditPrize;
        private System.Windows.Forms.Label ExpectedValue;
        public System.Windows.Forms.TreeView HandView;
        private System.Windows.Forms.TabPage evaluationTab;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.TableLayoutPanel HandEvalLayout;
        private RangeControl rangeControl1;
        private RangeControl MainRange;
    }
}