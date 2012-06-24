using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Worlds.World2
{
    /// <summary>
    /// This class loads the level 5 of world 2 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level2_5 : World2Level
    {
        private const string LEVEL_PATH = "Worlds\\World2\\LEVEL2_5.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "Level  4: A  long  way  down";

        public Level2_5(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addBackground()
        {
            Vector2 offset = new Vector2(0, -7.5f);

            addSky(offset);
            addMountains(offset);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(0f, -6f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }
    }
}
