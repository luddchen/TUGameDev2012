using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class LevelHUD : HUD.HUD
    {

        private int HUD_height = 30;
        private int HUD_width = 120;

        private HUDString hudString;

        public override void OnViewPortResize()
        {
            if (hudString != null && hudString.String != "")
            {
                setString(hudString.String);
            }
        }

        public LevelHUD(RoBuddies game)
            : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//SquareRound");
            this.backgroundColor = new Color(0, 0, 0, 160);

            hudString = new HUDString("", null, null, null, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(hudString);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void setString(String hudString) {
            if (hudString != "")
            {
                this.isVisible = true;
                this.hudString.String = hudString;
                this.HUD_width = (int)this.hudString.Width;
                this.viewport.Y = 10; //for bottom use: -1 + this.viewport.Height - this.HUD_height;
                this.viewport.X = this.viewport.X + (this.viewport.Width / 2 - (int)this.hudString.Width / 2);
                this.viewport.Height = this.HUD_height;
                this.viewport.Width = (int)this.hudString.Width;
                this.hudString.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height / 2);
                this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
            }
            else
            {
                this.isVisible = false;
            }
        }

    }
}