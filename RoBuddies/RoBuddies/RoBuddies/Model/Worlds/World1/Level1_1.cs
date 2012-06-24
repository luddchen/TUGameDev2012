﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;
using RoBuddies.View.HUD;

namespace RoBuddies.Model.Worlds.World1
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level1_1 : World1Level
    {
        private const string LEVEL_PATH = "Worlds\\World1\\Level1_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Level  1:  The  journey  begins";

        public Level1_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(9f, -1f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, true);
            Switch doorSwitcher = new Switch(new Vector2(9f, 5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);

            //Switch wall testing 
            Wall switchWall = new Wall(new Vector2(4f, -1f), new Vector2(2f, 5f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher = new Switch(new Vector2(2f, 0f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            levelObjects.Add(switchWall);
            levelObjects.Add(wallSwitcher);

        }

        protected override void addLevelLabels()
        {
            HUDString testString = new HUDString("test", null, new Vector2(0, 0), null, Color.Gray, 1, 0, game.Content);
            testString.Position = new Vector2(0, 0);
            levelLabels.Add(testString);
        }
    }
    
}
