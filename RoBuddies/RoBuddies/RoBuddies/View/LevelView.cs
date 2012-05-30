using System;
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
    public class LevelView : HUD.HUD
    {
        /// <summary>
        /// space between levelbottom and screen bottom
        /// </summary>
        private const float bottomBorder = 30;

        public override void OnViewPortResize()
        {
            if (this.Camera != null)
            {
                this.Camera.Viewport = this.viewport;
            }
        }

        /// <summary>
        /// the active camera
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// the level
        /// </summary>
        public Level Level { get; set; }


        public LevelView(RoBuddies game) : base(game)
        {
            this.backgroundColor = new Color(255,255,255,230);
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Menu//back_1");
            this.Camera = new Camera();
            this.Level = new Level(new Vector2(0, 10f));

            //  some testing code here --------------------------------------------------------------------------

                // body1
                    Texture2D crate = this.Game.Content.Load<Texture2D>("Sprites//Crate2");
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                    Texture2D waitTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
                    Texture2D jumpTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");
                    Wall box1 = new Wall(new Vector2(10f, 7f), new Vector2(2f, 2f), Color.BurlyWood, crate, this.Level);
                    box1.BodyType = BodyType.Dynamic;
                    Wall wall1 = new Wall(new Vector2(0f, 8f), new Vector2(100f, 0.5f), Color.YellowGreen, square, this.Level);

                // body 2
                    AnimatedPhysicObject body2 = new AnimatedPhysicObject(this.Level);
                    body2.FixedRotation = true;
                    body2.Position = new Vector2(5f, 7f);
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
                    body2.StateMachine = stateMachine;


                // layer
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    mainLayer.AllObjects.Add(box1);
                    //mainLayer.AllObjects.Add(wall1);
                    mainLayer.AllObjects.Add(body2);
                    this.Level.AllLayers.Add(mainLayer);

                // body 3
                    PhysicObject body3;
                    for (int i = 0; i < 24; i++)
                    {
                        body3 = new PhysicObject(this.Level);
                        body3.Position = new Vector2(1 + i * 0.6f, 8);
                        body3.BodyType = BodyType.Static;
                        FixtureFactory.AttachRectangle(0.5f, 0.5f, 1, Vector2.Zero, body3);
                        body3.Width = 0.5f;
                        body3.Height = 0.5f;
                        body3.Texture = square;
                        body3.Color = Color.DarkKhaki;
                        mainLayer.AllObjects.Add(body3);
                    }
            // end testing code ---------------------------------------------------------------------------------
        }


        /// <summary>
        /// update content
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Level.Update(gameTime);
        }

        /// <summary>
        /// draw a specified layer
        /// </summary>
        /// <param name="layer">layer to draw</param>
        /// <param name="spriteBtach">spritebatch</param>
        public void Draw(Layer layer, SpriteBatch spriteBatch) {

            this.Game.GraphicsDevice.Viewport = this.Viewport;

            this.Game.SpriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, this.Camera.GetViewMatrix(layer.Parallax));

            foreach (IBody body in layer.AllObjects)
            {
                Vector2 displayPos = ConvertUnits.ToDisplayUnits(body.Position);
                Rectangle dest = new Rectangle(
                    (int) displayPos.X,
                    (int) -displayPos.Y,
                    (int) ConvertUnits.ToDisplayUnits(body.Width),
                    (int) ConvertUnits.ToDisplayUnits(body.Height));
                this.Game.SpriteBatch.Draw(body.Texture, dest, null, body.Color, -body.Rotation, body.Origin, body.Effect, layer.LayerDepth);
            }

            this.Game.SpriteBatch.End();
        }

        /// <summary>
        /// draw all layers
        /// </summary>
        /// <param name="spriteBtach">spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Layer layer in this.Level.AllLayers)
            {
                // todo : order layer (layerDepth)

                Draw(layer, spriteBatch); 
            }
        }
    }
}
