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
        protected List<Texture2D> textureList;
        private ContentManager contentManager;
        protected Robot robot;
        private bool hasHead;

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
            this.HasHead = true;

            textureList.Add(contentManager.Load<Texture2D>("Sprites//Robot//BridgeHead"));
            body.Texture = textureList[0];

            AllStates.Add(new WaitingState(WAIT_STATE, textureList, this));

            SwitchToState(WAIT_STATE);
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePosition();

            CurrentState.Update(gameTime);
        }

        public void UpdatePosition()
        {
            if (robot.ActivePart != robot.LowerPart)
            {
                robot.Head.Effect = robot.ActivePart.Effect;
            }
            float xOffset = 0;
            if (robot.RobotStateMachine.PartsCombinedStateMachine.CurrentState is PushingState) { xOffset -= 0.2f; }
            if (robot.Head.Effect == SpriteEffects.None) { xOffset *= -1; }
            if (robot.ActivePart == robot.PartsCombined)
            {
                Body.Position = robot.PartsCombined.Position + new Vector2(xOffset, robot.PartsCombined.Height / 2 + robot.Head.Height / 4);
            }
            else
            {
                Body.Position = robot.UpperPart.Position + new Vector2(xOffset, robot.UpperPart.Height / 4 + robot.Head.Height / 4);
            }
        }

        private bool canUseHead()
        {
            bool canUseHead = robot.ActivePart == robot.UpperPart || robot.ActivePart == robot.PartsCombined;
            return canUseHead;
        }
    }
}
