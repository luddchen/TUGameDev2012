using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;
using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    public class LevelHUD : HUD.HUD
    {

        private int HUD_height = 60;
        private int HUD_width = 120;

        private HUDString timeElapsed;

        public override void OnViewPortResize()
        {
            this.viewport.Y = -1 + this.viewport.Height - this.HUD_height;
            this.viewport.X = this.viewport.Width/2 - this.HUD_width/2;
            this.viewport.Height = this.HUD_height;
            this.viewport.Width = this.HUD_width;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);

            if (this.timeElapsed != null) { this.timeElapsed.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height / 2); }
        }

        public LevelHUD(RoBuddies game)
            : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//SquareRound");
            this.backgroundColor = new Color(0, 0, 0, 160);

            timeElapsed = new HUDString("", null, null, null, null, 0.5f, null, this.Game.Content);
            timeElapsed.Scale = 0.5f;
            this.AllElements.Add(timeElapsed);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeElapsed.String = String.Format("{0:00}", gameTime.TotalGameTime.Hours) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Minutes) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Seconds);
        }

    }
}