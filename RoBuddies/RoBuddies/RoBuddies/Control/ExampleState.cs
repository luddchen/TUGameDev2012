using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;

using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Control
{
    class ExampleState : State
    {

        public ExampleState(String name, Texture2D texture, StateMachine stateMachine)
        {
            this.StateMachine = stateMachine;
            this.Name = name;
            this.Texture = texture;
        }

    }
}
