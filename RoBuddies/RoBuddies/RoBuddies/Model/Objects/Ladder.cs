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
        private const float LADDER_WIDTH = 1.5f;

        private int ladderSteps;  //calculate the repeat number of the center texture by using the parameter size
        private Texture2D top;
        private Texture2D center;
        private Texture2D bottom;
        private Game game;

        private Fixture ladderFixture;

        public Ladder(Vector2 pos, float height, Color color, Level level, Game game)
           : base(level)
        {
            top = game.Content.Load<Texture2D>("Sprites//LadderTop");
            center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            bottom = game.Content.Load<Texture2D>("Sprites//LadderBottom");

            this.Position = pos;
            this.Width = LADDER_WIDTH;
            this.Height = height;
            this.Color = color;
            this.game = game;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;
            ladderFixture = FixtureFactory.AttachRectangle(Width, Height, 1f,Vector2.Zero, this);
            //ladderFixture = FixtureFactory.AttachEdge(Vector2.Zero, Vector2.Zero, this);
            //ladderFixture = FixtureFactory.AttachEdge(new Vector2(0, -2), new Vector2(0, -2), this);

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
        /// Changes the height of this ladder object and the attached rectangle fixture
        /// </summary>
        /// <param name="newHeight">the new size of the crate</param>
        public void changeLadderHeight(float newHeight)
        {
            this.Height = Math.Max(4, newHeight);
            this.DestroyFixture(ladderFixture);
            ladderFixture = FixtureFactory.AttachRectangle(Width, Height, 1, Vector2.Zero, this);
            applyTexture();
        }
    }
}
