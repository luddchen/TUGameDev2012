using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Control.RobotStates
{
    class PipeClimbingState : AnimatedState
    {
        public PipeClimbingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Console.Out.WriteLine("asdf");
            (this.StateMachine.Body as Body).LinearVelocity = new Vector2((this.StateMachine.Body as Body).LinearVelocity.X, 0);
        }
    }
}
