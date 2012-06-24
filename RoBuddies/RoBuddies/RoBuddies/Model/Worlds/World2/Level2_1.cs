using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Worlds.World2
{    
    /// <summary>
    /// This class loads the level 1 of world 2 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level2_1 : World2Level
    {
        private const string LEVEL_PATH = "Worlds\\World2\\LEVEL2_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "Level  5:  The  unsolvable  level";

        public Level2_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
            addSky(Vector2.Zero);
            addMountains(Vector2.Zero);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(50f, 8f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            Switch doorSwitcher = new Switch(new Vector2(8f, 5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);
        }

        protected override void addLevelLabels()
        {

        }
    }
}
