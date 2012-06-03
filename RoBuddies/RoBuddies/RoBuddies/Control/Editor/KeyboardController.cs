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
using System.Text.RegularExpressions;
using RoBuddies.Model.Serializer;

namespace RoBuddies.Control.Editor
{

    /// <summary>
    /// This Class is for changing the model for the EditorView with key inputs
    /// </summary>
    class KeyboardController
    {
        private enum InputState { NORMAL, LOAD, SAVE };

        private const String SAVE_LOAD_PATH = ".\\Levels";
        private EditorView HUD;
        private MouseController mouseController;
        
        private KeyboardState oldKeyboardState;

        private InputState inputState;

        /// <summary>
        /// the entered name for the level loading / saving
        /// </summary>
        public String loadSaveFileString;

        /// <summary>
        /// Creates a new KeyboardController, which handles the key inputs to the editor
        /// </summary>
        /// <param name="editorView">the editor for this keyboardController</param>
        /// <param name="mouseController">the mouseController of the editorView</param>
        public KeyboardController(EditorView editorView, MouseController mouseController) {
            this.HUD = editorView;
            this.mouseController = mouseController;
            this.inputState = InputState.NORMAL;
            this.loadSaveFileString = "";
        }

        /// <summary>
        /// This method handles the keyboard input to the Editor
        /// </summary>
        public void handleInput() {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (inputState == InputState.NORMAL)
            {
                changeObjectSizeInput(newKeyboardState);
            }
            if (inputState == InputState.SAVE)
            {
                newKeyboardState = handleSaveInput(newKeyboardState);
            }
            if (inputState == InputState.LOAD)
            {
                newKeyboardState = handleLoadInput(newKeyboardState);
            }
            oldKeyboardState = newKeyboardState;
        }

        private KeyboardState handleLoadInput(KeyboardState newKeyboardState)
        {
            if (newKeyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)) // start loading
            {
                Level loadedLevel = (new LevelReader(this.HUD.Game.Content)).readLevel(SAVE_LOAD_PATH, loadSaveFileString + ".json");
                if (loadedLevel != null)
                {
                    this.HUD.Level = loadedLevel;
                    Console.Out.WriteLine("Game loaded!");
                    this.HUD.ResetCamera();
                }
                this.inputState = InputState.NORMAL;
                loadSaveFileString = "";
                this.HUD.hudString.String = "";

            }
            else if (newKeyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                this.inputState = InputState.NORMAL;
                this.loadSaveFileString = "";
                this.HUD.hudString.String = "";
            }
            else
            {
                fileNameInput(newKeyboardState);
                this.HUD.hudString.String = "Loading from: \n" + loadSaveFileString + "|";
            }
            return newKeyboardState;
        }

        private KeyboardState handleSaveInput(KeyboardState newKeyboardState)
        {
            if (newKeyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)) // start saving
            {
                (new LevelWriter(this.HUD.Level)).writeLevel(SAVE_LOAD_PATH, loadSaveFileString + ".json");
                Console.Out.WriteLine("Game saved!");
                this.inputState = InputState.NORMAL;
                this.loadSaveFileString = "";
                this.HUD.hudString.String = "";
            }
            else if (newKeyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                this.inputState = InputState.NORMAL;
                this.loadSaveFileString = "";
                this.HUD.hudString.String = "";
            }
            else
            {
                fileNameInput(newKeyboardState);
                this.HUD.hudString.String = "Saving to: \n" + loadSaveFileString + "|";
            }
            return newKeyboardState;
        }

        private void fileNameInput(KeyboardState newKeyboardState)
        {
            Keys[] pressedKeys;
            pressedKeys = newKeyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (oldKeyboardState.IsKeyUp(key))
                {
                    String keyString = key.ToString();
                    if (key == Keys.Back && loadSaveFileString.Length > 0) // delete characters
                    {
                        loadSaveFileString = loadSaveFileString.Remove(loadSaveFileString.Length - 1, 1);
                    }
                    if (loadSaveFileString.Length <= 10) // text not longer than 10 characters
                    {
                        if (key == Keys.OemMinus)
                        {
                            loadSaveFileString += "_";
                        }
                        else if (keyString.Length == 1) // normal characters
                        {
                            loadSaveFileString += keyString;
                        }
                        else if (Regex.IsMatch(keyString, "[0-9]$")) // numbers
                        {
                            loadSaveFileString += keyString.ElementAt<char>(keyString.Length - 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// start the input line for saving the level
        /// </summary>
        public void savingInput()
        {
            this.inputState = InputState.SAVE;
        }

        /// <summary>
        /// start the input line for loading the level
        /// </summary>
        public void loadingInput()
        {
            this.inputState = InputState.LOAD;
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
