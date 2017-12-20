using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.BossMedia;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class BossMediaParserFactory : HandHistoryParserFactory
    {
        public BossMediaParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        BossMediaFastParserImpl _HandHistoryParser = new BossMediaFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get { return _HandHistoryParser; }
        }
    }
}