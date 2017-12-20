using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Serializers;
using HandHistories.Objects.GameDescription;
using MongoDB.Bson.Serialization;

namespace HandHistories.Objects.Utils
{
    public class SeatTypeSerializer : StructSerializerBase<SeatType>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, SeatType value)
        {
            context.Writer.WriteInt32((int) value.Type);
        }

        public override SeatType Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new SeatType { Type = (SeatType.SeatTypeEnum) context.Reader.ReadInt32() };
        }
    }
}
