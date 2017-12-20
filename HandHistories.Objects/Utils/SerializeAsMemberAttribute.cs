using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;

namespace HandHistories.Objects.Utils
{
    /*
    public class SerializeAsMemberAttribute : Attribute, IBsonClassMapAttribute
    {
        private string Member;

        public SerializeAsMemberAttribute()
        {
            Member = null;
        }

        public SerializeAsMemberAttribute(string member)
        {
            Member = member;
        }

        public void Apply(BsonClassMap classMap)
        {
            classMap.SetIsRootClass(false);
            BsonSerializer.RegisterSerializer(classMap.ClassType, new SingleMemberSerializer(classMap.ClassType, Member));
        }
    }
    
    class SingleMemberSerializer : SerializerBase
    {
        private string Member;
        private Type ParentType;

        public SingleMemberSerializer(Type parentType)
        {
            ParentType = parentType;
            Member = null;
        }
        public SingleMemberSerializer(Type parentType, string member)
        {
            ParentType = parentType;
            Member = member;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var field = Member == null ? args.NominalType.GetFields().First() : args.NominalType.GetField(Member);
            BsonSerializer.Serialize(context.Writer, field.FieldType, field.GetValue(value));
        }

        public override object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var field = Member == null ? args.NominalType.GetFields().First() : args.NominalType.GetField(Member);
            var fieldValue = BsonSerializer.Deserialize(context.Reader, field.FieldType);
            return Activator.CreateInstance(ParentType, fieldValue);
        }
    }
    */
}
