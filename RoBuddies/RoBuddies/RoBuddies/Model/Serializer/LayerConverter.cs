using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Model.Serializer
{
    public class LayerConverter : Converter
    {

        public LayerConverter()
            : base("RoBuddies.Model.Layer")
        {
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            Layer layer = (Layer)value;
            writer.WriteStartObject();
            writer.WritePropertyName("Layer");
                writer.WriteStartObject();
                writer.WritePropertyName("Walls");
                foreach (IBody obj in layer.AllObjects)
                {
                    serializer.Serialize(writer, obj);
                }
                writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}
