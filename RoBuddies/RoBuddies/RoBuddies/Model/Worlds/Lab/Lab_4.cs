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

namespace RoBuddies.Model.Worlds.Lab
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Lab_4 : LabLevel
    {
        private const string LEVEL_PATH = "Worlds\\Lab\\LAB_4.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "";

        public Lab_4(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(24.5f, 0f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, true);
            Switch doorSwitcher = new Switch(new Vector2(17f, 13f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);

            Wall switchWall = new Wall(new Vector2(-16f, -2f), new Vector2(3f, 7f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher = new Switch(new Vector2(-19.5f, -3f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            levelObjects.Add(switchWall);
            levelObjects.Add(wallSwitcher);

            Wall switchWall2 = new Wall(new Vector2(-6f, -2f), new Vector2(3f, 7f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher2 = new Switch(new Vector2(-3.5f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall2, this.Level.Robot);
            levelObjects.Add(switchWall2);
            levelObjects.Add(wallSwitcher2);

            Wall switchWall3 = new Wall(new Vector2(2f, -2f), new Vector2(3f, 7f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher3 = new Switch(new Vector2(4.5f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall3, this.Level.Robot);
            levelObjects.Add(switchWall3);
            levelObjects.Add(wallSwitcher3);

            Wall switchWall4 = new Wall(new Vector2(10f, -2f), new Vector2(3f, 7f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher4 = new Switch(new Vector2(12.5f, -4.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall4, this.Level.Robot);
            levelObjects.Add(switchWall4);
            levelObjects.Add(wallSwitcher4);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 100);

            HUDString hintStringSwitch = new HUDString("Press 's'-Key\nto use switcher", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringSwitch.Position = ConvertUnits.ToDisplayUnits(new Vector2(-17, 2.5f));
            levelLabels.Add(hintStringSwitch);

            HUDString hintStringDoor = new HUDString("Door is locked on", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(22.5f, 3f));
            levelLabels.Add(hintStringDoor);
        }
    }

}
