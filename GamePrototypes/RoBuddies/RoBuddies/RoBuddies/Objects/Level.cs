using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{
    class Level
    {

        public List<Layer> layers;

        public Layer mainLayer;
        public GameObject Bud;
        public GameObject Budi;
        public GameObject Bro;

        public Game game;
        public SpriteBatch spriteBatch;

        float offset;
        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }
                Bud.setPosition(offset, Bud.Position.Y);
                Budi.setPosition(offset, Budi.Position.Y);
                Bro.setPosition(offset, Bro.Position.Y);
            }

            get { return offset; }
        }

        public Level(Game1 game)
        {
            this.game = game;
            layers = new List<Layer>();
        }

        public virtual void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            Texture2D budTex = game.Content.Load<Texture2D>("Sprites\\Bud2");
            Texture2D budiTex = game.Content.Load<Texture2D>("Sprites\\Budi2");
            Texture2D broTex = game.Content.Load<Texture2D>("Sprites\\Bro2");

            mainLayer = new Layer(game);
            mainLayer.LoadContent();
            mainLayer.Depth = 0.5f;
            Bud = new GameObject(budTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height + budTex.Height / 2) * 0.1f)); Bud.Size *= 0.1f;
            Budi = new GameObject(budiTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height / 2) * 0.1f)); Budi.Size *= 0.1f;
            Bro = new GameObject(broTex, new Vector2(TitleSafe.Width / 2, (budiTex.Height + budTex.Height + broTex.Height / 2) * 0.1f)); Bro.Size *= 0.1f;
            mainLayer.add(Budi);
            mainLayer.add(Bud);
            mainLayer.add(Bro);

            layers.Add(mainLayer);
        }

        Rectangle titleSafe;
        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set
            {
                titleSafe = value;
                foreach (Layer l in layers) { l.TitleSafe = titleSafe; }
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (Layer l in layers)
            {
                l.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

    }
}
