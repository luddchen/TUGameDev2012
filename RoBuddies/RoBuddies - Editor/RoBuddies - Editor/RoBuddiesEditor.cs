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

using RoBuddies___Editor.View;
using RoBuddies___Editor.Controls;

namespace RoBuddies___Editor
{
    /// <summary>
    /// main class for robot game
    /// </summary>
    public class RoBuddiesEditor : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get; set; } // need that somehow in LevelView, Menu and HUD

        private LevelView LevelView;
        private Menu Menu;
        private HUD HUD;
        private MouseView mouseView;
        private MouseController mouseController;

        public RoBuddiesEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }


        /// <summary>
        /// called if client size change
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Viewport viewport;

            // mouse size
            viewport = graphics.GraphicsDevice.Viewport;
            mouseView.Viewport = viewport;

            // LevelView size
            viewport = graphics.GraphicsDevice.Viewport;
            viewport.Height = viewport.Height - 30;
            LevelView.Viewport = viewport;

            // menu size
            viewport = graphics.GraphicsDevice.Viewport;
            if (viewport.Width > Menu.PreferedWidth)
            {
                viewport.X = viewport.Width / 2 - Menu.PreferedWidth / 2;
                viewport.Width = Menu.PreferedWidth;
            }
            if (viewport.Height > Menu.PreferedHeight)
            {
                viewport.Y = viewport.Height / 2 - Menu.PreferedHeight / 2;
                viewport.Height = Menu.PreferedHeight;
            }
            Menu.Viewport = viewport;

            // HUD size
            viewport = graphics.GraphicsDevice.Viewport;
            viewport.Y = viewport.Height - 30;
            viewport.Height = 30;
            HUD.Viewport = viewport;
        }

        /// <summary>
        /// init
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            LevelView = new LevelView(this);
            Menu = new Menu(this);
            HUD = new HUD(this);
            RoBuddies___Editor.Model.Mouse mouse = new RoBuddies___Editor.Model.Mouse();
            mouseView = new MouseView(this, mouse);
            mouseController = new MouseController(this, null, mouse);

            Window_ClientSizeChanged(null, null);
        }

        /// <summary>
        /// UnloadContent
        /// </summary>
        protected override void UnloadContent() { }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gametime</param>
        protected override void Update(GameTime gameTime)
        {
            LevelView.Update(gameTime);
            Menu.Update(gameTime);
            HUD.Update(gameTime);
            mouseController.Update(gameTime);

            // testing camera
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.I)) { this.LevelView.Camera.Zoom *= 1.01f; }
                if (Keyboard.GetState().IsKeyDown(Keys.O)) { this.LevelView.Camera.Zoom /= 1.01f; }

                if (Keyboard.GetState().IsKeyDown(Keys.K)) { this.LevelView.Camera.Rotation -= 0.005f; }
                if (Keyboard.GetState().IsKeyDown(Keys.L)) { this.LevelView.Camera.Rotation += 0.005f; }

                if (Keyboard.GetState().IsKeyDown(Keys.Left)) { this.LevelView.Camera.Move(this.LevelView.Camera.Position + new Vector2(-1, 0)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) { this.LevelView.Camera.Move(this.LevelView.Camera.Position + new Vector2(+1, 0)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) { this.LevelView.Camera.Move(this.LevelView.Camera.Position + new Vector2(0, -1)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) { this.LevelView.Camera.Move(this.LevelView.Camera.Position + new Vector2(0, +1)); }
            }

        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">gametime</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(this.LevelView.Level.Background);

            LevelView.Draw();
            Menu.Draw(gameTime);
            HUD.Draw(gameTime);
            mouseView.Draw();
        }
    }
}
