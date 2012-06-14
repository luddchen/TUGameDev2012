
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.View.HUD;
using RoBuddies.View.MenuPages;

namespace RoBuddies.View
{
    class LevelMenu : HUDMenu
    {

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (this.AllElements != null && this.AllElements.Count > 0)
            {
                this.AllElements[0].Position = new Vector2(this.Viewport.Width / 2, MenuPageBorder);
                this.AllElements[1].Position = new Vector2(MenuPageBorder - MenuPageTopExtraBorder, MenuPageBorder - MenuPageTopExtraBorder);
                this.AllElements[2].Position = new Vector2(this.Viewport.Width + MenuPageTopExtraBorder - MenuPageBorder, MenuPageBorder - MenuPageTopExtraBorder);
                this.AllElements[3].Position = new Vector2(MenuPageBorder - MenuPageTopExtraBorder, this.Viewport.Height + MenuPageTopExtraBorder - MenuPageBorder);
                this.AllElements[4].Position = new Vector2(this.Viewport.Width + MenuPageTopExtraBorder - MenuPageBorder, this.Viewport.Height + MenuPageTopExtraBorder - MenuPageBorder);
            }
        }

        public LevelMenu(RoBuddies game) : base(game)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//Menu_Background");

            // some decoration -------------------------------------------------------------------
            this.AllElements.Add(new HUDString( "RoBuddies Menu", this.Game.Content));
            this.AllElements[0].Color = HeadLineColor;
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));

            MainMenu mainMenu = new MainMenu(this, this.Game.Content);
            QuitMenu quitMenu = new QuitMenu(this, this.Game.Content);
            OptionMenu optionMenu = new OptionMenu(this, this.Game.Content);
            HelpMenu helpMenu = new HelpMenu(this, this.Game.Content);
            GameMenu gameMenu = new GameMenu(this, this.Game.Content);

            mainMenu.gamePage = gameMenu;
            mainMenu.quitPage = quitMenu;
            mainMenu.optionPage = optionMenu;
            mainMenu.helpPage = helpMenu;

            this.DefaultPage = mainMenu ;
            this.ActivePage = this.DefaultPage;
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
