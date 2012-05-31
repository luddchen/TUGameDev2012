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
    class MainMenu : HUDMenuPage
    {
        private HUDString editor;
        private HUDString help;  
        private HUDString options; 
        private HUDString quit;

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }
        public HUDMenuPage helpPage { get; set; }

        public override void OnViewPortResize()
        {
            if (editor != null) { editor.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.2f); }
            if (help != null) { help.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.4f); }
            if (options != null) { options.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.6f); }
            if (quit != null) { quit.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.8f); }
        }

        public MainMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            editor = new HUDString("Editor", null, null, null, 0.7f, null, content);
            this.AllElements.Add(editor);
            this.ChoiceList.Add(editor);

            help = new HUDString("Help", null, null, null, 0.7f, null, content);
            this.AllElements.Add(help);
            this.ChoiceList.Add(help);

            options = new HUDString("Options", null, null, null, 0.7f, null, content);
            this.AllElements.Add(options);
            this.ChoiceList.Add(options);

            quit = new HUDString("Quit", null, null, null, 0.7f, null, content);
            this.AllElements.Add(quit);
            this.ChoiceList.Add(quit);

            this.ActiveElement = editor;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == editor)
                    {
                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Editor);
                    }
                    if (this.ActiveElement == quit)
                    {
                        this.Menu.ActivePage = this.quitPage;
                    }

                    if (this.ActiveElement == options)
                    {
                        this.Menu.ActivePage = this.optionPage;
                    }

                    if (this.ActiveElement == help)
                    {
                        this.Menu.ActivePage = this.helpPage;
                    }
                }
            }
        }

    }
}
