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
        /// background color of level
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        /// our holy robot
        /// </summary>
        public Robot Robot { get; set; }

        /// <summary>
        /// list of all layers in this level
        /// </summary>
        public List<Layer> AllLayers;


        /// <summary>
        /// creates a new world / level
        /// </summary>
        /// <param name="gravity">initial gravity of physics</param>
        public Level(Vector2 gravity)
            : base(gravity)
        {
            this.AllLayers = new List<Layer>();
            this.Background = Color.DarkBlue;
        }

        /// <summary>
        /// update the world / level
        /// </summary>
        /// <param name="gameTime">time of game</param>
        public void Update( GameTime gameTime) 
        {
            foreach (Layer layer in AllLayers)
            {
                layer.Update(gameTime);
            }
            this.Step(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
        }

    }

}
