using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Serializers;
using HandHistories.Objects.Cards;
using MongoDB.Bson.Serialization;

namespace HandHistories.Objects.Utils
{
    public class CardSerializer : StructSerializerBase<Card>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Card value)
        {
            context.Writer.WriteInt32(value.CardIntValue);
        }

        public override Card Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Card(context.Reader.ReadInt32());
        }
    }
}
