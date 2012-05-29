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

        private String helpText = "Welcome to RoBuddies first aid\n"
                                + "==============================\n"
                                + "\n"
                                + "some keys to help you :\n"
                                + "---------------------------------\n"
                                + "<space> : jump\n"
                                + "<s> : separate or unseparate the parts\n"
                                + "<alt> : switch between the robot parts\n"
                                + "<ctrl> : grab a crate to pull\n"
                                + "<esc> : open / close menu\n"
                                + "<left> : walk to left\n"
                                + "<right> : walk to right\n"
                                + "<a> : use a lever\n"
                                + "\n"
                                + "remember some rules\n"
                                + "----------------------------------\n"
                                + "upper part can't walk without legs\n"
                                + "\n"
                                + "lower part can't climb armless \n"
                                + "\n"
                                + "lower part can jump higher than the combined robot\n"
                                + "\n"
                                + "lower part can't pull crates only push\n"
                                + "\n"
                                + "combined robot can pull crates\n"
                                + "and push bigger crates than lower part\n"
                                + "\n"
                                + "only the combined robot can pass the door to next level\n"
                                + "\n"
                                + "\n"
                                + "\n"
                                + "\n"
                                + "\n"
                                + "\n"
                                + "\n";
                              

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
