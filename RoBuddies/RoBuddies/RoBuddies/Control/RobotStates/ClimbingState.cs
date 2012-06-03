﻿using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control.RobotStates
{
    class ClimbingState : AnimatedState
    {
        public ClimbingState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
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
