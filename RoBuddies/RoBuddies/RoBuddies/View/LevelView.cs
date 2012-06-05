
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

                // Robot
                    Robot robot = new Robot(this.Game.Content, new Vector2(5f, -6f), this.Level, this.Game);

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

                // layerLayer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    backLayer.AddObject(ladder);
                    backLayer.AddObject(pipe);

                    mainLayer.AddObject(crateExm);
                    mainLayer.AddObject(box1);

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
                        body3.Color = Color.BurlyWood;
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

            // camera following test
            if ( this.Level.Robot != null && this.Level.Robot.ActivePart != null)
            {
                this.Camera.Move(Utilities.ConvertUnits.ToDisplayUnits(this.Level.Robot.ActivePart.Position));
            }

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
