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
    class Mountain_3 : MountainLevel
    {
        private const string LEVEL_PATH = "Worlds\\Mountain\\Mountain_3.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "Level 3";

        public Mountain_3 (Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
            Vector2 offset = new Vector2(0, -2);
            addSky(offset);
            addMountains(offset);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(24.5f, -2f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, true);
            levelObjects.Add(door);
            Switch doorSwitcher = new Switch(new Vector2(23.5f, 13f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(doorSwitcher);

            Wall switchWall = new Wall(new Vector2(-14f, -2f), new Vector2(3f, 8f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher = new Switch(new Vector2(-17f, -3.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            levelObjects.Add(switchWall);
            levelObjects.Add(wallSwitcher);

            Wall switchWall2 = new Wall(new Vector2(-6f, -2f), new Vector2(3f, 8f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher2 = new Switch(new Vector2(-3f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall2, this.Level.Robot);
            levelObjects.Add(switchWall2);
            levelObjects.Add(wallSwitcher2);

            Wall switchWall3 = new Wall(new Vector2(1f, -2f), new Vector2(3f, 8f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher3 = new Switch(new Vector2(4f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall3, this.Level.Robot);
            levelObjects.Add(switchWall3);
            levelObjects.Add(wallSwitcher3);

            Wall switchWall4 = new Wall(new Vector2(8f, -2f), new Vector2(3f, 8f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher4 = new Switch(new Vector2(11f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall4, this.Level.Robot);
            levelObjects.Add(switchWall4);
            levelObjects.Add(wallSwitcher4);



        }

        protected override void addLevelLabels()
        {

        }
    }
}
