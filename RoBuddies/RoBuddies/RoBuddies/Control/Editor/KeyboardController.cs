using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using RoBuddies.View;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace RoBuddies.Control.Editor
{
    /// <summary>
    /// This Class is for changing the model for the EditorView with key inputs
    /// </summary>
    class KeyboardController
    {
        private EditorView HUD;
        private MouseController mouseController;
        
        private KeyboardState oldKeyboardState;

        /// <summary>
        /// Creates a new KeyboardController, which handles the key inputs to the editor
        /// </summary>
        /// <param name="editorView">the editor for this keyboardController</param>
        /// <param name="mouseController">the mouseController of the editorView</param>
        public KeyboardController(EditorView editorView, MouseController mouseController) {
            this.HUD = editorView;
            this.mouseController = mouseController;
        }

        /// <summary>
        /// This method handles the keyboard input to the Editor
        /// </summary>
        public void handleInput() {
            KeyboardState newKeyboardState = Keyboard.GetState();

            changeObjectSizeInput(newKeyboardState);

            oldKeyboardState = newKeyboardState;
        }

        /// <summary>
        /// this input method is for changing the size of the currently clicked object in the editor
        /// </summary>
        private void changeObjectSizeInput(KeyboardState newKeyboardState)
        {
            // increase height
            if (newKeyboardState.IsKeyDown(Keys.Up) && oldKeyboardState.IsKeyUp(Keys.Up) && mouseController.isMovingObject)
            {
                if (mouseController.clickedBody is Wall)
                {
                    Wall wallObject = (Wall)mouseController.clickedBody;
                    Vector2 newWallSize = new Vector2(wallObject.Width, wallObject.Height + 2);
                    wallObject.changeWallSize(newWallSize);
                }
            }
            // increase width
            if (newKeyboardState.IsKeyDown(Keys.Right) && oldKeyboardState.IsKeyUp(Keys.Right) && mouseController.isMovingObject)
            {
                if (mouseController.clickedBody is Wall)
                {
                    Wall wallObject = (Wall)mouseController.clickedBody;
                    Vector2 newWallSize = new Vector2(wallObject.Width + 2, wallObject.Height);
                    wallObject.changeWallSize(newWallSize);
                }
            }
            // decrease height
            if (newKeyboardState.IsKeyDown(Keys.Down) && oldKeyboardState.IsKeyUp(Keys.Down) && mouseController.isMovingObject)
            {
                if (mouseController.clickedBody is Wall)
                {
                    Wall wallObject = (Wall)mouseController.clickedBody;
                    Vector2 newWallSize = new Vector2(wallObject.Width, wallObject.Height - 2);
                    wallObject.changeWallSize(newWallSize);
                }
            }
            // decrease width
            if (newKeyboardState.IsKeyDown(Keys.Left) && oldKeyboardState.IsKeyUp(Keys.Left) && mouseController.isMovingObject)
            {
                if (mouseController.clickedBody is Wall)
                {
                    Wall wallObject = (Wall)mouseController.clickedBody;
                    Vector2 newWallSize = new Vector2(wallObject.Width - 2, wallObject.Height);
                    wallObject.changeWallSize(newWallSize);
                }
            }
        }
    }
}
