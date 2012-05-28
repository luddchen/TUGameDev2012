using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies.View
{
    class Menu
    {
        private Texture2D square;
        private Rectangle squareDest;
        private Viewport viewport;
        private bool isVisible;
        private Color squareColor = new Color(128,128,128,192);
        private KeyboardState oldKeybordState;

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
        public MenuPage ActivePage { get; set; }

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
            this.oldKeybordState = Keyboard.GetState();

            this.DefaultPage = new MenuPage(this.Game.Content);
            this.ActivePage = this.DefaultPage;
        }



        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeybordState = Keyboard.GetState();

            if (currentKeybordState.IsKeyDown(Keys.Escape) && oldKeybordState.IsKeyUp(Keys.Escape)) 
            {
                IsVisible = !IsVisible;
            }

            this.oldKeybordState = currentKeybordState;

            if (this.ActivePage != null)
            {
                this.ActivePage.Update(gameTime);

                this.ActivePage.Viewport = this.Viewport;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                this.Game.GraphicsDevice.Viewport = this.Viewport;
                this.Game.SpriteBatch.Begin();

                this.Game.SpriteBatch.Draw(this.square, this.squareDest, this.squareColor);

                if (this.ActivePage != null)
                {
                    this.ActivePage.Draw(this.Game.SpriteBatch);
                }

                this.Game.SpriteBatch.End();
            }
        }
    }
}
