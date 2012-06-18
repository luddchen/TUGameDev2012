using System.Collections.Generic;

using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Snapshot
{
    class KeyFrame
    {
        private List<BodyKeyFrame> AllBodyKeyFrames;
        private PhysicObject activeRobotPart;
        private Level Level;

        public KeyFrame(List<IBody> bodyList, Level level)
        {
            this.Level = level;
            this.activeRobotPart = this.Level.Robot.ActivePart;

            this.AllBodyKeyFrames = new List<BodyKeyFrame>();
            foreach (IBody body in bodyList)
            {
                BodyKeyFrame bodyKeyFrame;
                if (body is Switch)
                {
                    bodyKeyFrame = new SwitchKeyFrame((Switch)body);
                }
                else
                {
                    bodyKeyFrame = new BodyKeyFrame((PhysicObject)body);
                }
                this.AllBodyKeyFrames.Add(bodyKeyFrame);
            }
        }

        public void Restore()
        {
            foreach (BodyKeyFrame bodyKeyFrame in this.AllBodyKeyFrames)
            {
                bodyKeyFrame.Restore();
            }

            this.Level.Robot.RobotStateMachine.setActivePart(this.activeRobotPart);
        }

        public void Release()
        {
            foreach (BodyKeyFrame bodyKeyFrame in this.AllBodyKeyFrames)
            {
                bodyKeyFrame.Release();
            }
            this.AllBodyKeyFrames.Clear();
        }

    }
}
