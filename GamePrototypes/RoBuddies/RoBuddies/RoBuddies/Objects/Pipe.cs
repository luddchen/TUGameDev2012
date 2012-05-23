using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    /*
     *  Pipe which the upper part can use for climbing
     *  from left to right
     * 
     *  Author: Thomas
     * 
     */
    public class Pipe : PhysicObject
    {
        private const int PIPE_HEIGHT = 2;
        private Color color;
        private int length;

        public Pipe(Vector2 pos, int length, Color color, Texture2D tex, World world)
            : base(tex, pos, world)
        {
            this.color = color;
            this.length = length;
            FixtureFactory.AttachRectangle(length, PIPE_HEIGHT, 1, new Vector2(this.length / 2, PIPE_HEIGHT / 2), this.Body);
            this.Body.BodyType = BodyType.Static;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)length, (int)PIPE_HEIGHT);
            spriteBatch.Draw(Texture, dest, this.color);
        }
    }
}
