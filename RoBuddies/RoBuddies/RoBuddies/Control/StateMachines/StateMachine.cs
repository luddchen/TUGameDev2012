using System;
using System.Collections.Generic;

using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class StateMachine
    {
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
            CurrentState = state;
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
                    CurrentState = state;
                    break;
                }
            }
        }

    }
}
