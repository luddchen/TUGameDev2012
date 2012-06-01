using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;
using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class EditorToolbar : HUD.HUD
    {

        private int HUD_height = 200;
        private int HUD_width = 200;

        public HUDString resetCamButton;
        public HUDString clearButton;
        public HUDString loadButton;
        public HUDString saveButton;

        public override void OnViewPortResize()
        {
            this.viewport.Y = -1 + this.viewport.Height - this.HUD_height;
            this.viewport.X += 1;
            this.viewport.Height = this.HUD_height;
            this.viewport.Width = this.HUD_width;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
        }

        public EditorToolbar(RoBuddies game)
            : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//SquareRound");
            this.backgroundColor = new Color(0, 0, 0, 192);
            this.AllElements.Add(new HUDString("Toolbar", null, new Vector2(this.HUD_width / 2, 12), Color.Red, null, 0.5f, 0, game.Content));
            this.resetCamButton = new HUDString("cam reset", null, new Vector2(this.HUD_width / 2, 40), Color.Black, Color.Gray, 0.4f, 0, game.Content);
            this.AllElements.Add(this.resetCamButton);
            this.clearButton = new HUDString("clear", null, new Vector2(1 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.4f, 0, game.Content);
            this.AllElements.Add(this.clearButton);
            this.loadButton = new HUDString("load", null, new Vector2(2 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.4f, 0, game.Content);
            this.AllElements.Add(this.loadButton);
            this.saveButton = new HUDString("save", null, new Vector2(3 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.4f, 0, game.Content);
            this.AllElements.Add(this.saveButton);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}