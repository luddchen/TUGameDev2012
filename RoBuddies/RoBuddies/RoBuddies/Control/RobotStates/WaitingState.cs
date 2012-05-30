using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control.RobotStates
{
    class WaitingState : AnimatedState
    {
        public WaitingState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToJumping(JumpingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToWalking(WalkingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToPushing(PushingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToPulling(PullingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToSeperate(SeperateState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToClimbing(ClimbingState state)
        {
            StateMachine.CurrentState = state;
        }
    }
}
