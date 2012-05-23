using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Robuddies.Objects;
using Robuddies.Levels;

namespace Robuddies
{

    class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Overlay overlay;
        public Level level;

        Rectangle titleSafe;
        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set { titleSafe = value; }
        }

        public Game1()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            overlay = new Overlay(this);
            Components.Add(overlay);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e) {
            titleSafe.X = graphics.GraphicsDevice.Viewport.X;
            titleSafe.Y = graphics.GraphicsDevice.Viewport.Y;
            titleSafe.Width = graphics.GraphicsDevice.Viewport.Width;
            titleSafe.Height = graphics.GraphicsDevice.Viewport.Height;
            overlay.TitleSafe = titleSafe;
            level.ChangeViewport(graphics.GraphicsDevice.Viewport);
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            level = new Level_1(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level.LoadContent();
            Window_ClientSizeChanged(null, null);
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q) )
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                overlay.SwitchMenuVisible();
            }

            levelChooser();

            level.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear( level.BackgroundColor );
            level.Draw( gameTime );
            base.Draw( gameTime );
        }

        public void levelChooser()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                level = new Level_1(this);
                level.LoadContent();
                Window_ClientSizeChanged(null, null);
                Console.Out.WriteLine("Level_1 loaded");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                level = new Level_2(this);
                level.LoadContent();
                Window_ClientSizeChanged(null, null);
                Console.Out.WriteLine("Level_2 loaded");   
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                level = new Level_3(this);
                level.LoadContent();
                Window_ClientSizeChanged(null, null);
                Console.Out.WriteLine("Level_2 loaded");
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.D4))
            //{
            //    level = new Level_Hospital(this);
            //    level.LoadContent();
            //    Window_ClientSizeChanged(null, null);
            //    Console.Out.WriteLine("Level_Hospital loaded");
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.D5))
            //{
            //    level = new Level_See(this);
            //    level.LoadContent();
            //    Window_ClientSizeChanged(null, null);
            //    Console.Out.WriteLine("Level_See loaded");
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.D6))
            //{
            //    level = new Level_Underwater(this);
            //    level.LoadContent();
            //    Window_ClientSizeChanged(null, null);
            //    Console.Out.WriteLine("Level_Underwater loaded");
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.D7))
            //{
            //    level = new Level_City(this);
            //    level.LoadContent();
            //    Window_ClientSizeChanged(null, null);
            //    Console.Out.WriteLine("Level_City loaded");
            //}
        }
    }
}
