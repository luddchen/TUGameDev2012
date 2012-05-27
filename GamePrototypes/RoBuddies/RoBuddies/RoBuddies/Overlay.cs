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

    public class Overlay : DrawableGameComponent
    {
        #region Fields and Properties

        private ContentManager content;
        private SpriteBatch spriteBatch;

        private int fontSpacing = 4;
        private SpriteFont font;
        private SpriteFont smallFont;

        Color menuBackColor = new Color(0, 0, 128, 128);
        Color menuBackColor2 = new Color(128, 128, 128, 128);
        Texture2D menuTex;
        Vector2 menuOrigin;
        bool IsMenuVisible = false;
        int menuDelay = 0;
        public void SwitchMenuVisible()
        {
            if (menuDelay == 0)
            {
                IsMenuVisible = !IsMenuVisible;
                menuDelay = 10;
            }
        }

        string menuString =
                " (space)     : jump"
            + "\n (a)         : use buttons"
            + "\n (s)         : seperate"
            + "\n (left alt)  : switch robot part"
            +" \n (left ctrl) : grab box"
            + "\n (1) - (3)   : choose level"
            + "\n (q)         : quit"
            + "\n (escape)    : this menu";
        Vector2 menuStringPos = new Vector2();
        float menuStringSize = 1.0f;

        string bottomCenterString = "(escape) : controls ";
        Vector2 bottomCenterPos = new Vector2();
        float bouttomCenterSize = 0.5f;

        string centerString = "";
        Vector2 centerPos = new Vector2();
        float centerSize = 1.32f;

        TimeSpan elapsedTime = TimeSpan.Zero;

        Color foreground;

        Rectangle titleSafe;
        Rectangle menuSafe = new Rectangle();
        Rectangle menuSafe2 = new Rectangle();
        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set 
            { 
                titleSafe = value;
                bottomCenterPos.X = titleSafe.X + titleSafe.Width / 2;
                bottomCenterPos.Y = titleSafe.Y + titleSafe.Height;
                menuSafe.X = titleSafe.Width / 2;
                menuSafe.Y = titleSafe.Height / 2;
                centerPos.X = titleSafe.X + titleSafe.Width / 2;
                centerPos.Y = titleSafe.Y + titleSafe.Height / 2;
                menuSafe.Width = (int)(titleSafe.Width * 0.8f);
                menuSafe.Height = (int)(titleSafe.Height * 0.8f);
                menuSafe2.X = titleSafe.Width / 2;
                menuSafe2.Y = titleSafe.Height / 2;
                menuSafe2.Width = (int)(titleSafe.Width * 0.8f + 8);
                menuSafe2.Height = (int)(titleSafe.Height * 0.8f + 8);
                menuStringPos.X = titleSafe.Width / 2;
                menuStringPos.Y = titleSafe.Height / 2;
            }
        }

        public String BottomCenterString
        {
            get { return bottomCenterString; }
            set { bottomCenterString = value; }
        }

        public String CenterString
        {
            get { return centerString; }
            set { centerString = value; }
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
            menuTex = content.Load<Texture2D>("Sprites\\Square");
            menuOrigin = new Vector2(menuTex.Width/2, menuTex.Height/2);

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
            if (menuDelay > 0) { menuDelay--; }
        }

        #endregion

        #region Rendering

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (IsMenuVisible)
            {
                spriteBatch.Draw(menuTex, menuSafe2, null, menuBackColor2, 0, menuOrigin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(menuTex, menuSafe, null, menuBackColor, 0, menuOrigin, SpriteEffects.None, 0.0f); 
                spriteBatch.DrawString(font, menuString, menuStringPos, foreground,
                                        0, font.MeasureString(menuString)/2, menuStringSize, SpriteEffects.None, 0.0f);
            }

            spriteBatch.DrawString(font, centerString, centerPos + font.MeasureString(centerString) / 2, Color.Red,
    0, font.MeasureString(centerString), centerSize, SpriteEffects.None, 0.5f);


            spriteBatch.DrawString(font, bottomCenterString, bottomCenterPos, foreground,
                0, font.MeasureString(bottomCenterString), bouttomCenterSize, SpriteEffects.None, 0.0f);

            spriteBatch.End();
        }

        #endregion
    }
}