using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;

namespace RoBuddies.Model.Worlds.Mountain
{
    class End : MountainLevel.MountainLevel
    {
        private const string LEVEL_PATH = "Worlds\\END.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;
        private const String LEVEL_NAME = "THE END";

        public End(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Vector2 offset = new Vector2(0, 0);
            addSky(offset);
            addMountains(offset);
        }

        protected override void addLevelObjects()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);
            HUDString hintStringDoor = new HUDString("Thanks for playing!", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(3.0f, 6f));
            levelLabels.Add(hintStringDoor);
        }

        protected override void addLevelLabels()
        {
        }
    }
}
