using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control.RobotStates
{
    class WalkingState : AnimatedState
    {
        public WalkingState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToJumping(JumpingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
