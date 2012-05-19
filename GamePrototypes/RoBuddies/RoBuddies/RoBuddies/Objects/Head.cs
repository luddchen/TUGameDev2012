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
        public Head(ContentManager content, Vector2 pos, World world)
            : base(content, pos, world)
        {
        }
    }
}
