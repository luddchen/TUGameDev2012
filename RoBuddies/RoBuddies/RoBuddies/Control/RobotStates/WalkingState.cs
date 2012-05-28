using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class WalkingState : AnimatedState
    {
        public void ToWaiting()
        {
            StateMachine.CurrentState = new WaitingState();
        }

        public void ToJumping()
        {
            StateMachine.CurrentState = new JumpingState();
        }
    }
}
