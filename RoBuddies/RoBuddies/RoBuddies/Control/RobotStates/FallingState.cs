using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class FallingState : AnimatedState
    {
        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }

        public void ToCombine()
        {
            StateMachine.CurrentState = new CombineState();
        }
    }
}
