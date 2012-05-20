using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;

namespace Robuddies.Objects
{
    class Bud : RobotPart
    {
        private const int ANIMATION_END = 40;
        private KeyboardState oldState;
        private float texNr = 0;
        private float speedTemp;

        private RobotPart budi;

        public Bud(ContentManager content, Vector2 pos, World world, PhysicObject physics)
            : base(content, pos, world, physics)
        {
            for (int i = 1; i <= ANIMATION_END; i++)
            {
                TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Bud\\Bud_" + String.Format("{0:000}", i)));
            }
            Texture = TextureList[0];
            DirectionX = 0;
            speedTemp = 0;
        }

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

        public RobotPart Budi
        {
            set { budi = value; }
        }

        public override void Update(GameTime gameTime)
        {
            GetInput();

            if (texNr > TextureList.Count - 1) { texNr = TextureList.Count - 1; }
            if (texNr < 0) { texNr = 0; }
            //Console.Out.WriteLine(texNr);
            Texture = TextureList[(int)(texNr)];
            Physics.Texture = TextureList[(int)(texNr)];

            if (CurrentState != State.Waiting)
            {
                texNr += 0.5f;
                if (texNr > TextureList.Count) { texNr = TextureList.Count-1; }
                setPosition(Position.X + DirectionX * 2, Position.Y);
                //budi.Destination.Offset( (int)- DirectionX * 2, 0);
            }

            if (CurrentState == State.Walking)
            {
                if (texNr > 24) texNr = 4;
            }

            if (CurrentState == State.StartWalking)
            {
                if (texNr > 3) CurrentState = State.Walking;
            }

            if (CurrentState == State.StopWalking)
            {
                if (texNr > 28)
                {
                    texNr = 0;
                    CurrentState = State.Waiting;
                    DirectionX = 0.0f;
                }
            }

            if (CurrentState == State.StartJumping)
            {
                if (texNr < 4) { 
                    texNr = 30; 
                }
                if (texNr == 30)
                {
                    speedTemp = DirectionX;
                    DirectionX = 0.0f;
                }
                if (texNr >= 39)
                {
                    CurrentState = State.Jumping;
                    DirectionX = speedTemp;
                }
            }

            if (CurrentState == State.Jumping)
            {
                texNr = 39;
                setPosition(Position.X, Position.Y + DirectionY * 2);
                DirectionY -= 0.1f;
                if (Position.Y <= 0)
                {
                    CurrentState = State.StopJumping;
                }
            }

            if (CurrentState == State.StopJumping)
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
                    CurrentState = State.Waiting;
                }
            }
        }

        private void GetInput()
        {
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Left))
            {
                if (this.CurrentState == RobotPart.State.Waiting)
                {
                    this.CurrentState = RobotPart.State.StartWalking;
                    this.DirectionX = -1; ;
                }
            }

            if (currentState.IsKeyDown(Keys.Right))
            {
                if (this.CurrentState == RobotPart.State.Waiting)
                {
                    this.CurrentState = RobotPart.State.StartWalking;
                    this.DirectionX = 1;
                }
            }

            if (!currentState.IsKeyDown(Keys.Right) && this.DirectionX == 1)
            {
                if (this.CurrentState == RobotPart.State.Walking)
                {
                    this.CurrentState = RobotPart.State.StopWalking;
                }
            }

            if (!currentState.IsKeyDown(Keys.Left) && this.DirectionX == -1)
            {
                if (this.CurrentState == RobotPart.State.Walking)
                {
                    this.CurrentState = RobotPart.State.StopWalking;
                }
            }

            if (currentState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                if ((this.CurrentState != RobotPart.State.Jumping) &&
                    (this.CurrentState != RobotPart.State.StartJumping) &&
                    (this.CurrentState != RobotPart.State.StopJumping))
                {
                    this.CurrentState = RobotPart.State.StartJumping;
                    if (this.IsSeperated) { this.DirectionY = 3.5f; }
                    if (!this.IsSeperated) { this.DirectionY = 2.5f; }
                }
            }

            oldState = currentState;
        }
    }
}
