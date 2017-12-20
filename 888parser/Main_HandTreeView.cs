using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using HandHistories.Objects.Actions;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Players;

namespace PokerAnalytics
{
    public partial class Main
    {
        public void SetupHandTreeView()
        {
            DatabaseHandler databaseHandler = DatabaseHandler.getInstance();
            IMongoCollection <HandHistory> allHands = databaseHandler.GetAllHands();
            IAggregateFluent<HandHistory> cashHands = 
                allHands.Aggregate().Match(Builders<HandHistory>.Filter.Where(h => h.GameDescription.PokerFormat == PokerFormat.CashGame));
            GroupHandTreeNode cashGamesNode =
                new GroupHandTreeNode("Cash Games", cashHands);
            IEnumerable<AggregateResult<Limit>> limits =
                cashHands.Group(h => h.GameDescription.Limit, h => new AggregateResult<Limit> { result = h.Key })
                         .Sort(Builders<AggregateResult<Limit>>.Sort.Ascending(l => l.result.BigBlind).Ascending(l =>l.result.SmallBlind))
                         .ToEnumerable();

            foreach (AggregateResult<Limit> limit in limits)
            {
                IAggregateFluent<HandHistory> hands =
                    cashHands.Match(Builders<HandHistory>.Filter.Where(h2 => h2.GameDescription.Limit.Equals(limit.result)));
                cashGamesNode.Nodes.Add(
                    new HandsTreeNode(string.Format("{0} {1}", limit.result.ToString(), HandCount(hands)), hands));
            }

            HandView.Nodes.Add(cashGamesNode);
             

            TreeNode sitAndGoNode = new TreeNode("Sit and Go");
            setupTournamentHands(sitAndGoNode, PokerFormat.SitAndGo);
            HandView.Nodes.Add(sitAndGoNode);

            TreeNode tournamentNode = new TreeNode("Tournaments");
            setupTournamentHands(tournamentNode, PokerFormat.MultiTableTournament);
            HandView.Nodes.Add(tournamentNode);

            HandView.BeforeExpand += BeforeExpand;
            HandView.AfterCollapse += AfterCollapse;
            HandView.ContextMenuStrip = new ContextMenuStrip();
            HandView.ContextMenuStrip.Items.Add(new QuickViewMenuItem(this));
            HandView.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Setup custom view...", null, (s, e) => SetupCustomView()));
        }

        private void SetupCustomView()
        {

        }

        private void setupTournamentHands(TreeNode parentNode, PokerFormat format)
        {
            DatabaseHandler databaseHandler = DatabaseHandler.getInstance();
            IAggregateFluent<TournamentSummary> tournaments = databaseHandler.GetAllTournaments()
                .Aggregate().Match(Builders<TournamentSummary>.Filter.Where(h => h.SitAndGo == (format == PokerFormat.SitAndGo)));
            IAggregateFluent<HandHistory> allHands = databaseHandler.GetAllHands().Aggregate()
                                        .Match(Builders<HandHistory>.Filter.Where(h => h.GameDescription.PokerFormat == format));
            IEnumerable<AggregateResult<Buyin>> allBuyins = 
                tournaments.Group(h => h.Buyin, h => new AggregateResult<Buyin>{ result = h.Key })
                           .Sort(Builders<AggregateResult<Buyin>>.Sort.Ascending(b => b.result))
                           .ToEnumerable();

            decimal lastBuyin = -0.01m; 
            foreach (dynamic buyinObj in allBuyins)
            {                
                Buyin buyin = buyinObj.result;
                
                if (buyin.Total == lastBuyin)
                    continue;
                lastBuyin = buyin.Total;
                IAggregateFluent<HandHistory> hands = 
                    allHands.Match(Builders<HandHistory>.Filter.Where(h => h.GameDescription.TournamentSummary.Buyin.Total == buyin.Total));
                GroupHandTreeNode buyinNode = 
                    new GroupHandTreeNode(string.Format("{0}{1} {2}", buyin.GetCurrencySymbol(),
                                                                              buyin.Total,
                                                                              HandCount(hands)),
                                                  hands);
                dynamic allIds = hands.Group(h => h.GameDescription.TournamentId, h => new { name = h.Key }).ToEnumerable();

                foreach (dynamic idObj in allIds)
                {
                    string id = idObj.name;

                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        IEnumerable<TournamentSummary> summaries = tournaments.Match(t => t.Id == id).ToEnumerable();

                        if (summaries.Count() > 0)
                        {
                            TournamentSummary summary = summaries.First();

                            IAggregateFluent <HandHistory> tournyHands =
                                allHands.Match(Builders<HandHistory>.Filter.Where(h => h.GameDescription.TournamentId == summary.Id));
                            buyinNode.Nodes.Add(new HandsTreeNode(summary.ToString(), tournyHands));

                        }
                    }
                }

                if (buyinNode.Nodes.Count > 0)
                    parentNode.Nodes.Add(buyinNode);
            }
        }

        private void BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.GetType() == typeof(HandsTreeNode))
                ((HandsTreeNode)e.Node).Populate();
        }

        private void AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.GetType() == typeof(HandsTreeNode))
                ((HandsTreeNode)e.Node).Depopulate();
        }

        private string HandCount(IAggregateFluent<HandHistory> hands)
        {
            /*
            IEnumerable<AggregateCountResult> countResult = hands.Count().ToEnumerable();
            long count = countResult.Count() > 0 ? countResult.First().Count : 0;
            return string.Format("({0} hands)", count);
            */
            return "";
        }

        }

    abstract class FilteredTreeNode : TreeNode
    {
        public List<BsonDocument> Filter { get; }

        public FilteredTreeNode(string title, IAggregateFluent<HandHistory> pipeline) : base(title)
        {
            Filter = new List<BsonDocument>();
            foreach (IPipelineStageDefinition stage in pipeline.Stages)
                Filter.Add(stage.ToBsonDocument());
        }
    }

    class GroupHandTreeNode : FilteredTreeNode
    {
        public GroupHandTreeNode(String name, IAggregateFluent<HandHistory> pipeline) : base(name, pipeline)
        {
        }
    }

    class HandsTreeNode : FilteredTreeNode
    {
        IAggregateFluent<HandHistory> Hands;
        public HandsTreeNode(string format, IAggregateFluent<HandHistory> hands) : base(format, hands)
        {
            Hands = hands;
            Nodes.Add("dummy");
        }

        public void Populate()
        {
            Nodes.Clear();
            foreach (HandHistory hand in Hands.ToEnumerable())
                Nodes.Add(new HandTreeNode(hand));
        }

        public void Depopulate()
        {
            Nodes.Clear();
            Nodes.Add("dummy");
        }

        class HandTreeNode : TreeNode
        {
            public HandHistory hand { get; }

            public HandTreeNode(HandHistory h) : base(h.ToString())
            {
                hand = h;

                TreeNode gameNode = Nodes.Add("Game");
                gameNode.Nodes.Add("Game type: " + GameTypeUtils.GetDisplayName(hand.GameDescription.GameType));
                gameNode.Nodes.Add("Game format: " + PokerFormatUtils.GetDisplayName(hand.GameDescription.PokerFormat));

                TreeNode players = Nodes.Add("Players");
                foreach (Player player in hand.Players)
                {

                    TreeNode playerNode = players.Nodes.Add(player.PlayerName);
                    playerNode.Nodes.Add("Seat: " + player.SeatNumber);

                    if (!player.IsSittingOut)
                    {
                        playerNode.Nodes.Add("Starting stack: $" + player.StartingStack);
                        if (player.hasHoleCards)
                            playerNode.Nodes.Add("Hole Cards: " + player.HoleCards);
                    }
                    else
                    {
                        playerNode.Nodes.Add("Sitting out");
                    }
                }

                TreeNode actions = Nodes.Add("Actions");


                foreach (HandAction action in hand.HandActions)
                {
                    actions.Nodes.Add(action.ToString());
                }

                Nodes.Add("Date (Utc): " + hand.DateOfHandUtc.ToString());
                Nodes.Add("Date: " + hand.DateOfHandUtc.ToLocalTime().ToString());
                Nodes.Add("Community cards: " +
                            (hand.ComumnityCards.Count == 0 ? "None" : string.Join(" ", hand.ComumnityCards.Select(c => c.ToString()))));
                Nodes.Add("Active players: " + hand.NumPlayersActive);
                Nodes.Add("Total pot: " + hand.TotalPot + " BB");
            }
        };
    }

    class QuickViewMenuItem : ViewMenuItem
    {
        public QuickViewMenuItem(Main main) : base(main, DatabaseHandler.Views.QuickView, "&Quick View")
        {
        }

        protected override void selectView(object sender, EventArgs ev)
        {
            FilteredTreeNode filteredTreeNode = (FilteredTreeNode) Main.HandView.SelectedNode;
            DatabaseHandler.getInstance().AddView(DatabaseHandler.Views.QuickView, filteredTreeNode.Filter);
            base.selectView(sender, ev);
        }
    }
}
