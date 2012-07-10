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
        private const int START_ANIMATION = 80;
        private const int END_ANIMATION = 130;

        private ContentManager contentManager;
        private Robot robot;
        public Pipe currentPipe;
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
            : base(body, robot.Game)
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
            if (CurrentState.Name != PIPE_CLIMBING_STATE && canClimb(0))
            {
                if (CurrentState.Name == SHOOTING_STATE)
                {
                    this.robot.UpperPart.Position = new Vector2(this.robot.UpperPart.Position.X, currentPipe.Position.Y - this.robot.UpperPart.Height / 2 + 0.25f);
                    this.robot.UpperPart.LinearVelocity = Vector2.Zero;
                }
                SwitchToState(PIPE_CLIMBING_STATE);
            }

            if (CurrentState.Name == PIPE_CLIMBING_STATE)
            {
                ((PipeClimbingState)CurrentState).IsMoving = false;

                if (ButtonIsDown(ControlButton.left) && canClimb(-1))
                {
                    (Body as Body).LinearVelocity = new Vector2(-7, (Body as Body).LinearVelocity.Y);
                    Body.Effect = SpriteEffects.FlipHorizontally;
                    ((PipeClimbingState)CurrentState).UpdateClimbAnimation(gameTime);
                }

                if (ButtonIsDown(ControlButton.right) && canClimb(1))
                {
                    (Body as Body).LinearVelocity = new Vector2(7, (Body as Body).LinearVelocity.Y);
                    Body.Effect = SpriteEffects.None;
                    ((PipeClimbingState)CurrentState).UpdateClimbAnimation(gameTime);
                }

                if (ButtonPressed(ControlButton.releasePipe))
                {
                    releasePipe();
                }
            }
            else if (ButtonIsDown(ControlButton.left))
            {
                Body.Effect = SpriteEffects.FlipHorizontally;
            }
            else if (ButtonIsDown(ControlButton.right))
            {
                Body.Effect = SpriteEffects.None;
            }

            CurrentState.Update(gameTime);

            if (mHeadStateMachine.HasHead)
            {
                mHeadStateMachine.Update(gameTime);
            }
        }

        public void releasePipe()
        {
            if (currentPipe != null)
            {
                this.robot.UpperPart.Position = new Vector2(this.robot.UpperPart.Position.X, this.robot.UpperPart.Position.Y - currentPipe.Height / 2 - 0.3f);
                SwitchToState(WAIT_STATE);
            }
        }

        /// <summary>
        /// Test with ray if the robot hits a pipe for climbing
        /// </summary>
        /// <returns>true if the upper part hit a pipe</returns>
        private bool canClimb(float direction)
        {
            Vector2 upperPartPos = robot.UpperPart.Position + new Vector2(robot.UpperPart.Width / 6, 0) * direction;
            float rayEnd = upperPartPos.Y + robot.UpperPart.Height / 2;
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, upperPartPos, new Vector2(upperPartPos.X, rayEnd));
            bool hitsPipe = intersectingObject is Pipe;
            if (hitsPipe)
            {
                currentPipe = intersectingObject as Pipe;
            }
            return hitsPipe;
        }

        public bool isOnGround()
        {
            return RayCastUtility.isOnGround(this.Level, robot.UpperPart);
        }
    }
}
