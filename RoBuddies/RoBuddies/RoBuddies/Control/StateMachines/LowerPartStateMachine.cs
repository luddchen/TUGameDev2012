using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class LowerPartStateMachine : StateMachine
    {
        public LowerPartStateMachine(IBody body)
            : base(body)
        {
        }
    }
}
