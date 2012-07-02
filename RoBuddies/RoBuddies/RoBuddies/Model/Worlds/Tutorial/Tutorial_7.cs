using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;
using RoBuddies.View.HUD;
using RoBuddies.Utilities;
using Microsoft.Xna.Framework.Input;

namespace RoBuddies.Model.Worlds.Tutorial
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Tutorial_7 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_6.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 7: Bridge Head";

        public Tutorial_7(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            //Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(21.5f, 2f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);
            
            bool gamePadConnected = GamePad.GetCapabilities(PlayerIndex.One).IsConnected;

            if (gamePadConnected)
            {
                HUDString hintStringBridge = new HUDString("build bridge", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringBridge.Position = ConvertUnits.ToDisplayUnits(new Vector2(2f, 5.5f));
                levelLabels.Add(hintStringBridge);

                HUDTexture xboxTextureBridge = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_B"), null, 250, 161, null, 0.8f, null, game.Content);
                xboxTextureBridge.Position = ConvertUnits.ToDisplayUnits(new Vector2(7.0f, 5.5f));
                levelLabels.Add(xboxTextureBridge);
            }
            else
            {
                HUDString hintStringBridge = new HUDString("Press 'Y'-key\nto build bridge", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringBridge.Position = ConvertUnits.ToDisplayUnits(new Vector2(2f, 5.5f));
                levelLabels.Add(hintStringBridge);
            }
        }
    }

}
