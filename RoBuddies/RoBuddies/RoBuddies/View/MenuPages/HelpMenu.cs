using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class HelpMenu : LevelMainMenu
    {
        private HUDString help;

        private String helpText = "Welcome to RoBuddies first aid\n"
                                + "==============================\n"
                                + "\n"
                                + "some keys to help you :\n"
                                + "---------------------------------\n"
                                + "<space>      jump\n\n"
                                + "<left>       walk or climb to left\n\n"
                                + "<right>      walk or climb to right\n\n"
                                + "<up>         climb up or use level ending door\n\n"
                                + "<down>       climb down\n\n"
                                + "<x>          separate, unseparate or switch\n"
                                + "             between the robot parts\n\n"
                                + "<s>          use a lever or grab a crate to pull/push\n\n"
                                + "<esc>        open / close menu\n"
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
                                + "only the combined robot can pass the door to next level";
                              

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (help != null) { help.Position = new Vector2(this.Viewport.Width / 2, help.Height / 2); }
        }

        public HelpMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            help = new HUDString(helpText, null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(help);
            OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            if (ButtonIsDown(ControlButton.left) || ButtonIsDown(ControlButton.right))
            {
                base.Update(gameTime);
            }
            else
            {
                animate(gameTime);
            }

            // Key.down -----------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.down))
            {
                if (help.Position.Y > viewport.Height - help.Height / 2)
                {
                    help.Position = new Vector2(help.Position.X, help.Position.Y - 2);
                }
            } // ------------------------------------------------------------------------------------

            // Key.Up -------------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.up))
            {
                if (help.Position.Y < help.Height / 2)
                {
                    help.Position = new Vector2(help.Position.X, help.Position.Y + 2);
                }
            } // -----------------------------------------------------------------------------------

        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(1, 2);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }
    }
}
