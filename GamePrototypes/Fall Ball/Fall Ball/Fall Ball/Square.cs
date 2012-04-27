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
    class Square : GameObject
    {
        private Vector2 size;           // = (width, height, 0)
        private Vector2 spriteOrigin;   // sprite center
        private Rectangle dest;
        private float rot;

        public Square(Vector2 pos, Vector2 size, float rot, SpriteBatch batch, Texture2D texture, World world)
            : base(pos, batch, texture, world)
        {
            this.size = size;
            this.rot = rot;
            this.texture = texture;
            this.spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);

            Matrix rMat = Matrix.CreateRotationZ(rot);

            Vector2 leftTop = Vector2.Transform(new Vector2(-size.X / 2, -size.Y / 2), rMat);
            Vector2 rightTop = Vector2.Transform(new Vector2(size.X / 2, -size.Y / 2), rMat);
            Vector2 leftBottom = Vector2.Transform(new Vector2(-size.X / 2, size.Y / 2), rMat);
            Vector2 rightBottom = Vector2.Transform(new Vector2(size.X / 2, size.Y / 2), rMat);
            FixtureFactory.AttachEdge(leftTop, rightTop, body);
            FixtureFactory.AttachEdge(rightTop, rightBottom, body);
            FixtureFactory.AttachEdge(rightBottom, leftBottom, body);
            FixtureFactory.AttachEdge(leftBottom, leftTop, body);
        }


        public override void draw(Vector3 offset)
        {
            dest = new Rectangle((int)(this.body.Position.X + offset.X), (int)(this.body.Position.Y + offset.Y), (int)size.X, (int)size.Y);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, Color.Gray, rot, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}