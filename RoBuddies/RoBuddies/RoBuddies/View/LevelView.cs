
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Control;
using RoBuddies.Control.RobotStates;
using RoBuddies.Control.StateMachines;
using RoBuddies.Model;
using RoBuddies.Model.Objects;

namespace RoBuddies.View
{
    class LevelView : HUD.HUDLevelView
    {
        public Model.Snapshot.Snapshot SnapShot;
        private KeyboardState oldKeyboardState;

        public HUD.HUD HUD;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();
            if (this.HUD != null)
            {
                this.HUD.Viewport = this.viewport;
            }
        }

        public LevelView(RoBuddies game) : base(game)
        {
            this.HUD = new LevelHUD(game);

            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//back_1");

            Layer mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
            Layer backLayer = new Layer("backLayer", new Vector2(1, 1), 0.51f);
            this.Level.AddLayer(mainLayer);
            this.Level.AddLayer(backLayer);

            //  some testing code here --------------------------------------------------------------------------
                // ladder test
                    Ladder ladder = new Ladder(new Vector2(8f, -4.7f), new Vector2(1.5f, 6f), Color.RosyBrown, this.Level, this.Game);
                    //ladder.BodyType = BodyType.Dynamic;
                    Pipe pipe = new Pipe(new Vector2(8f, -0.7f), 10f, Color.LightGray, this.Level, this.Game);
                    
                    
                // body1
                    Texture2D crate = this.Game.Content.Load<Texture2D>("Sprites//Crate");
                    Texture2D crate2 = this.Game.Content.Load<Texture2D>("Sprites//Crate2");
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Texture2D waitTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
                    Texture2D jumpTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");

                    //Crate crateExm = new Crate(new Vector2(4f, -5.75f), new Vector2(2f, 2f), Color.BurlyWood, this.Level, this.Game);
                    Crate crateExm = new Crate(new Vector2(4f, -5.75f), new Vector2(2f, 4f), Color.BurlyWood, this.Level, this.Game);
                    Crate box1 = new Crate(new Vector2(10f, -7f), new Vector2(2f, 2f), Color.BurlyWood, this.Level, this.Game);                 
                    box1.BodyType = BodyType.Dynamic;

                // body 2
                    PhysicObject body2 = new PhysicObject(this.Level);
                    body2.FixedRotation = true;
                    body2.Position = new Vector2(5f, -6f);
                    body2.BodyType = BodyType.Dynamic;
                    body2.Color = Color.White;
                    FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, body2);
                    body2.Width = 3;
                    body2.Height = 3;

                    StateMachine stateMachine = new PartsCombinedStateMachine(body2, this.Game);
                    State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, stateMachine);
                    State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, stateMachine);
                    State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, stateMachine);
                    stateMachine.AllStates.Add(waitingState);
                    //State example2 = new ExampleState("ExampleState2", waitTex, stateMachine);
                    stateMachine.AllStates.Add(jumpState);
                    stateMachine.AllStates.Add(walkingState);
                    stateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
                    this.Level.AddStateMachine(stateMachine);

                // layerLayer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    backLayer.AddObject(ladder);
                    backLayer.AddObject(pipe);

                    mainLayer.AddObject(crateExm);
                    mainLayer.AddObject(box1);
                    mainLayer.AddObject(body2);

                // body 3
                    PhysicObject body3;
                    for (int i = 0; i < 3; i++)
                    {
                        body3 = new PhysicObject(this.Level);
                        body3.Position = new Vector2(4 + i * 4f, -7.9f);
                        body3.BodyType = BodyType.Static;
                        FixtureFactory.AttachRectangle(4f, 0.3f, 1, Vector2.Zero, body3);
                        body3.Width = 4f;
                        body3.Height = 0.3f;
                        body3.Texture = square;
                        body3.Color = Color.DarkKhaki;
                        mainLayer.AddObject(body3);
                    }
            // end testing code ---------------------------------------------------------------------------------

            this.SnapShot = new Model.Snapshot.Snapshot(this.Level);

            this.Level.ClearForces();
            this.SnapShot.MakeSnapshot();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Level.Update(gameTime);    // taken from HUDLevelView because editor need no level update
            this.HUD.Update(gameTime);

            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.S) && oldKeyboardState.IsKeyUp(Keys.S))
            {
                this.Level.ClearForces();
                this.SnapShot.MakeSnapshot();
                this.Level.ClearForces();
            }

            if (newKeyboardState.IsKeyDown(Keys.R) && oldKeyboardState.IsKeyUp(Keys.R))
            {
                this.Level.ClearForces();
                this.SnapShot.Rewind(1);
                this.Level.ClearForces();
            }

            if (newKeyboardState.IsKeyDown(Keys.E) && oldKeyboardState.IsKeyUp(Keys.E))
            {
                this.Level.ClearForces();
                this.SnapShot.Rewind(2);
                this.Level.ClearForces();
            }

            this.oldKeyboardState = newKeyboardState;
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            base.DrawContent(spriteBatch);
            this.HUD.Draw(spriteBatch);
        }

    }
}
