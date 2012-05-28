using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies___Editor.View
{
    public class Menu
    {
        private float animationValue = 0.0f;
        private Texture2D square;
        private Rectangle squareDest;
        private Viewport viewport;
        private bool isVisible;
        private Color squareColor = Color.Black;
        private KeyboardState oldKeybordState;

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
        public RoBuddiesEditor Game { get; set; }


        public Menu(RoBuddiesEditor game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            this.square = this.Game.Content.Load<Texture2D>("Sprites//Menu//Menu_Background");
            this.IsVisible = false;
            this.oldKeybordState = Keyboard.GetState();


            // some decoration -------------------------------------------------------------------
            this.AllMenuDecoration = new List<IHUDElement>();
            this.AllMenuDecoration.Add(new HUDString( "Robuddies Menu", this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            this.AllMenuDecoration.Add(new HUDTexture(this.Game.Content));
            // end decoration -------------------------------------------------------------------


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

            this.AllMenuDecoration[0].Position = new Vector2(this.Viewport.Width / 2 + (float)(Math.Cos(this.animationValue) * 1), 60 + (float)(Math.Sin(this.animationValue * 2) * 0.5f));
            this.AllMenuDecoration[1].Position = new Vector2(42, 62);
            this.AllMenuDecoration[2].Position = new Vector2(this.Viewport.Width - 42, 42);
            this.AllMenuDecoration[3].Position = new Vector2(52, this.Viewport.Height - 42);
            this.AllMenuDecoration[4].Position = new Vector2(this.Viewport.Width - 72, this.Viewport.Height - 62);
            foreach (IHUDElement element in this.AllMenuDecoration)
            {
                element.Update(gameTime);
            }

            this.animationValue += 0.25f;
            if (this.animationValue > MathHelper.Pi * 2) { this.animationValue = 0.0f; }
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

                if (this.ActivePage != null)
                {
                    this.ActivePage.Draw(this.Game.SpriteBatch);
                }

                this.Game.SpriteBatch.End();
            }
        }
    }
}
