
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

        private PhysicObject lowerPart;
        private PhysicObject upperPart;
        private PartsCombined partsCombined;
        private PhysicObject head;
        private PhysicObject activePart;

        private RobotStateMachine robotStateMachine;
        private Level level;

        public PhysicObject LowerPart
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

        public PhysicObject Head
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

        #endregion

        public Robot(ContentManager content, Vector2 pos, Level level, Game game) 
        {
            this.level = level;
            initRobot(content, pos, game);

            this.activePart = this.partsCombined;
            this.level.Robot = this;
        }

        private void initRobot(ContentManager content, Vector2 pos, Game game) 
        {
            // partsCombined construction ------------------------------------------------------------------------------    

            // don't change the order of the initialization. The order of adding the bodys to the world is important for the editor.
            initLowerPart(pos, content);
            initUpperPart(pos, content);
            initPartsCombined(pos, content);

            this.head = new PhysicObject(this.level);
           
            //headStateMachine = new HeadStateMachine(head, content, this);

            this.robotStateMachine = new RobotStateMachine(activePart, content, this);
            this.level.AddStateMachine(robotStateMachine);

            this.upperPart.IsVisible = false;
            this.upperPart.Friction = 3f;
            this.upperPart.IgnoreCollisionWith(this.partsCombined);
            this.upperPart.IgnoreCollisionWith(this.lowerPart);

            this.lowerPart.IsVisible = false;
            this.lowerPart.Friction = 3f;
            this.lowerPart.IgnoreCollisionWith(partsCombined);
            this.lowerPart.IgnoreCollisionWith(upperPart);
            //----------------------------------------------------------------------------------------------------------
        }

        private void initLowerPart(Vector2 pos, ContentManager content)
        {
            Texture2D lowerWaitTex = content.Load<Texture2D>("Sprites//Robot//Bud//0001");

            this.lowerPart = new PhysicObject(this.level);

            lowerPart.FixedRotation = true;
            lowerPart.Position = pos;
            lowerPart.BodyType = BodyType.Dynamic;
            lowerPart.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 1.5f, 1, Vector2.Zero, lowerPart);
            lowerPart.Width = 3;
            lowerPart.Height = 1.8f;

            this.level.GetLayerByName("mainLayer").AddObject(this.lowerPart);
        }

        private void initPartsCombined(Vector2 pos, ContentManager content)
        {
            this.partsCombined = new PartsCombined(this.level, pos);
            this.partsCombined.IgnoreCollisionWith(this.upperPart);
            this.partsCombined.IgnoreCollisionWith(this.lowerPart);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.upperPart);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.lowerPart);
            this.ActivePart = partsCombined;
            this.level.GetLayerByName("mainLayer").AddObject(this.partsCombined);
        }

        private void initUpperPart(Vector2 pos, ContentManager content)
        {
            Texture2D upperWaitTex = content.Load<Texture2D>("Sprites//Robot//Budi//0080");

            this.upperPart = new PhysicObject(this.level);

            upperPart.FixedRotation = true;
            upperPart.Position = pos;
            upperPart.BodyType = BodyType.Dynamic;
            upperPart.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 1.5f, 1, Vector2.Zero, upperPart);
            upperPart.Width = 5;
            upperPart.Height = 3.7f;

            this.level.GetLayerByName("mainLayer").AddObject(this.upperPart);
        }

        public void Update() 
        {
        }

    }
}
