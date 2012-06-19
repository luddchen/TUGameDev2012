using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;

namespace RoBuddies.Model.RobotParts
{
    class PartsCombined : PhysicObject
    {

        public Body wheelBody;
        public RevoluteJoint wheelMotor;

        public PartsCombined(Level level, Vector2 pos)
            : base(level)
        {
            this.FixedRotation = true;
            this.Position = pos;
            this.BodyType = BodyType.Dynamic;
            this.Color = Color.White;
            this.Width = 3;
            this.Height = 3f;
            this.IsVisible = true;
            this.Friction = 3f;
            FixtureFactory.AttachRectangle(1, 2.3f, 1, new Vector2(0, 0.20f), this);
            // attach wheel
            this.wheelBody = BodyFactory.CreateCircle(this.level, 0.5f, 1, this.Position + new Vector2(0, (-2.3f / 2f) + 0.20f));
            this.wheelBody.BodyType = BodyType.Dynamic;
            this.wheelMotor = JointFactory.CreateRevoluteJoint(this.level, this, this.wheelBody, Vector2.Zero);
            this.wheelBody.Friction = 3f;
            this.wheelMotor.MaxMotorTorque = 1000f;
            this.wheelMotor.MotorEnabled = true;
        }
    }
}
