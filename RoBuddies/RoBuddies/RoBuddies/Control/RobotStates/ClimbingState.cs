﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class ClimbingState : AnimatedState
    {
        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }

        public void ToFalling()
        {
            StateMachine.CurrentState = new FallingState();
        }
    }
}
