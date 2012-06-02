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
    /// <summary>
    /// This class manages the serialization of a layer.
    /// </summary>
    class LayerConverter : Converter
    {

        private Level level;

        /// <summary>
        /// Creates a new LayerConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level parameter.
        /// </summary>
        public LayerConverter()
            : base("RoBuddies.Model.Layer")
        {
        }

        /// <summary>
        /// Creates a new LayerConverter, which can generate json
        /// </summary>
        /// <param name="level">the level where the deserialized layer will be added to</param>
        public LayerConverter(Level level)
            : base("RoBuddies.Model.Layer")
        {
            this.level = level;
        }

        /// <summary>
        /// This method deserialize a layer object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level parameter.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized layer</returns>
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            Layer layer = null;
            if (level != null)
            {
                JToken tokens = JObject.ReadFrom(reader).First();
                String name = tokens.SelectToken("Name").ToObject<String>();
                Vector2 parallax = tokens.SelectToken("Parallax").ToObject<Vector2>();
                float LayerDepth = tokens.SelectToken("LayerDepth").ToObject<float>();

                // test if layer exists
                layer = level.GetLayerByName(name);
                if (layer == null) { layer = new Layer(name, parallax, LayerDepth); }

                IJEnumerable<JToken> wallTokens = tokens.SelectToken("Walls").Values();
                foreach (JToken wallToken in wallTokens)
                {
                    Wall wall = serializer.Deserialize<Wall>(wallToken.CreateReader());
                    layer.AddObject(wall);
                }
                level.AddLayer(layer);
            }
            else
            {
                throw new InvalidOperationException("no level reference");
            }
            return layer;
        }

        /// <summary>
        /// This method will serialize an layer object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
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
