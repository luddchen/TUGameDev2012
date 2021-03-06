﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// basis class for HeadUpDisplay
    /// </summary>
    class HUD : PlayerControl
    {
        /// <summary>
        /// a background texture , if null there will be no background
        /// </summary>
        protected Texture2D background;

        /// <summary>
        /// internal destination rectangle for background texture, same coordinates as in viewport
        /// </summary>
        protected Rectangle backgroundDest;

        /// <summary>
        /// color for background texture, if background texture is null this color has no effect
        /// </summary>
        protected Color     backgroundColor = Color.White;

        protected Viewport  viewport;
        protected bool      isVisible;

        /// <summary>
        /// contains all elements on this HUD
        /// </summary>
        protected List<IHUDElement> AllElements;

        /// <summary>
        /// called if viewport resized
        /// </summary>
        public virtual void OnViewPortResize() { }

        /// <summary>
        /// called if visibility of the HUD is changed
        /// </summary>
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
        /// visibility of this HUD
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
        /// HUD constructor
        /// </summary>
        /// <param name="game">the game</param>
        public HUD(RoBuddies game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport; // only for initialization, will be overwriten later by game
            this.IsVisible = true;
            this.AllElements = new List<IHUDElement>();
        }

        /// <summary>
        /// updates all elements
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
        /// draw the content in front of background and back of HUD elements
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected virtual void DrawContent(SpriteBatch spriteBatch) { }

        /// <summary>
        /// draw the content in front of background and back of HUD elements
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="rewind">visualisation of rewinding</param>
        protected virtual void DrawContent(SpriteBatch spriteBatch, bool rewind) { }

        /// <summary>
        /// draw all elements and background
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

                spriteBatch.End();

                this.DrawContent(spriteBatch);
                this.Game.GraphicsDevice.Viewport = this.Viewport;

                spriteBatch.Begin();

                foreach (IHUDElement element in AllElements)
                {
                    element.Draw(spriteBatch);
                }

                spriteBatch.End();
            }
        }
    }
}
