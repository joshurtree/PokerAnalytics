using System;
using System.Runtime.Serialization;
using HandHistories.Objects.Utils;
using MongoDB.Bson.Serialization.Attributes;

namespace HandHistories.Objects.Cards
{
    //When Card is a struct it only allocates 1 byte on the stack instead of 4 Reference bytes and two strings on the heap
    //Combined with lookup tables and using enums we get a 20x speedup of parsing cards
    [DataContract]
    //[SerializeAsMember]
    [BsonSerializer(typeof(CardSerializer))]
    public partial struct Card
    {
        public static readonly char[] SUITS = { 'c', 'd', 'h', 's' };
        public static readonly char[] RANKS = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        const int SuitCardMask = 0x03;
        const int RankCardMask = 0xFF ^ SuitCardMask;

        #region Properties
        private SuitEnum suit
        {
            get
            {
                return (SuitEnum)((int)_card & SuitCardMask);
            }
        }

        private CardValueEnum rank
        {
            get
            {
                return (CardValueEnum)((int)_card & RankCardMask);
            }
        }

        public string Rank
        {
            get
            {
                //int rank = (int)_card & RankCardMask;
                return ((CardValueEnum)rank).ToString().Substring(1);
            }
        }

        public int RankNumericValue
        {
            get
            {
                return (int) rank;
            }
        }

        public string Suit
        {
            get
            {
                return ((SuitEnum)suit).ToString().Substring(1).ToLower();
            }
        }

        public string CardStringValue
        {
            get { return _card.ToString().Substring(1); }
        }

        public bool isEmpty
        {
            get
            {
                return _card == CardEnum._Unknown;
            }
        }
        #endregion

        [DataMember]
        [BsonElement]
        private CardEnum _card;

        #region Constructors
        
        public Card(int card)
        {
            _card = (card >= 0 && card <= 51)  ? (CardEnum) card : CardEnum._Unknown;
        }

        public Card(char rank, char suit)
        {
            _card = CardEnum._Unknown;

            if (rankParseLookup[rank] == CardValueEnum.Unknown)
                throw new ArgumentException("Rank is not correctly formatted. Should be 2-9, T, J, Q, K or A.");
            if (suitParseLookup[suit] == SuitEnum.Unknown)
                throw new ArgumentException("Suit is not correctly formatted. Should be c, d, h or s.");

            _card = (CardEnum)((int)suitParseLookup[suit] + (int)rankParseLookup[rank]);
        }

        /// <summary>
        /// </summary>
        /// <param name="rank">Rank should be 2-9,T,J,Q,K,A.</param>
        /// <param name="suit">Suit should be c,d,h,s.</param>
        public Card(string rank, string suit) : this(rank[0], suit[0])
        {
        }

        private Card(SuitEnum suit, CardValueEnum rank)
        {
            _card = (CardEnum)((int)suit + (int)rank);
        }
        #endregion

        #region Operators
        public static bool operator ==(Card c1, Card c2)
        {
            return c1._card == c2._card;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            return c1._card != c2._card;
        }

        public static explicit operator Card(string card)
        {
            return Card.Parse(card);
        }

        public static explicit operator Card(int card)
        {
            return new Card(card);
        }

        public static explicit operator int(Card card)
        {
            return (int)card._card;
        }
        #endregion

        public static string [] PossibleRanksHighCardFirst
        {
            get
            {
                return new string[]
                       {
                           "A",
                           "K",
                           "Q",
                           "J",
                           "T",
                           "9",
                           "8",
                           "7",
                           "6",
                           "5",
                           "4",
                           "3",
                           "2"
                       };
            }            
        }

        public static Card Parse(string card)
        {
            if (card.Length != 2)
                throw new ArgumentException("Cards must be length 2. Format Rs where R is rank and s is suit.");

            return new Card(card[0], card[1]);
        }
       
        public static Card GetCardFromIntValue(int value)
        {
            return new Card(value);
        }

        /// <summary>
        /// 2c = 0, 3c = 1, ..., Ac = 12, ..., As = 51. Returns -1 if there was an error with the rank or suit values.
        /// </summary>
        public int CardIntValue
        {
            get
            {
                return (int) _card;
            }
        }

        public static string GetMaximumRank(string rank1, string rank2)
        {
            return GetRankNumericValue(rank1) > GetRankNumericValue(rank2) ? rank1 : rank2;
        }

        public static string GetMinimumRank(string rank1, string rank2)
        {
            return GetRankNumericValue(rank1) < GetRankNumericValue(rank2) ? rank1 : rank2;
        }

        public static int GetRankNumericValue(string rank1)
        {
            return (int)rankParseLookup[rank1[0]] >> 2;
        }

        public static int ToRank(int card)
        {
            return (card & RankCardMask) >> 2;
        }

        public static int ToSuit(int card)
        {
            return (card & SuitCardMask);
        }

        public override string ToString()
        {
            return CardStringValue;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj.ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return _card.GetHashCode();
        }
    }
}
