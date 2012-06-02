using System;
using System.Collections.Generic;

namespace RoBuddies.Model.Snapshot
{
    class KeyFrame
    {
        private List<BodyKeyFrame> AllBodyKeyFrames;

        public KeyFrame(List<IBody> bodyList)
        {
            this.AllBodyKeyFrames = new List<BodyKeyFrame>();
            foreach (IBody body in bodyList)
            {
                BodyKeyFrame bodyKeyFrame = new BodyKeyFrame(body);
                this.AllBodyKeyFrames.Add(bodyKeyFrame);
            }
        }

        public void Restore()
        {
            foreach (BodyKeyFrame bodyKeyFrame in this.AllBodyKeyFrames)
            {
                bodyKeyFrame.Restore();
            }
        }

        public void Release()
        {
            this.AllBodyKeyFrames.Clear();
        }

    }
}
