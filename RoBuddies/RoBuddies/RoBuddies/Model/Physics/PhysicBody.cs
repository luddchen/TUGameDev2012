using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Physics
{
    public enum BodyType { Static, Dynamic }

    class PhysicBody
    {

        public PhysicBody(PhysicWorld world)
        {
        }


        public Vector2 Position
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public BodyType BodyType { get; set; }
    }
}
