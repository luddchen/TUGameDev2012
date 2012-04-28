using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fall_Ball
{

    // base class for levels
    class Level
    {

        public Vector2 size;
        public World world;
        public Field gamefield;
        public GameObject ball1;
        public GameObject ball2;

        public Level(List<Texture2D> textures, SpriteBatch batch)
        {
            this.size = new Vector2(1, 1);
            this.world = new World(new Vector2(0.0f, 10.0f));
            this.gamefield = new Field();
        }

    }
}
