﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using RoBuddies.Model;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;
using RoBuddies.Control;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates;
using RoBuddies.Model.Objects;

namespace RoBuddies.View
{
    public class LevelView : HUD.HUDLevelView
    {
        public HUD.HUD HUD;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();
            if (this.HUD != null)
            {
                this.HUD.Viewport = this.viewport;
            }
        }

        public LevelView(RoBuddies game) : base(game)
        {
            this.HUD = new LevelHUD(game);

            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//back_1");

            Layer mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
            this.Level.AddLayer(mainLayer);

            //  some testing code here --------------------------------------------------------------------------
                // ladder test
                    //Ladder ladder = new Ladder(new Vector2(8f, -4.7f), new Vector2(1.5f, 6f), Color.RosyBrown, this.Level, this.Game, 7);
                    //ladder.BodyType = BodyType.Dynamic;

                    Crate crateExm = new Crate(new Vector2(4f, -7f), new Vector2(2f, 4f), Color.BurlyWood, this.Level, this.Game);    

                // body1
                    Texture2D crate = this.Game.Content.Load<Texture2D>("Sprites//Crate2");
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Texture2D waitTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
                    Texture2D jumpTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");
                    Wall box1 = new Wall(new Vector2(10f, -7f), new Vector2(2f, 2f), Color.BurlyWood, crate, this.Level);
                    //Pipe box1 = new Pipe(new Vector2(10f, -7f), new Vector2(2f, 2f), Color.BurlyWood, crate, this.Level);                   
                    box1.BodyType = BodyType.Dynamic;

                // body 2
                    PhysicObject body2 = new PhysicObject(this.Level);
                    body2.FixedRotation = true;
                    body2.Position = new Vector2(5f, -6f);
                    body2.BodyType = BodyType.Dynamic;
                    body2.Color = Color.White;
                    FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, body2);
                    body2.Width = 3;
                    body2.Height = 3;

                    StateMachine stateMachine = new PartsCombinedStateMachine(body2);
                    State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, stateMachine);
                    State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, stateMachine);
                    State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, stateMachine);
                    stateMachine.AllStates.Add(waitingState);
                    //State example2 = new ExampleState("ExampleState2", waitTex, stateMachine);
                    stateMachine.AllStates.Add(jumpState);
                    stateMachine.AllStates.Add(walkingState);
                    stateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
                    this.Level.AddStateMachine(stateMachine);

                // layerLayer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    //mainLayer.AddObject(ladder);
                    mainLayer.AddObject(crateExm);
                    mainLayer.AddObject(box1);
                    mainLayer.AddObject(body2);
                    this.Level.AllLayers.Add(mainLayer);

                // body 3
                    PhysicObject body3;
                    for (int i = 0; i < 24; i++)
                    {
                        body3 = new PhysicObject(this.Level);
                        body3.Position = new Vector2(1 + i * 0.6f, -8);
                        body3.BodyType = BodyType.Static;
                        FixtureFactory.AttachRectangle(0.5f, 0.5f, 1, Vector2.Zero, body3);
                        body3.Width = 0.5f;
                        body3.Height = 0.5f;
                        body3.Texture = square;
                        body3.Color = Color.DarkKhaki;
                        mainLayer.AddObject(body3);
                    }
            // end testing code ---------------------------------------------------------------------------------
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.HUD.Update(gameTime);
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            base.DrawContent(spriteBatch);
            this.HUD.Draw(spriteBatch);
        }

    }
}
