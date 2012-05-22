using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class GameObject
    {
        //public Rectangle Destination;
        //public float LayerDepth;

        protected Texture2D texture;

        public virtual Texture2D Texture
        {
            get { return texture; }
            set { 
                texture = value;
                origin.X = texture.Width / 2;
                origin.Y = texture.Height / 2;
            }
        }

        Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Vector2 origin;

        Vector2 position;
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        float scale;
        public virtual float Width
        {
            get {
                if (texture != null)
                {
                    return texture.Width * scale;
                }
                else
                {
                    return 0;
                }
            }
        }

        public virtual float Height
        {
            get {
                if (texture != null)
                {
                    return texture.Height * scale;
                }
                else
                {
                    return 0;
                }
            }
        }

        public virtual float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        float rotation;
        public virtual float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public SpriteEffects effects;

        public GameObject() 
        {
            init();
            position = new Vector2();
        }

        public GameObject(Texture2D tex, Vector2 pos)
        {
            init();
            position = pos;
            if (tex != null) {
                Texture = tex;
            }
        }

        private void init()
        {
            scale = 1;
            rotation = 0;
            origin = new Vector2();
            color = Color.White;
            effects = SpriteEffects.None;
            //Destination = new Rectangle();
        }

        public virtual void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("test");
            spriteBatch.Draw(Texture, Position, null, Color, 0, origin, Scale, effects, 0);
        }
    }
}
