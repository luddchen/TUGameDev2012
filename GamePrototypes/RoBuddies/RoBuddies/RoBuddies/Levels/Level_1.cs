﻿using System;
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
            levelPhysicObjects.Add(new Wall(new Vector2(250, 100), new Vector2(10, 100), Color.Red, square, gameWorld));

            foreach (PhysicObject physicObj in levelPhysicObjects)
            {
                addToMyOnCollision(physicObj);
                mainLayer.add(physicObj);
            }


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
