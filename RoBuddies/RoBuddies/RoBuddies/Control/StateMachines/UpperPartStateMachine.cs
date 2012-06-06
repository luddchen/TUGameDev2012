using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class UpperPartStateMachine : StateMachine
    {
        public const String WAIT_STATE = "WaitingState";

        private KeyboardState oldState;
        private ContentManager contentManager;
        private Robot robot;

        public UpperPartStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.contentManager = contentManager;
            this.robot = robot;
        }

        public override void Update(GameTime gameTime)
        {
            if (robot.ActivePart == robot.UpperPart)
            {
                KeyboardState newState = Keyboard.GetState();

                if (newState.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
                {
                    robot.PartsCombined.Position = robot.LowerPart.Position + new Vector2(0, 2);
                    robot.PartsCombined.Enabled = true;
                    robot.ActivePart = robot.PartsCombined;

                    Body.Layer.AddObject(robot.PartsCombined);
                    robot.LowerPart.Enabled = false;
                    robot.UpperPart.Enabled = false;
                    Body.Layer.RemoveObject(robot.LowerPart);
                    Body.Layer.RemoveObject(robot.UpperPart);
                }

                oldState = newState;
            }
        }
    }
}
