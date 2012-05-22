using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Robuddies.Utilities
{
    class RaycastUtility
    {
        /*
         * 
         * Returns true, if the line from rayStart to rayEnd is intersecting an object in the world
         * 
         */
        public static bool isIntesectingAnObject(World world, Vector2 rayStart, Vector2 rayEnd)
        {
            bool isIntersecting = false;
            world.RayCast((fixture, point, normal, fraction) =>
            {
                if (fixture != null)
                {
                    isIntersecting = true;
                    return 1;
                }
                return fraction;
            }, rayStart, rayEnd);
            return isIntersecting;
        }

    }

    
}
