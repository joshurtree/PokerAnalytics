using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.FullTiltPoker;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class FullTiltPokerParserFactory : HandHistoryParserFactory
    {
        public FullTiltPokerParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        FullTiltPokerFastParserImpl _HandHistoryParser = new FullTiltPokerFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get { return _HandHistoryParser; }
        }

    }
}