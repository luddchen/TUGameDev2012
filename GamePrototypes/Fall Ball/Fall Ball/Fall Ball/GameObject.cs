using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Fall_Ball
{

    // base class for all objects in game
    // need to be extended for circles , squares ...
    // Vector3 for compatibility with Boundingbox
    class GameObject
    {

        // square bounds - simple as possible
        public BoundingBox box;

        // world coords of centerpoint
        public Vector3 position;

        // internal for loading and drawing sprites
        public SpriteBatch spriteBatch;
        public ContentManager content;

        // constructor
        public GameObject(Vector3 pos, SpriteBatch batch, ContentManager content)
        {
            this.position = pos;
            this.box = new BoundingBox( new Vector3(pos.X, pos.Y, 0), new Vector3(pos.X, pos.Y, 0) );
            this.spriteBatch = batch;
            this.content = content;
        }

        // draw the object on position + offset
        public virtual void draw( Vector3 offset ) {}


        // here we need something for exact collision detection and computation of object normal
        // public virtual ??? testCollisionAndReturnNormal( ?? ?? ??) { }


        // moves the object -> for the balls ..
        public virtual void move(Vector3 offset)
        {
            this.position += offset;
            this.box.Min += offset;
            this.box.Max += offset;
        }

    }
}
