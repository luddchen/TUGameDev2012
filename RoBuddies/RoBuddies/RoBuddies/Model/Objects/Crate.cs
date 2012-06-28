using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// crate : parameter size decides the size of the crate and the corresponding propeties
    /// </summary>
    class Crate : PhysicObject
    {
        private bool isMoving = false; //statement of the crate, as pulling in prototype
        private bool isHeavyCrate = false; // true for heavy box which fulfill the condition width*height > width*width 

        public Crate(Vector2 pos, Vector2 size, Color color, Level level, Game game)
            : base(pos, size, color, 1, level)
        {

            Texture2D crateTex = game.Content.Load<Texture2D>("Sprites//Crate1");
            defineTextures(crateTex, crateTex, crateTex);
            this.BodyType = BodyType.Dynamic;
            this.FixedRotation = true;
            changeCrateSize(size);
        }

        private void calculateHeaviness()
        {
            if (this.Height * this.Width > this.Width * this.Width)
            {
                isHeavyCrate = true;
            }
        }


        /// <summary>
        /// return the moving state of crate 
        /// </summary>
        public bool IsMoving
        {
            get { return isMoving; }
        }

        /// <summary>
        /// change the property and color of heavy crate when the robot is combined 
        /// bool value show the state of the robot, combined robot is true, seperate robot is false
        /// TODO: call this function when the robot combining state changes
        /// </summary>
        public bool stateUpdate
        {
            get { return isHeavyCrate; }

            set
            {
                if (value && isHeavyCrate)
                {
                    this.BodyType = BodyType.Dynamic;
                    this.Color = Color.BurlyWood;
                }
                else if (isHeavyCrate)
                {
                    this.BodyType = BodyType.Static;
                    this.Color = Color.White;
                }
            }
        }

        /// <summary>
        /// Changes the size of this crate object and the attached rectangle fixture
        /// </summary>
        /// <param name="newSize">the new size of the crate</param>
        public void changeCrateSize(Vector2 newSize)
        {
            this.Width = Math.Max(1, newSize.X);
            this.Height = Math.Max(1, newSize.Y);
            createRectangleFixture(10000000f);
            calculateHeaviness();
        }

        public override void createRectangleFixture(float density)
        {
            float fricFixSize = 0.1f;
            Fixture nonFricFix = FixtureFactory.AttachRectangle(this.Width, this.Height - 2 * fricFixSize, density, new Vector2(0, - 2 * fricFixSize / 2), this);
            nonFricFix.Friction = 0f;
            Fixture fricFixUp = FixtureFactory.AttachRectangle(this.Width - fricFixSize, fricFixSize, density, new Vector2(0, (this.Height - 2 * fricFixSize) / 2), this);
            fricFixUp.Friction = 1f;
            Fixture fricFixDown = FixtureFactory.AttachRectangle(this.Width - fricFixSize, fricFixSize, density, new Vector2(0, -(this.Height - 2 * fricFixSize) / 2), this);
            fricFixDown.Friction = 10000000f;
        }
    }
}
