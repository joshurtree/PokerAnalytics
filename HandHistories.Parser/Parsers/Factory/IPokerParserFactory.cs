using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.IPoker;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class IPokerParserFactory : HandHistoryParserFactory
    {

        public IPokerParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private IPokerFastParserImpl _HandHistoryParser = new IPokerFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

    }
}