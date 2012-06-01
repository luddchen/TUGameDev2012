using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using RoBuddies.Model.Serializer;

namespace RoBuddies.View
{
    class EditorView : HUD.HUDLevelView
    {
        MouseController mouseController;

        private KeyboardState oldKeyboardState;

        public HUDMouse Mouse;

        public EditorView(RoBuddies game)
            : base(game)
        {
            this.Mouse = new HUDMouse(game.Content);
            this.AllElements.Add(this.Mouse);
            this.mouseController = new MouseController(this.Game, this.Level, this.Camera, this.Mouse);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.mouseController.Update(gameTime);

            KeyboardState newKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S) && oldKeyboardState.IsKeyUp(Keys.S))
                {
                    (new LevelWriter(this.Level)).writeLevel("", "");
                    Console.Out.WriteLine("Game saved!");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.L) && oldKeyboardState.IsKeyUp(Keys.L))
                {
                    Level loadedLevel = (new LevelReader(this.Game.Content)).readLevel("", "");
                    if (loadedLevel != null)
                    {
                        this.mouseController.level = loadedLevel;
                        this.Level = loadedLevel;
                        Console.Out.WriteLine("Game loaded!");
                    }
                }
            }

            // just for the presentation:
            if (newKeyboardState.IsKeyDown(Keys.A) && oldKeyboardState.IsKeyUp(Keys.A))
            {
                Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                Random ran = new Random();
                Color color = new Color(ran.Next(0, 255), ran.Next(0, 255), ran.Next(0, 255));
                Wall wall1 = new Wall(new Vector2(2f, -2f), new Vector2(5f, 1f), color, square, this.Level);
                this.Level.GetLayerByName("mainLayer").AddObject(wall1);
            }
            if (newKeyboardState.IsKeyDown(Keys.Q) && oldKeyboardState.IsKeyUp(Keys.Q))
            {
                Texture2D square = this.Game.Content.Load<Texture2D>("Sprites//Square");
                Random ran = new Random();
                Color color = new Color(ran.Next(0, 255), ran.Next(0, 255), ran.Next(0, 255));
                Wall wall1 = new Wall(new Vector2(2f, -2f), new Vector2(1f, 5f), color, square, this.Level);
                this.Level.GetLayerByName("mainLayer").AddObject(wall1);
            }

            // testing the layer - bug
            if (newKeyboardState.IsKeyDown(Keys.X) && oldKeyboardState.IsKeyUp(Keys.X))
            {
                Console.Out.WriteLine(this.Level.AllLayers.Count +" layer(s)");
                foreach (Layer layer in this.Level.AllLayers)
                {
                    Console.Out.WriteLine("Layer : " + layer.Name);
                }
            }

            oldKeyboardState = newKeyboardState;
        }
    }
}
