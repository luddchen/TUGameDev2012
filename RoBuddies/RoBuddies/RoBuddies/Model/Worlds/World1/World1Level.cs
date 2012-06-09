using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Worlds.World1
{
    /// <summary>
    /// Still abstract, but more concrete class of a specific level.
    /// Here you can add e.g. specific background art, which apply to
    /// all world 1 levels
    /// </summary>
    abstract class World1Level : WorldLevel
    {

        public World1Level(Game game, string LEVEL_PATH, LevelTheme LEVEL_THEME)
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
            
        }

        protected override void addBackground()
        {

        }
    }
}
