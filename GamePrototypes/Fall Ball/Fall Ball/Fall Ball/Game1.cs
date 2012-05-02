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
using FarseerPhysics.Dynamics.Joints;
using Fall_Ball.Objects;

namespace Fall_Ball
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Overlay overlay;
        KeyboardState keyboardState;
        GamePadState gamepadState;
        int maxScrollBackDelay = 1000;
        int scrollBackDelay;

        Color background = Color.LightGray;
        Color foreground = Color.White;

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
        GameObject movingObject;

        public FixedMouseJoint fixedMouseJoint;

        private int oldMouseWheelValue;

        public bool startFallingBalls = false;

        //sound effects
        Song backgroundMusic; //background Music

        public static SoundEffect bodyclickedEffect; //body click
        public static SoundEffect bodyPlacedEffect; // body placed
        public static SoundEffect collicionEffect; //collicion Effect
        public static SoundEffect bonusEffect; //get bonus point
        public static SoundEffect normalHitEffect; //hit the wall
        public static SoundEffect endGameEffect; //hit the wall

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            //foreground = new Color(255 - background.R, 255 - background.G, 255 - background.B);
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
            textures.Add(Content.Load<Texture2D>("Sprites\\Triangle")); // textures[7]
            textures.Add(Content.Load<Texture2D>("Sprites\\background_stones")); // textures[8]

            level = new Level_2(textures, spriteBatch);
            level.overlay = overlay;

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

            normalHitEffect = Content.Load<SoundEffect>("Sounds/hyperspace_activate");
            collicionEffect = Content.Load<SoundEffect>("Sounds/hyperspace_activate");
            bonusEffect = Content.Load<SoundEffect>("Sounds/bonus");
            bodyclickedEffect = Content.Load<SoundEffect>("Sounds/tong");
            bodyPlacedEffect = Content.Load<SoundEffect>("Sounds/click");
            endGameEffect = Content.Load<SoundEffect>("Sounds/hyperspace_activate");
        }

        public void startBackgroundmusic()
        {
            //sound effect
            backgroundMusic = Content.Load<Song>("Sounds/savanna-stomp-groove");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        private void stopBackgroundmusic()
        {
            //sound effect
            MediaPlayer.Stop();
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            
            //soundEngineInstance.Volume = 0.25f;
            //soundEngineInstance.IsLooped = true;
            //soundEngineInstance.Play();

            if (gamepad.Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboard.IsKeyDown(Keys.R))
            {
                //audioEngine.Update();
                offset.Y = 0;
                stopBackgroundmusic();
                restartGame();
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                startFallingBalls = true;
                startBackgroundmusic();
                level.enableFallingBalls();
            }

            if (oldMouseWheelValue != mouse.ScrollWheelValue)
            {
                offset.Y -= (oldMouseWheelValue - mouse.ScrollWheelValue) / 3;
                oldMouseWheelValue = mouse.ScrollWheelValue;
            }
            if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
            {
                if (keyboard.IsKeyDown(Keys.Up))
                {
                    //playerOffset.Y += 10;
                    offset.Y += 10;
                }

                if (keyboard.IsKeyDown(Keys.Down))
                {
                    //playerOffset.Y -= 10;
                    offset.Y -= 10;
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

            Vector2 pos = ScreenToWorld(mouseController.Cursor);
            if (mouseController.IsNewMouseButtonPressed(MouseButtons.LEFT_BUTTON) && !startFallingBalls)
            {
                if (fixedMouseJoint == null)
                {
                    Fixture savedFixture = this.level.world.TestPoint(pos);
                    if (savedFixture != null)
                    {
                        foreach (GameObject obj in level.addObjects.objects)
                        {
                            if (obj.body == savedFixture.Body)
                            {
                                movingObject = obj;
                                level.addObjects.objects.Remove(obj);
                                movingObject.body.CollidesWith = Category.None;
                                movingObject.body.FixedRotation = true;
                                fixedMouseJoint = new FixedMouseJoint(movingObject.body, pos);
                                fixedMouseJoint.MaxForce = 1000.0f * movingObject.body.Mass;
                                this.level.world.AddJoint(fixedMouseJoint);

                                Console.WriteLine("Body Clicked " + movingObject.body.Position);
                                bodyclickedEffect.Play();
                                break;
                            }
                        }


                        foreach (GameObject obj in level.gamefield.objects)
                        {
                            if (obj.isMoveable && obj.body == savedFixture.Body)
                            {
                                movingObject = obj;
                                level.gamefield.objects.Remove(obj);
                                movingObject.body.CollidesWith = Category.None;
                                movingObject.body.FixedRotation = true;
                                movingObject.body.BodyType = BodyType.Dynamic;
                                fixedMouseJoint = new FixedMouseJoint(movingObject.body, pos);
                                fixedMouseJoint.MaxForce = 1000.0f * movingObject.body.Mass;
                                this.level.world.AddJoint(fixedMouseJoint);

                                Console.WriteLine("Body Clicked " + movingObject.body.Position);
                                bodyclickedEffect.Play();
                                break;
                            }
                        }

                    }
                } 
                else 
                {
                    this.level.world.RemoveJoint(fixedMouseJoint);
                    fixedMouseJoint = null;
                    movingObject.body.BodyType = BodyType.Static;
                    movingObject.body.CollidesWith = Category.All;
                    movingObject.body.Awake = true;
                    level.gamefield.add(movingObject);
                    movingObject = null;
                    bodyPlacedEffect.Play();
                }
            }

            if (mouseController.IsNewMouseButtonPressed(MouseButtons.RIGHT_BUTTON) && movingObject != null)
            {
                movingObject.body.FixedRotation = false;
                movingObject.body.Rotation += 0.7f;
                movingObject.body.FixedRotation = true;
            }

            if (fixedMouseJoint != null)
            {
                fixedMouseJoint.WorldAnchorB = pos;
            }

            base.Update(gameTime);
            level.update(gameTime);
            mouseController.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear( background);

            float drawScale = gameScale * screenScale;

            if (startFallingBalls)
            {
                offset.Y = (float)(screenHeight / 2 - (level.ball1.body.Position.Y + level.ball2.body.Position.Y) * drawScale / 2);
            }

            // limit offset to level
            if (-offset.Y < 0)
            {
                offset.Y = 0;
            }
            if (-(offset.Y - screenHeight) > level.size.Y *drawScale)
            {
                offset.Y = -((level.size.Y * drawScale) - screenHeight);
            }
            // draw backgound pic
            Rectangle backgroundPicDest = new Rectangle(0, 0, (int) (level.size.X * drawScale), screenHeight);
            Rectangle backgroundPicSource = new Rectangle((int)offset.X, (int) -offset.Y / 30, textures[8].Width, 400);
            spriteBatch.Begin();
            spriteBatch.Draw(textures[8], backgroundPicDest, backgroundPicSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();

            // draw level
            level.gamefield.draw( offset + playerOffset, drawScale );

            // draw addObjects
            Vector2 addPos = new Vector2(level.size.X * 1.1f, 0) - (offset + playerOffset)/drawScale;
            for (int i = 0; i < level.addObjects.objects.Count; i++)
            {
                addPos.Y += level.addObjects.objects[i].height/2;
                level.addObjects.objects[i].body.Position = addPos;
                addPos.Y += level.addObjects.objects[i].height / 2;
            }
            level.addObjects.draw(offset + playerOffset, drawScale);

            // draw cursor
            addPos.X = screenWidth * gameScale / 2;
            addPos.Y = screenHeight * gameScale / 2;
            //cursor.draw(addPos + cursorOffset, drawScale);

            // draw minimap
            Rectangle minimapDest = new Rectangle((int)(minimapOffset.X - 2), 
                                            (int)(minimapOffset.Y - 2), 
                                            (int)(2 + screenWidth - minimapOffset.X), 
                                            (int)(2 + screenHeight - minimapOffset.Y));
            Rectangle minimapSource = new Rectangle(0, 0, minimapDest.Width, minimapDest.Height);
            spriteBatch.Begin();
            spriteBatch.Draw(textures[0], minimapDest, null, foreground, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();

            minimapDest.X += 1;
            minimapDest.Y += 1;
            minimapDest.Width -= 2;
            minimapDest.Height -= 2;

            spriteBatch.Begin();
            spriteBatch.Draw(textures[8], minimapDest, minimapSource, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.End();

            level.gamefield.drawMap( minimapOffset, minimapScale * screenScale );

            if (movingObject != null)
            {
                movingObject.draw(offset + playerOffset, drawScale);
            }
            mouseController.Draw();
            base.Draw(gameTime);
        }

        public Vector2 ScreenToWorld(Vector2 pos)
        {
            Vector2 posWorld = (pos - offset) / (gameScale * screenScale);
            return posWorld;
        }

        public Vector2 WorldToScreen(Vector2 pos)
        {
            Vector2 posScreen = (pos * (gameScale * screenScale)) + offset;
            return posScreen;
        }

        private void restartGame()
        {
            startFallingBalls = false;


            level = new Level_2(textures, spriteBatch);
            level.overlay = overlay;
        }

        public void playMusic() {
            bonusEffect.Play();
        }
    }
}