using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Control
{
    /// <summary>
    /// a enumaration of all player usable buttons / keys
    /// </summary>
    public enum ControlButton { left, right, up, down, jump, use, head, robot, menu, enter }; 

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
        /// check if a button was released since last update ; 
        /// that means before update the button was down and now the button is up
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was released</returns>
        public bool ButtonReleased(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        /// <summary>
        /// check if a button was pressed since last update ;
        /// that means before update the button was up and now the button is down
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was pressed</returns>
        public bool ButtonPressed(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        /// <summary>
        /// check if a button is down
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button is down</returns>
        public bool ButtonIsDown(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        /// <summary>
        /// check if a button is up
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button is up</returns>
        public bool ButtonIsUp(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        /// <summary>
        /// check if a button state has toggled ; 
        /// that means the state before update and the state after update are not equal
        /// </summary>
        /// <param name="button">a control button to check</param>
        /// <returns>true, if control button was toggled</returns>
        public bool ButtonToggled(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) != oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        /// <summary>
        /// translate the control button to a specific key
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private Keys getKeyboardKey(ControlButton button)
        {
            Keys key = Keys.Enter;
            switch (button)
            {
                case ControlButton.down: { key = Keys.Down; break; }
                case ControlButton.up: { key = Keys.Up; break; }
                case ControlButton.left: { key = Keys.Left; break; }
                case ControlButton.right: { key = Keys.Right; break; }
                case ControlButton.jump: { key = Keys.Space; break; }
                case ControlButton.use: { key = Keys.S; break; }
                case ControlButton.head: { key = Keys.Y; break; }
                case ControlButton.robot: { key = Keys.X; break; }
                case ControlButton.menu: { key = Keys.Escape; break; }
                case ControlButton.enter: { key = Keys.Enter; break; }
            }
            return key;
        }

    }
}
