using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RoBuddies.Control;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing animated objects with physical behavior
    /// </summary>
    class AnimatedPhysicObject : PhysicObject
    {
        public StateMachine StateMachine { get; set; }


        /// <summary>
        /// update the object
        /// </summary>
        /// /// <param name="gametime">time of game</param>
        public void Update(GameTime gameTime)
        {
        }
    }
}
