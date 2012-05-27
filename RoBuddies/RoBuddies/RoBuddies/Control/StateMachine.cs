using System;
using System.Collections.Generic;

using RoBuddies.Model;

namespace RoBuddies.Control
{
    class StateMachine
    {
        public State CurrentState;

        public List<State> AllStates;

        public IBody body;


        public StateMachine(IBody body)
        {
            this.body = body;
            this.AllStates = new List<State>();
        }

        public void SwitchToState(String stateName)
        {
        }

    }
}
