using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class WalkingState : AnimatedState
    {
        private const int START_WALKING = 5;
        private const int STOP_WALKING = 24;

        private float currentTextureIndex;

        public WalkingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public override void Update(GameTime gameTime)
        {
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
