using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Robuddies.Objects
{
    class BudBudi : AnimatedObject
    {
        float texNr = 0;
        float speedTemp;

        public override float DirectionX
        {
            set
            {
                directionX = value;
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

        public BudBudi(ContentManager content, Vector2 pos)
        {
            textureList = new List<Texture2D>();
            for (int i = 1; i < 10; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\BudBudi_00" + i));
            }
            for (int i = 10; i < 41; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\BudBudi_0" + i));
            }

            Position = pos;
            Size = 1;
            Rotation = 0;
            effects = SpriteEffects.None;
            origin = new Vector2();
            Color = Color.White;
            Texture = textureList[0];
            state = State.Waiting;
            directionX = 0;
            speedTemp = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (texNr > textureList.Count - 1) { texNr = textureList.Count - 1; }
            if (texNr < 0) { texNr = 0; }
            //Console.Out.WriteLine(texNr);
            Texture = textureList[(int)(texNr)];

            if (state != State.Waiting)
            {
                texNr += 0.5f;
                if (texNr > textureList.Count) { texNr = textureList.Count - 1; }
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
                    DirectionX = 0.0f;
                }
            }

            if (state == State.StartJumping)
            {
                if (texNr < 4)
                {
                    texNr = 30;
                }
                if (texNr == 30)
                {
                    speedTemp = DirectionX;
                    DirectionX = 0.0f;
                }
                if (texNr >= 39)
                {
                    state = State.Jumping;
                    DirectionX = speedTemp;
                }
            }

            if (state == State.Jumping)
            {
                texNr = 39;
                setPosition(Position.X, Position.Y + DirectionY * 2);
                DirectionY -= 0.1f;
                if (Position.Y <= 0)
                {
                    state = State.StopJumping;
                }
            }

            if (state == State.StopJumping)
            {
                if (texNr == 39)
                {
                    speedTemp = DirectionX;
                    DirectionX = 0;
                }
                texNr -= 1;
                if (texNr <= 30)
                {
                    DirectionX = speedTemp;
                    texNr = 0;
                    state = State.Waiting;
                }
            }
        }

    }
}
