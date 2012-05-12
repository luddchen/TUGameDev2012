using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;

namespace Robuddies.Levels
{
    class Level
    {

        public List<Layer> layers;

        public Layer mainLayer;
        public GameObject Bud;
        public GameObject Budi;
        //public GameObject Bro;
        public GameObject BudBudi;

        public bool seperated;
        int seperationDelay = 0;
        public void seperate()
        {
            if (seperationDelay > 0) return;
            if (!seperated)
            {
                mainLayer.remove(BudBudi);
                mainLayer.add(Bud);
                mainLayer.add(Budi);
                seperated = true;
                seperationDelay = 10;
            }
            else
            {
                mainLayer.add(BudBudi);
                mainLayer.remove(Bud);
                mainLayer.remove(Budi);
                seperated = false;
                seperationDelay = 10;
            }
        }

        public Game game;
        public SpriteBatch spriteBatch;

        float offset;
        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }

                // todo.begin
                Bud.setPosition(offset, Bud.Position.Y);
                Budi.setPosition(offset, Budi.Position.Y);
                BudBudi.setPosition(offset, BudBudi.Position.Y);
                // todo.end
            }

            get { return offset; }
        }

        public Level(Game game)
        {
            this.game = game;
            layers = new List<Layer>();
            seperated = false;
        }

        public virtual void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            Texture2D budTex = game.Content.Load<Texture2D>("Sprites\\Bud");
            Texture2D budiTex = game.Content.Load<Texture2D>("Sprites\\Budi");

            mainLayer = new Layer();
            mainLayer.LoadContent();
            mainLayer.Depth = 0.5f;
            Bud = new GameObject(budTex, new Vector2(TitleSafe.Width / 2, (budTex.Height / 2) * 0.15f)); Bud.Size *= 0.15f;
            Budi = new GameObject(budiTex, new Vector2(TitleSafe.Width / 2, (budTex.Height + budiTex.Height) * 0.15f)); Budi.Size *= 0.15f;
            BudBudi = new BudBudi(game.Content, new Vector2(TitleSafe.Width / 2, 0)); BudBudi.Size *= 0.3f;
            mainLayer.add(BudBudi);

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

        public void Update(GameTime gameTime)
        {
            // only for testing seperation
            seperationDelay--;
            Offset = BudBudi.Position.X;
            mainLayer.Update(gameTime);
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
