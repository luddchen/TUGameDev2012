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

        private const int END_ANIMATION = 55;

        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private Robot robot;

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

            AllStates.Add(new WalkingState(WalkingState.LEFT_WALK_STATE, textureList, this));
            AllStates.Add(new WalkingState(WalkingState.RIGHT_WALK_STATE, textureList, this));
            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));
            AllStates.Add(new JumpingState(JUMP_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if (newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space) && !(CurrentState is JumpingState) && isOnGround())
            if (ButtonPressed(ControlButton.jump) && !(CurrentState is JumpingState) && isOnGround())
            {
                SwitchToState(JUMP_STATE);
            }

            //if (newKeyboardState.IsKeyDown(Keys.Left))
            if (ButtonIsDown(ControlButton.left))
            {
                startWalk(WalkingState.LEFT_WALK_STATE, -100, -3, 15);
            }

            //if (newKeyboardState.IsKeyUp(Keys.Left) && oldKeyboardState.IsKeyDown(Keys.Left))
            if (ButtonReleased(ControlButton.left))
            {
                stopWalk();
            }

            //if (newKeyboardState.IsKeyDown(Keys.Right))
            if (ButtonIsDown(ControlButton.right))
            {
                startWalk(WalkingState.RIGHT_WALK_STATE, 100, 3, -15);
            }

            //if (newKeyboardState.IsKeyUp(Keys.Right) && oldKeyboardState.IsKeyDown(Keys.Right))
            if (ButtonReleased(ControlButton.right))
            {
                stopWalk();
            }

            CurrentState.Update(gameTime);
        }

        public bool isOnGround()
        {
            return RayCastUtility.isOnGround(this.Level, robot.LowerPart.wheelBody);
        }

        private void startWalk(String newStateName, float force, float velocityLimit, float motorSpeed)
        {
            if (!(CurrentState is PullingState))
            {
                SwitchToState(newStateName);
            }
            if (!isOnGround())
            {
                robot.LowerPart.wheelMotor.MotorSpeed = 0f;
                robot.LowerPart.ApplyForce(new Vector2(force, 0));
                if (Math.Abs(robot.LowerPart.LinearVelocity.X) > Math.Abs(velocityLimit))
                {
                    robot.LowerPart.LinearVelocity = new Vector2(velocityLimit, robot.LowerPart.LinearVelocity.Y);
                }
            }
            else
            {
                robot.LowerPart.wheelMotor.MotorSpeed = motorSpeed;
            }
        }

        private void stopWalk()
        {
            if (!(CurrentState is PullingState))
            {
                SwitchToState(WAIT_STATE);
            }
            robot.LowerPart.LinearVelocity = new Vector2(0, robot.LowerPart.LinearVelocity.Y);
            robot.LowerPart.wheelMotor.MotorSpeed = 0f;
        }

    }
}
