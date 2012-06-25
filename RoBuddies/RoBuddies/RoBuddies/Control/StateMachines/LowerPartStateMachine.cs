using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;
using RoBuddies.Utilities;

namespace RoBuddies.Control.StateMachines
{
    class LowerPartStateMachine : StateMachine
    {
        #region Members and Properties

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
            : base(body, robot.Game)
        {
            this.contentManager = contentManager;
            this.robot = robot;
            this.textureList = new List<Texture2D>();

            for (int i = 1; i <= END_ANIMATION; i++)
            {
                textureList.Add(contentManager.Load<Texture2D>("Sprites\\Robot\\Bud\\" + String.Format("{0:0000}", i)));
            }

            body.Texture = textureList[0];

            AllStates.Add(new WalkingState(WALK_STATE, textureList, this));
            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));
            AllStates.Add(new JumpingState(JUMP_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {

            if (ButtonPressed(ControlButton.jump) && !(CurrentState is JumpingState) && isOnGround())
            {
                SwitchToState(JUMP_STATE);
            }

            if (ButtonIsDown(ControlButton.left))
            {
                startWalk(WALK_STATE, -1);
            }

            if (ButtonReleased(ControlButton.left))
            {
                stopWalk();
            }

            if (ButtonIsDown(ControlButton.right))
            {
                startWalk(WALK_STATE, 1);
            }

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

        private void startWalk(String newStateName, int direction)
        {
            WalkingState.joinMovement(robot.LowerPart, robot.LowerPart.wheelMotor, isOnGround(), direction);

            if (!(CurrentState is PullingState))
            {
                SwitchToState(newStateName);
                if (direction > 0)
                {
                    robot.LowerPart.Effect = SpriteEffects.None;
                }
                else
                {
                    robot.LowerPart.Effect = SpriteEffects.FlipHorizontally;
                }
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
