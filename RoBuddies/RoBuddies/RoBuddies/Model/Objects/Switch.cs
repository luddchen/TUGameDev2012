using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    class Switch : PhysicObject
    {
        private Texture2D switcher;

        public Switch(string thema, Vector2 pos, Vector2 size, Color color, World world, Game game)
            : base(world)
        {
            if (thema.Equals(""))
            {
                switcher = game.Content.Load<Texture2D>("Sprites//lever_right");
            }
            this.Texture = switcher;

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            
            this.BodyType = BodyType.Static;
            this.Friction = 10f; //not know exactly

            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
        }
    }
}
