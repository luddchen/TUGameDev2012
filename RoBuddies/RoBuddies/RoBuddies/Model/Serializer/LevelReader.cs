using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RoBuddies.Model.Serializer
{
    /// <summary>
    /// This class can be used for creating Level objects from
    /// serialized levels
    /// </summary>
    class LevelReader
    {

        /// <summary>
        /// creates a new LevelReader
        /// </summary>
        public LevelReader()
        {
            
        }

        /// <summary>
        /// reads a json level file and creates a Level object
        /// </summary>
        /// <param name="path">the path to the file</param>
        /// <param name="filename">the name of the file</param>
        /// <returns>returns the desirialized level as a Level object</returns>
        public Level readLevel(String path, String filename)
        {
            throw new NotImplementedException("will be implemented soon");
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
