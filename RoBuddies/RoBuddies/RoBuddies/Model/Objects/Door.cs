﻿using System;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Robuddies.Interfaces;

namespace RoBuddies.Model.Objects
{
    class Door : PhysicObject, ISwitchable
    {
        private bool isActivated = false; //[combined robot && isSwitchalbe] to activate it.
        private bool isSwitchedOn = true; //(key || switch) to change it to true if needed.

        private Texture2D doorOpenTex;

        public bool IsSwitchedOn
        {
            get { return isSwitchedOn; }
        }


        public Door(Vector2 pos, Vector2 size, Color color, Level level, Game game, bool switchable)
            : base(level)
        {
            Texture2D doorTex = game.Content.Load<Texture2D>("Sprites//door_locked");
            defineTextures(doorTex, doorTex, doorTex);
            doorOpenTex = game.Content.Load<Texture2D>("Sprites//door_closed");
            //defineTextures(doorOpenTex, doorOpenTex, doorOpenTex);

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;

            if (switchable)
            {
                this.isSwitchedOn = false;
            }
            this.Color = Color.Brown;

            this.BodyType = BodyType.Static;
            
            this.Friction = 10f;
        }

        /// <summary>
        /// change the property of door so that only combined robot can go through
        /// bool value show the state of the robot, combined robot is true, seperate robot is false
        /// TODO: call this function when the robot combining state changes
        /// </summary>
        public bool activate
        {
            get { return isActivated; }
            set { 
                if(!value)
                {   
                    isActivated = false;
                }
                else if (isSwitchedOn)
                {
                    isActivated = value;
                }
            }
        }

        public void switchOn()
        {            
            isSwitchedOn = true;
            this.Texture = doorOpenTex;
        }        
    }
}
