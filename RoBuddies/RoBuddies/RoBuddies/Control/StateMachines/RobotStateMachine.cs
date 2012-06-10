using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;

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
                    mRobot.UpperPart.Position = mRobot.ActivePart.Position;
                    mRobot.LowerPart.Position = mRobot.ActivePart.Position;
                    setCombined(false);
                    mRobot.ActivePart = mRobot.UpperPart;
                }
                else
                {
                    mActiveStateMachine = mPartsCombinedStateMachine;
                    mRobot.PartsCombined.Position = mRobot.ActivePart.Position;
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
                }
                else if (mActiveStateMachine == mUpperPartStateMachine)
                {
                    mActiveStateMachine = mLowerPartStateMachine;
                    mRobot.ActivePart = mRobot.LowerPart;
                }
            }

            mOldState = newState;
            mActiveStateMachine.Update(gameTime);
        }

        private void setCombined(bool isCombined)
        {
            mRobot.UpperPart.IsVisible = !isCombined;
            mRobot.LowerPart.IsVisible = !isCombined;
            mRobot.PartsCombined.IsVisible = isCombined;
        }
    }
}
