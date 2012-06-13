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

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String PULL_STATE = "PullingState";

        private const int END_ANIMATION = 80;

        private KeyboardState oldState;
        private Robot robot;
        private ContentManager contentManager;
        private List<Texture2D> textureList;
        private WeldJoint crateJoint;
        private Crate currentCrate;

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

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

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
                if (isOnGround() && !(CurrentState is PullingState))
                {
                    SwitchToState(WalkingState.LEFT_WALK_STATE);
                }
            }

            if (newState.IsKeyUp(Keys.Left))
            {
                if (!(CurrentState is PullingState))
                {
                    SwitchToState(WAIT_STATE);
                }
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                if (isOnGround() && !(CurrentState is PullingState))
                {
                    SwitchToState(WalkingState.RIGHT_WALK_STATE);
                }
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
                    if (Vector2.Distance(robot.PartsCombined.Position, door.Position) < 1) { // TODO: check also if door isn't locked!
                        canOpenLevelEndingDoor = true;
                    }
                }
            }
            return canOpenLevelEndingDoor;
        }

        private bool isOnGround()
        {
            Vector2 combinedPartPos = robot.PartsCombined.Position;
            float rayEnd = combinedPartPos.Y - robot.PartsCombined.Height / 2;
            bool isOnGround = RayCastUtility.isIntesectingAnObject(this.Level, combinedPartPos, new Vector2(combinedPartPos.X, rayEnd));
            return isOnGround;
        }

        private Body getMovableObject()
        {
            Vector2 combinedPartPos = robot.PartsCombined.Position;
            float rayEnd = combinedPartPos.X - robot.PartsCombined.Width / 2;
            Body body = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(rayEnd, combinedPartPos.Y));
            return body;
        }

        private void pullCrate()
        {
            if (!(CurrentState is PullingState))
            {
                Body movableObject = getMovableObject();

                if (movableObject != null && movableObject is Crate)
                {
                    currentCrate = movableObject as Crate;

                    SwitchToState(PULL_STATE);

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
                SwitchToState(WAIT_STATE);
                this.Level.RemoveJoint(crateJoint);
                currentCrate.FixedRotation = true;
            }
        }
    }
}
