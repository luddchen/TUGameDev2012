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
        private HUDString timeElapsed;

        private int HUD_height = 30; 

        public override void OnViewPortResize()
        {
            this.viewport.Y = this.viewport.Height - this.HUD_height;
            this.viewport.Height = this.HUD_height; 
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
            if (timeElapsed != null)
            {
                timeElapsed.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height / 2);
            }
        }

        public LevelHUD(RoBuddies game) : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.backgroundColor = new Color(0,0,0,160);

            timeElapsed = new HUDString("", this.Game.Content);
            timeElapsed.Scale = 0.5f;
            this.AllElements.Add(timeElapsed);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeElapsed.String = String.Format("{0:00}", gameTime.TotalGameTime.Hours) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Minutes) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Seconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}