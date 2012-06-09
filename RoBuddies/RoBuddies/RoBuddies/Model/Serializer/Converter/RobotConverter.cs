using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.Model.Serializer.Converter
{
    /// <summary>
    /// This class manages the serialization of the Robot.
    /// </summary>
    class RobotConverter : Converter
    {
        
        private Game game;
        private Level level;
        private ContentManager content;

        /// <summary>
        /// Creates a new RobotConverter. If you only want to generate json and not deserialize json code,
        /// then you can use this constructer. For deserialization you have to use the constructor with the 
        /// game, level and content parameter.
        /// </summary>
        public RobotConverter()
            : base("RoBuddies.Model.Robot")
        {
        }

        /// <summary>
        /// Creates a new RobotConverter, which can generate json
        /// </summary>
        /// <param name="level">a level object where the deserialized robot state will be added to</param>
        /// <param name="game">the content of the game</param>
        /// <param name="content">the content of the game</param>

        public RobotConverter(Level level, Game game, ContentManager content)
            : base("RoBuddies.Model.Robot")
        {
            this.game = game;
            this.level = level;
            this.content = content;
        }

        /// <summary>
        /// This method deserialize a robot object from the generated json code. In order to use this method,
        /// you needed to use the constructor with the game, level and contentManger parameters.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized robot</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Robot robot = null;
            if (level != null && game != null && content != null)
            {
                JToken tokens = JObject.ReadFrom(reader).SelectToken("Robot");
                if (tokens != null)
                {
                    Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
                    robot = new Robot(content, pos, level, game);
                }
            }
            else
            {
                throw new InvalidOperationException("no level, game or content reference");
            }
            return robot;
        }

        /// <summary>
        /// This method will serialize a robot object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Robot robot = (Robot)value;
            writer.WritePropertyName("Robot");
            writer.WriteStartObject();
            writer.WritePropertyName("Position");
            serializer.Serialize(writer, robot.ActivePart.Position);
            writer.WriteEndObject();
        }
    }
}
