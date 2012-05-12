using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class GameObject
    {
        public Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { 
                texture = value;
                size.X = texture.Width;
                size.Y = texture.Height;
                origin = size / 2;
            }
        }

        Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        Vector2 size;
        public float Width
        {
            get { return size.X; }
            set { size.X = value; }
        }

        public float Height
        {
            get { return size.Y; }
            set { size.Y = value; }
        }

        public Vector2 Size
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
            size = new Vector2();
            rotation = 0;
            origin = new Vector2();
            color = Color.White;
            effects = SpriteEffects.None;
        }

        public GameObject(Texture2D tex, Vector2 pos)
        {
            position = pos;
            size = new Vector2();
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
