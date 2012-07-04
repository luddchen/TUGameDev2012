using System;
using RoBuddies.Model.Objects;
using RoBuddies.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Control.StateMachines
{
    class BridgeHeadStateMachine : HeadStateMachine
    {
        public static Wall wall; // needs to be static, because actually we've got two bridge heads in the game

        public BridgeHeadStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base( body, contentManager, robot)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (ButtonPressed(ControlButton.head))
            {

                if (isOnGround() 
                    && (robot.RobotStateMachine.ActiveStateMachine == robot.RobotStateMachine.PartsCombinedStateMachine
                    || robot.RobotStateMachine.UpperPartStateMachine.CurrentState.Name != UpperPartStateMachine.PIPE_CLIMBING_STATE))
                {
                    Vector2 pos;
                    Layer mainLayer = robot.Level.GetLayerByName("mainLayer");
                    if (robot.RobotStateMachine.ActiveStateMachine == robot.RobotStateMachine.PartsCombinedStateMachine)
                    {
                        pos = new Vector2((int)Math.Round(robot.PartsCombined.Position.X), (int)Math.Round(robot.PartsCombined.Position.Y - 2.3f));
                    }
                    else
                    {
                        pos = new Vector2((int)Math.Round(robot.UpperPart.Position.X), (int)Math.Round(robot.UpperPart.Position.Y - 1));
                    }
                    if (robot.RobotStateMachine.LooksRight)
                    {
                        pos += new Vector2(1f, 0f);
                    }
                    else
                    {
                        pos -= new Vector2(1f, 0f);
                    }
                    if (wall == null)
                    {
                        wall = new Wall(pos, new Vector2(3, 1), Color.LightGreen, robot.Level, robot.Game, false);
                        mainLayer.AddObject(wall);
                    }
                    else
                    {
                        wall.Position = pos;
                    }
                }
            }

            base.Update(gameTime);
        }

        private bool isOnGround()
        {
            bool isOnGround = false;
            if (robot.RobotStateMachine.ActiveStateMachine == robot.RobotStateMachine.PartsCombinedStateMachine)
            {
                isOnGround = robot.RobotStateMachine.PartsCombinedStateMachine.isOnGround();
            }
            else
            {
                isOnGround = robot.RobotStateMachine.LowerPartStateMachine.isOnGround();
            }
            return isOnGround;
        }
    }
}
