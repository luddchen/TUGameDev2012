using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;
using RoBuddies.View.HUD;
 
namespace RoBuddies.View.MenuPages
{
    class EditorMainMenu : HUDMenuPage
    {
        private HUDString level;

        public override void OnViewPortResize()
        {
            if (level != null) { level.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.2f); }
        }

        public EditorMainMenu(HUDMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            level = new HUDString("Game", null, null, null, null, 0.7f, null, content);
            this.AllElements.Add(level);
            this.ChoiceList.Add(level);

            this.ActiveElement = level;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == level)
                    {
                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Level);
                    }

                }
            }
        }

    }
}
