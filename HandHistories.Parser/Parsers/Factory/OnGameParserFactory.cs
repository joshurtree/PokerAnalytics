using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.OnGame;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class OnGameParserFactory : HandHistoryParserFactory
    {
        public OnGameParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private OnGameFastParserImpl _HandHistoryParser = new OnGameFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

    }
}