using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;

namespace RoBuddies.View
{
    class LevelView
    {
        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport { get; set; }

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public LevelView(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            this.Level = new Level( new Vector2( 0, -9.8f));
        }



        public void Update(GameTime gameTime)
        {
            this.Level.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            this.Game.GraphicsDevice.Clear(this.Level.Background);
        }
    }
}
