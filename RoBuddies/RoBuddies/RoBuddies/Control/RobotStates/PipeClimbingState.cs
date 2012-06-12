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
        private Level level;
        private FixedPrismaticJoint prismaticJoint;
        private FixedFrictionJoint frictionJoint;

        public PipeClimbingState(String name, List<Texture2D> textureList, Level level, StateMachine machine)
            : base(name, textureList, machine)
        {
            this.level = level;
        }

        public override void Enter()
        {
            frictionJoint = JointFactory.CreateFixedFrictionJoint(level, (Body)this.StateMachine.Body, this.StateMachine.Body.Position);
            prismaticJoint = JointFactory.CreateFixedPrismaticJoint(level, ((Body)this.StateMachine.Body), this.StateMachine.Body.Position, new Vector2(0, 1));
            prismaticJoint.MotorEnabled = true;
            prismaticJoint.MaxMotorForce = 1000;
        }

        public override void Exit()
        {
            this.level.RemoveJoint(prismaticJoint);
            this.level.RemoveJoint(frictionJoint);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
