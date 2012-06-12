using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private KeyboardState oldState;
        private ContentManager contentManager;
        private Robot robot;
        private List<Texture2D> textureList;

        public Level Level
        {
            get { return robot.Level; }
        }

        public UpperPartStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.contentManager = contentManager;
            this.robot = robot;
            textureList = new List<Texture2D>();

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
            KeyboardState newState = Keyboard.GetState();

            if (CurrentState.Name == SHOOTING_STATE && hitsPipe())
            {
                SwitchToState(PIPE_CLIMBING_STATE);
            }

            if (CurrentState.Name == PIPE_CLIMBING_STATE && newState.IsKeyDown(Keys.Left))
            {
                (Body as Body).LinearVelocity = new Vector2(-2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.FlipHorizontally;
            }

            if (CurrentState.Name == PIPE_CLIMBING_STATE && newState.IsKeyDown(Keys.Right))
            {
                (Body as Body).LinearVelocity = new Vector2(2, (Body as Body).LinearVelocity.Y);
                Body.Effect = SpriteEffects.None;
            }

            CurrentState.Update(gameTime);
            oldState = newState;
        }

        private bool hitsPipe()
        {
            Vector2 upperPartPos = robot.UpperPart.Position;
            float rayEnd = upperPartPos.Y + robot.UpperPart.Height / 2 - 0.5f;
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, upperPartPos, new Vector2(upperPartPos.X, rayEnd));
            bool hitsPipe = intersectingObject is Pipe;
            return hitsPipe;
        }
    }
}
