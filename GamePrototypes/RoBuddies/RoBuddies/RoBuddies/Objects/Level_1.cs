using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class Level_1 : Level
    {

        public Level_1(Game1 game) : base(game)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Texture2D background = game.Content.Load<Texture2D>("Sprites\\background");
            Texture2D home = game.Content.Load<Texture2D>("Sprites\\home");
            Texture2D tree1 = game.Content.Load<Texture2D>("Sprites\\tree1");
            Texture2D tree2 = game.Content.Load<Texture2D>("Sprites\\tree2");
            Texture2D tree3 = game.Content.Load<Texture2D>("Sprites\\tree3");
            Texture2D tree4 = game.Content.Load<Texture2D>("Sprites\\tree4");

            Layer layer1 = new Layer(game);
            layer1.LoadContent();
            layer1.Depth = 1.0f;
            layer1.add( new GameObject(background, new Vector2(0, background.Height / 2)));

            Layer layer2 = new Layer(game);
            layer2.LoadContent();
            layer2.Depth = 0.7f;
            layer2.add(new GameObject(home, new Vector2(home.Width / 2, home.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width*2, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 4, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 6, tree1.Height / 2)));

            Layer layer3 = new Layer(game);
            layer3.LoadContent();
            layer3.Depth = 0.8f;
            layer3.add(new GameObject(tree1, new Vector2(home.Width, tree1.Height / 2)));
            layer3.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 3, tree1.Height / 2)));
            layer3.add(new GameObject(tree3, new Vector2(home.Width + tree1.Width * 3, tree3.Height / 2)));
            layer3.add(new GameObject(tree4, new Vector2(home.Width + tree1.Width * 5, tree4.Height / 2)));
            layer3.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 7, tree1.Height / 2)));

            layers.Add(layer1);
            layers.Add(layer2);
            layers.Add(layer3);

        }

    }
}
