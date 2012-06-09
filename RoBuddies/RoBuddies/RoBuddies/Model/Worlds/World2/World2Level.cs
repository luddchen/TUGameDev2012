using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Worlds.World2
{
    /// <summary>
    /// Still abstract, but more concrete class of a specific level.
    /// Here you can add e.g. specific background art, which apply to
    /// all world 2 levels
    /// </summary>
    abstract class World2Level : WorldLevel
    {

        public World2Level(Game game, string LEVEL_PATH, LevelTheme LEVEL_THEME)
            : base(game, LEVEL_PATH, LEVEL_THEME)
        {

        }

        protected override void addBackground()
        {

        }

        protected override void addLevelObjects()
        {

        }
    }
}
