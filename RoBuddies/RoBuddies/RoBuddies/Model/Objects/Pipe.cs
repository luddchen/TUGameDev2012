using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;


namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// pipe line which hangs up on the ceiling
    /// upperPart can climb along it
    /// </summary>
    class Pipe : PhysicObject
    {
        public Pipe(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(world)
        {
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.Texture = texture;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;

            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

        }
    }
}
