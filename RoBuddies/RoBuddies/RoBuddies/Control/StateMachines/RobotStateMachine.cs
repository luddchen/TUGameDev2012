using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Utilities;
using RoBuddies.Model.Objects;

namespace RoBuddies.Control.StateMachines
{
    class RobotStateMachine : StateMachine
    {
        #region Members and Properties

        public const String WAIT_STATE = "WaitingState";
        public const String JUMP_STATE = "JumpState";
        public const String WALK_STATE = "WalkingState";

        private StateMachine mActiveStateMachine;
        private HeadStateMachine mHeadStateMachine;
        private PartsCombinedStateMachine mPartsCombinedStateMachine;
        private UpperPartStateMachine mUpperPartStateMachine;
        private LowerPartStateMachine mLowerPartStateMachine;

        private Robot mRobot;
        private KeyboardState mOldState;

        public StateMachine ActiveStateMachine
        {
            get { return ActiveStateMachine; }
            set { mActiveStateMachine = value; }
        }

        public HeadStateMachine HeadStateMachine
        {
            get { return mHeadStateMachine; }
        }

        public PartsCombinedStateMachine PartsCombinedStateMachine
        {
            get { return mPartsCombinedStateMachine; }
        }

        public UpperPartStateMachine UpperPartStateMachine
        {
            get { return mUpperPartStateMachine; }
        }

        public LowerPartStateMachine LowerPartStateMachine
        {
            get { return mLowerPartStateMachine; }
        }

        public Level Level
        {
            get { return this.mRobot.Level; }
        }

        #endregion

        public RobotStateMachine(IBody body, ContentManager content, Robot robot)
            : base(body)
        {
            mRobot = robot;

            mPartsCombinedStateMachine = new PartsCombinedStateMachine(robot.PartsCombined, content, robot);
            mHeadStateMachine = new HeadStateMachine(robot.Head, content, robot);
            mUpperPartStateMachine = new UpperPartStateMachine(robot.UpperPart, content, robot);
            mLowerPartStateMachine = new LowerPartStateMachine(robot.LowerPart, content, robot);
            mActiveStateMachine = mPartsCombinedStateMachine;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.X) && mOldState.IsKeyUp(Keys.X))
            {
                if (mActiveStateMachine == mPartsCombinedStateMachine)
                {
                    mActiveStateMachine = mUpperPartStateMachine;
                    mRobot.UpperPart.Position = Vector2.Add(mRobot.PartsCombined.Position, new Vector2(0, mRobot.PartsCombined.Height / 2));
                    mRobot.LowerPart.Position = new Vector2(mRobot.PartsCombined.Position.X, mRobot.PartsCombined.Position.Y - mRobot.PartsCombined.Height / 2 + mRobot.LowerPart.Height / 2);
                    setCombined(false);
                    mRobot.ActivePart = mRobot.UpperPart;
                    mRobot.UpperPart.Color = Color.White;
                    mRobot.LowerPart.Color = new Color(160, 160, 160, 160);
                    mUpperPartStateMachine.SwitchToState(UpperPartStateMachine.SHOOTING_STATE);
                }
                else if (canCombine())
                {
                    mActiveStateMachine = mPartsCombinedStateMachine;
                    mRobot.PartsCombined.Position = new Vector2(mRobot.LowerPart.Position.X, mRobot.LowerPart.Position.Y - mRobot.LowerPart.Height / 2 + mRobot.PartsCombined.Height / 2);
                    setCombined(true);
                    mRobot.ActivePart = mRobot.PartsCombined;
                }
            }

            if (newState.IsKeyDown(Keys.LeftAlt) && mOldState.IsKeyUp(Keys.LeftAlt))
            {
                if (mActiveStateMachine == mLowerPartStateMachine)
                {
                    mActiveStateMachine = mUpperPartStateMachine;
                    mRobot.ActivePart = mRobot.UpperPart;
                    mRobot.UpperPart.Color = Color.White;
                    mRobot.LowerPart.Color = new Color(160, 160, 160, 160);
                }
                else if (mActiveStateMachine == mUpperPartStateMachine)
                {
                    mActiveStateMachine = mLowerPartStateMachine;
                    mRobot.ActivePart = mRobot.LowerPart;
                    mRobot.UpperPart.Color = new Color(160, 160, 160, 160);
                    mRobot.LowerPart.Color = Color.White;
                }
            }

            if (newState.IsKeyDown(Keys.S) && mOldState.IsKeyUp(Keys.S))
            {
                if (mActiveStateMachine == mPartsCombinedStateMachine || mActiveStateMachine == mUpperPartStateMachine)
                {
                    Layer backLayer = Level.GetLayerByName("backLayer");
                    foreach (IBody obj in backLayer.AllObjects)
                    {
                        if (obj is Switch && (Vector2.Distance(mRobot.ActivePart.Position, obj.Position) < 5))
                        {
                            Switch switcher = (Switch)obj;
                            switcher.Activate();
                        }
                    }
                }
            }

            mOldState = newState;
            mActiveStateMachine.Update(gameTime);
        }

        /// <summary>
        /// This method calculates the distance between the upper and lower part 
        /// and calculates if they are able to combine.
        /// </summary>
        /// <returns>returns true if the parts are near enough to combine</returns>
        private bool canCombine()
        {
            bool canCombine = false;
            if (Vector2.Distance(mRobot.UpperPart.Position, mRobot.LowerPart.Position) < 1)
            {
                canCombine = true;
            }
            return canCombine;
        }

        private void setCombined(bool isCombined)
        {
            mRobot.UpperPart.IsVisible = !isCombined;
            mRobot.LowerPart.IsVisible = !isCombined;
            mRobot.PartsCombined.IsVisible = isCombined;
        }
    }
}
