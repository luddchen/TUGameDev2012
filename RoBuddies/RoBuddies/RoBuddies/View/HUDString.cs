using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.View
{
    class HUDString : IHUDElement
    {
        protected SpriteFont font;

        private Vector2 measureString;
         
        public String Name { get; set; }

        public String String { get; set; }

        public Vector2 Position { get; set; }

        public Color Color { get; set; }

        public float Scale { get; set; }

        public float Width
        {
            get { return this.MeasureString.X * this.Scale; }
            set { } 
        }

        public float Height
        {
            get { return this.MeasureString.Y * this.Scale; }
            set { }
        }

        public Vector2 MeasureString
        {
            get 
            { 
                this.measureString = this.font.MeasureString(this.String) * this.Scale;
                return this.measureString;
            }
        }

        public HUDString(ContentManager content)
        {
            this.font = content.Load<SpriteFont>("Fonts\\Linds");
            this.Position = Vector2.Zero;
            this.String = "RoBuddies";
            this.Color = Color.Beige;
            this.Scale = 1.0f;
        }

        public HUDString(String text, ContentManager content)
        {
            this.font = content.Load<SpriteFont>("Fonts\\Linds");
            this.Position = Vector2.Zero;
            this.String = text;
            this.Color = Color.Beige;
            this.Scale = 1.0f;
        }

        public void Update(GameTime gameTime){}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font, this.String, this.Position, this.Color, 0, this.MeasureString / 2, this.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
