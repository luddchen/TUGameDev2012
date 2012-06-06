
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
            //Texture2D textureLowerPart = content.Load<Texture2D>("Sprite//Robot//Bud//0001");

            // partsCombined construction ------------------------------------------------------------------------------
            Texture2D waitTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
            Texture2D jumpTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");
            Texture2D lowerWaitTex = content.Load<Texture2D>("Sprites//Robot//Bud//0001");

            this.lowerPart = new PhysicObject(this.level);
            this.upperPart = new PhysicObject(this.level);
            this.partsCombined = new PhysicObject(this.level);
            this.head = new PhysicObject(this.level);
            this.ActivePart = partsCombined;
            partsCombined.FixedRotation = true;
            partsCombined.Position = pos;
            partsCombined.BodyType = BodyType.Dynamic;
            partsCombined.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, partsCombined);
            partsCombined.Width = 3;
            partsCombined.Height = 3;

            partsCombinedStateMachine = new PartsCombinedStateMachine(partsCombined, game.Content, this);
            lowerPartStateMachine = new LowerPartStateMachine(lowerPart, game.Content, this);
            upperPartStateMachine = new UpperPartStateMachine(upperPart, game.Content, this);
            headStateMachine = new HeadStateMachine(head, game.Content, this);
            State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, partsCombinedStateMachine);
            State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, partsCombinedStateMachine);
            State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, partsCombinedStateMachine);

            State lowerWaitingState = new WaitingState(LowerPartStateMachine.WAIT_STATE, lowerWaitTex, lowerPartStateMachine);

            partsCombinedStateMachine.AllStates.Add(waitingState);
            partsCombinedStateMachine.AllStates.Add(jumpState);
            partsCombinedStateMachine.AllStates.Add(walkingState);
            partsCombinedStateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);

            lowerPartStateMachine.AllStates.Add(lowerWaitingState);
            lowerPartStateMachine.SwitchToState(LowerPartStateMachine.WAIT_STATE);

            this.level.AddStateMachine(partsCombinedStateMachine);
            this.level.AddStateMachine(lowerPartStateMachine);
            this.level.GetLayerByName("mainLayer").AddObject(this.partsCombined);
            //this.level.GetLayerByName("mainLayer").AddObject(this.lowerPart);
            this.level.Robot = this;
            //----------------------------------------------------------------------------------------------------------
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
