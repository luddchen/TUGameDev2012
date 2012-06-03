using System;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

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
