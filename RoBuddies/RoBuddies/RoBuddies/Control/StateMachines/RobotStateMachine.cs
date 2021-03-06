﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.Model;
using RoBuddies.Model.Objects;

namespace RoBuddies.Control.StateMachines
{
    class RobotStateMachine : StateMachine
    {
        #region Members and Properties

        private StateMachine mActiveStateMachine;
        private PartsCombinedStateMachine mPartsCombinedStateMachine;
        private UpperPartStateMachine mUpperPartStateMachine;
        private LowerPartStateMachine mLowerPartStateMachine;

        private Robot mRobot;
        private bool looksRight = true; // maybe redundent, but i need this quick(&dirty) for the bridgehead

        public StateMachine ActiveStateMachine
        {
            get { return mActiveStateMachine; }
            set { mActiveStateMachine = value; }
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

        public bool LooksRight
        {
            get { return this.looksRight; }
        }

        #endregion

        public RobotStateMachine(IBody body, ContentManager content, Robot robot)
            : base(body, robot.Game)
        {
            mRobot = robot;

            mPartsCombinedStateMachine  = new PartsCombinedStateMachine(robot.PartsCombined, content, robot);
            mUpperPartStateMachine      = new UpperPartStateMachine(robot.UpperPart, content, robot);
            mLowerPartStateMachine      = new LowerPartStateMachine(robot.LowerPart, content, robot);
            mActiveStateMachine         = mPartsCombinedStateMachine;
        }

        public override void Update(GameTime gameTime)
        {
            if (ButtonPressed(ControlButton.switchRobotPart))
            {
                if (mActiveStateMachine != mPartsCombinedStateMachine)
                {
                    if (mActiveStateMachine == mLowerPartStateMachine)
                    {
                        mRobot.LowerPart.wheelMotor.MotorSpeed = 0f;
                        setActivePart(mRobot.UpperPart);
                    }
                    else if (mActiveStateMachine == mUpperPartStateMachine)
                    {
                        crateStateUpdate(false);
                       // doorStateUpdate(false);

                        setActivePart(mRobot.LowerPart);
                    }
                }
            }

            if (mActiveStateMachine == mPartsCombinedStateMachine)
            {
                doorStateUpdate(true);
            }
            else
            {
                doorStateUpdate(false);
            }

            if (ButtonPressed(ControlButton.separateRobot))
            {
                if (mActiveStateMachine == mPartsCombinedStateMachine)
                {
                    mPartsCombinedStateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
                    mRobot.PartsCombined.wheelMotor.MotorSpeed = 0;
                    mRobot.LowerPart.LinearVelocity = mRobot.PartsCombined.LinearVelocity;
                    mRobot.UpperPart.LinearVelocity = Vector2.Zero;
                    mRobot.UpperPart.Position = Vector2.Add(mRobot.PartsCombined.Position, new Vector2(0, mRobot.PartsCombined.Height / 2));
                    mRobot.LowerPart.Position = new Vector2(mRobot.PartsCombined.Position.X, mRobot.PartsCombined.Position.Y - mRobot.PartsCombined.Height / 2 + mRobot.LowerPart.Height / 2);
                    mRobot.LowerPart.wheelBody.Position = mRobot.LowerPart.Position + new Vector2(0, (-1f / 2f) + 0.20f);
                    setActivePart(mRobot.UpperPart);

                    mUpperPartStateMachine.SwitchToState(UpperPartStateMachine.SHOOTING_STATE);
                }
                else
                {
                    if (canCombine())
                    {
                        crateStateUpdate(true);
                        mRobot.LowerPart.wheelMotor.MotorSpeed = 0;
                        mUpperPartStateMachine.SwitchToState(UpperPartStateMachine.WAIT_STATE);
                        mLowerPartStateMachine.SwitchToState(LowerPartStateMachine.WAIT_STATE);
                        mRobot.PartsCombined.LinearVelocity = mRobot.LowerPart.LinearVelocity;
                        mRobot.PartsCombined.Position = new Vector2(mRobot.LowerPart.Position.X, mRobot.LowerPart.Position.Y - mRobot.LowerPart.Height / 2 + mRobot.PartsCombined.Height / 2);
                        mRobot.PartsCombined.wheelBody.Position = mRobot.PartsCombined.Position + new Vector2(0, (-2.3f / 2f) + 0.20f);
                        setActivePart(mRobot.PartsCombined);
                    }
                }
            }


            if (ButtonPressed(ControlButton.use))
            {
                if (mActiveStateMachine == mPartsCombinedStateMachine || mActiveStateMachine == mUpperPartStateMachine)
                {
                    Layer backLayer = Level.GetLayerByName("backLayer");
                    foreach (IBody obj in backLayer.AllObjects)
                    {
                        if (obj is Switch && (Vector2.Distance(mRobot.ActivePart.Position, obj.Position) < 1.5))
                        {
                            Switch switcher = (Switch)obj;
                            switcher.Activate();
                        }
                    }
                }
            }

            if (ButtonIsDown(ControlButton.right))
            {
                this.looksRight = true;
            }

            if (ButtonIsDown(ControlButton.left))
            {
                this.looksRight = false;
            }

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
                if (UpperPartStateMachine.HeadStateMachine.HasHead)
                {
                    UpperPartStateMachine.HeadStateMachine.Body.Color = Color.White;
                }
                setCombined(true);
            }

            if (part == mRobot.LowerPart)
            {
                mActiveStateMachine = mLowerPartStateMachine;
                mRobot.ActivePart = mRobot.LowerPart;
                setCombined(false);
                mRobot.LowerPart.Color = Color.White;
                mRobot.UpperPart.Color = new Color(160, 160, 160, 100);
                if (UpperPartStateMachine.HeadStateMachine.HasHead)
                {
                    UpperPartStateMachine.HeadStateMachine.Body.Color = new Color(160, 160, 160, 100);
                }
            }

            if (part == mRobot.UpperPart)
            {
                mActiveStateMachine = mUpperPartStateMachine;
                mRobot.ActivePart = mRobot.UpperPart;
                setCombined(false);
                mRobot.LowerPart.Color = new Color(160, 160, 160, 100);
                mRobot.UpperPart.Color = Color.White;
                if (UpperPartStateMachine.HeadStateMachine.HasHead)
                {
                    UpperPartStateMachine.HeadStateMachine.Body.Color = Color.White;
                }
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
            mRobot.UpperPart.setVisible( !isCombined );
            mRobot.LowerPart.setVisible( !isCombined );
            mRobot.PartsCombined.setVisible( isCombined );
        }

        private void crateStateUpdate(bool newState)
        {
            Layer mainLayer = Level.GetLayerByName("mainLayer");
            foreach (IBody obj in mainLayer.AllObjects)
            {
                if (obj is Crate)
                {
                    Crate crate = (Crate)obj;
                    crate.stateUpdate = newState;
                }
            }
        }

        private void doorStateUpdate(bool newState)
        {
            Layer backLayer = Level.GetLayerByName("backLayer");

            foreach (IBody obj in backLayer.AllObjects)
            {
                if (obj is Door)
                {
                    Door door = obj as Door;
                    door.stateUpdate = newState;
                }
            }
        }
    }
}
