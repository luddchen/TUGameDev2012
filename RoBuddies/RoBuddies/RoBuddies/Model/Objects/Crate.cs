using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
        private Fixture crateFixture;

        public Crate(Vector2 pos, Vector2 size, Color color, Level level, Game game)
            : base(level)
        {

            Texture2D crateTex = game.Content.Load<Texture2D>("Sprites//Crate2");
            defineTextures(crateTex, crateTex, crateTex);

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;

            this.BodyType = BodyType.Dynamic;
            this.Color = Color.BurlyWood;

            calculateHeaviness();

            this.FixedRotation = true;
            this.Friction = 100f;
            //this.Mass = size.X * size.Y * Int16.MaxValue;
            crateFixture = FixtureFactory.AttachRectangle(Width, Height, 3, Vector2.Zero, this);
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
                    this.Color = new Color(320, 320, 320, 320);
                    //Color temp = Color.BurlyWood;
                    //temp.R /= 2; temp.G /= 2; temp.B /= 2;
                    //this.Color = temp; //the crate cannot be moved now
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
            this.DestroyFixture(crateFixture);
            crateFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
            calculateHeaviness();
        }
    }
}
