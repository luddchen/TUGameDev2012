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
            Texture2D background = game.Content.Load<Texture2D>("Sprites\\background");
            Texture2D home = game.Content.Load<Texture2D>("Sprites\\home");
            Texture2D tree1 = game.Content.Load<Texture2D>("Sprites\\tree1");
            Texture2D tree2 = game.Content.Load<Texture2D>("Sprites\\tree2");
            Texture2D tree3 = game.Content.Load<Texture2D>("Sprites\\tree3");
            Texture2D tree4 = game.Content.Load<Texture2D>("Sprites\\tree4");
            Texture2D budTex = game.Content.Load<Texture2D>("Sprites\\Bud2");
            Texture2D budiTex = game.Content.Load<Texture2D>("Sprites\\Budi2");
            Texture2D broTex = game.Content.Load<Texture2D>("Sprites\\Bro2");

            Layer layer1 = new Layer(game);
            layer1.LoadContent();
            layer1.LayerDepth = 1.0f;
            layer1.add( new GameObject(background, new Vector2(background.Width / 2, background.Height / 2)));

            Layer layer2 = new Layer(game);
            layer2.LoadContent();
            layer2.LayerDepth = 0.8f;
            layer2.add(new GameObject(home, new Vector2(home.Width / 2, home.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width*2, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 4, tree1.Height / 2)));
            layer2.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 6, tree1.Height / 2)));

            Layer layer3 = new Layer(game);
            layer3.LoadContent();
            layer3.LayerDepth = 0.85f;
            layer3.add(new GameObject(tree1, new Vector2(home.Width, tree1.Height / 2)));
            layer3.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 3, tree1.Height / 2)));
            layer3.add(new GameObject(tree3, new Vector2(home.Width + tree1.Width * 3, tree3.Height / 2)));
            layer3.add(new GameObject(tree4, new Vector2(home.Width + tree1.Width * 5, tree4.Height / 2)));
            layer3.add(new GameObject(tree1, new Vector2(home.Width + tree1.Width * 7, tree1.Height / 2)));

            layers.Add(layer1);
            layers.Add(layer3);
            layers.Add(layer2);

            mainLayer = new Layer(game);
            mainLayer.LoadContent();
            mainLayer.LayerDepth = 0.5f;
            Bud = new GameObject(budTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height + budTex.Height / 2)*0.1f)); Bud.Size *= 0.1f;
            Budi = new GameObject(budiTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height / 2) * 0.1f)); Budi.Size *= 0.1f;
            Bro = new GameObject(broTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height + budTex.Height + broTex.Height / 2) * 0.1f)); Bro.Size *= 0.1f;
            mainLayer.add(Budi);
            mainLayer.add(Bud);
            mainLayer.add(Bro);

            layers.Add(mainLayer);
        }

    }
}
