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
    public class SiteNameSerializer : StructSerializerBase<SiteName>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, SiteName value)
        {
            context.Writer.WriteInt32(value);
        }

        public override SiteName Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return (SiteName) context.Reader.ReadInt32() ;
        }
    }
}
