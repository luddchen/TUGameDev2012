using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing an layer
    /// </summary>
    class Layer
    {

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
        public List<IBody> AllObjects;




        /// <summary>
        /// creates a new layer
        /// </summary>
        /// /// <param name="name">name for this layer, only for identification without knowing a reference</param>
        /// /// <param name="parallax">value for paralax-scrolling</param>
        /// /// <param name="layerDepth">the depth for drawing, see SpriteBatch.Draw</param>
        public Layer(String name, Vector2 parallax, float layerDepth, Level level)
        {
            this.Name = name;
            this.Parallax = parallax;
            this.LayerDepth = layerDepth;
            this.Level = level;
            this.AllObjects = new List<IBody>(); 
        }


        /// <summary>
        /// update all objects on this layer
        /// </summary>
        public void Update(GameTime gameTime)
        {
        }

    }
}
