using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace RoBuddies.Model.RobotParts
{
    class LowerPart : PhysicObject
    {
        public Body wheelBody;
        public RevoluteJoint wheelMotor;

        public LowerPart(Level level, Vector2 pos)
            : base(level)
        {
            this.FixedRotation = true;
            this.Position = pos;
            this.BodyType = BodyType.Dynamic;
            this.Color = Color.White;
            this.Width = 3;
            this.Height = 1.8f;
            this.IsVisible = true;
            this.Friction = 0f;
            FixtureFactory.AttachRectangle(1, 1f, 1, Vector2.Zero, this);
            // attach wheel
            this.wheelBody = BodyFactory.CreateCircle(this.level, 0.5f, 1, this.Position + new Vector2(0, (-1f / 2f) + 0.20f));
            this.wheelBody.BodyType = BodyType.Dynamic;
            this.wheelMotor = JointFactory.CreateRevoluteJoint(this.level, this, this.wheelBody, Vector2.Zero);
            this.wheelBody.Friction = 100f;
            this.wheelMotor.MaxMotorTorque = 1000f;
            this.wheelMotor.MotorEnabled = true;
        }
    }
}
