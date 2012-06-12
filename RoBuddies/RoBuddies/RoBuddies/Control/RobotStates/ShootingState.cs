using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace RoBuddies.Control.RobotStates
{
    class ShootingState : AnimatedState
    {
        private const float SHOOTING_FORCE = 1000;

        public ShootingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public override void Enter()
        {
            (StateMachine.Body as Body).ApplyForce(new Vector2(0, 1000));
        }

    }
}
