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

        public PartsCombined(Vector2 pos, Level level)
            : base(pos, new Vector2(3,3), Color.White, 0, level)
        {
            this.BodyType = BodyType.Dynamic;
            FixtureFactory.AttachRectangle(1f, 2.3f, 1, new Vector2(0, 0.20f), this);
            this.Friction = 0f;
            this.level.GetLayerByName("mainLayer").AddObject(this);
            // attach wheel
            this.wheelBody = BodyFactory.CreateCircle(this.level, 0.5f, 1, this.Position + new Vector2(0, (-2.3f / 2f) + 0.20f));
            this.wheelBody.BodyType = BodyType.Dynamic;
            this.wheelMotor = JointFactory.CreateRevoluteJoint(this.level, this, this.wheelBody, Vector2.Zero);
            this.wheelBody.Friction = 100f;
            this.wheelMotor.MaxMotorTorque = 100f;
            this.wheelMotor.MotorEnabled = true;
        }

        public override void setVisible(bool visible)
        {
            base.setVisible(visible);
            this.wheelBody.Enabled = visible;
        }

    }
}
