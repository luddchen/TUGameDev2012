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

        public SwitchableWall(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(pos, size, color, texture, world)
        {
        }

        public void activate()
        {
            _switched = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_switched)
            {
                Position += new Vector2(0, 1);
            }
        }
    }
}
