using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;

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
        private BudiState CurrentBudiState;
        private RobotPart bud;

        public Budi(ContentManager content, Vector2 pos, Robot robot, World world, PhysicObject physics)
            : base(content, pos, robot, world, physics)
        {
            for (int i = ANIMATION_BEGIN; i <= ANIMATION_END; i++)
            {
                TextureList.Add(content.Load<Texture2D>("Sprites\\Buddies\\Budi\\0" + String.Format("{0:000}", i)));
            }
            
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
            if (texNr > TextureList.Count + ANIMATION_BEGIN) { texNr = TextureList.Count + ANIMATION_BEGIN; }
            if (texNr < ANIMATION_BEGIN) { texNr = ANIMATION_BEGIN; }
            Texture = TextureList[(int)(texNr - ANIMATION_BEGIN)];
            Physics.Texture = TextureList[(int)(texNr - ANIMATION_BEGIN)];

            if (CurrentState != State.Waiting)
            {
                
            }

            if (CurrentBudiState == BudiState.Climbing)
            {
                // walk animation
            }
        }

    }
}
