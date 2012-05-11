using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{

    class Layer
    {
        #region Fields and Properties

        public List<GameObject> objects;
        public void add(GameObject obj) { objects.Add(obj); }
        public void remove(GameObject obj) { objects.Remove(obj); }

        Game game;

        float layerDepth;
        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value; }
        }

        float offset;
        public float Offset
        {
            get { return offset; }
            set { offset = value/layerDepth; }
        }

        private SpriteBatch spriteBatch;

        Rectangle titleSafe;
        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set { titleSafe = value; }
        }

        #endregion

        #region Construction and Initialization

        public Layer(Game game)
        {
            objects = new List<GameObject>();
            this.game = game;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }


        public void UnloadContent() { objects.Clear();  }

        #endregion

        #region Update

        public void Update(GameTime gameTime) { }

        #endregion

        #region Rendering

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Rectangle dest = new Rectangle();
            foreach (GameObject obj in objects)
            {
                dest.X = (int)(titleSafe.Width/2 + obj.Position.X - offset); dest.Y = (int)(titleSafe.Height - (obj.Position.Y + 20));
                dest.Width = (int)obj.Width; dest.Height = (int)obj.Height;
                spriteBatch.Draw(obj.Texture, dest, null, obj.Color, obj.Rotation, obj.Origin, SpriteEffects.None, layerDepth);
                //Console.Out.WriteLine("X="+dest.X+" Y="+dest.Y+" width="+dest.Width+" height="+dest.Height);
            }
            spriteBatch.End();
        }

        #endregion
    }
}