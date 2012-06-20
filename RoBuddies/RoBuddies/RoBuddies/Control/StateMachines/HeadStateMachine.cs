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
        private Robot robot;
        private bool hasHead;
        private KeyboardState oldState;

        public const String WAIT_STATE = "WaitingState";

        public bool HasHead
        {
            get { return hasHead; }
            set { hasHead = value; }
        }

        public HeadStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.textureList = new List<Texture2D>();
            this.contentManager = contentManager;
            this.robot = robot;
            this.hasHead = true;

            textureList.Add(contentManager.Load<Texture2D>("Sprites\\stop"));
            body.Texture = textureList[0];

            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.LeftControl) && oldState.IsKeyUp(Keys.LeftControl))
            {
                Console.WriteLine("Head");
            }

            UpdatePosition();

            CurrentState.Update(gameTime);
            oldState = newState;
        }

        private void UpdatePosition()
        {
            Body.Position = robot.PartsCombined.Position + new Vector2(0, 2);
        }
    }
}
