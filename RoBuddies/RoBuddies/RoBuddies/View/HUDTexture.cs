using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.View
{
    class HUDTexture : IHUDElement
    {
        private Texture2D texture;
         
        public String Name { get; set; }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            set
            {
                this.texture = value;
                this.Origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
            }
        }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Rotation { get; set; }

        public SpriteEffects Effect { get; set; }

        public Color Color { get; set; }

        public HUDTexture(ContentManager content)
        {
            this.Position = Vector2.Zero;
            this.Color = Color.White;
            this.Texture = content.Load<Texture2D>("Sprites//Circle");
            this.Width = 20;
        }

        public void Update(GameTime gameTime){}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture,
                            this.Position,
                            null,
                            this.Color,
                            -this.Rotation,
                            this.Origin,
                            this.Width / this.Texture.Width,
                            this.Effect,
                            0.0f);
        }
    }
}
