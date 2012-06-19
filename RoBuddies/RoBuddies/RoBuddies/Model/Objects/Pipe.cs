using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// pipe line which hangs up on the ceiling
    /// upperPart can climb along it
    /// </summary>
    class Pipe : PhysicObject
    {
        private Game game;
        private Fixture pipeFixture;

        private Texture2D center;

        public Pipe(Vector2 pos, float width, Color color, Level level, Game game)
            : base(level)
        {
            center = game.Content.Load<Texture2D>("Sprites//pipe");

            this.game = game;
            this.Position = pos;
            this.Width = width;
            this.Height = 0.5f;
            this.Color = color;
            attachTexture();

            this.BodyType = BodyType.Static;
            this.Friction = 100f;

            pipeFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);

            //this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
        }

        private void attachTexture()
        {
            int pipeSteps = (int)this.Width;
            this.Texture = Utilities.TextureConverter.connectLCR(this.game.GraphicsDevice, center, center, center, pipeSteps);
        }

        /// <summary>
        ///  Changes the size of this pipe object and the attached rectangle fixture
        /// </summary>
        /// <param name="newLength">the new length of this pipe</param>
        public void changePipeLength(float newLength)
        {
            this.Width = Math.Max(1, newLength);
            this.DestroyFixture(pipeFixture);
            attachTexture();
            pipeFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }
    }
}
