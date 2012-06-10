using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.StateMachines;

namespace RoBuddies.Control
{
    class AnimatedState : State
    {
        private List<Texture2D> mTextureList;

        public List<Texture2D> TextureList
        {
            get { return mTextureList; }
        }

        public AnimatedState(String name, List<Texture2D> textureList, StateMachine machine)
        {
            mTextureList = textureList;
            this.Name = name;
            this.Texture = mTextureList[0];
            this.StateMachine = machine;
        }

    }
}
