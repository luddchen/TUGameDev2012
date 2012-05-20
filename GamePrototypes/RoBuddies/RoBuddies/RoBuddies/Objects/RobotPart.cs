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
        // force for moving the robotPart
        private const float movementForce = 1000.0f;
        private State currentState;
        private PhysicObject physics;

        protected bool seperated;

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

        public RobotPart(ContentManager content, Vector2 pos, World world, PhysicObject physics)
        {
            TextureList = new List<Texture2D>();
            Position = pos;
            Size = 1;
            Rotation = 0;
            effects = SpriteEffects.None;
            origin = new Vector2();
            Color = Color.White;
            CurrentState = State.Waiting;
            this.Physics = physics;
            this.Physics.Destination = this.Destination;
            this.Physics.Position = this.Position;
        }

        public float MovementForce 
        {
            get { return movementForce; }
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

        public PhysicObject Physics
        {
            get { return physics; }
            set { physics = value; }
        }
    }
}
