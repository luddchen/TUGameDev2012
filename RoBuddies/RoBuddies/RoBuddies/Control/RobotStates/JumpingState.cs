using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
﻿
namespace RoBuddies.Control.RobotStates
{
    class JumpingState : AnimatedState
    {
        private const int START_JUMPING = 29;
        private const int STOP_JUMPING = 38;

        private float currentTextureIndex;

        public JumpingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public override void Enter()
        {
            (StateMachine.Body as Body).ApplyForce(new Vector2(0, 1500));
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTextureIndex < START_JUMPING)
            {
                currentTextureIndex = START_JUMPING;
            }

            if (currentTextureIndex > STOP_JUMPING)
            {
                currentTextureIndex = START_JUMPING;
            }

            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
            currentTextureIndex += 0.6f;
        }
    }
}
