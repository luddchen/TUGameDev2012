using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;

namespace RoBuddies.View.MenuPages
{
    public class MenuPage
    {
        private float activeScale;
        private float animationValue;

        protected Viewport viewPort;
        protected IHUDElement activeElement;

        public List<IHUDElement> AllElements { get; set; }
        public List<IHUDElement> ChoiceList { get; set; }

        public Menu Menu { get; set; }

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

        public virtual Viewport Viewport 
        {
            get { return this.viewPort; }
            set { this.viewPort = value; }
        }

        public MenuPage(Menu menu, ContentManager content)
        {
            this.AllElements = new List<IHUDElement>();
            this.ChoiceList = new List<IHUDElement>();
            this.Menu = menu;
        }

        public virtual void Update(GameTime gameTime)
        {

            if (this.activeElement != null)
            {
                this.activeElement.Scale = this.activeScale * ( 1.2f - (float)Math.Cos(this.animationValue) / 20);
                this.animationValue += (float)(gameTime.ElapsedGameTime.Milliseconds * MathHelper.TwoPi / 700);
                if (this.animationValue > MathHelper.TwoPi) { this.animationValue = 0.0f; }
            }

            foreach (IHUDElement element in this.AllElements)
            {
                element.Update(gameTime);
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (IHUDElement element in this.AllElements)
            {
                element.Draw(spriteBatch);
            }
        }
    }
}
