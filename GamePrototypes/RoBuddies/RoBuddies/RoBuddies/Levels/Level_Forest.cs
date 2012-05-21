using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_Forest : Level
    {
        public Level_Forest(Game1 game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundColor = Color.Green;
        }

        public override bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public override void MyOnSeperation(Fixture f1, Fixture f2)
        {

        }
    }
}
