
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing objects without physical behavior
    /// </summary>
    class StaticObject : IBody
    {
        private Texture2D texture;

        public bool IsVisible
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public SpriteEffects Effect
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            }
        }

        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public Layer Layer
        {
            get;
            set;
        }

        /// <summary>
        /// creates an game object whithout physical behavier
        /// </summary>
        public StaticObject()
        {
            this.Color = Color.White;
            this.Height = 1;
            this.Width = 1;
            this.Effect = SpriteEffects.None;
            this.Rotation = 0;
            this.Position = new Vector2(0, 0);
            this.IsVisible = true;
        }

        /// <summary>
        /// creates an game object whithout physical behavier
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        public StaticObject(Texture2D texture, Vector2? position, float? width, float? height, Color? color, float? rotation)
        {
            this.Texture = texture;
            this.Position = position ?? Vector2.Zero;
            this.Width = width ?? 1;
            this.Height = height ?? 1;
            this.Color = color ?? Color.White;
            this.Rotation = rotation ?? 0.0f;
            this.IsVisible = true;
        }

    }
}
