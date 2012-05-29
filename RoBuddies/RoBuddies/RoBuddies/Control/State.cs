using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;

using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control
{
    abstract class State
    {
        public String Name { get; set; }

        public Texture2D Texture { get; set; }

        public StateMachine StateMachine { get; set; }
    }
}
