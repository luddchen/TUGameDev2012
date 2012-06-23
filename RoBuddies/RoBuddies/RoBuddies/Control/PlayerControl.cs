using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Control
{
    public enum ControlButton { left, right, up, down, jump, use, robot }; 

    class PlayerControl
    {
        protected KeyboardState oldKeyboardState;
        protected KeyboardState newKeyboardState;

        public void Update()
        {
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();
        }

        public bool ButtonReleased(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key) && oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        public bool ButtonPressed(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        public bool ButtonIsDown(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key)) return true;

            return false;
        }

        public bool ButtonIsUp(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyUp(key)) return true;

            return false;
        }

        public bool ButtonToggled(ControlButton button)
        {
            Keys key = getKeyboardKey(button);

            if (newKeyboardState.IsKeyDown(key) != oldKeyboardState.IsKeyDown(key)) return true;

            return false;
        }


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
                case ControlButton.robot: { key = Keys.X; break; }
            }
            return key;
        }

    }
}
