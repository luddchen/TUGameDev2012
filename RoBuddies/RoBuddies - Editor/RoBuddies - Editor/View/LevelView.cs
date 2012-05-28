using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;
using RoBuddies.View;

namespace RoBuddies___Editor.View
{
    public class LevelView
    {
        /// <summary>
        /// space between levelbottom and screen bottom
        /// </summary>
        private const float bottomBorder = 30;
        private Viewport viewport;

        /// <summary>
        /// viewport for this gamecomponent
        /// </summary>
        public Viewport Viewport 
        {
            get { return viewport; }
            set
            {
                this.viewport = value;
                this.Camera.Viewport = this.viewport;
            }
        }

        /// <summary>
        /// scale physic to screen
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// the game
        /// </summary>
        public RoBuddiesEditor Game { get; set; }

        /// <summary>
        /// the active camera
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public LevelView(RoBuddiesEditor game)
        {
            this.Game = game;
            this.Camera = new Camera();
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            Scale = 50;
            this.Level = new Level(new Vector2(0, -9.8f));

            //  some testing code here --------------------------------------------------------------------------

                // body1
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    PhysicObject body1 = new PhysicObject(this.Level);
                    body1.Position = new Vector2(2f, -2f);
                    body1.BodyType = BodyType.Static;
                    FixtureFactory.AttachRectangle(1, 1, 1, Vector2.Zero, body1);
                    body1.Width = 1;
                    body1.Height = 1;
                    body1.Texture = square;
                    body1.Color = Color.YellowGreen;

                // body 2
                    PhysicObject body2 = new PhysicObject(this.Level);
                    body2.Position = new Vector2(10, -8);
                    body2.BodyType = BodyType.Static;
                    FixtureFactory.AttachRectangle(3, 3, 1, Vector2.Zero, body2);
                    body2.Width = 3;
                    body2.Height = 3;
                    body2.Texture = square;
                    body2.Color = Color.Tomato;

                // layer
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    mainLayer.AllObjects.Add(body1);
                    mainLayer.AllObjects.Add(body2);
                    this.Level.AllLayers.Add(mainLayer);
            // end testing code ---------------------------------------------------------------------------------
        }


        /// <summary>
        /// update content
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public void Update(GameTime gameTime)
        {
            this.Level.Update(gameTime);
        }

        /// <summary>
        /// draw a specified layer
        /// </summary>
        /// <param name="layer">layer to draw</param>
        public void Draw(Layer layer) {

            this.Game.GraphicsDevice.Viewport = this.Viewport;

            this.Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, this.Camera.GetViewMatrix(layer.Parallax));

            foreach (IBody body in layer.AllObjects)
            {
                //Console.Out.WriteLine(body.Position);
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

        /// <summary>
        /// draw all layers
        /// </summary>
        public void Draw()
        {
            foreach (Layer layer in this.Level.AllLayers)
            {
                // todo : order layer (layerDepth)

                Draw(layer);
            }
        }
    }
}
