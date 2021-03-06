using System;
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using RoBuddies.Model.RobotParts;
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
            StateMachine.Game.soundBank.PlayCue("Jump");

            if (StateMachine.Body is PartsCombined)
            {
                (StateMachine.Body as Body).ApplyForce(new Vector2(0, 1800));
            }

            if (StateMachine.Body is LowerPart)
            {
                (StateMachine.Body as Body).ApplyForce(new Vector2(0, 1450));
            }
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
            //currentTextureIndex += 0.6f; // need first the new animated sprites
            switchBackToWaitingState();
        }

        private void switchBackToWaitingState() {
            if (StateMachine is PartsCombinedStateMachine)
            {
                StateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
            }

            if (StateMachine is LowerPartStateMachine)
            {
                StateMachine.SwitchToState(LowerPartStateMachine.WAIT_STATE);
            }
        }
    }
}
