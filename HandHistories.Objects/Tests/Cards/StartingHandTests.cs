using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HandHistories.Objects.Cards;

namespace HandHistories.Objects.Tests.Cards
{
    [TestFixture]
    class StartingHandTests
    {
        [Test]
        public void CountTest()
        {
            Assert.AreEqual(6, StartingHand.AllStartingHands["AA"].Count);
            Assert.AreEqual(4, StartingHand.AllStartingHands["AKs"].Count);
            Assert.AreEqual(12, StartingHand.AllStartingHands["AKo"].Count);
        }
    }
}
