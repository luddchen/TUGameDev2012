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

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (((BudBudi)level.BudBudi).state == BudBudi.State.Waiting)
                {
                    ((BudBudi)level.BudBudi).setState(BudBudi.State.StartWalking);
                    ((BudBudi)level.BudBudi).WalkDirection = -1; ;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            { 
                if (((BudBudi)level.BudBudi).state == BudBudi.State.Waiting)
                {
                    ((BudBudi)level.BudBudi).setState(BudBudi.State.StartWalking);
                    ((BudBudi)level.BudBudi).WalkDirection = 1; ;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Right) && ((BudBudi)level.BudBudi).WalkDirection == 1)
            {
                if (((BudBudi)level.BudBudi).state == BudBudi.State.Walking)
                {
                    ((BudBudi)level.BudBudi).setState(BudBudi.State.StopWalking);
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Left) && ((BudBudi)level.BudBudi).WalkDirection == -1)
            {
                if (((BudBudi)level.BudBudi).state == BudBudi.State.Walking)
                {
                    ((BudBudi)level.BudBudi).setState(BudBudi.State.StopWalking);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                level.seperate();
            }
            level.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            level.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
