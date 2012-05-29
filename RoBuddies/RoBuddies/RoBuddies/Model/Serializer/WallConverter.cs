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
            writer.Flush();
            writer.WritePropertyName("Wall");
            writer.WriteStartObject();
                writer.WritePropertyName("Position");
                serializer.Serialize(writer, wall.Position);
                writer.WritePropertyName("Color");
                serializer.Serialize(writer, wall.Color);
                writer.WritePropertyName("Size");
                writer.WriteStartObject();
                    writer.WritePropertyName("Width");
                    serializer.Serialize(writer, wall.Width);
                    writer.WritePropertyName("Heigth");
                    serializer.Serialize(writer, wall.Height);
                writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}
