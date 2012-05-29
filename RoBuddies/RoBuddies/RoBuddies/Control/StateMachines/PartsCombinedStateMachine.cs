﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class PartsCombinedStateMachine : StateMachine
    {
        public PartsCombinedStateMachine(IBody body)
            : base(body)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
