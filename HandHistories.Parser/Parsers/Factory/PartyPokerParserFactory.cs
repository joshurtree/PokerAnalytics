using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.Base;
using HandHistories.Parser.Parsers.FastParser.PartyPoker;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class PartyPokerParserFactory : HandHistoryParserFactory
    {
        public PartyPokerParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private PartyPokerFastParserImpl _HandHistoryParser = new PartyPokerFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

    }
}