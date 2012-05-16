using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Robuddies.Objects
{

    class Layer
    {
        #region Fields and Properties

        protected List<GameObject> objects;
        protected List<AnimatedObject> animatedObjects;

        private float offset;
        private float scrollSpeed;
        private float layerDepth;
        private Rectangle titleSafe;

        public void add(GameObject obj) 
        {
            objects.Add(obj);
            if (obj is AnimatedObject) animatedObjects.Add((AnimatedObject)obj);
        }
        public void remove(GameObject obj) 
        { 
            objects.Remove(obj);
            if (obj is AnimatedObject) animatedObjects.Remove((AnimatedObject)obj);
        }

        /// <summary>
        /// Depth or Z-Coord of this Layer 
        /// float  in [0.0 .. 1.0]
        /// 1.0 == far away
        /// 0.5 == mainLayer where bud, budi and bro live
        /// 0.0 == in front of mainLayer
        /// </summary>
        public float Depth
        {
            get { return layerDepth; }
            set { 
                layerDepth = value;
                scrollSpeed = (float)( 256.0d / Math.Pow(value*32.0d, 2.0d) );
                if (value >= 0.9f) { scrollSpeed = (float)(Math.Pow(scrollSpeed,4d) * 16d); }
            }
        }

        public List<GameObject> Objects
        {
            get { return objects; }
        }

        public float Offset
        {
            get { return offset; }
            set { offset = value * scrollSpeed; }
        }

        public Rectangle TitleSafe
        {
            get { return titleSafe; }
            set { titleSafe = value; }
        }

        #endregion

        #region Construction and Initialization

        public Layer()
        {
            objects = new List<GameObject>();
            animatedObjects = new List<AnimatedObject>();
        }

        public void LoadContent(){}

        public void UnloadContent() { objects.Clear();  }

        #endregion

        #region Update

        public void Update(GameTime gameTime) 
        {
            foreach (AnimatedObject obj in animatedObjects) 
            { 
                obj.Update(gameTime); 
            }
        }

        #endregion

        #region Rendering

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in objects)
            {
                float xPos = (float)titleSafe.Width / 2.0f + obj.Position.X - offset;
                obj.Destination.X = (int)(xPos);
                obj.Destination.Y = (int)((float)titleSafe.Height - (obj.Position.Y + 20));
                obj.Destination.Width = (int)obj.Width;
                obj.Destination.Height = (int)obj.Height;
                obj.LayerDepth = layerDepth;
                obj.Draw(spriteBatch);
            }

        }

        #endregion
    }
}