﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;

namespace RoBuddies.Model.Worlds.Tutorial
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Tutorial_3 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_3.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 3: Climbing";

        public Tutorial_3(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(18.5f, 8f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 100);

            HUDString hintStringClimb = new HUDString("Press 'up'\nto climb ladders", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringClimb.Position = ConvertUnits.ToDisplayUnits(new Vector2(-12f, 10f));
            levelLabels.Add(hintStringClimb);
        }
    }

}
