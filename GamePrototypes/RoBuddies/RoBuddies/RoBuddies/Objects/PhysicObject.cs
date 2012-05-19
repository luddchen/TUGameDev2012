using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using System.Collections.Generic;

namespace Robuddies.Objects
{
    class PhysicObject : GameObject
    {
        private Body body;
        protected World world;
        private List<Texture2D> textureList;

        protected float directionX;
        protected float directionY;


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
            set { world = value; }
        }

        public override Vector2 Position
        {
            get { return body.Position; }
            set { body.Position = value; }
        }

        public override float Rotation
        {
            get { return body.Rotation; }
            set { body.Rotation = value; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.Position.X + Destination.X, (int)this.Position.Y, (int)Width, (int)Height);
            spriteBatch.Draw(Texture, dest, null, Color, Rotation, origin, effects, LayerDepth);
        }

        public virtual float DirectionX
        {
            set { directionX = value; }
            get { return directionX; }
        }

        public virtual float DirectionY
        {
            set { directionY = value; }
            get { return directionY; }
        }

        public List<Texture2D> TextureList
        {
            get { return textureList; }
            set { textureList = value; }
        }
    }
}
