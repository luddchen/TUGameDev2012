
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    class Ladder : PhysicObject
    {
        /// <summary>
        /// Ladder stays on the floor
        /// Robot can climb up with the upperPart
        /// </summary>
        public Ladder(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(world)
        {
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.Texture = texture;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;

            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }


        // sorry, i did it only for testing Utilities.TextureConverter
        public Ladder(Vector2 pos, Vector2 size, Color color, World world, Game game, int ladderSteps)
           : base(world)
        {
            Texture2D center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            this.Texture = Utilities.TextureConverter.connectTCD(game.GraphicsDevice, center, center, center, ladderSteps);

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.BodyType = BodyType.Static;
            this.Friction = 1f;
            FixtureFactory.AttachRectangle(Width, Height, 0.5f, Vector2.Zero, this);
        }

    }
}
