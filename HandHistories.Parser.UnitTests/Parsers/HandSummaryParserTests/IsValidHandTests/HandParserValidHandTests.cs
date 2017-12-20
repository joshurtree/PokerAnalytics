using HandHistories.Objects.GameDescription;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.Exceptions;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HandHistories.Parser.UnitTests.Parsers.HandSummaryParserTests.IsValidHandTests
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
    internal class HandParserValidHandTests : HandHistoryParserBaseTests
    {
        public HandParserValidHandTests(string site)
            : base(site)
        {

        }
       
        [TestCase(true)]
        [TestCase(false)]
        public void IsValidHand_Works(bool expected)
        {
             string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, expected);

            Assert.AreEqual(expected, GetParser().IsValidHand(handText), "IHandHistorySummaryParser: IsValidHand");
            Assert.AreEqual(expected, GetParser().IsValidHand(handText), "IHandHistoryParser: IsValidHand");
        }

        [Test]
        public void SummaryParser_InvalidHand_RethrowFalse_ReturnsNull()
        {
            string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, false);

            Assert.IsNull(GetParser().ParseFullHandSummary(handText, false), "IHandHistorySummaryParser: ParseFullHandSummary");
        }

        [Test]
        public void FullParser_InvalidHand_RethrowFalse_ReturnsNull()
        {
            string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, false);

            Assert.IsNull(GetParser().ParseFullHandHistory(handText, false), "IHandHistorySummaryParser: ParseFullHandSummary");
        }

        [Test]
        public void SummaryParser_InvalidHand_RethrowTrue_ThrowsException()
        {
            string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, false);

            Assert.Throws<InvalidHandException>(() => GetParser().ParseFullHandSummary(handText, true), "IHandHistorySummaryParser: ParseFullHandSummary");
        }

        [Test]
        public void FullParser_InvalidHand_RethrowTrue_ThrowsException()
        {
            string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, false);

            Assert.Throws<InvalidHandException>(() => GetParser().ParseFullHandHistory(handText, true), "IHandHistorySummaryParser: ParseFullHandSummary");
        }

        [Test]
        public void IsHandCancelled_Works()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.PartyPoker:
                case SiteName.Values.FullTilt:
                case SiteName.Values.IPoker:
                case SiteName.Values.OnGame:
                case SiteName.Values.MicroGaming:
                case SiteName.Values.BossMedia:
                case SiteName.Values.Ladbrokes:
                case SiteName.Values.Pacific:
                case SiteName.Values.Winamax:
                case SiteName.Values.Merge:
                case SiteName.Values.Entraction:
                    Assert.Ignore("Site do not produce cancelled handhistories: " + Site);
                    break;
            }

            string cancelledHandText = SampleHandHistoryRepository.GetCancelledHandHandHistoryText(PokerFormat.CashGame, Site);
            bool isCancelled;
            Assert.IsTrue(GetParser().IsValidOrCancelledHand(cancelledHandText, out isCancelled), "IHandHistorySummaryParser: IsCancelledHand");
            Assert.IsTrue(isCancelled, "IHandHistoryParser: IsCancelledHand");
        }

        List<IHandHistoryParser> GetAllParsers()
        {
            return new List<IHandHistoryParser>()
            {
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.Entraction).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.FullTilt).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.Pacific).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.PartyPoker).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.PokerStars).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.OnGame).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.Merge).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.MicroGaming).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.IPoker).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.Winamax).HandHistoryParser,
                HandHistoryParserFactory.GetHandHistoryParserFactory(SiteName.Values.WinningPoker).HandHistoryParser,
            };
        }

        [Test]
        public void IsValidHand_Unique()
        {
            string handText = SampleHandHistoryRepository.GetValidHandHandHistoryText(PokerFormat.CashGame, Site, true);

            var handParser = GetParser();
            Assert.AreEqual(true, handParser.IsValidHand(handText), "IHandHistoryParser: IsValidHand");

            foreach (var otherParser in GetAllParsers()
                .Where(p => p.SiteName != handParser.SiteName))
            {
                try
                {
                    Assert.IsFalse(otherParser.IsValidHand(handText), "IHandHistoryParser: Should be invalid hand");
                }
                catch
                {
                    continue;//When the parser throws that indicates that it is an invalid hand
                }
            }
        }
    }
}
