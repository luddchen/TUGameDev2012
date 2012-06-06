using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class LowerPartStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private const int START_WALKING = 5;
        private const int STOP_WALKING = 24;
        private const int END_ANIMATION = 55;

        private KeyboardState oldState;
        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private Robot robot;
        private float currentTextureIndex;
        
        public LowerPartStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.contentManager = contentManager;
            this.robot = robot;
            this.textureList = new List<Texture2D>();

            for (int i = 1; i <= END_ANIMATION; i++)
            {
                textureList.Add(contentManager.Load<Texture2D>("Sprites\\Robot\\Bud\\" + String.Format("{0:0000}", i)));
            }

            body.Texture = textureList[0];
        }

        public override void Update(GameTime gameTime)
        {
            if (robot.ActivePart == robot.LowerPart)
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
                    //Console.WriteLine("Walk Left");
                }

                if (newState.IsKeyDown(Keys.Right))
                {
                    SwitchToState(WALK_STATE);
                    (Body as Body).LinearVelocity = new Vector2(2, (Body as Body).LinearVelocity.Y);
                    Body.Effect = SpriteEffects.None;
                    UpdateWalkAnimation(gameTime);
                    //Console.WriteLine("Walk Right");
                }

                if (newState.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
                {
                    robot.PartsCombined.Position = robot.LowerPart.Position + new Vector2(0, 2);
                    robot.PartsCombined.IsVisible = true;
                    robot.ActivePart = robot.PartsCombined;

                    robot.LowerPart.IsVisible = false;
                    robot.UpperPart.IsVisible = false;
                }

                oldState = newState;
            }
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

            Body.Texture = textureList[(int)currentTextureIndex];
            currentTextureIndex += 0.6f;
        }

        public void ToJumping(State state)
        {
            Console.WriteLine("Jump!");
            SwitchToState(JUMP_STATE);
            ((Body)Body).ApplyForce(new Vector2(0, 1000));
        }
    }
}
