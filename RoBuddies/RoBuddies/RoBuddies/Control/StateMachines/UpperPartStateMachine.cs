using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class UpperPartStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";

        private const int START_ANIMATION = 80;
        private const int END_ANIMATION = 130;

        private KeyboardState oldState;
        private ContentManager contentManager;
        private Robot robot;
        private List<Texture2D> textureList;

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
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            oldState = newState;
        }
    }
}
