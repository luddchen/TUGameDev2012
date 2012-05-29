using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Serializer
{
    public class WallConverter : Converter
    {

        public WallConverter()
            : base("RoBuddies.Model.Objects.Wall")
        {

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Wall wall = (Wall)value;
            writer.WriteStartObject();
            writer.WritePropertyName("Wall");
                //writer.WriteStartObject()
            writer.WriteEndObject();
        }
    }
}
