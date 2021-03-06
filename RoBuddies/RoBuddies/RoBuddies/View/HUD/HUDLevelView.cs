﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


using RoBuddies.Model;
using RoBuddies.Utilities;

namespace RoBuddies.View.HUD
{
    class HUDLevelView : HUD
    {
        protected Texture2D stop;

        private Texture2D rewindForeground;

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
        /// true if level should be paused, only camera update
        /// </summary>
        public bool Pause { get; set; }

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
            this.backgroundColor = Color.White;
            this.stop = this.Game.Content.Load<Texture2D>("Sprites//stop");
            this.rewindForeground = game.Content.Load<Texture2D>("Sprites//menu//RewindForeground");
            this.Camera = new Camera();
            this.Level = new Level();
        }


        /// <summary>
        /// update content
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Camera.Update(gameTime);
        }

        /// <summary>
        /// draw a specified layer
        /// </summary>
        /// <param name="layer">layer to draw</param>
        /// <param name="spriteBtach">spritebatch</param>
        public void Draw(Layer layer, SpriteBatch spriteBatch, bool rewind)
        {

            this.Game.GraphicsDevice.Viewport = this.Viewport;

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, this.Camera.GetViewMatrix(layer.Parallax));

            foreach (IBody body in layer.AllObjects)
            {
                if (body.IsVisible)
                {
                    if (body.Texture == null) { body.Texture = stop; }
                    Vector2 displayPos = ConvertUnits.ToDisplayUnits(body.Position);
                    Rectangle dest = new Rectangle(
                        (int)displayPos.X,
                        (int)displayPos.Y,
                        (int)ConvertUnits.ToDisplayUnits(body.Width),
                        (int)ConvertUnits.ToDisplayUnits(body.Height));
                    Color color;
                    if (rewind && layer.Name != "mainLayer")
                    {
                            color = Color.DarkGray;
                    }
                    else
                    {
                        color = body.Color;
                    }
                    spriteBatch.Draw(body.Texture, dest, null, color, -body.Rotation, body.Origin, body.Effect, layer.LayerDepth);
                }
            }

            foreach (IHUDElement hudElement in layer.allLabels)
            {
                hudElement.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        protected override void DrawContent(SpriteBatch spriteBatch, bool rewind)
        {
            foreach (Layer layer in this.Level.AllLayers)
            {
                Draw(layer, spriteBatch, rewind);
            }

            if (rewind)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(this.rewindForeground, this.backgroundDest, Color.White);
                spriteBatch.End();
            }
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in this.Level.AllLayers)
            {
                Draw(layer, spriteBatch, false);
            }
        }
    }
}
