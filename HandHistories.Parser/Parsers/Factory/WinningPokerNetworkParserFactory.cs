using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.Winning;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class WinningPokerNetworkParserFactory : HandHistoryParserFactory
    {
        public WinningPokerNetworkParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private WinningPokerNetworkFastParserImpl _HandHistoryParser = new WinningPokerNetworkFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }
    }
}