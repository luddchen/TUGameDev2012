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
        Field gamefield;
        Vector2 offset;
        Vector2 minimapOffset;
        float minimapScale;
        Texture2D squareSprite;
        Texture2D circleSprite;
        World world;

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
            // Erstellen Sie einen neuen SpriteBatch, der zum Zeichnen von Texturen verwendet werden kann.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new World(new Vector2(0.0f, 3.0f));

            squareSprite = Content.Load<Texture2D>("Sprites\\Square2");
            circleSprite = Content.Load<Texture2D>("Sprites\\Circle2");

            // TODO: Verwenden Sie this.Content, um Ihren Spiel-Content hier zu laden
            offset = new Vector2(0, 0); // move of the full gamefield
            minimapOffset = new Vector2(650, 350);
            minimapScale = 0.25f;
            gamefield = new Field();

            gamefield.add(new Ball(new Vector2(30, 0), 10.0f, spriteBatch, circleSprite, world));
            gamefield.add(new Ball(new Vector2(200, 0), 15.0f, spriteBatch, circleSprite, world));
            gamefield.add(new Square(new Vector2(50, 100), new Vector2(100, 10), 0.6f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(125, 140), new Vector2(70, 5), 0.05f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(225, 175), new Vector2(150, 10), 0.2f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(320, 250), new Vector2(350, 5), -0.4f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(320, 250), new Vector2(10, 10), -0.4f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(50, 335), new Vector2(7, 50), -0.5f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(200, 400), new Vector2(300, 7), 0.3f, spriteBatch, squareSprite, world));
            gamefield.add(new Square(new Vector2(370, 425), new Vector2(7, 70), 0.9f, spriteBatch, squareSprite, world));
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

            gamefield.draw( offset );
            gamefield.draw( minimapOffset, minimapScale );

            world.Step(0.1f);
            base.Draw(gameTime);
        }
    }
}