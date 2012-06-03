using System;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class CombineState : AnimatedState
    {
        public CombineState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
