using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates
{
    class WaitingState : AnimatedState
    {
        public void ToJumping()
        {
            StateMachine.CurrentState = new JumpingState();
        }

        public void ToWalking()
        {
            StateMachine.CurrentState = new WalkingState();
        }

        public void ToPushing()
        {
            StateMachine.CurrentState = new PushingState();
        }

        public void ToPulling()
        {
            StateMachine.CurrentState = new PushingState();
        }

        public void ToSeperate()
        {
            StateMachine.CurrentState = new SeperateState();
        }

        public void ToClimbing()
        {
            StateMachine.CurrentState = new ClimbingState();
        }
    }
}
