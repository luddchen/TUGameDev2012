using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using RoBuddies.Model.Serializer.Converter;

namespace RoBuddies.Model.Serializer
{
    /// <summary>
    /// This class can be used for creating Level objects from
    /// serialized levels
    /// </summary>
    class LevelReader
    {

        private ContentManager content;

        private Level level;
        private Game game;

        /// <summary>
        /// creates a new LevelReader
        /// <param name="game">the game object (will be needed for some deserialization)</param>
        /// </summary>
        public LevelReader(Game game)
        {
            this.content = game.Content;
            this.level = new Level();
            this.game = game;
        }

        /// <summary>
        /// reads a json level file and creates a Level object
        /// </summary>
        /// <param name="path">the path to the file</param>
        /// <param name="filename">the name of the file</param>
        /// <returns>returns the desirialized level as a Level object</returns>
        public Level readLevel(String path, String filename)
        {
            Level loadedLevel = null;
            if (File.Exists(@path + "\\" + filename))
            {
                StreamReader sr = new StreamReader(@path + "\\" + filename);
                JsonReader reader = new JsonTextReader(sr);
                JsonSerializer serializer = new JsonSerializer();
                // add your converter of the level objects here:
                serializer.Converters.Add(new LevelConverter(this.level));
                serializer.Converters.Add(new LayerConverter(this.level));
                serializer.Converters.Add(new WallConverter(this.level, this.game));
                serializer.Converters.Add(new CrateConverter(this.level, this.game));
                serializer.Converters.Add(new PipeConverter(this.level, this.game));
                serializer.Converters.Add(new LadderConverter(this.level, this.game));
                serializer.Converters.Add(new RobotConverter(this.level, this.game, this.content));
                loadedLevel = serializer.Deserialize<Level>(reader);
                reader.Close();
            }
            return loadedLevel;
        }

        /// <summary>
        /// creates a level object from a json level string
        /// </summary>
        /// <param name="levelString">the serialized level as a json level string</param>
        /// <returns>returns the desirialzed level as a Level object</returns>
        public Level readLevel(String levelString)
        {
            Level deserializedLevel = JsonConvert.DeserializeObject<Level>(levelString);
            return deserializedLevel;
        }
    }
}
