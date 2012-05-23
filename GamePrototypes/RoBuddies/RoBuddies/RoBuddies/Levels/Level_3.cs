using System;
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
    class Level_3 : Level
    {
        private SwitchableWall switchDoor;

        public Level_3(Game1 game)
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
            loadRobot(new Vector2(20, 160));

            Texture2D square = game.Content.Load<Texture2D>("Sprites\\Square");
            backgroundColor = Color.LightBlue;

            pipes.Add(new Pipe(new Vector2(0, 70), 600, Color.Gray, square, gameWorld));

            List<PhysicObject> levelPhysicObjects = new List<PhysicObject>();
            levelPhysicObjects.Add(new Wall(new Vector2(0, 50), new Vector2(600, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 50), new Vector2(10, 150), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 200), new Vector2(300, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(100, 95), new Vector2(140, 80), Color.LightGray, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(300, 200), new Vector2(10, 80), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(300, 280), new Vector2(60, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(350, 200), new Vector2(10, 80), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(350, 94), new Vector2(150, 80), Color.LightGray, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(350, 200), new Vector2(250, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(600, 50), new Vector2(10, 150), Color.White, square, gameWorld));

            switchDoor = new SwitchableWall(new Vector2(600, 160), new Vector2(10, 40), false, Color.Brown, square, gameWorld);
            switchDoor.Activated = true;
            levelPhysicObjects.Add(switchDoor);

            boxes.Add(new MovableBox(square, new Vector2(280, 120), new Vector2(35, 80), gameWorld, true, player));

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
