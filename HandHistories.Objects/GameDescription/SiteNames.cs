using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using HandHistories.Objects.Utils;

namespace HandHistories.Objects.GameDescription
{
    [BsonSerializer(typeof(SiteNameSerializer))]
    public struct SiteName
    {
        public enum Values : byte
        {
            Unknown,
            Pacific, // This is 888
            Bodog,
            BossMedia,
            Cereus,
            DollaroPoker,
            Entraction,
            Everest,
            FullTilt,
            IPoker,
            IPoker2,
            IPokerIt,
            IPokerFr,
            Ladbrokes,
            Merge,
            MicroGaming,
            OnGame,
            OnGameIt,
            OnGameFr,
            PartyPoker,
            PartyPokerEs,
            PartyPokerFr,
            PartyPokerIt,
            PartyPokerNJ,
            PokerStars,
            PokerStarsFr,
            PokerStarsEs,
            PokerStarsIt,
            PokerStarsZoom,
            Winamax,
            WinningPoker,
            //All = 63 // note: can't go higher than 63 due to bit value optimizations
        }

        public static readonly string[] PokerSites = {
            "Unknown",
            "888",
            "Bodog",
            "Boss Media",
            "Cereus",
            "Dollaro Poker",
            "Entraction",
            "Everest",
            "FullTilt",
            "iPoker",
            "iPoker - low",
            "iPoker It",
            "iPoker Fr",
            "Ladbrokes",
            "Merge",
            "Micro Gaming",
            "OnGame",
            "OnGame It",
            "OnGame Fr",
            "Party Poker",
            "Party Poker Es",
            "Party Poker Fr",
            "Party Poker It",
            "Party Poker NJ",
            "Poker Stars",
            "Poker Stars Fr",
            "Poker Stars Es",
            "Poker Stars It",
            "Poker Stars Zoom",
            "Winamax",
            "WinningPoker",
        };

        private Values Site;

        private SiteName(int site)
        {
            Site = (SiteName.Values) site;
        }

        public SiteName(SiteName.Values site)
        {
            Site = site;
        }

        #region operators
        public static implicit operator int(SiteName site)
        {
            return (int) site.Site;
        }

        public static implicit operator SiteName(int site)
        {
            return new SiteName(site);
        }

        public static implicit operator SiteName.Values(SiteName site)
        {
            return site.Site;
        }

        public static implicit operator SiteName(SiteName.Values site)
        {
            return new SiteName((int) site);
        }
        #endregion

        public static SiteName ParseSiteName(string site)
        {
            if (string.IsNullOrEmpty(site))
            {
                return SiteName.Values.Unknown;
            }

            switch (site.ToLower().Replace(" ", ""))
            {
                //case "all":
                //    return Values.All;
                case "ftp":
                case "fulltilt":
                case "fulltiltpoker":
                    return Values.FullTilt;
                case "ps":
                case "pokerstars":
                case "stars":
                case "strs":
                    return Values.PokerStars;
                case "partypoker":
                case "party":
                case "pty":
                case "pp":
                    return Values.PartyPoker;
                case "ipoker1":
                case "ipoker-top":
                case "ipoker - top":
                case "ipoker":
                case "titanpoker":
                case "titan":
                case "poker770":
                case "expektpoker":
                    return Values.IPoker;
                case "absolute":
                case "cereus":
                case "ub":
                case "ultimatebet":
                    return Values.Cereus;
                case "ongame":
                case "on game":
                case "bestpoker":
                    return Values.OnGame;
                case "bodog":
                    return Values.Bodog;
                case "merge":
                case "carbon":
                case "carbonpoker":
                    return Values.Merge;
                case "entraction":
                    return Values.Entraction;
                case "microgaming":
                    return Values.MicroGaming;
                case "ladbrokes":
                    return Values.Ladbrokes;
                case "everest":
                    return Values.Everest;
                case "international":
                case "bossmedia":
                case "boss":
                case "paradise":
                    return Values.BossMedia;
                case "dollaropoker":
                    return Values.DollaroPoker;
                case "starsfr":
                case "pokerstarsfr":
                    return Values.PokerStarsFr;
                case "starsit":
                case "pokerstarsit":
                    return Values.PokerStarsIt;
                case "starses":
                case "pokerstarses":
                    return Values.PokerStarsEs;
                case "partyfr":
                case "partypokerfr":
                    return Values.PartyPokerFr;
                case "partyit":
                case "partypokerit":
                    return Values.PartyPokerIt;
                case "partynj":
                case "partypokernj":
                    return Values.PartyPokerNJ;
                case "partyes":
                case "partypokeres":
                    return Values.PartyPokerEs;
                case "ongameit":
                    return Values.OnGameIt;
                case "ongamefr":
                    return Values.OnGameFr;
                case "ipokerit":
                    return Values.IPokerIt;
                case "ipokerfr":
                    return Values.IPokerFr;
                case "ipoker2":
                case "ipoker-low":
                case "betmost":
                    return Values.IPoker2;
                case "888":
                case "888poker":
                case "pacific":
                    return Values.Pacific;
                case "winamax":
                case "winamaxfr":
                case "winamax.fr":
                    return Values.Winamax;
                default:
                    return Values.Unknown;
            }
        }

        public override string ToString()
        {
            return PokerSites[(int) Site];
        }
    }
}