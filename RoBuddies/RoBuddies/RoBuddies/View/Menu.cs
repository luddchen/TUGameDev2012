using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View
{
    class Menu
    {
        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport { get; set; }

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }


        public Menu(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;
        }



        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
        }
    }
}
