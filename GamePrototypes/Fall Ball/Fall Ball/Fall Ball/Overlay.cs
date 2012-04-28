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

namespace Fall_Ball
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
        private int fontSpacing = 3;
        private SpriteFont font;
        private SpriteFont smallFont;

        // Some strings and positioning data
        string buttomString = "buttom text 1";
        string buttomString2 = "buttom text 2";
        string centerString = "center text";

        Vector2 buttomPos = new Vector2(0,0);
        Vector2 buttomPos2 = new Vector2(0, 0);
        Vector2 centerPos = new Vector2(0,0);

        float buttomSize = 0.66f;
        float buttomSize2 = 0.66f;
        float centerSize = 1.32f;

        TimeSpan elapsedTime = TimeSpan.Zero;

        Color foreground;
        Color foregroundAlpha;

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
                centerPos.X = titleSafe.X + titleSafe.Width / 2;
                centerPos.Y = titleSafe.Y + titleSafe.Height / 2;
                buttomPos.X = titleSafe.X + titleSafe.Width / 3;
                buttomPos.Y = titleSafe.Y + titleSafe.Height;
                buttomPos2.X = titleSafe.X + titleSafe.Width * 2 / 3;
                buttomPos2.Y = titleSafe.Y + titleSafe.Height;
            }
        }

        public String CenterString
        {
            get { return centerString; }
            set { centerString = value; }
        }

        public String ButtomString
        {
            get { return buttomString; }
            set { buttomString = value; }
        }

        public String ButtomString2
        {
            get { return buttomString2; }
            set { buttomString2 = value; }
        }

        public float CenterSize
        {
            get { return centerSize; }
            set { centerSize = value; }
        }

        public float ButtomSize
        {
            get { return buttomSize; }
            set { buttomSize = value; }
        }

        public float BUttomSize2
        {
            get { return buttomSize2; }
            set { buttomSize2 = value; }
        }

        #endregion

        #region Construction and Initialization

        public Overlay(Game game, Color foreground)
            : base(game)
        {
            content = new ContentManager(game.Services);
            content.RootDirectory = "Content";
            this.foreground = foreground;
            this.foregroundAlpha = new Color( foreground.R, foreground.G, foreground.B, 128 );
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

        public override void Update(GameTime gameTime)
        {
        }

        #endregion

        #region Rendering

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, buttomString, buttomPos, foreground,
                0, font.MeasureString(buttomString), buttomSize, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, buttomString2, buttomPos2, foreground,
                0, font.MeasureString(buttomString2), buttomSize2, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, centerString, centerPos + font.MeasureString(centerString) / 2, foregroundAlpha,
                0, font.MeasureString(centerString), centerSize, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }

        #endregion
    }
}