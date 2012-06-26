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
    class Lab_1 : LabLevel
    {
        private const string LEVEL_PATH = "Worlds\\Lab\\LAB_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = " ";

        public Lab_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(1.5f, 2f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            //Switch doorSwitcher = new Switch(new Vector2(9f, 5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            //levelObjects.Add(doorSwitcher);

            //Switch wall testing 
            //Wall switchWall = new Wall(new Vector2(4f, -1f), new Vector2(2f, 5f), Color.BurlyWood, this.Level, this.game, true);
            //Switch wallSwitcher = new Switch(new Vector2(2f, 0f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            //levelObjects.Add(switchWall);
            //levelObjects.Add(wallSwitcher);

        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 100);

            HUDString hintStringJump = new HUDString("Press 'space'-Key\nto jump", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-18f, 5.5f));
            levelLabels.Add(hintStringJump);

            HUDString hintStringCrate = new HUDString("Press 's'-Key\nto move the crate", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringCrate.Position = ConvertUnits.ToDisplayUnits(new Vector2(-13f, 8.5f));
            levelLabels.Add(hintStringCrate);

            HUDString hintStringDoor = new HUDString("Press 'up'-Key\nto go to next level", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(4.5f, 6f));
            levelLabels.Add(hintStringDoor);

            HUDString hintStringRewind = new HUDString("Press 'r'-Key to rewind", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringRewind.Position = ConvertUnits.ToDisplayUnits(new Vector2(16f, 6.5f));
            levelLabels.Add(hintStringRewind);
        }
    }

}
