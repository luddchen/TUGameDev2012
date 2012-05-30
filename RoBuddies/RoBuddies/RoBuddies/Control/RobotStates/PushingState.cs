using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control.RobotStates
{
    class PushingState : AnimatedState
    {
        public PushingState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToPulling(PullingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
