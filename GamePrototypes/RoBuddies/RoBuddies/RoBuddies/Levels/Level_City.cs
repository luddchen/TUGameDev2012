﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level_City : Level
    {
        public Level_City(Game1 game)
            : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundColor = Color.LightBlue;

            Texture2D sunTex = game.Content.Load<Texture2D>("Sprites\\Sun");
            GameObject sun = new GameObject(sunTex, new Vector2(0.3f, 0.7f)); sun.Scale = 0.3f;
            backgroundLayer.add(sun);

            Texture2D tree = game.Content.Load<Texture2D>("Sprites\\tree4");

            Layer layer1 = new Layer(Camera);
            layer1.LoadContent();
            layer1.Depth = 0.75f;
            layer1.add(new GameObject(tree, new Vector2(200, tree.Height / 2)));

            layers.Add(layer1);
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
