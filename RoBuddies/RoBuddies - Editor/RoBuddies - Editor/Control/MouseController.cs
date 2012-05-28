using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies___Editor.Model;
using RoBuddies.Model;

namespace RoBuddies___Editor.Controls
{
    /// <summary>
    /// This class changes the state of the mouse.
    /// </summary>
    class MouseController
    {
        private RoBuddiesEditor game;

        private Mouse mouse;

        private Level level;

        /// <summary>
        /// creates a new MouseController
        /// </summary>
        /// <param name="game">the game instance</param>
        /// <param name="level">the level which will be controlled by the mouse</param>
        /// <param name="mouse">the mouse model, which will be controlled</param>
        public MouseController(RoBuddiesEditor game, Level level, Mouse mouse)
        {
            this.game = game;
            this.mouse = mouse;
        }

        /// <summary>
        /// this update method updates the mouse and the level according to the current mouse state
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void Update(GameTime gameTime)
        {
            this.mouse.LastMouseState = this.mouse.CurrentMouseState;
            this.mouse.CurrentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            this.mouse.Position = new Vector2(this.mouse.CurrentMouseState.X, this.mouse.CurrentMouseState.Y);
            Vector2 mousePosition = new Vector2(
                MathHelper.Clamp(this.mouse.Position.X, 0f, game.GraphicsDevice.Viewport.Width),
                MathHelper.Clamp(this.mouse.Position.Y, 0f, game.GraphicsDevice.Viewport.Height)
                );
            this.mouse.Position = mousePosition;
        }

        /// <summary>
        /// tests if the state of the pressed mousebuttons have changed
        /// </summary>
        /// <param name="button">the left or right mousebutton</param>
        /// <returns></returns>
        public bool IsNewMouseButtonPressed(MouseButtons button)
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
        public bool IsNewMouseButtonReleased(MouseButtons button)
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
