using System;
using System.Collections.Generic;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control
{
    abstract class State
    {
        public String Name { get; set; }

        public StateMachine StateMachine { get; set; }
    }
}
