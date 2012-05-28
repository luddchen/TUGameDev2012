using System;
using System.Collections.Generic;

using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class StateMachine
    {
        public State CurrentState;

        public List<State> AllStates;

        public IBody Body;


        public StateMachine(IBody body)
        {
            this.Body = body;
            this.AllStates = new List<State>();
        }

        public void SwitchToState(State state)
        {
            CurrentState = state;
        }
    }
}
