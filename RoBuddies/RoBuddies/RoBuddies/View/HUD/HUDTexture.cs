using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            this.Height = 10;
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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(Width * Scale),
                    (int)(Height* Scale));
            spriteBatch.Draw(this.Texture,
                            dest,
                            null,
                            this.Color,
                            -this.Rotation,
                            this.Origin,
                            this.Effect,
                            0.0f);
        }

        /// <summary>
        /// testing intersection with point
        /// </summary>
        /// <param name="point">the test point</param>
        /// <returns>true if there is an intersetion</returns>
        public bool Intersects(Vector2 point)
        {
            if (Rotation != 0)
            {
                return false;
            }
            if (point.X < Position.X - Scale*Width/2 || point.X > Position.X + Scale*Width/2 ||
                point.Y < Position.Y - Scale*Height/2 || point.Y > Position.Y + Scale*Height/2)
            {
                return false;
            }
            return true;
        }
    }
}
