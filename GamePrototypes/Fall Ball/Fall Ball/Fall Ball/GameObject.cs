using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace Fall_Ball
{

    // base class for all objects in game
    // need to be extended for circles , squares ...
    // Vector3 for compatibility with Boundingbox
    class GameObject
    {

        // internal for loading and drawing sprites
        public SpriteBatch spriteBatch;
        public Texture2D texture;
        public Body body;
        public Color color;
        public float width;
        public float height;

        // constructor
        public GameObject(Vector2 pos, SpriteBatch batch, Texture2D texture, World world)
        {
            body = BodyFactory.CreateBody(world, pos);
            body.BodyType = BodyType.Static;
            this.spriteBatch = batch;
            this.texture = texture;
            this.color = Color.Gray;
            this.width = 0;
            this.height = 0;
        }

        // draw the object on position + offset , using some scaling
        public virtual void draw(Vector2 offset, float size) { }


        // draw the object simple for map
        public virtual void drawMap(Vector2 offset, float size) { }

        public float min(float a, float b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public float max(float a, float b)
        {
            if (a < b)
            {
                return b;
            }
            return a;
        }
    }
}