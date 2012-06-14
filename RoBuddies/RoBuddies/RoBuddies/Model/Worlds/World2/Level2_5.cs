using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Worlds.World2
{
    /// <summary>
    /// This class loads the level 5 of world 2 and adds 
    /// objects to the level, which can not be used in the editor
    /// </summary>
    class Level2_5 : World2Level
    {
        private const string LEVEL_PATH = "Worlds\\World2\\LEVEL2_5.json";
        private const LevelTheme LEVEL_THEME = LevelTheme.MOUNTAIN;

        public Level2_5(Game game)
            : base(game, LEVEL_PATH, LEVEL_THEME)
        {

        }

        protected override void addBackground()
        {
            this.Level.Background = Color.Black;
            float offset = -7.5f;

            Layer sunLayer = new Layer("sunLayer", new Vector2(0.05f, 0.25f), 0.99f);
            this.Level.AddLayer(sunLayer);

            Texture2D sunTexture = game.Content.Load<Texture2D>("Sprites//Mountain//sun1");
            StaticObject sun = new StaticObject(sunTexture, new Vector2(-3, 6 + offset), 2.5f, 2.5f, new Color(192, 192, 0, 255), 0);
            sunLayer.AddObject(sun);

            Layer farAwayLayer = new Layer("farAwayLayer", new Vector2(0.2f, 0.15f), 0.95f);
            this.Level.AddLayer(farAwayLayer);
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("Sprites//Mountain//sky");
            StaticObject background;
            for (int i = 0; i < 3; i++)
            {
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, -2 + offset), 40, 8, new Color(255, 255, 255, 180), 0);
                farAwayLayer.AddObject(background);
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, 6 + offset), 40, 8, new Color(255, 255, 255, 180), 0);
                background.Effect = SpriteEffects.FlipVertically;
                farAwayLayer.AddObject(background);
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, 14 + offset), 40, 8, new Color(255, 255, 255, 180), 0);
                farAwayLayer.AddObject(background);
            }

            Layer notSoFarAwayLayer = new Layer("notSoFarAwayLayer", new Vector2(0.5f, 0.4f), 0.8f);
            this.Level.AddLayer(notSoFarAwayLayer);
            Texture2D mountain1Texture = game.Content.Load<Texture2D>("Sprites//Mountain//dark_mountain1");
            Texture2D mountain2Texture = game.Content.Load<Texture2D>("Sprites//Mountain//light_mountain1");
            StaticObject mountain;

            mountain = new StaticObject(mountain2Texture, new Vector2(0, 0f + offset), 40, 10, new Color(245, 255, 240, 255), 0);
            notSoFarAwayLayer.AddObject(mountain);
            mountain = new StaticObject(mountain2Texture, new Vector2(18, -1f + offset), 40, 8, new Color(245, 255, 240, 255), 0);
            notSoFarAwayLayer.AddObject(mountain);

            Layer notSoFarAwayLayer2 = new Layer("notSoFarAwayLayer2", new Vector2(0.4f, 0.30f), 0.85f);
            this.Level.AddLayer(notSoFarAwayLayer2);

            mountain = new StaticObject(mountain1Texture, new Vector2(12, 0.0f + offset), 48, 13, new Color(255, 245, 240, 255), 0);
            notSoFarAwayLayer2.AddObject(mountain);

            Layer notSoFarAwayLayer3 = new Layer("notSoFarAwayLayer3", new Vector2(0.3f, 0.20f), 0.9f);
            this.Level.AddLayer(notSoFarAwayLayer3);
            mountain = new StaticObject(mountain2Texture, new Vector2(30, 3f + offset), 60, 15, new Color(255, 245, 230, 255), 0);
            notSoFarAwayLayer3.AddObject(mountain);
        }

        protected override void addLevelObjects()
        {
            Door door = new Door(new Vector2(0f, -6f), new Vector2(2f, 3f), Color.BurlyWood, this.Level, this.game, false);
            levelObjects.Add(door);
        }
    }
}
