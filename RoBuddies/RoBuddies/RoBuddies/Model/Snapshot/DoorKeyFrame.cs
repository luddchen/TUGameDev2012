using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Snapshot
{
    class DoorKeyFrame : BodyKeyFrame
    {
        private bool switchedOn;
        private Door door;

        public DoorKeyFrame(Door door) : base(door)
        {
            this.door = door;
            this.switchedOn = door.IsSwitchedOn;
        }

        public override void Restore()
        {
            base.Restore();
            if (switchedOn != this.door.IsSwitchedOn)
            {
                if (switchedOn)
                {
                    door.switchOn();
                } 
                else
                {
                    door.switchOff();
                }
            }
        }
    }
}
