﻿using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
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
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String PULL_STATE = "PullingState";
        public const String CLIMBING_STATE = "ClimbingState";

        private const int END_ANIMATION = 80;

        private KeyboardState oldState;
        private Robot robot;
        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private WeldJoint crateJoint;
        private Crate currentCrate;
        private bool isPulling;

        public Level Level
        {
            get { return robot.Level; }
        }

        public PartsCombinedStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.robot = robot;
            this.contentManager = contentManager;
            this.textureList = new List<Texture2D>();
            this.isPulling = false;

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

        private bool canClimbUp()
        {
            Vector2 combinedPartPos = new Vector2(robot.ActivePart.Position.X, robot.ActivePart.Position.Y + robot.ActivePart.Height);
            float rayEnd = combinedPartPos.Y + robot.ActivePart.Height;
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(combinedPartPos.Y, rayEnd));
            bool canClimbUp = intersectingObject is Pipe;
            return canClimbUp;
        }

        private bool canClimbDown()
        {
            Vector2 combinedPartPos = new Vector2(robot.ActivePart.Position.X, robot.ActivePart.Position.Y - robot.ActivePart.Height / 4);
            float rayEnd = combinedPartPos.Y + robot.ActivePart.Height / 3;
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(combinedPartPos.Y, rayEnd));
            bool canClimbDown = intersectingObject is Ladder;
            return canClimbDown;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Up) && CurrentState.Name == CLIMBING_STATE && canClimbUp())
            {
                ((LadderClimbingState)CurrentState).IsMoving = false;
                (Body as Body).LinearVelocity = new Vector2((Body as Body).LinearVelocity.X, 8);
                Body.Effect = SpriteEffects.None;
                ((LadderClimbingState)CurrentState).UpdateClimbAnimation(gameTime);
            }

            if (newState.IsKeyDown(Keys.A))
            {
                pullCrate();
            }

            if (oldState.IsKeyDown(Keys.A) && newState.IsKeyUp(Keys.A))
            {
                stopPulling();
            }

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && !(CurrentState is JumpingState) && isOnGround())
            {
                SwitchToState(JUMP_STATE);
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WalkingState.LEFT_WALK_STATE);
                }
                if (!isOnGround())
                {
                    robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
                    robot.PartsCombined.ApplyForce(new Vector2(-100, 0));
                    if (robot.PartsCombined.LinearVelocity.X < -3)
                    {
                        robot.PartsCombined.LinearVelocity = new Vector2(-3, robot.PartsCombined.LinearVelocity.Y);
                    }
                }
                else
                {
                    robot.PartsCombined.wheelMotor.MotorSpeed = 10f;
                }
            }

            if (newState.IsKeyUp(Keys.Left))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WAIT_STATE);
                }
            }

            if (newState.IsKeyUp(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WAIT_STATE);
                }
                robot.PartsCombined.LinearVelocity = new Vector2(0, robot.PartsCombined.LinearVelocity.Y);
                robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WalkingState.RIGHT_WALK_STATE);
                }
                robot.PartsCombined.wheelMotor.MotorSpeed = -10f;
                if (!isOnGround())
                {
                    robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
                    robot.PartsCombined.ApplyForce(new Vector2(100, 0));
                    if (robot.PartsCombined.LinearVelocity.X > 3)
                    {
                        robot.PartsCombined.LinearVelocity = new Vector2(3, robot.PartsCombined.LinearVelocity.Y);
                    }
                }
                else
                {
                    robot.PartsCombined.wheelMotor.MotorSpeed = -10f;
                }
            }

            if (newState.IsKeyUp(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WAIT_STATE);
                }
                robot.PartsCombined.LinearVelocity = new Vector2(0, robot.PartsCombined.LinearVelocity.Y);
                robot.PartsCombined.wheelMotor.MotorSpeed = 0f;
            }

            if (newState.IsKeyDown(Keys.Up) && canOpenLevelEndingDoor())
            {
                Level.finished = true;
            }

            CurrentState.Update(gameTime);
            oldState = newState;
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

        private bool isOnGround()
        {
            float rayEnd;
            bool isOnGround = false;

            Vector2 combinedPartPos = robot.PartsCombined.wheelBody.Position;
            rayEnd = combinedPartPos.Y - 0.6f;
            isOnGround = RayCastUtility.isIntesectingAnObject(this.Level, combinedPartPos, new Vector2(combinedPartPos.X, rayEnd));

            return isOnGround;
        }

        private Crate getMovableCrate()
        {
            float rayEnd;

            Vector2 combinedPartPos = robot.PartsCombined.Position;

            rayEnd = combinedPartPos.X - robot.PartsCombined.Width / 2;
            Body bodyLeft = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(rayEnd, combinedPartPos.Y));

            if (bodyLeft != null && bodyLeft is Crate)
            {
                return bodyLeft as Crate;
            }

            rayEnd = combinedPartPos.X + robot.PartsCombined.Width / 2;
            Body bodyRight = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(rayEnd, combinedPartPos.Y));

            if (bodyRight != null && bodyRight is Crate)
            {
                return bodyRight as Crate;
            }

            return null;
        }

        private void pullCrate()
        {
            if (!isPulling)
            {
                Crate crate = getMovableCrate();

                if (crate != null)
                {
                    currentCrate = crate;

                    isPulling = true;
                    //SwitchToState(PULL_STATE);

                    crateJoint = new WeldJoint(robot.PartsCombined, currentCrate, currentCrate.WorldCenter, robot.PartsCombined.WorldCenter);
                    currentCrate.FixedRotation = false;
                    this.Level.AddJoint(crateJoint);
                }
            }
        }

        private void stopPulling()
        {
            if (crateJoint != null)
            {
                isPulling = false;
                this.Level.RemoveJoint(crateJoint);
                currentCrate.FixedRotation = true;
            }
        }
    }
}
