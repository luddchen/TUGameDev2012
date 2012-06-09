using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RoBuddies.Model.Serializer;

namespace RoBuddies.Model.Worlds.World1
{
    class Level1_1
    {
        private const string LEVEL_PATH = "Worlds\\World1\\Level1_1.json";
        private const LevelTheme theme = LevelTheme.MENTAL_HOSPITAL;
        private Level level;

        /// <summary>
        /// Getter for the loaded level
        /// </summary>
        public Level Level
        {
            get { return this.level; }
        }

        public Level1_1(Game game)
        {
            LevelReader levelReader = new LevelReader(game);
            this.level = levelReader.readLevel(".\\" + game.Content.RootDirectory, LEVEL_PATH);
        }
    }
}
