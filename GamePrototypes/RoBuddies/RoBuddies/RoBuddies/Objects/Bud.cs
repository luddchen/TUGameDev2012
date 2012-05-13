using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Robuddies.Objects
{
    class Bud : AnimatedObject
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
                origin.X = Width / 2;
                origin.Y = Height;
            }
        }

        public Bud(ContentManager content, Vector2 pos)
        {
            textureList = new List<Texture2D>();
            for (int i = 1; i < 10; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Bud_00" + i));
            }
            for (int i = 10; i < 41; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Bud_0" + i));
            }

            Position = pos;
            Size = 1;
            Rotation = 0;
            effects = SpriteEffects.None;
            origin = new Vector2();
            Color = Color.White;
            Texture = textureList[0];
            state = State.Waiting;
            directionX = 1;
        }

        public override void Update(GameTime gameTime)
        {
            texture = textureList[(int)(texNr)];

            if (state != State.Waiting)
            {
                texNr += 0.5f;
                if (texNr > textureList.Count) { texNr = 0; }
                setPosition(Position.X + directionX * 2, Position.Y);
            }

            if (state == State.Walking)
            {
                if (texNr > 24) texNr = 4;
            }

            if (state == State.StartWalking)
            {
                if (texNr > 3) state = State.Walking;
            }

            if (state == State.StopWalking)
            {
                if (texNr > 28)
                {
                    texNr = 0;
                    state = State.Waiting;
                }
            }
        }

    }
}
