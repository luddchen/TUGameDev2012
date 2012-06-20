
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
            initRobot(content, pos, game);

            this.activePart = this.partsCombined;
            this.level.Robot = this;
        }

        private void initRobot(ContentManager content, Vector2 pos, Game game) 
        {
            this.game = game;

            // don't change the order of the initialization. The order of adding the bodys to the world is important for the editor.
            initLowerPart(pos, content);
            initUpperPart(pos, content);
            initPartsCombined(pos, content);

            initHead(pos, content);
            //this.head = new Head(this.level);
            this.head.IsVisible = true;
           
            //headStateMachine = new HeadStateMachine(head, content, this);

            this.robotStateMachine = new RobotStateMachine(activePart, content, this);
            this.level.AddStateMachine(robotStateMachine);

            this.head.IgnoreCollisionWith(this.partsCombined);
            this.head.IgnoreCollisionWith(this.partsCombined.wheelBody);
            this.head.IgnoreCollisionWith(this.lowerPart);
            this.head.IgnoreCollisionWith(this.lowerPart.wheelBody);
            this.head.IgnoreCollisionWith(this.upperPart);

            this.upperPart.IsVisible = false;
            this.upperPart.Friction = 3f;
            this.upperPart.IgnoreCollisionWith(this.partsCombined);
            this.upperPart.IgnoreCollisionWith(this.lowerPart);

            this.lowerPart.IsVisible = false;
            this.lowerPart.IgnoreCollisionWith(this.head);
            this.lowerPart.IgnoreCollisionWith(this.upperPart);
            this.lowerPart.IgnoreCollisionWith(this.PartsCombined);
            this.lowerPart.IgnoreCollisionWith(this.PartsCombined.wheelBody);
            this.lowerPart.wheelBody.IgnoreCollisionWith(this.upperPart);
            this.lowerPart.wheelBody.IgnoreCollisionWith(this.PartsCombined);
            this.lowerPart.wheelBody.IgnoreCollisionWith(this.PartsCombined.wheelBody);
            this.lowerPart.wheelBody.IgnoreCollisionWith(this.Head);

            this.partsCombined.IgnoreCollisionWith(this.head);
            this.partsCombined.IgnoreCollisionWith(this.upperPart);
            this.partsCombined.IgnoreCollisionWith(this.lowerPart);
            this.partsCombined.IgnoreCollisionWith(this.lowerPart.wheelBody);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.head);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.upperPart);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.lowerPart);
            this.partsCombined.wheelBody.IgnoreCollisionWith(this.lowerPart.wheelBody);
            //----------------------------------------------------------------------------------------------------------
        }

        private void initHead(Vector2 pos, ContentManager content)
        {
            Texture2D headTex = content.Load<Texture2D>("Sprites//stop");

            this.head = new Head(this.level);

            head.FixedRotation = true;
            head.Position = pos;
            head.IgnoreGravity = true;
            head.BodyType = BodyType.Dynamic;
            head.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 1, 1, Vector2.Zero, head);
            head.Width = 3;
            head.Height = 3;

            this.level.GetLayerByName("mainLayer").AddObject(this.head);
        }

        private void initLowerPart(Vector2 pos, ContentManager content)
        {
            this.lowerPart = new LowerPart(this.level, pos);
            this.level.GetLayerByName("mainLayer").AddObject(this.lowerPart);
            this.lowerPart.Enabled = false;
            this.lowerPart.wheelBody.Enabled = false;
        }

        private void initPartsCombined(Vector2 pos, ContentManager content)
        {
            this.partsCombined = new PartsCombined(this.level, pos);
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
            this.upperPart.Enabled = false;
        }

        public void Update() 
        {
        }

    }
}
