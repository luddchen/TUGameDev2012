using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RoBuddies.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies___Editor.Model
{
    /// <summary>
    /// the mousebuttons which the model supports
    /// </summary>
    public enum MouseButtons
    {
        LEFT_BUTTON,
        MIDDLE_BUTTON,
        RIGHT_BUTTON
    }

    class Mouse : StaticObject
    {

        private MouseState currentMouseState;
        private MouseState lastMouseState;

        public MouseState CurrentMouseState
        {
            get
            {
                return this.currentMouseState;
            }
            set
            {
                this.currentMouseState = value;
            }
        }

        public MouseState LastMouseState
        {
            get
            {
                return this.lastMouseState;
            }
            set
            {
                this.lastMouseState = value;
            }
        }

        /// <summary>
        /// creates a new mouse instance
        /// <param name="layer">the main layer of the editor, where the objects are</param>
        /// </summary>
        public Mouse()
        {
            this.IsVisible = true;
            this.Position = Vector2.Zero;
            this.Rotation = 0f;
            this.Effect = SpriteEffects.None;
            this.Color = Color.White;
        }

    }
}
