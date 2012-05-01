using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

// author: Thomas

namespace Fall_Ball
{
    abstract class LevelBallCollision : Level
    {
        public LevelBallCollision(List<Texture2D> textures, SpriteBatch batch)
            : base(textures, batch)
        {

        }
    }
}
