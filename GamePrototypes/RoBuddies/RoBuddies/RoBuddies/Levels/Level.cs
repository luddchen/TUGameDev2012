﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Robuddies.Objects;
using FarseerPhysics.Dynamics.Contacts;
using Robuddies.Utilities;

namespace Robuddies.Levels
{
    abstract class Level
    {
        protected List<Layer> layers;
        protected Layer backgroundLayer;
        protected Layer mainLayer;

        protected Robot player;
        protected GameObject activePart;

        protected World gameWorld;
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected Color backgroundColor;
        protected Camera camera;

        public Robot Player 
        {
            get { return player; }
            set { player = value; }
        }

        public Color BackgroundColor 
        {
            get { return backgroundColor; }
            set { backgroundColor = value; } 
        }

        public GameObject ActivePart 
        {
            get { return activePart; }
            set { activePart = value; }
        }

        public World GameWorld 
        {
            get { return gameWorld; }
            set { gameWorld = value; } 
        }
        
        public Layer MainLayer 
        { 
            get { return mainLayer;}
            set { mainLayer = value; }
        }

        public Camera Camera 
        {
            get { return camera; }
            set { camera = value; }
        }

        public Level(Game1 game)
        {
            this.game = game;
            this.gameWorld = new World(new Vector2(0, 20.0f));
            layers = new List<Layer>();
            backgroundColor = Color.Black;
        }

        public  virtual void LoadContent()
        {
            camera = new Camera(game.GraphicsDevice.Viewport);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            backgroundLayer = new BackgroundLayer(Camera);
            layers.Add( backgroundLayer );

            mainLayer = new Layer(Camera);
            mainLayer.LoadContent();

            player = new Robot(game.Content, new Vector2(0, 0), gameWorld, this); 
            //player.Scale *= 0.3f;

            mainLayer.add(player);
            activePart = player.ActivePart;

            layers.Add(mainLayer);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            mainLayer.Update(gameTime);
            Camera.LookAt(player.ActivePart.Physics.Position);

            gameWorld.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Layer l in layers)
            {
                l.Draw(spriteBatch);
            }
        }

        public void ChangeViewport(Viewport viewport)
        {
            Camera.Viewport = viewport;
        }

        /*
         * 
         * This method will be called if the objects you added to the collission detection collide.
         * If you return false, the collission will be ignored from the physic engine.
         * 
         */
        public virtual bool MyOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            // test collision between upper and lower part
            if ((f1.Body == Player.Bud.Physics.Body && f2.Body == Player.Budi.Physics.Body) || (f1.Body == Player.Budi.Physics.Body && f2.Body == Player.Bud.Physics.Body))
            {
                player.IsCombinable = true;
                return true;
            }
            // test if budBudi is on ground
            if (f1.Body == Player.budBudiGroundChecker || f2.Body == Player.budBudiGroundChecker)
            {
                Player.BudBudi.IsOnGround = true;
                return true;
            }
            // test if bud is on ground
            if (f1.Body == Player.budGroundChecker || f2.Body == Player.budGroundChecker)
            {
                Player.Bud.IsOnGround = true;
                return true;
            }
            return true;
        }

        /*
         * 
         * This method will be called if the objects you added to the seperation detection are 
         * not touching anymore afer a not ignored collission (returned true before in the MyOnCollision)
         * 
         */
        public virtual void MyOnSeperation(Fixture f1, Fixture f2)
        {
            // test seperation between upper and lower part
            // damn this only happens if myOnCollision returned true. This will need an other approach.
            if ((f1.Body == Player.Bud.Physics.Body && f2.Body == Player.Budi.Physics.Body) || (f1.Body == Player.Budi.Physics.Body && f2.Body == Player.Bud.Physics.Body))
            {
                player.IsCombinable = false;
            }
            // test if budBudi is on ground
            if (f1.Body == Player.budBudiGroundChecker || f2.Body == Player.budBudiGroundChecker)
            {
                Player.BudBudi.IsOnGround = false;
            }
            // test if player is on ground
            if (f1.Body == Player.budGroundChecker || f2.Body == Player.budGroundChecker)
            {
                Player.Bud.IsOnGround = false;
            }
        }

        /*
         * Add PhysicObjects here to detect collisions with the myOnCollision Method.
         */
        public void addToMyOnCollision(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnCollision += MyOnCollision;
            }
        }

        /*
         * Add Body objects here to detect collisions with the myOnCollision Method.
         */
        public void addToMyOnCollision(Body body)
        {
            foreach (Fixture fix in body.FixtureList)
            {
                fix.OnCollision += MyOnCollision;
            }
        }


        /*
         * Add PhysicObjects here to detect seperations with the myOnSeperation Method.
         */
        public void addToMyOnSeperation(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnSeparation += MyOnSeperation;
            }
        }

        /*
         * Add Body objects here to detect seperations with the myOnSeperation Method.
         */
        public void addToMyOnSeperation(Body body)
        {
            foreach (Fixture fix in body.FixtureList)
            {
                fix.OnSeparation += MyOnSeperation;
            }
        }

        /*
         * Removes a PhysicObject from the collision detection
         */
        public void removeFromMyOnCollision(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnCollision -= MyOnCollision;
            }
        }

        /*
         * Removes a Body object from the collision detection
         */
        public void removeFromMyOnCollision(Body body)
        {
            foreach (Fixture fix in body.FixtureList)
            {
                fix.OnCollision += MyOnCollision;
            }
        }

        /*
         * Removes a Body object from the seperation detection
         */
        public void removeFromMyOnSeperation(Body body)
        {
            foreach (Fixture fix in body.FixtureList)
            {
                fix.OnSeparation -= MyOnSeperation;
            }
        }

        /*
         * Removes a PhysicObject object from the seperation detection
         */
        public void removeFromMyOnSeperation(PhysicObject physicObject)
        {
            foreach (Fixture fix in physicObject.Body.FixtureList)
            {
                fix.OnSeparation -= MyOnSeperation;
            }
        }

    }
}
