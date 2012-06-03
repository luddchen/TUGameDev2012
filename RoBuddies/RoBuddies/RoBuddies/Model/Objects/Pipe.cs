using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// pipe line which hangs up on the ceiling
    /// upperPart can climb along it
    /// </summary>
    class Pipe : PhysicObject
    {
        public Pipe(Vector2 pos, float width, Color color, World world, Game game)
            : base(world)
        {
            int pipeSteps = (int)width / 2;
            // TODO: make good looking Pipe with starts and ends
            Texture2D left = game.Content.Load<Texture2D>("Sprites//PipeStart");
            Texture2D center = game.Content.Load<Texture2D>("Sprites//PipeCenter");
            Texture2D right = game.Content.Load<Texture2D>("Sprites//PipeEnd");
            Texture2D texture = Utilities.TextureConverter.connectLCR(game.GraphicsDevice, center, center, center, pipeSteps);
            this.Texture = texture;

            this.Position = pos;
            this.Width = width;
            this.Height = 0.25f;
            this.Color = color;         

            this.BodyType = BodyType.Static;
            this.Friction = 10f;

            FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

        }
    }
}
