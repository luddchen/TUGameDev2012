﻿using System;
using FarseerPhysics;
using FarseerPhysics.DebugViews;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Model.Worlds;
using RoBuddies.Utilities;
using RoBuddies.Model.Worlds.Mountain;

namespace RoBuddies.View
{
    class LevelView : HUD.HUDLevelView
    {
        public Model.Snapshot.Snapshot SnapShot;
        private int snapshotTimer = 10;
        private int snapshotCounter = 10;
        private int rewindTimer = 3;
        private int rewindCounter = 0;

        public LevelHUD topHud;
        private TimeSpan nextLevelLoadedTime;

        public Worlds worlds;

        /// <summary>
        /// for debugging the physical world
        /// </summary>
        public DebugViewXNA debugView;

        private const float MeterInPixels = 64f;
        private bool showDebug = false;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();
            if (this.topHud != null)
            {
                this.topHud.Viewport = this.viewport;
            }
            if (this.Camera != null)
            {
                this.Camera.SetBoundingBox(this.Level);
            }
        }

        public LevelView(RoBuddies game) : base(game)
        {
            this.background = game.Content.Load<Texture2D>("Sprites//menu//splashscreen");
            this.topHud = new LevelHUD(game);
            this.topHud.IsVisible = false;
            this.Camera.SmoothMove = true;

            this.worlds = new Worlds(game);
        }

        /// <summary>
        /// sets the view to the next level
        /// </summary>
        /// <param name="newLevel">the new level , nullable</param>
        public void viewNextLevel(Level newLevel, GameTime gameTime)
        {
            Level nextLevel;
            if (newLevel == null)
            {
                nextLevel = this.worlds.getNextLevel();
            }
            else
            {
                nextLevel = newLevel;
                this.worlds.setLevel(newLevel);
            }
            if (nextLevel != null)
            {
                SaveGameUtility.saveIfHigher(this.worlds.currentLevelIndex);
            }
            else
            {
                nextLevel = new End(this.Game).Level;
            }
            nextLevelLoadedTime = gameTime.TotalGameTime;
            if (this.SnapShot != null)
            {
                // reset previous level
                this.SnapShot.RewindToStart();
                this.Level.finished = false;
                this.SnapShot.Release();
            }
            this.Level = nextLevel;
            this.Level.Step(0.001f);
            this.SnapShot = new Model.Snapshot.Snapshot(this.Level);
            this.SnapShot.MakeSnapshot(false);

            this.Camera.SetBoundingBox(this.Level);
            this.Camera.SmoothMove = false;
            CameraUpdate();
            this.Camera.SmoothMove = true;
            Console.Out.WriteLine("load level");

            this.debugView = new DebugViewXNA(this.Level);
            this.debugView.AppendFlags(DebugViewFlags.AABB | DebugViewFlags.Joint | DebugViewFlags.DebugPanel | DebugViewFlags.ContactPoints | DebugViewFlags.Shape);
            this.debugView.DefaultShapeColor = Color.White;
            this.debugView.LoadContent(Game.GraphicsDevice, Game.Content);

            this.topHud.setString(this.Level.LevelName);
            this.background = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.SnapShot != null)
            {
                if (ButtonIsDown(Control.ControlButton.rewind))
                {
                    Pause = true;
                    this.rewindCounter--;
                    if (this.rewindCounter < 0)
                    {
                        SnapShot.Rewind();
                        this.rewindCounter = this.rewindTimer;
                    }
                }

                if (ButtonReleased(Control.ControlButton.rewind))
                {
                    Pause = false;
                    SnapShot.PlayOn();
                }
            }

            if (!Pause)
            {

                if (this.Level.finished) // load next level, when current level is finished
                {
                    viewNextLevel(null, gameTime);
                }

                if (gameTime.TotalGameTime - nextLevelLoadedTime > new TimeSpan(0, 0, 5))
                {
                    this.topHud.IsVisible = false;
                }

                if (this.SnapShot != null)
                {
                    snapshotCounter--;
                    if (snapshotCounter == 0)
                    {
                        snapshotCounter = snapshotTimer;
                        this.SnapShot.MakeSnapshot(true);
                    }
                }

                this.Level.Update(gameTime);
                this.topHud.Update(gameTime);

                // test key for switching to the next level
                if (newKeyboardState.IsKeyDown(Keys.F12) && oldKeyboardState.IsKeyUp(Keys.F12))
                {
                    this.Level.finished = true;
                }

                if (newKeyboardState.IsKeyDown(Keys.L) && oldKeyboardState.IsKeyUp(Keys.L))
                {
                    showDebug = !showDebug;
                }
            }

            CameraUpdate();
        }

        /// <summary>
        /// camera following
        /// </summary>
        public void CameraUpdate()
        {
            if (this.Level.Robot != null && this.Level.Robot.ActivePart != null)
            {
                this.Camera.Move(Utilities.ConvertUnits.ToDisplayUnits(this.Level.Robot.ActivePart.Position));
            }
        }

        protected override void DrawContent(SpriteBatch spriteBatch)
        {
            if (this.SnapShot != null)
            {
                base.DrawContent(spriteBatch, this.SnapShot.IsRewinding);
            }
            else
            {
                base.DrawContent(spriteBatch);
            }

            if (showDebug)
            {
                // calculate the projection and view adjustments for the debug view
                Matrix projection = Matrix.CreateOrthographicOffCenter(0f, Game.GraphicsDevice.Viewport.Width / MeterInPixels,
                                                                0f, Game.GraphicsDevice.Viewport.Height / MeterInPixels , 0f,
                                                                 1f) * Matrix.CreateTranslation(new Vector3(1f, 1f, 0)) * Matrix.CreateScale(0.25f);

                Vector2 _screenCenter = this.Camera.Origin;

                Matrix view =  Matrix.CreateTranslation(new Vector3((new Vector2(-this.Camera.Position.X, this.Camera.Position.Y) / MeterInPixels) - (_screenCenter / MeterInPixels), 0f)) * Matrix.CreateTranslation(new Vector3((_screenCenter / MeterInPixels), 0f));
                // draw the debug view
                this.debugView.RenderDebugData(ref projection, ref view);
            }

            if (this.topHud.IsVisible)
            {
                this.topHud.Draw(spriteBatch);
            }
        }
    }
}
