using System.Runtime.Serialization;
using HandHistories.Objects.Cards;
using MongoDB.Bson.Serialization.Attributes;

namespace HandHistories.Objects.Actions
{
    [DataContract]
    [BsonDiscriminator("winnings")]
    public class WinningsAction : HandAction
    {
        [DataMember]
        public int PotNumber { get; private set; }

        public WinningsAction(string playerName, 
                              HandActionType handActionType, 
                              decimal amount,                               
                              int potNumber,
                              Street street = Street.Showdown,
                              int actionNumber = 0) : base(playerName, handActionType, amount, street, actionNumber)
        {
            PotNumber = potNumber;
        }

        public override string ToString()
        {
            return base.ToString() + "-Pot" + PotNumber;
        }
    }
}