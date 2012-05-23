using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Utilities;

namespace Robuddies.Objects
{

    public class Layer
    {
        #region Fields and Properties

        protected Vector2 parallax;
        protected List<GameObject> objects;
        //protected List<AnimatedObject> animatedObjects;

        //private float offset;
        //private float scrollSpeed;
        //private float layerDepth;
        //private Rectangle titleSafe;
        protected Camera camera;

        public void add(GameObject obj) 
        {
            objects.Add(obj);
            //if (obj is AnimatedObject) animatedObjects.Add((AnimatedObject)obj);
        }
        public void remove(GameObject obj) 
        { 
            objects.Remove(obj);
            //if (obj is AnimatedObject) animatedObjects.Remove((AnimatedObject)obj);
        }

        /// <summary>
        /// Depth or Z-Coord of this Layer 
        /// float  in [0.0 .. 1.0]
        /// 1.0 == far away
        /// 0.5 == mainLayer where bud, budi and bro live
        /// 0.0 == in front of mainLayer
        /// </summary>
        public float Depth { get; set; }
            //set
            //{
            //    layerDepth = value;
            //    scrollSpeed = (float)(256.0d / Math.Pow(value * 32.0d, 2.0d));
            //    if (value >= 0.9f) { scrollSpeed = (float)(Math.Pow(scrollSpeed, 4d) * 16d); }
            //}
        //}

        public Vector2 Parallax { get { return parallax; } set { parallax = value; } }
        public List<GameObject> Objects { get { return objects; } set { objects = value;} }

        //public float Offset
        //{
        //    get { return offset; }
        //    set { offset = value * scrollSpeed; }
        //}

        //public Rectangle TitleSafe
        //{
        //    get { return titleSafe; }
        //    set { titleSafe = value; }
        //}

        #endregion

        #region Construction and Initialization

        public Layer(Camera camera, Vector2 parallax)
        {
            this.camera = camera;
            this.parallax = parallax;
            objects = new List<GameObject>();
        }

        public void LoadContent(){}

        public void UnloadContent() { objects.Clear();  }

        #endregion

        #region Update

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, camera.getViewMatrix(parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(camera.getViewMatrix(parallax)));
        }

        public void Update(GameTime gameTime) 
        {
            foreach (GameObject obj in objects) 
            { 
                obj.Update(gameTime); 
            }
        }

        #endregion

        #region Rendering

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getViewMatrix(parallax));

            foreach (GameObject obj in objects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        #endregion
    }
}