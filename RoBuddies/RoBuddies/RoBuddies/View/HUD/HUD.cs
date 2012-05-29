using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View.HUD
{
    public class HUD
    {
        protected Texture2D background;
        protected Rectangle backgroundDest;
        protected Color     backgroundColor = Color.White;

        protected Viewport  viewport;
        protected bool      isVisible; 
        protected List<IHUDElement> AllElements;

        public virtual void OnViewPortResize() { }
        public virtual void OnVisibilityChange() { }

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport
        {
            get { return this.viewport; }
            set
            {
                this.viewport = value;
                this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
                OnViewPortResize();
            }
        }

        /// <summary>
        /// visibility of this menu
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set
            {
                this.isVisible = value;
                OnVisibilityChange();
            }
        }

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }

        /// <summary>
        /// HUD constructor
        /// </summary>
        /// <param name="game"></param>
        public HUD(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport; // only for initialization, will be overwriten later by game
            this.IsVisible = true;
            this.AllElements = new List<IHUDElement>();
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public virtual void Update(GameTime gameTime) 
        {
            foreach (IHUDElement element in AllElements)
            {
                element.Update(gameTime);
            }
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="spriteBtach">spritebatch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.Game.GraphicsDevice.Viewport = this.Viewport;

            if (IsVisible)
            {
                spriteBatch.Begin();

                if (this.background != null)
                {
                    spriteBatch.Draw(this.background, this.backgroundDest, this.backgroundColor);
                }

                foreach (IHUDElement element in AllElements)
                {
                    element.Draw(this.Game.SpriteBatch);
                }

                spriteBatch.End();
            }
        }
    }
}
