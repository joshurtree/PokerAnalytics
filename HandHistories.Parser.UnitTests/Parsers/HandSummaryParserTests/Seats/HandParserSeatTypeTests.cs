using HandHistories.Objects.GameDescription;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;

namespace HandHistories.Parser.UnitTests.Parsers.HandSummaryParserTests.Seats
{
    [TestFixture("PartyPoker")]
    [TestFixture("PokerStars")]
    [TestFixture("OnGame")]
    [TestFixture("IPoker")]
    [TestFixture("Pacific")]
    [TestFixture("Merge")]
    [TestFixture("Entraction")]
    [TestFixture("FullTilt")]
    [TestFixture("MicroGaming")]
    [TestFixture("Winamax")]
    class HandParserSeatTypeTests : HandHistoryParserBaseTests 
    {
        public HandParserSeatTypeTests(string site) : base(site)
        {
        }

        private void TestSeatType(SeatType expectedSeatType)
        {
            string handText = SampleHandHistoryRepository.GetSeatExampleHandHistoryText(PokerFormat.CashGame, Site, expectedSeatType);

            Assert.AreEqual(expectedSeatType, GetParser().ParseSeatType(handText), "IHandHistorySummaryParser: ParseSeatType");
            Assert.AreEqual(expectedSeatType, GetParser().ParseSeatType(handText), "IHandHistoryParser: ParseSeatType");
        }

        [Test]
        public void ParseSeatType_HeadsUp()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.MicroGaming:
                    Assert.Ignore(Site + " currently only has anonymous HU tables");
                    break;
            }
            TestSeatType(SeatType.FromMaxPlayers(2));
        }

        [Test]
        public void ParseSeatType_6Max()
        {
            TestSeatType(SeatType.FromMaxPlayers(6));
        }

        [Test]
        public void ParseSeatType_4Max()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.IPoker:
                case SiteName.Values.Merge:
                case SiteName.Values.PartyPoker:
                case SiteName.Values.PokerStars:
                case SiteName.Values.OnGame:
                case SiteName.Values.Entraction:
                case SiteName.Values.FullTilt:
                case SiteName.Values.MicroGaming:
                case SiteName.Values.Winamax:
                    Assert.Ignore(Site + " currently doesn't have 4 max games.");
                    break;
            }            

            TestSeatType(SeatType.FromMaxPlayers(4));
        }

        [Test]
        public void ParseSeatType_10Handed()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.PartyPoker:
                case SiteName.Values.Merge:
                case SiteName.Values.OnGame:
                case SiteName.Values.Entraction:
                case SiteName.Values.FullTilt:
                case SiteName.Values.MicroGaming:
                case SiteName.Values.IPoker:
                case SiteName.Values.Winamax:
                    Assert.Ignore(Site + " currently doesn't have 10 handed games.");
                    break;
            }            

            TestSeatType(SeatType.FromMaxPlayers(10));
        }

        [Test]
        public void ParseSeatType_9Handed()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.Winamax:
                    Assert.Ignore(Site + " currently doesn't have 9 handed games.");
                    break;
            }
            TestSeatType(SeatType.FromMaxPlayers(9));
        }

    }
}
