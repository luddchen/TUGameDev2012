using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface IIPartsCombinedState
    {
        void ToSeperated();
        void ToJumping();
        void ToPushing();
        void ToPulling();
        void ToWaiting();
        void ToWalking();
    }
}
