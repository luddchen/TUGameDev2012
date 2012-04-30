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

namespace Fall_Ball.Objects
{
    class Egg : GameObject
    {
        private float radiusX;
        private float radiusY;
        private Vector2 spriteOrigin;   // sprite center
        private Rectangle dest;

        public Egg(Vector2 pos, float radiusX, float radiusY, SpriteBatch batch, Texture2D texture, World world)
            : base(pos, batch, texture, world)
        {
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.texture = texture;
            this.color = Color.Green;
            this.spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);

            if (Settings.MaxPolygonVertices < 24) Settings.MaxPolygonVertices = 24;
            FixtureFactory.AttachEllipse(radiusX, radiusY, 24, 1.0f, body);
            this.width = this.height = max( radiusX , radiusY) *2;
        }

        public Egg(Vector2 pos, float radiusX, float radiusY, Color color, SpriteBatch batch, Texture2D texture, World world)
            : this(pos, radiusX, radiusY, batch, texture, world)
        {
            this.color = color;
        }

        public override void draw(Vector2 offset, float scale)
        {
            int drawSizeX = (int)(radiusX * 2 * scale);
            if (drawSizeX < 1) drawSizeX = 1;
            int drawSizeY = (int)(radiusY * 2 * scale);
            if (drawSizeY < 1) drawSizeY = 1;

            dest = new Rectangle((int)(this.body.Position.X * scale + offset.X), (int)(this.body.Position.Y * scale + offset.Y), drawSizeX, drawSizeY);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.color, this.body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void drawMap(Vector2 offset, float scale)
        {
            draw(offset, scale);
        }

    }
}