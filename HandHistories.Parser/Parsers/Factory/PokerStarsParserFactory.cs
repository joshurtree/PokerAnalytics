using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.PokerStars;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class PokerStarsParserFactory : HandHistoryParserFactory
    {
        public PokerStarsParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private PokerStarsFastParserImpl _HandHistoryParser = new PokerStarsFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }
    }
}