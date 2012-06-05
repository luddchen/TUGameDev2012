using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    class Door : PhysicObject
    {
        private Texture2D door;


        public Door(string theme, Vector2 pos, Vector2 size, Color color, World world, Game game)
            : base(world)
        {
            if (theme.Equals(""))
            {
                door = game.Content.Load<Texture2D>("Sprites//ClosedDoor");
            }
            this.Texture = door;

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;

            this.BodyType = BodyType.Static;
            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
            this.Friction = 10f;
            FixtureFactory.AttachRectangle(Width, Height, 0.5f, Vector2.Zero, this);
        }
    }
}
