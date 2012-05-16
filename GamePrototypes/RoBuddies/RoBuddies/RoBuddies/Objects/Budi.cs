using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Robuddies.Objects
{
    class Budi : RobotPart
    {
        float texNr = 0;

        public override float DirectionX
        {
            set
            {
                if (value > 0) { directionX = 1; effects = SpriteEffects.None; }
                if (value < 0) { directionX = -1; effects = SpriteEffects.FlipHorizontally; }
            }
            get { return directionX; }
        }

        public override Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                origin.X = texture.Width / 2;
                origin.Y = texture.Height;
            }
        }

        public Budi(ContentManager content, Vector2 pos)
            : base(content, pos)
        {
            TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Budi\\Budi_001"));

            Texture = TextureList[0];
            DirectionX = 1;
        }

        public override void Update(GameTime gameTime)
        {
        }

    }
}
