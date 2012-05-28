using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class UpperPartStateMachine : StateMachine
    {
        public UpperPartStateMachine(IBody body)
            : base(body)
        {
        }
    }
}
