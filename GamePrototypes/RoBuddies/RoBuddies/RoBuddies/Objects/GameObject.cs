using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class GameObject
    {
        public Texture2D texture;
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
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        float size;
        public float Width
        {
            get { return texture.Width * size; }
        }

        public float Height
        {
            get { return texture.Height * size; }
        }

        public float Size
        {
            get { return size; }
            set { size = value; }
        }

        float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public SpriteEffects effects;

        public GameObject() 
        {
            position = new Vector2();
            size = 1;
            rotation = 0;
            origin = new Vector2();
            color = Color.White;
            effects = SpriteEffects.None;
        }

        public GameObject(Texture2D tex, Vector2 pos)
        {
            position = pos;
            size = 1;
            rotation = 0;
            origin = new Vector2();
            color = Color.White;
            effects = SpriteEffects.None;
            Texture = tex;
        }

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

    }
}
