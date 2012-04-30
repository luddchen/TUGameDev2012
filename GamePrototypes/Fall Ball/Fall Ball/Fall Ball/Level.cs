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
        public MouseController mouse;
        private FixedMouseJoint fixedMouseJoint;
        private Game1 game;

        public Level(Game1 game, List<Texture2D> textures, SpriteBatch batch)
        {
            this.size = new Vector2(1, 1);
            this.world = new World(new Vector2(0.0f, 10.0f));
            this.gamefield = new Field();
            this.addObjects = new Field();
            this.game = game;
        }

        public virtual bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public virtual void update(GameTime gameTime, Vector2 pos)
        {
            if ((mouse.IsNewMouseButtonPressed(MouseButtons.LEFT_BUTTON)) &&
                fixedMouseJoint == null)
            {
                Fixture savedFixture = this.world.TestPoint(pos);
                if (savedFixture != null)
                {
                    Body body = savedFixture.Body;
                    fixedMouseJoint = new FixedMouseJoint(body, pos);
                    fixedMouseJoint.MaxForce = 1000.0f * body.Mass;
                    this.world.AddJoint(fixedMouseJoint);
                    body.Awake = true;
                    body.BodyType = BodyType.Dynamic;

                    foreach (GameObject obj in addObjects.objects)
                    {
                        Console.WriteLine("Body Pos: " + game.WorldToScreen(obj.body.Position));
                        //obj.body.BodyType = BodyType.Dynamic;
                        
                    }
                }

                
            }

            if ((mouse.IsNewMouseButtonReleased(MouseButtons.LEFT_BUTTON)) &&
                fixedMouseJoint != null)
            {
                this.world.RemoveJoint(fixedMouseJoint);
                fixedMouseJoint = null;
            }

            if (fixedMouseJoint != null)
            {
                fixedMouseJoint.WorldAnchorB = pos;
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
