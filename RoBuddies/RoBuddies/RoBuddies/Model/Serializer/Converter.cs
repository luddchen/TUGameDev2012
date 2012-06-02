using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Model.Serializer
{
    /// <summary>
    /// This is a abstract class for all converters, which will serialize 
    /// and deserialize a level and its objects with the JsonConverter.
    /// </summary>
    public abstract class Converter : JsonConverter
    {

        private String fullNameOfClass;

        /// <summary>
        /// Creates a new converter
        /// </summary>
        /// <param name="fullNameOfClass">The full name of the class, which the converter converts. E.g.: RoBuddies.Model.Layer</param>
        public Converter(String fullNameOfClass)
        {
            this.fullNameOfClass = fullNameOfClass;
        }

        /// <summary>
        /// This method will be called from the json.net framework to test, 
        /// if this converter can convert the current object type
        /// </summary>
        /// <param name="objectType">The type of the object, which should be converted</param>
        /// <returns>true if the full name of the object type is equal to the full name of the class the converter converts</returns>
        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            if (objectType.FullName.Equals(fullNameOfClass))
            {
                canConvert = true;
            }
            return canConvert;
        }

        /// <summary>
        /// This method will deserialize json code to an object.
        /// It will be called from the json.net framwork, if needed.
        /// </summary>
        /// <param name="reader">the reader which reads the json code</param>
        /// <param name="objectType">the existing value of object being read</param>
        /// <param name="existingValue">the existing value of object being read</param>
        /// <param name="serializer">the calling serializer</param>
        /// <returns>the deserialized object</returns>
        public abstract override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

        /// <summary>
        /// This method will serialize an object and generate json code.
        /// </summary>
        /// <param name="writer">the writer for the json output</param>
        /// <param name="value">the object which will be serialized</param>
        /// <param name="serializer">the calling serializer</param>
        public abstract override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);

    }
}
