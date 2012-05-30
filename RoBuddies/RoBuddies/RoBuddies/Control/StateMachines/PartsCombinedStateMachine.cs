using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Control.RobotStates;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private KeyboardState oldState;

        public PartsCombinedStateMachine(IBody body)
            : base(body)
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                Console.WriteLine("Jump!");
                SwitchToState(JUMP_STATE);
                ((Body)Body).ApplyForce(new Vector2(0, -1500));
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                SwitchToState(WALK_STATE);
                ((Body)Body).LinearVelocity = new Vector2(-2, ((Body)Body).LinearVelocity.Y);
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                SwitchToState(WALK_STATE);
                ((Body)Body).LinearVelocity = new Vector2(2, ((Body)Body).LinearVelocity.Y);
            }

            SwitchToState(WAIT_STATE);

            oldState = newState;
        }
    }
}
