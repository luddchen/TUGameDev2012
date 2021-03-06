﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Input;

namespace Robuddies.Objects
{
    class Budi : RobotPart
    {
        public enum BudiState
        {
            Seperation,
            Unseperation,
            StartClimbing,
            Climbing,
            StopClimbing,
            UseLadder,
            UseLever
        };

        private const int ANIMATION_BEGIN = 80;
        private const int ANIMATION_END = 130;
        private float texNr = ANIMATION_BEGIN;
        private BudiState currentBudiState;
        private RobotPart bud;
        private KeyboardState oldState;

        public Budi(ContentManager content, Vector2 pos, Robot robot, World world, PhysicObject physics)
            : base(content, pos, robot, world, physics)
        {
            for (int i = ANIMATION_BEGIN; i <= ANIMATION_END; i++)
            {
                TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Budi\\" + String.Format("{0:0000}", i)));
            }
            
            Texture = TextureList[0];
            DirectionX = 1;
        }

        public BudiState CurrentBudiState
        {
            get { return currentBudiState; }
            set { currentBudiState = value; }
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
            if (Robot.ActivePart == this)
            {
                GetInput();
            }

            if (texNr > TextureList.Count + ANIMATION_BEGIN - 1) { texNr = TextureList.Count + ANIMATION_BEGIN - 1; }
            if (texNr < ANIMATION_BEGIN) { texNr = ANIMATION_BEGIN; }
            Texture = TextureList[(int)(texNr - ANIMATION_BEGIN)];
            Physics.Texture = TextureList[(int)(texNr - ANIMATION_BEGIN)];

            if (CurrentBudiState == BudiState.StopClimbing)
            {
                texNr = 100;
            }

            if (CurrentBudiState == BudiState.StartClimbing)
            {
                this.Physics.Body.LinearVelocity = new Vector2(this.Physics.Body.LinearVelocity.X, -1);
                if (texNr < 90 || texNr >= 100) texNr = 90;
            }

            if (CurrentBudiState == BudiState.Climbing)
            {
                this.Physics.Body.LinearVelocity = new Vector2(this.Physics.Body.LinearVelocity.X, -1);
            }

            if (CurrentBudiState == BudiState.UseLever)
            {
                texNr += 0.3f;
                if (texNr > 116.0f && texNr < 116.5f) { OnActivate(EventArgs.Empty); }
                if (texNr >= 120) { CurrentBudiState = BudiState.StopClimbing; }
            }
        }

        private void GetInput()
        {
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Left))
            {
                if (CurrentBudiState == BudiState.StartClimbing
                    || CurrentBudiState == BudiState.Climbing)
                {
                    Physics.Body.LinearVelocity = new Vector2(-MovementForce, Physics.Body.LinearVelocity.Y); texNr += 0.4f;
                    if (texNr < 90 || texNr >= 100) texNr = 90;
                }
                this.DirectionX = -1;
            }

            if (currentState.IsKeyDown(Keys.Right))
            {
                if (CurrentBudiState == BudiState.StartClimbing
                    || CurrentBudiState == BudiState.Climbing)
                {
                    Physics.Body.LinearVelocity = new Vector2(MovementForce, Physics.Body.LinearVelocity.Y); texNr += 0.4f;
                    if (texNr < 90 || texNr >= 100) texNr = 90;
                }
                this.DirectionX = 1;
            }

            if (!currentState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right) && this.DirectionX == 1)
            {
                Physics.Body.LinearVelocity = new Vector2(0, Physics.Body.LinearVelocity.Y);
                //this.CurrentState = RobotPart.State.StopWalking;
                texNr = 90;
            }

            if (!currentState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left) && this.DirectionX == -1)
            {
                Physics.Body.LinearVelocity = new Vector2(0, Physics.Body.LinearVelocity.Y);
                //this.CurrentState = RobotPart.State.StopWalking;
                texNr = 90;
            }
            if (currentState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                if (CurrentBudiState == BudiState.StartClimbing
                    || CurrentBudiState == BudiState.Climbing)
                {
                    this.CurrentBudiState = BudiState.StopClimbing;
                }
            }

            if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                if (texNr < 110 || texNr > 120) { texNr = 110; }
                this.CurrentBudiState = BudiState.UseLever;
                //OnActivate(EventArgs.Empty);
            }

            oldState = currentState;
        }

    }
}
