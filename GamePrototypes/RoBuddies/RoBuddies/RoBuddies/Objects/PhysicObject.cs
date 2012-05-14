using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    class PhysicObject : GameObject
    {
        private Body body;
        private World world;

        public PhysicObject(Texture2D tex, Vector2 pos, World world)
        : base(tex, pos)
        {
            this.world = world;
            body = BodyFactory.CreateBody(this.world, pos);
            body.BodyType = BodyType.Static;
        }

        public Body Body
        {
            get { return body; }
        }

        public World World
        {
            get { return world; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.body.Position.X + Destination.X, (int)this.body.Position.Y, (int)Width, (int)Height);
            spriteBatch.Draw(Texture, dest, null, Color, Rotation, origin, effects, LayerDepth);
        }
    }
}
