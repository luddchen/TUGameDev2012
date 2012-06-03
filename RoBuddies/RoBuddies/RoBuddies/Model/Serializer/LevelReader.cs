using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

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

        /// <summary>
        /// creates a new LevelReader
        /// <param name="content">content manager for loading default textures</param>
        /// <param name="level">the level which will be filled with the loaded objects</param>
        /// </summary>
        public LevelReader(ContentManager content)
        {
            this.content = content;
            this.level = new Level(Vector2.Zero);
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
                serializer.Converters.Add(new LevelConverter(level));
                serializer.Converters.Add(new LayerConverter(level));
                serializer.Converters.Add(new WallConverter(level, content));
                serializer.Converters.Add(new CrateConverter(level, content));
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
