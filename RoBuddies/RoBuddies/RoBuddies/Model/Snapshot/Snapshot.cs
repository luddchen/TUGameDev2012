﻿using System;
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

        /// <summary>
        /// creates a new Snapshot unit
        /// </summary>
        /// <param name="level"></param>
        public Snapshot(Level level)
        {
            this.AllKeyFrames = new List<KeyFrame>();
            this.BodyList = new List<IBody>();
            CreateBodyList(level);
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
                    this.BodyList.Add(body);
                }
            }

            this.AllKeyFrames = new List<KeyFrame>();

        }

        /// <summary>
        /// do a new snapshot
        /// </summary>
        public void MakeSnapshot()
        {
            KeyFrame keyFrame = new KeyFrame(this.BodyList);
            this.AllKeyFrames.Add(keyFrame);
            Console.Out.WriteLine("available Snapshots : " + this.AllKeyFrames.Count);
        }

        /// <summary>
        /// rewind one or more keyframes
        /// </summary>
        /// <param name="steps">steps to rewind</param>
        public void Rewind(int steps)
        {
            if (steps < 1) { return; }  // nothing to do

            if (this.AllKeyFrames.Count > (steps - 1))
            {
                Console.Out.WriteLine("rewind " + steps + " from " + this.AllKeyFrames.Count + " available Snapshots");

                int newLastFrame = this.AllKeyFrames.Count - steps;
                KeyFrame keyFrame = this.AllKeyFrames[newLastFrame];
                keyFrame.Restore();

                //release skipped keyframes
                while ((this.AllKeyFrames.Count - 1) > newLastFrame)
                {
                    this.AllKeyFrames[this.AllKeyFrames.Count - 1].Release();
                    this.AllKeyFrames.RemoveAt(this.AllKeyFrames.Count - 1);
                }

                Console.Out.WriteLine("left Snapshots : " + this.AllKeyFrames.Count);
            }
            else
            {
                Console.Out.WriteLine("not enough Snapshots available");
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
