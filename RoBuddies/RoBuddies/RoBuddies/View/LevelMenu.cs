
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
        public HUDTexture editor;
        public HUDTexture rewind;
        public HUDTexture forward;
        public HUDTexture help;
        public HUDTexture info;
        public HUDTexture options;
        public HUDTexture quit;

        public LevelMainMenu quitMenu { get; set; }
        public LevelMainMenu optionMenu { get; set; }
        public LevelMainMenu helpMenu { get; set; }
        public LevelMainMenu infoMenu { get; set; }
        public LevelMainMenu mainMenu { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (this.AllElements != null && this.AllElements.Count > 0)
            {
                this.AllElements[0].Position = new Vector2(this.Viewport.Width * 0.5f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);
                this.AllElements[1].Position = new Vector2(this.Viewport.Width * 0.25f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);
                this.AllElements[2].Position = new Vector2(this.Viewport.Width * 0.75f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.25f);

                if (play != null) { play.Position = new Vector2(40, 40); }

                if (reload != null) { reload.Position = new Vector2(35, 85); }
                if (rewind != null) { rewind.Position = new Vector2(70, 87); }
                if (forward != null) { forward.Position = new Vector2(105, 87); }

                if (editor != null) { editor.Position = new Vector2(this.Viewport.Width * 0.4f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.75f); }
                if (help != null) { help.Position = new Vector2(this.Viewport.Width * 0.5f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.75f); }
                if (info != null) { info.Position = new Vector2(this.Viewport.Width * 0.6f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.75f); }
                if (options != null) { options.Position = new Vector2(this.Viewport.Width * 0.7f, (MenuPageTopExtraBorder + MenuPageBorder) * 0.75f); }

                if (quit != null) { quit.Position = new Vector2(this.Viewport.Width - 40, 40); }
            }
        }

        public LevelMenu(RoBuddies game) : base(game)
        {

            // some decoration -------------------------------------------------------------------
            this.AllElements.Add(new HUDString( "RoBuddies", this.Game.Content));
            this.AllElements[0].Color = HeadLineColor;
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));

            play = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//play_64"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(play);

            reload = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//reload_64"), null, 40, 40, null, 0.7f, null, this.Game.Content);
            this.AllElements.Add(reload);

            editor = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Stationery"), null, 40, 40, null, null, null, this.Game.Content);
            this.AllElements.Add(editor);

            rewind = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//rewind_64"), null, 50, 50, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(rewind);

            forward = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//forward_64"), null, 50, 50, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(forward);

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
            mainMenu = new LevelChoiceMenu(this, this.Game.Content);

            this.DefaultPage = mainMenu;
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
