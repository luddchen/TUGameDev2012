using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;
using RoBuddies.View.HUD;
using System.IO;

namespace RoBuddies
{
    /// <summary>
    /// main class for robot game
    /// </summary>
    class RoBuddies : Microsoft.Xna.Framework.Game
    {
        public enum ViewMode{ Level, Editor }

        /// <summary>
        /// the keyboard state before update
        /// </summary>
        public KeyboardState oldKeyboardState;

        /// <summary>
        /// the keyboard state after update
        /// </summary>
        public KeyboardState newKeyboardState;

        /// <summary>
        /// the gamepad state before update
        /// </summary>
        public GamePadState oldGamePadState;

        /// <summary>
        /// the gamepad state after update
        /// </summary>
        public GamePadState newGamePadState;

        GraphicsDeviceManager graphics;
        Viewport ViewPort;
        SpriteBatch SpriteBatch;
        Texture2D splash;
        bool startScreen = true;
        bool started = false;
        Color splashColor = Color.White;

        public HUDLevelView LevelView;
        HUDMenu LevelMenu;

        public HUDLevelView EditorView;
        HUDMenu EditorMenu;

        public HUDLevelView View;
        public HUDMenu Menu;

        ViewMode currentViewMode = ViewMode.Level;

        bool viewModeChanged = false;

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
        }

        /// <summary>
        /// init
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // delete old editor_temp
            if (File.Exists(@".\\editor_temp.json")) {
                File.Delete(@".\\editor_temp.json");
            }
        }

        /// <summary>
        /// LoadContent
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            LevelView = new LevelView(this);
            LevelMenu = new LevelMenu(this);

            EditorView = new EditorView(this);
            EditorMenu = new EditorMenu(this);

            SwitchViewMode();
            Window_ClientSizeChanged(null, null);

            this.LevelMenu.IsVisible = true;

            splash = Content.Load<Texture2D>("Sprites//Menu//splashscreen");
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
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            this.oldGamePadState = this.newGamePadState;
            this.newGamePadState = GamePad.GetState(PlayerIndex.One);

            if (startScreen)
            {
                if (newKeyboardState.GetPressedKeys().Length > 0)
                {
                    startScreen = false;
                }
            }
            
            if (started)
            {
                View.Pause = Menu.IsVisible;

                View.Update(gameTime);
                Menu.Update(gameTime);
            }

        }

        private void SwitchViewMode()
        {
            if (this.currentViewMode == ViewMode.Level)
            {
                this.View = this.LevelView;
                this.Menu = this.LevelMenu;
                this.Menu.IsVisible = false;
            }
            if (this.currentViewMode == ViewMode.Editor)
            {
                this.View = this.EditorView;
                this.Menu = this.EditorMenu;
                this.Menu.IsVisible = false;
            }

            Window_ClientSizeChanged(null, null);
            this.viewModeChanged = false;
        }

        /// <summary>
        /// switch between game and editor
        /// </summary>
        /// <param name="mode">the new viewmode</param>
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

            if (startScreen || !started)
            {
                SpriteBatch.Begin();
                Rectangle dest = new Rectangle(
                            (int)0,
                            (int)0,
                            (int)this.ViewPort.Width,
                            (int)this.ViewPort.Height);
                SpriteBatch.Draw(splash, dest, null, splashColor, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
                SpriteBatch.End();

                if (!startScreen)
                {
                    splashColor.A -= 6;
                    if (splashColor.A < 6)
                    {
                        started = true;
                    }
                }
            }

            if (this.viewModeChanged)
            {
                SwitchViewMode();
            }

            if (started)
            {
                View.Draw(SpriteBatch);
                Menu.Draw(SpriteBatch);
            }

            GraphicsDevice.Viewport = this.ViewPort;
        }
    }
}
