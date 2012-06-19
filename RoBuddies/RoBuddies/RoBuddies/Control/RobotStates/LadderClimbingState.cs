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
        
        // this texture numbers works only for upper part ... for combined climbing use 81 up to 90 but it is possible that this textures are heigher than walking textures
        private const int START_CLIMBING = 20; // 100 - 80
        private const int STOP_CLIMBING = 30;  // 110 - 80
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
            prismaticJoint = JointFactory.CreateFixedPrismaticJoint(level, ((Body)this.StateMachine.Body), this.StateMachine.Body.Position, new Vector2(0, -f));
            prismaticJoint.MotorEnabled = true;
            prismaticJoint.MaxMotorForce = MAX_MOTOR_FORCE;
        }

        public override void Exit()
        {
            this.level.RemoveJoint(prismaticJoint);
            this.level.RemoveJoint(frictionJoint);
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
            {
                currentTextureIndex = START_CLIMBING;
                StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
                (this.StateMachine.Body as Body).LinearVelocity = new Vector2( (this.StateMachine.Body as Body).LinearVelocity.X, 0);
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
