using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Model.Worlds.MountainLevel
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Mountain_8 : MountainLevel
    {
        private const string LEVEL_PATH = "Worlds\\HardLevel\\H_LEVEL_2.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "";

        public Mountain_8(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
            Vector2 offset = new Vector2(0, -6);
            addSky(offset);
            addMountains(offset);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(-14f, 3f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);

            Wall switchWall = new Wall(new Vector2(5f, -1f), new Vector2(5f, 14f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher = new Switch(new Vector2(-25f, 17f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            levelObjects.Add(switchWall);
            levelObjects.Add(wallSwitcher);

        }

        protected override void addLevelLabels()
        {
        }
    }

}
