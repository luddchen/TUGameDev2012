using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Worlds.MountainLevel
{
    /// <summary>
    /// This class loads the level 1 of world 2 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Mountain_2 : MountainLevel
    {
        private const string LEVEL_PATH = "Worlds\\Mountain\\Mountain_2.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "Level 2";

        public Mountain_2(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
            addSky(Vector2.Zero);
            addMountains(Vector2.Zero);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(-27.5f, 1f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {

        }
    }
}
