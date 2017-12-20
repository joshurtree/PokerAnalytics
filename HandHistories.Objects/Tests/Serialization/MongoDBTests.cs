using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System;
using System.Collections.Generic;
using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Players;
using HandHistories.Objects.UnitTests.Utils.Serialization;
using NUnit.Framework;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using HandHistories.Objects.Utils;

namespace HandHistories.Objects.UnitTests.Serialization
{
    [TestFixture]
    class MongoDBTests
    {
        private HandHistory _handHistory;

        [SetUp]
        public void SetUp()
        {
            _handHistory = new HandHistory()
                               {                                  
                                   ComumnityCards =
                                       BoardCards.ForFlop(new Card(5), new Card(14), new Card(40)),
                                   DateOfHandUtc = new DateTime(2012, 3, 20, 12, 30, 44),
                                   DealerButtonPosition = 5,
                                   FullHandHistoryText = "some hand text",
                                   GameDescription =
                                       new GameDescriptor(PokerFormat.CashGame,
                                                          SiteName.Values.PartyPoker, 
                                                          GameType.NoLimitHoldem,
                                                          Limit.FromSmallBlindBigBlind(10, 20, Currency.USD),
                                                          TableType.FromTableTypeDescriptions(TableTypeDescription.Regular),
                                                          SeatType.FromMaxPlayers(6)),
                                   HandActions = new List<HandAction>()
                                                     {
                                                         new HandAction("Player1", HandActionType.POSTS, 0.25m, Street.Preflop)
                                                     },
                                   HandId = 141234124,
                                   NumPlayersSeated = 2,
                                   Players = new PlayerList()
                                                 {
                                                     new Player("Player1", 1000, 1),
                                                     new Player("Player2", 300, 5),
                                                 },
                                   TableName = "Test Table",                                   
                               };
            _handHistory.Players[0].HoleCards = new HoleCards(CardGroup.Parse("Ac Ad"));
            _handHistory.Players[1].HoleCards = new HoleCards(CardGroup.Parse("Kh Qs"));

            BsonSerializer.RegisterSerializationProvider(new TestSerializationProvider());
        }

        [Test]
        public void CanSerailizeHandHistory()
        {
            BsonDocument serialized = _handHistory.ToBsonDocument();
            Assert.IsNotNull(serialized);
            Assert.IsNotEmpty(serialized);
        }

        [Test]
        public void CanDeSerailizeHandHistory()
        {
           BsonDocument serialized = _handHistory.ToBsonDocument();

            HandHistory deserailizedHandHistory = (HandHistory) BsonSerializer.Deserialize(serialized, typeof(HandHistory));

            Assert.AreEqual(_handHistory, deserailizedHandHistory);
        }
    }

    class TestSerializationProvider : IBsonSerializationProvider
    {
        Dictionary<Type, IBsonSerializer> Serializers = new Dictionary<Type, IBsonSerializer>();

        public TestSerializationProvider()
        {
            Serializers.Add(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            Serializers.Add(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            //Serializers.Add(typeof(SeatType), new SeatTypeSerializer());
            //Serializers.Add(typeof(Card), new CardSerializer());
            //Serializers.Add(typeof(SiteName), new SiteNameSerializer());

        }

        public IBsonSerializer GetSerializer(Type type)
        {
            return Serializers.ContainsKey(type) ? Serializers[type] : null;
        }
    }
}
