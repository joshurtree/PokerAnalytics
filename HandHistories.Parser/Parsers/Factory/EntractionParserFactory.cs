using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.Entraction;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class EntractionParserFactory : HandHistoryParserFactory
    {
        public EntractionParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        EntractionFastParserImpl _HandHistoryParser = new EntractionFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get { return _HandHistoryParser; }
        }

    }
}