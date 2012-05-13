using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Robuddies.Objects;

namespace Robuddies.Levels
{
    class Level
    {
        private int seperationDelay = 0;
        private float offset;
        private Rectangle titleSafe;

        protected List<Layer> layers;
        protected Layer backgroundLayer;
        protected Layer mainLayer;

        protected GameObject bud;
        protected GameObject budi;
        //protected GameObject Bro;
        protected GameObject budBudi;
        protected GameObject controledObject;

        protected World gameWorld;
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected Color backgroundColor;
        protected bool seperated;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public GameObject ControledObject
        {
            get { return controledObject; }
            set { controledObject = value; }
        }

        public World GameWorld
        {
            get { return gameWorld; }
        }

        public void seperate()
        {
            if (seperationDelay > 0) return;

            if (!seperated)
            {
                mainLayer.remove(budBudi);
                mainLayer.add(bud);
                mainLayer.add(budi);
                controledObject = bud;
                seperated = true;
                seperationDelay = 10;
            }
            else
            {
                mainLayer.add(budBudi);
                controledObject = budBudi;
                mainLayer.remove(bud);
                mainLayer.remove(budi);
                seperated = false;
                seperationDelay = 10;
            }
        }


        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }

                // todo.begin
                bud.setPosition(offset, bud.Position.Y);
                budi.setPosition(offset, budi.Position.Y);
                budBudi.setPosition(offset, budBudi.Position.Y);
                // todo.end
            }

            get { return offset; }
        }

        public Level(Game game)
        {
            this.game = game;
            this.gameWorld = new World(Vector2.Zero);
            layers = new List<Layer>();
            seperated = false;
            offset = 0;
            backgroundColor = Color.Black;
        }

        public  virtual void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            backgroundLayer = new BackgroundLayer();
            layers.Add( backgroundLayer );

            mainLayer = new Layer();
            mainLayer.LoadContent();
            mainLayer.Depth = 0.5f;
            bud = new Bud(game.Content, new Vector2(TitleSafe.Width / 2, 0)); bud.Size *= 0.3f;
            budi = new Budi(game.Content, new Vector2(TitleSafe.Width / 2, 200)); budi.Size *= 0.3f;
            budBudi = new BudBudi(game.Content, new Vector2(TitleSafe.Width / 2, 0)); budBudi.Size *= 0.3f;
            mainLayer.add(budBudi);
            controledObject = budBudi;

            layers.Add(mainLayer);
        }

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
            // todo
            seperationDelay--;
            if (!seperated) { Offset = budBudi.Position.X; }
            if (seperated) { Offset = bud.Position.X; }
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
