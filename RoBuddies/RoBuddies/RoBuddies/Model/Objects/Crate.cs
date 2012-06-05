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
        private Texture2D crate;
        private bool isMoving = false; //statement of the crate, as pulling in prototype
        private bool isHeavyCrate = false; // true for heavy box which fulfill the condition width*height > width*width 

        public Crate(String text, Vector2 pos, Vector2 size, Color color, World world, Game game)
            : base(world)
        {
            if (text.Equals(""))
            {
                crate = game.Content.Load<Texture2D>("Sprites//Crate2");
            }

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;   
            this.Texture = crate;

            this.BodyType = BodyType.Dynamic;
            this.Color = Color.BurlyWood;

            if (size.Y * size.X > size.X * size.X)
            {
                isHeavyCrate = true;
            }

            this.FixedRotation = true;
            this.Friction = 1f;
            //this.Mass = size.X * size.Y * Int16.MaxValue;
            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
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
                    Color temp = Color.BurlyWood;
                    temp.R /= 2; temp.G /= 2; temp.B /= 2;
                    this.Color = temp; //the crate cannot be moved now
                }
            }
        }
    }
}
