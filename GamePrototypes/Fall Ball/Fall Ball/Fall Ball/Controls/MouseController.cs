using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fall_Ball.Controls
{
    public enum MouseButtons
    {
        LEFT_BUTTON,
        MIDDLE_BUTTON,
        RIGHT_BUTTON
    }

    public struct MouseSprite
    {
        public Vector2 origin;
        public Texture2D texture;

        public MouseSprite(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;
        }

        public MouseSprite(Texture2D sprite)
        {
            this.texture = sprite;
            this.origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
        }
    }

    public class MouseController
    {
        private MouseState currentMouseState;
        private MouseState lastMouseState;

        private Vector2 cursor;
        private MouseSprite cursorSprite;

        private Game game;
        private Viewport viewport;
        private SpriteBatch spriteBatch;
        private int ScreenWidth;
        private int ScreenHeight;

        public MouseController(Game game, SpriteBatch spriteBatch)
        {
            this.cursor = Vector2.Zero;
            this.game = game;
            this.spriteBatch = spriteBatch;
        }

        public MouseState MouseState
        {
            get { return currentMouseState; }
        }

        public MouseState PreviousMouseState
        {
            get { return lastMouseState; }
        }

        public Vector2 Cursor
        {
            get { return cursor; }
        }

        public void OnScreenResize()
        {
            ScreenHeight = game.GraphicsDevice.Viewport.Height;
            ScreenWidth = game.GraphicsDevice.Viewport.Width;
        }

        public void LoadContent()
        {
            cursorSprite = new MouseSprite(game.Content.Load<Texture2D>("Sprites/Cursor"));
            viewport = game.GraphicsDevice.Viewport;
            ScreenHeight = viewport.Height;
            ScreenWidth = viewport.Width;
        }

        public void Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();


            cursor = new Vector2(currentMouseState.X, currentMouseState.Y);

            cursor.X = MathHelper.Clamp(cursor.X, 0f, ScreenWidth);
            cursor.Y = MathHelper.Clamp(cursor.Y, 0f, ScreenHeight);
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cursorSprite.texture, cursor, null, Color.White, 0f, cursorSprite.origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public bool IsNewMouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT_BUTTON:
                    return (currentMouseState.LeftButton == ButtonState.Pressed &&
                        lastMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (currentMouseState.RightButton == ButtonState.Pressed &&
                        lastMouseState.RightButton == ButtonState.Released);
                default:
                    return false;
            }
        }

        public bool IsNewMouseButtonReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT_BUTTON:
                    return (lastMouseState.LeftButton == ButtonState.Pressed &&
                        currentMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RIGHT_BUTTON:
                    return (lastMouseState.RightButton == ButtonState.Pressed &&
                        currentMouseState.RightButton == ButtonState.Released);
                default:
                    return false;
            }
        }
    }
}
