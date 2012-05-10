using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Robuddies
{
    /// <summary>
    /// game component for a simple text overlay 
    /// </summary>
    public class Overlay : DrawableGameComponent
    {
        #region Fields and Properties

        private ContentManager content;
        private SpriteBatch spriteBatch;

        // For font rendering
        private int fontSpacing = 4;
        private SpriteFont font;
        private SpriteFont smallFont;

        // Some strings and positioning data
        string bottomCenterString = "RoBuddies prototyp";

        Vector2 bottomCenterPos = new Vector2(0, 0);

        float bouttomCenterSize = 0.5f;

        TimeSpan elapsedTime = TimeSpan.Zero;

        Color foreground;

        // for detecting the visible region on the xbox 360 (this is set from Sandbox.cs)
        Rectangle titleSafe;
        /// <summary>
        /// The title safe rectangle used for both the game and the text overlay
        /// </summary>
        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set 
            { 
                titleSafe = value;
                bottomCenterPos.X = titleSafe.X + titleSafe.Width / 2;
                bottomCenterPos.Y = titleSafe.Y + titleSafe.Height;
            }
        }

        public String BottomCenterString
        {
            get { return bottomCenterString; }
            set { bottomCenterString = value; }
        }

        #endregion

        #region Construction and Initialization

        public Overlay(Game game)
            : base(game)
        {
            content = new ContentManager(game.Services);
            content.RootDirectory = "Content";
            this.foreground = Color.White;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = content.Load<SpriteFont>("Fonts\\Font");
            font.Spacing = fontSpacing;
            smallFont = content.Load<SpriteFont>("Fonts\\FontSmall");
        }


        protected override void UnloadContent()
        {
            content.Unload();
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime) { }

        #endregion

        #region Rendering

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, bottomCenterString, bottomCenterPos, foreground,
                0, font.MeasureString(bottomCenterString), bouttomCenterSize, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }

        #endregion
    }
}