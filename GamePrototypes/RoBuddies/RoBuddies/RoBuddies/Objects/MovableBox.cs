using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    class MovableBox : PhysicObject
    {
        private Vector2 size;
        private Robot _player;
        private bool pulling;
        private WeldJoint djd;

        public MovableBox(Texture2D texture, Vector2 pos, World world, Robot player)
            : base (texture, pos, world)
        {
            djd = null;
            pulling = false;
            _player = player;
            _player.ActivePart.Activate += Activate;
            size = new Vector2(35, 35);
            FixtureFactory.AttachRectangle(size.X, size.Y, 1, new Vector2(this.size.X / 2, this.size.Y / 2), this.Body);
            Body.BodyType = BodyType.Dynamic;
            Body.Friction = 0.9f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, Color.Red);
        }

        private void Activate(object sender, EventArgs e)
        {
            if (_player.ActivePart == _player.BudBudi && 
                Vector2.Distance(this.Body.WorldCenter, _player.ActivePart.Physics.Body.WorldCenter) < 35)
            {
                Console.WriteLine("Box activate");
                if (!pulling)
                {
                    djd = new WeldJoint(Body, _player.ActivePart.Physics.Body, this.Body.WorldCenter,
                                                  _player.ActivePart.Physics.Body.WorldCenter);
                    world.AddJoint(djd);
                    pulling = true;
                }
                else
                {
                    world.RemoveJoint(djd);
                    pulling = false;
                }
            }
        }
    }
}
