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

        public int width;   // width of full level
        public int height;  // height of full level
        public World world;
        public Field gamefield;
        public GameObject ball1;
        public GameObject ball2;

        public Level(List<Texture2D> textures, SpriteBatch batch)
        {
            this.width = 1;
            this.height = 1;
            this.world = new World(new Vector2(0.0f, 10.0f));
            this.gamefield = new Field();
        }

    }
}
