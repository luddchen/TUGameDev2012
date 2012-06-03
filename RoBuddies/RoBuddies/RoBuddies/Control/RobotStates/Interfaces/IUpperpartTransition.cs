﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface IUpperpartTransition
    {
        void ToClimbing();
        void ToCombine();
        void ToFalling();
        void ToShooting();
        void ToWaiting();
    }
}