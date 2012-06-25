using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Control;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// basis class for Head Up Display Menu Pages
    /// </summary>
    class HUDMenuPage : HUD
    {

        protected Color textColor = Color.White;

        /// <summary>
        /// sclae of the active element
        /// </summary>
        protected float activeScale;

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
        protected List< List<IHUDElement> > ChoiceList;

        protected int ChoiceLine = -1;

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
        public HUDMenuPage(HUDMenu menu, ContentManager content) : base(menu.Game)
        {
            this.ChoiceList = new List< List<IHUDElement> >();
            this.Menu = menu;
        }

        /// <summary>
        /// updates all elements
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            animate(gameTime);

            if (this.ChoiceList.Count > 0)
            {

                if (this.ChoiceList.Count > 1)
                {
                    // Key.down -----------------------------------------------------------------------------
                    if (ButtonPressed(ControlButton.down))
                    {
                        int index = this.ChoiceList[ChoiceLine].IndexOf(this.activeElement);
                        ChoiceLine++;
                        if (ChoiceLine >= this.ChoiceList.Count) { ChoiceLine = 0; }

                        if (this.ChoiceList[ChoiceLine].Count <= index) { index = this.ChoiceList[ChoiceLine].Count - 1; }

                        if (index >= 0)
                        {
                            this.ActiveElement = this.ChoiceList[ChoiceLine][index];
                        }
                    } // ------------------------------------------------------------------------------------

                    // Key.Up -------------------------------------------------------------------------------
                    if (ButtonPressed(ControlButton.up))
                    {
                        int index = this.ChoiceList[ChoiceLine].IndexOf(this.activeElement);
                        ChoiceLine--;
                        if (ChoiceLine < 0) { ChoiceLine = this.ChoiceList.Count - 1; }

                        if (this.ChoiceList[ChoiceLine].Count <= index) { index = this.ChoiceList[ChoiceLine].Count - 1; }

                        if (index >= 0)
                        {
                            this.ActiveElement = this.ChoiceList[ChoiceLine][index];
                        }
                    } // -----------------------------------------------------------------------------------
                }

                // Key.right -----------------------------------------------------------------------------
                if (ButtonPressed(ControlButton.right))
                {
                    int index = this.ChoiceList[ChoiceLine].IndexOf(this.activeElement);
                    if (index >= 0)
                    {
                        index++;
                        if (index >= this.ChoiceList[ChoiceLine].Count) { index = 0; }

                        this.ActiveElement = this.ChoiceList[ChoiceLine][index];
                    }
                } // ------------------------------------------------------------------------------------

                // Key.left -------------------------------------------------------------------------------
                if (ButtonPressed(ControlButton.left))
                {
                    int index = this.ChoiceList[ChoiceLine].IndexOf(this.activeElement);
                    if (index >= 0)
                    {
                        index--;
                        if (index < 0) { index = this.ChoiceList[ChoiceLine].Count - 1; }

                        this.ActiveElement = this.ChoiceList[ChoiceLine][index];
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

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
            if (this.activeElement != null)
            {
                this.activeElement.Scale = this.activeScale;
            }
        }

        protected void addChoiceElement(IHUDElement element, bool addToAllElements) 
        {
            if (addToAllElements) { this.AllElements.Add(element); }

            if (this.ChoiceLine >= 0)
            {
                this.ChoiceList[ChoiceLine].Add(element); 
            }
        }

        protected void addChoiceLine()
        {
            this.ChoiceList.Add(new List<IHUDElement>());
            this.ChoiceLine = this.ChoiceList.Count - 1;
        }

        protected void chooseActiveElement(int line, int position)
        {
            if (line < this.ChoiceList.Count)
            {
                if (position < this.ChoiceList[line].Count)
                {
                    this.ActiveElement = this.ChoiceList[line][position];
                    this.ChoiceLine = line;
                }
            }
        }


        protected void animate(GameTime gameTime)
        {
            if (this.activeElement != null)
            {
                this.activeElement.Scale = this.activeScale * (1.2f - (float)Math.Cos(this.animationValue) / 20);
                this.animationValue += (float)(gameTime.ElapsedGameTime.Milliseconds * MathHelper.TwoPi / 700);
                if (this.animationValue > MathHelper.TwoPi) { this.animationValue = 0.0f; }
            }
        }

    }
}
