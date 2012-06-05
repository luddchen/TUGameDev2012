
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
        private IBody lowerPart;
        private IBody upperPart;
        private IBody partsCombined;
        private IBody head;
        private IBody activePart;

        private Level level;

        public Robot(ContentManager content, Vector2 pos, Level level, RoBuddies game) 
        {
            this.level = level;
            initRobot(content, pos, game);

            this.activePart = this.partsCombined;
            this.level.Robot = this;
        }

        private void initRobot(ContentManager content, Vector2 pos, RoBuddies game) 
        {
            //Texture2D textureLowerPart = content.Load<Texture2D>("Sprite//Robot//Bud//0001");

            // partsCombined construction ------------------------------------------------------------------------------
            Texture2D waitTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
            Texture2D jumpTex = content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");

            PhysicObject combined = new PhysicObject(this.level);
            combined.FixedRotation = true;
            combined.Position = pos;
            combined.BodyType = BodyType.Dynamic;
            combined.Color = Color.White;
            FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, combined);
            combined.Width = 3;
            combined.Height = 3;

            StateMachine stateMachine = new PartsCombinedStateMachine(combined, game);
            State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, stateMachine);
            State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, stateMachine);
            State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, stateMachine);
            stateMachine.AllStates.Add(waitingState);
            //State example2 = new ExampleState("ExampleState2", waitTex, stateMachine);
            stateMachine.AllStates.Add(jumpState);
            stateMachine.AllStates.Add(walkingState);
            stateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
            this.level.AddStateMachine(stateMachine);
            this.partsCombined = combined;
            this.level.GetLayerByName("mainLayer").AddObject(this.partsCombined);
            //----------------------------------------------------------------------------------------------------------
        }

        public IBody LowerPart 
        {
            get { return lowerPart; }
        }

        public IBody UpperPart 
        {
            get { return upperPart; }
        }

        public IBody PartsCombined
        {
            get { return partsCombined; }
        }

        public IBody Head 
        {
            get { return head; }
            set { head = value; }
        }

        public IBody ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }


        public void Update() 
        {
        }

    }
}
