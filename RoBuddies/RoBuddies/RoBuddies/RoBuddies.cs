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

using RoBuddies.View;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies
{
    /// <summary>
    /// main class for robot game
    /// </summary>
    public class RoBuddies : Microsoft.Xna.Framework.Game
    {
        public enum ViewMode{ Level, Editor }

        GraphicsDeviceManager graphics;
        Viewport ViewPort;
        public SpriteBatch SpriteBatch {get; set; } // need that somehow in LevelView, Menu and HUD

        HUDLevelView LevelView;
        HUDMenu LevelMenu;
        HUD LevelHUD; 

        HUDLevelView EditorView;
        HUDMenu EditorMenu;
        HUD EditorHUD;

        HUDLevelView View;
        HUDMenu Menu;
        HUD HUD;

        public ViewMode currentViewMode = ViewMode.Level;


        private MouseController mouseController;

        private bool viewModeChanged = false;

        public RoBuddies()
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
            this.ViewPort = GraphicsDevice.Viewport;

            View.Viewport = this.ViewPort;
            Menu.Viewport = this.ViewPort;
            HUD.Viewport = this.ViewPort;
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
            LevelMenu = new LevelMenu(this);
            LevelHUD = new LevelHUD(this);

            EditorView = new EditorView(this);
            EditorMenu = new EditorMenu(this);
            EditorHUD = new EditorHUD(this);

            SwitchViewMode();
            Window_ClientSizeChanged(null, null);

            this.mouseController = new MouseController(this, this.EditorView.Level, this.EditorView.Camera, ((EditorView)this.EditorView).Mouse );
        }

        /// <summary>
        /// UnloadContent
        /// </summary>
        protected override void UnloadContent(){}

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gametime</param>
        protected override void Update(GameTime gameTime)
        {
            View.Update(gameTime);
            Menu.Update(gameTime);
            HUD.Update(gameTime);
            this.mouseController.Update(gameTime);

            // testing camera
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.I)) { this.View.Camera.Zoom *= 1.01f; }
                if (Keyboard.GetState().IsKeyDown(Keys.O)) { this.View.Camera.Zoom /= 1.01f; }

                if (Keyboard.GetState().IsKeyDown(Keys.K)) { this.View.Camera.Rotation -= 0.005f; }
                if (Keyboard.GetState().IsKeyDown(Keys.L)) { this.View.Camera.Rotation += 0.005f; }

                if (Keyboard.GetState().IsKeyDown(Keys.Left)) { this.View.Camera.Move(this.View.Camera.Position + new Vector2(-1, 0)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) { this.View.Camera.Move(this.View.Camera.Position + new Vector2(+1, 0)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Up)) { this.View.Camera.Move(this.View.Camera.Position + new Vector2(0, -1)); }
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) { this.View.Camera.Move(this.View.Camera.Position + new Vector2(0, +1)); }
            }

        }

        private void SwitchViewMode()
        {
            if (this.currentViewMode == ViewMode.Level)
            {
                this.View = this.LevelView;
                this.Menu = this.LevelMenu;
                this.HUD = this.LevelHUD;
            }
            if (this.currentViewMode == ViewMode.Editor)
            {
                this.View = this.EditorView;
                this.Menu = this.EditorMenu;
                this.HUD = this.EditorHUD;
            }
            this.Menu.IsVisible = false;

            Window_ClientSizeChanged(null, null);
            this.viewModeChanged = false;
        }

        public void SwitchToViewMode( ViewMode mode ) 
        {
            this.currentViewMode = mode;
            this.viewModeChanged = true;
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">gametime</param>
        protected override void Draw(GameTime gameTime)
        {
            this.ViewPort = GraphicsDevice.Viewport;

            GraphicsDevice.Clear(this.View.Level.Background);

            if (this.viewModeChanged)
            {
                SwitchViewMode();
            }

            View.Draw(SpriteBatch);
            Menu.Draw(SpriteBatch);
            HUD.Draw(SpriteBatch);

            GraphicsDevice.Viewport = this.ViewPort;
        }
    }
}
