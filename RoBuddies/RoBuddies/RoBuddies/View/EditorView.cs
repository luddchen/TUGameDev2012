using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control;
using RoBuddies.Control.Editor;
using RoBuddies.Model;
using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class EditorView : HUD.HUDLevelView
    {
        private Vector2 parallax = new Vector2(1, 1);
        private MouseController mouseController;
        private KeyboardController keyboardController;

        public EditorToolbar Toolbar;
        public EditorHUD Infobar;
        public bool IsGridVisible;

        public HUDMouse Mouse;

        public HUDTexture UpArrow;
        public HUDTexture DownArrow;
        public HUDTexture LeftArrow;
        public HUDTexture RightArrow;

        public HUDString hudString;

        private Texture2D dot;
        private Vector2 dotOrigin;
        private Color gridColor = new Color(128, 128, 128, 128);

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();
            if (this.hudString != null)
            {
                this.hudString.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height / 2);
            }
            if (this.UpArrow != null)
            {
                this.UpArrow.Position = new Vector2(this.viewport.Width / 2, 15);
            }
            if (this.DownArrow != null)
            {
                this.DownArrow.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height - 15); 
            }
            if (this.LeftArrow != null)
            {
                this.LeftArrow.Position = new Vector2(15, this.viewport.Height / 2);
            }
            if (this.RightArrow != null)
            { 
                this.RightArrow.Position = new Vector2(this.viewport.Width - 15, this.viewport.Height / 2); 
            }
            if (this.Toolbar != null)
            {
                this.Toolbar.Viewport = this.viewport;
            }
            if (this.Infobar != null)
            {
                this.Infobar.Viewport = this.viewport;
            }
        }

        public EditorView(RoBuddies game)
            : base(game)
        {
            hudString = new HUDString(this.Game.Content);
            hudString.String = "";
            this.AllElements.Add(hudString);

            this.Toolbar = new EditorToolbar(game);
            this.Infobar = new EditorHUD(this);
            this.Camera.Zoom = 0.5f;
            this.IsGridVisible = true;

            this.Mouse = new HUDMouse(game.Content);
            this.AllElements.Add(this.Mouse);
            this.mouseController = new MouseController(this, this.Mouse);
            this.keyboardController = new KeyboardController(this, this.mouseController);
            this.mouseController.keyboardController = this.keyboardController;

            this.DownArrow = new HUDTexture(game.Content);
            this.DownArrow.Color = new Color(128, 128, 128, 128);
            this.DownArrow.Width = 100;
            this.DownArrow.Height = 25;
            this.DownArrow.Effect = SpriteEffects.FlipVertically;
            this.DownArrow.Texture = game.Content.Load<Texture2D>("Sprites//Arrow");
            this.AllElements.Add(this.DownArrow);

            this.LeftArrow = new HUDTexture(game.Content);
            this.LeftArrow.Color = new Color(128, 128, 128, 128);
            this.LeftArrow.Width = 25;
            this.LeftArrow.Height = 100;
            this.LeftArrow.Texture = game.Content.Load<Texture2D>("Sprites//Arrow90");
            this.AllElements.Add(this.LeftArrow);

            this.RightArrow = new HUDTexture(game.Content);
            this.RightArrow.Color = new Color(128, 128, 128, 128);
            this.RightArrow.Width = 25;
            this.RightArrow.Height = 100;
            this.RightArrow.Effect = SpriteEffects.FlipHorizontally;
            this.RightArrow.Texture = game.Content.Load<Texture2D>("Sprites//Arrow90");
            this.AllElements.Add(this.RightArrow);

            this.UpArrow = new HUDTexture(game.Content);
            this.UpArrow.Color = new Color(128, 128, 128, 128);
            this.UpArrow.Width = 100;
            this.UpArrow.Height = 25;
            this.UpArrow.Texture = game.Content.Load<Texture2D>("Sprites//Arrow");
            this.AllElements.Add(this.UpArrow);

            Layer mainLayer = new Layer("mainLayer", parallax, 0.5f);
            this.Level.AddLayer(mainLayer);

            this.dot = game.Content.Load<Texture2D>("Sprites//Dot");
            this.dotOrigin = new Vector2(this.dot.Width/2, this.dot.Height/2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.mouseController.Update(gameTime);
            this.Toolbar.Update(gameTime);
            this.Infobar.Update(gameTime, mouseController.CursorSimPos);

            getInput();
        }

        private void getInput()
        {
            keyboardController.handleInput();
        }


        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            DrawGrid(spriteBatch);

            base.DrawContent(spriteBatch);

            this.Toolbar.Draw(spriteBatch);

            this.Infobar.Draw(spriteBatch);
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            if (this.Camera.Rotation != 0 || !this.IsGridVisible) { return; } // grid would not be rotated

            Vector2 zero = Utilities.ConvertUnits.ToSimUnits(this.Camera.screenToWorld(Vector2.Zero, new Vector2(1, 1)));
            zero.X -= (int)(zero.X) + 0.5f;
            zero.Y -= (int)(zero.Y) + 0.5f;
            Vector2 screenZero = Utilities.ConvertUnits.ToDisplayUnits(-zero * this.Camera.Zoom);
            float delta = Math.Abs(Utilities.ConvertUnits.ToDisplayUnits(1) * this.Camera.Zoom);

            Rectangle dest;

            spriteBatch.Begin();

                dest = new Rectangle(0, this.viewport.Height/2, 1, (int)(this.viewport.Height));
                for (float x = screenZero.X; x < this.viewport.Width; x += delta) 
                {
                    dest.X = (int)x;
                    spriteBatch.Draw(this.dot, dest, null, this.gridColor, 0, this.dotOrigin, SpriteEffects.None, 0.0f);
                }

                dest = new Rectangle(this.viewport.Width/2, 0, (int)(this.viewport.Width), 1);
                for (float y = screenZero.Y; y < this.viewport.Height; y += delta)
                {
                    dest.Y = (int)y;
                    spriteBatch.Draw(this.dot, dest, null, this.gridColor, 0, this.dotOrigin, SpriteEffects.None, 0.0f);
                }

            spriteBatch.End();
        }

        /// <summary>
        /// resets the camera to the origin of the level
        /// </summary>
        public void ResetCamera()
        {
            this.Camera.Move(Vector2.Zero);
            this.Camera.Rotation = 0.0f;
            this.Camera.Zoom = 0.5f;
        }
    }
}
