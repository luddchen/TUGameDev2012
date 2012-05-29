using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing objects without physical behavior
    /// </summary>
    public class StaticObject : IBody
    {


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

        public Microsoft.Xna.Framework.Graphics.SpriteEffects Effect
        {
            get;
            set;
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get;
            set;
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

        public Level World
        {
            get;
            set;
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
