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

        public PipeClimbingState(String name, List<Texture2D> textureList, Level level, StateMachine machine)
            : base(name, textureList, machine)
        {
            this.level = level;
        }

        public override void Enter()
        {
            FixedPrismaticJoint joint = JointFactory.CreateFixedPrismaticJoint(level, ((Body)this.StateMachine.Body), ((Body)this.StateMachine.Body).Position, new Vector2(0, 1));
            joint.MotorEnabled = true;
            joint.MaxMotorForce = 100;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
