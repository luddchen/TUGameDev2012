using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;

namespace Robuddies.Levels
{
    class Level_See : Level
    {
        public Level_See(Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundColor = Color.Blue;
        }
    }
}
