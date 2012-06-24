using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class InfoMenu : LevelMainMenu
    {
        private HUDString info;

        private String infoText = "RoBuddies Info\n"
                                + "==============================\n"
                                + "uni project .. blah\n";


        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (info != null) { info.Position = new Vector2(this.Viewport.Width / 2, info.Height / 2); }
        }

        public InfoMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            info = new HUDString(infoText, null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(info);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
                }
            }

            // Key.down -----------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.down))
            {
                if (info.Position.Y > viewport.Height - info.Height / 2)
                {
                    info.Position = new Vector2(info.Position.X, info.Position.Y - 2);
                }
            } // ------------------------------------------------------------------------------------

            // Key.Up -------------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.up))
            {
                if (info.Position.Y < info.Height / 2)
                {
                    info.Position = new Vector2(info.Position.X, info.Position.Y + 2);
                }
            } // -----------------------------------------------------------------------------------

        }

    }
}
