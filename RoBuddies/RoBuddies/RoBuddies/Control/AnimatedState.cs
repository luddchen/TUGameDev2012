using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control
{
    class AnimatedState : State
    {
        public AnimatedState(String name, Texture2D texture, StateMachine machine)
        {
            this.Name = name;
            this.Texture = texture;
            this.StateMachine = machine;
        }

    }
}
