using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Robuddies.Interfaces;

namespace Robuddies.Objects
{
    class SwitchableWall : Wall, ISwitchable
    {
        private bool _switched;
        public bool visible;

        public SwitchableWall(Vector2 pos, Vector2 size, bool vis, Color color, Texture2D texture, World world)
            : base(pos, size, color, texture, world)
        {
            this.visible = vis;
        }

        public void activate()
        {

            if (visible)
            {
                //_switched = false;
                _switched = true;
            }
            else
            {
                visible = true;
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_switched)
            {
                Position += new Vector2(0, 1);
                //this.Color = Color.Brown;
            }
        }
    }
}
