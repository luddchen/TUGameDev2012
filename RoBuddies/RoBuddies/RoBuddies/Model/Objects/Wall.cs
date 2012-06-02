﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using RoBuddies.Utilities;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// This class can be used to add wall objects to the level.
    /// </summary>
    class Wall : PhysicObject
    {
        /// <summary>
        /// constructs a new wall object
        /// </summary>
        /// <param name="pos">the position of the wall object in the level</param>
        /// <param name="size">the width and height of the wall</param>
        /// <param name="color">the color of the wall</param>
        /// <param name="texture">the texture which will be layed over the wall</param>
        /// <param name="world">the world object of the physics engine for the physics calculations</param>
        public Wall(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(world)
        {
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.Texture = texture;
            this.BodyType = BodyType.Static;
            this.Friction = 1f;
            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }

    }
}
