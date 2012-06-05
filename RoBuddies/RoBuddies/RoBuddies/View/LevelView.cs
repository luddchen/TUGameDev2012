﻿
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

                // objects test
                    Ladder ladder = new Ladder(new Vector2(8f, -4.7f), new Vector2(1.5f, 6f), Color.RosyBrown, this.Level, this.Game);
                    //ladder.BodyType = BodyType.Dynamic;
                    Pipe pipe = new Pipe(new Vector2(8f, -0.7f), 10f, Color.LightGray, this.Level, this.Game);
                    Door door = new Door("", new Vector2(13f, -5.27f), new Vector2(2f, 5f), Color.BurlyWood, this.Level, this.Game, false);
                    Switch switcher = new Switch("", new Vector2(11f, -5.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.Game);        

                    Crate crateExm = new Crate("", new Vector2(4f, -5.75f), new Vector2(2f, 4f), Color.BurlyWood, this.Level, this.Game);
                    Crate box1 = new Crate("",new Vector2(10f, -7f), new Vector2(2f, 2f), Color.BurlyWood, this.Level, this.Game);                 

                    backLayer.AddObject(ladder);
                    backLayer.AddObject(pipe);
                    backLayer.AddObject(door);
                    backLayer.AddObject(switcher);  

                    mainLayer.AddObject(crateExm);
                    mainLayer.AddObject(box1);

                // body 3
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Wall wall; 
                    for (int i = 0; i < 3; i++)
                    {
                        wall = new Wall(new Vector2(4 + i * 4f, -7.9f), new Vector2(4f, 0.3f), Color.BurlyWood, square, this.Level);
                        mainLayer.AddObject(wall);
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
