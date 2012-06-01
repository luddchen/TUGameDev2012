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

        private HUDLevelView HUD;
        private EditorToolbar Toolbar;
        //private Camera camera;
        private HUDMouse mouse;
        private bool useToolbar = false;
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
            get { return ConvertUnits.ToSimUnits(this.HUD.Camera.screenToWorld(mouse.Position, new Vector2(1, 1))); }
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
        public MouseController(HUDLevelView HUD, Level level, HUDMouse mouse)
        {
            this.HUD = HUD;
            this.Toolbar = ((EditorView)this.HUD).Toolbar;
            this.level = level;
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
            if (this.useToolbar)
            {
                if (this.Toolbar.resetCamButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
                {
                    this.Toolbar.resetCamButton.Scale = 0.45f;
                    if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                    {
                        ResetCamera();
                    }
                }
                else
                {
                    this.Toolbar.resetCamButton.Scale = 0.4f;
                }

                if (this.Toolbar.clearButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
                {
                    this.Toolbar.clearButton.Scale = 0.45f;
                    if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                    {
                        ResetCamera();
                    }
                }
                else
                {
                    this.Toolbar.clearButton.Scale = 0.4f;
                }

                if (this.Toolbar.loadButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
                {
                    this.Toolbar.loadButton.Scale = 0.45f;
                    if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                    {
                        ResetCamera();
                    }
                }
                else
                {
                    this.Toolbar.loadButton.Scale = 0.4f;
                }

                if (this.Toolbar.saveButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
                {
                    this.Toolbar.saveButton.Scale = 0.45f;
                    if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                    {
                        ResetCamera();
                    }
                }
                else
                {
                    this.Toolbar.saveButton.Scale = 0.4f;
                }


            }

            if (!this.useToolbar)
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
        }

        private void updateMousePosition()
        {
            this.mouse.LastMouseState = this.mouse.CurrentMouseState;
            this.mouse.CurrentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            
            this.mouse.Position = new Vector2(this.mouse.CurrentMouseState.X, this.mouse.CurrentMouseState.Y);
            if (!isMouseOverViewport(this.HUD.Viewport)) 
            {
                this.mouse.IsVisible = false;
                Vector2 mousePosition = new Vector2(
                                                    MathHelper.Clamp(this.mouse.Position.X, this.HUD.Viewport.X, this.HUD.Viewport.Width),
                                                    MathHelper.Clamp(this.mouse.Position.Y, this.HUD.Viewport.Y, this.HUD.Viewport.Height)
                                                    );
                this.mouse.Position = mousePosition;
                return; // no need to calculate something else
            }           
            
            this.mouse.IsVisible = true;

            // toolbar calculations
            if (isMouseOverViewport(this.Toolbar.Viewport))
            {
                this.mouse.Color = Color.Red;
                this.useToolbar = true;
            }
            else // editor calculations
            {
                this.mouse.Color = Color.White;
                this.useToolbar = false;
                CameraMoveConstraint(((EditorView)(this.HUD)).UpArrow, new Vector2(0, -3));
                CameraMoveConstraint(((EditorView)(this.HUD)).DownArrow, new Vector2(0, 3));
                CameraMoveConstraint(((EditorView)(this.HUD)).LeftArrow, new Vector2(-3, 0));
                CameraMoveConstraint(((EditorView)(this.HUD)).RightArrow, new Vector2(3, 0));

                if (fixedMouseJoint != null)
                {
                    fixedMouseJoint.WorldAnchorB = this.CursorSimPos;
                }
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

        private void CameraMoveConstraint(HUDTexture obj, Vector2 moveStep)
        {
            if (obj.Intersects(this.mouse.Position))
            {
                obj.Scale = 1.2f;
                this.HUD.Camera.Move(this.HUD.Camera.Position + moveStep);
            }
            else
            {
                obj.Scale = 1.0f;
            }
        }

        private bool isMouseOverViewport(Viewport viewport) 
        {
            if (this.mouse.Position.X < viewport.X ||
                this.mouse.Position.Y < viewport.Y ||
                this.mouse.Position.X > viewport.X + viewport.Width ||
                this.mouse.Position.Y > viewport.Y + viewport.Height)
            {
                return false;
            }
            return true;
        }

        private void ResetCamera()
        {
            this.HUD.Camera.Move(Vector2.Zero);
            this.HUD.Camera.Rotation = 0.0f;
            this.HUD.Camera.Zoom = 1.0f;
        }

    }
}
