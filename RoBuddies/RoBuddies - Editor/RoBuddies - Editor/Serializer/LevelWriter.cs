using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using RoBuddies.Model;

namespace RoBuddies___Editor.Serializer
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
        /// !currently not implemented!
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="filename">the name of the file</param>
        public void writeLevel(String path, String filename) 
        {
            string output = JsonConvert.SerializeObject(this.level);
            Console.Out.WriteLine(output);
            Level deserializedLevel = JsonConvert.DeserializeObject<Level>(output);
        }
    }
}
