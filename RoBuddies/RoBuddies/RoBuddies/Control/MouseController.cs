using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model;
using RoBuddies.Utilities;
using FarseerPhysics.Dynamics;
using RoBuddies.View;
using FarseerPhysics.Dynamics.Joints;

using RoBuddies.View.HUD;

namespace RoBuddies.Control
{
    /// <summary>
    /// This class changes the state of the mouse.
    /// </summary>
    class MouseController
    {
        private RoBuddies game;
        private Camera camera;
        private HUDMouse mouse;
        public Level level { get; set; }

        /// <summary>
        /// the joint for moving the objects in the editor
        /// </summary>
        private FixedMouseJoint fixedMouseJoint;
        /// <summary>
        /// the current body, which was clicked
        /// </summary>
        Body clickedBody;

        public Vector2 CursorSimPos
        {
            get { return ConvertUnits.ToSimUnits(this.camera.screenToWorld(mouse.Position, new Vector2(1, 1))); }
        }

        private bool isMovingObject
        {
            get { return fixedMouseJoint != null; }
        }

        /// <summary>
        /// creates a new MouseController
        /// </summary>
        /// <param name="game">the game instance</param>
        /// <param name="level">the level which will be controlled by the mouse</param>
        /// <param name="camera">the camera of the level</param>
        /// <param name="mouse">the mouse model, which will be controlled</param>
        public MouseController(RoBuddies game, Level level, Camera camera, HUDMouse mouse)
        {
            this.game = game;
            this.level = level;
            this.camera = camera;
            this.mouse = mouse;
        }

        /// <summary>
        /// this update method updates the mouse and the level according to the current mouse state
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void Update(GameTime gameTime)
        {

            //Console.Out.WriteLine("Units" + mouse.Position);
            updateMousePosition();
            updateMouseButtons();
        }

        private void updateMouseButtons()
        {
            if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
            {
                if (!isMovingObject)
                {
                    //Console.Out.WriteLine("Sim Units" + this.CursorSimPos);
                    Fixture savedFixture = this.level.TestPoint(this.CursorSimPos);
                    if (savedFixture != null)
                    {
                        //Console.Out.WriteLine("jap");
                        clickedBody = savedFixture.Body;
                        clickedBody.BodyType = BodyType.Dynamic;
                        clickedBody.CollidesWith = Category.None;
                        clickedBody.FixedRotation = true;
                        fixedMouseJoint = new FixedMouseJoint(clickedBody, this.CursorSimPos);
                        fixedMouseJoint.MaxForce = 1000.0f * clickedBody.Mass;
                        this.level.AddJoint(fixedMouseJoint);
                    }
                    else // no object behind cursor position
                    {
                        //Console.Out.WriteLine("neee");
                    }
                }
                else // drop current object
                {
                    clickedBody.BodyType = BodyType.Static;
                    clickedBody.CollidesWith = Category.All;
                    clickedBody.Position = adjustAtGrid(clickedBody.Position);
                    this.level.RemoveJoint(fixedMouseJoint);
                    clickedBody = null;
                    fixedMouseJoint = null;
                }
            }
        }

        private void updateMousePosition()
        {
            this.mouse.LastMouseState = this.mouse.CurrentMouseState;
            this.mouse.CurrentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            this.mouse.Position = new Vector2(this.mouse.CurrentMouseState.X, this.mouse.CurrentMouseState.Y);
            Vector2 mousePosition = new Vector2(
                MathHelper.Clamp(this.mouse.Position.X, 0f, game.GraphicsDevice.Viewport.Width),
                MathHelper.Clamp(this.mouse.Position.Y, 0f, game.GraphicsDevice.Viewport.Height)
                );
            this.mouse.Position = mousePosition;

            if (this.mouse.Position.Y < 50) { this.camera.Move(this.camera.Position + new Vector2(0, -3)); }
            if (this.mouse.Position.Y > this.camera.Viewport.Height - 50) { this.camera.Move(this.camera.Position + new Vector2(0, 3)); }
            if (this.mouse.Position.X < 50) { this.camera.Move(this.camera.Position + new Vector2(-3,0)); }
            if (this.mouse.Position.X > this.camera.Viewport.Width - 50) { this.camera.Move(this.camera.Position + new Vector2(3,0)); }

            if (fixedMouseJoint != null)
            {
                fixedMouseJoint.WorldAnchorB = this.CursorSimPos;
            }
        }

        private Vector2 adjustAtGrid(Vector2 pos)
        {
            return new Vector2((int)Math.Round(pos.X), (int)Math.Round(pos.Y));
        }

        /// <summary>
        /// tests if the state of the pressed mousebuttons have changed
        /// </summary>
        /// <param name="button">the left or right mousebutton</param>
        /// <returns></returns>
        public bool isNewMouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT_BUTTON:
                    return (this.mouse.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.mouse.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (this.mouse.CurrentMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.mouse.LastMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                default:
                    return false;
            }
        }

        /// <summary>
        /// tests if the state of the released mousebuttons have changed
        /// </summary>
        /// <param name="button"></param>
        /// <returns>the left or right mousebuttons</returns>
        public bool isNewMouseButtonReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT_BUTTON:
                    return (this.mouse.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.mouse.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (this.mouse.LastMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.mouse.CurrentMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                default:
                    return false;
            }
        }
    }
}
