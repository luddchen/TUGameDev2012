using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Model.Serializer;
using RoBuddies.View;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;

namespace RoBuddies
{
    /// <summary>
    /// main class for robot game
    /// </summary>
    class RoBuddies : Microsoft.Xna.Framework.Game
    {
        public enum ViewMode{ Level, Editor }

        public AudioEngine audioEngine;
        public SoundBank soundBank;
        public WaveBank waveBank;

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

        public GraphicsDeviceManager graphics;
        Viewport ViewPort;
        SpriteBatch SpriteBatch;
        Texture2D splash;
        bool startScreen = true;

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
            graphics.PreferMultiSampling = true;
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
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent
        /// </summary>
        protected override void LoadContent()
        {
            audioEngine = new AudioEngine(Content.RootDirectory + "//Sound//Robuddies.xgs");
            waveBank = new WaveBank(audioEngine, Content.RootDirectory + "//Sound//Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, Content.RootDirectory + "//Sound//Sound Bank.xsb");

            soundBank.PlayCue("BGM_SummerDays");
            audioEngine.GetCategory("Music").SetVolume(1f);
            audioEngine.GetCategory("SFX").SetVolume(3.5f);

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

            if (oldKeyboardState.IsKeyUp(Keys.F1) && newKeyboardState.IsKeyDown(Keys.F1))
            {
                Level loadedLevel = (new LevelReader(this)).readLevel(".\\", "editor_temp.json");
                if (loadedLevel != null)
                {
                    this.EditorView.Level = loadedLevel;
                }
                this.SwitchToViewMode(RoBuddies.ViewMode.Editor);
            }
            
            if (!startScreen)
            {
                View.Pause = Menu.IsVisible;

                View.Update(gameTime);
                Menu.Update(gameTime);
            }

            if (startScreen)
            {
                if (newKeyboardState.GetPressedKeys().Length > 0
                    || newGamePadState.IsButtonDown(Buttons.Start) || newGamePadState.IsButtonDown(Buttons.A)
                    || newGamePadState.IsButtonDown(Buttons.B) || newGamePadState.IsButtonDown(Buttons.X)
                    || newGamePadState.IsButtonDown(Buttons.Y) || newGamePadState.IsButtonDown(Buttons.Start)
                    || newGamePadState.IsButtonDown(Buttons.Back)) // unfortunately there's only this ugly way to detect any pressed gamepad buttons :-(
                {
                    startScreen = false;
                }
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

            if (this.viewModeChanged)
            {
                SwitchViewMode();
            }


            View.Draw(SpriteBatch);

            if (!startScreen)
            {
                Menu.Draw(SpriteBatch);
            }

            GraphicsDevice.Viewport = this.ViewPort;
        }
    }
}
