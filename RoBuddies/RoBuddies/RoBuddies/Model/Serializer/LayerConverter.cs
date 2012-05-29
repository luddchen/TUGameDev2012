using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Model.Serializer
{
    public class LayerConverter : Converter
    {

        private Level level;

        public LayerConverter()
            : base("RoBuddies.Model.Layer")
        {
        }

        public LayerConverter(Level level)
            : base("RoBuddies.Model.Layer")
        {
            this.level = level;
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JToken tokens = JObject.ReadFrom(reader).First();
            String name = tokens.SelectToken("Name").ToObject<String>();
            Vector2 parallax = tokens.SelectToken("Parallax").ToObject<Vector2>();
            float LayerDepth = tokens.SelectToken("LayerDepth").ToObject<float>();;
            Layer layer = new Layer(name, parallax, LayerDepth, null);
            IJEnumerable<JToken> wallTokens = tokens.SelectToken("Walls").Values();
            foreach (JToken wallToken in wallTokens)
            {
                Wall wall = serializer.Deserialize<Wall>(wallToken.CreateReader());
                layer.AllObjects.Add(wall);
            }
            level.AllLayers.Add(layer);
            return layer;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            Layer layer = (Layer)value;
            
            writer.WritePropertyName("Layer");
            writer.WriteStartObject();
                writer.WritePropertyName("Name");
                serializer.Serialize(writer, layer.Name);
                writer.WritePropertyName("Parallax");
                serializer.Serialize(writer, layer.Parallax);
                writer.WritePropertyName("LayerDepth");
                serializer.Serialize(writer, layer.LayerDepth);
                writer.WritePropertyName("Walls");
                writer.WriteStartArray();
                foreach (IBody obj in layer.AllObjects)
                {
                    writer.WriteStartObject();
                    serializer.Serialize(writer, obj);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
