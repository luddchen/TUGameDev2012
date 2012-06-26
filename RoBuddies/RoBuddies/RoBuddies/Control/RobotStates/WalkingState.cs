using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using RoBuddies.Model.RobotParts;

namespace RoBuddies.Control.RobotStates
{
    class WalkingState : AnimatedState
    {
        private const int START_WALKING = 5;
        private const int STOP_WALKING = 24;

        private const float force = 100;
        private const float velocityLimit = 3;
        private const float motorSpeed = -15;

        private float currentTextureIndex;
        private Body body;

        public WalkingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
            body = machine.Body as Body;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateWalkAnimation(gameTime);
        }

        private void UpdateWalkAnimation(GameTime gameTime)
        {
            if (currentTextureIndex < START_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            if (currentTextureIndex > STOP_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
            currentTextureIndex += 0.8f;
        }

        public static void joinMovement(Model.PhysicObject body, FarseerPhysics.Dynamics.Joints.RevoluteJoint motor, bool isOnGround, Model.Direction direction)
        {
            int dir = 1;
            if (direction == Model.Direction.left) { dir = -1; }
            if (!isOnGround)
            {
                motor.MotorSpeed = 0f;
                body.ApplyForce(new Vector2(dir * force, 0));
                if (Math.Abs(body.LinearVelocity.X) > Math.Abs(velocityLimit))
                {
                    body.LinearVelocity = new Vector2(dir * velocityLimit, body.LinearVelocity.Y);
                }
            }
            else
            {
                motor.MotorSpeed = dir * motorSpeed;
            }

        }

        public static void stopMovement(Model.PhysicObject body, FarseerPhysics.Dynamics.Joints.RevoluteJoint motor)
        {
            body.LinearVelocity = new Vector2(0, body.LinearVelocity.Y);
            motor.MotorSpeed = 0f;
        }

    }
}
