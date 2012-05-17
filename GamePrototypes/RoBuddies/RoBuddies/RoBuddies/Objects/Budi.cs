using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Robuddies.Objects
{
    class Budi : RobotPart
    {
        private const int ANIMATION_END = 1;
        float texNr = 0;

        private RobotPart bud;

        public Budi(ContentManager content, Vector2 pos)
            : base(content, pos)
        {
            TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Budi\\Budi_001"));

            Texture = TextureList[0];
            DirectionX = 1;
        }

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

        public RobotPart Bud
        {
            set { bud = value; }
        }

        public override void Update(GameTime gameTime)
        {
            if (texNr > TextureList.Count - 1) { texNr = TextureList.Count - 1; }
            if (texNr < 0) { texNr = 0; }
            //Console.Out.WriteLine(texNr);
            Texture = TextureList[(int)(texNr)];

            if (CurrentState != State.Waiting)
            {
                setPosition(Position.X + DirectionX * 2, Position.Y);
                bud.Destination.Offset((int)-DirectionX * 2, 0);
            }

            if (CurrentState == State.Walking)
            {
                // walk animation
            }

            if (CurrentState == State.StartWalking)
            {
                CurrentState = State.Walking;
            }

            if (CurrentState == State.StopWalking)
            {
                CurrentState = State.Waiting;
            }

            if (CurrentState == State.StartJumping)
            {
                CurrentState = State.Jumping;
            }

            if (CurrentState == State.Jumping)
            {
                CurrentState = State.StopJumping;
            }

            if (CurrentState == State.StopJumping)
            {
                    CurrentState = State.Waiting;
            }
        }

    }
}
