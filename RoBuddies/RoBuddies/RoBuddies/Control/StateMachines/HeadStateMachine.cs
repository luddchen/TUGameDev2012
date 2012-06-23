using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class HeadStateMachine : StateMachine
    {
        private List<Texture2D> textureList;
        private ContentManager contentManager;
        protected Robot robot;
        private bool hasHead;

        public const String WAIT_STATE = "WaitingState";

        public bool HasHead
        {
            get { return hasHead; }
            set { 
                hasHead = value;
                ((PhysicObject)Body).IsVisible = value;
            }
        }

        public HeadStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body, robot.Game)
        {
            this.textureList = new List<Texture2D>();
            this.contentManager = contentManager;
            this.robot = robot;
            this.hasHead = true;

            textureList.Add(contentManager.Load<Texture2D>("Sprites\\Circle"));
            body.Texture = textureList[0];

            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            if (newKeyboardState.IsKeyDown(Keys.LeftControl) && oldKeyboardState.IsKeyUp(Keys.LeftControl))
            {
                Console.WriteLine("Head");
            }

            UpdatePosition();

            CurrentState.Update(gameTime);
        }

        private void UpdatePosition()
        {
            if (robot.ActivePart != robot.UpperPart)
            {
                Body.Position = robot.ActivePart.Position + new Vector2(0, robot.ActivePart.Height / 2);
            }
            else
            {
                Body.Position = robot.ActivePart.Position + new Vector2(0, robot.ActivePart.Height / 4);
            }
        }
    }
}
