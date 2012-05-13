using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class AnimatedObject : GameObject
    {

        public enum State { Waiting, Walking, StartWalking, StopWalking, Jumping, StartJumping, StopJumping };
        public State state;

        public float directionX;
        public virtual float DirectionX 
        {
            set { directionX = value; }
            get { return directionX; }
        }

        public float directionY;
        public virtual float DirectionY
        {
            set { directionY = value; }
            get { return directionY; }
        }

        public List<Texture2D> textureList;

        public virtual void Update(GameTime gameTime)
        {
        }

    }
}
