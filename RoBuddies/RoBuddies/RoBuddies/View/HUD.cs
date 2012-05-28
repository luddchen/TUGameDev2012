using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;

namespace RoBuddies.View
{
    class HUD
    {
        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport { get; set; }

        public List<IHUDElement> AllElements;

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }


        public HUD(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            this.AllElements = new List<IHUDElement>();
        }



        public void Update(GameTime gameTime)
        {
            foreach (IHUDElement element in AllElements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.Game.GraphicsDevice.Viewport = this.Viewport;

            this.Game.SpriteBatch.Begin();

            foreach (IHUDElement element in AllElements)
            {
                element.Draw(this.Game.SpriteBatch);
            }

            this.Game.SpriteBatch.End();
        }
    }
}
