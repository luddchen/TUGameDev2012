using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Worlds.World3
{
    /// <summary>
    /// Still abstract, but more concrete class of a specific level.
    /// Here you can add e.g. specific background art, which apply to
    /// all world 3 levels
    /// </summary>
    class World3Level : WorldLevel
    {
        
        public World3Level(Game game, string LEVEL_PATH, LevelTheme LEVEL_THEME, String LEVEL_NAME)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
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
