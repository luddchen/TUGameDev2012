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

    // base class for levels
    class Level_1 : Level
    {
        private List<GameObject> bonus;
        private List<int> bonusLifetime;
        private int maxLifetime = 500;

        public Level_1(Game1 game, List<Texture2D> textures, SpriteBatch batch)
            : base(game, textures, batch)
        {
            this.size.X = 500;
            this.size.Y = 1000;

            ball1 = new Ball(new Vector2(50, 10), 10.0f, Color.IndianRed, batch, textures[1], world);
            ball1.body.BodyType = BodyType.Dynamic;
            gamefield.add(ball1);
            addToMyOnCollision(ball1);

            ball2 = new Ball(new Vector2(485, 15), 15.0f, Color.HotPink, batch, textures[3], world);
            ball2.body.BodyType = BodyType.Dynamic;
            gamefield.add(ball2);
            addToMyOnCollision(ball2);

            gamefield.add(new SquareStack(new Vector2(18, 40), new Vector2(50, 10), new Vector2(10, 5), 0.6f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(61, 64), new Vector2(50, 10), new Vector2(10, 5), 0.4f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(181, 93), new Vector2(200, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(303, 115), new Vector2(50, 10), new Vector2(10, 5), 0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new Pipeline(new Vector2(303, 100), new Vector2(50, 30), 3.0f, 0.1f, Color.Green, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(447, 116), new Vector2(120, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(320, 200), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(220, 230), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(400, 230), new Vector2(90, 10), new Vector2(10, 5), 0.3f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(120, 260), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));


            gamefield.add(new SquareStack(new Vector2(18, 340), new Vector2(50, 10), new Vector2(10, 5), 0.6f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(61, 364), new Vector2(50, 10), new Vector2(10, 5), 0.4f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(181, 393), new Vector2(200, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(303, 415), new Vector2(50, 10), new Vector2(10, 5), 0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(447, 416), new Vector2(120, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(320, 500), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(220, 530), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(400, 530), new Vector2(90, 10), new Vector2(10, 5), 0.3f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(120, 560), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));


            gamefield.add(new SquareStack(new Vector2(18, 640), new Vector2(50, 10), new Vector2(10, 5), 0.6f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(61, 664), new Vector2(50, 10), new Vector2(10, 5), 0.4f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(181, 693), new Vector2(200, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(303, 715), new Vector2(50, 10), new Vector2(10, 5), 0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(447, 716), new Vector2(120, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(320, 800), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(220, 830), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(400, 830), new Vector2(90, 10), new Vector2(10, 5), 0.3f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(120, 860), new Vector2(80, 10), new Vector2(10, 5), -0.1f, Color.Green, Color.LightGreen, batch, textures[0], world));


            gamefield.add(new SquareStack(new Vector2(18, 925), new Vector2(50, 10), new Vector2(10, 5), 0.6f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(61, 949), new Vector2(50, 10), new Vector2(10, 5), 0.4f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(181, 978), new Vector2(200, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(447, 971), new Vector2(120, 10), new Vector2(10, 5), -0.5f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new Triangle(new Vector2(350,975), new Vector2(50,50), 0.0f, batch, textures[7], world));


            // bonus objects
            bonus = new List<GameObject>();
            bonusLifetime = new List<int>();
            bonus.Add(new Egg(new Vector2(150, 360), 15.0f, 13.0f, Color.Green, batch, textures[1], world));
            Console.WriteLine("max Vertices: " + Settings.MaxPolygonVertices);
            bonus.Add(new Ball(new Vector2(150, 660), 12.0f, Color.Red, batch, textures[4], world));
            bonus.Add(new Ball(new Vector2(150, 945), 10.0f, Color.Yellow, batch, textures[4], world));

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
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(80, 10), new Vector2(10, 5), -0.6f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(50, 15), new Vector2(10, 5), -0.4f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(60, 20), new Vector2(10, 5), -0.2f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(80, 10), new Vector2(10, 5), 0.0f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(40, 45), new Vector2(10, 5), 0.2f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(50, 15), new Vector2(10, 5), 0.4f, Color.Blue, Color.LightBlue, batch, textures[0], world));
            addObjects.add(new SquareStack(new Vector2(0, 0), new Vector2(50, 10), new Vector2(10, 5), 0.6f, Color.Blue, Color.LightBlue, batch, textures[0], world));
        
            foreach (GameObject obj in addObjects.objects)
            {
                //gamefield.add(obj);
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

        public override void update(GameTime gameTime, Vector2 pos)
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

            base.update(gameTime, pos);
        }
    }
}