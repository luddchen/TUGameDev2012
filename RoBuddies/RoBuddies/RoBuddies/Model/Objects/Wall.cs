﻿using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Robuddies.Interfaces;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// This class can be used to add crate objects to the level.
    /// </summary>
    class Wall : PhysicObject, ISwitchable
    {
        private Fixture wallFixture;

        private bool switchable = false;

        /// <summary>
        /// constructs a new crate object
        /// </summary>
        /// <param name="pos">the position of the crate object in the level</param>
        /// <param name="size">the width and height of the crate</param>
        /// <param name="color">the color of the crate</param>
        /// <param name="texture">the texture which will be layed over the crate</param>
        /// <param name="level">the world object of the physics engine for the physics calculations</param>
        public Wall(Vector2 pos, Vector2 size, Color color, Level level, Game game, bool switchAble)
            : base(level)
        {
            Texture2D wallTex = game.Content.Load<Texture2D>("Sprites//WallTest");
            if (size.X > 1)
            {
                wallTex = Utilities.TextureConverter.connectLCR(game.GraphicsDevice, wallTex, wallTex, wallTex, (int)(size.X - 2));
            }
            if (size.Y > 1)
            {
                wallTex = Utilities.TextureConverter.connectTCB(game.GraphicsDevice, wallTex, wallTex, wallTex, (int)(size.Y - 2));
            }

            defineTextures(wallTex, wallTex, wallTex);

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;

            if (switchAble)
            {
                this.switchable = true ;
                Color temp = color;
                temp.R /= 2; temp.G /= 2; temp.B /= 2;
                this.Color = temp;
                this.CollisionCategories = Category.Cat1;
                this.CollidesWith = Category.None;
            }
            else
            {
                this.Color = color;
            }

            this.BodyType = BodyType.Static;
            this.Friction = 10f;
            wallFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

        }

        /// <summary>
        /// Changes the size of this wall object and the attached rectangle fixture
        /// </summary>
        /// <param name="newSize">the new size of the wall</param>
        public void changeWallSize(Vector2 newSize) {
            this.Width = Math.Max(1, newSize.X);
            this.Height = Math.Max(1, newSize.Y);
            this.DestroyFixture(wallFixture);
            wallFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }

        public void switchOn()
        {
            if (switchable)
            {
                Color temp = this.Color;
                temp.R *= 2; temp.G *= 2; temp.B *= 2;
                this.Color = temp;
                //Position += new Vector2(0, 1);
            }
        }  

    }
}
