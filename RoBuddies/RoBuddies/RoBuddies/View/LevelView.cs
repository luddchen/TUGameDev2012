using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;

namespace RoBuddies.View
{
    class LevelView
    {
        /// <summary>
        /// scale physic to screen
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport { get; set; }

        /// <summary>
        /// the game
        /// </summary>
        public Game1 Game { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public LevelView(Game1 game)
        {
            this.Game = game;
            this.Viewport = this.Game.GraphicsDevice.Viewport;

            //  some testing code here
            Scale = 50;
            this.Level = new Level( new Vector2( 0, -9.8f));

            Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
            PhysicObject body = new PhysicObject(this.Level);
            body.Position = new Vector2(11.5005f, 10);
            body.BodyType = BodyType.Dynamic;
            FixtureFactory.AttachRectangle(1, 1, 1, Vector2.Zero, body);
            body.Width = 1;
            body.Height = 1;
            body.Texture = square;
            body.Color = Color.YellowGreen;

            PhysicObject body2 = new PhysicObject(this.Level);
            body2.Position = new Vector2(10, 1);
            body2.BodyType = BodyType.Static;
            FixtureFactory.AttachRectangle(3, 3, 1, Vector2.Zero, body2);
            body2.Width = 3;
            body2.Height = 3;
            body2.Texture = square;
            body2.Color = Color.Tomato;

            Layer mainLayer = new Layer("mainLayer", Vector2.Zero, 0.5f, this.Level);
            mainLayer.AllObjects.Add(body);
            mainLayer.AllObjects.Add(body2);
            this.Level.AllLayers.Add(mainLayer);
        }



        public void Update(GameTime gameTime)
        {
            this.Level.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            this.Game.GraphicsDevice.Clear(this.Level.Background);
            foreach (Layer layer in this.Level.AllLayers)
            {
                Matrix matrix = Matrix.CreateTranslation(0, Viewport.Height, 0);
                this.Game.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, matrix);

                foreach (IBody body in layer.AllObjects)
                {
                    this.Game.SpriteBatch.Draw( body.Texture, 
                                                new Vector2(body.Position.X, -body.Position.Y) * Scale, 
                                                null, 
                                                body.Color, 
                                                -body.Rotation, 
                                                body.Origin, 
                                                Scale * body.Width / body.Texture.Width, 
                                                body.Effect, 
                                                layer.LayerDepth);
                }

                this.Game.SpriteBatch.End();
            }
        }
    }
}
