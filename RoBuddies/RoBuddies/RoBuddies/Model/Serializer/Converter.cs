using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Model.Serializer
{
    public abstract class Converter : JsonConverter
    {

        private String fullNameOfClass;

        public Converter(String fullNameOfClass)
        {
            this.fullNameOfClass = fullNameOfClass;
        }

        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            if (objectType.FullName.Equals(fullNameOfClass))
            {
                canConvert = true;
            }
            return canConvert;
        }

        public abstract override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

        public abstract override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);

    }
}
