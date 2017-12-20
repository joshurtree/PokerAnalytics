using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Actions;
using HandHistories.Objects.Players;
using System.Threading;

namespace PokerAnalytics
{
       
    public class AggregateResult<Type>
    {
        public string _id { get; set; }
        public Type result { get; set; }
    }

    [BsonIgnoreExtraElements]
    class UnwoundHand
    {
        [BsonElement("_id")]
        public long HandId { get; set; }
        public HandAction HandActions { get; set; }
        public Player Hero { get; set; }
        public DateTime DateOfHandUtc { get; set; }
        public Street Street { get; set; }
        public GameDescriptor GameDescription { get; set; }
        public PlayerList Players { get; set; }
    }

    public class StatsByStreet<Type>
    {
        public Street _id { get; set; }
        public Type result { get; set; }
    }

    public class StatsByStreetDecimal
    {
        public Street _id { get; set; }
        [BsonRepresentation(BsonType.Decimal128, AllowTruncation = true)]
        public decimal result { get; set; }
    }

    public class StatsByPosition<Type>
    {
        public int position { get; set; }
        public Type result { get; set; }
    }


    public class Statistic
    {
        [BsonId]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Page { get; set; }
        public string Group { get; set; }
        public string DefaultValue { get; set; }
        [BsonIgnore]
        public Func<string, int, bool> Listener = null;
    }

    public class Queries
    {
        private static string dollarPattern = "$#####0.00";
        private static string precentPattern = "#0.0%";

        delegate T ZeroIfNull<T>(StatsByStreet<T> value);
        private IMongoCollection<HandHistory> handCollection;
        private IMongoCollection<TournamentSummary> tournamentCollection;

        public IMongoCollection<HandHistory> primaryHandCollection { get; set; }
        public IMongoCollection<HandHistory> secondaryHandCollection { get; set; }

        private List<StatsByStreet<long>> potWonAtStreet;
        private List<StatsByStreetDecimal> amountCommitedAtStreet;
        private List<StatsByStreet<long>> handsByStreetReached;
        private List<StatsByStreet<long>> handsWonByPlayer;

        private List<string> PlayerNames;
        public List<Statistic> statistics { get; } = new List<Statistic>();

        public Queries(IMongoCollection<HandHistory> hands, IMongoCollection<TournamentSummary> tournaments)
        {
            handCollection = hands;
            tournamentCollection = tournaments;
            
            PlayerNames = new List<string>();
            // Add player names used in every hand
            hands.Aggregate().Group(h => h.Hero.PlayerName, h => new AggregateResult<string> { result = h.Key })
                                      .ToList()
                                      .ForEach(x => { if (x.result != null) PlayerNames.Add(x.result); });

            statistics = JsonConvert.DeserializeObject<List<Statistic>>(File.ReadAllText("Queries.json"));
        }

        public void LoadOverview()
        {
            OverviewDetails cashGameDetails = LoadCashOverview();
            AddStatistic("cashHands", cashGameDetails.Hands.ToString());
            AddStatistic("cashWinnings", cashGameDetails.Winnings.ToString(dollarPattern));
            AddStatistic("cashLosses", cashGameDetails.Losses.ToString(dollarPattern));

            OverviewDetails tournamentDetails = LoadTournamentOverview(false);
            AddStatistic("tournamentHands", tournamentDetails.Hands.ToString());
            AddStatistic("tournamentWinnings", tournamentDetails.Winnings.ToString(dollarPattern));
            AddStatistic("freerollWinnings", () =>
                tournamentCollection.Aggregate().Match(t => t.Buyin.Total == 0)
                                                .Group(t => t.SitAndGo, t => new { sum = t.Sum(t2 => t2.Winnings)}).First().sum.ToString(dollarPattern));
            AddStatistic("tournamentCosts", tournamentDetails.Losses.ToString(dollarPattern));
            AddStatistic("tournamentsPlayed", tournamentDetails.Tournaments.ToString());

            OverviewDetails sitAndGoDetails = LoadTournamentOverview(true);
            AddStatistic("sitAndGoHands", sitAndGoDetails.Hands.ToString());
            AddStatistic("sitAndGoWinnings", sitAndGoDetails.Winnings.ToString(dollarPattern));
            AddStatistic("sitAndGoCosts", sitAndGoDetails.Losses.ToString(dollarPattern));
            AddStatistic("sitAndGosPlayed", sitAndGoDetails.Tournaments.ToString());


            decimal totalProfit =
                cashGameDetails.Winnings + tournamentDetails.Winnings + sitAndGoDetails.Winnings +
                cashGameDetails.Losses + tournamentDetails.Losses + sitAndGoDetails.Losses;
            long totalHands = cashGameDetails.Hands + tournamentDetails.Hands + sitAndGoDetails.Hands;
            AddStatistic("handsPlayed", totalHands.ToString());
            AddStatistic("profit", totalProfit.ToString(dollarPattern));
        }

        private OverviewDetails LoadCashOverview()
        {
            OverviewDetails details = new OverviewDetails();
            IAggregateFluent<HandHistory> cashGameHands =
                handCollection.Aggregate().Match(h => h.GameDescription.PokerFormat == PokerFormat.CashGame &&
                                                 h.GameDescription.Limit.Currency != Currency.PlayMoney);

            IAggregateFluent<UnwoundHand> actions = cashGameHands
                .Unwind<HandHistory, UnwoundHand>(h => h.HandActions)
                .Match(h => PlayerNames.Contains(h.HandActions.PlayerName));

            if (!cashGameHands.Any())
                return details;     // Return empty result set if there are no hands available

            details.Hands = cashGameHands.Count().First().Count;

            foreach (var profit in actions.Group(h => h.HandId, h => new { sum = h.Sum(h2 => h2.HandActions.Amount * h2.GameDescription.Limit.BigBlind)}).ToEnumerable())
            {
                if (profit.sum > 0)
                    details.Winnings += profit.sum;
                else
                    details.Losses += profit.sum;

            }
            
            return details;
        }

        private OverviewDetails LoadTournamentOverview(bool sitAndGo)
        {
            OverviewDetails details = new OverviewDetails();
            PokerFormat format = sitAndGo ? PokerFormat.SitAndGo : PokerFormat.MultiTableTournament;
            IAggregateFluent<HandHistory> hands = handCollection.Aggregate().Match(h => h.GameDescription.PokerFormat == format);

            if (!hands.Any())
                return details;

            details.Hands = hands.Count().First().Count;
            IAggregateFluent<TournamentSummary> tournaments =
                tournamentCollection.Aggregate()
                                    .Match(t => t.SitAndGo == sitAndGo && t.Buyin.Currency != Currency.PlayMoney);

            details.Winnings = tournaments.Group(t => t.SitAndGo, t => new { sum = t.Sum(t2 => t2.Winnings) }).First().sum;
            details.Losses = -tournaments.Group(t => t.SitAndGo, 
                t => new { sum = t.Sum(t2 => t2.Buyin.Total + t2.Rebuy.Total * t2.RebuyCount + t2.AddOn.Total) }).First().sum;
            details.Tournaments = tournaments.Count().First().Count;
            return details;
        }

        public void LoadStatisitcs(IMongoCollection<HandHistory> filteredHandCollection, int index)
        {
            potWonAtStreet =
                filteredHandCollection.Aggregate().Group(h => h.HandActions[-1].Street,
                                     h => new StatsByStreet<long> { result = h.Count(), _id = h.Key })
                              .ToList();
            /*
            amountCommitedAtStreet =
                filteredHandCollection.Aggregate().Unwind<HandHistory, UnwoundHand>(h => h.HandActions)
                                .Match(h => h.HandActions.Amount < 0)
                                .Group(h => new { street = (byte)h.HandActions.Street, id = h.DateOfHandUtc }, h => new { result = (decimal) h.Sum(h2 => h2.HandActions.Amount), _id = h.Key })
                                .Group(h => h._id, h => new StatsByStreetDecimal { result = h.Average(h2 => h2.result), _id = (Street) h.Key.street })
                                .ToList();
                                */
            /*
            handsByStreetReached =
                handCollection.Aggregate().Group(h =>  ((byte) h.HandActions.FindAll(h2 => h2.PlayerName == h.Hero.PlayerName)[-1].Street), 
                                        h => new StatsByStreet<long> { _id = (Street) h.Key, result = h.Count() })
                                        .ToList();
                                        */

            handsByStreetReached =
                filteredHandCollection.Aggregate().Unwind<HandHistory, UnwoundHand>(h => h.HandActions)
                    .Match(Builders<UnwoundHand>.Filter.In(h => h.HandActions.PlayerName, PlayerNames))
                    .Group(h => h.DateOfHandUtc, h => new AggregateResult<Street> { result = h.Max(h2 => h2.HandActions.Street)})
                    .Group(h => (byte)h.result, h => new StatsByStreet<long> { _id = (Street)h.Key, result = h.Count() })
                    .ToList();
            handsWonByPlayer =
                filteredHandCollection.Aggregate()
                    .Unwind<HandHistory, UnwoundHand>(h => h.HandActions)
                    .Match(Builders<UnwoundHand>.Filter.Where(h => PlayerNames.Contains(h.HandActions.PlayerName) && 
                                                            h.HandActions.HandActionType == HandActionType.WINS))
                    .Group(h => (byte)h.HandActions.Street,
                            h => new StatsByStreet<long> { _id = (Street)h.Key, result = h.Count() })
                    .ToList();

            //List<HandHistory> hands = handCollection.AsQueryable().ToList();
            long handCount = filteredHandCollection.Aggregate().Any() ? 
                                filteredHandCollection.Aggregate().Count().First().Count : 0;
            
            var streets = new[] { new { postfix = "Preflop", street = Street.Preflop },
                              new { postfix = "Flop", street = Street.Flop },
                              new { postfix = "Turn", street = Street.Turn },
                              new { postfix = "River", street = Street.River},
                              new { postfix = "Showdown", street = Street.Showdown}
            };

            long cumulativeHandCount = handCount;
            foreach (var street in streets)
            {
                Func<StatsByStreet<long>, long> countZeroIfNull = x => x != null ? x.result : 0;
                Func<StatsByStreet<decimal>, decimal> valueZeroIfNull = x => x != null ? x.result : 0;

                long handCountAtStreet = countZeroIfNull(potWonAtStreet.Find(s => s._id == street.street));
                AddStatistic("potsWon" + street.postfix,  
                    ()=> (handCountAtStreet / (decimal)handCount).ToString(precentPattern), index);
                AddStatistic("playerReached" + street.postfix,
                    () => (handsByStreetReached.Find(s => s._id == street.street).result/ (decimal)handCount).ToString(precentPattern), index);
                AddStatistic("playerWon" + street.postfix,
                    () => ((handsWonByPlayer.Find(s => s._id == street.street).result) / (decimal)handCount).ToString(precentPattern), index);

                /*
                if (street.street != Street.Showdown)
                    AddStatistic("aveAmountCommitted" + street.postfix,
                        () => (-amountCommitedAtStreet.Find(s => s._id == street.street).result).ToString(),
                        index);
                cumulativeHandCount -= handCountAtStreet;
                */
            }
        }

        private void AddStatistic(string name, string value, int index = 1)
        {
            Statistic stat = statistics.Find(s => s.Name == name);

            if (stat != null)
                stat.Listener(value != null ? value : stat.DefaultValue, index);
            else
                throw new ArgumentException(string.Format("Statistic '{0}' not found", name));
        }


        private void AddStatistic(string name, Func<string> valueFunc, int index = 1)
        {
            string value = null;
            try
            {
                value = valueFunc();
            }
            catch (DivideByZeroException e)
            {
                Console.Write("Query '{0}' failed due to divide by zero\n", name);
                // Use default value
            }
            catch (NullReferenceException e)
            {
                Console.Write("Query '{0}' failed due to null reference\n", name);
                // Use default value
            }
            catch (InvalidOperationException e)
            {
                Console.Write("Query '{0}' failed due to invalid operation\n", name);
            }

            AddStatistic(name, value, index);
        }



        /*public static List<StatsByPosition<long>> potWonByPosition(IMongoCollection<HandHistory> hands)
        {
            return hands.Aggregate().Group(h => h.)
        }*/
    }

    class OverviewDetails
    {
        public decimal Winnings { get; set; } = 0;
        public decimal Losses { get; set; } = 0;
        public long Hands { get; set; } = 0;
        public long Tournaments { get; set; } = 0;
    }
}
