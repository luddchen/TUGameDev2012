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
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
using Fall_Ball.Controls;
using Fall_Ball.Objects;

namespace Fall_Ball
{

    // base class for levels
    class Level
    {

        public Vector2 size;
        public World world;
        public Field gamefield;
        public Field addObjects;
        public GameObject ball1;
        public GameObject ball2;
        public Overlay overlay;
        public float timeFactor;

        public Level(List<Texture2D> textures, SpriteBatch batch)
        {
            this.size = new Vector2(1, 1);
            this.world = new World(new Vector2(0.0f, 10.0f));
            this.gamefield = new Field();
            this.addObjects = new Field();
            this.timeFactor = 1;
        }

        public virtual bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public virtual void update(GameTime gameTime)
        {        
            int millis = gameTime.ElapsedGameTime.Milliseconds;
            for (int i = 0; i < millis; i += 4)
            {
                world.Step(0.01f * this.timeFactor);
            }
        }

        public void addToMyOnCollision(GameObject gameObject)
        {
            foreach (Fixture fix in gameObject.body.FixtureList)
            {
                fix.OnCollision += MyOnCollision;
            }
        }

        public void removeFromMyOnCollision(GameObject gameObject)
        {
            foreach (Fixture fix in gameObject.body.FixtureList)
            {
                fix.OnCollision -= MyOnCollision;
            }
        }

    }
}
