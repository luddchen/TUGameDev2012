using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Robuddies.Interfaces;

namespace RoBuddies.Model.Objects
{
    /// <summary>
    /// This class can be used to add wall objects to the level.
    /// </summary>
    class Wall : PhysicObject, ISwitchable
    {
        private Game game;
        private bool switchable = false;

        private Texture2D roboLabTex;
        private Texture2D mountainTex;
        private Texture2D soilTex;
        private static Random random = new Random();

        /// <summary>
        /// constructs a new crate object
        /// </summary>
        /// <param name="pos">the position of the crate object in the level</param>
        /// <param name="size">the width and height of the crate</param>
        /// <param name="color">the color of the crate</param>
        /// <param name="texture">the texture which will be layed over the crate</param>
        /// <param name="level">the world object of the physics engine for the physics calculations</param>
        public Wall(Vector2 pos, Vector2 size, Color color, Level level, Game game, bool switchAble)
            : base(pos, size, color, 4f, level)
        {
            this.game = game;

            roboLabTex = game.Content.Load<Texture2D>("Sprites//WallTest");
           // mountainTex = game.Content.Load<Texture2D>("Sprites//Wall" + RandomNumber(1, 4));
            mountainTex = game.Content.Load<Texture2D>("Sprites//Platform");
            soilTex = game.Content.Load<Texture2D>("Sprites//Wall1");

            defineTextures(createTexture(size, roboLabTex), createTexture(size, mountainTex), createTexture(size, roboLabTex));

            if (switchAble)
            {
                this.switchable = true ;
                this.Color = new Color(color.R/2, color.G/2, color.B/2);
                setUncollidable(true);
            }

            createRectangleFixture();
        }

        /// <summary>
        /// Changes the size of this wall object and the attached rectangle fixture
        /// </summary>
        /// <param name="newSize">the new size of the wall</param>
        public void changeWallSize(Vector2 newSize) {
            this.Width = Math.Max(1, newSize.X);
            this.Height = Math.Max(1, newSize.Y);

            createRectangleFixture();

            Texture2D wallTex = createTexture(newSize, roboLabTex);
            defineTextures(wallTex, wallTex, wallTex);
        }

        private Texture2D createTexture(Vector2 newSize, Texture2D wallTex)
        {
            if (newSize.X > 1)
            {
                //wallTex = Utilities.TextureConverter.connectLCR(game.GraphicsDevice, wallTex, wallTex, wallTex, (int)(newSize.X - 2));
                wallTex = Utilities.TextureConverter.connectLCR(game.GraphicsDevice, mountainTex, mountainTex, mountainTex, (int)(newSize.X - 2));

            }
            if (newSize.Y > 1)
            {
                wallTex = Utilities.TextureConverter.connectTCB(game.GraphicsDevice, mountainTex, soilTex, soilTex, (int)(newSize.Y - 2));

             //   wallTex = Utilities.TextureConverter.connectTCB(game.GraphicsDevice, wallTex, wallTex, wallTex, (int)(newSize.Y - 2));
            }
            return wallTex;
        }

        public void switchOn()
        {
            if (switchable)
            {
                Color temp = this.Color;
                temp.R *= 2; temp.G *= 2; temp.B *= 2;
                this.Color = temp;
                for (int i = 0; i < 10 * this.Height; i++ )
                    Position += new Vector2(0, -1);
            }
        }

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
