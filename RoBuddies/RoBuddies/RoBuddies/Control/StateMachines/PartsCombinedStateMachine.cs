using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using RoBuddies.Control.RobotStates;
using RoBuddies.Control.RobotStates.Interfaces;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private const int END_ANIMATION = 80;

        private KeyboardState oldState;
        private Robot robot;
        private ContentManager contentManager;
        private List<Texture2D> textureList;

        public PartsCombinedStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.robot = robot;
            this.contentManager = contentManager;
            this.textureList = new List<Texture2D>();

            for (int i = 1; i <= END_ANIMATION; i++)
            {
                textureList.Add(contentManager.Load<Texture2D>("Sprites\\Robot\\BudBudi\\" + String.Format("{0:0000}", i)));
            }

            body.Texture = textureList[0];

            AllStates.Add(new WalkingState(WALK_STATE, textureList, this));
            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));
            AllStates.Add(new JumpingState(JUMP_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && !(CurrentState is JumpingState))
            {
                SwitchToState(JUMP_STATE);
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                SwitchToState(WALK_STATE);
                (Body as Body).LinearVelocity = new Vector2(-2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.FlipHorizontally;
            }

            if (newState.IsKeyUp(Keys.Left))
            {
                SwitchToState(WAIT_STATE);
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                SwitchToState(WALK_STATE);
                (Body as Body).LinearVelocity = new Vector2(2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.None;
            }

            CurrentState.Update(gameTime);
            oldState = newState;
        }
    }
}
