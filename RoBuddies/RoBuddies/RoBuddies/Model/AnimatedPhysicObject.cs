using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing animated objects with physical behavior
    /// </summary>
    class AnimatedPhysicObject : PhysicObject
    {
        public StateMachine StateMachine { get; set; }


        public AnimatedPhysicObject(World world) : base(world) { }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
