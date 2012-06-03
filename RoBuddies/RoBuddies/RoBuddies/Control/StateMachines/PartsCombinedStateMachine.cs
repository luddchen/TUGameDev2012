using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Control.RobotStates;
using RoBuddies.Control.RobotStates.Interfaces;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine, IPartsCombinedTransition
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private KeyboardState oldState;
        private Game game;

        public PartsCombinedStateMachine(IBody body, Game game)
            : base(body)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && !(CurrentState is JumpingState))
            {
                ToJumping(CurrentState);
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                SwitchToState(WALK_STATE);
                (Body as Body).LinearVelocity = new Vector2(-2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.FlipHorizontally;
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                SwitchToState(WALK_STATE);
                (Body as Body).LinearVelocity = new Vector2(2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.None;
            }

            oldState = newState;
        }

        #region IPartsCombinedTransition Member

        public void ToSeperated(State state)
        {
            throw new NotImplementedException();
        }

        public void ToJumping(State state)
        {
            Console.WriteLine("Jump!");
            SwitchToState(JUMP_STATE);
            ((Body)Body).ApplyForce(new Vector2(0, 1500));
        }

        public void ToPushing(State state)
        {
            throw new NotImplementedException();
        }

        public void ToPulling(State state)
        {
            throw new NotImplementedException();
        }

        public void ToWaiting(State state)
        {
            throw new NotImplementedException();
        }

        public void ToWalking(State state)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
