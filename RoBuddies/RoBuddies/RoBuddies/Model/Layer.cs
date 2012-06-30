using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using RoBuddies.View.HUD;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing an layer
    /// </summary>
    class Layer : IComparable<Layer>
    {
        private List<IBody> allObjects;
        private List<IHUDElement> layerLabels;

        /// <summary>
        /// name for this layer, only for identification without knowing a reference, dont know if we need
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// value for paralax-scrolling
        /// </summary>
        public Vector2 Parallax { get; set; }

        /// <summary>
        /// the depth for drawing, see SpriteBatch.Draw, dont know if we need
        /// </summary>
        public float LayerDepth { get; set; }

        /// <summary>
        /// referecnce to the level
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// list of all objects on this layer
        /// </summary>
        public List<IBody> AllObjects { get { return this.allObjects; } }

        /// <summary>
        /// list of all objects on this layer
        /// </summary>
        public List<IHUDElement> allLabels { get { return this.layerLabels; } }

        /// <summary>
        /// add an object to this layer
        /// </summary>
        /// <param name="body">the object, which will be added</param>
        public void AddObject(IBody body)
        {
            if (body != null)
            {
                this.AllObjects.Add(body);
                body.Layer = this;
            }
        }

        /// <summary>
        /// adds an label to this layer
        /// </summary>
        /// <param name="hudString">the label, which will be added</param>
        public void AddLabel(IHUDElement hudElement)
        {
            if (hudElement != null)
            {
                this.allLabels.Add(hudElement);
            }
        }

        /// <summary>
        /// removes an object from this layer
        /// </summary>
        /// <param name="body">the object, which will be removed</param>
        public void RemoveObject(IBody body)
        {
            if (body != null && this.allObjects.Contains(body))
            {
                this.allObjects.Remove(body);
                body.Layer = null;
            }
        }

        /// <summary>
        /// creates a new layer
        /// </summary>
        /// <param name="name">name for this layer, only for identification without knowing a reference</param>
        /// <param name="parallax">value for paralax-scrolling</param>
        /// <param name="layerDepth">the depth for drawing, see SpriteBatch.Draw</param>
        public Layer(String name, Vector2 parallax, float layerDepth)
        {
            this.Name = name;
            this.Parallax = parallax;
            this.LayerDepth = layerDepth;
            this.allObjects = new List<IBody>();
            this.layerLabels = new List<IHUDElement>();
        }


        public int CompareTo(Layer other)
        {
            return other.LayerDepth.CompareTo(this.LayerDepth);
        }
    }
}
