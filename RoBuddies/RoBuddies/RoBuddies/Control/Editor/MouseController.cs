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
    /// This class changes the state of the mouse.
    /// </summary>
    class MouseController
    {

        private EditorView HUD;
        private EditorToolbar Toolbar;
        //private Camera camera;
        private HUDMouse mouse;
        /// <summary>
        /// flag if the mouse is within the toolbar box
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
            get { return ConvertUnits.ToSimUnits(this.HUD.Camera.screenToWorld(mouse.Position, new Vector2(1, 1))); }
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
        /// <param name="level">the level which will be controlled by the mouse</param>
        /// <param name="camera">the camera of the level</param>
        /// <param name="mouse">the mouse model, which will be controlled</param>
        public MouseController(EditorView HUD, HUDMouse mouse)
        {
            this.HUD = HUD;
            this.Toolbar = this.HUD.Toolbar;
            this.mouse = mouse;
        }

        /// <summary>
        /// this update method updates the mouse and the level according to the current mouse state
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void Update(GameTime gameTime)
        {
            updateMousePosition();
            updateMouseButtons();
        }

        private void updateMouseButtons()
        {
            if (this.useToolbar) // mouse is within the toolbar box
            {
                updateCameraResetButton();

                updateClearButton();

                updateLoadButton();

                updateSaveButton();

                UpdateGridButton();

                UpdateWallButton();

                UpdateBudBudiButton();

                UpdateCrateButton();

                UpdatePipeButton();

            }

            if (!this.useToolbar) // mouse is not in the toolbar box
            {
                UpdateMovingObject();
            }
        }

        /// <summary>
        /// handles the movement and rotation of the objects with the mouse
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

        private void UpdateBudBudiButton()
        {
            if (this.Toolbar.BudBudiButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.BudBudiButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        // Todo: change to Robot Model:
                        //Robot robot = new Robot(this.HUD.Game.Content, this.CursorSimPos, this.HUD.Level);

                        // <very dirty stuff, which will be removed when the robot model is working>
                        Texture2D waitTex = this.HUD.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0001");
                        Texture2D jumpTex = this.HUD.Game.Content.Load<Texture2D>("Sprites//Robot//BudBudi//0040");

                        // robot body
                        PhysicObject robotBody = new PhysicObject(this.HUD.Level);
                        robotBody.FixedRotation = true;
                        robotBody.Position = this.CursorSimPos;
                        robotBody.BodyType = BodyType.Dynamic;
                        robotBody.Color = Color.White;
                        FixtureFactory.AttachRectangle(1, 2.9f, 1, Vector2.Zero, robotBody);
                        robotBody.Width = 3;
                        robotBody.Height = 3;

                        StateMachine stateMachine = new PartsCombinedStateMachine(robotBody, this.HUD.Game);
                        State waitingState = new WaitingState(PartsCombinedStateMachine.WAIT_STATE, waitTex, stateMachine);
                        State jumpState = new JumpingState(PartsCombinedStateMachine.JUMP_STATE, jumpTex, stateMachine);
                        State walkingState = new WaitingState(PartsCombinedStateMachine.WALK_STATE, waitTex, stateMachine);
                        stateMachine.AllStates.Add(waitingState);
                        stateMachine.AllStates.Add(jumpState);
                        stateMachine.AllStates.Add(walkingState);
                        stateMachine.SwitchToState(PartsCombinedStateMachine.WAIT_STATE);
                        this.HUD.Level.AddStateMachine(stateMachine);

                        this.HUD.Level.GetLayerByName("mainLayer").AddObject(robotBody);
                        // </very dirty stuff, which will be removed when the robot model is working>

                        DragObject();
                    }
                }
            }
            else
            {
                this.Toolbar.BudBudiButton.Scale = 0.5f;
            }
        }

        private void UpdateCrateButton()
        {
            if (this.Toolbar.CrateButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.CrateButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Crate crate = new Crate(this.CursorSimPos, new Vector2(3, 3), Color.White, this.HUD.Level, this.HUD.Game);
                        this.HUD.Level.GetLayerByName("mainLayer").AddObject(crate);

                        DragObject();
                    }
                }
            }
            else
            {
                this.Toolbar.CrateButton.Scale = 0.5f;
            }
        }

        private void UpdateWallButton()
        {
            if (this.Toolbar.WallButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.WallButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Texture2D square = this.HUD.Game.Content.Load<Texture2D>("Sprites//Square");
                        Random ran = new Random();
                        Color color = new Color(ran.Next(0, 255), ran.Next(0, 255), ran.Next(0, 255));
                        Wall wall1 = new Wall(this.CursorSimPos, new Vector2(5f, 1f), color, square, this.HUD.Level);
                        this.HUD.Level.GetLayerByName("mainLayer").AddObject(wall1);

                        DragObject();
                    }
                }
            }
            else
            {
                this.Toolbar.WallButton.Scale = 0.5f;
            }
        }

        private void UpdatePipeButton()
        {
            if (this.Toolbar.PipeButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.PipeButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    if (!isMovingObject)
                    {
                        Pipe pipe = new Pipe(this.CursorSimPos, 3, Color.White, this.HUD.Level, this.HUD.Game);
                        Layer layer = this.HUD.Level.GetLayerByName("backLayer");
                        if (layer == null)
                        {
                            layer = new Layer("backLayer", new Vector2(1, 1), 0.51f);
                            this.HUD.Level.AddLayer(layer);
                        }
                        layer.AddObject(pipe);

                        DragObject();
                    }
                }
            }
            else
            {
                this.Toolbar.PipeButton.Scale = 0.5f;
            }
        }

        private void UpdateGridButton()
        {
            if (this.Toolbar.gridButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.gridButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.HUD.IsGridVisible = !this.HUD.IsGridVisible;
                    if (this.HUD.IsGridVisible)
                    {
                        this.Toolbar.gridButton.Color = Color.Red;
                    }
                    else
                    {
                        this.Toolbar.gridButton.Color = Color.Black;
                    }
                }
            }
            else
            {
                this.Toolbar.gridButton.Scale = 0.5f;
            }
        }

        private void updateSaveButton()
        {
            if (this.Toolbar.saveButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.saveButton.Scale = 0.55f;
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
                this.Toolbar.saveButton.Scale = 0.5f;
            }
        }

        private void updateLoadButton()
        {
            if (this.Toolbar.loadButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.loadButton.Scale = 0.55f;
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
                this.Toolbar.loadButton.Scale = 0.5f;
            }
        }

        private void updateClearButton()
        {
            if (this.Toolbar.clearButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.clearButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.HUD.ResetCamera();
                    this.HUD.Level = new Level(new Vector2(0, 10));
                    Layer mainLayer = new Layer("mainLayer", new Vector2(1, 1), 0.5f);
                    this.HUD.Level.AddLayer(mainLayer);
                }
            }
            else
            {
                this.Toolbar.clearButton.Scale = 0.5f;
            }
        }

        private void updateCameraResetButton()
        {
            if (this.Toolbar.resetCamButton.Intersects(this.mouse.Position - new Vector2(this.Toolbar.Viewport.X, this.Toolbar.Viewport.Y)))
            {
                this.Toolbar.resetCamButton.Scale = 0.55f;
                if (isNewMouseButtonPressed(MouseButtons.LEFT_BUTTON))
                {
                    this.HUD.ResetCamera();
                }
            }
            else
            {
                this.Toolbar.resetCamButton.Scale = 0.5f;
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
                CameraMoveConstraint(((EditorView)(this.HUD)).UpArrow, new Vector2(0, -20 / this.HUD.Camera.Zoom));
                CameraMoveConstraint(((EditorView)(this.HUD)).DownArrow, new Vector2(0, 20 / this.HUD.Camera.Zoom));
                CameraMoveConstraint(((EditorView)(this.HUD)).LeftArrow, new Vector2(-20 / this.HUD.Camera.Zoom, 0));
                CameraMoveConstraint(((EditorView)(this.HUD)).RightArrow, new Vector2(20 / this.HUD.Camera.Zoom, 0));

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

        private void DragObject()
        {
            Fixture savedFixture = this.HUD.Level.TestPoint(this.CursorSimPos);
            if (savedFixture != null)
            {
                clickedBody = savedFixture.Body;
                clickedBody.BodyType = BodyType.Dynamic;
                clickedBody.CollidesWith = Category.None;
                clickedBody.FixedRotation = true;
                fixedMouseJoint = new FixedMouseJoint(clickedBody, this.CursorSimPos);
                fixedMouseJoint.MaxForce = 1000.0f * clickedBody.Mass;
                this.HUD.Level.AddJoint(fixedMouseJoint);
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
                clickedBody.CollidesWith = Category.All;
                if (this.HUD.IsGridVisible) { clickedBody.Position = adjustAtGrid(clickedBody.Position); }
                this.HUD.Level.RemoveJoint(fixedMouseJoint);
                clickedBody = null;
                fixedMouseJoint = null;
            }
        }

    }
}
