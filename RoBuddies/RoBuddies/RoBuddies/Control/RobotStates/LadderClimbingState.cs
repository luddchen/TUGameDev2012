using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Control.StateMachines;
using RoBuddies.Model;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;

namespace RoBuddies.Control.RobotStates
{
    class LadderClimbingState : AnimatedState
    {
        private const float MAX_MOTOR_FORCE = 1000f;
        
        // for lower part the right texture numbers are 20-29
        private const int START_CLIMBING = 80; // 100 - 80
        private const int STOP_CLIMBING = 89;  // 110 - 80
        private float currentTextureIndex;

        private Level level;
        private FixedPrismaticJoint prismaticJoint;
        private FixedFrictionJoint frictionJoint;

        public bool IsMoving { get; set; }

        public LadderClimbingState(String name, List<Texture2D> textureList, Level level, StateMachine machine)
            : base(name, textureList, machine)
        {
            this.level = level;
        }

        public override void Enter()
        {
            frictionJoint = JointFactory.CreateFixedFrictionJoint(level, (Body)this.StateMachine.Body, this.StateMachine.Body.Position);
            prismaticJoint = JointFactory.CreateFixedPrismaticJoint(level, ((Body)this.StateMachine.Body), this.StateMachine.Body.Position, new Vector2(0, -1f));
            prismaticJoint.MotorEnabled = true;
            prismaticJoint.MaxMotorForce = MAX_MOTOR_FORCE;
            StateMachine.Body.Width = 2.5f;
            StateMachine.Body.Height = 3.6f;
        }

        public override void Exit()
        {
            this.level.RemoveJoint(prismaticJoint);
            this.level.RemoveJoint(frictionJoint);
            StateMachine.Body.Width = 3f;
            StateMachine.Body.Height = 3f;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
            {
                currentTextureIndex = STOP_CLIMBING;
                StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
                (this.StateMachine.Body as Body).LinearVelocity = new Vector2( (this.StateMachine.Body as Body).LinearVelocity.X, 0);
            }

            if (IsMoving)
            {
                UpdateClimbAnimation(gameTime);
            }
        }

        private void UpdateClimbAnimation(GameTime gameTime)
        {

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
