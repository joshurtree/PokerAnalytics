﻿using System.Collections.Generic;
using System.Linq;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Players;
using HandHistories.Parser.UnitTests.Parsers.Base;
using NUnit.Framework;

namespace HandHistories.Parser.UnitTests.Parsers.HandParserTests.Players
{
    abstract internal class HandParserPlayersTests : HandHistoryParserBaseTests
    {
        protected HandParserPlayersTests(string site) : base(site)
        {
        }

        protected void TestParsePlayers(string fileName, PlayerList expectedPlayers)
        {
            string handText = SampleHandHistoryRepository.GetHandExample(PokerFormat.CashGame, Site, "PlayerTests", fileName);

            PlayerList playerList = GetParser().ParsePlayers(handText);

            Assert.AreEqual(expectedPlayers.Count, playerList.Count, "Player List Count");
            Assert.AreEqual(string.Join(",", expectedPlayers), string.Join(",", playerList));
        }

        protected abstract PlayerList ExpectedNoHoleCardsPlayers { get; }
        protected abstract PlayerList ExpectedWithShowdownPlayers { get; }
        protected abstract PlayerList ExpectedWithSittingOutPlayers { get; }
        protected abstract PlayerList ExpectedOmahaShowdownPlayers { get; }
        protected abstract PlayerList ExpectedOmahaHiLoShowdownPlayers { get; }
            
        [Test]
        public void ParsePlayers_NoHoleCards()
        {
            TestParsePlayers("NoHoleCards", ExpectedNoHoleCardsPlayers);
        }

        [Test]
        public void ParsePlayers_WithHoleCards()
        {
            TestParsePlayers("WithShowdown", ExpectedWithShowdownPlayers);
        }

        [Test]
        public void ParsePlayers_SittingOutPlayers()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.Winamax:
                case SiteName.Values.OnGame:
                case SiteName.Values.Pacific:
                case SiteName.Values.Entraction:
                case SiteName.Values.PartyPoker:
                case SiteName.Values.PokerStars:
                case SiteName.Values.BossMedia:
                    Assert.Ignore("No sitting out examples for " + Site);
                    break;
            }

            TestParsePlayers("WithSittingOut", ExpectedWithSittingOutPlayers);
        }

        [Test]
        public void ParsePlayers_OmahaShowdown()
        {
            switch ((SiteName.Values)Site)
            {            
                case SiteName.Values.Pacific:
                    Assert.Ignore("No omaha examples for " + Site);
                    break;
            }

            TestParsePlayers("OmahaShowdown", ExpectedOmahaShowdownPlayers);
        }

        [Test]
        public void ParsePlayers_OmahaHiLoShowdown()
        {
            switch ((SiteName.Values) Site)
            {
                case SiteName.Values.Winamax:
                case SiteName.Values.Merge:
                case SiteName.Values.IPoker:
                case SiteName.Values.Pacific:
                case SiteName.Values.WinningPoker:
                case SiteName.Values.BossMedia:
                    Assert.Ignore("No Hi-Lo examples for " + Site);
                    break;
            }

            TestParsePlayers("OmahaHiLoShowdown", ExpectedOmahaHiLoShowdownPlayers);
        }
    }
}
