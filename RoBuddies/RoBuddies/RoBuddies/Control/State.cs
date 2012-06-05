﻿using System;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control
{
    abstract class State
    {
        public String Name { get; set; }

        public Texture2D Texture { get; set; }

        public StateMachine StateMachine { get; set; }

        /// <summary>
        /// will be called evertime this state is entered 
        /// </summary>
        public virtual void Enter()
        {
            this.StateMachine.Body.Texture = this.Texture;
        }
    }
}
