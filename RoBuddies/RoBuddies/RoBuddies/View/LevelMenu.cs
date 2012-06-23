
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
                this.AllElements[0].Position = new Vector2(this.Viewport.Width / 2, (MenuPageTopExtraBorder + MenuPageBorder) / 2);
                this.AllElements[1].Position = new Vector2(MenuPageTopExtraBorder * 3, (MenuPageTopExtraBorder + MenuPageBorder) / 2);
                this.AllElements[2].Position = new Vector2(this.Viewport.Width - MenuPageTopExtraBorder * 3, (MenuPageTopExtraBorder + MenuPageBorder) / 2);
            }
        }

        public LevelMenu(RoBuddies game) : base(game)
        {

            // some decoration -------------------------------------------------------------------
            this.AllElements.Add(new HUDString( "RoBuddies", this.Game.Content));
            this.AllElements[0].Color = HeadLineColor;
            this.AllElements.Add(new HUDTexture(this.Game.Content));
            this.AllElements.Add(new HUDTexture(this.Game.Content));

            QuitMenu quitMenu = new QuitMenu(this, this.Game.Content);
            OptionMenu optionMenu = new OptionMenu(this, this.Game.Content);
            HelpMenu helpMenu = new HelpMenu(this, this.Game.Content);
            LevelMainMenu mainMenu = new LevelChoiceMenu(this, this.Game.Content);

            mainMenu.quitPage = quitMenu;
            mainMenu.optionPage = optionMenu;
            mainMenu.helpPage = helpMenu;

            this.DefaultPage = mainMenu; //mainMenu ;
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
