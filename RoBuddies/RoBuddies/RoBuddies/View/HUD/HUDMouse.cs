using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.View.HUD
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

    class HUDMouse : HUDTexture
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

        public bool IsVisible { get; set; }

        /// <summary>
        /// constructs a Head Up Display Element Texture
        /// </summary>
        /// <param name="content"></param>
        public HUDMouse(ContentManager content) : base(content)
        {
            this.Texture = content.Load<Texture2D>("Sprites//Cursor");
            this.Width = 20;
            this.Height = 20;
            this.IsVisible = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                base.Draw(spriteBatch);
            }
        }

    }
}
