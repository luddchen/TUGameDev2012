using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;

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
            LevelReader levelReader = new LevelReader(game);
            this.level = levelReader.readLevel(".\\" + game.Content.RootDirectory, levelPath, levelTheme);
            this.Level.LevelName = levelName;
            addLevelObjects();
            addLevelObjectsToLevel();
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

        protected void setLevelName(String levelName)
        {
            this.Level.LevelName = levelName;
        }

    }
}
