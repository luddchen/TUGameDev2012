using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Robuddies.Objects
{
    class BackgroundLayer : Layer
    {

        public BackgroundLayer() : base()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle();
            foreach (GameObject obj in objects)
            {
                float drawSize = ((float)(TitleSafe.Height)) * obj.Size;
                dest.X = (int)(TitleSafe.Width * obj.Position.X); dest.Y = (int)(TitleSafe.Height - TitleSafe.Height * obj.Position.Y);
                dest.Width = (int)(drawSize * ((float)obj.Texture.Width) / ((float)obj.Texture.Height)); dest.Height = (int)(drawSize);
                spriteBatch.Draw(obj.Texture, dest, null, obj.Color, obj.Rotation, obj.origin, obj.effects, 0.99f);
            }

        }

    }
}
