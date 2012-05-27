using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing objects with physical behavior
    /// </summary>
    class PhysicObject : FarseerPhysics.Dynamics.Body , IBody
    {


        /// <summary>
        /// update the world / level
        /// </summary>
        /// /// <param name="gametime">time of game</param>
        public void Update(GameTime gameTime)
        {
        }
    }
}
