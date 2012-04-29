using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Fall_Ball.Controls;

namespace Fall_Ball
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Overlay overlay;
        KeyboardState keyboardState;
        GamePadState gamepadState;
        int maxScrollBackDelay = 100;
        int scrollBackDelay;

        Color background = Color.SeaShell;
        Color foreground;

        int screenWidth;
        int screenHeight;

        Vector2 offset;
        Vector2 minimapOffset;

        Vector2 playerOffset;
        Vector2 cursorOffset;

        float gameScale;
        float minimapScale;
        float screenScale;
        List<Texture2D> textures;

        Level level;
        MouseController mouseController;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            foreground = new Color(255 - background.R, 255 - background.G, 255 - background.B);
            overlay = new Overlay(this, foreground);
            Components.Add(overlay);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenScale = (float)(screenWidth) / (float)(level.size.X);
            minimapOffset.X = (float)((float)(screenWidth) - (level.size.X * minimapScale * screenScale));
            minimapOffset.Y = (float)((float)(screenHeight) - (level.size.Y * minimapScale * screenScale));
            mouseController.OnScreenResize();

            overlay.TitleSafe = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mouseController = new MouseController(this, spriteBatch);
            mouseController.LoadContent();

            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Sprites\\Square"));   // textures[0]
            textures.Add(Content.Load<Texture2D>("Sprites\\Circle2"));  // textures[1]
            textures.Add(Content.Load<Texture2D>("Sprites\\Diamond"));  // textures[2]
            textures.Add(Content.Load<Texture2D>("Sprites\\Flower"));   // textures[3]
            textures.Add(Content.Load<Texture2D>("Sprites\\Pepper"));   // textures[4]
            textures.Add(Content.Load<Texture2D>("Sprites\\Smiley"));   // textures[5]
            textures.Add(Content.Load<Texture2D>("Sprites\\Sun"));      // textures[6]

            level = new Level_1(textures, spriteBatch);

            offset = new Vector2(0, 0); // move of the full gamefield
            playerOffset = new Vector2(0, 0);
            cursorOffset = new Vector2(0, 0);

            if ((float)(level.size.Y) / (float)(level.size.X) > 1.0f)
            {
                minimapScale = 0.2f / ((float)(level.size.Y) / (float)(level.size.X)); 
            }
            else
            {
                minimapScale = 0.2f;
            }
            gameScale = 0.8f;
            Window_ClientSizeChanged( null, null);  // sets the minimapoffset vector
            scrollBackDelay = maxScrollBackDelay;

            //if (level.addObjects.objects.Count > 0)
            //{
            //    cursor = level.addObjects.objects[0];
            //    cursor.color = new Color( cursor.color.R, cursor.color.G, cursor.color.B, cursor.color.A / 2);
            //    level.addObjects.remove(cursor);
            //}
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            if (gamepad.Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    playerOffset.Y += 1;
                    scrollBackDelay = maxScrollBackDelay;
                }

                if (keyboard.IsKeyDown(Keys.Down))
                {
                    playerOffset.Y -= 1;
                    scrollBackDelay = maxScrollBackDelay;
                }
            }
            else
            {
                if (scrollBackDelay > 0)
                {
                    scrollBackDelay--;
                }
                else
                {
                    if (playerOffset.Y > 0) playerOffset.Y -= 0.5f;
                    if (playerOffset.Y < 0) playerOffset.Y += 0.5f;
                }

            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                cursorOffset.X -= 1;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                cursorOffset.X += 1;
            }

            keyboardState = keyboard;
            gamepadState = gamepad;

            base.Update(gameTime);
            mouseController.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear( background);

            float drawScale = gameScale * screenScale;

            offset.Y =  (float)(screenHeight / 2 - (level.ball1.body.Position.Y + level.ball2.body.Position.Y) * drawScale / 2);

            // draw level
            level.gamefield.draw( offset + playerOffset, drawScale );

            // draw addObjects
            Vector2 addPos = new Vector2( minimapOffset.X, 0);
            for (int i = 0; i < level.addObjects.objects.Count; i++)
            {
                addPos.Y += 30;
                level.addObjects.objects[i].draw( addPos, drawScale);
            }

            // draw cursor
            addPos.X = screenWidth * gameScale / 2;
            addPos.Y = screenHeight * gameScale / 2;
            //cursor.draw(addPos + cursorOffset, drawScale);

            // draw minimap
            Rectangle dest = new Rectangle((int)(minimapOffset.X - 2), 
                                            (int)(minimapOffset.Y - 2), 
                                            (int)(2 + screenWidth - minimapOffset.X), 
                                            (int)(2 + screenHeight - minimapOffset.Y));
            spriteBatch.Begin();
            spriteBatch.Draw(textures[0], dest, null, foreground, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();

            dest.X += 1;
            dest.Y += 1;
            dest.Width -= 2;
            dest.Height -= 2;
            spriteBatch.Begin();
            spriteBatch.Draw(textures[0], dest, null, background, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();
            level.gamefield.draw( minimapOffset, minimapScale * screenScale );

            level.world.Step(0.1f);
            base.Draw(gameTime);
            mouseController.Draw();
        }
    }
}