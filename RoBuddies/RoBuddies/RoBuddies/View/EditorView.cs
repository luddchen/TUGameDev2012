using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using RoBuddies.Model.Serializer;

namespace RoBuddies.View
{
    class EditorView : HUD.HUDLevelView
    {
        MouseController mouseController;

        public EditorToolbar Toolbar;

        private KeyboardState oldKeyboardState;

        public HUDMouse Mouse;

        public HUDTexture UpArrow;
        public HUDTexture DownArrow;
        public HUDTexture LeftArrow;
        public HUDTexture RightArrow;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();
            if (this.UpArrow != null) this.UpArrow.Position = new Vector2(this.viewport.Width / 2, 15);
            if (this.DownArrow != null) this.DownArrow.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height - 15);
            if (this.LeftArrow != null) this.LeftArrow.Position = new Vector2(15, this.viewport.Height/2);
            if (this.RightArrow != null) this.RightArrow.Position = new Vector2(this.viewport.Width - 15, this.viewport.Height / 2);

            if (this.Toolbar != null) { this.Toolbar.Viewport = this.viewport; }
        }

        public EditorView(RoBuddies game)
            : base(game)
        {
            this.Toolbar = new EditorToolbar(game);

            this.Mouse = new HUDMouse(game.Content);
            this.AllElements.Add(this.Mouse);
            this.mouseController = new MouseController(this, this.Mouse);

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

            Layer mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
            this.Level.AddLayer(mainLayer);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.mouseController.Update(gameTime);
            this.Toolbar.Update(gameTime);

            KeyboardState newKeyboardState = Keyboard.GetState();

            oldKeyboardState = newKeyboardState;
        }


        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            base.DrawContent(spriteBatch);
            this.Toolbar.Draw(spriteBatch);
        }
    }
}
