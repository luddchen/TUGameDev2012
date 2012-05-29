using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class ExampleStateMachine : StateMachine
    {
        public KeyboardState oldKeyboardState { get; set; }
        public KeyboardState newKeyboardState { get; set; }

        bool switcher = false;

        public ExampleStateMachine(IBody body) : base(body)
        {
            this.Body = body;
            this.AllStates = new List<State>();
        }

        public override void Update(GameTime gameTime)
        {
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            base.Update(gameTime);

            if (newKeyboardState.IsKeyDown(Keys.T) && oldKeyboardState.IsKeyUp(Keys.T) )
            {
                switcher = !switcher;
                if (switcher) 
                {
                    SwitchToState("ExampleState2");
                }
                if (!switcher)
                {
                    SwitchToState("ExampleState1");
                }
            }
        }

    }
}
