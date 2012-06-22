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

        private Texture2D top;
        private Texture2D center;
        private Texture2D bottom;
        private Game game;

        public Ladder(Vector2 pos, float height, Color color, Level level, Game game)
           : base(pos, new Vector2(LADDER_WIDTH, height), color, 10f, level)
        {
            top = game.Content.Load<Texture2D>("Sprites//LadderTop");
            center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            bottom = game.Content.Load<Texture2D>("Sprites//LadderBottom");

            this.game = game;
            //ladderFixture = FixtureFactory.AttachEdge(Vector2.Zero, Vector2.Zero, this);
            //ladderFixture = FixtureFactory.AttachEdge(new Vector2(0, -2), new Vector2(0, -2), this);
            changeLadderHeight(this.Height);
            setUncollidable();
        }

        private void applyTexture()
        {
            float heightOfCenter = this.Height - top.Height * this.Width * 2 / top.Width;
            int ladderSteps = Convert.ToInt32(heightOfCenter * top.Width / (this.Width * center.Height));

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
            createRectangleFixture();
            applyTexture();
        }
    }
}
