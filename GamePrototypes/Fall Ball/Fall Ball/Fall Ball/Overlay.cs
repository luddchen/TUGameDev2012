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
        private int fontSpacing = 4;
        private SpriteFont font;
        private SpriteFont smallFont;

        // Some strings and positioning data
        string bottomCenterString = "center text";
        string bottomString = "bottom text 1";
        string bottomString2 = "bottom text 2";
        string centerString = "center text";
        string locatedString = "";

        Vector2 bottomCenterPos = new Vector2(0, 0);
        Vector2 bottomPos = new Vector2(0,0);
        Vector2 bottomPos2 = new Vector2(0, 0);
        Vector2 centerPos = new Vector2(0,0);
        Vector2 locatedPos = new Vector2(0, 0);

        float bouttomCenterSize = 1f;
        float bottomSize = 0.66f;
        float bottomSize2 = 0.66f;
        float centerSize = 1.32f;
        float locatedSize = 1.0f;

        int centerTimeout = 0;
        int locatedTimeout = 0;

        int maxTimer = 150;

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
                bottomCenterPos.X = titleSafe.X + titleSafe.Width / 2;
                bottomCenterPos.Y = titleSafe.Y + titleSafe.Height;
                bottomPos.X = titleSafe.X + titleSafe.Width / 3;
                bottomPos.Y = titleSafe.Y + titleSafe.Height;
                bottomPos2.X = titleSafe.X + titleSafe.Width * 2 / 3;
                bottomPos2.Y = titleSafe.Y + titleSafe.Height;
            }
        }

        public String CenterString
        {
            get { return centerString; }
            set { centerString = value; centerTimeout = maxTimer; }
        }

        public String BottomCenterString
        {
            get { return bottomCenterString; }
            set { bottomCenterString = value; }
        }

        public String BottomString
        {
            get { return bottomString; }
            set { bottomString = value; }
        }

        public String BottomString2
        {
            get { return bottomString2; }
            set { bottomString2 = value; }
        }

        public string LocatedString
        {
            get { return locatedString; }
            set { locatedString = value; locatedTimeout = maxTimer;  }
        }

        public float CenterSize
        {
            get { return centerSize; }
            set { centerSize = value; }
        }

        public float ButtomSize
        {
            get { return bottomSize; }
            set { bottomSize = value; }
        }

        public float ButtomSize2
        {
            get { return bottomSize2; }
            set { bottomSize2 = value; }
        }

        public float LocatedSize
        {
            get { return locatedSize; }
            set { locatedSize = value; }
        }

        public Vector2 LocatedPos
        {
            get { return locatedPos; }
            set { locatedPos = value; }
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
            if (centerTimeout > 0)
            {
                centerTimeout--;
            }
            else
            {
                centerString = "";
            }

            if (locatedTimeout > 0)
            {
                locatedTimeout--;
            }
            else
            {
                locatedString = "";
            }
        }

        #endregion

        #region Rendering

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, bottomCenterString, bottomCenterPos, foreground,
                0, font.MeasureString(bottomCenterString), bouttomCenterSize, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, bottomString, bottomPos, foreground,
                0, font.MeasureString(bottomString), bottomSize, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, bottomString2, bottomPos2, foreground,
                0, font.MeasureString(bottomString2), bottomSize2, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, centerString, centerPos + font.MeasureString(centerString) / 2, foregroundAlpha,
                0, font.MeasureString(centerString), centerSize, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, locatedString, locatedPos + font.MeasureString(locatedString) / 2, foregroundAlpha,
                0, font.MeasureString(locatedString), locatedSize, SpriteEffects.None, 0.5f);

            spriteBatch.End();
        }

        #endregion
    }
}