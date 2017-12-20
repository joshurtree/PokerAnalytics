using System;
using System.Runtime.Serialization;

namespace HandHistories.Objects.Cards
{
    public partial struct Card
    {
        #region EnumLookups
        public readonly static CardValueEnum[] rankParseLookup = initRankParseLookup();

        private static CardValueEnum[] initRankParseLookup()
        {
            CardValueEnum[] ranks = new CardValueEnum[128];
            for (int i = 0; i < 128; ++i)
                ranks[i] = CardValueEnum.Unknown;
            ranks['A'] = CardValueEnum._A;
            ranks['a'] = CardValueEnum._A;
            ranks['K'] = CardValueEnum._K;
            ranks['k'] = CardValueEnum._K;
            ranks['Q'] = CardValueEnum._Q;
            ranks['q'] = CardValueEnum._Q;
            ranks['J'] = CardValueEnum._J;
            ranks['j'] = CardValueEnum._J;
            ranks['T'] = CardValueEnum._T;
            ranks['t'] = CardValueEnum._T;
            for (int i = 2; i <= 9; i++)
            {
                ranks[i.ToString()[0]] = (CardValueEnum)((i-2)*4);
            }
            return ranks;
        }

        public readonly static SuitEnum[] suitParseLookup = initSuitParseLookup();

        private static SuitEnum[] initSuitParseLookup()
        {
            SuitEnum[] suits = new SuitEnum[128];
            for (int i = 0; i < 128; ++i)
                suits[i] = SuitEnum.Unknown;

            suits['C'] = SuitEnum.Clubs;
            suits['c'] = SuitEnum.Clubs;
            suits['D'] = SuitEnum.Diamonds;
            suits['d'] = SuitEnum.Diamonds;
            suits['H'] = SuitEnum.Hearts;
            suits['h'] = SuitEnum.Hearts;
            suits['S'] = SuitEnum.Spades;
            suits['s'] = SuitEnum.Spades;
            return suits;
        }
        #endregion

        #region Enums
        public enum CardValueEnum : byte
        {
            _2 = 0x0,
            _3 = 0x4,
            _4 = 0x8,
            _5 = 0xC,
            _6 = 0x10,
            _7 = 0x14,
            _8 = 0x18,
            _9 = 0x1C,
            _T = 0x20,
            _J = 0x24,
            _Q = 0x28,
            _K = 0x2C,
            _A = 0x30,
            Unknown = 0xFF
        }

        public enum SuitEnum : byte
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3,
            Unknown = 0xFF
        }

        [DataContract]
        public enum CardEnum : byte
        {
            [EnumMember]
            _2c = SuitEnum.Clubs | CardValueEnum._2,
            [EnumMember]
            _3c = SuitEnum.Clubs | CardValueEnum._3,
            [EnumMember]
            _4c = SuitEnum.Clubs | CardValueEnum._4,
            [EnumMember]
            _5c = SuitEnum.Clubs | CardValueEnum._5,
            [EnumMember]
            _6c = SuitEnum.Clubs | CardValueEnum._6,
            [EnumMember]
            _7c = SuitEnum.Clubs | CardValueEnum._7,
            [EnumMember]
            _8c = SuitEnum.Clubs | CardValueEnum._8,
            [EnumMember]
            _9c = SuitEnum.Clubs | CardValueEnum._9,
            [EnumMember]
            _Tc = SuitEnum.Clubs | CardValueEnum._T,
            [EnumMember]
            _Jc = SuitEnum.Clubs | CardValueEnum._J,
            [EnumMember]
            _Qc = SuitEnum.Clubs | CardValueEnum._Q,
            [EnumMember]
            _Kc = SuitEnum.Clubs | CardValueEnum._K,
            [EnumMember]
            _Ac = SuitEnum.Clubs | CardValueEnum._A,
            [EnumMember]
            _2d = SuitEnum.Diamonds | CardValueEnum._2,
            [EnumMember]
            _3d = SuitEnum.Diamonds | CardValueEnum._3,
            [EnumMember]
            _4d = SuitEnum.Diamonds | CardValueEnum._4,
            [EnumMember]
            _5d = SuitEnum.Diamonds | CardValueEnum._5,
            [EnumMember]
            _6d = SuitEnum.Diamonds | CardValueEnum._6,
            [EnumMember]
            _7d = SuitEnum.Diamonds | CardValueEnum._7,
            [EnumMember]
            _8d = SuitEnum.Diamonds | CardValueEnum._8,
            [EnumMember]
            _9d = SuitEnum.Diamonds | CardValueEnum._9,
            [EnumMember]
            _Td = SuitEnum.Diamonds | CardValueEnum._T,
            [EnumMember]
            _Jd = SuitEnum.Diamonds | CardValueEnum._J,
            [EnumMember]
            _Qd = SuitEnum.Diamonds | CardValueEnum._Q,
            [EnumMember]
            _Kd = SuitEnum.Diamonds | CardValueEnum._K,
            [EnumMember]
            _Ad = SuitEnum.Diamonds | CardValueEnum._A,
            [EnumMember]
            _2h = SuitEnum.Hearts | CardValueEnum._2,
            [EnumMember]
            _3h = SuitEnum.Hearts | CardValueEnum._3,
            [EnumMember]
            _4h = SuitEnum.Hearts | CardValueEnum._4,
            [EnumMember]
            _5h = SuitEnum.Hearts | CardValueEnum._5,
            [EnumMember]
            _6h = SuitEnum.Hearts | CardValueEnum._6,
            [EnumMember]
            _7h = SuitEnum.Hearts | CardValueEnum._7,
            [EnumMember]
            _8h = SuitEnum.Hearts | CardValueEnum._8,
            [EnumMember]
            _9h = SuitEnum.Hearts | CardValueEnum._9,
            [EnumMember]
            _Th = SuitEnum.Hearts | CardValueEnum._T,
            [EnumMember]
            _Jh = SuitEnum.Hearts | CardValueEnum._J,
            [EnumMember]
            _Qh = SuitEnum.Hearts | CardValueEnum._Q,
            [EnumMember]
            _Kh = SuitEnum.Hearts | CardValueEnum._K,
            [EnumMember]
            _Ah = SuitEnum.Hearts | CardValueEnum._A,
            [EnumMember]
            _2s = SuitEnum.Spades | CardValueEnum._2,
            [EnumMember]
            _3s = SuitEnum.Spades | CardValueEnum._3,
            [EnumMember]
            _4s = SuitEnum.Spades | CardValueEnum._4,
            [EnumMember]
            _5s = SuitEnum.Spades | CardValueEnum._5,
            [EnumMember]
            _6s = SuitEnum.Spades | CardValueEnum._6,
            [EnumMember]
            _7s = SuitEnum.Spades | CardValueEnum._7,
            [EnumMember]
            _8s = SuitEnum.Spades | CardValueEnum._8,
            [EnumMember]
            _9s = SuitEnum.Spades | CardValueEnum._9,
            [EnumMember]
            _Ts = SuitEnum.Spades | CardValueEnum._T,
            [EnumMember]
            _Js = SuitEnum.Spades | CardValueEnum._J,
            [EnumMember]
            _Qs = SuitEnum.Spades | CardValueEnum._Q,
            [EnumMember]
            _Ks = SuitEnum.Spades | CardValueEnum._K,
            [EnumMember]
            _As = SuitEnum.Spades | CardValueEnum._A,
            [EnumMember]
            _Unknown = CardValueEnum.Unknown
        }
        #endregion
    }
}
