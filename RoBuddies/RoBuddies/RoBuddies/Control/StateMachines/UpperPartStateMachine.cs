using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model;
using RoBuddies.Control.RobotStates;
using RoBuddies.Utilities;
using RoBuddies.Model.Objects;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Control.StateMachines
{
    class UpperPartStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";
        public const String SHOOTING_STATE = "ShootingState";
        public const String PIPE_CLIMBING_STATE = "PipeClimbingState";

        private const int START_ANIMATION = 80;
        private const int END_ANIMATION = 130;

        private ContentManager contentManager;
        private Robot robot;
        private List<Texture2D> textureList;

        private HeadStateMachine mHeadStateMachine;

        public Level Level
        {
            get { return robot.Level; }
        }

        public HeadStateMachine HeadStateMachine
        {
            get { return mHeadStateMachine; }
        }

        public UpperPartStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.contentManager = contentManager;
            this.robot = robot;
            textureList = new List<Texture2D>();

            mHeadStateMachine = new BridgeHeadStateMachine(robot.Head, contentManager, robot);

            for (int i = START_ANIMATION; i <= END_ANIMATION; i++)
            {
                textureList.Add(contentManager.Load<Texture2D>("Sprites\\Robot\\Budi\\" + String.Format("{0:0000}", i)));
            }

            body.Texture = textureList[0];

            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));
            AllStates.Add(new ShootingState(SHOOTING_STATE, textureList, this));
            AllStates.Add(new PipeClimbingState(PIPE_CLIMBING_STATE, textureList, Level, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (CurrentState.Name != PIPE_CLIMBING_STATE && canClimb(0))
            {
                SwitchToState(PIPE_CLIMBING_STATE);
            }

            if (CurrentState.Name == PIPE_CLIMBING_STATE)
            {
                ((PipeClimbingState)CurrentState).IsMoving = false;

                if (ButtonIsDown(ControlButton.left) && canClimb(-1))
                {
                    (Body as Body).LinearVelocity = new Vector2(-5, (Body as Body).LinearVelocity.Y);
                    Body.Effect = SpriteEffects.FlipHorizontally;
                    ((PipeClimbingState)CurrentState).UpdateClimbAnimation(gameTime);
                }

                if (ButtonIsDown(ControlButton.right) && canClimb(1))
                {
                    (Body as Body).LinearVelocity = new Vector2(5, (Body as Body).LinearVelocity.Y);
                    Body.Effect = SpriteEffects.None;
                    ((PipeClimbingState)CurrentState).UpdateClimbAnimation(gameTime);
                }

                if (ButtonIsDown(ControlButton.jump))
                {
                    SwitchToState(WAIT_STATE); // TODO: maybe a falling state for the lower part
                }
            }

            CurrentState.Update(gameTime);

            if (mHeadStateMachine.HasHead)
            {
                mHeadStateMachine.Update(gameTime);
            }

            oldKeyboardState = newKeyboardState;
        }

        /// <summary>
        /// Test with ray if the robot hits a pipe for climbing
        /// </summary>
        /// <returns>true if the upper part hit a pipe</returns>
        private bool canClimb(float direction)
        {
            Vector2 upperPartPos = robot.UpperPart.Position + new Vector2(robot.UpperPart.Width / 6, 0) * direction;
            float rayEnd = upperPartPos.Y + robot.UpperPart.Height / 3;
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, upperPartPos, new Vector2(upperPartPos.X, rayEnd));
            bool hitsPipe = intersectingObject is Pipe;
            return hitsPipe;
        }
    }
}
