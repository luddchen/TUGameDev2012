using System.Collections.Generic;

using RoBuddies.Model.Objects;
using RoBuddies.Model.RobotParts;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model.Snapshot
{
    class KeyFrame
    {
        public List<BodyKeyFrame> AllBodyKeyFrames;
        public BodyKeyFrame BridgeHeadWallFrame;
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
                else if (body is PartsCombined)
                {
                    bodyKeyFrame = new PartsCombinedKeyFrame((PartsCombined)body);
                }
                else if (body is LowerPart)
                {
                    bodyKeyFrame = new LowerPartKeyFrame((LowerPart)body);
                }
                else if (body is Door)
                {
                    bodyKeyFrame = new DoorKeyFrame((Door)body);
                }
                else
                {
                    bodyKeyFrame = new BodyKeyFrame((PhysicObject)body);
                }
                this.AllBodyKeyFrames.Add(bodyKeyFrame);
            }
            // save bridge head wall state
            if (BridgeHeadStateMachine.wall != null)
            {
                this.BridgeHeadWallFrame = new BodyKeyFrame(BridgeHeadStateMachine.wall);
            }
        }

        public KeyFrame(KeyFrame oldKeyFrame, Level level) 
        {
            bool changes = false;

            this.Level = level;
            this.activeRobotPart = this.Level.Robot.ActivePart;
            if (oldKeyFrame.activeRobotPart != this.activeRobotPart) { changes = true; }

            this.AllBodyKeyFrames = new List<BodyKeyFrame>();
            foreach (BodyKeyFrame oldBodyKeyframe in oldKeyFrame.AllBodyKeyFrames)  
            {
                BodyKeyFrame bodyKeyFrame;
                if (oldBodyKeyframe.Body is Switch)
                {
                    bodyKeyFrame = new SwitchKeyFrame((Switch)oldBodyKeyframe.Body);
                }
                else if (oldBodyKeyframe.Body is PartsCombined)
                {
                    bodyKeyFrame = new PartsCombinedKeyFrame((PartsCombined)oldBodyKeyframe.Body);
                }
                else if (oldBodyKeyframe.Body is LowerPart)
                {
                    bodyKeyFrame = new LowerPartKeyFrame((LowerPart)oldBodyKeyframe.Body);
                }
                else if (oldBodyKeyframe.Body is Door)
                {
                    bodyKeyFrame = new DoorKeyFrame((Door)oldBodyKeyframe.Body);
                }
                else
                {
                    bodyKeyFrame = new BodyKeyFrame((PhysicObject)oldBodyKeyframe.Body);
                }
                this.AllBodyKeyFrames.Add(bodyKeyFrame);

                if (bodyKeyFrame.Position != oldBodyKeyframe.Position) { changes = true; }
            }
            // save bridge head wall state
            if (BridgeHeadStateMachine.wall != null)                
            {
                this.BridgeHeadWallFrame = new BodyKeyFrame(BridgeHeadStateMachine.wall);
                if (oldKeyFrame.BridgeHeadWallFrame == null || (oldKeyFrame.BridgeHeadWallFrame.Position != this.BridgeHeadWallFrame.Position))
                {
                    changes = true;
                }
            }

            if (!changes)
            {
                this.AllBodyKeyFrames.Clear();
                this.AllBodyKeyFrames = null;
            }
        }

        public void Restore()
        {
            foreach (BodyKeyFrame bodyKeyFrame in this.AllBodyKeyFrames)
            {
                bodyKeyFrame.Restore();
            }
            // restore bridge head wall state
            if (this.BridgeHeadWallFrame != null)
            {
                this.BridgeHeadWallFrame.Restore();
            }
            else if (BridgeHeadStateMachine.wall != null)
            {
                this.Level.removeObject(BridgeHeadStateMachine.wall);
                BridgeHeadStateMachine.wall.Dispose();
                BridgeHeadStateMachine.wall = null;
            }
            this.Level.Robot.RobotStateMachine.setActivePart(this.activeRobotPart);
        }

        public void Release()
        {
            foreach (BodyKeyFrame bodyKeyFrame in this.AllBodyKeyFrames)
            {
                bodyKeyFrame.Release();
            }
            if (this.BridgeHeadWallFrame != null)
            {
                this.BridgeHeadWallFrame.Release();
            }
            this.AllBodyKeyFrames.Clear();
        }

    }
}
