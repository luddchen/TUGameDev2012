using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class CombineState : AnimatedState
    {
        public CombineState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
