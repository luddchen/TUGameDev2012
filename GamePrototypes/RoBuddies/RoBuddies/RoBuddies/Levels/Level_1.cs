using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Robuddies.Objects;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_1 : Level
    {

        public Level_1(Game game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent(); 
            backgroundColor = Color.LightBlue;

            Texture2D sunTex = game.Content.Load<Texture2D>("Sprites\\Sun"); 
            Texture2D cloudTex = game.Content.Load<Texture2D>("Sprites\\Cloud1");
            Texture2D groundTex = game.Content.Load<Texture2D>("Sprites\\Ground1");
            Texture2D ground2Tex = game.Content.Load<Texture2D>("Sprites\\Ground2");

            GameObject cloud1 = new GameObject(cloudTex, new Vector2(0.25f, 0.8f)); cloud1.Size = 0.4f;
            GameObject cloud2 = new GameObject(cloudTex, new Vector2(0.75f, 0.7f)); cloud2.Size = 0.3f;
            GameObject sun = new GameObject(sunTex, new Vector2(0.4f, 0.7f)); sun.Size = 0.3f;
            backgroundLayer.add(cloud1);
            backgroundLayer.add(cloud2);
            backgroundLayer.add(sun);

            Texture2D home = game.Content.Load<Texture2D>("Sprites\\home");
            Texture2D tree1 = game.Content.Load<Texture2D>("Sprites\\tree1");
            Texture2D tree2 = game.Content.Load<Texture2D>("Sprites\\tree2");
            Texture2D tree3 = game.Content.Load<Texture2D>("Sprites\\tree3");
            Texture2D tree4 = game.Content.Load<Texture2D>("Sprites\\tree4");

            Texture2D squareCloud = game.Content.Load<Texture2D>("Sprites\\Cloud1");

            Texture2D square = game.Content.Load<Texture2D>("Sprites\\Square");

            Layer layer10 = new Layer(); layer10.LoadContent(); layer10.Depth = 0.98f;
            Layer layer11 = new Layer(); layer11.LoadContent(); layer11.Depth = 0.96f;
            Layer layer12 = new Layer(); layer12.LoadContent(); layer12.Depth = 0.94f;
            Layer layer13 = new Layer(); layer13.LoadContent(); layer13.Depth = 0.92f;
            Layer layer14 = new Layer(); layer14.LoadContent(); layer14.Depth = 0.90f;
            Random random = new Random();
            GameObject cloud;
            for (int i = 0; i < 5; i++)
            {
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next((int)(home.Height*0.5f), (int)(home.Height*1.9f)))); 
                cloud.Size = (float)(random.NextDouble()/8 + 0.3d);
                cloud.Color = new Color(random.Next(192,255), random.Next(192,255), random.Next(192,255));
                cloud.Rotation = (float)((random.NextDouble()-0.5d) * MathHelper.PiOver4);
                layer10.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next((int)(home.Height * 0.5f), (int)(home.Height * 1.9f))));
                cloud.Size = (float)(random.NextDouble() / 7 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer11.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next((int)(home.Height * 0.5f), (int)(home.Height * 1.9f))));
                cloud.Size = (float)(random.NextDouble() / 6 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer12.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next((int)(home.Height * 0.5f), (int)(home.Height * 1.9f))));
                cloud.Size = (float)(random.NextDouble() / 5 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer13.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next((int)(home.Height * 0.5f), (int)(home.Height * 1.9f))));
                cloud.Size = (float)(random.NextDouble() / 4 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer14.add(cloud);
            }

            Layer layer2 = new Layer();
            layer2.LoadContent();
            layer2.Depth = 0.7f;
            layer2.add(new GameObject(home, new Vector2(home.Width / 2, groundTex.Height + home.Height / 2)));

            Layer layer30 = new Layer();
            layer30.LoadContent();
            layer30.Depth = 0.72f;
            GameObject tree;
            for (int i = 0; i < 10; i++)
            {
                tree = new GameObject(tree1, new Vector2(home.Width + random.Next(-1000,2000), (float)(groundTex.Height * (1.0f+random.NextDouble()/10) + tree1.Height / 2)));
                layer30.add(tree);
            }

            Layer layer31 = new Layer();
            layer31.LoadContent();
            layer31.Depth = 0.8f;
            for (int i = 0; i < 10; i++)
            {
                tree = new GameObject(tree3, new Vector2(home.Width + random.Next(-1000, 2000), (float)(groundTex.Height * (1.5f + random.NextDouble() / 10) + tree3.Height / 2)));
                layer31.add(tree);
            }

            Layer layer32 = new Layer();
            layer32.LoadContent();
            layer32.Depth = 0.85f;
            for (int i = 0; i < 10; i++)
            {
                tree = new GameObject(tree1, new Vector2(home.Width + random.Next(-1000, 2000), (float)(groundTex.Height * (2.0f + random.NextDouble() / 10) + tree1.Height / 2)));
                layer32.add(tree);
            }

            Layer layer4 = new Layer();  // the road to walk
            layer4.LoadContent();
            layer4.Depth = 0.52f;

            GameObject ground;
            for (int i = -10; i < 30; i++)
            {
                ground = new GameObject(groundTex, new Vector2(i * (groundTex.Width - 25), groundTex.Height/2 - 2));
                layer4.add(ground);
            }

            Layer layer5 = new Layer();
            layer5.LoadContent();
            layer5.Depth = 0.88f;
            GameObject ground2 = new GameObject(ground2Tex, new Vector2(ground2Tex.Width * 30, ground2Tex.Height * 20)); ground2.Size = 60;
            layer5.add(ground2);
            ground2 = new GameObject(ground2Tex, new Vector2(-ground2Tex.Width * 30, ground2Tex.Height * 20)); ground2.Size = 60;
            layer5.add(ground2);

            layers.Add(layer10);
            layers.Add(layer11);
            layers.Add(layer12);
            layers.Add(layer13);
            layers.Add(layer14);
            layers.Add(layer2);
            layers.Add(layer30);
            layers.Add(layer31);
            layers.Add(layer32);
            layers.Add(layer4);
            layers.Add(layer5);

            // testing collision
            //PhysicObject squareObj = new PhysicObject(square, new Vector2(100, 200), gameWorld);
            //squareObj.Color = Color.Red;
            //squareObj.Size = 0.5f;
            //squareObj.Body.BodyType = BodyType.Dynamic;
            //FixtureFactory.AttachRectangle(150, 150, 10, Vector2.Zero, squareObj.Body);
            //addToMyOnCollision(squareObj);
            //mainLayer.add(squareObj);

            //PhysicObject squareObj2 = new PhysicObject(square, new Vector2(200, 200), gameWorld);
            //squareObj2.Color = Color.Red;
            //squareObj2.Size = 0.5f;
            //squareObj2.Body.BodyType = BodyType.Dynamic;
            //FixtureFactory.AttachRectangle(150, 150, 10, Vector2.Zero, squareObj2.Body);
            //addToMyOnCollision(squareObj2);
            //mainLayer.add(squareObj2);

            // testing wall
            Wall wall1 = new Wall(new Vector2(100, 500), new Vector2(50000, 100), new Vector2(10, 5), 0.0f, Color.Green, Color.LightGreen, square, gameWorld);
            wall1.Body.BodyType = BodyType.Static;
            FixtureFactory.AttachRectangle(50, 200, 10, Vector2.Zero, wall1.Body);
            addToMyOnCollision(wall1);
            mainLayer.add(wall1);
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            Console.WriteLine("test");
            // test collision both balls
            //if ((f1.Body == BudBudi.body && f2.Body == ball2.body) || (f1.Body == ball2.body && f2.Body == ball1.body))
            //{
            //    if (overlay != null) overlay.CenterString = "ball collision";
            //    return true;
            //}
            return true;
        }
    }
}
