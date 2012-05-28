using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;

namespace RoBuddies.View.MenuPages
{
    class QuitMenu : MenuPage
    {
        private HUDString question; 
        private HUDString yes;
        private HUDString no;

        public override Viewport Viewport
        {
            get { return this.viewPort; }
            set
            {
                this.viewPort = value;
                question.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f);
                yes.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.6f);
                no.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.8f);
            }
        }

        public QuitMenu(Menu menu, ContentManager content)
            : base(menu, content)
        {
            question = new HUDString("realy quit ?", content);
            question.Scale = 0.7f;
            this.AllElements.Add(question);

            yes = new HUDString("YES, i need a short break", content);
            yes.Scale = 0.5f;
            this.AllElements.Add(yes);
            this.ChoiceList.Add(yes);

            no= new HUDString("NO, bring me back to RoBuddies world", content);
            no.Scale = 0.7f;
            this.AllElements.Add(no);
            this.ChoiceList.Add(no);

            this.ActiveElement = no;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == yes)
                    {
                        this.Menu.Game.Exit();
                    }

                    if (this.ActiveElement == no)
                    {
                        this.Menu.IsVisible = false;
                    }
                }
            }
        }

    }
}
