using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class CombineState : AnimatedState
    {
        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }
    }
}
