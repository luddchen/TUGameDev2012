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


        private bool hitsLadder()
        {
            Vector2 combinedPartPos = mRobot.PartsCombined.Position;
            float rayEnd = combinedPartPos.Y + mRobot.PartsCombined.Height; //mRobot.UpperPart.Height
            FarseerPhysics.Dynamics.Body intersectingObject = RayCastUtility.getIntersectingObject(this.Level, combinedPartPos, new Vector2(combinedPartPos.X, rayEnd));
            bool hitsLadder = intersectingObject is Ladder;    
            return hitsLadder;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            //if (newState.IsKeyDown(Keys.Up) && mOldState.IsKeyUp(Keys.Up))
            //{
            //    if (mActiveStateMachine == mPartsCombinedStateMachine && hitsLadder())
            //    {
            //        mPartsCombinedStateMachine.SwitchToState(PartsCombinedStateMachine.CLIMBING_STATE);
            //    }
            //}

            if (newState.IsKeyDown(Keys.X) && mOldState.IsKeyUp(Keys.X))
            {
                if (mActiveStateMachine == mPartsCombinedStateMachine)
                {
                    mRobot.UpperPart.Position = Vector2.Add(mRobot.PartsCombined.Position, new Vector2(0, mRobot.PartsCombined.Height / 2));
                    mRobot.LowerPart.Position = new Vector2(mRobot.PartsCombined.Position.X, mRobot.PartsCombined.Position.Y - mRobot.PartsCombined.Height / 2 + mRobot.LowerPart.Height / 2);
                    setActivePart(mRobot.UpperPart);
                    //mActiveStateMachine = mUpperPartStateMachine;
                    //setCombined(false);
                    //mRobot.ActivePart = mRobot.UpperPart;
                    //mRobot.UpperPart.Color = Color.White;
                    //mRobot.LowerPart.Color = new Color(160, 160, 160, 160);
                    mUpperPartStateMachine.SwitchToState(UpperPartStateMachine.SHOOTING_STATE);
                }
                else if (canCombine())
                {
                    //change heavy crates to dynamic
                    Layer mainLayer = Level.GetLayerByName("mainLayer");
                    foreach (IBody obj in mainLayer.AllObjects)
                    {
                        if (obj is Crate)
                        {
                            Crate crate = (Crate)obj;
                            crate.stateUpdate = true;
                        }
                    }

                    mRobot.PartsCombined.Position = new Vector2(mRobot.LowerPart.Position.X, mRobot.LowerPart.Position.Y - mRobot.LowerPart.Height / 2 + mRobot.PartsCombined.Height / 2);
                    setActivePart(mRobot.PartsCombined);
                    //mActiveStateMachine = mPartsCombinedStateMachine;
                    //setCombined(true);
                    //mRobot.ActivePart = mRobot.PartsCombined;
                }
            }

            if (newState.IsKeyDown(Keys.LeftAlt) && mOldState.IsKeyUp(Keys.LeftAlt))
            {
                if (mActiveStateMachine == mLowerPartStateMachine)
                {
                    setActivePart(mRobot.UpperPart);
                    //mActiveStateMachine = mUpperPartStateMachine;
                    //mRobot.ActivePart = mRobot.UpperPart;
                    //mRobot.UpperPart.Color = Color.White;
                    //mRobot.LowerPart.Color = new Color(160, 160, 160, 160);
                }
                else if (mActiveStateMachine == mUpperPartStateMachine)
                {
                    //change heavy crates to static
                    Layer mainLayer = Level.GetLayerByName("mainLayer");
                    foreach (IBody obj in mainLayer.AllObjects)
                    {
                        if (obj is Crate)
                        {
                            Crate crate = (Crate)obj;
                            crate.stateUpdate = false;
                        }
                    }

                    setActivePart(mRobot.LowerPart);
                    //mActiveStateMachine = mLowerPartStateMachine;
                    //mRobot.ActivePart = mRobot.LowerPart;
                    //mRobot.UpperPart.Color = new Color(160, 160, 160, 160);
                    //mRobot.LowerPart.Color = Color.White;
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
        /// sets the active robotpart with all statemachines colors and visibility
        /// </summary>
        /// <param name="part"></param>
        public void setActivePart(IBody part)
        {

            if (part == mRobot.PartsCombined)
            {
                mActiveStateMachine = mPartsCombinedStateMachine;
                mRobot.ActivePart = mRobot.PartsCombined;
                setCombined(true);
            }

            if (part == mRobot.LowerPart)
            {
                mActiveStateMachine = mLowerPartStateMachine;
                mRobot.ActivePart = mRobot.LowerPart;
                setCombined(false);
                mRobot.LowerPart.Color = Color.White;
                mRobot.UpperPart.Color = new Color(160, 160, 160, 160);
            }

            if (part == mRobot.UpperPart)
            {
                mActiveStateMachine = mUpperPartStateMachine;
                mRobot.ActivePart = mRobot.UpperPart;
                setCombined(false);
                mRobot.LowerPart.Color = new Color(160, 160, 160, 160);
                mRobot.UpperPart.Color = Color.White;
            }
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
