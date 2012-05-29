using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace RoBuddies.Model.Serializer
{
    public class LevelConverter : Converter
    {

        private Level level;

        public LevelConverter()
            : base("RoBuddies.Model.Level")
        {
        }

        public LevelConverter(Level level)
            : base("RoBuddies.Model.Level")
        {
            this.level = level;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken tokens = JObject.ReadFrom(reader);
            Vector2 gravity = tokens.SelectToken("Gravity").ToObject<Vector2>();
            level.Gravity = gravity;
            IJEnumerable<JToken> layerTokens = tokens.SelectToken("Layers").Values();
            foreach (JToken layerToken in layerTokens)
            {
                Layer layer = serializer.Deserialize<Layer>(layerToken.CreateReader());
                layer.Level = level;
                level.AllLayers.Add(layer);
            }
            return level;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Level level = (Level)value;
            writer.WriteStartObject();
                writer.WritePropertyName("Gravity");
                serializer.Serialize(writer, level.Gravity);
                writer.WritePropertyName("Layers");
                writer.WriteStartArray();
                    foreach(Layer layer in level.AllLayers) {
                        writer.WriteStartObject();
                        serializer.Serialize(writer, layer);
                        writer.WriteEndObject();
                    }
                writer.WriteEndArray();
           writer.WriteEndObject();
        }
    }
}
