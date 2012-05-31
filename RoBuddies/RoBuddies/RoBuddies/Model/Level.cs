using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RoBuddies.Control.StateMachines;

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
        public List<Layer> AllLayers { get; set; }

        /// <summary>
        /// list of all active Statemachines
        /// </summary>
        public List<StateMachine> AllStateMachines;

        /// <summary>
        /// creates a new world / level
        /// </summary>
        /// <param name="gravity">initial gravity of physics</param>
        public Level(Vector2 gravity)
            : base(gravity)
        {
            this.AllLayers = new List<Layer>();
            this.AllStateMachines = new List<StateMachine>();
            this.Background = Color.DeepSkyBlue;
        }

        /// <summary>
        /// get an layer by name
        /// </summary>
        /// <param name="layerName">the name of layer you look for</param>
        /// <returns>a layer with the given name , null if unknown in this level</returns>
        public Layer GetLayerByName(String layerName) 
        {
            Layer layer = null;
            foreach (Layer l in AllLayers)
            {
                if (l.Name == layerName)
                {
                    layer = l;
                    break;
                }
            }
            return layer;
        }

        /// <summary>
        /// update the world / level
        /// </summary>
        /// <param name="gameTime">time of game</param>
        public void Update( GameTime gameTime) 
        {
            foreach (StateMachine machine in this.AllStateMachines)
            {
                machine.Update(gameTime);
            }

            this.Step(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
        }

    }

}
