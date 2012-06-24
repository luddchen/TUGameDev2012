using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Worlds.World2
{
    /// <summary>
    /// Still abstract, but more concrete class of a specific level.
    /// Here you can add e.g. specific background art, which apply to
    /// all world 2 levels
    /// </summary>
    abstract class World2Level : WorldLevel
    {
        /// <summary>
        /// Still abstract, but more concrete class of a specific level.
        /// Here you can add e.g. specific background art, which apply to
        /// all world 1 levels
        /// </summary>
        public World2Level(Game game, string LEVEL_PATH, LevelTheme LEVEL_THEME, String LEVEL_NAME)
            : base(game, LEVEL_PATH, LEVEL_THEME, LEVEL_NAME)
        {
            this.Level.Background = Color.SkyBlue;
        }

        protected override void addBackground()
        {
        }

        protected void addSky(Vector2 offset)
        {
            Layer sunLayer = new Layer("sunLayer", new Vector2(0.05f, 0.25f), 0.99f);
            this.Level.AddLayer(sunLayer);

            Texture2D sunTexture = game.Content.Load<Texture2D>("Sprites//Mountain//sun1");
            StaticObject sun = new StaticObject(sunTexture, new Vector2(-3, 4) + offset, 2.5f, 2.5f, Color.White, 0);
            sunLayer.AddObject(sun);

            Layer farAwayLayer = new Layer("farAwayLayer", new Vector2(0.2f, 0.15f), 0.95f);
            this.Level.AddLayer(farAwayLayer);
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("Sprites//Mountain//clouds_continuous");
            StaticObject background;
            for (int i = 0; i < 3; i++)
            {
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, -2) + offset, 40, 8, Color.White, 0);
                farAwayLayer.AddObject(background);
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, 6) + offset, 40, 8, Color.White, 0);
                background.Effect = SpriteEffects.FlipVertically;
                farAwayLayer.AddObject(background);
                background = new StaticObject(backgroundTexture, new Vector2(i * 40, 14) + offset, 40, 8, Color.White, 0);
                farAwayLayer.AddObject(background);
            }
        }

        protected void addMountains(Vector2 offset)
        {
            Layer notSoFarAwayLayer = new Layer("notSoFarAwayLayer", new Vector2(0.5f, 0.4f), 0.8f);
            this.Level.AddLayer(notSoFarAwayLayer);
            Texture2D mountain1Texture = game.Content.Load<Texture2D>("Sprites//Mountain//dark_mountain1");
            Texture2D mountain2Texture = game.Content.Load<Texture2D>("Sprites//Mountain//light_mountain1");
            StaticObject mountain;

            mountain = new StaticObject(mountain2Texture, new Vector2(0, -2f) + offset, 40, 10, new Color(245, 255, 240, 255), 0);
            notSoFarAwayLayer.AddObject(mountain);
            mountain = new StaticObject(mountain2Texture, new Vector2(18, -3f) + offset, 40, 8, new Color(245, 255, 240, 255), 0);
            notSoFarAwayLayer.AddObject(mountain);

            Layer notSoFarAwayLayer2 = new Layer("notSoFarAwayLayer2", new Vector2(0.4f, 0.30f), 0.85f);
            this.Level.AddLayer(notSoFarAwayLayer2);

            mountain = new StaticObject(mountain1Texture, new Vector2(12, 0.0f) + offset, 48, 13, new Color(255, 245, 240, 255), 0);
            notSoFarAwayLayer2.AddObject(mountain);

            Layer notSoFarAwayLayer3 = new Layer("notSoFarAwayLayer3", new Vector2(0.3f, 0.20f), 0.9f);
            this.Level.AddLayer(notSoFarAwayLayer3);
            mountain = new StaticObject(mountain2Texture, new Vector2(30, 3f) + offset, 60, 15, new Color(255, 245, 230, 255), 0);
            notSoFarAwayLayer3.AddObject(mountain);
        }

        protected override void addLevelObjects()
        {

        }
    }
}
