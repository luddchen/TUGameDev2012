using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class WalkingState : AnimatedState
    {
        public const String LEFT_WALK_STATE = "LeftWalkingState";
        public const String RIGHT_WALK_STATE = "RightWalkingState";

        private const int START_WALKING = 5;
        private const int STOP_WALKING = 24;

        private float currentTextureIndex;
        private Body body;

        public WalkingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
            body = machine.Body as Body;
        }

        public override void Update(GameTime gameTime)
        {
            if (Name == LEFT_WALK_STATE)
            {
                body.LinearVelocity = new Vector2(-3, body.LinearVelocity.Y);
                StateMachine.Body.Effect = SpriteEffects.FlipHorizontally;
            }

            if (Name == RIGHT_WALK_STATE)
            {
                body.LinearVelocity = new Vector2(3, body.LinearVelocity.Y);
                StateMachine.Body.Effect = SpriteEffects.None;
            }

            UpdateWalkAnimation(gameTime);
        }

        private void UpdateWalkAnimation(GameTime gameTime)
        {
            if (currentTextureIndex < START_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            if (currentTextureIndex > STOP_WALKING)
            {
                currentTextureIndex = START_WALKING;
            }

            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
            currentTextureIndex += 0.6f;
        }
    }
}
