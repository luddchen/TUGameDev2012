using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing animated objects with physical behavior
    /// </summary>
    class AnimatedStaticObject : StaticObject
    {
        public StateMachine StateMachine { get; set; }


        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
