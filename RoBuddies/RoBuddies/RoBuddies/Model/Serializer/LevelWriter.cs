using System;
using System.IO;
using Newtonsoft.Json;
using RoBuddies.Model.Serializer.Converter;


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
            if(!File.Exists(@path)) {
                System.IO.Directory.CreateDirectory(path);
            }
            if (filename.Length == ".json".Length)
            {
                filename = DateTime.Now.ToLongTimeString().Replace(":", "_") + ".json";
            }
            Console.Out.WriteLine(filename);
            StreamWriter sw = new StreamWriter(@path + "\\" + filename, false);
            JsonWriter writer = new JsonTextWriter(sw);
            JsonSerializer serializer = new JsonSerializer();
            // add your converter of the level objects here:
            serializer.Converters.Add(new LevelConverter());
            serializer.Converters.Add(new LayerConverter());
            serializer.Converters.Add(new WallConverter());
            serializer.Converters.Add(new CrateConverter());
            serializer.Converters.Add(new PipeConverter());
            serializer.Converters.Add(new LadderConverter());
            serializer.Converters.Add(new RobotConverter());
            serializer.Serialize(writer, this.level);
            writer.Flush();
            writer.Close();
        }
    }
}
