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

        private PhysicObject budBudiPhysics;
        private PhysicObject budiPhysics;
        private PhysicObject budPhysics;

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
            Texture2D budBudiTexture = content.Load<Texture2D>("Sprites\\Buddies\\BudBudi\\BudBudi_001");
            // init physics for budBudi
            budBudiPhysics = new PhysicObject(budBudiTexture, new Vector2(100, 1600), world);
            budBudiPhysics.Color = Color.White;
            //budBudiPhysics.Scale = 1f;
            budBudiPhysics.Body.FixedRotation = true;
            budBudiPhysics.Body.BodyType = BodyType.Dynamic;
            // TODO: set better bounding box
            FixtureFactory.AttachRectangle(budBudiPhysics.Width / 3, budBudiPhysics.Height, 10, new Vector2(budBudiPhysics.Width / 2, budBudiPhysics.Height / 2), budBudiPhysics.Body);
            level.MainLayer.add(budBudiPhysics);
            level.addToMyOnCollision(budBudiPhysics);

            // init physics for budi
            budiPhysics = new PhysicObject(null, new Vector2(250, 200), world);
            budiPhysics.Body.FixedRotation = true;
            budiPhysics.Color = Color.White;
            budiPhysics.Scale = 0.3f;
            budBudiPhysics.Body.FixedRotation = true;
            budiPhysics.Body.BodyType = BodyType.Dynamic;
            budiPhysics.Body.Enabled = false;
            // TODO: set better bounding box
            FixtureFactory.AttachRectangle(100, 30, 10, Vector2.Zero, budiPhysics.Body);
            level.MainLayer.add(budiPhysics);
            level.addToMyOnCollision(budiPhysics);

            // init physics for bud
            budPhysics = new PhysicObject(null, new Vector2(300, 200), world);
            budPhysics.Color = Color.White;
            budPhysics.Scale = 0.3f;
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

        public override float Scale
        {
            get { return activePart.Scale; }
            set {
                bud.Scale = value;
                budBudi.Scale = value;
                budi.Scale = value;
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

        // TODO: rewrite seperation logic!
        private void Seperate()
        {
            if (!IsSeperated)
            {
                robotParts.Remove(budBudi);
                inactiveParts.Add(bud);
                inactiveParts.Remove(budi);
                inactiveParts.Remove(budBudi);
                robotParts.Add(budi);
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
        // TODO: Rewrite changeActivePart logic
        private void changeActivePart()
        {
            // switch from bud to budi
            if (IsSeperated && activePart == bud)
            {
                robotParts.Remove(bud);
                inactiveParts.Add(bud);
                inactiveParts.Remove(budi);
                robotParts.Add(budi);
                activePart = budi;
            }
            // switch from budi to bud
            if (IsSeperated && activePart == budi) {
                robotParts.Remove(budi);
                inactiveParts.Add(budi);
                inactiveParts.Remove(bud);
                robotParts.Add(bud);
                activePart = bud;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("PhysicPosition: " + budBudiPhysics.Position);

            foreach (RobotPart part in robotParts)
            {
                //robot will be drawn from the Physics object
                //part.Draw(spriteBatch);
            }

            foreach (RobotPart part in inactiveParts)
            {
                //part.Draw(spriteBatch);
            }
        }
    }
}
