using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using RoBuddies.Model;
using FarseerPhysics.Dynamics.Joints;
using RoBuddies.Utilities;
using RoBuddies.Model.Objects;

namespace RoBuddies.Control.RobotStates
{
    class PipeClimbingState : AnimatedState
    {
        private const float MAX_MOTOR_FORCE = 100000f;

        private const int START_CLIMBING = 10; // 90 - 80
        private const int STOP_CLIMBING = 20;  // 100 - 80
        private float currentTextureIndex;

        private Level level;
        private FixedPrismaticJoint prismaticJoint;
        private FixedFrictionJoint frictionJoint;

        public PipeClimbingState(String name, List<Texture2D> textureList, Level level, StateMachine machine)
            : base(name, textureList, machine)
        {
            this.level = level;
        }

        public bool IsMoving { get; set; }

        public override void Enter()
        {
            frictionJoint = JointFactory.CreateFixedFrictionJoint(level, (Body)this.StateMachine.Body, this.StateMachine.Body.Position);
            prismaticJoint = JointFactory.CreateFixedPrismaticJoint(level, ((Body)this.StateMachine.Body), this.StateMachine.Body.Position, new Vector2(0, 1));
            prismaticJoint.MotorEnabled = true;
            prismaticJoint.MaxMotorForce = MAX_MOTOR_FORCE;
        }

        public override void Exit()
        {
            this.level.RemoveJoint(prismaticJoint);
            this.level.RemoveJoint(frictionJoint);
            (this.StateMachine.Body as Body).LinearVelocity = new Vector2(0, -3);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
            {
                currentTextureIndex = START_CLIMBING;
                StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
                (this.StateMachine.Body as Body).LinearVelocity = new Vector2(0, (this.StateMachine.Body as Body).LinearVelocity.Y);
            }
        }

        public void UpdateClimbAnimation(GameTime gameTime)
        {
            IsMoving = true;

            if (currentTextureIndex < START_CLIMBING)
            {
                currentTextureIndex = START_CLIMBING;
            }

            if (currentTextureIndex > STOP_CLIMBING)
            {
                currentTextureIndex = START_CLIMBING;
            }

            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
            currentTextureIndex += 0.7f;
        }
    }
}
