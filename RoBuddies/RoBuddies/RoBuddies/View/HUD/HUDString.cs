using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// a Head Up Display String
    /// </summary>
    class HUDString : IHUDElement
    {
        protected SpriteFont font;

        private Vector2 measureString;
        private Color BackgroundColor;
        private Texture2D BackgroundTexture;
        private Vector2 BackgroundTextureOrigin;

        /// <summary>
        /// name of this element
        /// </summary>
        public String Name { get; set; }

        public String String { get; set; }

        /// <summary>
        /// local position of this element 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// color of this element
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// scale of this element
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// get width of this element
        /// </summary>
        public float Width
        {
            get { return this.MeasureString.X * this.Scale; }
            set { }
        }

        /// <summary>
        /// get height of this element
        /// </summary>
        public float Height
        {
            get { return this.MeasureString.Y * this.Scale; }
            set { }
        }

        /// <summary>
        /// rotation of this element
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// if this element visible or not
        /// </summary>
        public bool isVisible { get; set; }

        /// <summary>
        /// size of unscaled String
        /// </summary>
        protected Vector2 MeasureString
        {
            get 
            {
                this.measureString = this.font.MeasureString(this.String);
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
            this.isVisible = true;
        }

        public HUDString(String text, ContentManager content)
        {
            this.font = content.Load<SpriteFont>("Fonts\\Linds");
            this.Position = Vector2.Zero;
            this.String = text;
            this.Color = Color.Beige;
            this.Scale = 1.0f;
            this.isVisible = true;
        }

        public HUDString(String text, SpriteFont font, Vector2? position, Color? color, Color? backgroundColor, float? scale, float? rotation, ContentManager content)
        {
            if (text == null) { this.String = " "; }
            if (text != null) { this.String = text; }

            if (font == null) { this.font = content.Load<SpriteFont>("Fonts\\Linds"); }
            if (font != null) { this.font = font; }

            this.Position = position ?? Vector2.Zero;
            this.Color = color ?? Color.Beige;
            this.Scale = scale ?? 1.0f;
            this.Rotation = rotation ?? 0.0f;
            this.isVisible = true;

            if (backgroundColor != null) 
            { 
                this.BackgroundColor = (Color)backgroundColor;
                this.BackgroundTexture = content.Load<Texture2D>("Sprites\\SquareRound");
                this.BackgroundTextureOrigin = new Vector2(BackgroundTexture.Width/2, BackgroundTexture.Height/2);
            }
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
            if (isVisible)
            {
                if (this.BackgroundTexture != null)
                {
                    Rectangle dest = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.Width * 1.2f), (int)this.Height);
                    spriteBatch.Draw(this.BackgroundTexture, dest, null, this.BackgroundColor, -this.Rotation, this.BackgroundTextureOrigin, SpriteEffects.None, 1.0f);
                }
                spriteBatch.DrawString(this.font, this.String, this.Position, this.Color, -this.Rotation, this.MeasureString / 2, this.Scale, SpriteEffects.None, 0.0f);
            }
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
            if (point.X < Position.X - Width / 2 || point.X > Position.X + Width / 2 ||
                point.Y < Position.Y - Height / 2 || point.Y > Position.Y + Height / 2)
            {
                return false;
            }
            return true;
        }
    }
}
