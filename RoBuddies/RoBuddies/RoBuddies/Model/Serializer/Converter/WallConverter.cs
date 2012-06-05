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
    class WallConverter : Converter
    {

        private Level level;
        private Game game;

        /// <summary>
        /// Creates a new WallConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level and game parameter.
        /// </summary>
        public WallConverter()
            : base("RoBuddies.Model.Objects.Wall")
        {
        }

        /// <summary>
        /// Creates a new WallConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized crate state will be added to</param>
        /// <param name="game">the game of the level</param>
        public WallConverter(Level level, Game game)
            : base("RoBuddies.Model.Objects.Wall")
        {
            this.level = level;
            this.game = game;
        }

        /// <summary>
        /// This method deserialize a crate object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level and contentManger parameters.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized crate</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Wall wall = null;
            if (level != null && game != null)
            {
                JToken tokens = JObject.ReadFrom(reader).First();
                Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
                Color color = tokens.SelectToken("Color").ToObject<Color>();
                float width = tokens.SelectToken("Size.Width").ToObject<float>();
                float height = tokens.SelectToken("Size.Heigth").ToObject<float>();
                wall = new Wall(pos, new Vector2(width, height), color, level, game, false);
            }
            else
            {
                throw new InvalidOperationException("no level or game reference");
            }
            return wall;
        }

        /// <summary>
        /// This method will serialize a crate object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Wall wall = (Wall)value;
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
