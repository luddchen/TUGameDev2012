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

        protected Robot robot;
        //protected GameObject budi;
        //protected GameObject Bro;
        //protected GameObject budBudi;
        protected GameObject controledObject;

        protected World gameWorld;
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected Color backgroundColor;
        protected bool seperated;

        public bool IsSeperated
        {
            get { return seperated; }
            set { seperated = value; }
        }

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
                //mainLayer.remove(budBudi);
                //mainLayer.add(bud);
                //mainLayer.add(budi);
                //controledObject = bud;
                seperated = true;
                seperationDelay = 10;
            }
            else
            {
                //mainLayer.add(budBudi);
                //controledObject = budBudi;
                //mainLayer.remove(bud);
                //mainLayer.remove(budi);
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
                robot.setPosition(offset, robot.Position.Y);
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
            robot = new Robot(game.Content, new Vector2(TitleSafe.Width / 2, 0)); robot.Size *= 0.3f;
            //budi = new Budi(game.Content, new Vector2(TitleSafe.Width / 2, 200)); budi.Size *= 0.3f;
            //budBudi = new BudBudi(game.Content, new Vector2(TitleSafe.Width / 2, 0)); budBudi.Size *= 0.3f;
            mainLayer.add(robot.ActivePart);
            controledObject = robot.ActivePart;

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
            if (!seperated) { Offset = robot.ActivePart.Position.X; }
            //if (seperated) { Offset = bud.Position.X; }
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
