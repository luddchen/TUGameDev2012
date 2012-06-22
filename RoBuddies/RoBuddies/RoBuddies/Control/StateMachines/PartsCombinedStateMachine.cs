﻿using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using RoBuddies.Utilities;
using RoBuddies.Model.RobotParts;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        #region Members and Properties

        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String PULL_STATE = "PullingState";
        public const String CLIMBING_STATE = "ClimbingState";

        private const int END_ANIMATION = 90;

        private Robot robot;
        private List<Texture2D> textureList;
        private Crate currentCrate;
        private bool isPulling;
        private float pullingDistance;

        private HeadStateMachine mHeadStateMachine;

        public Level Level
        {
            get { return robot.Level; }
        }

        public HeadStateMachine HeadStateMachine
        {
            get { return mHeadStateMachine; }
        }

        #endregion

        public PartsCombinedStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.robot = robot;
            this.textureList = new List<Texture2D>();
            this.isPulling = false;

            mHeadStateMachine = new BridgeHeadStateMachine(robot.Head, contentManager, robot);

            for (int i = 1; i <= END_ANIMATION; i++)
            {
                textureList.Add(contentManager.Load<Texture2D>("Sprites\\Robot\\BudBudi\\" + String.Format("{0:0000}", i)));
            }

            body.Texture = textureList[0];

            AllStates.Add(new WalkingState(WalkingState.LEFT_WALK_STATE, textureList, this));
            AllStates.Add(new WalkingState(WalkingState.RIGHT_WALK_STATE, textureList, this));
            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));
            AllStates.Add(new JumpingState(JUMP_STATE, textureList, this));
            AllStates.Add(new PullingState(PULL_STATE, textureList, this));
            AllStates.Add(new LadderClimbingState(CLIMBING_STATE, textureList, Level, this));

            SwitchToState(WAIT_STATE);
        }

        private bool canClimb(float yCheckDistance)
        {
            Vector2 combinedPartPos = robot.PartsCombined.Position;
            Vector2 rayDirection = new Vector2(0.51f, yCheckDistance);
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, combinedPartPos + rayDirection);

            if (!(intersectingObject is Ladder)) // second ray on other side of robot
            {
                rayDirection = new Vector2(-0.51f, yCheckDistance);
                intersectingObject = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, combinedPartPos + rayDirection);
            }

            bool canClimbUp = intersectingObject is Ladder;
            return canClimbUp;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if ((newKeyboardState.IsKeyDown(Keys.Up) && canClimb(0.71f)))
            if ( ButtonIsDown(ControlButton.up) && canClimb(0.71f))
            {
                climbLadder(0.15f);
            }

            //if (newKeyboardState.IsKeyDown(Keys.Down) && canClimb(-0.71f)) // && !isOnGround() -> doesnt work because raycast hits ladder
            if (ButtonIsDown(ControlButton.down) && canClimb(-0.71f)) // && !isOnGround() -> doesnt work because raycast hits ladder
            {
                climbLadder(-0.15f);
            }

            //if (newKeyboardState.IsKeyDown(Keys.A) && isOnGround())
            if (ButtonIsDown(ControlButton.use) && isOnGround())
            {
                pullCrate();
            }

            //if (oldKeyboardState.IsKeyDown(Keys.A) && newKeyboardState.IsKeyUp(Keys.A))
            if (ButtonReleased(ControlButton.use) && isPulling)
            {
                stopPulling();
            }
           
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

            //if (newKeyboardState.IsKeyDown(Keys.Up) && canOpenLevelEndingDoor())
            if (ButtonIsDown(ControlButton.up) && canOpenLevelEndingDoor())
            {
                Level.finished = true;
            }

            CurrentState.Update(gameTime);

            if (mHeadStateMachine.HasHead)
            {
                mHeadStateMachine.Update(gameTime);
            }

        }

        /// <summary>
        /// Checks if the robot is next to the level ending door and can open it
        /// </summary>
        /// <returns>true if the robot can open the level ending door</returns>
        private bool canOpenLevelEndingDoor()
        {
            bool canOpenLevelEndingDoor = false;
            Layer backLayer = Level.GetLayerByName("backLayer");
            foreach (IBody body in backLayer.AllObjects) {
                if (body is Door)
                {
                    Door door = (Door)body;
                    if (Vector2.Distance(robot.PartsCombined.Position, door.Position) < 1 && door.IsSwitchedOn)
                    { 
                        canOpenLevelEndingDoor = true;
                    }
                }
            }
            return canOpenLevelEndingDoor;
        }

        public bool isOnGround()
        {
            return RayCastUtility.isOnGround(this.Level, robot.PartsCombined.wheelBody);
        }

        private Crate getMovableCrate()
        {
            float rayEnd;

            Vector2 combinedPartPos = robot.PartsCombined.Position;

            rayEnd = combinedPartPos.X - robot.PartsCombined.Width / 3;
            Body bodyLeft = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(rayEnd, combinedPartPos.Y));

            if (bodyLeft != null && bodyLeft is Crate)
            {
                return bodyLeft as Crate;
            }

            rayEnd = combinedPartPos.X + robot.PartsCombined.Width / 3;
            Body bodyRight = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(rayEnd, combinedPartPos.Y));

            if (bodyRight != null && bodyRight is Crate)
            {
                return bodyRight as Crate;
            }

            return null;
        }

        private void pullCrate()
        {
            Crate crate = getMovableCrate();

            if (!isPulling)
            {
                if (crate != null)
                {
                    currentCrate = crate;

                    robot.PartsCombined.wheelBody.IgnoreCollisionWith(currentCrate);
                    robot.PartsCombined.IgnoreCollisionWith(currentCrate);

                    pullingDistance = robot.PartsCombined.Position.X - currentCrate.Position.X;

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

        private void stopPulling()
        {
            isPulling = false;

            robot.PartsCombined.wheelBody.RestoreCollisionWith(currentCrate);
            robot.PartsCombined.RestoreCollisionWith(currentCrate);
            currentCrate.LinearVelocity = Vector2.Zero;
        }

        private void climbLadder(float distance)
        {
            if (!(CurrentState is LadderClimbingState))
            {
                SwitchToState(CLIMBING_STATE);
            }
            ((LadderClimbingState)CurrentState).IsMoving = true;
            robot.ActivePart.Position += new Vector2(0, distance);
        }

        private void startWalk(String newStateName, float force, float velocityLimit, float motorSpeed)
        {
            if (!(CurrentState is PullingState))
            {
                SwitchToState(newStateName);
            }
            if (!isOnGround())
            {
                robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
                robot.PartsCombined.ApplyForce(new Vector2(force, 0));
                if (Math.Abs(robot.PartsCombined.LinearVelocity.X) > Math.Abs(velocityLimit))
                {
                    robot.PartsCombined.LinearVelocity = new Vector2(velocityLimit, robot.PartsCombined.LinearVelocity.Y);
                }
            }
            else
            {
                robot.PartsCombined.wheelMotor.MotorSpeed = motorSpeed;
            }

            if (isPulling)
            {
                currentCrate.LinearVelocity = robot.PartsCombined.LinearVelocity;
            }
        }

        private void stopWalk()
        {
            if (!(CurrentState is PullingState))
            {
                SwitchToState(WAIT_STATE);
            }
            robot.PartsCombined.LinearVelocity = new Vector2(0, robot.PartsCombined.LinearVelocity.Y);
            robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
            if (isPulling)
            {
                currentCrate.LinearVelocity = Vector2.Zero;
            }
        }
    }
}
