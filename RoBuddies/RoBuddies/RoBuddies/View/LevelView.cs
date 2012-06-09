
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
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds;

namespace RoBuddies.View
{
    class LevelView : HUD.HUDLevelView
    {
        public Model.Snapshot.Snapshot SnapShot;
        private KeyboardState oldKeyboardState;

        public HUD.HUD HUD;

        private Worlds worlds;

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
            this.Camera.SmoothMove = true;
            // TODO: calculate level bounds
            //this.Camera.SetBoundingBox( new Rectangle(400, 100, 600, 100) );
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//back_1");

            this.worlds = new Worlds(game);

            this.Level = this.worlds.getNextLevel();

            this.SnapShot = new Model.Snapshot.Snapshot(this.Level);

            this.Level.ClearForces();
            this.SnapShot.MakeSnapshot();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Level.finished) // load next level, when current level is finished
            {
                Level nextLevel = this.worlds.getNextLevel();
                if (nextLevel != null)
                {
                    this.Level = nextLevel;
                }
            }

            this.Level.Update(gameTime);    // taken from HUDLevelView because editor need no level update
            this.HUD.Update(gameTime);

            // camera following test
            if ( this.Level.Robot != null && this.Level.Robot.ActivePart != null)
            {
                this.Camera.Move(Utilities.ConvertUnits.ToDisplayUnits(this.Level.Robot.ActivePart.Position));
            }

            KeyboardState newKeyboardState = Keyboard.GetState();

            // this is very dirty and won't work in the real game:
            // TODO: overwork the door logic
            //if (newKeyboardState.IsKeyDown(Keys.C) && oldKeyboardState.IsKeyUp(Keys.C))
            //{
            //    doorSwitcher.Activate();
            //}

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

            // test key for switching to the next level
            if (newKeyboardState.IsKeyDown(Keys.F12) && oldKeyboardState.IsKeyUp(Keys.F12))
            {
                this.Level.finished = true;
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
