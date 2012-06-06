using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// Ladder stays on the floor
    /// Robot can climb up with the upperPart
    /// </summary>    
    class Ladder : PhysicObject
    {
        private int ladderSteps;  //calculate the repeat number of the center texture by using the parameter size
        private Texture2D top;
        private Texture2D center;
        private Texture2D bottom;
        private Game game;

        private Fixture ladderFixture;

        public Ladder(Vector2 pos, Vector2 size, Color color, Level level, Game game)
           : base(level)
        {
            top = game.Content.Load<Texture2D>("Sprites//LadderTop");
            center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            bottom = game.Content.Load<Texture2D>("Sprites//LadderBottom");

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.game = game;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;
            ladderFixture = FixtureFactory.AttachRectangle(Width, Height, 0.5f, Vector2.Zero, this);

            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
            applyTexture();
        }

        private void applyTexture()
        {
            float heightOfCenter = this.Height - top.Height * this.Width * 2 / top.Width;
            ladderSteps = Convert.ToInt32(heightOfCenter * top.Width / (this.Width * center.Height));

            Texture2D ladderTex = Utilities.TextureConverter.connectTCB(game.GraphicsDevice, top, center, bottom, ladderSteps);

            defineTextures(ladderTex, ladderTex, ladderTex);
        }


        /// <summary>
        /// Changes the size of this ladder object and the attached rectangle fixture
        /// </summary>
        /// <param name="newSize">the new size of the crate</param>
        public void changeLadderSize(Vector2 newSize)
        {
            this.Width = Math.Max(4, newSize.X);
            this.Height = Math.Max(4, newSize.Y);
            this.DestroyFixture(ladderFixture);
            ladderFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
        }
    }
}
