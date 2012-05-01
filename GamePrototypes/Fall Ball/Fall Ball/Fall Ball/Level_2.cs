using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Contacts;
using Fall_Ball.Controls;
using Fall_Ball.Objects;

namespace Fall_Ball
{

    // second level
    class Level_2 : Level
    {
        private List<GameObject> bonus;
        private List<int> bonusLifetime;
        private int maxLifetime = 500;

        public Level_2(List<Texture2D> textures, SpriteBatch batch)
            : base(textures, batch)
        {
            this.size.X = 500;
            this.size.Y = 800;
            this.timeFactor = 1.5f;

            ball1 = new Ball(new Vector2(50, 10), 11.0f, 10000f, Color.HotPink, batch, textures[5], world);
            ball1.body.BodyType = BodyType.Dynamic;
            gamefield.add(ball1);
            addToMyOnCollision(ball1);

            ball2 = new Ball(new Vector2(485, 200), 15.0f, 10000f, Color.HotPink, batch, textures[2], world);
            ball2.body.BodyType = BodyType.Dynamic;
            gamefield.add(ball2);
            addToMyOnCollision(ball2);

            // left
            gamefield.add(new SquareStack(new Vector2(40, 100), new Vector2(100, 10), new Vector2(10, 5), 0.0f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //arrow 1
            gamefield.add(new SquareStack(new Vector2(90, 220), new Vector2(100, 10), new Vector2(10, 5), -0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(155, 220), new Vector2(100, 10), new Vector2(10, 5), 0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //vertical one:
            gamefield.add(new SquareStack(new Vector2(122, 280), new Vector2(10, 160), new Vector2(10, 5), 0.0f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //arrow 2
            gamefield.add(new SquareStack(new Vector2(90, 410), new Vector2(100, 10), new Vector2(10, 5), -0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(155, 410), new Vector2(100, 10), new Vector2(10, 5), 0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //vertical one:
            gamefield.add(new SquareStack(new Vector2(122, 470), new Vector2(10, 160), new Vector2(10, 5), 0.0f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //arrow 3
            gamefield.add(new SquareStack(new Vector2(90, 600), new Vector2(100, 10), new Vector2(10, 5), -0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(155, 600), new Vector2(100, 10), new Vector2(10, 5), 0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(120, 700), new Vector2(260, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));

            // right
            gamefield.add(new SquareStack(new Vector2(420, 250), new Vector2(150, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(350, 200), new Vector2(200, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(320, 320), new Vector2(100, 10), new Vector2(10, 5), 0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(370, 425), new Vector2(100, 10), new Vector2(10, 5), -0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(425, 435), new Vector2(80, 10), new Vector2(10, 5), -0.8f, Color.Green, Color.LightGreen, batch, textures[0], world));
            //vertical one:
            gamefield.add(new SquareStack(new Vector2(280, 400), new Vector2(10, 160), new Vector2(10, 5), 0.0f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(370, 570), new Vector2(230, 10), new Vector2(10, 5), 0.7f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(450, 730), new Vector2(100, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));

            // just for testing, delete this objects for the real level
            gamefield.add(new Triangle(new Vector2(40, 50), new Vector2(25, 25), 0.0f, batch, textures[7], world));
            

            // bonus objects
            bonus = new List<GameObject>();
            bonusLifetime = new List<int>();
            bonus.Add(new Ball(new Vector2(400, 430), 12.0f, Color.LightGreen, batch, textures[4], world));

            foreach (GameObject obj in bonus)
            {
                gamefield.add( obj );
                addToMyOnCollision( obj );
                bonusLifetime.Add( maxLifetime );
            }

            // border
            gamefield.add(new Square(new Vector2(size.X / 2, 0), new Vector2(size.X, 7), 0.0f, Color.DarkGreen, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(size.X / 2, size.Y ), new Vector2(size.X, 7), 0.0f, Color.DarkGreen, batch, textures[0], world));

            gamefield.add(new Square(new Vector2(0, size.Y / 2), new Vector2(7, size.Y), 0.0f, Color.DarkGreen, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(size.X, size.Y / 2), new Vector2(7, size.Y), 0.0f, Color.DarkGreen, batch, textures[0], world));


            // add objects

            addObjects.add( new Egg(new Vector2(0, 0), 15.0f, 13.0f, Color.Blue, batch, textures[1], world) );
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(50, 15), new Vector2(10, 5), -0.4f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new Ball(new Vector2(0, 0), 13.0f, Color.Blue, batch, textures[1], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(80, 10), new Vector2(10, 5), 0.0f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new Square(new Vector2(0, 0), new Vector2(40, 45), 0.2f, Color.Blue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(50, 15), new Vector2(10, 5), 0.4f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new Triangle(new Vector2(0, 0), new Vector2(50, 50), 0.0f, Color.Blue, batch, textures[7], world));
        
            foreach (GameObject obj in addObjects.objects)
            {
                obj.body.BodyType = BodyType.Dynamic;
            }
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            // test collision both balls
            if ( (f1.Body == ball1.body && f2.Body == ball2.body) || (f1.Body == ball2.body && f2.Body == ball1.body) )
            {
                if (overlay != null) overlay.CenterString = "ball collision";
                return true;
            }

            // test bonus objects
            foreach (GameObject obj in bonus)
            {
                if (f1.Body == obj.body || f2.Body == obj.body)
                {
                    if (overlay != null && obj.body.BodyType == BodyType.Static) overlay.CenterString = "Bonus";
                    obj.body.BodyType = BodyType.Dynamic;
                }
            }
            return true;
        }

        public override void update(GameTime gameTime)
        {
            for (int i = 0; i < bonus.Count; i++ )
            {
                if (bonus[i].body.BodyType == BodyType.Dynamic)
                {
                    bonusLifetime[i]--;
                    if (bonusLifetime[i] < 1)
                    {
                        gamefield.remove(bonus[i]);
                        world.RemoveBody(bonus[i].body);
                        bonusLifetime.RemoveAt(i);
                        bonus.RemoveAt(i);
                        break;
                    }
                }
            }

            base.update(gameTime);
        }
    }
}