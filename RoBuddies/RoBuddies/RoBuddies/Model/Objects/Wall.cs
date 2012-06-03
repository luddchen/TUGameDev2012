using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// This class can be used to add crate objects to the level.
    /// </summary>
    class Wall : PhysicObject
    {

        private Fixture wallFixture;

        /// <summary>
        /// constructs a new crate object
        /// </summary>
        /// <param name="pos">the position of the crate object in the level</param>
        /// <param name="size">the width and height of the crate</param>
        /// <param name="color">the color of the crate</param>
        /// <param name="texture">the texture which will be layed over the crate</param>
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
            wallFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }

        /// <summary>
        /// Changes the size of this crate object and the attached rectangle fixture
        /// </summary>
        /// <param name="newSize">the new size of the crate</param>
        public void changeWallSize(Vector2 newSize) {
            this.Width = Math.Max(1, newSize.X);
            this.Height = Math.Max(1, newSize.Y);
            this.DestroyFixture(wallFixture);
            wallFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }

    }
}
