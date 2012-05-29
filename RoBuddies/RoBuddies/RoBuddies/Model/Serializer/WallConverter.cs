using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RoBuddies.Model.Objects;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.Model.Serializer
{
    public class WallConverter : Converter
    {

        private Level level;
        private ContentManager content;
        private Texture2D defaultTexture;

        public WallConverter()
            : base("RoBuddies.Model.Objects.Wall")
        {
        }

        public WallConverter(Level level, ContentManager content)
            : base("RoBuddies.Model.Objects.Wall")
        {
            this.level = level;
            this.content = content;
            this.defaultTexture = content.Load<Texture2D>("Sprites//Square");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken tokens = JObject.ReadFrom(reader).First();
            Vector2 pos = tokens.SelectToken("Position").ToObject<Vector2>();
            Color color = tokens.SelectToken("Color").ToObject<Color>();
            float width = tokens.SelectToken("Size.Width").ToObject<float>();
            float height = tokens.SelectToken("Size.Heigth").ToObject<float>();
            
            Wall wall = new Wall(pos, new Vector2(width, height), color, defaultTexture, level);
            return wall;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Wall wall = (Wall)value;
            writer.WritePropertyName("Wall");
            writer.WriteStartObject();
                writer.WritePropertyName("Position");
                serializer.Serialize(writer, wall.Position);
                writer.WritePropertyName("Color");
                serializer.Serialize(writer, wall.Color);
                writer.WritePropertyName("Size");
                writer.WriteStartObject();
                    writer.WritePropertyName("Width");
                    serializer.Serialize(writer, wall.Width);
                    writer.WritePropertyName("Heigth");
                    serializer.Serialize(writer, wall.Height);
                writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}
