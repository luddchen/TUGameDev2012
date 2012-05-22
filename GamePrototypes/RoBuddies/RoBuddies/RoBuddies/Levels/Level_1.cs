using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Robuddies.Objects;
using Robuddies.Interfaces;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_1 : Level
    {
        private List<Switch> switches;
        private SwitchableWall switchWall;

        public Level_1(Game1 game) 
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent(); 
            backgroundColor = Color.LightBlue;

            Texture2D square = game.Content.Load<Texture2D>("Sprites\\Square");

            List<PhysicObject> levelPhysicObjects = new List<PhysicObject>();
            levelPhysicObjects.Add(new Wall(new Vector2(0, 100), new Vector2(200, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 200), new Vector2(300, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 100), new Vector2(10, 100), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(200, 0), new Vector2(10, 100), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(200, 0), new Vector2(100, 10), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(300, 0), new Vector2(10, 200), Color.White, square, gameWorld));
            pipes.Add(new Pipe(new Vector2(215, 20), 80, Color.Gray, square, gameWorld));

            switchWall = new SwitchableWall(new Vector2(250, 100), new Vector2(10, 100), Color.Red, square, gameWorld);
            Switch doorSwitch = new Switch(switchWall, player, square, new Vector2(280, 190));
            mainLayer.add(doorSwitch);

            levelPhysicObjects.Add(switchWall);

            foreach (PhysicObject physicObj in levelPhysicObjects)
            {
                addToMyOnCollision(physicObj);
                mainLayer.add(physicObj);
            }

            foreach (Pipe pipe in pipes)
            {
                addToMyOnCollision(pipe);
                mainLayer.add(pipe);
            }

            List<Switch> switches = new List<Switch>();
            switches.Add(doorSwitch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return base.MyOnCollision(f1, f2, contact);
        }

        public override void MyOnSeperation(Fixture f1, Fixture f2)
        {
            base.MyOnSeperation(f1, f2);
        }
    }
}
