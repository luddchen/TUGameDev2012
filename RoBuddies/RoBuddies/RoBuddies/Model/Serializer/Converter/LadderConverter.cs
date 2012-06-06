using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using RoBuddies.Model.Objects;
using Newtonsoft.Json.Linq;

namespace RoBuddies.Model.Serializer.Converter
{
    /// <summary>
    /// This class manages the serialization of a laddeer
    /// </summary>
    class LadderConverter : Converter
    {
        
        private Level level;
        private Game game;

        /// <summary>
        /// Creates a new LadderConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level and game parameter.
        /// </summary>
        public LadderConverter()
            : base("RoBuddies.Model.Objects.Ladder")
        {
        }

        /// <summary>
        /// Creates a new LadderConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized ladder state will be added to</param>
        /// <param name="game">the game of the level</param>
        public LadderConverter(Level level, Game game)
            : base("RoBuddies.Model.Objects.Ladder")
        {
            this.level = level;
            this.game = game;
        }

        /// <summary>
        /// This method deserialize a ladder object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level and game parameters.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized ladder</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Ladder ladder = null;
            if (level != null && game != null)
            {
                JToken tokens = JObject.ReadFrom(reader).First();
                Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
                Color color = tokens.SelectToken("Color").ToObject<Color>();
                float height = tokens.SelectToken("Heigth").ToObject<float>();
                ladder = new Ladder(pos, height, color, this.level, this.game);
            }
            else
            {
                throw new InvalidOperationException("no level or game reference");
            }
            return ladder;
        }

        /// <summary>
        /// This method will serialize a ladder object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Ladder ladder = (Ladder)value;
            writer.WritePropertyName("Ladder");
            writer.WriteStartObject();
            writer.WritePropertyName("Position");
            serializer.Serialize(writer, ladder.Position);
            writer.WritePropertyName("Color");
            serializer.Serialize(writer, ladder.Color);
            writer.WritePropertyName("Heigth");
            serializer.Serialize(writer, ladder.Height);
            writer.WriteEndObject();
        }
    }
}
