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

        protected Robot player;
        protected GameObject activePart;

        protected World gameWorld;
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected Color backgroundColor;

        public Robot Player
        {
            get { return player; }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public GameObject ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }

        public World GameWorld
        {
            get { return gameWorld; }
        }

        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }

                // todo.begin
                player.setPosition(offset, player.Position.Y);
                //budi.setPosition(offset, budi.Position.Y);
                //budBudi.setPosition(offset, budBudi.Position.Y);
                // todo.end
            }

            get { return offset; }
        }

        public Level(Game game)
        {
            this.game = game;
            this.gameWorld = new World(new Vector2(0, 9.82f));
            layers = new List<Layer>();
            //seperated = false;
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
            player = new Robot(game.Content, new Vector2(TitleSafe.Width / 2, 0)); player.Size *= 0.3f;
            //budi = new Budi(game.Content, new Vector2(TitleSafe.Width / 2, 200)); budi.Size *= 0.3f;
            //budBudi = new BudBudi(game.Content, new Vector2(TitleSafe.Width / 2, 0)); budBudi.Size *= 0.3f;
            mainLayer.add(player.ActivePart);
            activePart = player.ActivePart;

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
            if (!player.IsSeperated) { Offset = player.ActivePart.Position.X; }
            //if (seperated) { Offset = bud.Position.X; }

            player.Update(gameTime);
            mainLayer.Update(gameTime);

            gameWorld.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
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
