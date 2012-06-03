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
        private Texture2D top;
        private Texture2D center;
        private Texture2D bottom;
        private int ladderSteps;  //calculate the repeat number of the center texture by using the parameter size

        public Ladder(Vector2 pos, Vector2 size, Color color, World world, Game game)
           : base(world)
        {
            top = game.Content.Load<Texture2D>("Sprites//LadderTop");
            center = game.Content.Load<Texture2D>("Sprites//LadderCenter");
            bottom = game.Content.Load<Texture2D>("Sprites//LadderBottom");

            float heightOfCenter = size.Y - top.Height * size.X * 2 / top.Width;
            ladderSteps = Convert.ToInt32(heightOfCenter * top.Width / (size.X * center.Height));

            this.Texture = Utilities.TextureConverter.connectTCD(game.GraphicsDevice, top, center, bottom, ladderSteps);

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.BodyType = BodyType.Static;
            this.Friction = 10f;
            FixtureFactory.AttachRectangle(Width, Height, 0.5f, Vector2.Zero, this);
        }

    }
}
