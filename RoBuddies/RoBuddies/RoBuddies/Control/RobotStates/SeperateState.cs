using System;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class SeperateState : AnimatedState
    {
        public SeperateState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToCombine(CombineState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
