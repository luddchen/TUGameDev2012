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

namespace Fall_Ball
{

    // base class for levels
    class Level_1 : Level
    {
        private GameObject specialObject1;
        private GameObject specialObject2;

        public Level_1(Game game, List<Texture2D> textures, SpriteBatch batch, MouseController mouse)
            : base(game, textures, batch, mouse)
        {

            ball1 = new Ball(new Vector2(30, 30), 10.0f, Color.IndianRed, batch, textures[1], world);
            ball1.body.BodyType = BodyType.Dynamic;

            ball2 = new Ball(new Vector2(200, 30), 15.0f, Color.HotPink, batch, textures[3], world);
            ball2.body.BodyType = BodyType.Dynamic;

            gamefield.add(ball1);
            gamefield.add(ball2);

            addToMyOnCollision(ball1);
            addToMyOnCollision(ball2);

            gamefield.add(new SquareStack(new Vector2(50, 100), new Vector2(100, 10), new Vector2(10, 5), 0.6f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(125, 140), new Vector2(70, 6), new Vector2(10, 6), 0.05f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(225, 175), new Vector2(150, 10), new Vector2(10, 5), 0.2f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new SquareStack(new Vector2(320, 250), new Vector2(350, 5), new Vector2(10, 5), -0.4f, Color.Green, Color.LightGreen, batch, textures[0], world));

            gamefield.add(new Ball(new Vector2(470, 170), 12.0f, Color.Indigo, batch, textures[2], world));
            gamefield.add(new Ball(new Vector2(440, 185), 12.0f, Color.LightSteelBlue, batch, textures[4], world));
            gamefield.add(new Ball(new Vector2(445, 160), 12.0f, Color.LemonChiffon, batch, textures[5], world));
            gamefield.add(new Ball(new Vector2(445, 140), 12.0f, Color.Yellow, batch, textures[6], world));

            gamefield.add(new Square(new Vector2(320, 250), new Vector2(10, 10), -0.4f, batch,  textures[0], world));
            gamefield.add(new Square(new Vector2(50, 335), new Vector2(8, 50), -0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(200, 400), new Vector2(300, 8), 0.3f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(390, 500), new Vector2(7, 100), 0.9f, batch, textures[0], world));

            gamefield.add(new Square(new Vector2(250, 600), new Vector2(160, 8), 0.0f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(150, 700), new Vector2(120, 8), 0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(350, 700), new Vector2(80, 8), -0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(195, 800), new Vector2(80, 8), 0.2f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(305, 800), new Vector2(80, 8), -0.2f, batch, textures[0], world));

            gamefield.add(new SquareStack(new Vector2(250, 905), new Vector2(400, 15), new Vector2(20, 5), 0.0f, Color.Green, Color.LightGreen, batch, textures[0], world));
            gamefield.add(new Ball(new Vector2(50, 885), 12.0f, Color.LightSalmon, batch, textures[2], world));
            gamefield.add(new Ball(new Vector2(450, 885), 12.0f, Color.MistyRose, batch, textures[6], world));

            this.size.X = 550;
            this.size.Y = 950;
            gamefield.add(new Square(new Vector2(size.X/2, -5), new Vector2(size.X, 10), 0.0f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(size.X / 2, size.Y+5), new Vector2(size.X, 10), 0.0f, batch, textures[0], world));

            gamefield.add(new Square(new Vector2(5, size.Y/2), new Vector2(10, size.Y), 0.0f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(size.X - 5, size.Y / 2), new Vector2(10, size.Y), 0.0f, batch, textures[0], world));


            addObjects.add(new Square(new Vector2(0, 0), new Vector2(80, 7), 0.0f, Color.Blue, batch, textures[0], world));
            addObjects.add(new Ball(new Vector2(0, 0), 12.0f, Color.Blue, batch, textures[6], world));
            addObjects.add(new Ball(new Vector2(0, 0), 12.0f, Color.Blue, batch, textures[5], world));

            specialObject1 = new Ball(new Vector2(250, 800), 15.0f, Color.LightGreen, batch, textures[6], world);
            gamefield.add(specialObject1);
            addToMyOnCollision(specialObject1);

            specialObject2 = new Ball(new Vector2(150, 350), 15.0f, Color.Green, batch, textures[6], world);
            gamefield.add(specialObject2);
            addToMyOnCollision(specialObject2);

            gamefield.add(new Pipeline(new Vector2(270, 230), new Vector2(120, 40), 5.0f, -0.1f, Color.Green, batch, textures[0], world));
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            if (f1.Body == ball1.body || f2.Body == ball1.body || f1.Body == ball2.body || f2.Body == ball2.body)
            {
                if (f1.Body == specialObject1.body || f2.Body == specialObject1.body)
                {
                    gamefield.remove(specialObject1);
                    removeFromMyOnCollision(specialObject1);
                    world.RemoveBody(specialObject1.body);
                    world.Gravity.Y /= 3;
                    return false;
                }
                if (f1.Body == specialObject2.body || f2.Body == specialObject2.body)
                {
                    specialObject2.body.BodyType = BodyType.Dynamic;
                    if(overlay!=null) overlay.CenterString = "Bonus";
                    //specialObject2.body.BodyType = BodyType.Kinematic;
                    //specialObject2.body.LinearVelocity += new Vector2(0.3f, 0.3f);
                }
            }
            return true;
        }

        public override void update(GameTime gameTime, Vector2 pos)
        {
            
            base.update(gameTime, pos);
        }
    }
}