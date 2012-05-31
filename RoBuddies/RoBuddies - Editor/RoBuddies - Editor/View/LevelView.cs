using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;
using RoBuddies.View;
using RoBuddies.Model.Objects;
using RoBuddies.Utilities;

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

        // dirty stuff for the presentation:
        public Layer mainLayer;
        private Texture2D background;

        public LevelView(RoBuddiesEditor game)
        {
            this.Game = game;
            this.Camera = new Camera();
            this.Viewport = this.Game.GraphicsDevice.Viewport;
            this.Level = new Level(new Vector2(0, -9.8f));
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//back_1");
            //  some testing code here --------------------------------------------------------------------------

                // body1
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Wall wall1 = new Wall(new Vector2(11.501f, -2f), new Vector2(1f, 1f), Color.YellowGreen, square, this.Level);

                // body 2
                    Wall wall2 = new Wall(new Vector2(10f, -8f), new Vector2(3f, 3f), Color.Tomato, square, this.Level);
                    
                // layer
                    mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
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
            Rectangle backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
            this.Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, this.Camera.GetViewMatrix(layer.Parallax));
            this.Game.SpriteBatch.Draw(this.background, backgroundDest, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            foreach (IBody body in layer.AllObjects)
            {
                Vector2 displayPos = ConvertUnits.ToDisplayUnits(body.Position);
                Rectangle dest = new Rectangle(
                    (int)displayPos.X,
                    (int)displayPos.Y,
                    (int)ConvertUnits.ToDisplayUnits(body.Width),
                    (int)ConvertUnits.ToDisplayUnits(body.Height));
                this.Game.SpriteBatch.Draw(body.Texture, dest, null, body.Color, -body.Rotation, body.Origin, body.Effect, layer.LayerDepth);
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
