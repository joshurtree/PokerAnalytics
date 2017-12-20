using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.MicroGaming;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class MicroGamingParserFactory : HandHistoryParserFactory
    {
        public MicroGamingParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private MicroGamingFastParserImpl _HandHistoryParser = new MicroGamingFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

    }
}