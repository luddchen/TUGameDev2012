using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RoBuddies.Model.Serializer
{
    public class LevelConverter : Converter
    {

        public LevelConverter()
            : base("RoBuddies.Model.Level")
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Level level = (Level)value;
            writer.WriteStartObject();
                writer.WritePropertyName("Layers");
                writer.WriteStartObject();
                    foreach(Layer layer in level.AllLayers) {
                        serializer.Serialize(writer, layer);
                    }
                writer.WriteEndObject();
           writer.WriteEndObject();
        }
    }
}
