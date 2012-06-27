using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.RobotParts;

namespace RoBuddies.Model.Snapshot
{
    class LowerPartKeyFrame : BodyKeyFrame
    {
        private LowerPart lowerPart;
        private Vector2 wheelPosition;
        private Vector2 wheelVelocity;
        private float wheelRotation;

        public LowerPartKeyFrame(LowerPart body)
            : base(body)
        {
            this.lowerPart = body;
            this.wheelPosition = body.wheelBody.Position;
            this.wheelVelocity = body.wheelBody.LinearVelocity;
            this.wheelRotation = body.wheelBody.Rotation;
        }

        public override void Restore()
        {
            base.Restore();
            this.lowerPart.wheelBody.Position = this.wheelPosition;
            this.lowerPart.wheelBody.LinearVelocity = this.wheelVelocity;
            this.lowerPart.wheelBody.Rotation = this.wheelRotation;
            this.lowerPart.wheelMotor.MotorSpeed = 0;
        }
    }
}
