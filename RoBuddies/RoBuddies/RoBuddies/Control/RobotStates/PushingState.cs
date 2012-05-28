using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class PushingState : AnimatedState
    {
        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }

        public void ToPulling()
        {
            StateMachine.CurrentState = new PullingState();
        }
    }
}
