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
        /// <summary>
        /// Still abstract, but more concrete class of a specific level.
        /// Here you can add e.g. specific background art, which apply to
        /// all world 1 levels
        /// </summary>
        public World1Level(Game game, string LEVEL_PATH, LevelTheme LEVEL_THEME)
            : base(game, LEVEL_PATH, LEVEL_THEME)
        {
            Layer farAwayLayer = new Layer("farAwayLayer", new Vector2(0.5f, 0.1f), 0.9f);
            this.Level.AddLayer(farAwayLayer);
            Texture2D backgroundTexture = game.Content.Load<Texture2D>("Sprites//back_1");
            StaticObject background;
            for (int i = -2; i < 3; i++)
            {
                background = new StaticObject(backgroundTexture, new Vector2(i * 20, 0.1f), 20, 20, Color.White, 0);
                farAwayLayer.AddObject(background);
            }

            Layer notSoFarAwayLayer = new Layer("notSoFarAwayLayer", new Vector2(0.75f, 0.2f), 0.8f);
            //this.Level.AddLayer(notSoFarAwayLayer);
            Texture2D crateTexture = game.Content.Load<Texture2D>("Sprites//Crate3");
            StaticObject crate;
            for (int i = -2; i < 3; i++)
            {
                crate = new StaticObject(crateTexture, new Vector2(i * 5, -1f), 1, 1, Color.White, 0);
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
