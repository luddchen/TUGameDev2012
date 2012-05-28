﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View
{

    /// <summary>
    /// representation of a global camera
    /// </summary>
    class Camera
    {
        private Viewport viewport;
        private float zoom;

        /// <summary>
        /// global position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// origin on screen
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// viewport on screen
        /// </summary>
        public Viewport Viewport
        {
            get { return viewport; }
            set {
                this.viewport = value;
                Origin = new Vector2(value.Width / 2, value.Height / 2); 
            }
        }

        /// <summary>
        /// global zoom
        /// </summary>
        public float Zoom 
        {
            get { return this.zoom; }
            set 
            {
                this.zoom = value;
                if (this.zoom > 2.0f) { this.zoom = 2.0f; }  // some bug if zoom is higher than 2
            } 
        }

        /// <summary>
        /// global rotation
        /// </summary>
        public float Rotation { get; set; }


        /// <summary>
        /// creates a new camera object
        /// </summary>
        public Camera()
        {
            this.Position = Vector2.Zero;
            this.Zoom = 1.0f;
            this.Rotation = 0.0f;
        }


        /// <summary>
        /// move the camera to given global coordinates
        /// </summary>
        /// <param name="to">target coordinates</param>
        public void Move(Vector2 to)
        {
            // need here something for smooth camera movement
            // replace following code
            this.Position = to;
        }

        /// <summary>
        /// gets the View Matrix according given parallax
        /// </summary>
        /// <param name="parallax">parallax value</param>
        /// <returns>view matrix</returns>
        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f))
                    * Matrix.CreateTranslation(new Vector3(-Origin, 0.0f))
                    * Matrix.CreateRotationZ(Rotation)
                    * Matrix.CreateScale(Zoom, Zoom, 1)
                    * Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}