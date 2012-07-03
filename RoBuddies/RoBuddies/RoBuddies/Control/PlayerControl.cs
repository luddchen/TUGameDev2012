using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RoBuddies.Control
{
    /// <summary>
    /// a enumaration of all player usable buttons / keys
    /// </summary>
    public enum ControlButton { left, right, up, down, jump, releasePipe, use, head, separateRobot, switchRobotPart, menu, enter, rewind }; 

    /// <summary>
    /// class to manage player inputs from different devices 
    /// (keyboard, gamepad, ...)
    /// </summary>
    class PlayerControl
    {
        /// <summary>
        /// reference to game
        /// </summary>
        public RoBuddies Game { get; set; }

        /// <summary>
        /// the keyboard state before update
        /// </summary>
        protected KeyboardState oldKeyboardState
        {
            get { return Game.oldKeyboardState; }
        }

        /// <summary>
        /// the keyboard state after update
        /// </summary>
        protected KeyboardState newKeyboardState
        {
            get { return Game.newKeyboardState; }
        }

        /// <summary>
        /// the gamepad state before update
        /// </summary>
        protected GamePadState oldGamePadState
        {
            get { return Game.oldGamePadState; }
        }

        /// <summary>
        /// the gamepad state after update
        /// </summary>
        protected GamePadState newGamePadState
        {
            get { return Game.newGamePadState; }
        }

        /// <summary>
        /// check if a button was released since last update ; 
        /// that means before update the button was down and now the button is up
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was released</returns>
        public bool ButtonReleased(ControlButton controlButton)
        {
            Keys key = getKeyboardKey(controlButton);
            Buttons button = getGamePadButton(controlButton);
            Buttons altButton = getGamePadAltButton(controlButton);

            if (newKeyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key)) return true;

            if (newGamePadState.IsButtonUp(button) && oldGamePadState.IsButtonDown(button)) return true;

            if (newGamePadState.IsButtonUp(altButton) && oldGamePadState.IsButtonDown(altButton)) return true;

            return false;
        }

        /// <summary>
        /// check if a button was pressed since last update ;
        /// that means before update the button was up and now the button is down
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was pressed</returns>
        public bool ButtonPressed(ControlButton controlButton)
        {
            Keys key = getKeyboardKey(controlButton);
            Buttons button = getGamePadButton(controlButton);
            Buttons altButton = getGamePadAltButton(controlButton);

            if (newKeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key)) return true;

            if (newGamePadState.IsButtonDown(button) && oldGamePadState.IsButtonUp(button)) return true;

            if (newGamePadState.IsButtonDown(altButton) && oldGamePadState.IsButtonUp(altButton)) return true;

            return false;
        }

        /// <summary>
        /// check if a button is down
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button is down</returns>
        public bool ButtonIsDown(ControlButton controlButton)
        {
            Keys key = getKeyboardKey(controlButton);
            Buttons button = getGamePadButton(controlButton);
            Buttons altButton = getGamePadAltButton(controlButton);

            if (newKeyboardState.IsKeyDown(key)) return true;

            if (newGamePadState.IsButtonDown(button)) return true;

            if (newGamePadState.IsButtonDown(altButton)) return true;

            return false;
        }

        /// <summary>
        /// check if a button is up
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button is up</returns>
        public bool ButtonIsUp(ControlButton controlButton)
        {
            Keys key = getKeyboardKey(controlButton);
            Buttons button = getGamePadButton(controlButton);
            Buttons altButton = getGamePadAltButton(controlButton);

            if (newKeyboardState.IsKeyUp(key)) return true;

            if (newGamePadState.IsButtonUp(button)) return true;

            if (newGamePadState.IsButtonUp(altButton)) return true;

            return false;
        }

        /// <summary>
        /// check if a button state has toggled ; 
        /// that means the state before update and the state after update are not equal
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was toggled</returns>
        public bool ButtonToggled(ControlButton controlButton)
        {
            Keys key = getKeyboardKey(controlButton);
            Buttons button = getGamePadButton(controlButton);
            Buttons altButton = getGamePadAltButton(controlButton);

            if (newKeyboardState.IsKeyDown(key) != oldKeyboardState.IsKeyDown(key)) return true;

            if (newGamePadState.IsButtonDown(button) != oldGamePadState.IsButtonDown(button)) return true;

            if (newGamePadState.IsButtonDown(altButton) != oldGamePadState.IsButtonDown(altButton)) return true;

            return false;
        }

        /// <summary>
        /// translate the control button to a specific key
        /// </summary>
        /// <param name="controlButton"></param>
        /// <returns></returns>
        private Keys getKeyboardKey(ControlButton controlButton)
        {
            Keys key = Keys.Enter;
            switch (controlButton)
            {
                case ControlButton.down: { key = Keys.Down; break; }
                case ControlButton.up: { key = Keys.Up; break; }
                case ControlButton.left: { key = Keys.Left; break; }
                case ControlButton.right: { key = Keys.Right; break; }

                case ControlButton.jump: { key = Keys.Space; break; }
                case ControlButton.releasePipe: { key = Keys.X; break; }

                case ControlButton.use: { key = Keys.A; break; }
                case ControlButton.head: { key = Keys.Y; break; }

                case ControlButton.separateRobot: { key = Keys.X; break; }
                case ControlButton.switchRobotPart: { key = Keys.S; break; }

                case ControlButton.menu: { key = Keys.Escape; break; }
                case ControlButton.enter: { key = Keys.Enter; break; }
                case ControlButton.rewind: { key = Keys.R; break; }
            }
            return key;
        }

        /// <summary>
        /// translate the control button to a specific gamepad button
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private Buttons getGamePadButton(ControlButton controlButton) 
        {
            Buttons button = Buttons.Start;
            switch (controlButton)
            {
                case ControlButton.down: { button = Buttons.DPadDown; break; }
                case ControlButton.up: { button = Buttons.DPadUp; break; }
                case ControlButton.left: { button = Buttons.DPadLeft; break; }
                case ControlButton.right: { button = Buttons.DPadRight; break; }

                case ControlButton.jump: { button = Buttons.A; break; }
                case ControlButton.releasePipe: { button = Buttons.Y; break; }

                case ControlButton.use: { button = Buttons.X; break; }
                case ControlButton.head: { button = Buttons.B; break; }

                case ControlButton.separateRobot: { button = Buttons.Y; break; }
                case ControlButton.switchRobotPart: { button = Buttons.LeftShoulder; break; }

                case ControlButton.menu: { button = Buttons.Start; break; }
                case ControlButton.enter: { button = Buttons.A; break; }
                case ControlButton.rewind: { button = Buttons.LeftTrigger; break; }
            }
            return button;
        }

        /// <summary>
        /// translate the control button to the alternative gamepad button
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private Buttons getGamePadAltButton(ControlButton controlButton)
        {
            Buttons button = Buttons.BigButton;
            switch (controlButton)
            {
                case ControlButton.down: { button = Buttons.LeftThumbstickDown; break; }
                case ControlButton.up: { button = Buttons.LeftThumbstickUp; break; }
                case ControlButton.left: { button = Buttons.LeftThumbstickLeft; break; }
                case ControlButton.right: { button = Buttons.LeftThumbstickRight; break; }

                case ControlButton.switchRobotPart: { button = Buttons.RightShoulder; break; }

                case ControlButton.rewind: { button = Buttons.RightTrigger; break; }
            }
            return button;
        }

    }
}
