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
    class Tutorial_5 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_5.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 5: Use Switches";

        public Tutorial_5(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(30.5f, 2f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, true);
            Switch doorSwitcher = new Switch(new Vector2(21.5f, 7.5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);

            Wall switchWall = new Wall(new Vector2(6f, 4.5f), new Vector2(3f, 8f), Color.BurlyWood, this.Level, this.game, true);
            Switch wallSwitcher = new Switch(new Vector2(3f, 3f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, switchWall, this.Level.Robot);
            levelObjects.Add(switchWall);
            levelObjects.Add(wallSwitcher);

        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);
            bool gamePadConnected = GamePad.GetCapabilities(PlayerIndex.One).IsConnected;

            if (gamePadConnected)
            {
                HUDString hintStringSwitcher = new HUDString("use switches", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringSwitcher.Position = ConvertUnits.ToDisplayUnits(new Vector2(1.5f, 9f));
                levelLabels.Add(hintStringSwitcher);

                HUDTexture xboxTextureSwitcher = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_X"), null, 250, 161, null, 0.8f, null, game.Content);
                xboxTextureSwitcher.Position = ConvertUnits.ToDisplayUnits(new Vector2(1.5f, 6.0f));
                levelLabels.Add(xboxTextureSwitcher);
            }
            else
            {
                HUDString hintStringSwitcher = new HUDString("Press 'S'-Key\nto use switches", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
                hintStringSwitcher.Position = ConvertUnits.ToDisplayUnits(new Vector2(1.5f, 9f));
                levelLabels.Add(hintStringSwitcher);
            }
        }
    }

}
