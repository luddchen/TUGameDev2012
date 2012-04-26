using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Fall_Ball
{
    class Square : GameObject
    {
        private Vector3 size;           // = (width, height, 0)
        private Texture2D texture;
        private Vector2 spriteOrigin;   // sprite center
        Rectangle dest;                 


        public Square(Vector3 pos, Vector3 size, SpriteBatch batch, ContentManager content) : base( pos, batch , content)
        {
            this.size = size;
            this.box.Min -= size / 2;
            this.box.Max += size / 2;
            this.texture = content.Load<Texture2D>("Sprites\\Square");
            spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
        }


        public override void draw( Vector3 offset )
        {
            dest = new Rectangle((int)(position.X + offset.X), (int)(position.Y + offset.Y), (int)size.X, (int)size.Y);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, Color.Red, 0f, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


        // here we need something for exact collision detection and computation of object normal
        // public virtual ??? testCollisionAndReturnNormal( ?? ?? ??) { }

    }
}
