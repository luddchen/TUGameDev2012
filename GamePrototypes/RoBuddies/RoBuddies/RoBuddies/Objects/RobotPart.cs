using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using Robuddies.Utilities;

namespace Robuddies.Objects
{
    public delegate void ActivateEventHandler(object sender, EventArgs e);

    class RobotPart : AnimatedObject
    {
        public event ActivateEventHandler Activate;

        // force for moving the robotPart
        private const float MOVEMENT_FORCE = 70000.0f;
        private State currentState;
        private PhysicObject physics;
        private Robot robot;

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
            StartPushing,
            Pushing,
            StopPushing,
        };

        public bool IsOnGround 
        {
            get {
                // the offset is really dirty, but this will allow better jumping at the moment
                float offsetX = 20f;
                return RaycastUtility.isIntesectingAnObject(robot.World, physics.Position + new Vector2(offsetX, 0f), physics.Position + new Vector2(offsetX, physics.Height / 10 + 1));
            }
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
            this.Physics.Body.Restitution = 0.0f;
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

        protected void OnActivate(EventArgs e)
        {
            if (Activate != null)
            {
                Activate(this, e);
            }
        }
    }
}
