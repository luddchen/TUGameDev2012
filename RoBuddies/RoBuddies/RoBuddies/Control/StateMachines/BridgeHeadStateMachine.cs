using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model.Objects;
using RoBuddies.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Control.StateMachines
{
    class BridgeHeadStateMachine : HeadStateMachine
    {
        private KeyboardState oldState;
        private Wall oldWall;

        public BridgeHeadStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base( body, contentManager, robot)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Y) && oldState.IsKeyUp(Keys.Y))
            {
                Layer mainLayer = robot.Level.GetLayerByName("mainLayer");
                Vector2 pos = new Vector2((int)Math.Round(robot.PartsCombined.Position.X), (int)Math.Round(robot.PartsCombined.Position.Y - 2.3f));
                Wall newWall = new Wall(pos, new Vector2(3, 1), Color.White, robot.Level, robot.Game, false);
                mainLayer.AddObject(newWall);
                if (oldWall != null)
                {
                    robot.Level.removeObject(oldWall);
                    oldWall.Dispose();

                }
                oldWall = newWall;
            }

            oldState = newState;
        }
    }
}
