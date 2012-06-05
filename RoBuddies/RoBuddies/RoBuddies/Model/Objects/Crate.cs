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
        private bool moveable = true; //current state of the crate; true for all small crates
        private bool isMoving = false; //statement of the crate, as pulling in prototype


        public Crate(Vector2 pos, Vector2 size, Color color, World world, Game game)
            : base(world)
        {
            crate = game.Content.Load<Texture2D>("Sprites//Crate2");

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;   
            //this.Color = color;
            this.Texture = crate;

            if (size.Y > size.X)    // maybe a better criteria is the volume
            {
                moveable = false; //heavy crate
                this.BodyType = BodyType.Static;
            }
            else
            {
                moveable = true; // light crate
                this.BodyType = BodyType.Dynamic;
            }

            this.FixedRotation = true;
            this.Friction = 1f;
            //this.Mass = size.X * size.Y * Int16.MaxValue;
            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

            standardColors();
        }

        private void standardColors()
        {
            if (moveable)
            {
                this.Color = Color.BurlyWood; //Color.CadetBlue; //the crate can be moved now
            }
            else
            {
                Color temp = Color.BurlyWood;
                temp.R /= 2; temp.G /= 2; temp.B /= 2;
                this.Color = temp; //the crate cannot be moved now
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
        /// change the property of heavy crate when the robot is combined 
        /// </summary>
        public bool IsCombinedRobot
        {
            set
            {
                this.BodyType = BodyType.Dynamic;
            }
        }
    }
}
