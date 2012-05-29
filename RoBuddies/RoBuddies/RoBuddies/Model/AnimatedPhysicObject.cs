using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;

using RoBuddies.Control;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing animated objects with physical behavior
    /// </summary>
    class AnimatedPhysicObject : PhysicObject
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

        public override Vector2 Origin
        {
            get { return new Vector2(Texture.Width / 2, Texture.Height / 2);  }
            set { }
        }


        public AnimatedPhysicObject(World world) : base(world) { }

        public override void Update(GameTime gameTime)
        {
            StateMachine.Update(gameTime);
        }
    }
}
