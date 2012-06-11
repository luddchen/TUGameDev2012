
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Model.Worlds;
using RoBuddies.Utilities;

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
            if (this.Camera != null)
            {
                this.Camera.SetBoundingBox(this.Level);
            }
        }

        public LevelView(RoBuddies game) : base(game)
        {
            this.HUD = new LevelHUD(game);
            this.Camera.SmoothMove = true;

            this.worlds = new Worlds(game);

            viewNextLevel();
        }

        /// <summary>
        /// sets the view to the next level
        /// </summary>
        private void viewNextLevel()
        {
            Level nextLevel = this.worlds.getNextLevel();
            if (nextLevel != null)
            {
                if (this.SnapShot != null)
                {
                    this.SnapShot.Release();
                }
                this.Level = nextLevel;
                this.SnapShot = new Model.Snapshot.Snapshot(this.Level);
                this.Level.ClearForces();
                this.SnapShot.MakeSnapshot();
            }            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Level.finished) // load next level, when current level is finished
            {
                viewNextLevel();
                this.Camera.SetBoundingBox(this.Level);
            }

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
