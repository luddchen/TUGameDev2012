using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;
using RoBuddies.View.HUD;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// basis class for Head Up Display Menu Pages
    /// </summary>
    public class HUDMenuPage : HUD
    {

        /// <summary>
        /// sclae of the active element
        /// </summary>
        private float activeScale;

        /// <summary>
        /// value to control the animation of active / selected element
        /// </summary>
        private float animationValue;

        /// <summary>
        /// the active / selected element
        /// </summary>
        protected IHUDElement activeElement;

        /// <summary>
        /// list of all elements that can be choosen
        /// </summary>
        protected List<IHUDElement> ChoiceList { get; set; }

        /// <summary>
        /// the Head Up Display Menu containing this page
        /// </summary>
        public HUDMenu Menu { get; set; }

        /// <summary>
        /// get and set the active / selected element
        /// </summary>
        public IHUDElement ActiveElement
        {
            get { return this.activeElement; }
            set 
            {
                if (this.activeElement != null)
                {
                    this.activeElement.Scale = this.activeScale;
                }
                this.activeElement = value;
                this.activeScale = this.activeElement.Scale;
                this.animationValue = 0.0f;
            }
        }

        /// <summary>
        /// constructor for a new Head Up Display Menu Page
        /// </summary>
        /// <param name="menu">the menu containing this page</param>
        /// <param name="content">Content Manager</param>
        public HUDMenuPage(GameMenu menu, ContentManager content) : base(menu.Game)
        {
            this.backgroundColor = new Color(0,0,0,20);
            this.ChoiceList = new List<IHUDElement>();
            this.Menu = menu;
        }

        /// <summary>
        /// updates all elements
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.activeElement != null)
            {
                this.activeElement.Scale = this.activeScale * ( 1.2f - (float)Math.Cos(this.animationValue) / 20);
                this.animationValue += (float)(gameTime.ElapsedGameTime.Milliseconds * MathHelper.TwoPi / 700);
                if (this.animationValue > MathHelper.TwoPi) { this.animationValue = 0.0f; }
            }

            if (this.ChoiceList.Count > 0)   // at least one element
            {

                // Key.down -----------------------------------------------------------------------------
                if (this.Menu.newKeyboardState.IsKeyDown(Keys.Down) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Down))
                {
                    int index = this.ChoiceList.IndexOf(this.activeElement);
                    if (index >= 0)                 // found active elements index
                    {
                        index++;
                        if (index >= this.ChoiceList.Count) { index = 0; } // cycle
                        this.ActiveElement = this.ChoiceList[index];
                    }
                } // ------------------------------------------------------------------------------------

                // Key.Up -------------------------------------------------------------------------------
                if (this.Menu.newKeyboardState.IsKeyDown(Keys.Up) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Up))
                {
                    int index = this.ChoiceList.IndexOf(this.activeElement);
                    if (index >= 0)                 // found active elements index
                    {
                        index--;
                        if (index < 0) { index = this.ChoiceList.Count - 1; } // cycle
                        this.ActiveElement = this.ChoiceList[index];
                    }
                } // -----------------------------------------------------------------------------------

            }
        }

        /// <summary>
        /// draw all elements and background
        /// </summary>
        /// <param name="spriteBtach">spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
