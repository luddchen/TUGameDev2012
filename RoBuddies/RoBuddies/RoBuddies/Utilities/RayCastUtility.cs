using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace RoBuddies.Utilities
{

    /// <summary>
    /// This Utility contains functions, which helps to detect object 
    /// in the world with casting rays.
    /// 
    /// Author: thomas
    /// </summary>
    class RayCastUtility
    {

        /// <summary>
        /// Returns true, if the line from rayStart to rayEnd is intersecting an object in the world
        /// </summary>
        /// <param name="world">The world with body object for the raycasting</param>
        /// <param name="rayStart">the position of the ray start</param>
        /// <param name="rayEnd">the position of the ray end</param>
        /// <returns>return true, if an object intersects in the world with the ray</returns>
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

        /// <summary>
        /// Returns the nearest object, that intersects the line from rayStart to rayEnd in the world 
        /// </summary>
        /// <param name="world">The world with body object for the raycasting</param>
        /// <param name="rayStart">the position of the ray start</param>
        /// <param name="rayEnd">the position of the ray end</param>
        /// <returns>the nearest object to the ray start, which intersects with the ray</returns>
        public static Body getIntersectingObject(World world, Vector2 rayStart, Vector2 rayEnd)
        {
            Body nearestIntersectingBody = null;
            world.RayCast((fixture, point, normal, fraction) =>
            {
                if (fixture != null)
                {
                    nearestIntersectingBody = fixture.Body;
                }
                return fraction; // clip the ray to this point and continue
            }, rayStart, rayEnd);
            return nearestIntersectingBody;
        }
    }
}
