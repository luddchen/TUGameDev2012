using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates;
using FarseerPhysics.Dynamics.Joints;
using RoBuddies.Model.RobotParts;

namespace RoBuddies.Model
{

    /// <summary>
    /// collection of all robotparts
    /// </summary>
    class Robot
    {
        #region Members and Properties

        private LowerPart lowerPart;
        private PhysicObject upperPart;
        private PartsCombined partsCombined;
        private Head head;
        private PhysicObject activePart;

        private RobotStateMachine robotStateMachine;
        private Level level;
        private Game game;

        public LowerPart LowerPart
        {
            get { return lowerPart; }
        }

        public PhysicObject UpperPart
        {
            get { return upperPart; }
        }

        public PartsCombined PartsCombined
        {
            get { return partsCombined; }
        }

        public Head Head
        {
            get { return head; }
            set { head = value; }
        }

        public PhysicObject ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }

        public RobotStateMachine RobotStateMachine
        {
            get { return robotStateMachine; }
        }

        public Level Level
        {
            get { return level; }
        }

        public Game Game
        {
            get { return game; }
        }

        #endregion

        public Robot(ContentManager content, Vector2 pos, Level level, Game game) 
        {
            this.level = level;
            this.game = game;

            initRobot(content, pos);

            this.activePart = this.partsCombined;
            this.level.Robot = this;
        }

        public void removeHead() {
            this.robotStateMachine.PartsCombinedStateMachine.HeadStateMachine.HasHead = false;
            this.robotStateMachine.UpperPartStateMachine.HeadStateMachine.HasHead = false;
            this.level.Robot.head.setVisible(false);
        }

        private void initRobot(ContentManager content, Vector2 pos) 
        {
            // don't change the order of the initialization. The order of adding the bodys to the world is important for the editor.
            initLowerPart(pos, content);
            initUpperPart(pos, content);
            initPartsCombined(pos, content);
            initHead(pos, content);

            this.robotStateMachine = new RobotStateMachine(activePart, content, this);
            this.level.AddStateMachine(robotStateMachine);

            IgnorePartCollisions();
        }

        private void initHead(Vector2 pos, ContentManager content)
        {
            this.head = new Head(pos, new Vector2(1,1), Color.White, this.level);
        }

        private void initLowerPart(Vector2 pos, ContentManager content)
        {
            this.lowerPart = new LowerPart(pos, this.level);
            this.lowerPart.setVisible(false);
        }

        private void initPartsCombined(Vector2 pos, ContentManager content)
        {
            this.partsCombined = new PartsCombined(pos, this.level);
        }

        private void initUpperPart(Vector2 pos, ContentManager content)
        {
            this.upperPart = new PhysicObject(pos, new Vector2(5, 3.7f), Color.White, 3.0f, this.level);

            upperPart.BodyType = BodyType.Dynamic;
            FixtureFactory.AttachRectangle(1, 1.5f, 1, Vector2.Zero, upperPart);
            this.level.GetLayerByName("mainLayer").AddObject(this.upperPart);
            this.upperPart.setVisible(false);
        }

        private void IgnorePartCollisions()
        {
            List<Body> allParts = new List<Body>();
            allParts.Add(this.lowerPart);
            allParts.Add(this.lowerPart.wheelBody);
            allParts.Add(this.partsCombined);
            allParts.Add(this.partsCombined.wheelBody);
            allParts.Add(this.upperPart);
            allParts.Add(this.head);
            foreach (Body body1 in allParts)
            {
                foreach (Body body2 in allParts)
                {
                    if (body1 != body2)
                    {
                        body1.IgnoreCollisionWith( body2 );
                    }
                }
            }
            allParts.Clear();
        }

    }
}
