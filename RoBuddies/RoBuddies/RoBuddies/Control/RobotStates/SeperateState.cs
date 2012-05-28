using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class SeperateState : AnimatedState
    {
        public void ToCombine()
        {
            StateMachine.CurrentState = new CombineState();
        }

        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }
    }
}
