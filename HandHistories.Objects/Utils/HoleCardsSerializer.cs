using HandHistories.Objects.Cards;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHistories.Objects.Utils
{
    public class HoleCardsSerializer : IBsonSerializer<HoleCards>
    {
        public Type ValueType
        {
            get
            {
                return typeof(HoleCards);
            }
        }

        public HoleCards Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType != MongoDB.Bson.BsonType.Null)
            {
                string cards = context.Reader.ReadString();
                return new HoleCards(CardGroup.Parse(cards));
            }
            else
            {
                context.Reader.SkipValue();
                return null;
            }
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            Serialize(context, args, (HoleCards)value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, HoleCards value)
        {
            if (value != null)
            {
                context.Writer.WriteString(value.ToString());
            }
            else
            {
                context.Writer.WriteNull();
            }
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }
}
