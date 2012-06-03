using System;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

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
