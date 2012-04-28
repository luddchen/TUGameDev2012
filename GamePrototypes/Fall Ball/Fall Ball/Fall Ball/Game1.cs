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
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace Fall_Ball
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenWidth;
        int screenHeight;

        Vector2 offset;
        Vector2 minimapOffset;
        float gameScale;
        float minimapScale;
        float screenScale;
        List<Texture2D> textures;

        Level level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            screenWidth = graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            screenHeight = graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            screenScale = (float)(screenWidth) / (float)(level.width);
            minimapOffset.X = (float)((float)(screenWidth) - (level.width * minimapScale * screenScale));
            minimapOffset.Y = (float)((float)(screenHeight) - (level.height * minimapScale * screenScale));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Sprites\\Square"));   // textures[0]
            textures.Add(Content.Load<Texture2D>("Sprites\\Circle2"));  // textures[1]
            textures.Add(Content.Load<Texture2D>("Sprites\\Diamond"));  // textures[2]
            textures.Add(Content.Load<Texture2D>("Sprites\\Flower"));   // textures[3]
            textures.Add(Content.Load<Texture2D>("Sprites\\Pepper"));   // textures[4]
            textures.Add(Content.Load<Texture2D>("Sprites\\Smiley"));   // textures[5]
            textures.Add(Content.Load<Texture2D>("Sprites\\Sun"));      // textures[6]

            level = new Level_1(textures, spriteBatch);

            offset = new Vector2(0, 0); // move of the full gamefield

            if ((float)(level.height) / (float)(level.width) > 1.0f)
            {
                minimapScale = 0.2f / ((float)(level.height) / (float)(level.width)); 
            }
            else
            {
                minimapScale = 0.2f;
            }
            gameScale = 1.0f - minimapScale;
            Window_ClientSizeChanged( null, null);  // sets the minimapoffset vector

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            offset.Y =  (float)(screenHeight / 2 - (level.ball1.body.Position.Y + level.ball2.body.Position.Y) * gameScale * screenScale / 2);

            level.gamefield.draw( offset , gameScale * screenScale );
            level.gamefield.draw( minimapOffset, minimapScale * screenScale );

            level.world.Step(0.1f);
            base.Draw(gameTime);
        }
    }
}