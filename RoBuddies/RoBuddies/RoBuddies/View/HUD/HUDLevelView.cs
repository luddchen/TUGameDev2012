using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;
using RoBuddies.Control;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model.Objects;

namespace RoBuddies.View.HUD
{
    public class HUDLevelView : HUD
    {
        protected Texture2D stop;

        protected Layer mainLayer;

        /// <summary>
        /// space between levelbottom and screen bottom
        /// </summary>
        private const float bottomBorder = 30;

        public override void OnViewPortResize()
        {
            if (this.Camera != null)
            {
                this.Camera.Viewport = this.viewport;
            }
        }

        /// <summary>
        /// the active camera
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public HUDLevelView(RoBuddies game)
            : base(game)
        {
            this.backgroundColor = new Color(255, 255, 255, 230);
            this.stop = this.Game.Content.Load<Texture2D>("Sprites//stop");
            this.Camera = new Camera();
            this.Level = new Level(new Vector2(0, -10f));

            // layer
            this.mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
            this.Level.AddLayer(mainLayer);
        }


        /// <summary>
        /// update content
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Level.Update(gameTime);
        }

        /// <summary>
        /// draw a specified layer
        /// </summary>
        /// <param name="layer">layer to draw</param>
        /// <param name="spriteBtach">spritebatch</param>
        public void Draw(Layer layer, SpriteBatch spriteBatch)
        {

            this.Game.GraphicsDevice.Viewport = this.Viewport;

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, this.Camera.GetViewMatrix(layer.Parallax));

            foreach (IBody body in layer.AllObjects)
            {
                if (body.Texture == null) { body.Texture = stop; }
                Vector2 displayPos = ConvertUnits.ToDisplayUnits(body.Position);
                Rectangle dest = new Rectangle(
                    (int)displayPos.X,
                    (int)displayPos.Y,
                    (int)ConvertUnits.ToDisplayUnits(body.Width),
                    (int)ConvertUnits.ToDisplayUnits(body.Height));
                spriteBatch.Draw(body.Texture, dest, null, body.Color, -body.Rotation, body.Origin, body.Effect, layer.LayerDepth);
            }

            spriteBatch.End();
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in this.Level.AllLayers)
            {
                // todo : order layer (layerDepth)

                Draw(layer, spriteBatch);
            }
        }

    }
}
