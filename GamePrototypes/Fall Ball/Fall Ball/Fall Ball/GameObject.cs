﻿using System;
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

        // constructor
        public GameObject(Vector2 pos, SpriteBatch batch, Texture2D texture, World world)
        {
            body = BodyFactory.CreateBody(world, pos);
            body.BodyType = BodyType.Static;
            this.spriteBatch = batch;
            this.texture = texture;
        }

        // draw the object on position + offset
        public virtual void draw(Vector3 offset) { }

    }
}