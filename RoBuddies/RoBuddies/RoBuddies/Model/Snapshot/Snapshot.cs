using System;
using System.Collections.Generic;

namespace RoBuddies.Model.Snapshot
{

    /// <summary>
    /// a Snapshot Unit
    /// </summary>
    class Snapshot
    {
        private List<KeyFrame> AllKeyFrames;

        private List<IBody> BodyList;

        private Level Level;

        private int currentKeyFrame;

        /// <summary>
        /// creates a new Snapshot unit
        /// </summary>
        /// <param name="level"></param>
        public Snapshot(Level level)
        {
            this.Level = level;
            this.AllKeyFrames = new List<KeyFrame>();
            this.BodyList = new List<IBody>();
            CreateBodyList(level);
            this.currentKeyFrame = -1;
        }

        /// <summary>
        /// create a List of all Objects that will be snapshoted
        /// </summary>
        /// <param name="level">the level</param>
        public void CreateBodyList(Level level)
        {
            this.BodyList = new List<IBody>();

            foreach (Layer layer in level.AllLayers)
            {
                foreach (IBody body in layer.AllObjects)
                {
                    if (body is PhysicObject)
                    {
                        this.BodyList.Add(body);
                    }
                }
            }

            this.AllKeyFrames = new List<KeyFrame>();

        }

        /// <summary>
        /// do a new snapshot
        /// </summary>
        public void MakeSnapshot()
        {
            KeyFrame keyFrame = new KeyFrame(this.BodyList, this.Level);
            this.AllKeyFrames.Add(keyFrame);
            this.currentKeyFrame++;
            Console.Out.WriteLine("available Snapshots : " + this.AllKeyFrames.Count);
        }

        /// <summary>
        /// rewind a single step without clean keyframes
        /// </summary>
        public void Rewind()
        {
            if (this.currentKeyFrame > 0)
            {
                this.currentKeyFrame--;
                this.AllKeyFrames[currentKeyFrame].Restore();
            }
        }

        /// <summary>
        /// forward a single step
        /// </summary>
        public void Forward()
        {
            if (this.currentKeyFrame < this.AllKeyFrames.Count - 2)
            {
                this.currentKeyFrame++;
                this.AllKeyFrames[currentKeyFrame].Restore();
            }
        }

        /// <summary>
        /// cleanup keyframes that are skipped by rewind/forward
        /// </summary>
        public void PlayOn()
        {
            for (int i = this.AllKeyFrames.Count - 1; i > this.currentKeyFrame; i--)
            {
                this.AllKeyFrames.RemoveAt(i);
            }
        }

        /// <summary>
        /// clean up all references
        /// </summary>
        public void Release()
        {
            foreach (KeyFrame keyframe in this.AllKeyFrames)
            {
                keyframe.Release();
            }
            this.AllKeyFrames.Clear();
            this.BodyList.Clear();
        }

    }
}
