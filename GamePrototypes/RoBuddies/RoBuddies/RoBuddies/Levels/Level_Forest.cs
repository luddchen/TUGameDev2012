using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;

namespace Robuddies.Levels
{
    class Level_Forest : Level
    {
        public Level_Forest(Game game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundColor = Color.Green;
        }
    }
}
