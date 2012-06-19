using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace RoBuddies.Control.RobotStates
{
    class LadderClimbingState : AnimatedState
    {

        // this texture numbers works only for upper part ... for combined climbing use 81 up to 90 but it is possible that this textures are heigher than walking textures
        private const int START_CLIMBING = 20; // 100 - 80
        private const int STOP_CLIMBING = 30;  // 110 - 80
        private float currentTextureIndex;

        public bool IsMoving { get; set; }

        public LadderClimbingState(String name, List<Texture2D> textureList, StateMachine machine)
            : base(name, textureList, machine)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
            {
                currentTextureIndex = START_CLIMBING;
                StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
                (this.StateMachine.Body as Body).LinearVelocity = new Vector2( (this.StateMachine.Body as Body).LinearVelocity.Y, 0);
            }
        }

        public void UpdateClimbAnimation(GameTime gameTime)
        {
            IsMoving = true;

            if (currentTextureIndex < START_CLIMBING)
            {
                currentTextureIndex = START_CLIMBING;
            }

            if (currentTextureIndex > STOP_CLIMBING)
            {
                currentTextureIndex = START_CLIMBING;
            }

            StateMachine.Body.Texture = TextureList[(int)currentTextureIndex];
            currentTextureIndex += 0.7f;
        }

    }
}
