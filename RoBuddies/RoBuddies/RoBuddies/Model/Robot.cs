
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates;

namespace RoBuddies.Model
{

    /// <summary>
    /// collection of all robotparts
    /// </summary>
    class Robot
    {
        private PhysicObject lowerPart;
        private PhysicObject upperPart;
        private PhysicObject partsCombined;
        private PhysicObject head;
        private PhysicObject activePart;

        private PartsCombinedStateMachine partsCombinedStateMachine;
        private LowerPartStateMachine lowerPartStateMachine;
        private UpperPartStateMachine upperPartStateMachine;
        private HeadStateMachine headStateMachine;

        private Level level;

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
           
            headStateMachine = new HeadStateMachine(head, content, this);

            this.partsCombined.IsVisible = true;
            this.partsCombined.IgnoreCollisionWith(this.upperPart);
            this.partsCombined.IgnoreCollisionWith(this.lowerPart);

            this.upperPart.IsVisible = false;
            this.upperPart.IgnoreCollisionWith(this.partsCombined);
            this.upperPart.IgnoreCollisionWith(this.lowerPart);

            this.lowerPart.IsVisible = false;
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

            lowerPartStateMachine = new LowerPartStateMachine(lowerPart, content, this);
            State lowerWaitingState = new WaitingState(LowerPartStateMachine.WAIT_STATE, lowerWaitTex, lowerPartStateMachine);

            lowerPartStateMachine.AllStates.Add(lowerWaitingState);
            lowerPartStateMachine.SwitchToState(LowerPartStateMachine.WAIT_STATE);

            this.level.AddStateMachine(lowerPartStateMachine);
            this.level.GetLayerByName("mainLayer").AddObject(this.lowerPart);
        }

        private void initPartsCombined(Vector2 pos, ContentManager content)
        {
            Texture2D waitTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
            Texture2D jumpTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");

            this.partsCombined = new PhysicObject(this.level);
            this.ActivePart = partsCombined; 

            partsCombined.FixedRotation = true;
            partsCombined.Position = pos;
            partsCombined.BodyType = BodyType.Dynamic;
            partsCombined.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, partsCombined);
            partsCombined.Width = 3;
            partsCombined.Height = 3;

            partsCombinedStateMachine = new PartsCombinedStateMachine(partsCombined, content, this);
            State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, partsCombinedStateMachine);
            State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, partsCombinedStateMachine);
            State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, partsCombinedStateMachine);

            partsCombinedStateMachine.AllStates.Add(waitingState);
            partsCombinedStateMachine.AllStates.Add(jumpState);
            partsCombinedStateMachine.AllStates.Add(walkingState);
            partsCombinedStateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);

            this.level.AddStateMachine(partsCombinedStateMachine);
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

            upperPartStateMachine = new UpperPartStateMachine(upperPart, content, this);

            State upperWaitState = new WaitingState(UpperPartStateMachine.WAIT_STATE, upperWaitTex, upperPartStateMachine);

            upperPartStateMachine.AllStates.Add(upperWaitState);
            upperPartStateMachine.SwitchToState(UpperPartStateMachine.WAIT_STATE);

            this.level.AddStateMachine(upperPartStateMachine);
            this.level.GetLayerByName("mainLayer").AddObject(this.upperPart);
        }

        public PhysicObject LowerPart 
        {
            get { return lowerPart; }
        }

        public PhysicObject UpperPart 
        {
            get { return upperPart; }
        }

        public PhysicObject PartsCombined
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

        public PartsCombinedStateMachine PartsCombinedStateMachine
        {
            get { return partsCombinedStateMachine; }
        }

        public HeadStateMachine HeadStateMachine
        {
            get { return headStateMachine; }
        }

        public LowerPartStateMachine LowerPartStateMachine
        {
            get { return lowerPartStateMachine; }
        }

        public UpperPartStateMachine UpperPartStateMachine
        {
            get { return upperPartStateMachine; }
        }

        public void Update() 
        {
        }

    }
}
