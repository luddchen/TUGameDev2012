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

namespace RoBuddies
{
    /// <summary>
    /// Dies ist der Haupttyp für Ihr Spiel
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch { get; set; }

        LevelView LevelView;
        Menu Menu;
        HUD HUD;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
        }

        /// <summary>
        /// on clientsize changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            LevelView.Viewport = GraphicsDevice.Viewport;
            Menu.Viewport = GraphicsDevice.Viewport;
            HUD.Viewport = GraphicsDevice.Viewport;
        }

        /// <summary>
        /// initialize
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
        }

        /// <summary>
        /// UnloadContent
        /// </summary>
        protected override void UnloadContent(){}

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime">gameTime</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LevelView.Update(gameTime);
            Menu.Update(gameTime);
            HUD.Update(gameTime);
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime">gametime.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            LevelView.Draw(gameTime);
            Menu.Draw(gameTime);
            HUD.Draw(gameTime);
        }
    }
}
