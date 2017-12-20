using System;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.FastParser.Winamax;
using HandHistories.Parser.Parsers.FastParser.Base;

namespace HandHistories.Parser.Parsers.Factory
{
    internal class WinamaxParserFactory : HandHistoryParserFactory
    {

        public WinamaxParserFactory(SiteDetails siteDetails) : base(siteDetails)
        {
        }

        private WinamaxFastParserImpl _HandHistoryParser = new WinamaxFastParserImpl();
        public override HandHistoryParserFast HandHistoryParser
        {
            get
            {
                return _HandHistoryParser;
            }
        }
    }
}