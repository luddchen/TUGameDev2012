using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{

    /**
     * Simple wall for the game environment
     * 
     * Author: Thomas
     */
    class Wall : PhysicObject
    {
        private Color color;
        private Vector2 size;

        public Wall(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(texture, pos, world)
        {
            this.size = size;
            this.color = color;
            FixtureFactory.AttachRectangle(size.X, size.Y, 1, new Vector2(this.size.X / 2, this.size.Y / 2), this.Body);
            this.Body.BodyType = BodyType.Static;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, this.color);
        }
    }
}
