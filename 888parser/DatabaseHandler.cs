using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Players;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using HandHistories.Objects.Utils;
namespace PokerAnalytics
{ 
    class DatabaseHandler
    {
        public static class Collections
        {
            public const string hands = "hands";
            public const string settings = "settings";
            public const string tournaments = "tournaments";
        }

        
        public static class Views
        {
            public const string allHands = "hands";
            public const string cashHands = "cashHands";
            public const string sitAndGoHands = "sitAndGoHands";
            public const string tournamentHands = "tournamentHands";
            public const string QuickView = "quickView";
        }

        private const string DATABASE_NAME = "PokerAnalytics";
        private IMongoDatabase database;

        private static DatabaseHandler handler;

        private DatabaseHandler()
        {
            database = new MongoClient().GetDatabase(DATABASE_NAME);
        }

        private void Initiate()
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
 

            List<BsonDocument> collections = database.ListCollections().ToList();
            if (collections.Find(h => h["name"].Equals(Views.cashHands)) == null)
                CreateDefaultViews();
        }
        private void CreateDefaultViews()
        {
            AddView(Views.cashHands, 
                (PipelineDefinition <HandHistory, HandHistory>) new BsonDocument[] {
                    new BsonDocument{ { "$match", new BsonDocument("GameDescription.PokerFormat", 0) } }
                });

            AddView(Views.sitAndGoHands,
                (PipelineDefinition<HandHistory, HandHistory>)new BsonDocument[]
                {
                    new BsonDocument { {"$match", new BsonDocument("GameDescription.PokerFormat", 1) } }
                });

            AddView(Views.tournamentHands, 
                (PipelineDefinition<HandHistory, HandHistory>)new BsonDocument[]
                {
                    new BsonDocument { {"$match", new BsonDocument("GameDescription.PokerFormat", 2) } }
                });
        }

        public static DatabaseHandler getInstance()
        {
            if (handler == null)
            {
                handler = new DatabaseHandler();
                if (!handler.database.RunCommandAsync<BsonDocument>("{ping : 1}").Wait(3000))
                {
                    handler = null;
                    throw new MongoException("Failed to connect to database");
                }
                handler.Initiate();
            }
            return handler;
        }

        public void Add(HandHistory hand)
        {
            database.GetCollection<HandHistory>(Collections.hands).InsertOne(hand);
        }
 
        public void Add(TournamentSummary tournament)
        {
            database.GetCollection<TournamentSummary>(Collections.tournaments).InsertOne(tournament);
        }
        
        public void AddView(string name, PipelineDefinition<HandHistory, HandHistory> pipeline)
        {
            database.CreateView<HandHistory, HandHistory>(name, Collections.hands, pipeline);
        }

        public IMongoCollection<HandHistory> GetView(string name)
        {
            return database.GetCollection<HandHistory>(name);
        }

        public IMongoCollection<HandHistory> GetAllHands() 
        {
            return database.GetCollection<HandHistory>(Collections.hands);
        }

        public IMongoCollection<TournamentSummary> GetAllTournaments()
        {
            return database.GetCollection<TournamentSummary>(Collections.tournaments);
        }

        public void ClearDatabase()
        {
            database.DropCollection(Collections.hands);
            database.DropCollection(Collections.tournaments);
            Properties.Settings.Default.lastUpdate = DateTime.MinValue;
            Properties.Settings.Default.Save(); 
        }

        public void Update(string tournamentId, UpdateDefinition<TournamentSummary> newSummary)
        {
            database.GetCollection<TournamentSummary>(Collections.tournaments)
                .UpdateOne(Builders<TournamentSummary>.Filter.Where(h => h.Id == tournamentId), newSummary);
        }
    }
}
