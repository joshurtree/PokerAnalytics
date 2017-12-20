using System;
using System.Security.Policy;
using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.UnitTests.Infrastructure;
using Ninject;

namespace HandHistories.Parser.UnitTests.Parsers.Base
{
    abstract class HandHistoryParserBaseTests
    {
        protected readonly SiteName Site;

        // Due to issue I reported here have to take site as a string:
        //  http://youtrack.jetbrains.com/issue/RSRP-292611
        protected HandHistoryParserBaseTests(string site)
        {
            Kernel = new NinjectKernel();
            SampleHandHistoryRepository = Kernel.Get<ISampleHandHistoryRepository>();
            Site = (SiteName)Enum.Parse(typeof(SiteName), site);
        }

        protected IKernel Kernel;

        protected ISampleHandHistoryRepository SampleHandHistoryRepository;

        protected IHandHistoryParser GetParser()
        {
            HandHistoryParserFactory handHistoryParserFactory = HandHistoryParserFactory.GetHandHistoryParserFactory(Site);
            return handHistoryParserFactory.HandHistoryParser;
        }
    }
}