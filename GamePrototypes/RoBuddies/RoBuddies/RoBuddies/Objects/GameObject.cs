﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class GameObject
    {
        public Rectangle Destination;
        public float LayerDepth;

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
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        float size;
        public virtual float Width
        {
            get { return texture.Width * size; }
        }

        public virtual float Height
        {
            get { return texture.Height * size; }
        }

        public virtual float Size
        {
            get { return size; }
            set { size = value; }
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
            if (tex != null)
            {
                Texture = tex;
            }
        }

        private void init()
        {
            size = 1;
            rotation = 0;
            origin = new Vector2();
            color = Color.White;
            effects = SpriteEffects.None;
            Destination = new Rectangle();
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
            spriteBatch.Draw(Texture, Destination, null, Color, Rotation, origin, effects, LayerDepth);
        }
    }
}
