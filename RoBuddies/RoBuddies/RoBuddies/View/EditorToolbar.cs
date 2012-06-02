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
        public HUDString gridButton;

        public HUDTexture WallButton;
        public HUDTexture WallButton2;

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
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts\\Font");

            this.background = this.Game.Content.Load<Texture2D>("Sprites//SquareRound");
            this.backgroundColor = new Color(0, 0, 0, 192);
            this.AllElements.Add(new HUDString("Toolbar", null, new Vector2(this.HUD_width / 2, 12), Color.Red, null, 0.5f, 0, game.Content));

            this.resetCamButton = new HUDString("camera reset", font, new Vector2(this.HUD_width * 0.35f, 40), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.resetCamButton);

            this.gridButton = new HUDString("grid", font, new Vector2(this.HUD_width * 0.8f, 40), Color.Red, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.gridButton);

            this.clearButton = new HUDString("clear", font, new Vector2(1 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.clearButton);

            this.loadButton = new HUDString("load", font, new Vector2(2 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.loadButton);

            this.saveButton = new HUDString("save", font, new Vector2(3 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.saveButton);

            this.WallButton = new HUDTexture(game.Content);
            this.WallButton.Texture = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.WallButton.Color = Color.Green;
            this.WallButton.Position = new Vector2(this.HUD_width * 0.3f, 110);
            this.WallButton.Width = this.HUD_width;
            this.WallButton.Height = this.WallButton.Width / 5;
            this.WallButton.Scale = 0.5f;
            this.AllElements.Add(this.WallButton);

            this.WallButton2 = new HUDTexture(game.Content);
            this.WallButton2.Texture = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.WallButton2.Color = Color.Green;
            this.WallButton2.Position = new Vector2(this.HUD_width * 0.7f, this.HUD_height * 0.7f);
            this.WallButton2.Height = this.HUD_height;
            this.WallButton2.Width = this.WallButton2.Height / 5;
            this.WallButton2.Scale = 0.5f;
            this.AllElements.Add(this.WallButton2);
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