using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Control.Editor;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using RoBuddies.Utilities;
using RoBuddies.View;
using RoBuddies.View.HUD;
using FarseerPhysics.Factories;
using RoBuddies.Control.StateMachines;
using RoBuddies.Control.RobotStates;

namespace RoBuddies.Control
{
    /// <summary>
    /// This class changes the state of the hudMouse.
    /// </summary>
    class MouseController
    {

        private EditorView editorView;
        private EditorToolbar toolbar;
        //private Camera camera;
        private HUDMouse hudMouse;
        /// <summary>
        /// flag if the hudMouse is within the toolbar box
        /// </summary>
        private bool useToolbar = false;

        /// <summary>
        /// the joint for moving the objects in the editor
        /// </summary>
        private FixedMouseJoint fixedMouseJoint;

        /// <summary>
        /// the current body, which was clicked
        /// </summary>
        public Body clickedBody;

        /// <summary>
        /// get & set the keyboardController of the level editor
        /// </summary>
        public KeyboardController keyboardController;

        /// <summary>
        /// the current position of the cursor in sim coordinates
        /// </summary>
        public Vector2 CursorSimPos
        {
            get { return ConvertUnits.ToSimUnits(this.editorView.Camera.screenToWorld(hudMouse.Position, new Vector2(1, 1))); }
        }

        /// <summary>
        /// true, if the cursor is currently moving a object like a crate
        /// </summary>
        public bool isMovingObject
        {
            get { return clickedBody != null; }
        }

        /// <summary>
        /// creates a new MouseController
        /// </summary>
        /// <param name="game">the game instance</param>
        /// <param name="HUDmouse">the hudMouse model, which will be controlled</param>
        public MouseController(EditorView editorView, HUDMouse hudMouse)
        {
            this.editorView = editorView;
            this.toolbar = this.editorView.Toolbar;
            this.hudMouse = hudMouse;
        }

        /// <summary>
        /// this update method updates the hudMouse and the level according to the current hudMouse state
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void Update(GameTime gameTime)
        {
            updateMousePosition();
            updateMouseButtons();
        }

        private void updateMouseButtons()
        {
            if (this.useToolbar) // hudMouse is within the toolbar box
            {
                updateCameraResetButton();

                updateClearButton();

                updateLoadButton();

                updateSaveButton();

                UpdateGridButton();

                UpdateWallButton();

                UpdateLadderButton();

                UpdateBudBudiButton();

                UpdateCrateButton();

                UpdatePipeButton();

            }

            if (!this.useToolbar) // hudMouse is not in the toolbar box
            {
                UpdateMovingObject();
            }
        }

        /// <summary>
        /// handles the movement and rotation of the objects with the hudMouse
        /// </summary>
        private void UpdateMovingObject()
        {
            if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON)) // drag or drop objects
            {
                if (!isMovingObject)
                {
                    DragObject();
                }
                else
                {
                    DropObject();
                }
            }
            if (isNewMouseButtonPressed(MouseButtons.RIGHT_BUTTON) && isMovingObject) // rotate dragged objects
            {
                // rotate walls
                if (clickedBody is Wall)
                {
                    Wall clickedWall = (Wall)clickedBody;
                    Vector2 newSize = new Vector2(clickedWall.Height, clickedWall.Width);
                    clickedWall.changeWallSize(newSize);
                }
            }
        }

        private void UpdateLadderButton()
        {
            if (this.toolbar.LadderButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.LadderButton.Scale = 0.45f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Ladder ladder = new Ladder(this.CursorSimPos, 4f, Color.RosyBrown, this.editorView.Level, this.editorView.Game);
                        Layer layer = this.editorView.Level.GetLayerByName("backLayer");
                        if (layer == null)
                        {
                            layer = new Layer("backLayer", new Vector2(1, 1), 0.51f);
                            this.editorView.Level.AddLayer(layer);
                        }
                        layer.AddObject(ladder);
                        DragObject();
                    }
                }
            }
            else
            {
                this.toolbar.LadderButton.Scale = 0.4f;
            }
        }

        private void UpdateBudBudiButton()
        {
            if (this.toolbar.BudBudiButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.BudBudiButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Robot robot = new Robot(this.editorView.Game.Content, this.CursorSimPos, this.editorView.Level, this.editorView.Game);

                        DragObject();
                    }
                }
            }
            else
            {
                this.toolbar.BudBudiButton.Scale = 0.5f;
            }
        }

        private void UpdateCrateButton()
        {
            if (this.toolbar.CrateButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.CrateButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Crate crate = new Crate(this.CursorSimPos, new Vector2(3, 3), Color.White, this.editorView.Level, this.editorView.Game);
                        this.editorView.Level.GetLayerByName("mainLayer").AddObject(crate);

                        DragObject();
                    }
                }
            }
            else
            {
                this.toolbar.CrateButton.Scale = 0.5f;
            }
        }

        private void UpdateWallButton()
        {
            if (this.toolbar.WallButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.WallButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Texture2D square = this.editorView.Game.Content.Load<Texture2D>("Sprites//Square");
                        Wall wall = new Wall(this.CursorSimPos, new Vector2(5f, 1f), Color.White, this.editorView.Level, this.editorView.Game, false);
                        this.editorView.Level.GetLayerByName("mainLayer").AddObject(wall);

                        DragObject();
                    }
                }
            }
            else
            {
                this.toolbar.WallButton.Scale = 0.5f;
            }
        }

        private void UpdatePipeButton()
        {
            if (this.toolbar.PipeButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.PipeButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Pipe pipe = new Pipe(this.CursorSimPos, 3, Color.White, this.editorView.Level, this.editorView.Game);
                        Layer layer = this.editorView.Level.GetLayerByName("backLayer");
                        if (layer == null)
                        {
                            layer = new Layer("backLayer", new Vector2(1, 1), 0.51f);
                            this.editorView.Level.AddLayer(layer);
                        }
                        layer.AddObject(pipe);

                        DragObject();
                    }
                }
            }
            else
            {
                this.toolbar.PipeButton.Scale = 0.5f;
            }
        }

        private void UpdateGridButton()
        {
            if (this.toolbar.gridButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.gridButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.editorView.IsGridVisible = !this.editorView.IsGridVisible;
                    if (this.editorView.IsGridVisible)
                    {
                        this.toolbar.gridButton.Color = Color.Red;
                    }
                    else
                    {
                        this.toolbar.gridButton.Color = Color.Black;
                    }
                }
            }
            else
            {
                this.toolbar.gridButton.Scale = 0.5f;
            }
        }

        private void updateSaveButton()
        {
            if (this.toolbar.saveButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.saveButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (keyboardController != null)
                    {
                        keyboardController.savingInput();
                    }
                    else
                    {
                        throw new InvalidOperationException("no keyboardController reference");
                    }
                }
            }
            else
            {
                this.toolbar.saveButton.Scale = 0.5f;
            }
        }

        private void updateLoadButton()
        {
            if (this.toolbar.loadButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.loadButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (keyboardController != null)
                    {
                        keyboardController.loadingInput();
                    }
                    else
                    {
                        throw new InvalidOperationException("no keyboardController reference");
                    }
                }
            }
            else
            {
                this.toolbar.loadButton.Scale = 0.5f;
            }
        }

        private void updateClearButton()
        {
            if (this.toolbar.clearButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.clearButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.editorView.ResetCamera();
                    this.editorView.Level = new Level(new Vector2(0, 10));
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
                    this.editorView.Level.AddLayer(mainLayer);
                }
            }
            else
            {
                this.toolbar.clearButton.Scale = 0.5f;
            }
        }

        private void updateCameraResetButton()
        {
            if (this.toolbar.resetCamButton.Intersects(this.hudMouse.Position - new Vector2(this.toolbar.Viewport.X, this.toolbar.Viewport.Y)))
            {
                this.toolbar.resetCamButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.editorView.ResetCamera();
                }
            }
            else
            {
                this.toolbar.resetCamButton.Scale = 0.5f;
            }
        }

        private void updateMousePosition()
        {
            this.hudMouse.LastMouseState = this.hudMouse.CurrentMouseState;
            this.hudMouse.CurrentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            
            this.hudMouse.Position = new Vector2(this.hudMouse.CurrentMouseState.X, this.hudMouse.CurrentMouseState.Y);
            if (!isMouseOverViewport(this.editorView.Viewport)) 
            {
                this.hudMouse.IsVisible = false;
                Vector2 mousePosition = new Vector2(
                                                    MathHelper.Clamp(this.hudMouse.Position.X, this.editorView.Viewport.X, this.editorView.Viewport.Width),
                                                    MathHelper.Clamp(this.hudMouse.Position.Y, this.editorView.Viewport.Y, this.editorView.Viewport.Height)
                                                    );
                this.hudMouse.Position = mousePosition;
                return; // no need to calculate something else
            }           
            
            this.hudMouse.IsVisible = true;

            // toolbar calculations
            if (isMouseOverViewport(this.toolbar.Viewport))
            {
                this.hudMouse.Color = Color.Red;
                this.useToolbar = true;
            }
            else // editor calculations
            {
                this.hudMouse.Color = Color.White;
                this.useToolbar = false;
                CameraMoveConstraint(((EditorView)(this.editorView)).UpArrow, new Vector2(0, -20 / this.editorView.Camera.Zoom));
                CameraMoveConstraint(((EditorView)(this.editorView)).DownArrow, new Vector2(0, 20 / this.editorView.Camera.Zoom));
                CameraMoveConstraint(((EditorView)(this.editorView)).LeftArrow, new Vector2(-20 / this.editorView.Camera.Zoom, 0));
                CameraMoveConstraint(((EditorView)(this.editorView)).RightArrow, new Vector2(20 / this.editorView.Camera.Zoom, 0));

                if (clickedBody != null)
                {
                    fixedMouseJoint.WorldAnchorB = this.CursorSimPos;
                    clickedBody.Position = this.CursorSimPos;
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
                    return (this.hudMouse.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.hudMouse.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (this.hudMouse.CurrentMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.hudMouse.LastMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
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
                    return (this.hudMouse.LastMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.hudMouse.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (this.hudMouse.LastMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                        this.hudMouse.CurrentMouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
                default:
                    return false;
            }
        }

        private void CameraMoveConstraint(HUDTexture obj, Vector2 moveStep)
        {
            if (obj.Intersects(this.hudMouse.Position))
            {
                obj.Scale = 1.2f;
                this.editorView.Camera.Move(this.editorView.Camera.Position + moveStep);
            }
            else
            {
                obj.Scale = 1.0f;
            }
        }

        private bool isMouseOverViewport(Viewport viewport) 
        {
            if (this.hudMouse.Position.X < viewport.X ||
                this.hudMouse.Position.Y < viewport.Y ||
                this.hudMouse.Position.X > viewport.X + viewport.Width ||
                this.hudMouse.Position.Y > viewport.Y + viewport.Height)
            {
                return false;
            }
            return true;
        }

        private void DragObject()
        {
            Fixture savedFixture = this.editorView.Level.TestPoint(this.CursorSimPos);
            if (savedFixture != null)
            {
                clickedBody = savedFixture.Body;
                clickedBody.BodyType = BodyType.Dynamic;
                clickedBody.CollidesWith = Category.None;
                clickedBody.FixedRotation = true;
                fixedMouseJoint = new FixedMouseJoint(clickedBody, this.CursorSimPos);
                fixedMouseJoint.MaxForce = 1000.0f * clickedBody.Mass;
                this.editorView.Level.AddJoint(fixedMouseJoint);
            }
        }

        /// <summary>
        /// Drops the current clickedBody
        /// </summary>
        public void DropObject() 
        {
            if (clickedBody != null)
            {
                clickedBody.BodyType = BodyType.Static;
                if (this.editorView.IsGridVisible) { clickedBody.Position = adjustAtGrid(clickedBody.Position); }
                this.editorView.Level.RemoveJoint(fixedMouseJoint);

                // filtering collisionobjects -> maybe there is an better way to do it (Body.CollisionGroup is not readable)
                if ( (clickedBody is Pipe) || (clickedBody is Door) || (clickedBody is Ladder) || (clickedBody is Switch) )
                {
                    clickedBody.CollisionCategories = Category.Cat1;
                    clickedBody.CollidesWith = Category.None;
                } 
                else 
                {
                    clickedBody.CollidesWith = Category.All;
                }

                clickedBody = null;
                fixedMouseJoint = null;
            }
        }

    }
}
