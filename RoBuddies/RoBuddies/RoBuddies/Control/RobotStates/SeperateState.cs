using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

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
