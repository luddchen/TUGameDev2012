using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Robuddies.Objects;
using FarseerPhysics.Dynamics.Contacts;

namespace Robuddies.Levels
{
    class Level
    {
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

        public Layer MainLayer
        {
            get { return mainLayer; }
        }

        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }
                player.setPosition(offset, player.Position.Y);
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

            player = new Robot(game.Content, new Vector2(TitleSafe.Width / 2, 0), gameWorld, this); player.Size *= 0.3f;
            //budi = new Budi(game.Content, new Vector2(TitleSafe.Width / 2, 200)); budi.Size *= 0.3f;
            //budBudi = new BudBudi(game.Content, new Vector2(TitleSafe.Width / 2, 0)); budBudi.Size *= 0.3f;
            mainLayer.add(player);
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
            if (!player.IsSeperated) { Offset = player.ActivePart.Position.X; }
            if (player.IsSeperated) { Offset = player.ActivePart.Position.X; }

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

        public virtual bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            return true;
        }

        public void addToMyOnCollision(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnCollision += MyOnCollision;
            }
        }

        public void removeFromMyOnCollision(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnCollision -= MyOnCollision;
            }
        }

    }
}
