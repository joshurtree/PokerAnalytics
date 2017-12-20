using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.Merge;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class MergeParserFactory : HandHistoryParserFactory
    {
        public MergeParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private MergeFastParserImpl _HandHistoryParser = new MergeFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }

    }
}