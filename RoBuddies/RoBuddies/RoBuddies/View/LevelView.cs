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
            this.Level = new Level(new Vector2(0, -9.8f));

            //  some testing code here --------------------------------------------------------------------------

                // body1
                    Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Crate2");
                    Texture2D circle = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
                    Texture2D jumpTex = this.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");
                    PhysicObject body1 = new PhysicObject(this.Level);
                    body1.Position = new Vector2(11.501f, -2);
                    body1.BodyType = BodyType.Dynamic;
                    FixtureFactory.AttachRectangle(2, 2, 1, Vector2.Zero, body1);
                    body1.Width = 2;
                    body1.Height = 2;
                    body1.Texture = circle;
                    body1.Color = Color.YellowGreen;

                // body 2
                    AnimatedPhysicObject body2 = new AnimatedPhysicObject(this.Level);
                    body2.Position = new Vector2(10, -8);
                    body2.BodyType = BodyType.Dynamic;
                    FixtureFactory.AttachRectangle(3, 3, 1, Vector2.Zero, body2);
                    body2.Width = 3;
                    body2.Height = 3;

                    StateMachine stateMachine = new PartsCombinedStateMachine(body2);
                    State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, circle, stateMachine);
                    State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, stateMachine);
                    State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, circle, stateMachine);
                    stateMachine.AllStates.Add(waitingState);
                    //State example2 = new ExampleState("ExampleState2", circle, stateMachine);
                    stateMachine.AllStates.Add(jumpState);
                    stateMachine.AllStates.Add(walkingState);
                    stateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);

                    body2.Color = Color.Tomato;
                    body2.StateMachine = stateMachine;


                // layer
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1,1) , 0.5f, this.Level);
                    mainLayer.AllObjects.Add(body1);
                    mainLayer.AllObjects.Add(body2);
                    this.Level.AllLayers.Add(mainLayer);
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
                this.Game.SpriteBatch.Draw( body.Texture,
                                            ConvertUnits.ToDisplayUnits(body.Position),     // here i have to know what coordinates i get -> dont want to filter here what type of body that is
                                            null,
                                            body.Color,
                                            -body.Rotation,
                                            body.Origin,
                                            ConvertUnits.ToDisplayUnits( body.Width / body.Texture.Width ),
                                            body.Effect,
                                            layer.LayerDepth);
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
