using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Worlds.World1
{
    /// <summary>
    /// This class loads the level 3 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level1_3 : World1Level
    {
        private const string LEVEL_PATH = "Worlds\\World1\\Level1_3.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Level  3:  Don't  be  headless";

        public Level1_3(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {

        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(-13f, 0f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, true);
            Switch doorSwitcher = new Switch(new Vector2(-15f, 13f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);
        }
    }
}
