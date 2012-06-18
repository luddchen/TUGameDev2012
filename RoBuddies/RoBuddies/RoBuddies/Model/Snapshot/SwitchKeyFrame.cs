
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Snapshot
{
    class SwitchKeyFrame : BodyKeyFrame
    {
        private bool isActivated;
        private Switch switcher;

        public SwitchKeyFrame(Switch switcher) : base(switcher)
        {
            this.switcher = switcher;
            this.isActivated = switcher.IsActivated;
        }

        public override void Restore()
        {
            base.Restore();
            this.switcher.IsActivated = this.isActivated;
        }

    }
}
