using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    public enum ControlButton { left, right, up, down, jump, use, robot };  // robot means seperate / switch part -> both can put on one key  

    class StateMachine
    {
        protected KeyboardState oldKeyboardState;
        protected KeyboardState newKeyboardState;

        /// <summary>
        /// the current state
        /// </summary>
        public State CurrentState;

        /// <summary>
        /// list of all possible states
        /// </summary>
        public List<State> AllStates;

        /// <summary>
        /// body of this statemachine
        /// </summary>
        public IBody Body;


        public StateMachine(IBody body)
        {
            this.Body = body;
            this.AllStates = new List<State>();
        }

        /// <summary>
        /// switch to an state by reference
        /// </summary>
        /// <param name="state">the new state</param>
        public void SwitchToState(State state)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = state;
            state.Enter();
        }

        /// <summary>
        /// switch to an state by name
        /// if that state doesnt exist, state will not be switched
        /// </summary>
        /// <param name="stateName">name of new state</param>
        public void SwitchToState(String stateName)
        {
            foreach (State state in AllStates)
            {
                if (state.Name == stateName)
                {
                    if (CurrentState != null)
                    {
                        CurrentState.Exit();
                    }
                    CurrentState = state;
                    CurrentState.Enter();
                    break;
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();
        }

        public bool ButtonReleased(ControlButton button) 
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        public bool ButtonPressed(ControlButton button) 
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        public bool ButtonIsDown(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        public bool ButtonIsUp(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        public bool ButtonToggled(ControlButton button) 
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) != oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }


        private Keys getKeyboardKey(ControlButton button)
        {
            Keys key = Keys.Enter;
            switch (button)
            {
                case ControlButton.down : { key = Keys.Down; break; }
                case ControlButton.up   : { key = Keys.Down; break; }
                case ControlButton.left : { key = Keys.Left; break; }
                case ControlButton.right: { key = Keys.Right; break; }
                case ControlButton.jump : { key = Keys.Space; break; }
                case ControlButton.use  : { key = Keys.S; break; }
                case ControlButton.robot: { key = Keys.X; break; }
            }
            return key;
        }

    }
}
