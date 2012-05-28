using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RoBuddies___Editor.Model;

namespace RoBuddies___Editor.View
{
    class MouseView
    {
        private RoBuddiesEditor game;
        private Mouse mouse;

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport { get; set; }

        public MouseView(RoBuddiesEditor game, Mouse mouse)
        {
            this.mouse = mouse;
            this.game = game;
            this.Viewport = this.game.GraphicsDevice.Viewport;
            LoadContent();
        }

        public void LoadContent()
        {
            this.mouse.Texture = game.Content.Load<Texture2D>("Sprites//Cursor");
            Viewport = game.GraphicsDevice.Viewport;
        }

        public void Draw()
        {
            if (this.mouse.IsVisible)
            {
                this.game.GraphicsDevice.Viewport = this.Viewport;
                this.game.SpriteBatch.Begin();
                    this.game.SpriteBatch.Draw(this.mouse.Texture, this.mouse.Position, null, this.mouse.Color, 0f, this.mouse.Origin, 1f, this.mouse.Effect, 0f);
                this.game.SpriteBatch.End();
            }
        }
    }
}
