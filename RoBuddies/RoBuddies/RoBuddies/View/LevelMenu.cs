
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.View.HUD;
using RoBuddies.View.MenuPages;

namespace RoBuddies.View
{
    class LevelMenu : HUDMenu
    {
        public HUDTexture play;
        public HUDTexture reload;
        public HUDTexture rewind;
        public HUDTexture forward;
        public HUDTexture help;
        public HUDTexture info;
        public HUDTexture options;
        public HUDTexture quit;
        public HUDTexture chooser;

        public LevelMainMenu quitMenu { get; set; }
        public LevelMainMenu optionMenu { get; set; }
        public LevelMainMenu helpMenu { get; set; }
        public LevelMainMenu infoMenu { get; set; }
        public LevelMainMenu chooserMenu { get; set; }
        public LevelMainMenu mainMenu { get; set; }

        private float firstLine;
        private float secondLine;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (this.AllElements != null && this.AllElements.Count > 0)
            {
                this.AllElements[0].Position = new Vector2(this.Viewport.Width * 0.5f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);
                this.AllElements[1].Position = new Vector2(this.Viewport.Width * 0.25f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);
                this.AllElements[2].Position = new Vector2(this.Viewport.Width * 0.75f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);

                if (reload != null) { reload.Position = new Vector2(32, firstLine); }
                if (rewind != null) { rewind.Position = new Vector2(64, firstLine + 2); }
                if (forward != null) { forward.Position = new Vector2(96, firstLine + 2); }

                if (play != null) { play.Position = new Vector2(40, secondLine); }

                if (chooser != null) { chooser.Position = new Vector2(this.Viewport.Width * 0.4f, secondLine); }
                if (help != null) { help.Position = new Vector2(this.Viewport.Width * 0.5f, secondLine); }
                if (info != null) { info.Position = new Vector2(this.Viewport.Width * 0.6f, secondLine); }
                if (options != null) { options.Position = new Vector2(this.Viewport.Width * 0.7f, secondLine); }
                if (quit != null) { quit.Position = new Vector2(this.Viewport.Width - 40, secondLine); }
            }
        }

        public LevelMenu(RoBuddies game) : base(game)
        {
            firstLine = 32;
            secondLine = MenuPageTopExtraBorder + MenuPageBorder - 32;
            // some decoration -------------------------------------------------------------------
            this.AllElements.Add(new HUDString( "RoBuddies", this.Game.Content));
            this.AllElements[0].Color = HeadLineColor;
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));


            reload = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//reload_64"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(reload);

            rewind = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//rewind_64"), null, 50, 50, null, null, null, this.Game.Content);
            this.AllElements.Add(rewind);

            forward = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//forward_64"), null, 50, 50, null, null, null, this.Game.Content);
            this.AllElements.Add(forward);


            play = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//play_64"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(play);

            chooser = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Folder_Open"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(chooser);

            help = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Tip"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(help);

            info = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Info"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(info);

            options = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Tools"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(options);

            quit = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Close"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(quit);

            quitMenu = new QuitMenu(this, this.Game.Content);
            optionMenu = new OptionMenu(this, this.Game.Content);
            helpMenu = new HelpMenu(this, this.Game.Content);
            infoMenu = new InfoMenu(this, this.Game.Content);
            chooserMenu = new LevelChoiceMenu(this, this.Game.Content);
            mainMenu = new LevelMainMenu(this, this.Game.Content);

            this.DefaultPage = chooserMenu;
            // end decoration -------------------------------------------------------------------
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
