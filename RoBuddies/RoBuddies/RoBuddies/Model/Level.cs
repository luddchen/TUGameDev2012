using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model
{
    /// <summary>
    /// The different graphic themes of a level.
    /// </summary>
    public enum LevelTheme
    {
        ROBO_LAB,
        MOUNTAIN,
        MENTAL_HOSPITAL
    }

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
        /// The graphic theme of the level
        /// </summary>
        public LevelTheme theme = LevelTheme.MENTAL_HOSPITAL;

        private List<Layer> allLayers;

        private List<StateMachine> allStateMachines;

        /// <summary>
        /// list of all layers in this level
        /// </summary>
        public List<Layer> AllLayers { get { return this.allLayers; } }

        public void AddLayer(Layer layer)
        {
            if (this.GetLayerByName(layer.Name) != null)
            {
                throw new System.ArgumentException("a layer with this name already exists", layer.Name);
            }
            this.AllLayers.Add(layer);
            layer.Level = this;

            // sort by layerDepth to draw from back to front
            this.allLayers.Sort();
        }

        /// <summary>
        /// list of all active Statemachines
        /// </summary>
        public List<StateMachine> AllStateMachines { get { return this.allStateMachines; } }

        public void AddStateMachine(StateMachine stateMachine)
        {
            this.AllStateMachines.Add(stateMachine);
        }

        /// <summary>
        /// creates a new world / level
        /// </summary>
        /// <param name="gravity">initial gravity of physics</param>
        public Level(Vector2 gravity)
            : base(gravity)
        {
            this.allLayers = new List<Layer>();
            this.allStateMachines = new List<StateMachine>();
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

        /// <summary>
        /// Removes a IBody object from all possible layers of the level.
        /// If the IBody object is no object of any layer, then nothing will happen.
        /// </summary>
        /// <param name="iBody">the iBody which will be removed</param>
        public void removeObject(IBody iBody)
        {
            foreach(Layer layer in allLayers)
            {
                layer.RemoveObject(iBody);
            }
        }

    }

}
