using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Model.RobotParts
{
    class Head : PhysicObject
    {

        public Head(Vector2 position, Vector2 size, Color color, Level level)
            : base(position, size, color, 0, level)
        {
            this.IgnoreGravity = true;
            this.BodyType = BodyType.Dynamic;
            //this.createRectangleFixture();
            this.level.GetLayerByName("mainLayer").AddObject(this);
        }

    }
}
