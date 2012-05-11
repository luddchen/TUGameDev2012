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

        float offset;
        public float Offset
        {
            set
            {
                offset = value;
                foreach (Layer l in layers) { l.Offset = offset; }
                float budiesOffset = offset / mainLayer.LayerDepth;
                Bud.setPosition(budiesOffset, Bud.Position.Y);
                Budi.setPosition(budiesOffset, Budi.Position.Y);
                Bro.setPosition(budiesOffset, Bro.Position.Y);
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

            foreach (Layer l in layers)
            {
                l.Draw(gameTime);
            }

        }

    }
}
