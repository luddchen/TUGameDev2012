using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing the games world
    /// </summary>
    class Level : FarseerPhysics.Dynamics.World
    {

        /// <summary>
        /// list of all layers in this level
        /// </summary>
        public List<Layer> AllLayers;



        /// <summary>
        /// creates a new world / level
        /// </summary>
        /// /// <param name="gravity">initial gravity of physics</param>
        public Level(Vector2 gravity)
            : base(gravity)
        {
            this.AllLayers = new List<Layer>();
        }

        /// <summary>
        /// update the world / level
        /// </summary>
        /// /// <param name="gametime">time of game</param>
        public void Update( GameTime gameTime) 
        {
        }

    }

}
