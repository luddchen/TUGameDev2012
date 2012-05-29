using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing animated objects with physical behavior
    /// </summary>
    class AnimatedStaticObject : StaticObject
    {
        public StateMachine StateMachine { get; set; }

        public override Texture2D Texture
        {
            get
            {
                return StateMachine.CurrentState.Texture;   // get the texture of current state
            }
            set
            {
                // all textures have to set within the states 
            }
        }

        public override void Update(GameTime gameTime)
        {
            StateMachine.Update(gameTime);
        }
    }
}
