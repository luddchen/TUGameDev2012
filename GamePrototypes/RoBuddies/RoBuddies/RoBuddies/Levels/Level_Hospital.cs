using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_Hospital : Level
    {
        public Level_Hospital(Game1 game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundColor = Color.LightCyan;
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
