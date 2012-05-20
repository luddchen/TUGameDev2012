using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Utilities
{
    public class Camera
    {
        public Camera(Viewport viewport)
        {
            Viewport = viewport;
            Zoom = 0.1f;
        }

        private Viewport viewport;

        public Viewport Viewport 
        { 
            get { return viewport; } 
            set { Origin = new Vector2(value.Width / 2, value.Height / 2); } 
        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Matrix getViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) * 
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) * 
                   Matrix.CreateRotationZ(Rotation) * 
                   Matrix.CreateScale(Zoom, Zoom, 1) * 
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(Viewport.Width / 2.0f, Viewport.Height / 2.0f);
        }
    }
}
