﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface ILowerPartState
    {
        void ToWalking();
        void ToWaiting();
        void ToPushing();
        void ToCombine();
        void ToJumping();
    }
}
