using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.Objects.Cards;
using HandHistories.Objects.Hand;
using MongoDB.Driver;
using HandHistories.Objects.Players;
using HandHistories.Objects.Actions;
using MongoDB.Bson;
using PokerAnalytics.Properties;
using System.ComponentModel;
using HoldemHand;
using System.Collections.Concurrent;

namespace PokerAnalytics
{
    public class HandEvaluator : BackgroundWorker
    {
        public IList<HandRange> Ranges { get; }
        public IList<double> Bets { get; }
        public ulong Board { get; }
        public ulong Dead { get; }
        public List<HandResult> Results { get; private set; }
        long HandCount = 0;

        public HandEvaluator(IList<HandRange> ranges, IList<double> bets, ulong board, ulong dead)
        {
            Ranges = ranges;
            Bets = bets;
            Board = board;
            Dead = dead;

            WorkerSupportsCancellation = true;
            DoWork += DoCalculateResult;
            RunWorkerAsync();
        }

        public void DoCalculateResult(object sender, DoWorkEventArgs ev)
        {
            Results = new List<HandResult>();
            CalculateResult(new List<ulong>(), Dead);
        }

        public void CalculateResult(IList<ulong> hands, ulong dead)
        {
            if (CancellationPending)
                return;

            if (dead == 0)
                dead = Dead;
            // Count of total hands examined.
            int depth = hands.Count;
            HandRange range = Ranges[depth];

            foreach (ulong hand in range)
            {
                if ((hand & (dead | Board)) == 0)
                {
                    dead = hand | dead;
                    hands.Add(hand);
                    if (depth < Ranges.Count)
                    {
                        CalculateResult(hands, dead);
                    }
                    else
                    {
                        foreach (ulong boardMask in Hand.Hands(Board, dead, 5))
                        {
                            EvaluateHand(hands, boardMask, dead);
                            ReportProgress(0);
                            if (CancellationPending)
                                break;
                        }
                    }
                }

                if (CancellationPending)
                    break;
            }
        }

        public void EvaluateHand(IList<ulong> hands, ulong boardMask, ulong dead)
        {
            List<Pot> pots = new List<Pot>();
            pots.Add(new Pot());
            for (int i = 0; i < hands.Count; ++i)
            {
                uint eval = Hand.Evaluate(hands[i] | boardMask);
                int pos = 0;
                while (Bets[i] >= pots[pos].Size)
                {
                    if (eval > pots[pos].HandScore)
                    {// Wins pot
                        pots.Insert(pos, new Pot(eval, Bets[i], i));
                        break;
                    }
                    else if (eval == pots[pos].HandScore)
                    {
                        pots[pos].Players.Add(i);   // Shares pot
                        break;
                    }

                    ++pos;
                }

                if (pos < pots.Count - 1 && pots[pos].Size >= pots[pos + 1].Size)
                    pots.RemoveRange(pos + 1, pots.Count - pos);
            }

            if (pots.First().Players.Count == 1)
                Results[pots.First().Players.First()].Wins++;
            else
                foreach (int player in pots.First().Players)
                    Results[player].Ties++;

            double lastPot = 0;
            foreach (Pot pot in pots)
            {
                foreach (int player in pot.Players)
                    Results[player].ExpectedValue += Bets[player] / (double)pot.Players.Count;
            }

            ++HandCount;

        }
    }

    public class HandResult
    {
        public double ExpectedValue { get; set; }
        public long Wins { get; set; }
        public long Ties { get; set; }
    }

    public class Pot
    {
        public uint HandScore { get; set; }
        public double Size { get; set; }
        public List<int> Players { get; set; }

        // Dummy Pot
        public Pot()
        {
            HandScore = 0;
            Size = 0;
        }

        public Pot(uint handScore, double size, int player)
        {
            HandScore = handScore;
            Size = size;
            Players = new List<int>();
            Players.Add(player);
        }
    }


    //public delegate void RangeListener(Dictionary<StartingHand, double> scores, long totalCount);

    class RangeWorker : BackgroundWorker
    {
        private IMongoCollection<HandHistory> View;
        private decimal RaiseMultiplier;
        private bool Implied;
        public ConcurrentDictionary<StartingHand, double> Counts { get; private set; }
        public long TotalCount { get; private set; }

        //private List<RangeListener> Listeners;

        public RangeWorker(string viewName, decimal raiseMultiplier, bool implied)
        {
            WorkerSupportsCancellation = true;
            WorkerReportsProgress = true;
            DoWork += CalculateStartingHandRanges;

            View = DatabaseHandler.getInstance().GetView(viewName);
            RaiseMultiplier = raiseMultiplier;
            Implied = implied;
        }


        public void CalculateStartingHandRanges(object sender, DoWorkEventArgs ev)
        {
            if (Implied)
                CalculateImpliedStartingHands();
            else
                CalculateKnwonStartingHands();
        }


        public void CalculateKnwonStartingHands()
        {
            Counts = new ConcurrentDictionary<StartingHand, double>();
            TotalCount = 0;
            foreach (StartingHand hand in StartingHand.AllStartingHands.Values)
                Counts.TryAdd(hand, 0);

            foreach (HandHistory hand in View.Find(new BsonDocument()).ToEnumerable())
            {
                //HandHistory hand = view.Find(h => h.HandId.Equals(handidObj.HandId)).First();
                if (hand.HandActions.FindAll(h => h.IsPreFlopRaise).Sum(h => h.Amount) < RaiseMultiplier + 1)
                {
                    foreach (Player player in hand.Players)
                    {
                        if (player.IsSittingOut)
                            continue;
                        if (!player.Equals(hand.Hero) && player.hasHoleCards)
                        {
                            StartingHand startingHand = StartingHand.FindStartingHand(player.HoleCards);
                            Counts[startingHand] += 1 / (double)startingHand.Count;
                            ++TotalCount;
                        }
                    }

                    if (TotalCount % Settings.Default.RangeUpdateInterval == 0)
                        ReportProgress(10);

                    if (CancellationPending)
                        break;
                }
            }

            ReportProgress(10);
            //Console.Write(string.Format("{0} showdown actions found", handsFound));
        }




        public void CalculateImpliedStartingHands()
        {
            Dictionary<StartingHand, double> scores = new Dictionary<StartingHand, double>();
            long totalcount = 0;
            //long handid = 0;
            long handsFound = 0;
            foreach (HandHistory hand in View.Find(new BsonDocument()).ToEnumerable())
            {
                foreach (Player player in hand.Players)
                {
                    if (hand.HandActions.FindAll(a => a.PlayerName.Equals(player.PlayerName) && a.Street == Street.Preflop && !a.IsBlinds && !a.IsWinningsAction).Sum(a => a.Amount) >= RaiseMultiplier)
                        ++handsFound;
                    
                    ++totalcount;
                }

                double fractionPlayed = ((handsFound / (double)totalcount) * 52);
                int handCount = 0;
                foreach (StartingHand sHand in StartingHand.RankedHands)
                {
                    handCount += sHand.Count;

                    scores[sHand] = handCount > fractionPlayed ? 0 : 1;
                }

                ReportProgress(0);
                if (CancellationPending)
                    break;
            }
        }
    }

    public class OrderedCardGroup 
    {
        public Dictionary<string, SortedSet<Card>> suitedCards { get; }
        public SortedList<int, List<Card>> rankedCards { get;  }

        public OrderedCardGroup()
        {
            suitedCards = new Dictionary<string, SortedSet<Card>>();
            rankedCards = new SortedList<int, List<Card>>();
        }

        public OrderedCardGroup(HoleCards holeCards, BoardCards boardCards) : this()
        {
            AddCards(holeCards);
            AddCards(boardCards);
        }

        public OrderedCardGroup(IEnumerable<Card> cards)
        {
            AddCards(cards);
        }

        public void AddCard(Card card)
        {
            if (!suitedCards.ContainsKey(card.Suit))
                suitedCards[card.Suit] = new SortedSet<Card>(new RankComparator());
            suitedCards[card.Suit].Add(card);

            if (!rankedCards.ContainsKey(card.RankNumericValue))
                rankedCards[card.RankNumericValue] = new List<Card>();
            suitedCards[card.Suit].Add(card);
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
                AddCard(card);
        }

        public Card HighCard()
        {
            return rankedCards.Last().Value.First();
        }

        private class RankComparator : IComparer<Card>
        {
            public int Compare(Card x, Card y)
            {
                return x.RankNumericValue - x.RankNumericValue; 
            }
        }
    }

}
