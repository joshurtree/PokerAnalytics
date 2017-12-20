using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHistories.Objects.Cards
{

    public class HandRange : SortedSet<ulong>
    {
        public void AddRange(HandRange range)
        {
            foreach (ulong hand in range)
                Add(hand);
        }

        public static HandRange Parse(string handsString)
        {
            string[] hands = handsString.Split(',');
            HandRange handRange = new HandRange();

            foreach (string hand in hands)
            {
                string trimedHand = hand.Trim();
                if (StartingHand.AllStartingHands.ContainsKey(trimedHand))
                    handRange.AddRange(StartingHand.AllStartingHands[trimedHand]);
                else if (trimedHand.EndsWith("+"))
                {
                    ParseRange(trimedHand);
                }
                else if (trimedHand.Contains("-"))
                {
                    string[] endHands = trimedHand.Split('-');
                    ParseRange(endHands[0].Trim(), Card.GetRankNumericValue(endHands[1].Trim().Substring(1, 1)));
                }
            }

            return handRange;
        }

        public static HandRange AllHands()
        {
            HandRange range = new HandRange();
            foreach (StartingHand hand in StartingHand.AllStartingHands.Values)
                range.AddRange(hand);
            return range;
        }

        public override string ToString()
        {
            switch (Count)
            {
                case 1:
                    return ToStringSingle();

                case 1336:
                    return "All";

                default:
                    return ToStringMultiple();
            }
        }

        private string ToStringSingle()
        {
            ulong hand = this.First();
            List<Card> cards = new List<Card>();

            for (int card = 0; card <= 52; ++card)
            {
                if ((1 << card) != 0)
                    cards.Add(new Card(card));
            }

            return cards[0].RankNumericValue >= cards[1].RankNumericValue ? cards[0].ToString() + cards[1].ToString() : cards[1].ToString() + cards[0].ToString();
        }

        private string ToStringMultiple()
        {
            List<string> retVal = new List<string>();
            SortedSet<ulong> hands = new SortedSet<ulong>(this);
            RangeComparer rangeComparer = new RangeComparer();
            SortedSet<StartingHand> sHands = new SortedSet<StartingHand>(rangeComparer);

            foreach (StartingHand hand in StartingHand.AllStartingHands.Values)
            {
                if (hands.IsSupersetOf(hand))
                {
                    hands.RemoveWhere(h => hand.Contains(h));
                    sHands.Add(hand);
                }
            }

            List<StartingHand> run = new List<StartingHand>();

            foreach (StartingHand hand in sHands)
            {
                if (run.Count == 0 || rangeComparer.Compare(run.Last(), hand) == 1)
                    run.Add(hand);
                else
                {
                    StartingHand start = run.Last();
                    StartingHand end = run.First();
                    if (end.PrimaryRank - end.SecondaryRank == 1)
                        retVal.Add(start.ToString() + "+");
                    else
                        retVal.Add(start.ToString() + "-" + end.ToString());
                }

            }

            return string.Join(", ", retVal);
        }

        private static HandRange ParseRange(string startHand, int endRank = (int)Card.CardValueEnum._A)
        {
            HandRange range = new HandRange();
            Func<string, string> genhand;
            if (startHand[0] == startHand[1])
            {
                genhand = rank => rank + rank;
            }
            else
            {
                genhand = rank => startHand[0] + rank + startHand[2];
            }

            for (int rank = Card.GetRankNumericValue(startHand.Substring(1, 1)); rank <= endRank; ++rank)
                range.AddRange(StartingHand.AllStartingHands[genhand(((Card.CardValueEnum)rank).ToString())]);

            return range;
        }
    }


    class RangeComparer : IComparer<StartingHand>
    {
        private static List<string> RunOrder()
        {
            List<string> pairs = new List<string>();
            List<string> suited = new List<string>();
            List<string> offsuit = new List<string>();


            for (int rank1 = 0; rank1 < Card.PossibleRanksHighCardFirst.Length; ++rank1)
            {

                pairs.Add(Card.PossibleRanksHighCardFirst[rank1] + Card.PossibleRanksHighCardFirst[rank1]);
                for (int rank2 = rank1 + 1; rank2 < Card.PossibleRanksHighCardFirst.Length; ++rank2)
                {
                    string ranks = Card.PossibleRanksHighCardFirst[rank1] + Card.PossibleRanksHighCardFirst[rank2];
                    suited.Add(ranks + "s");
                    offsuit.Add(ranks + "o");
                }

                // Add braeks so that A2 and KQ are divided into seperate runs  
                suited.Add("break");
                offsuit.Add("break");
            }

            List<string> order = new List<string>();
            order.AddRange(pairs);
            order.AddRange(suited);
            order.AddRange(offsuit);
            return order;
        }

        private static List<string> HandOrder = RunOrder();
        public int Compare(StartingHand x, StartingHand y)
        {
            return HandOrder.IndexOf(x.ToString()) - HandOrder.IndexOf(y.ToString());
        }
    }
}