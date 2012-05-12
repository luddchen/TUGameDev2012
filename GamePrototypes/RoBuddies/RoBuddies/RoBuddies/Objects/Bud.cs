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
        public enum State { Waiting, Walking, StartWalking, StopWalking };
        public State state;
        int walkDirection;
        public int WalkDirection
        {
            set
            {
                if (value > 0) { walkDirection = 1; effects = SpriteEffects.None; }
                if (value < 0) { walkDirection = -1; effects = SpriteEffects.FlipHorizontally; }
            }
            get { return walkDirection; }
        }

        public void setState(State state)
        {
            if (state == State.StartWalking)
            {
                texNr = 1;
                this.state = state;
            }
            if (state == State.StopWalking)
            {
                this.state = state;
            }
        }

        public Bud(ContentManager content, Vector2 pos)
        {
            textureList = new List<Texture2D>();
            for (int i = 1; i < 10; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Bud_00" + i));
            }
            for (int i = 10; i < 31; i++)
            {
                textureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Bud_0" + i));
            }

            Position = pos;
            Size = new Vector2(textureList[0].Width, textureList[0].Height);
            Rotation = 0;
            effects = SpriteEffects.None;
            Origin = new Vector2(Size.X / 2, Size.Y);
            Color = Color.White;
            texture = textureList[0];
            state = State.Waiting;
            walkDirection = 1;
        }

        public override void Update(GameTime gameTime)
        {
            texture = textureList[(int)(texNr)];

            if (state != State.Waiting)
            {
                texNr += 0.5f;
                setPosition(Position.X + walkDirection * 2, Position.Y);
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
