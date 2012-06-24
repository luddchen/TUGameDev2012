using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RoBuddies.Control.StateMachines;
using RoBuddies.View.HUD;

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
        // initial gravity constants for the gravity of the level
        private const float INITIAL_GRAVITY_X = 0f;
        private const float INITIAL_GRAVITY_Y = -10;

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

        /// <summary>
        /// The levelName will be displayed at the beginning of a level
        /// </summary>
        public String LevelName { get; set; }

        private List<Layer> allLayers;

        private List<StateMachine> allStateMachines;


        /// <summary>
        /// Flag if the player has finished the level
        /// </summary>
        public bool finished { get; set; }

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
        public Level()
            : base(new Vector2(INITIAL_GRAVITY_X, INITIAL_GRAVITY_Y))
        {
            this.finished = false;
            this.allLayers = new List<Layer>();
            this.allStateMachines = new List<StateMachine>();
            this.Background = Color.SkyBlue;
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
