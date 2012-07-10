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

namespace RoBuddies.Model.Worlds.MountainLevel
{
    /// <summary>
    /// This class loads the level 1 of world 2 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Mountain_1: MountainLevel
    {
        private const string LEVEL_PATH = "Worlds\\Mountain\\Mountain_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "Level 1: The Journey Begins";

        public Mountain_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
            addSky(Vector2.Zero);
            addMountains(Vector2.Zero);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(11.5f, 5f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);

            HUDString hintStringJump = new HUDString("Now your epic journey begins", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-17f, 2f));
            levelLabels.Add(hintStringJump);

            HUDString glString = new HUDString("Good Luck!", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            glString.Position = ConvertUnits.ToDisplayUnits(new Vector2(-9f, 4f));
            levelLabels.Add(glString);
        }
    }
}
