using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using Robuddies.Levels;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

namespace Robuddies.Objects
{
    class Robot : GameObject
    {
        private const int BUD_BUDI_SHOOT_VELOCITY = 20000;
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

        // flag if roboter is seperated
        private bool seperated;

        // flag if seperated robots is able to combine again
        public bool IsCombinable {
            get { return Vector2.Distance(budi.Physics.Body.Position + new Vector2(budi.Physics.Width / 20, budi.Physics.Height / 20), bud.Physics.Body.Position + new Vector2(bud.Physics.Width / 20, bud.Physics.Height / 20)) <= 11; }
        }

        public Robot(ContentManager content, Vector2 pos, World world, Level level)
            : base()
        {
            this.level = level;
            initRobots(content, pos, world);
        }

        /**
         * Setups the robots and its physics in the current level
         */
        private void initRobots(ContentManager content, Vector2 startingPos, World world) 
        {
            Texture2D budBudiTexture = content.Load<Texture2D>("Sprites\\Buddies\\BudBudi\\0001");
            Texture2D budTexture = content.Load<Texture2D>("Sprites\\Buddies\\Bud\\0001");
            Texture2D budiTexture = content.Load<Texture2D>("Sprites\\Buddies\\Budi\\0080");

            // init physics for budBudi
            budBudiPhysics = new PhysicObject(budBudiTexture, startingPos, world);
            budBudiPhysics.Color = Color.White;
            budBudiPhysics.Body.FixedRotation = true;
            budBudiPhysics.Body.BodyType = BodyType.Dynamic;
            FixtureFactory.AttachRectangle(budBudiPhysics.Width / 30, budBudiPhysics.Height / 10, 1, new Vector2(budBudiPhysics.Width / 20, budBudiPhysics.Height / 20), budBudiPhysics.Body);
            level.MainLayer.add(budBudiPhysics);
            level.addToMyOnCollision(budBudiPhysics);

            // init physics for budi
            budiPhysics = new PhysicObject(budiTexture, startingPos, world);
            budiPhysics.Body.FixedRotation = true;
            budiPhysics.Color = Color.White;
            budiPhysics.Body.BodyType = BodyType.Dynamic;
            budiPhysics.Body.Enabled = false;
            budiPhysics.Visible = false;
            FixtureFactory.AttachRectangle(budiPhysics.Width / 30, budiPhysics.Height / 30, 1, new Vector2(budiPhysics.Width / 20, budiPhysics.Height / 20), budiPhysics.Body);
            level.MainLayer.add(budiPhysics);
            level.addToMyOnCollision(budiPhysics);
            level.addToMyOnSeperation(budiPhysics);

            // init physics for bud
            budPhysics = new PhysicObject(budTexture, startingPos, world);
            budPhysics.Color = Color.White;
            budPhysics.Body.FixedRotation = true;
            budPhysics.Body.BodyType = BodyType.Dynamic;
            budPhysics.Body.Enabled = false;
            budPhysics.Visible = false;
            FixtureFactory.AttachRectangle(budPhysics.Width / 30, budPhysics.Height / 10, 1, new Vector2(budPhysics.Width / 20, budPhysics.Height / 20), budPhysics.Body);
            level.MainLayer.add(budPhysics);
            level.addToMyOnCollision(budPhysics);
            level.addToMyOnSeperation(budPhysics);

            budi = new Budi(content, new Vector2(startingPos.X, startingPos.Y), this, world, budiPhysics);
            bud = new Bud(content, startingPos, this, world, budPhysics);
            budBudi = new BudBudi(content, startingPos, this, world, budBudiPhysics);
            bud.Budi = budi;
            budi.Bud = bud;
            activePart = budBudi;
        }

        /**
         * !UNTESTED!
         * Changes the current level of the robot
         */
        private void changeLevel(Level level, ContentManager content, Vector2 startingPos, World world)
        {
            this.level = level;
            initRobots(content, startingPos, world);
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

        public RobotPart Bud
        {
            get { return bud; }
        }

        public RobotPart Budi
        {
            get { return budi; }
        }

        public World World
        {
            get { return level.GameWorld; }
        }

        private void Seperate()
        {
            if (!IsSeperated)
            {
                budPhysics.Position = new Vector2(budBudiPhysics.Position.X, budBudiPhysics.Position.Y + budPhysics.Height / 20 + 5);
                budiPhysics.Position = new Vector2(budBudiPhysics.Position.X - budiPhysics.Width / 40, budBudiPhysics.Position.Y - budiPhysics.Height / 20);
                budiPhysics.Body.Enabled = true;
                budiPhysics.Visible = true;
                budPhysics.Body.Enabled = true;
                budPhysics.Visible = true;
                budBudiPhysics.Body.Enabled = false;
                budBudiPhysics.Visible = false;
                budiPhysics.Body.LinearVelocity = new Vector2(budiPhysics.Body.LinearVelocity.X, budiPhysics.Body.LinearVelocity.Y - BUD_BUDI_SHOOT_VELOCITY);
                activePart = budi;
                seperated = true;
            }
            else if (IsCombinable)
            {
                budBudiPhysics.Position = new Vector2(budPhysics.Position.X, budPhysics.Position.Y - bud.Height / 15);
                budiPhysics.Body.Enabled = false;
                budiPhysics.Visible = false;
                budPhysics.Body.Enabled = false;
                budPhysics.Visible = false;
                budBudiPhysics.Body.Enabled = true;
                budBudiPhysics.Visible = true;
                activePart = budBudi;
                seperated = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                this.Seperate();
            }

            if (currentState.IsKeyDown(Keys.LeftAlt) && oldState.IsKeyUp(Keys.LeftAlt))
            {
                this.changeActivePart();
            }

            bud.Update(gameTime); budPhysics.effects = bud.effects;
            budi.Update(gameTime); budiPhysics.effects = budi.effects;
            budBudi.Update(gameTime); budBudiPhysics.effects = budBudi.effects;

            oldState = currentState;
        }

        // changes the active robot part if seperated
        private void changeActivePart()
        {
            // switch from bud to budi
            if (IsSeperated && activePart == bud)
            {
                activePart = budi;
            }
            // switch from budi to bud 
            else if (IsSeperated && activePart == budi)
            {
                activePart = bud;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // without any function
        }

    }
}
