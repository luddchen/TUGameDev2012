using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Worlds.World1
{
    /// <summary>
    /// This class loads the level 2 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level1_2 : World1Level
    {
        private const string LEVEL_PATH = "Worlds\\World1\\Level1_2.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;

        public Level1_2(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME)
        {

        }

        protected override void addLevelObjects()
        {

        }
    }
}
