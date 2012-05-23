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
        private Color color;
        private bool isHeavyBox; // heavyBoxes can only moved together

        public MovableBox(Texture2D texture, Vector2 pos, Vector2 size, World world, bool isHeavyBox, Robot player)
            : base (texture, pos, world)
        {
            djd = null;

            pulling = false;
            _player = player;
            _player.ActivePart.Activate += Activate;
            this.size = size;
            this.isHeavyBox = isHeavyBox;
            FixtureFactory.AttachRectangle(size.X, size.Y, 1, new Vector2(this.size.X / 2, this.size.Y / 2), this.Body);
            Body.BodyType = BodyType.Dynamic;
            Body.Friction = 0.9f;
            standardColors();

        }

        private void standardColors()
        {
            if (isHeavyBox)
            {
                color = Color.DarkBlue;
            }
            else
            {
                color = Color.CadetBlue;
            }
        }

        public bool IsPulled {
            get { return pulling; }
        }

        public RobotPart TouchingPart
        {
            set {
                if (value is BudBudi)
                {
                    Body.BodyType = BodyType.Dynamic;
                }
                else if (value is Bud && !isHeavyBox)
                {
                    Body.BodyType = BodyType.Dynamic;
                }
                else
                {
                    Body.BodyType = BodyType.Static;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // we need this, because otherwise the box will keep the players motion while pulling
            if (pulling)
            {
                this.Body.LinearVelocity = Vector2.Zero;
            }
            else // with this the box is better controllable after pushing it
            {
                this.Body.LinearVelocity = new Vector2(this.Body.LinearVelocity.X / 2, this.Body.LinearVelocity.Y / 2);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)size.X, (int)size.Y);
            spriteBatch.Draw(Texture, dest, color);
        }

        private void Activate(object sender, EventArgs e)
        {
            if (_player.ActivePart == _player.BudBudi &&
                Vector2.Distance(this.Body.WorldCenter, _player.ActivePart.Physics.Body.WorldCenter) < 35)
            {
                Console.WriteLine("Box activate");
                if (!pulling)
                {
                    djd = new WeldJoint(_player.ActivePart.Physics.Body, Body, this.Body.WorldCenter,
                              _player.ActivePart.Physics.Body.WorldCenter);
                    this.color = Color.DarkGreen;
                    world.AddJoint(djd);
                    pulling = true;
                }
                else
                {
                    standardColors();
                    world.RemoveJoint(djd);
                    pulling = false;
                }
            }
        }
    }
}
