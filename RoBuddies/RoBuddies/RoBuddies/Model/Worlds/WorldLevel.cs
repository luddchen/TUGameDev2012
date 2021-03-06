﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;
using RoBuddies.Model.Serializer;
using RoBuddies.View.HUD;

namespace RoBuddies.Model.Worlds
{
    /// <summary>
    /// This abstract class is the upper class for all levels,
    /// which will be loaded from the json code of the editor.
    /// The subclasses can add with the addLevelObjects() method
    /// new objects to the level.
    /// </summary>
    abstract class WorldLevel
    {
        private Level level;
        protected Game game;

        /// <summary>
        /// Add your new level objects for the level to this list
        /// </summary>
        protected List<IBody> levelObjects;

        /// <summary>
        /// Add your new level objects for the level to this list
        /// </summary>
        protected List<IHUDElement> levelLabels;

        /// <summary>
        /// Getter for the loaded level
        /// </summary>
        public Level Level
        {
            get { return this.level; }
        }

        /// <summary>
        /// Creates a new WorldLevel object from a json level file
        /// </summary>
        /// <param name="game">The game for the level</param>
        /// <param name="levelPath">the path to the level file</param>
        /// <param name="levelTheme">the filename of the level file</param>
        /// <param name="levelName">the name of the level which will be displayed at the beginning of the level</param>
        public WorldLevel(Game game, String levelPath, LevelTheme levelTheme, String levelName)
        {
            this.game = game;
            this.levelObjects = new List<IBody>();
            this.levelLabels = new List<IHUDElement>();
            LevelReader levelReader = new LevelReader(game);
            this.level = levelReader.readLevel(".\\" + game.Content.RootDirectory, levelPath, levelTheme);
            this.Level.LevelName = levelName;
            addLevelObjects();
            addLevelLabels();
            addLevelObjectsToLevel();
            addLevelLabelsToLevel();
            addBackground();
        }

        /// <summary>
        /// In this method you can add your background stuff for the level.
        /// </summary>
        abstract protected void addBackground();

        /// <summary>
        /// In this method you can add your objects, to the level,
        /// which can not be added with the editor
        /// </summary>
        abstract protected void addLevelObjects();

        /// <summary>
        /// In this method you can add your level labels for the level
        /// </summary>
        abstract protected void addLevelLabels();

        /// <summary>
        /// adds the LevelObjects to the main or background layer
        /// </summary>
        private void addLevelObjectsToLevel()
        {
            Layer mainLayer = this.Level.GetLayerByName("mainLayer");
            Layer backLayer = this.Level.GetLayerByName("backLayer");
            foreach (IBody body in levelObjects)
            {
                if (body is Switch || body is Pipe || body is Door )//|| body is Ladder)
                {
                    backLayer.AddObject(body);
                } else {
                    mainLayer.AddObject(body);
                }
            }
        }

        /// <summary>
        /// adds the levelLabels to the background Layer
        /// </summary>
        private void addLevelLabelsToLevel()
        {
            Layer backLayer = this.Level.GetLayerByName("backLayer");
            foreach (IHUDElement hudElement in levelLabels)
            {
                backLayer.AddLabel(hudElement);
            }
        }

        protected void setLevelName(String levelName)
        {
            this.Level.LevelName = levelName;
        }

    }
}
