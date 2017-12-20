using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHistories.Objects.Cards
{
    public class StartingHand : HandRange
    {
        public static string[] SUITS = new string[] { "c", "d", "h", "s" };
        private static string[] ranks = Card.PossibleRanksHighCardFirst.Reverse().ToArray();


        private static Dictionary<string, StartingHand> _hands = SetupStartingHands();
        public static IReadOnlyDictionary<string, StartingHand> AllStartingHands
        {
            get { return (IReadOnlyDictionary<string, StartingHand>)_hands; }
        }

        public static IEnumerable<StartingHand> RankedHands
        {
            get
            {
                return AllStartingHands.Values.OrderBy(hand => -hand.ChenRanking() -
                                                        hand.PrimaryRank / 100.0f -
                                                        hand.SecondaryRank / 1000.0f);
            }
        }

        public int PrimaryRank { get; }
        public int SecondaryRank { get; }
        public HandType Type { get; }

        private static Dictionary<string, StartingHand> SetupStartingHands()
        {
            string[] ranks = Card.PossibleRanksHighCardFirst.Reverse().ToArray();
            Dictionary<string, StartingHand> hands = new Dictionary<string, StartingHand>();
            for (int i = 12; i >= 0; --i)
            {
                for (int j = 12; j >= 0; --j)
                {
                    string text;

                    if (i > j)
                        text = ranks[i] + ranks[j] + "o";
                    else if (i < j)
                        text = ranks[j] + ranks[i] + "s";
                    else
                        text = ranks[i] + ranks[j];

                    StartingHand hand = new StartingHand(text);
                    hands.Add(text, hand);
                }
            }

            return hands;
        }

        // Returns a StartingHand representing a generalised form of the hand (i.e. AA, AKs or AKo)
        public static StartingHand FindStartingHand(CardGroup Cards)
        {
            string retVal = "";

            if (Cards[0].RankNumericValue == Cards[1].RankNumericValue)
                return _hands[Cards[0].Rank + Cards[1].Rank];
            else if (Cards[0].RankNumericValue > Cards[1].RankNumericValue)
                retVal = Cards[0].Rank + Cards[1].Rank;
            else
                retVal = Cards[1].Rank + Cards[0].Rank;

            retVal += Cards[0].Suit.Equals(Cards[1].Suit) ? "s" : "o";
            return _hands[retVal];
        }

        private StartingHand(string hand)
        {
            PrimaryRank = Card.GetRankNumericValue(hand[0].ToString());
            SecondaryRank = Card.GetRankNumericValue(hand[1].ToString());

            if (hand.Length == 2)
                Type = HandType.Pair;
            else if (hand[2] == 's')
                Type = HandType.Suited;
            else
                Type = HandType.OffSuit;

            for (int i = 0; i <= 3; ++i)
            {
                if (Type == HandType.Suited)
                {
                    Add((1UL << (PrimaryRank * 4 + i)) | (1UL << (SecondaryRank * 4 + i)));
                }
                else
                {
                    for (int j = (Type == HandType.OffSuit ? 0 : i + 1); j <= 3; ++j)
                        if (i != j)
                            Add((1UL << (PrimaryRank * 4 + i)) | (1UL << (SecondaryRank * 4 + j)));
                }
            }
        }

        public static float[] ChenValue = new float[] { 1, 1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5, 6, 7, 8, 10 };
        public float ChenRanking()
        {
            float retVal = ChenValue[PrimaryRank];
            ;
            int seperation = PrimaryRank - SecondaryRank;
            switch (seperation)
            {
                case 0:
                    return retVal * 2;

                case 1:
                case 2:
                case 3:
                    retVal -= seperation - 1;
                    break;

                case 4:
                    retVal -= 4;
                    break;

                default:
                    retVal -= 5;
                    break;
            }

            if (PrimaryRank < Card.GetRankNumericValue("Q") && seperation < 2)
                retVal += 1;

            if (Type == HandType.Suited)
                retVal += 2;

            return retVal;
        }

        public override string ToString()
        {
            return ranks[PrimaryRank] + ranks[SecondaryRank] + PostFixes[(int)Type];
        }

        public enum HandType { Pair, Suited, OffSuit }
        private static string[] PostFixes = { "", "s", "o" };
        private static int[] HandCounts = { 6, 4, 12 };
    }
}
