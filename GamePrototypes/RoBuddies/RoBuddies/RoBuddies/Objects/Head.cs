using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;

namespace Robuddies.Objects
{
    class Head : RobotPart
    {
        public Head(ContentManager content, Vector2 pos, Robot robot, World world, PhysicObject physics)
            : base(content, pos, robot, world, physics)
        {
        }
    }
}
