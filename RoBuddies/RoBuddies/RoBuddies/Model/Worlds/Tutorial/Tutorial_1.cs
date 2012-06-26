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

namespace RoBuddies.Model.Worlds.Tutorial
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Tutorial_1 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 1: Jump";

        public Tutorial_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(8.5f, 7f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 100);

            HUDString hintStringJump = new HUDString("Press 'space'-Key\nto jump", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-20f, 10f));
            levelLabels.Add(hintStringJump);


            HUDString hintStringDoor = new HUDString("Press 'up'-Key\nto go to next level", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(6.5f, 14f));
            levelLabels.Add(hintStringDoor);
        }
    }

}
