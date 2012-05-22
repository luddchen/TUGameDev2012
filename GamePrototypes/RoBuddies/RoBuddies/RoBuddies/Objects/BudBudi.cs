using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    class BudBudi : RobotPart
    {
        private const int ANIMATION_END = 70;
        private KeyboardState oldState;
        private float texNr = 0;
        private float speedTemp;

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

        public BudBudi(ContentManager content, Vector2 pos, Robot robot, World world, PhysicObject physics)
            : base(content, pos, robot, world, physics)
        {
            for (int i = 1; i <= ANIMATION_END; i++)
            {
                TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\BudBudi\\" + String.Format("{0:0000}", i)));
            }

            Physics.Body.Mass = 100;
            Texture = TextureList[0];
            DirectionX = 0;
            speedTemp = 0;
        }

        public override void Update(GameTime gameTime)
        {
            GetInput();

            if (texNr > TextureList.Count - 1) { texNr = TextureList.Count - 1; }
            if (texNr < 0) { texNr = 0; }
            Physics.Texture = TextureList[(int)(texNr)];

            if (CurrentState != State.Waiting)
            {
                texNr += 0.2f;
                if (texNr > TextureList.Count) { texNr = TextureList.Count - 1; }
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
                texNr = 0;
                CurrentState = State.Waiting;
                DirectionX = 0.0f;
            }

            if (CurrentState == State.StartJumping)
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
                    CurrentState = State.Jumping;
                    DirectionX = speedTemp;
                }
            }

            if (CurrentState == State.Jumping)
            {
                texNr = 39;
                DirectionY -= 0.1f;
                CurrentState = State.StopJumping;
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
                if (IsOnGround)
                {
                    Physics.Body.LinearVelocity = new Vector2(-MovementForce, Physics.Body.LinearVelocity.Y);
                }
                else
                {
                    Physics.Body.ApplyForce(new Vector2(-MovementForce / 2, 0));
                }
                if (this.CurrentState == RobotPart.State.Waiting)
                {
                    this.CurrentState = RobotPart.State.StartWalking;
                    this.DirectionX = -1;
                }
            }

            if (currentState.IsKeyDown(Keys.Right))
            {
                if (IsOnGround)
                {
                    Physics.Body.LinearVelocity = new Vector2(MovementForce, Physics.Body.LinearVelocity.Y);
                }
                else
                {
                    Physics.Body.ApplyForce(new Vector2(MovementForce / 2, 0));
                }
                if (this.CurrentState == RobotPart.State.Waiting)
                {
                    this.CurrentState = RobotPart.State.StartWalking;
                    this.DirectionX = 1;
                }
            }

            if (!currentState.IsKeyDown(Keys.Right) && this.DirectionX == 1)
            {
                Physics.Body.LinearVelocity = new Vector2(0, Physics.Body.LinearVelocity.Y);
                this.CurrentState = RobotPart.State.StopWalking;
            }

            if (!currentState.IsKeyDown(Keys.Left) && this.DirectionX == -1)
            {
                Physics.Body.LinearVelocity = new Vector2(0, Physics.Body.LinearVelocity.Y);
                this.CurrentState = RobotPart.State.StopWalking;
            }

            //Console.Out.WriteLine("Vel: " + Physics.Body.LinearVelocity.X);
            if (currentState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                if ((this.CurrentState != RobotPart.State.Jumping) &&
                    (this.CurrentState != RobotPart.State.StartJumping) &&
                    (this.CurrentState != RobotPart.State.StopJumping) &&
                    IsOnGround )
                {
                    Physics.Body.ApplyForce(new Vector2(0, -20 * MovementForce));
                    this.CurrentState = RobotPart.State.StartJumping;
                    if (this.IsSeperated) { this.DirectionY = 3.5f; }
                    if (!this.IsSeperated) { this.DirectionY = 2.5f; }
                }
            }

            if (currentState.IsKeyUp(Keys.Space) && oldState.IsKeyDown(Keys.Space))
            {
                Physics.Body.ApplyForce(new Vector2(0, 20 * MovementForce));
                CurrentState = State.StopJumping;
            }

            if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                OnActivate(EventArgs.Empty);
            }

            oldState = currentState;
        }
    }
}
