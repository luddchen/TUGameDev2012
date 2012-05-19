using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;

namespace Robuddies.Objects
{
    class RobotPart : AnimatedObject
    {
        public enum State
        {
            Waiting,
            Walking,
            StartWalking,
            StopWalking,
            Jumping,
            StartJumping,
            StopJumping,
            Pushing,
            StartPushing,
            StopPushing
        };

        private State currentState;

        protected bool seperated;

        public RobotPart(ContentManager content, Vector2 pos, World world)
        {
            TextureList = new List<Texture2D>();
            Position = pos;
            Size = 1;
            Rotation = 0;
            effects = SpriteEffects.None;
            origin = new Vector2();
            Color = Color.White;
            CurrentState = State.Waiting;
        }

        public State CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public bool IsSeperated
        {
            get { return seperated; }
            set { seperated = value; }
        }
    }
}
