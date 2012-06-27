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
    class Tutorial_4 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_4.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 4: Move Crates";

        public Tutorial_4(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(8.5f, 9f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 100);

            HUDString hintStringCrate = new HUDString("Hold 's'-Key or\n'X'-Button to\nmove the crate", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringCrate.Position = ConvertUnits.ToDisplayUnits(new Vector2(16f, 17f));
            levelLabels.Add(hintStringCrate);

            HUDString hintStringRewind = new HUDString("You can rewind with\n'r'-Key or 'Back'-Button", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringRewind.Position = ConvertUnits.ToDisplayUnits(new Vector2(15f, 9f));
            levelLabels.Add(hintStringRewind);
        }
    }

}
