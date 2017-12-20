using System;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using HandHistories.Objects.Utils;
namespace HandHistories.Objects.Cards
{
    [DataContract]
    [BsonSerializer(typeof(HoleCardsSerializer))]
    public class HoleCards : CardGroup
    {
        //[DataMember]
        //public string PlayerName { get; private set; }

        public HoleCards(params Card [] cards) : base(cards)
        {
        }


        public static HoleCards ForHoldem(Card card1, Card card2)
        {
            return new HoleCards(card1, card2);
        }

        public static HoleCards ForHoldem(string playerName, Card card1, Card card2)
        {
            return new HoleCards(card1, card2);
        }

        public static HoleCards ForOmaha(Card card1, Card card2, Card card3, Card card4)
        {
            return new HoleCards(card1, card2, card3, card4);
        }

        /*
        public static HoleCards ForOmaha(string playerName, Card card1, Card card2, Card card3, Card card4)
        {            
            return new HoleCards(playerName, card1, card2, card3, card4);
        }*/

        public static HoleCards NoHolecards()
        {
            return new HoleCards();
        }

        /*
        public static HoleCards NoHolecards(string playerName)
        {
            return new HoleCards(playerName);
        }

        public static HoleCards FromCards(string playerName, string cards)
        {
            Card[] cardsArray = Parse(cards);

            return FromCards(playerName, cardsArray);
        }*/

        public static HoleCards FromCards(string cards)
        {
            Card[] cardsArray = Parse(cards);

            return FromCards(cardsArray);
        }

        
        public static HoleCards FromCards(Card[] cards)
        {
             switch (cards.Length)
             {
                 case 0:
                     return NoHolecards();
                 case 2:
                     return ForHoldem(cards[0], cards[1]);
                 case 4:
                     return ForOmaha(cards[0], cards[1], cards[2], cards[3]);
                 default:
                     throw new ArgumentException("Hole cards must contain atleast 0, 2 or 4 cards.");
             }
         }
                 
    }
}
