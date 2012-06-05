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

        public Ladder(Vector2 pos, Vector2 size, Color color, Level level, Game game)
           : base(level)
        {
            Texture2D top = game.Content.Load<Texture2D>("Sprites//LadderTop");
            Texture2D center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            Texture2D bottom = game.Content.Load<Texture2D>("Sprites//LadderBottom");

            float heightOfCenter = size.Y - top.Height * size.X * 2 / top.Width;
            ladderSteps = Convert.ToInt32(heightOfCenter * top.Width / (size.X * center.Height));

            Texture2D ladderTex = Utilities.TextureConverter.connectTCB(game.GraphicsDevice, top, center, bottom, ladderSteps);

            defineTextures(ladderTex, ladderTex, ladderTex);


            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;
            FixtureFactory.AttachRectangle(Width, Height, 0.5f, Vector2.Zero, this);

            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
        }

    }
}
