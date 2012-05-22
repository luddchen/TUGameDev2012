using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Utilities;

namespace Robuddies.Objects
{
    class BackgroundLayer : Layer
    {

        public BackgroundLayer(Camera camera, Vector2 parallax)
            : base(camera, parallax)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.getViewMatrix(parallax));

            //Rectangle dest = new Rectangle();
            //foreach (GameObject obj in objects)
            //{
            //    //float drawSize = ((float)(TitleSafe.Height)) * obj.Scale;
            //    //dest.X = (int)(TitleSafe.Width * obj.Position.X); dest.Y = (int)(TitleSafe.Height - TitleSafe.Height * obj.Position.Y);
            //    //dest.Width = (int)(drawSize * ((float)obj.Texture.Width) / ((float)obj.Texture.Height)); dest.Height = (int)(drawSize);
            //    spriteBatch.Draw(obj.Texture, dest, null, obj.Color, obj.Rotation, obj.origin, obj.effects, 0.99f);
            //}

            //spriteBatch.End();
        }

    }
}
