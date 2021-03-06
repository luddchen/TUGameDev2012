﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Robuddies.Objects;

using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_2 : Level
    {
        private List<Switch> switches;
        private SwitchableWall switchWall;
        private SwitchableWall switchDoor;

        public Level_2(Game1 game)
            : base(game)
        {

        }

        protected override void LoadContentBackground()
        {
            Texture2D sunTex = game.Content.Load<Texture2D>("Sprites\\Sun");
            Texture2D cloudTex = game.Content.Load<Texture2D>("Sprites\\Cloud1");

            GameObject sun = new GameObject(sunTex, new Vector2(50f, 0.7f)); sun.Scale = 0.7f;
            backgroundLayer.add(sun);

            Layer layer10 = new Layer(Camera, new Vector2(0.0f, 1.0f)); layer10.LoadContent(); layer10.Depth = 0.98f;
            Layer layer11 = new Layer(Camera, new Vector2(0.1f, 1.0f)); layer11.LoadContent(); layer11.Depth = 0.96f;
            Layer layer12 = new Layer(Camera, new Vector2(0.2f, 1.0f)); layer12.LoadContent(); layer12.Depth = 0.94f;
            Layer layer13 = new Layer(Camera, new Vector2(0.3f, 1.0f)); layer13.LoadContent(); layer13.Depth = 0.92f;
            Layer layer14 = new Layer(Camera, new Vector2(0.4f, 1.0f)); layer14.LoadContent(); layer14.Depth = 0.90f;
            Random random = new Random();
            GameObject cloud;
            for (int i = 0; i < 5; i++)
            {
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next(-100, 0)));
                cloud.Scale = (float)(random.NextDouble() / 50 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer10.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next(-100, 0)));
                cloud.Scale = (float)(random.NextDouble() / 50 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer11.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next(-100, 0)));
                cloud.Scale = (float)(random.NextDouble() / 50 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer12.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next(-100, 0)));
                cloud.Scale = (float)(random.NextDouble() / 50 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer13.add(cloud);
                cloud = new GameObject(cloudTex, new Vector2(random.Next(-500, 2000), random.Next(-100, 0)));
                cloud.Scale = (float)(random.NextDouble() / 50 + 0.3d);
                cloud.Color = new Color(random.Next(192, 255), random.Next(192, 255), random.Next(192, 255));
                cloud.Rotation = (float)((random.NextDouble() - 0.5d) * MathHelper.PiOver4);
                layer14.add(cloud);
            }

            layers.Add(layer10);
            layers.Add(layer11);
            layers.Add(layer12);
            layers.Add(layer13);
            layers.Add(layer14);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            loadRobot(new Vector2(50, 160));

            Texture2D square = game.Content.Load<Texture2D>("Sprites\\Square");
            backgroundColor = Color.LightBlue;

            pipes.Add(new Pipe(new Vector2(80, 70), 180, Color.Gray, square, gameWorld));

            List<PhysicObject> levelPhysicObjects = new List<PhysicObject>();
            levelPhysicObjects.Add(new Wall(new Vector2(0, 50), new Vector2(300, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 200), new Vector2(300, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 50), new Vector2(10, 150), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(80, 50), new Vector2(10, 30), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(260, 50), new Vector2(10, 30), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(300, 50), new Vector2(10, 150), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(265, 150), new Vector2(40, 10), Color.White, square, gameWorld));

            //switchWall = new SwitchableWall(new Vector2(250, 100), new Vector2(10, 100), true, Color.Red, square, gameWorld);
            //Switch wallSwitch = new Switch(switchWall, player, square, new Vector2(280, 190));
            //mainLayer.add(wallSwitch);
            //levelPhysicObjects.Add(switchWall);

            switchDoor = new SwitchableWall(new Vector2(300, 160), new Vector2(10, 40), false, Color.Brown, square, gameWorld);
            Switch doorSwitch = new Switch(switchDoor, player, square, new Vector2(280, 120));
            mainLayer.add(doorSwitch);
            levelPhysicObjects.Add(switchDoor);

            boxes.Add(new MovableBox(square, new Vector2(15, 160), new Vector2(35, 40), gameWorld, false, player));

            foreach (Pipe pipe in pipes)
            {
                addToMyOnCollision(pipe);
                mainLayer.add(pipe);
            }

            foreach (PhysicObject physicObj in levelPhysicObjects)
            {
                addToMyOnCollision(physicObj);
                mainLayer.add(physicObj);
            }

            foreach (MovableBox box in boxes)
            {
                addToMyOnCollision(box);
                mainLayer.add(box);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (switchDoor.Activated && !player.IsSeperated)
            {
                mainLayer.add(switchDoor);
                //if (overlay != null) overlay.CenterString = "Level Completed";
            }
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            if ((f1.Body == player.BudBudi.Physics.Body && f2.Body == switchDoor.Body ||
                f2.Body == player.BudBudi.Physics.Body && f1.Body == switchDoor.Body)
                && switchDoor.Activated)
            {
                game.level = new Level_3(game);
                game.level.LoadContent();
                Console.Out.WriteLine("Level_3 loaded");
                return true;
            }

            return base.MyOnCollision(f1, f2, contact);
        }

        public override void MyOnSeperation(Fixture f1, Fixture f2)
        {
            base.MyOnSeperation(f1, f2);
        }
    }
}
