using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Utils;

namespace HandHistories.Objects.Tests.GameDescription
{
    [TestFixture]
    class SiteNameTests
    {
        [Test]
        public void SiteNameToString()
        {
            Assert.AreEqual("Poker Stars", new SiteName(SiteName.Values.PokerStars).ToString());
            Assert.AreEqual("888", new SiteName(SiteName.Values.Pacific).ToString());
        }
    }
}
