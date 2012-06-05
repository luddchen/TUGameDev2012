using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RoBuddies.Model.Objects;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Serializer
{
    /// <summary>
    /// This class manages the serialization of a pipe.
    /// </summary>
    class PipeConverter : Converter
    {
        
        private Level level;
        private Game game;

        /// <summary>
        /// Creates a new PipeConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the
        /// level and game parameter.
        /// </summary>
        public PipeConverter()
            : base("RoBuddies.Model.Objects.Pipe")
        {
        }

        /// <summary>
        /// Creates a new PipeConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized pipe state will be added to</param>
        /// <param name="game">the game of the level</param>
        public PipeConverter(Level level, Game game)
            : base("RoBuddies.Model.Objects.Pipe")
        {
            this.level = level;
            this.game = game;
        }

        /// <summary>
        /// This method deserialize a pipe object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the level and contentManger parameters.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized pipe</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Pipe pipe = null;
            if (level != null && game != null)
            {
                JToken tokens = JObject.ReadFrom(reader).First();
                Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
                Color color = tokens.SelectToken("Color").ToObject<Color>();
                float width = tokens.SelectToken("Width").ToObject<float>();
                pipe = new Pipe(pos, width, color, level, game);
            }
            else
            {
                throw new InvalidOperationException("no level or game reference");
            }
            return pipe;
        }

        /// <summary>
        /// This method will serialize a pipe object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Pipe pipe = (Pipe)value;
            writer.WritePropertyName("Pipe");
            writer.WriteStartObject();
            writer.WritePropertyName("Position");
            serializer.Serialize(writer, pipe.Position);
            writer.WritePropertyName("Color");
            serializer.Serialize(writer, pipe.Color);
            writer.WritePropertyName("Width");
            serializer.Serialize(writer, pipe.Width);
            writer.WriteEndObject();
        }
    }
}
