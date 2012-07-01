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

namespace RoBuddies.Model.Worlds.Tutorial
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Tutorial_2 : TutorialLevel
    {
        private const string LEVEL_PATH = "Worlds\\Tutorial\\TUTORIAL_2.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;
        private const String LEVEL_NAME = "Tutorial 2: Seperate";

        public Tutorial_2(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            Level.Robot.removeHead();
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(13.5f, 7f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);            
        }

        protected override void addLevelLabels()
        {
            Color backgroundColor = new Color(0, 0, 0, 128);

            HUDString hintStringSeperate = new HUDString("Press 'x'-Key or\n'Y'-Button\nto seperate parts", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringSeperate.Position = ConvertUnits.ToDisplayUnits(new Vector2(-16f, 7.5f));
            levelLabels.Add(hintStringSeperate);

            HUDTexture xboxTextureSeperate = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_Y"), null, 250, 161, null, 0.8f, null, game.Content);
            xboxTextureSeperate.Position = ConvertUnits.ToDisplayUnits(new Vector2(-16f, 4.5f));
            levelLabels.Add(xboxTextureSeperate);


            HUDString hintStringStopClimb = new HUDString("Press 'space'-Key\n or 'A'-Button\nto stop climbing\n\n\n", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringStopClimb.Position = ConvertUnits.ToDisplayUnits(new Vector2(2.0f, 5.0f));
            levelLabels.Add(hintStringStopClimb);

            HUDTexture xboxTextureStopClimbing = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_A"), null, 250, 161, null, 0.8f, null, game.Content);
            xboxTextureStopClimbing.Position = ConvertUnits.ToDisplayUnits(new Vector2(2.0f, 3.6f));
            levelLabels.Add(xboxTextureStopClimbing);

            HUDString hintStringSwitchPart = new HUDString("switch between parts", null, new Vector2(0, 0), null, backgroundColor, 0.85f, null, game.Content);
            hintStringSwitchPart.Position = ConvertUnits.ToDisplayUnits(new Vector2(8f, 9.0f));
            levelLabels.Add(hintStringSwitchPart);

            HUDTexture xboxTextureSwitchPart = new HUDTexture(game.Content.Load<Texture2D>("Sprites//Xbox//Xbox_switchPart"), null, 250, 161, null, 0.8f, null, game.Content);
            xboxTextureSwitchPart.Position = ConvertUnits.ToDisplayUnits(new Vector2(8, 7.0f));
            levelLabels.Add(xboxTextureSwitchPart);
        }
    }

}
