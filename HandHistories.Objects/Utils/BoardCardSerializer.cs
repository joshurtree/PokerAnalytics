using HandHistories.Objects.Cards;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHistories.Objects.Utils
{
    public class BoardCardsSerializer : IBsonSerializer<BoardCards>
    {
        public Type ValueType
        {
            get
            {
                return typeof(BoardCards);
            }
        }

        public BoardCards Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return BoardCards.FromCards(BoardCards.Parse(context.Reader.ReadString()));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            Serialize(context, args, ((BoardCards)value));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BoardCards value)
        {
            context.Writer.WriteString(value.ToString());
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return this.Deserialize(context, args);
        }

    }
}
