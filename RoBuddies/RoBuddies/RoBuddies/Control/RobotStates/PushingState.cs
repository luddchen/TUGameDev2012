using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control.RobotStates
{
    class PushingState : AnimatedState
    {

        // for lower part i dont know the numbers ;)
        private const int START_PUSHING = 44;
        private const int STOP_PUSHING = 54;
        private const int WAIT_PUSHING = 43;
        private const int TO_PUSHING = 39;
        private float currentTextureIndex;

        public bool IsMoving { get; set; }

        public bool walkBackwards { get; set; }

        public PushingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public void ToWaiting(WaitingState state)
        {
            StateMachine.CurrentState = state;
        }

        public void ToPulling(PullingState state)
        {
            StateMachine.CurrentState = state;
        }

        public override void Update(GameTime gameTime)
        {
            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];

            UpdateClimbAnimation(gameTime);
        }

        private void UpdateClimbAnimation(GameTime gameTime)
        {
            if (currentTextureIndex < TO_PUSHING)
            {
                currentTextureIndex = TO_PUSHING;
            }

            if (!IsMoving)
            {
                if (currentTextureIndex < WAIT_PUSHING)
                {
                    currentTextureIndex += 0.5f;
                }
                if (currentTextureIndex > WAIT_PUSHING)
                {
                    currentTextureIndex = WAIT_PUSHING;
                }
            }
            else
            {

                if (currentTextureIndex > STOP_PUSHING)
                {
                    currentTextureIndex = START_PUSHING;
                }

                if (currentTextureIndex < START_PUSHING)
                {
                    currentTextureIndex = STOP_PUSHING;
                }

                if (walkBackwards)
                {
                    currentTextureIndex -= 0.3f;
                }
                else
                {
                    currentTextureIndex += 0.3f;
                }
            }


            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];

            IsMoving = false;
        }

        public override void Enter()
        {
            base.Enter();
            currentTextureIndex = TO_PUSHING;
        }
    }
}
