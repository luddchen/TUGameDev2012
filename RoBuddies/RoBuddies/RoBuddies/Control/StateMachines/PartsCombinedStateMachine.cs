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
    class PartsCombinedStateMachine : StateMachine, IPartsCombinedTransition
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private const int START_WALKING = 5;
        private const int STOP_WALKING = 24;
        private const int END_ANIMATION = 80;

        private KeyboardState oldState;
        private Robot robot;
        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private float currentTextureIndex;

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
                UpdateWalkAnimation(gameTime);
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                SwitchToState(WALK_STATE);
                (Body as Body).LinearVelocity = new Vector2(2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.None;
                UpdateWalkAnimation(gameTime);
            }

            oldState = newState;
        }

        private void UpdateWalkAnimation(GameTime gameTime)
        {
            if (currentTextureIndex < START_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            if (currentTextureIndex > STOP_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            Body.Texture = textureList[(int) currentTextureIndex];
            currentTextureIndex += 0.6f;
        }

        #region IPartsCombinedTransition Member

        public void ToSeperated(State state)
        {
            throw new NotImplementedException();
        }

        public void ToJumping(State state)
        {
            Console.WriteLine("Jump PartsCombined!");
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
