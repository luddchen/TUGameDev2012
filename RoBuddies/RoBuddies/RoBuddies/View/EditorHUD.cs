using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;
using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    public class EditorHUD : HUD.HUD
    {

        private int HUD_height = 150;
        private int HUD_width = 250;

        public override void OnViewPortResize()
        {
            this.viewport.Y = -10 + this.viewport.Height - this.HUD_height;
            this.viewport.X += 10;
            this.viewport.Height = this.HUD_height;
            this.viewport.Width = this.HUD_width;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
        }

        public EditorHUD(RoBuddies game)
            : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.backgroundColor = new Color(0, 0, 0, 160);
            this.AllElements.Add( new HUDString("Toolbar", null, new Vector2(this.HUD_width/2, this.HUD_height/2), Color.Red, 0.7f, 0, game.Content) );

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