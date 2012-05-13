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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Overlay overlay;
        Level level;

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

            level = new Level_1(this);

        }


        void Window_ClientSizeChanged(object sender, EventArgs e) {
            titleSafe.X = graphics.GraphicsDevice.Viewport.X;
            titleSafe.Y = graphics.GraphicsDevice.Viewport.Y;
            titleSafe.Width = graphics.GraphicsDevice.Viewport.Width;
            titleSafe.Height = graphics.GraphicsDevice.Viewport.Height;
            overlay.TitleSafe = titleSafe;
            level.TitleSafe = titleSafe;
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            level.LoadContent();
            Window_ClientSizeChanged(null, null);
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) )
                this.Exit();

            AnimatedObject obj = (AnimatedObject)level.ControledObject;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (obj.state == AnimatedObject.State.Waiting)
                {
                    obj.state = AnimatedObject.State.StartWalking;
                    obj.DirectionX = -1; ;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (obj.state == AnimatedObject.State.Waiting)
                {
                    obj.state = AnimatedObject.State.StartWalking;
                    obj.DirectionX = 1;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Right) && obj.DirectionX == 1)
            {
                if (obj.state == AnimatedObject.State.Walking)
                {
                    obj.state = AnimatedObject.State.StopWalking;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Left) && obj.DirectionX == -1)
            {
                if (obj.state == AnimatedObject.State.Walking)
                {
                    obj.state = AnimatedObject.State.StopWalking;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if ((obj.state != AnimatedObject.State.Jumping) && (obj.state != AnimatedObject.State.StartJumping) && (obj.state != AnimatedObject.State.StopJumping))
                {
                    obj.state = AnimatedObject.State.StartJumping;
                    if (level.IsSeperated) { obj.DirectionY = 3.5f; }
                    if (!level.IsSeperated) { obj.DirectionY = 2.5f; }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                level.seperate();
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
                if (!(level is Level_1))
                {
                    level = new Level_1(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_1 loaded");
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if (!(level is Level_City))
                {
                    level = new Level_City(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_City loaded");
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                if (!(level is Level_Forest))
                {
                    level = new Level_Forest(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_Forest loaded");
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (!(level is Level_Hospital))
                {
                    level = new Level_Hospital(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_Hospital loaded");
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                if (!(level is Level_See))
                {
                    level = new Level_See(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_See loaded");
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                if (!(level is Level_Underwater))
                {
                    level = new Level_Underwater(this);
                    level.LoadContent();
                    Window_ClientSizeChanged(null, null);
                    Console.Out.WriteLine("Level_Underwater loaded");
                }
            }
        }
    }
}
