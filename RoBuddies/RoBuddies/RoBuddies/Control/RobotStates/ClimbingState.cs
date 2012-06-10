using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class ClimbingState : AnimatedState
    {
        public ClimbingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public void ToWaiting(State state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToFalling(State state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
