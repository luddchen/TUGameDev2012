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
    class Tutorial_1 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 1: Jump";

        public Tutorial_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();

        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(8.5f, 7f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);

            bool gamePadConnected = GamePad.GetCapabilities(PlayerIndex.One).IsConnected;



            if (gamePadConnected)
            {
                HUDString hintStringJump = new HUDString("jump", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-18f, 10f));
                levelLabels.Add(hintStringJump);

                HUDTexture xboxTextureJump = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_A"), null, 250, 161, null, 0.8f, null, game.Content);
                xboxTextureJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-18f, 7.5f));
                levelLabels.Add(xboxTextureJump);

                HUDString hintStringDoor = new HUDString("use doors", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(5.0f, 10f));
                levelLabels.Add(hintStringDoor);

                HUDTexture xboxTextureDoor = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_X"), null, 250, 161, null, 0.8f, null, game.Content);
                xboxTextureDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(5.0f, 7.5f));
                levelLabels.Add(xboxTextureDoor);
            }
            else
            {
                HUDString hintStringJump = new HUDString("Press 'space'-Key to jump", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringJump.Position = ConvertUnits.ToDisplayUnits(new Vector2(-18f, 10f));
                levelLabels.Add(hintStringJump);

                HUDString hintStringDoor = new HUDString("Press 's'-Key to\ngo to use doors", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringDoor.Position = ConvertUnits.ToDisplayUnits(new Vector2(5.0f, 10f));
                levelLabels.Add(hintStringDoor);
            }


        }
    }

}
