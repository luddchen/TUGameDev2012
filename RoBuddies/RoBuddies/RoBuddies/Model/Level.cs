using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing the games world
    /// </summary>
    public class Level : FarseerPhysics.Dynamics.World
    {
        /// <summary>
        /// background color of level
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        /// our holy robot
        /// </summary>
        private Robot Robot { get; set; }

        /// <summary>
        /// list of all layers in this level
        /// </summary>
        public List<Layer> AllLayers { get; set; } // here some better solution -> private vs public -> need in LevelView an Iterator over this list


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
