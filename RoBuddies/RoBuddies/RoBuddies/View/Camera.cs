﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model;
using RoBuddies.Utilities;
using System.Collections.Generic;

namespace RoBuddies.View
{

    /// <summary>
    /// representation of a global camera
    /// </summary>
    class Camera
    {
        private Viewport viewport;
        private float zoom;
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 movingDirection;
        private float movingSpeed;
        private float movingDistance;
        private Rectangle boundingBox;
        private bool useBoundingBox;

        /// <summary>
        /// if the new position from move() is to far away the new position will not be set direct
        /// </summary>
        public float maxDirectMoveDistance = 1;

        /// <summary>
        /// Position of this Cam
        /// </summary>
        public Vector2 Position
        {
            get { return this.position; }
        }

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
        /// set the mode of camera movement ; 
        /// if false camera position is set directly ;
        /// if true camera try to move in a smooth way
        /// </summary>
        public bool SmoothMove { get; set; }

        /// <summary>
        /// creates a new camera object
        /// </summary>
        public Camera()
        {
            this.position = Vector2.Zero;
            this.targetPosition = Vector2.Zero;
            this.movingSpeed = 0;
            this.movingDirection = Vector2.Zero;
            this.movingDistance = 0;
            this.Zoom = 0.65f;
            this.Rotation = 0.0f;
            this.SmoothMove = true;
            this.useBoundingBox = false;
        }

        /// <summary>
        /// transforms a screen position of a layer into the world coordinate system
        /// </summary>
        /// <param name="screenPosition">the specific screen position</param>
        /// <param name="parallax">the parallax of the layer</param>
        /// <returns></returns>
        public Vector2 screenToWorld(Vector2 screenPosition, Vector2 parallax)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(this.GetViewMatrix(parallax)));
        }

        /// <summary>
        /// Transforms a world position into the screen position of a layer
        /// </summary>
        /// <param name="worldPosition">the world position which will be transformed</param>
        /// <param name="parallax">the parallax of the layer</param>
        /// <returns></returns>
        public Vector2 worldToScreen(Vector2 worldPosition, Vector2 parallax)
        {
            return Vector2.Transform(worldPosition, this.GetViewMatrix(parallax));
        }

        /// <summary>
        /// move the camera to given global coordinates
        /// </summary>
        /// <param name="to">target coordinates</param>
        public void Move(Vector2 to)
        {

            if (this.useBoundingBox)
            {
                to.X = MathHelper.Clamp(to.X, this.boundingBox.X, this.boundingBox.X + boundingBox.Width);
                to.Y = MathHelper.Clamp(to.Y, this.boundingBox.Y, this.boundingBox.Y + boundingBox.Height);
            }

            if (this.SmoothMove)
            {
                this.targetPosition = to;
                Vector2 direction = this.targetPosition - this.position;
                this.movingDistance = (float)Math.Sqrt(Math.Pow(direction.X, 2) + Math.Pow(direction.Y, 2));

                if (this.movingDistance > this.maxDirectMoveDistance)
                {
                    this.movingDirection = direction / this.movingDistance;
                    if (this.movingSpeed == 0.0f)
                    {
                        this.movingSpeed += 0.3f;
                    }
                }
                else
                {
                    this.movingSpeed = 0;
                    this.position = this.targetPosition;
                }
            }

            if (!this.SmoothMove)
            {
                this.movingSpeed = 0;
                this.position = to;
            }
        }

        /// <summary>
        /// update to make camera moves more smooth
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public void Update(GameTime gameTime)
        {
            if (this.SmoothMove)
            {
                if (this.movingSpeed > 0.0f)
                {
                    this.position += this.movingSpeed * this.movingDirection;
                    this.movingDistance -= this.movingSpeed;

                    if (this.movingDistance > Math.Pow(this.movingSpeed, 2) * 2)
                    {
                        this.movingSpeed += 0.3f;
                    }
                    else
                    {
                        this.movingSpeed -= 0.3f;
                    }
                }
                else
                {
                    this.movingDirection = Vector2.Zero;
                    this.movingSpeed = 0;
                    this.position = this.targetPosition;
                }
            }
        }

        /// <summary>
        /// gets the View Matrix according given parallax
        /// </summary>
        /// <param name="parallax">parallax value</param>
        /// <returns>view matrix</returns>
        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f))
                    //* Matrix.CreateTranslation(new Vector3(-Origin, 0.0f))            // origin should be new center
                    * Matrix.CreateRotationZ(Rotation)
                    * Matrix.CreateScale(Zoom, Zoom, 1)
                    * Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }


        public void SetBoundingBox(Rectangle boundingBox)
        {
            this.useBoundingBox = true;
            this.boundingBox = boundingBox;
        }

        public void ClearBoundingBox()
        {
            this.useBoundingBox = false;
        }

        /// <summary>
        /// This method calculates and set the bounds for the camera
        /// of the main and back layer of a level.
        /// </summary>
        /// <param name="level">the level which camera bounds will be calculated</param>
        public void SetBoundingBox(Level level) {
            float viewWidth = viewport.Width / (2 * zoom);
            float viewHeight = viewport.Height / (2 * zoom);
            Rectangle bounds = new Rectangle(int.MaxValue, int.MaxValue, 0, 0);
            List<Layer> layers = new List<Layer>();
            layers.Add(level.GetLayerByName("mainLayer"));
            layers.Add(level.GetLayerByName("backLayer"));
            foreach (Layer layer in layers)
            {
                if (layer != null)
                {
                    foreach (IBody body in layer.AllObjects)
                    {
                        float bodyWidth = body.Width / 2;
                        float bodyHeight = body.Height / 2;
                        bounds.X = (int)Math.Min(bounds.X, ConvertUnits.ToDisplayUnits(body.Position.X - bodyWidth) + viewWidth);
                        bounds.Y = (int)Math.Min(bounds.Y, ConvertUnits.ToDisplayUnits(-body.Position.Y - bodyHeight) + viewHeight);
                        bounds.Width = (int)Math.Max(bounds.Width, Math.Abs(bounds.X - ConvertUnits.ToDisplayUnits(body.Position.X + bodyWidth)) - viewWidth);
                        bounds.Height = (int)Math.Max(bounds.Height, Math.Abs(bounds.Y - ConvertUnits.ToDisplayUnits(-body.Position.Y + bodyHeight)) - viewHeight);
                    }
                }
            }
            this.SetBoundingBox(bounds);
        }
    }
}