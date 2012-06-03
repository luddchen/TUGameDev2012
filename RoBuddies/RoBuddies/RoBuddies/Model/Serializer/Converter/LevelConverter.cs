using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RoBuddies.Model.Serializer
{
    /// <summary>
    /// This class manages the serialization of a level.
    /// </summary>
    class LevelConverter : Converter
    {

        private Level level;

        /// <summary>
        /// Creates a new LevelConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level parameter.
        /// </summary>
        public LevelConverter()
            : base("RoBuddies.Model.Level")
        {
        }

        /// <summary>
        /// Creates a new LevelConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized level state will be added to</param>
        public LevelConverter(Level level)
            : base("RoBuddies.Model.Level")
        {
            this.level = level;
        }

        /// <summary>
        /// This method deserialize a level object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level parameter.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized level</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (level != null)
            {
                JToken tokens = JObject.ReadFrom(reader);
                Vector2 gravity = tokens.SelectToken("Gravity").ToObject<Vector2>();
                level.Gravity = gravity;
                IJEnumerable<JToken> layerTokens = tokens.SelectToken("Layers").Values();
                foreach (JToken layerToken in layerTokens)
                {
                    Layer layer = serializer.Deserialize<Layer>(layerToken.CreateReader());
                    if (level.GetLayerByName(layer.Name) == null)
                    {
                        level.AddLayer(layer);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("no level reference");
            }
            return level;
        }

        /// <summary>
        /// This method will serialize a level object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
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
