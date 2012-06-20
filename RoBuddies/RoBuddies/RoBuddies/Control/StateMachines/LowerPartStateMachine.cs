using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;
using RoBuddies.Utilities;
using RoBuddies.Model.Objects;

namespace RoBuddies.Control.StateMachines
{
    class LowerPartStateMachine : StateMachine
    {
        #region Members and Properties

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

        public Level Level
        {
            get { return robot.Level; }
        }

        #endregion

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
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && !(CurrentState is JumpingState) && isOnGround())
            {
                ToJumping(CurrentState);
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                SwitchToState(WALK_STATE);
                if (!isOnGround())
                {
                    robot.LowerPart.wheelMotor.MotorSpeed = 0f;
                    robot.LowerPart.ApplyForce(new Vector2(-100, 0));
                    if (robot.LowerPart.LinearVelocity.X < -3)
                    {
                        robot.LowerPart.LinearVelocity = new Vector2(-3, robot.LowerPart.LinearVelocity.Y);
                    }
                }
                else
                {
                    robot.LowerPart.wheelMotor.MotorSpeed = 10f;
                }
                Body.Effect = SpriteEffects.FlipHorizontally;
                UpdateWalkAnimation(gameTime);
                //Console.WriteLine("Walk Left");
            }

            if (newState.IsKeyUp(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                robot.LowerPart.LinearVelocity = new Vector2(0, robot.LowerPart.LinearVelocity.Y);
                robot.LowerPart.wheelMotor.MotorSpeed = 0f;
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                SwitchToState(WALK_STATE);
                if (!isOnGround())
                {
                    robot.LowerPart.wheelMotor.MotorSpeed = 0f;
                    robot.LowerPart.ApplyForce(new Vector2(100, 0));
                    if (robot.LowerPart.LinearVelocity.X > 3)
                    {
                        robot.LowerPart.LinearVelocity = new Vector2(3, robot.LowerPart.LinearVelocity.Y);
                    }
                }
                else
                {
                    robot.LowerPart.wheelMotor.MotorSpeed = -10f;
                }
                Body.Effect = SpriteEffects.None;
                UpdateWalkAnimation(gameTime);
                //Console.WriteLine("Walk Right");
            }

            if (newState.IsKeyUp(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                robot.LowerPart.LinearVelocity = new Vector2(0, robot.LowerPart.LinearVelocity.Y);
                robot.LowerPart.wheelMotor.MotorSpeed = 0f;
            }


            oldState = newState;
        }

        private bool isOnGround()
        {
            Vector2 lowerPartPos = robot.LowerPart.Position;
            float rayEnd = lowerPartPos.Y - robot.LowerPart.Height / 2;
            bool isOnGround = RayCastUtility.isIntesectingAnObject(this.Level, lowerPartPos, new Vector2(lowerPartPos.X, rayEnd));
            return isOnGround;
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
            Console.WriteLine("Jump LowerPart!");
            SwitchToState(JUMP_STATE);
            ((Body)Body).ApplyForce(new Vector2(0, 1200));
        }
    }
}
