﻿using System;
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
        private const float MOVEMENT_FORCE = 35000.0f;
        private State currentState;
        private PhysicObject physics;
        private Robot robot;

        protected bool seperated;

        private bool isOnGround = true;

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

        // TODO: calculate IsOnGround by the real distance to the ground under the robot
        // the myOnCollision and myOnSeperation method isn't working well
        public bool IsOnGround 
        {
            get { return isOnGround; }
            set { isOnGround = value; }
        }

        public RobotPart(ContentManager content, Vector2 pos, Robot robot, World world, PhysicObject physics)
        {
            TextureList = new List<Texture2D>();
            Position = pos;
            Scale = 1;
            Rotation = 0;
            effects = SpriteEffects.None;
            origin = new Vector2();
            Color = Color.White;
            CurrentState = State.Waiting;
            this.Physics = physics;
            this.Robot = robot;
        }

        public float MovementForce 
        {
            get { return MOVEMENT_FORCE; }
        }

        public State CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public Robot Robot
        {
            get { return robot; }
            set { robot = value; }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
