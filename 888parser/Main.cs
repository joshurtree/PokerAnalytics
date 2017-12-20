using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Players;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Cards;
using HandHistories.Objects.Actions;
using System.Threading;
using PokerAnalytics.Dialogs;
using PokerAnalytics.Properties;
using System.Collections.Specialized;

namespace PokerAnalytics
{
    public partial class Main : Form
    {
        SettingsDialog SettingsDialog;
        DatabaseHandler DatabaseHandler;
        Queries Queries;

        BackgroundWorker[] ViewThreads = new BackgroundWorker[2];

        string[] ViewNames = new string[2];

        TournamentAnalytics TournamentAnalytics = new TournamentAnalytics();
        
        public Main(string[] args)
        {
            InitializeComponent();

            DatabaseHandler = DatabaseHandler.getInstance();
            SettingsDialog = new SettingsDialog();
            if (args.Contains("-clear"))
                DatabaseHandler.ClearDatabase();
        }

        private bool HasSiteFilesSetup()
        {
            return Settings.Default.Sites != null && Settings.Default.Sites.Count != 0;
        }

        #region event handlers
        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            if (!HasSiteFilesSetup())
            {
                MessageBox.Show(this, "No hand history collections setup. Go to settings to provide details of the hand histories.", "Folder path needed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ParserProgressForm parserForm = new ParserProgressForm();
            parserForm.ShowDialog();
            SetupInterface();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            SettingsDialog.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;
            if (settings.FirstRun)
            {
                if (SettingsDialog.ShowDialog() == DialogResult.OK)
                    updateMenuItem_Click(null, null);
                settings.FirstRun = false;
                settings.Save();
            }

            if (settings.autoUpdate && HasSiteFilesSetup())
                new ParserProgressForm().ShowDialog();
            Queries = new Queries(DatabaseHandler.GetAllHands(), DatabaseHandler.GetAllTournaments());
            SetupInterface();
        }
        #endregion

        private void SetupInterface()
        {
            Cursor.Current = Cursors.WaitCursor;
            SetupOverview();
            SetupHandTreeView();
            SetupStatistics();
            //SetupRanges();

            ViewMenuItem.AllViewMenuItems[DatabaseHandler.Views.allHands].ViewMenuItems[0].PerformClick();
            ViewMenuItem.AllViewMenuItems[DatabaseHandler.Views.cashHands].ViewMenuItems[1].PerformClick();

            Cursor.Current = Cursors.Default;
        }

        private void SetupOverview()
        {
            overviewListView.ItemSelectionChanged += (s, e) => { if (e.IsSelected) e.Item.Selected = false; };
            //statisticsList.HeaderStyle = ColumnHeaderStyle.None;
            overviewListView.Columns.AddRange(new ColumnHeader[]
                { new ColumnHeader("Name"), new ColumnHeader("Value") });

            Dictionary<string, ListViewGroup> listGroups = new Dictionary<string, ListViewGroup>();

            listGroups.Add("general", new ListViewGroup("General"));
            listGroups.Add("cashGames", new ListViewGroup("Cash games"));
            listGroups.Add("tournaments", new ListViewGroup("Tournaments"));
            listGroups.Add("sitAndGos", new ListViewGroup("Sit & Go"));

            overviewListView.Groups.AddRange(listGroups.Values.ToArray());
            foreach (Statistic stat in Queries.statistics.FindAll(s => s.Page.Equals("overview")))
                overviewListView.Items.Add(new StatisticListViewItem(stat, listGroups[stat.Group]));

            overviewListView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            new Thread(Queries.LoadOverview).Start();
        }

        private void SetupStatistics()
        {
            statisticsList.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader() { Text = "" },
                new ColumnHeader(),
                new ColumnHeader()
            });
            statisticsList.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            Dictionary<string, ListViewGroup> ListGroups = new Dictionary<string, ListViewGroup>();

            ListGroups.Add("preflop", new ListViewGroup("Pre-Flop"));
            ListGroups.Add("flop", new ListViewGroup("Flop"));
            ListGroups.Add("turn", new ListViewGroup("Turn"));
            ListGroups.Add("river", new ListViewGroup("River"));
            ListGroups.Add("showdown", new ListViewGroup("Showdown"));
            statisticsList.Groups.AddRange(ListGroups.Values.ToArray());

            foreach (Statistic stat in Queries.statistics.FindAll(s => s.Page.Equals("statistics")))
                statisticsList.Items.Add(new StatisticListViewItem(stat, ListGroups[stat.Group]));

            statisticsList.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            statisticsList.ItemSelectionChanged += (s, e) => { if (e.IsSelected) e.Item.Selected = false; };

            viewsToolStripMenuItem.DropDown.Items.Add(new ViewMenuItem(this, "hands", "All Hands"));
            viewsToolStripMenuItem.DropDown.Items.Add(new ToolStripSeparator());
            viewsToolStripMenuItem.DropDown.Items.Add(new ViewMenuItem(this, "cashHands", "Cash Hands"));
            viewsToolStripMenuItem.DropDown.Items.Add(new ViewMenuItem(this, "sitAndGoHands", "Sit and Go Hands"));
            viewsToolStripMenuItem.DropDown.Items.Add(new ViewMenuItem(this, "tournamentHands", "Tournament Hands"));
        }

        private List<Label> rangeLabels = new List<Label>(169);
        private Dictionary<string, ToolTip> rangeTooltips = new Dictionary<string, ToolTip>();

        public void LoadView(string title, string viewName, int index)
        {
            ViewNames[index] = viewName;
            statisticsList.Columns[index + 1].Text = title;

            foreach (ListViewItem item in statisticsList.Items)
                item.SubItems[index + 1].Text = "Pending...";
            statisticsList.AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);

            AbortThread(ref ViewThreads[index]);

            ViewThreads[index] = new BackgroundWorker();
            ViewThreads[index].WorkerSupportsCancellation = true;
            ViewThreads[index].DoWork += (s, e) => Queries.LoadStatisitcs(DatabaseHandler.getInstance().GetView(viewName), index + 1);
            ViewThreads[index].RunWorkerAsync();

            if (index == 0)
                MainRange.LoadRange(viewName, false);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            AbortThread(ref ViewThreads[0]);
            AbortThread(ref ViewThreads[1]);

            //if (RangeThread != null && RangeThread.IsBusy)
            //    RangeThread.CancelAsync();
        }

        private void AbortThread(ref BackgroundWorker thread)
        {
            if (thread != null && thread.IsBusy)
                thread.CancelAsync();
        }
    }

    class ViewMenuItem : ToolStripMenuItem
    {
        public string ViewName { get; }
        public Main Main { get; }

        public ToolStripMenuItem[] ViewMenuItems = new ToolStripMenuItem[2];
        public static Dictionary<string, ViewMenuItem> AllViewMenuItems = new Dictionary<string, ViewMenuItem>();
        public static string[] SelectedViews = new string[2];
        public static string[] ViewNames = new[] { "primary", "secondary" };

        public ViewMenuItem(Main main, string name, string title) : base(title)
        {
            ViewName = name;
            Main = main;

            AllViewMenuItems.Add(name, this);

            for (int i = 0; i <= 1; ++i)
            {
                ViewMenuItems[i] = new ToolStripMenuItem(string.Format("Set as &{0} view", ViewNames[i]), null, selectView);
                ViewMenuItems[i].Tag = i;
                ViewMenuItems[i].CheckOnClick = true;
                DropDown.Items.Add(ViewMenuItems[i]);
            }
        }

        protected virtual void selectView(object sender, EventArgs ev)
        {
            int view = (int)((ToolStripMenuItem)sender).Tag;
            if (SelectedViews[view] == ViewName)
            {
                MessageBox.Show(Main, "This is view already selected", "View selected",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                ViewMenuItems[view].CheckState = CheckState.Checked;
            }
            else
            {
                if (SelectedViews[view] != null)
                    AllViewMenuItems[SelectedViews[view]].CheckState = CheckState.Unchecked;
                SelectedViews[view] = ViewName;
                ((Main)Main).LoadView(Text, ViewName, view);
            }

        }
    }

    public class StatisticListViewItem : ListViewItem
    {
        public Statistic stat { get; }
        public StatisticListViewItem(Statistic s, ListViewGroup group) : base(new[] { s.Title, "", "" })
        {
            stat = s;
            Group = group;
            ToolTipText = stat.Description;

            stat.Listener = (val, index) =>
            {
                if (ListView != null)
                    ListView.Invoke(new Action(() => SubItems[index] = new ListViewSubItem(this, val)));
                return true;
            };

        }
    }
}
