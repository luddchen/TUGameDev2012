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
    class Ball : GameObject
    {
        private float radius;           // = (width, height, 0)
        private Vector2 spriteOrigin;   // sprite center
        private Rectangle dest;
        private Color color;

        public Ball(Vector2 pos, float radius, SpriteBatch batch, Texture2D texture, World world)
            : base(pos, batch, texture, world)
        {
            this.radius = radius;
            this.texture = texture;
            this.color = Color.Green;
            this.spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);

            FixtureFactory.AttachCircle(radius, 1.0f, body);
        }

        public Ball(Vector2 pos, float radius, Color color, SpriteBatch batch, Texture2D texture, World world)
            : this(pos, radius, batch, texture, world)
        {
            this.color = color;
        }

        public override void draw(Vector2 offset)
        {
            dest = new Rectangle((int)(this.body.Position.X + offset.X), (int)(this.body.Position.Y + offset.Y), (int)(radius * 2), (int)(radius * 2));
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.color, this.body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void draw(Vector2 offset, float scale)
        {
            dest = new Rectangle((int)(this.body.Position.X*scale + offset.X), (int)(this.body.Position.Y*scale + offset.Y), (int)(radius * 2 * scale), (int)(radius * 2 * scale));
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.color, 0f, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}