using HandHistories.Objects.GameDescription;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;

namespace HandHistories.Parser.UnitTests.Parsers.HandSummaryParserTests.GameTypes
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
    [TestFixture("WinningPoker")]
    [TestFixture("BossMedia")]
    class HandParserGameTypeTests : HandHistoryParserBaseTests 
    {
        public HandParserGameTypeTests(string site) : base(site)
        {
           
        }
        
        private void TestGameType(GameType expected)
        {
            string handText = SampleHandHistoryRepository.GetGameTypeHandHistoryText(PokerFormat.CashGame, Site, expected);

            Assert.AreEqual(expected, GetParser().ParseGameType(handText), "IHandHistorySummaryParser: ParseGameType");
            Assert.AreEqual(expected, GetParser().ParseGameType(handText), "IHandHistoryParser: ParseGameType");
        }
        
        [Test]
        public void ParseGameType_ParsesFixedLimitHoldem()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.Winamax:
                    Assert.Ignore(Site + " currently doesn't have FL example.");
                    return;
                case SiteName.Values.WinningPoker:
                    Assert.Ignore(Site + " does not make a diffrence for Fixed/No Limit/Limit");
                    return;
            }
            TestGameType(GameType.FixedLimitHoldem);
        }

        [Test]
        public void ParseGameType_ParsesNoLimitHoldem()
        {
            TestGameType(GameType.NoLimitHoldem);
        }

        [Test]
        public void ParseGameType_ParsesPotLimitOmaha()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.FullTilt:
                    Assert.Ignore(Site + " currently doesn't have Pot Limit Omaha example.");
                    return;
            }

            TestGameType(GameType.PotLimitOmaha);
        }

        [Test]
        public void ParseGameType_ParsesFixedLimitOmahaHiLo()
        {
            if (Site != SiteName.Values.Entraction)
            {
                Assert.Ignore(Site + " currently doesn't have Fixed Limit Omaha HiLo.");
                return;
            }

            TestGameType(GameType.FixedLimitOmahaHiLo);
        }

        [Test]
        public void ParseGameType_ParsesPotLimitHoldem()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.MicroGaming:
                case SiteName.Values.Merge:
				case SiteName.Values.IPoker:
                case SiteName.Values.FullTilt:
                case SiteName.Values.OnGame:
                case SiteName.Values.Winamax:
                case SiteName.Values.PartyPoker:
                case SiteName.Values.BossMedia:
                    Assert.Ignore(Site + " currently doesn't have pot limit holdem.");
                    break;
                case SiteName.Values.WinningPoker:
                    Assert.Ignore(Site + " does not make a diffrence for Fixed/No Limit/Limit");
                    return;
            }

            TestGameType(GameType.PotLimitHoldem);
        }

        [Test]
        public void ParseGameType_ParsesNoLimitOmaha()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.MicroGaming:
                case SiteName.Values.IPoker:
                case SiteName.Values.PartyPoker:
                case SiteName.Values.Merge:
                case SiteName.Values.OnGame:
                case SiteName.Values.PokerStars:
                case SiteName.Values.PokerStarsFr:
                case SiteName.Values.PokerStarsIt:
                case SiteName.Values.FullTilt:
                case SiteName.Values.Entraction:
                case SiteName.Values.Winamax:
                case SiteName.Values.Pacific:
                case SiteName.Values.BossMedia:
                    Assert.Ignore(Site + " currently doesn't have No Limit Omaha example.");
                    break;
                case SiteName.Values.WinningPoker:
                    Assert.Ignore(Site + " does not make a diffrence for Fixed/No Limit/Limit");
                    return;
            }

            TestGameType(GameType.NoLimitOmaha);
        }

        [Test]
        public void ParseGameType_ParsesNoLimitOmahaHiLo()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.MicroGaming:
                case SiteName.Values.IPoker:
                case SiteName.Values.PartyPoker:
                case SiteName.Values.Merge:
                case SiteName.Values.OnGame:
                case SiteName.Values.Pacific:
                case SiteName.Values.PokerStarsFr:
                case SiteName.Values.FullTilt:
                case SiteName.Values.PokerStarsIt:
                case SiteName.Values.Entraction:
                case SiteName.Values.Winamax:
                case SiteName.Values.BossMedia:
                    Assert.Ignore(Site + " currently doesn't have No Limit Omaha HiLo example.");
                    break;
                case SiteName.Values.WinningPoker:
                    Assert.Ignore(Site + " does not make a diffrence for Fixed/No Limit/Limit");
                    return;
            }

            TestGameType(GameType.NoLimitOmahaHiLo);
        }
        
        [Test]
        public void ParseGameType_ParsesPotLimitOmahaHiLo()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.Merge:
				case SiteName.Values.IPoker:
                case SiteName.Values.FullTilt:
                case SiteName.Values.Entraction:
                case SiteName.Values.Winamax:
                    Assert.Ignore(Site + " currently doesn't have Pot Limit Omaha HiLo example.");
                    break;
                case SiteName.Values.WinningPoker:
                    Assert.Ignore(Site + " does not make a diffrence for Fixed/No Limit/Limit");
                    return;
            }

            TestGameType(GameType.PotLimitOmahaHiLo);
        }

        [Test]
        public void ParseGameType_ParsesFiveCardPotLimitOmaha()
        {
            if (Site != SiteName.Values.Entraction)
            {
                Assert.Ignore(Site + " currently doesn't have sample for " + GameType.FiveCardPotLimitOmaha);
            }

            TestGameType(GameType.FiveCardPotLimitOmaha);
        }
    }
}
