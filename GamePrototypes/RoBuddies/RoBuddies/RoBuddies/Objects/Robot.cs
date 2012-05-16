using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Robuddies.Objects
{
    class Robot : GameObject
    {
        private RobotPart bud;
        private RobotPart budi;
        private RobotPart budBudi;
        private RobotPart head;
        private RobotPart activePart;

        private bool seperated;
        private int seperationDelay = 0;

        private List<RobotPart> robotParts;

        public Robot(ContentManager content, Vector2 pos)
            : base()
        {
            robotParts = new List<RobotPart>();

            bud = new Bud(content, pos);
            budi = new Budi(content, new Vector2(pos.X, pos.Y + 200));
            budBudi = new BudBudi(content, pos);

            activePart = budBudi;
            robotParts.Add(budBudi);
        }

        public List<RobotPart> RobotParts
        {
            get { return robotParts; }
            set { robotParts = value; }
        }

        public override float Size
        {
            get { return activePart.Size; }
            set { 
                foreach (RobotPart part in robotParts)
                {
                    part.Size = value;
                }
            }
        }

        public override float Width
        {
            get { return activePart.Width; }
        }

        public override float Height
        {
            get { return activePart.Height; }
        }

        public bool IsSeperated
        {
            get { return seperated; }
            set { seperated = value; }
        }

        private void Seperate()
        {
            if (seperationDelay > 0) return;

            if (!IsSeperated)
            {
                robotParts.Remove(budBudi);
                robotParts.Add(bud);
                robotParts.Add(budi);
                activePart = bud;
                seperated = true;
                seperationDelay = 10;
            }
            else
            {
                robotParts.Add(budBudi);
                robotParts.Remove(bud);
                robotParts.Remove(budi);
                activePart = budBudi;
                seperated = false;
                seperationDelay = 10;
            }
        }

        public RobotPart Bud
        {
            get { return bud; }
        }

        public RobotPart Budi
        {
            get { return budi; }
        }

        public RobotPart BudBudi
        {
            get { return budBudi; }
        }

        public RobotPart Head
        {
            get { return head; }
            set { head = value; }
        }

        public RobotPart ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (activePart.CurrentState == RobotPart.State.Waiting)
                {
                    activePart.CurrentState = RobotPart.State.StartWalking;
                    activePart.DirectionX = -1; ;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (activePart.CurrentState == RobotPart.State.Waiting)
                {
                    activePart.CurrentState = RobotPart.State.StartWalking;
                    activePart.DirectionX = 1;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Right) && activePart.DirectionX == 1)
            {
                if (activePart.CurrentState == RobotPart.State.Walking)
                {
                    activePart.CurrentState = RobotPart.State.StopWalking;
                }
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.Left) && activePart.DirectionX == -1)
            {
                if (activePart.CurrentState == RobotPart.State.Walking)
                {
                    activePart.CurrentState = RobotPart.State.StopWalking;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if ((activePart.CurrentState != RobotPart.State.Jumping) &&
                    (activePart.CurrentState != RobotPart.State.StartJumping) &&
                    (activePart.CurrentState != RobotPart.State.StopJumping))
                {
                    activePart.CurrentState = RobotPart.State.StartJumping;
                    if (activePart.IsSeperated) { activePart.DirectionY = 3.5f; }
                    if (!activePart.IsSeperated) { activePart.DirectionY = 2.5f; }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Seperate();
            }

            foreach (RobotPart part in robotParts)
            {
                part.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            activePart.Draw(spriteBatch);

            foreach (RobotPart part in robotParts)
            {
                part.Draw(spriteBatch);
            }
        }
    }
}
