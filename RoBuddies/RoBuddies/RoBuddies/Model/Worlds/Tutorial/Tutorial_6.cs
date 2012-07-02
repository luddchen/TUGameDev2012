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
    class Tutorial_6 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_7.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 6: Rewind";

        public Tutorial_6(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();

        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(20.5f, 4f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);

            bool gamePadConnected = GamePad.GetCapabilities(PlayerIndex.One).IsConnected;

            if (gamePadConnected)
            {
                HUDString hintStringRewind = new HUDString("Hold to rewind", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringRewind.Position = ConvertUnits.ToDisplayUnits(new Vector2(8.5f, 0.0f));
                levelLabels.Add(hintStringRewind);


                HUDTexture xboxTextureRewind = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_rewind"), null, 250, 161, null, 0.8f, null, game.Content);
                xboxTextureRewind.Position = ConvertUnits.ToDisplayUnits(new Vector2(8.5f, -3.0f));
                levelLabels.Add(xboxTextureRewind);
            }
            else
            {
                HUDString hintStringRewind = new HUDString("Hold 'R'-key\nto rewind", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringRewind.Position = ConvertUnits.ToDisplayUnits(new Vector2(8.5f, 0.0f));
                levelLabels.Add(hintStringRewind);
            }
        }
    }

}
