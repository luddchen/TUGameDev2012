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

namespace Fall_Ball
{

    // base class for levels
    class Level_1 : Level
    {

        public Level_1(List<Texture2D> textures, SpriteBatch batch)
            : base(textures, batch)
        {
            ball1 = new Ball(new Vector2(30, 0), 10.0f, Color.IndianRed, batch, textures[1], world);
            ball1.body.BodyType = BodyType.Dynamic;

            ball2 = new Ball(new Vector2(200, 0), 15.0f, Color.HotPink, batch, textures[3], world);
            ball2.body.BodyType = BodyType.Dynamic;

            gamefield.add(ball1);
            gamefield.add(ball2);

            gamefield.add(new Square(new Vector2(50, 100), new Vector2(100, 10), 0.6f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(125, 140), new Vector2(70, 5), 0.05f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(225, 175), new Vector2(150, 10), 0.2f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(320, 250), new Vector2(350, 5), -0.4f, batch, textures[0], world));

            gamefield.add(new Ball(new Vector2(470, 170), 12.0f, Color.Indigo, batch, textures[2], world));
            gamefield.add(new Ball(new Vector2(440, 185), 12.0f, Color.LightSteelBlue, batch, textures[4], world));
            gamefield.add(new Ball(new Vector2(445, 160), 12.0f, Color.LemonChiffon, batch, textures[5], world));
            gamefield.add(new Ball(new Vector2(445, 140), 12.0f, Color.Yellow, batch, textures[6], world));

            gamefield.add(new Square(new Vector2(320, 250), new Vector2(10, 10), -0.4f, batch,  textures[0], world));
            gamefield.add(new Square(new Vector2(50, 335), new Vector2(7, 50), -0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(200, 400), new Vector2(300, 7), 0.3f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(390, 500), new Vector2(7, 100), 0.9f, batch, textures[0], world));

            gamefield.add(new Square(new Vector2(250, 600), new Vector2(160, 7), 0.0f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(150, 700), new Vector2(80, 7), 0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(350, 700), new Vector2(80, 7), -0.5f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(195, 800), new Vector2(80, 7), 0.2f, batch, textures[0], world));
            gamefield.add(new Square(new Vector2(305, 800), new Vector2(80, 7), -0.2f, batch, textures[0], world));

            gamefield.add(new Square(new Vector2(250, 900), new Vector2(400, 7), 0.0f, batch, textures[0], world));
            gamefield.add(new Ball(new Vector2(50, 885), 12.0f, Color.LightSalmon, batch, textures[2], world));
            gamefield.add(new Ball(new Vector2(450, 885), 12.0f, Color.MistyRose, batch, textures[6], world));

            this.size.X = 550;
            this.size.Y = 950;

            addObjects.add(new Square(new Vector2(0, 0), new Vector2(80, 7), 0.0f, Color.Blue, batch, textures[0], world));
            addObjects.add(new Ball(new Vector2(0, 0), 12.0f, Color.Blue, batch, textures[6], world));
            addObjects.add(new Ball(new Vector2(0, 0), 12.0f, Color.Blue, batch, textures[5], world));
        }

    }
}