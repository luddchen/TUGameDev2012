using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;
using RoBuddies.Utilities;
using RoBuddies.Model.Objects;

namespace RoBuddies.Control.StateMachines
{
    class LowerPartStateMachine : StateMachine
    {
        #region Members and Properties

        private const int END_ANIMATION = 55;

        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private Robot robot;
        private Crate currentCrate;
        private bool isPulling;
        private float pullingDistance;

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
            AllStates.Add(new PushingState(PULL_STATE, textureList, this));

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
                startWalk(Direction.left);
            }

            if (ButtonReleased(ControlButton.left))
            {
                stopWalk();
            }

            if (ButtonIsDown(ControlButton.right))
            {
                startWalk(Direction.right);
            }

            if (ButtonReleased(ControlButton.right))
            {
                stopWalk();
            }

            if (ButtonIsDown(ControlButton.use) && isOnGround())
            {
                pullCrate();
            }

            if (((ButtonPressed(ControlButton.left) && canPush(Direction.left)) || (ButtonPressed(ControlButton.right) && canPush(Direction.right)))
                && (CurrentState is PullingState || CurrentState is PushingState))
            {
                Game.soundBank.PlayCue("Pulling");
            }

            if (ButtonReleased(ControlButton.use) && isPulling)
            {
                stopPulling();
            }

            CurrentState.Update(gameTime);
        }

        public bool isOnGround()
        {
            return RayCastUtility.isOnGround(this.Level, robot.LowerPart.wheelBody);
        }

        private void startWalk(Direction direction)
        {
            WalkingState.joinMovement(robot.LowerPart, robot.LowerPart.wheelMotor, isOnGround(), direction);

            if (!isPulling)
            {
                SwitchToState(WALK_STATE);
                robot.LowerPart.chooseDirection(direction);
            }

            if (isPulling)
            {
                int dir = 1;
                if (direction == Model.Direction.left) { dir = -1; }
                if (CurrentState is PushingState)
                {
                    if (canPush(direction)) // crate is right
                    {
                        ((PushingState)CurrentState).joinMovement(this.currentCrate, this.robot.LowerPart, 100 * dir);
                    }
                }
            }

        }

        private bool canPush(Direction direction)
        {
            return this.currentCrate != null && ((this.currentCrate.Position.X <= robot.LowerPart.Position.X && direction == Model.Direction.left) // crate is left
                                    || (this.currentCrate.Position.X >= robot.LowerPart.Position.X && direction == Model.Direction.right));
        }

        private void stopWalk()
        {
            WalkingState.stopMovement(robot.LowerPart, robot.LowerPart.wheelMotor);

            if (!isPulling)
            {
                SwitchToState(WAIT_STATE);
            }

            if (isPulling)
            {
                currentCrate.LinearVelocity = Vector2.Zero;
                if (CurrentState is PushingState)
                {
                    ((PushingState)CurrentState).IsMoving = false;
                }
            }
        }

        private void pullCrate()
        {
            Crate crate = getMovableCrate();

            if (!isPulling)
            {
                if (crate != null)
                {
                    SwitchToState(PULL_STATE);

                    currentCrate = crate;
                    if (currentCrate.Position.X < robot.LowerPart.Position.X)
                    {
                        robot.LowerPart.chooseDirection(Model.Direction.left);
                    }
                    else
                    {
                        robot.LowerPart.chooseDirection(Model.Direction.right);
                    }

                    robot.LowerPart.wheelBody.IgnoreCollisionWith(currentCrate);
                    robot.LowerPart.IgnoreCollisionWith(currentCrate);

                    pullingDistance = robot.LowerPart.Position.X - currentCrate.Position.X;

                    isPulling = true;
                }
            }
            else
            {
                if (crate == null)
                {
                    stopPulling();
                }
            }
        }

        private Crate getMovableCrate()
        {
            float rayEnd;

            Vector2 lowerPartPos = robot.LowerPart.Position;

            rayEnd = lowerPartPos.X - robot.LowerPart.Width / 3;
            FarseerPhysics.Dynamics.Body bodyLeft = RayCastUtility.getIntersectingObject(this.Level, lowerPartPos, new Vector2(rayEnd, lowerPartPos.Y));

            if (bodyLeft != null && bodyLeft is Crate)
            {
                return bodyLeft as Crate;
            }

            rayEnd = lowerPartPos.X + robot.LowerPart.Width / 3;
            FarseerPhysics.Dynamics.Body bodyRight = RayCastUtility.getIntersectingObject(this.Level, lowerPartPos, new Vector2(rayEnd, lowerPartPos.Y));

            if (bodyRight != null && bodyRight is Crate)
            {
                return bodyRight as Crate;
            }

            return null;
        }

        private void stopPulling()
        {
            SwitchToState(WAIT_STATE);

            isPulling = false;

            robot.LowerPart.wheelBody.RestoreCollisionWith(currentCrate);
            robot.LowerPart.RestoreCollisionWith(currentCrate);
            currentCrate.LinearVelocity = Vector2.Zero;
        }

    }
}
