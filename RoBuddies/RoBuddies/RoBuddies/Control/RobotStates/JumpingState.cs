using System;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control.RobotStates
{
    class JumpingState : AnimatedState, IPartsCombinedTransition
    {
        public JumpingState(String name, Texture2D texture, StateMachine machine)
            : base(name, texture, machine)
        {
        }

        public void ToSeperated(State state)
        {
        }

        public void ToJumping(State state)
        {
            (StateMachine as PartsCombinedStateMachine).ToJumping(state);
        }

        public void ToPushing(State state)
        {
        }

        public void ToPulling(State state)
        {
        }

        public void ToWaiting(State state)
        {
        }

        public void ToWalking(State state)
        {
        }
    }
}
