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
            levelPhysicObjects.Add(new Wall(new Vector2(0, 1000), new Vector2(2000, 100), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 2000), new Vector2(3000, 100), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(0, 1000), new Vector2(100, 1000), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(2000, 0), new Vector2(100, 1000), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(2000, 0), new Vector2(1000, 100), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(3000, 0), new Vector2(100, 2000), Color.White, square, gameWorld));
            levelPhysicObjects.Add(new Wall(new Vector2(2500, 1000), new Vector2(100, 1000), Color.Red, square, gameWorld));

            foreach (PhysicObject physicObj in levelPhysicObjects)
            {
                addToMyOnCollision(physicObj);
                mainLayer.add(physicObj);
            }


        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            // test collision between upper and lower part
            if ((f1.Body == Player.Bud.Physics.Body && f2.Body == Player.Budi.Physics.Body) || (f1.Body == Player.Budi.Physics.Body && f2.Body == Player.Bud.Physics.Body))
            {
                player.IsCombinable = true;
                return true;
            }            
            return true;
        }

        public override void MyOnSeperation(Fixture f1, Fixture f2)
        {
            // test seperation between upper and lower part
            // damn this only happens if myOnCollision returned true. This will need an other approach.
            if ((f1.Body == Player.Bud.Physics.Body && f2.Body == Player.Budi.Physics.Body) || (f1.Body == Player.Budi.Physics.Body && f2.Body == Player.Bud.Physics.Body))
            {
                player.IsCombinable = false;
            }            
        }
    }
}
