using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View.MenuPages;

namespace RoBuddies.View.HUD
{
    public class HUDMenu : HUD
    {
        protected int MenuPageBorder = 50;
        protected int MenuPageTopExtraBorder = 15;
        protected Color HeadLineColor = Color.Orchid;

        protected HUDMenuPage activePage;

        protected List<HUDMenuPage> pageHistory;

        public KeyboardState oldKeyboardState { get; set; }
        public KeyboardState newKeyboardState { get; set; }

        /// <summary>
        /// prefered width of menu window
        /// </summary>
        public int PreferedWidth { get; set; }

        /// <summary>
        /// prefered height of menu window
        /// </summary>
        public int PreferedHeight { get; set; }


        public override void OnViewPortResize()
        {
            if (this.ActivePage != null)
            {
                Viewport temp = this.viewport;
                temp.Width -= 2 * MenuPageBorder;
                temp.Height -= 2 * MenuPageBorder;
                temp.X += MenuPageBorder;
                temp.Y += MenuPageBorder + MenuPageTopExtraBorder;
                this.ActivePage.Viewport = temp;
            }
        }


        public override void OnVisibilityChange()
        {
            this.ActivePage = this.DefaultPage;
        }

        /// <summary>
        /// actual menu page
        /// </summary>
        public HUDMenuPage ActivePage
        {
            get { return this.activePage; }
            set
            {
                this.activePage = value;
                this.Viewport = this.viewport; // dirty way to get actual (sub-)viewport
                if (this.activePage != null) { this.pageHistory.Add(this.activePage); }
            }
        }

        /// <summary>
        /// default menu page 
        /// </summary>
        public HUDMenuPage DefaultPage { get; set; }


        public HUDMenu(RoBuddies game)
            : base(game)
        {
            this.pageHistory = new List<HUDMenuPage>();
            this.backgroundColor = Color.Black;
            this.IsVisible = false;
            this.PreferedWidth = 800;
            this.PreferedHeight = 400;
            this.newKeyboardState = Keyboard.GetState();
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            if (this.newKeyboardState.IsKeyDown(Keys.Escape) && this.oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                if (!this.isVisible)
                {
                    this.pageHistory.Clear();
                    this.ActivePage = this.DefaultPage;
                    this.IsVisible = true;
                }
                else
                {
                    if (this.pageHistory.Count > 0) { this.pageHistory.RemoveAt(this.pageHistory.Count - 1); }  // active page should be always here, but nobody knows ..
                    if (this.pageHistory.Count == 0 || this.ActivePage == this.DefaultPage)
                    {
                        IsVisible = !IsVisible;
                    }
                    else
                    {
                        this.ActivePage = this.pageHistory[this.pageHistory.Count - 1];
                    }
                }
            }

            if (this.ActivePage != null && IsVisible)
            {
                this.ActivePage.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (IsVisible)
            {

                if (this.ActivePage != null)
                {
                    //this.Game.GraphicsDevice.Viewport = this.ActivePage.Viewport;
                    //spriteBatch.Begin();

                    this.ActivePage.Draw(spriteBatch);

                    //spriteBatch.End();
                }

            }
        }
    }
}
