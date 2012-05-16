using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class AnimatedObject : GameObject
    {
        private List<Texture2D> textureList;

        protected float directionX;
        protected float directionY;

        public virtual float DirectionX 
        {
            set { directionX = value; }
            get { return directionX; }
        }

        public virtual float DirectionY
        {
            set { directionY = value; }
            get { return directionY; }
        }

        public List<Texture2D> TextureList
        {
            get { return textureList; }
            set { textureList = value; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
