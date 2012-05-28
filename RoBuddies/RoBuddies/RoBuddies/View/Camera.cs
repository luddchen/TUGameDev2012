using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.View
{

    /// <summary>
    /// representation of a global camera
    /// </summary>
    class Camera
    {

        /// <summary>
        /// global position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// global zoom
        /// </summary>
        public float Zoom { get; set; }

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
        }

        /// <summary>
        /// gets the View Matrix according given parallax
        /// </summary>
        /// <param name="parallax">parallax value</param>
        /// <returns>view matrix</returns>
        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateScale(1, 1, 1);
        }
    }
}
