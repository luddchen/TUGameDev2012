using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class HeadStateMachine : StateMachine
    {
        public HeadStateMachine(IBody body)
            : base(body)
        {
        }
    }
}
