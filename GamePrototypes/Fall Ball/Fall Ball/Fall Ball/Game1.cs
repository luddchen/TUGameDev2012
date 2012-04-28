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
        Vector2 offset;
        Vector2 minimapOffset;
        float minimapScale;
        List<Texture2D> textures;

        Level level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            offset = new Vector2(0, 0); // move of the full gamefield
            minimapOffset = new Vector2(650, 350);
            minimapScale = 0.25f;

            level = new Level_1(textures, spriteBatch);
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

            level.gamefield.draw( offset );
            level.gamefield.draw( minimapOffset, minimapScale );

            level.world.Step(0.1f);
            base.Draw(gameTime);
        }
    }
}