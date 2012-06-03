﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace RoBuddies.Model.Objects
{
    class Crate : PhysicObject
    {
        public Crate(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(world)
        {
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;   
            this.Color = color;
            this.Texture = texture;

            this.FixedRotation = true;
            this.BodyType = BodyType.Dynamic;
            this.Friction = 1f;
            this.Mass = size.X * size.Y * Int16.MaxValue;
            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

        }
    }
}
