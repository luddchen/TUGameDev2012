using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        public PartsCombinedStateMachine(IBody body)
            : base(body)
        {
        }
    }
}
