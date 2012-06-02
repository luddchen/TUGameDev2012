using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using RoBuddies.Model;
using System.IO;

using RoBuddies.Model.Serializer;

namespace RoBuddies.Model.Serializer  
{
    /// <summary>
    /// This class is used to write a level of the level editor
    /// into a Json file.
    /// </summary>
    class LevelWriter
    {
        private Level level;

        /// <summary>
        /// creates a new LevelWriter for a level
        /// </summary>
        /// <param name="level">The Level, which will be serialized</param>
        public LevelWriter(Level level)
        {
            this.level = level;
        }

        /// <summary>
        /// writes a level into a json file
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="filename">the name of the file</param>
        public void writeLevel(String path, String filename) 
        {
            StreamWriter sw = new StreamWriter(@".\\SerializationTest.json", false);
            JsonWriter writer = new JsonTextWriter(sw);
            JsonSerializer serializer = new JsonSerializer();
            // add your converter of the level objects here:
            serializer.Converters.Add(new LevelConverter());
            serializer.Converters.Add(new LayerConverter());
            serializer.Converters.Add(new WallConverter());
            serializer.Serialize(writer, this.level);
            writer.Flush();
            writer.Close();
        }
    }
}
