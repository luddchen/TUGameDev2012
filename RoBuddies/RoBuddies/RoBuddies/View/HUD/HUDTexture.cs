using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// a Head Up Display Texture
    /// </summary>
    class HUDTexture : IHUDElement
    {
        private Texture2D texture;

        /// <summary>
        /// name of this element
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// texture of this element
        /// </summary>
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

        /// <summary>
        /// origin of the elements texture
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// local position of this element 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// width of this element
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// height of this element
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// rotation of this element
        /// </summary>
        public float Rotation { get; set; }

        public SpriteEffects Effect { get; set; }

        /// <summary>
        /// color of this element
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// scale of this element
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// constructs a Head Up Display Element Texture
        /// </summary>
        /// <param name="content"></param>
        public HUDTexture(ContentManager content)
        {
            this.Position = Vector2.Zero;
            this.Color = Color.White;
            this.Texture = content.Load<Texture2D>("Sprites//Circle");
            this.Width = 10;
            this.Scale = 1.0f;
        }

        /// <summary>
        /// for update of values and effects 
        /// </summary>
        /// <param name="gameTime">the gametime</param>
        public void Update(GameTime gameTime){}


        /// <summary>
        /// draw this element
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture,
                            this.Position,
                            null,
                            this.Color,
                            -this.Rotation,
                            this.Origin,
                            this.Scale * this.Width / this.Texture.Width,
                            this.Effect,
                            0.0f);
        }
    }
}
