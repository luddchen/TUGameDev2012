using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;

namespace RoBuddies.View.MenuPages
{
    class HelpMenu : MenuPage
    {
        private HUDString help;

        private String helpText = "RoBuddiesR\noBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBu\nddiesRoBuddies\n"
                                + "RoBudd\niesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBu\nddiesRoBuddies\n"
                                + "RoBuddiesRoBuddie\nsRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddies\nRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRo\nBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBudd\niesRoBuddies\n"
                                + "RoBuddiesRoBudd\niesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRo\nBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesR\noBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddi\nesRoBuddiesRoBuddies\n"
                                + "RoBuddiesR\noBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBudd\niesRoBuddies\n"
                                + "RoBuddiesRoBuddiesR\noBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoB\nuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddie\nsRoBuddiesRoBuddies\n"
                                + "RoBuddiesRo\nBuddiesRoBuddiesRoBuddies\n"
                                + "RoBuddiesRoBuddiesRoBudd\niesRoBuddies\n";
                              

        public MenuPage quitPage { get; set; }
        public MenuPage optionPage { get; set; }

        public override Viewport Viewport
        {
            get { return this.viewPort; }
            set
            {
                this.viewPort = value;
                help.Position = new Vector2(this.Viewport.Width / 2, help.Height / 2);
            }
        }

        public HelpMenu(Menu menu, ContentManager content)
            : base(menu, content)
        {
            help = new HUDString(helpText, content);
            help.Scale = 0.5f;
            this.AllElements.Add(help);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
                }
            }

            // Key.down -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Down) ) //&& this.Menu.oldKeyboardState.IsKeyUp(Keys.Down))
            {
                help.Position = new Vector2( help.Position.X, help.Position.Y - 2);
            } // ------------------------------------------------------------------------------------

            // Key.Up -------------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Up) ) //&& this.Menu.oldKeyboardState.IsKeyUp(Keys.Up))
            {
                help.Position = new Vector2(help.Position.X, help.Position.Y + 2);
            } // -----------------------------------------------------------------------------------

        }

    }
}
