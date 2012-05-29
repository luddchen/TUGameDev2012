using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;
using RoBuddies.Model.Objects;

namespace RoBuddies.View
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
        public float Scale { get; set; } // <--- please use ConvertUnits Utility instead (thomas)

        /// <summary>
        /// the game
        /// </summary>
        public RoBuddies Game { get; set; }

        /// <summary>
        /// the active camera
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public LevelView(RoBuddies game)
        {
            this.Game = game;
            this.Camera = new Camera();
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            Scale = 1;
            this.Level = new Level(new Vector2(0, -9.8f));

            //  some testing code here --------------------------------------------------------------------------

                // body1
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Wall wall1 = new Wall(new Vector2(550f, 200f), new Vector2(80f, 80f), Color.YellowGreen, square, this.Level);
                    wall1.BodyType = BodyType.Dynamic;

                // body 2
                    Wall wall2 = new Wall(new Vector2(480f, 380f), new Vector2(100f, 100f), Color.Tomato, square, this.Level);
                    
                // layer
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    mainLayer.AllObjects.Add(wall1);
                    mainLayer.AllObjects.Add(wall2);
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
                this.Game.SpriteBatch.Draw( body.Texture,
                                            new Vector2(body.Position.X, body.Position.Y) * Scale,
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
