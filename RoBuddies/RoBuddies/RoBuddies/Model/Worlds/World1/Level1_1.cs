using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Worlds.World1
{
    /// <summary>
    /// This class loads the level 1 of world 1 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level1_1 : WorldLevel
    {
        private const string LEVEL_PATH = "Worlds\\World1\\Level1_1.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MENTAL_HOSPITAL;

        public Level1_1(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME)
        {
            Layer farAwayLayer = new Layer("farAwayLayer", new Vector2(0.5f, 1.0f), 0.9f);
            this.Level.AddLayer(farAwayLayer);
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("Sprites//Menu//back_1");
            StaticObject background;
            for (int i = -2; i < 3; i++)
            {
                background = new StaticObject();
                background.Texture = backgroundTexture;
                background.Width = 10;
                background.Height = 5;
                background.Position = new Vector2(i * background.Width, 0.1f);
                farAwayLayer.AddObject(background);
            }

            Layer notSoFarAwayLayer = new Layer("notSoFarAwayLayer", new Vector2(0.75f, 1.0f), 0.8f);
            this.Level.AddLayer(notSoFarAwayLayer);
            Texture2D crateTexture = game.Content.Load<Texture2D>("Sprites//Crate3");
            StaticObject crate;
            for (int i = -2; i < 3; i++)
            {
                crate = new StaticObject();
                crate.Texture = crateTexture;
                crate.Width = 1;
                crate.Height = 1;
                crate.Position = new Vector2(i * crate.Width * 5, -1f);
                notSoFarAwayLayer.AddObject(crate);
            }
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(8f, 0f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            Switch doorSwitcher = new Switch(new Vector2(8f, 5f), new Vector2(1f, 1f), Color.BurlyWood, this.Level, this.game, door, this.Level.Robot);
            levelObjects.Add(door);
            levelObjects.Add(doorSwitcher);
        }
    }
    
}
