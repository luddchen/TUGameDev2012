using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View.MenuPages;

namespace RoBuddies.View
{
    class Menu
    {
        private const int MenuPageBorder = 50;
        private const int MenuPageTopExtraBorder = 15;
        private Color HeadLineColor = Color.Orchid;

        private MenuPage activePage;
        private Texture2D square;
        private Rectangle squareDest;
        private Viewport viewport;
        private bool isVisible;
        private Color squareColor = Color.Black;

        public KeyboardState oldKeyboardState { get; set; }
        public KeyboardState newKeyboardState { get; set; }

        private List<IHUDElement> AllMenuDecoration;

        /// <summary>
        /// prefered width of menu window
        /// </summary>
        public const int PreferedWidth = 800;

        /// <summary>
        /// prefered height of menu window
        /// </summary>
        public const int PreferedHeight = 400;

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport
        {
            get
            {
                return this.viewport;
            }
            set
            {
                this.viewport = value;
                this.squareDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
                if (this.ActivePage != null)
                {
                    Viewport temp = this.viewport;
                    temp.Width -= 2 * MenuPageBorder;
                    temp.Height -= 2 * MenuPageBorder;
                    temp.X += MenuPageBorder;
                    temp.Y += MenuPageBorder + MenuPageTopExtraBorder;
                    this.ActivePage.Viewport = temp;
                }
            }
        }

        /// <summary>
        /// visibility of this menu
        /// </summary>
        public bool IsVisible 
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
                this.ActivePage = this.DefaultPage;
            }
        }

        /// <summary>
        /// actual menu page
        /// </summary>
        public MenuPage ActivePage 
        {
            get { return this.activePage; }
            set
            {
                this.activePage = value;
                this.Viewport = this.viewport;
            }
        }

        /// <summary>
        /// default menu page 
        /// </summary>
        public MenuPage DefaultPage { get; set; }

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }


        public Menu(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            this.square = this.Game.Content.Load<Texture2D>("Sprites//Menu//Menu_Background");
            this.IsVisible = false;
            this.newKeyboardState = Keyboard.GetState();


            // some decoration -------------------------------------------------------------------
            this.AllMenuDecoration = new List<IHUDElement>();
            this.AllMenuDecoration.Add(new HUDString( "Robuddies Menu", this.Game.Content));
            this.AllMenuDecoration[0].Color = HeadLineColor;
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            // end decoration -------------------------------------------------------------------

            MainMenu mainMenu = new MainMenu(this, this.Game.Content);
            QuitMenu quitMenu = new QuitMenu(this, this.Game.Content);
            OptionMenu optionMenu = new OptionMenu(this, this.Game.Content);
            HelpMenu helpMenu = new HelpMenu(this, this.Game.Content);

            mainMenu.quitPage = quitMenu;
            mainMenu.optionPage = optionMenu;
            mainMenu.helpPage = helpMenu;

            this.DefaultPage = mainMenu ;
            this.ActivePage = this.DefaultPage;
        }



        public void Update(GameTime gameTime)
        {
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            if (this.newKeyboardState.IsKeyDown(Keys.Escape) && this.oldKeyboardState.IsKeyUp(Keys.Escape)) 
            {
                IsVisible = !IsVisible;
            }

            if (this.ActivePage != null && IsVisible)
            {
                this.ActivePage.Update(gameTime);
            }

            this.AllMenuDecoration[0].Position = new Vector2(this.Viewport.Width / 2, MenuPageBorder);
            this.AllMenuDecoration[1].Position = new Vector2(MenuPageBorder - MenuPageTopExtraBorder, MenuPageBorder - MenuPageTopExtraBorder);
            this.AllMenuDecoration[2].Position = new Vector2(this.Viewport.Width + MenuPageTopExtraBorder - MenuPageBorder, MenuPageBorder - MenuPageTopExtraBorder);
            this.AllMenuDecoration[3].Position = new Vector2(MenuPageBorder - MenuPageTopExtraBorder, this.Viewport.Height + MenuPageTopExtraBorder - MenuPageBorder);
            this.AllMenuDecoration[4].Position = new Vector2(this.Viewport.Width + MenuPageTopExtraBorder - MenuPageBorder, this.Viewport.Height + MenuPageTopExtraBorder - MenuPageBorder);
            foreach (IHUDElement element in this.AllMenuDecoration)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                this.Game.GraphicsDevice.Viewport = this.Viewport;
                this.Game.SpriteBatch.Begin();

                    this.Game.SpriteBatch.Draw(this.square, this.squareDest, this.squareColor);

                    foreach (IHUDElement element in this.AllMenuDecoration)
                    {
                        element.Draw(this.Game.SpriteBatch);
                    }

                this.Game.SpriteBatch.End();

                if (this.ActivePage != null)
                {
                    this.Game.GraphicsDevice.Viewport = this.ActivePage.Viewport;
                    this.Game.SpriteBatch.Begin();

                        this.ActivePage.Draw(this.Game.SpriteBatch);

                    this.Game.SpriteBatch.End();
                }

            }
        }
    }
}
