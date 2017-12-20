using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HandHistories.Objects.GameDescription
{
    [Serializable]
    [DataContract]
    public class TournamentSummary
    {
        [DataMember]
        [BsonId]
        public string Id { get; set; }

        [DataMember]
        public Buyin Buyin { get; set; } = new Buyin();

        [DataMember]
        public Buyin Rebuy { get; set; } = new Buyin();

        [DataMember]
        public int RebuyCount = 0;

        [DataMember]
        public Buyin AddOn { get; set; } = new Buyin();

        [DataMember]
        public decimal Winnings { get; set; } = 0;

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int Players { get; set; }

        [DataMember]
        public bool SitAndGo { get; set; }

        public TournamentSummary()
        {
            
        }

        public TournamentSummary(string id, bool sitAndGo, Buyin buyin, int players = 0, decimal winnings = 0, int position = 0)
        {
            Id = id;
            SitAndGo = sitAndGo;
            Buyin = buyin;
            Players = players;
            Winnings = winnings;
            Position = position;
        }

        public decimal TotalBuyin()
        {
            return Buyin.Total + (Rebuy.Total * RebuyCount) + AddOn.Total;
        }

        public override string ToString()
        {
            string retVal = string.Format("{0} #{1}, ${2} buyin. Finished {3}/{4}", 
                                          SitAndGo ? "Sit & Go" : "Tournament", 
                                          Id, 
                                          Buyin,  
                                          Position,
                                          Players);
            if (Winnings > 0)
                retVal += string.Format(" winning ${0}", Winnings);

            return retVal;
        }
    }
}
