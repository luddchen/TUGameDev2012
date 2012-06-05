using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Serializer.Converter
{
    /// <summary>
    /// This class manages the serialization of a crate.
    /// </summary>
    class CrateConverter : Converter
    {

        private Level level;
        private Game game;
        private ContentManager content;

        /// <summary>
        /// Creates a new CrateConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level and game parameter.
        /// </summary>
        public CrateConverter()
            : base("RoBuddies.Model.Objects.Crate")
        {
        }

        /// <summary>
        /// Creates a new CrateConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized crate state will be added to</param>
        /// <param name="game">the game of the level</param>
        /// <param name="content">the contentManager which is needed to add a default texture to the crate objects</param>
        public CrateConverter(Level level, Game game, ContentManager content)
            : base("RoBuddies.Model.Objects.Crate")
        {
            this.level = level;
            this.game = game;
            this.content = content;
        }

        /// <summary>
        /// This method deserialize a crate object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level and game parameters.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized crate</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Crate crate = null;
            if (level != null && game != null)
            {
                JToken tokens = JObject.ReadFrom(reader).First();
                Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
                Color color = tokens.SelectToken("Color").ToObject<Color>();
                float width = tokens.SelectToken("Size.Width").ToObject<float>();
                float height = tokens.SelectToken("Size.Heigth").ToObject<float>();
                crate = new Crate(pos, new Vector2(width, height), color, level, game);
            }
            else
            {
                throw new InvalidOperationException("no level or contentManager reference");
            }
            return crate;
        }

        /// <summary>
        /// This method will serialize a crate object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Crate crate = (Crate)value;
            writer.WritePropertyName("Crate");
            writer.WriteStartObject();
            writer.WritePropertyName("Position");
            serializer.Serialize(writer, crate.Position);
            writer.WritePropertyName("Color");
            serializer.Serialize(writer, crate.Color);
            writer.WritePropertyName("Size");
            writer.WriteStartObject();
            writer.WritePropertyName("Width");
            serializer.Serialize(writer, crate.Width);
            writer.WritePropertyName("Heigth");
            serializer.Serialize(writer, crate.Height);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}
