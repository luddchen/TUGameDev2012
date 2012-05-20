using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using Robuddies.Levels;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    class Robot : GameObject
    {
        private KeyboardState oldState;
        private Bud bud;
        private Budi budi;
        private RobotPart budBudi;
        private RobotPart head;
        private RobotPart activePart;
        // the current level of the robot
        private Level level;

        private bool seperated;

        private List<RobotPart> robotParts;
        private List<RobotPart> inactiveParts;

        public Robot(ContentManager content, Vector2 pos, World world, Level level)
            : base()
        {
            robotParts = new List<RobotPart>();
            inactiveParts = new List<RobotPart>();
            this.level = level;

            initRobots(content, pos, world);

        }

        /**
         * Setups the robots and its physics in the current level
         */
        private void initRobots(ContentManager content, Vector2 pos, World world) 
        {
            // init physics for budBudi
            PhysicObject budBudiPhysics = new PhysicObject(null, new Vector2(50, 200), world);
            budBudiPhysics.Color = Color.White;
            budBudiPhysics.Size = 0.3f;
            budBudiPhysics.Body.FixedRotation = true;
            budBudiPhysics.Body.BodyType = BodyType.Dynamic;
            // TODO: set better bounding box
            FixtureFactory.AttachRectangle(100, 30, 10, Vector2.Zero, budBudiPhysics.Body);
            level.MainLayer.add(budBudiPhysics);
            level.addToMyOnCollision(budBudiPhysics);

            // init physics for budi
            PhysicObject budiPhysics = new PhysicObject(null, new Vector2(250, 200), world);
            budiPhysics.Body.FixedRotation = true;
            budiPhysics.Color = Color.White;
            budiPhysics.Size = 0.3f;
            budBudiPhysics.Body.FixedRotation = true;
            budiPhysics.Body.BodyType = BodyType.Dynamic;
            budiPhysics.Body.Enabled = false;
            // TODO: set better bounding box
            FixtureFactory.AttachRectangle(100, 30, 10, Vector2.Zero, budiPhysics.Body);
            level.MainLayer.add(budiPhysics);
            level.addToMyOnCollision(budiPhysics);

            // init physics for bud
            PhysicObject budPhysics = new PhysicObject(null, new Vector2(300, 200), world);
            budPhysics.Color = Color.White;
            budPhysics.Size = 0.3f;
            budBudiPhysics.Body.FixedRotation = true;
            budPhysics.Body.BodyType = BodyType.Dynamic;
            budPhysics.Body.Enabled = false;
            // TODO: set better bounding box
            FixtureFactory.AttachRectangle(100, 30, 10, Vector2.Zero, budPhysics.Body);
            level.MainLayer.add(budPhysics);
            level.addToMyOnCollision(budPhysics);

            budi = new Budi(content, new Vector2(pos.X, pos.Y + 200), world, budiPhysics);
            bud = new Bud(content, pos, world, budPhysics);
            budBudi = new BudBudi(content, pos, world, budBudiPhysics);
            bud.Budi = budi;
            budi.Bud = bud;
            activePart = budBudi;
            robotParts.Add(budBudi);
        }

        /**
         * !UNTESTED!
         * Changes the current level of the robot
         * 
         */
        private void changeLevel(Level level, ContentManager content, Vector2 pos, World world)
        {
            this.level = level;
            initRobots(content, pos, world);
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
                bud.Size = value;
                budBudi.Size = value;
                budi.Size = value;
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
            if (!IsSeperated)
            {
                robotParts.Remove(budBudi);
                inactiveParts.Add(bud);
                inactiveParts.Remove(budi);
                inactiveParts.Remove(budBudi);
                robotParts.Add(budi);
                //bud.setPosition(budBudi.Position.X, bud.Position.Y);
                //bud.Destination.X = budBudi.Destination.X;
                //bud.Destination.Y = budBudi.Destination.Y;
                //bud.Destination.Width = (int)bud.Width;
                //bud.Destination.Height = (int)bud.Height;
                //budi.setPosition(budBudi.Position.X, budi.Position.Y);
                activePart = budi;
                seperated = true;
            }
            else
            {
                robotParts.Add(budBudi);
                robotParts.Remove(bud);
                robotParts.Remove(budi);
                inactiveParts.Remove(bud);
                inactiveParts.Remove(budi);
                activePart = budBudi;
                //budBudi.setPosition(bud.Position.X, bud.Position.Y);
                seperated = false;
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
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                if (activePart.CurrentState == RobotPart.State.Waiting)
                {
                    this.Seperate();
                }
            }

            if (currentState.IsKeyDown(Keys.LeftAlt) && oldState.IsKeyUp(Keys.LeftAlt))
            {
                if (activePart.CurrentState == RobotPart.State.Waiting)
                {
                    this.changeActivePart();
                }
            }

            foreach (RobotPart part in robotParts)
            {
                part.Update(gameTime);
            }

            foreach (RobotPart part in inactiveParts)
            {
                part.Update(gameTime);
            }

            oldState = currentState;
        }

        // changes the active robot part if seperated
        private void changeActivePart()
        {
            // switch from bud to budi
            if (IsSeperated && activePart == bud)
            {
                robotParts.Remove(bud);
                inactiveParts.Add(bud);
                inactiveParts.Remove(budi);
                robotParts.Add(budi);
                //bud.Destination.X = (int) (bud.Position.X - budi.Position.X + bud.Destination.X);
                //bud.Destination.Y = (int) (bud.Position.Y - budi.Position.Y + bud.Destination.Y);
                activePart = budi;
            }
            // switch from budi to bud
            if (IsSeperated && activePart == budi) {
                robotParts.Remove(budi);
                inactiveParts.Add(budi);
                inactiveParts.Remove(bud);
                robotParts.Add(bud);
                //budi.Destination.X = (int)(budi.Position.X - bud.Position.X + budi.Destination.X);
                //budi.Destination.Y = (int) (budi.Position.Y - bud.Position.Y + budi.Destination.Y);
                activePart = bud;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (RobotPart part in robotParts)
            {
                //Console.WriteLine("Position: " + Position);
                //Console.WriteLine("Destination: " + Destination);
                part.Destination = Destination;
                Position = ActivePart.Position;
                part.Draw(spriteBatch);
            }

            foreach (RobotPart part in inactiveParts)
            {
                //Console.WriteLine("Position: " + part.Position);
                //Console.WriteLine("Destination: " + part.Destination);
                part.Draw(spriteBatch);
            }
        }
    }
}
