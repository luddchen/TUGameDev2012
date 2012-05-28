using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model;

namespace RoBuddies.View
{
    class HUD
    {
        private HUDString timeElapsed;
        private Viewport viewport;
        private Texture2D square;
        private Rectangle squareDest;
        private Color squareColor = new Color(0,0,0,128);

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport
        {
            get { return this.viewport; }
            set
            {
                this.viewport = value;
                this.squareDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
                timeElapsed.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height / 2);
            }
        }

        public List<IHUDElement> AllElements;

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }


        public HUD(Game1 game)
        {
            this.Game = game;
            this.AllElements = new List<IHUDElement>();
            this.square = this.Game.Content.Load<Texture2D>("Sprites//Square");

            timeElapsed = new HUDString("", this.Game.Content);
            timeElapsed.Scale = 0.5f;
            this.AllElements.Add(timeElapsed);


            this.Viewport = this.Game.GraphicsDevice.Viewport;
        }



        public void Update(GameTime gameTime)
        {
            timeElapsed.String = String.Format("{0:00}", gameTime.TotalGameTime.Hours) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Minutes) + ":" + String.Format("{0:00}", gameTime.TotalGameTime.Seconds);

            foreach (IHUDElement element in AllElements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.Game.GraphicsDevice.Viewport = this.Viewport;

            this.Game.SpriteBatch.Begin();

                this.Game.SpriteBatch.Draw(this.square, this.squareDest, this.squareColor);

                foreach (IHUDElement element in AllElements)
                {
                    element.Draw(this.Game.SpriteBatch);
                }

            this.Game.SpriteBatch.End();
        }
    }
}
