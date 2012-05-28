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

    class Mouse : IBody
    {

        private bool isVisible;
        private Vector2 position;
        private float rotation;
        private SpriteEffects effect;
        private Texture2D texture;
        private float width;
        private float height;
        private Color color;
        private Vector2 origin;
        private Layer layer;
        private Level world;
        private MouseState currentMouseState;
        private MouseState lastMouseState;

        public bool IsVisible
        {
            get
            {
               return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this.rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        public SpriteEffects Effect
        {
            get
            {
                return this.effect;
            }
            set
            {
                this.effect = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            set
            {
                this.texture = value;
                this.origin = new Vector2(this.texture.Width / 2f, this.texture.Height / 2f);
            }
        }

        public float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public float Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                this.origin = value;
            }
        }

        public Layer Layer
        {
            get
            {
                return this.layer;
            }
            set
            {
                this.layer = value;
            }
        }

        public Level World
        {
            get
            {
                return this.world;
            }
            set
            {
                this.world = value;
            }
        }

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
        /// </summary>
        public Mouse()
        {
            this.isVisible = true;
            this.position = Vector2.Zero;
            this.rotation = 0f;
            this.effect = SpriteEffects.None;
            this.Color = Color.White;
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
