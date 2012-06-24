using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.RobotParts;

namespace RoBuddies.Model.Snapshot
{
    class PartsCombinedKeyFrame : BodyKeyFrame
    {
        private PartsCombined partsCombined;
        private Vector2 wheelPosition;
        private Vector2 wheelVelocity;
        private float wheelRotation;

        public PartsCombinedKeyFrame(PartsCombined body)
            : base(body)
        {
            this.partsCombined = body;
            this.wheelPosition = body.wheelBody.Position;
            this.wheelVelocity = body.wheelBody.LinearVelocity;
            this.wheelRotation = body.wheelBody.Rotation;
        }

        public override void Restore()
        {
            base.Restore();
            this.partsCombined.wheelBody.Position = this.wheelPosition;
            this.partsCombined.wheelBody.LinearVelocity = this.wheelVelocity;
            this.partsCombined.wheelBody.Rotation = this.wheelRotation;
            this.partsCombined.wheelMotor.MotorSpeed = 0;
        }
    }
}
