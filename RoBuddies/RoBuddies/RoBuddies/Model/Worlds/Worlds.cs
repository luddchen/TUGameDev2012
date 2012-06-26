using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model.Worlds.Tutorial;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Worlds
{
    /// <summary>
    /// This class is for creating the order of the levels
    /// </summary>
    class Worlds
    {

        private Game game;
        private Queue<WorldLevel> worlds;

        /// <summary>
        /// Creates a new worlds object, which can return the next level
        /// </summary>
        /// <param name="game">The game for the levels</param>
        public Worlds(Game game)
        {
            this.game = game;
            worlds = new Queue<WorldLevel>();
            addTutorial();
            addWorld1();            
            addWorld2();
            addWorld3();
        }

        /// <summary>
        /// add here the levels of world 1. The order of adding is the order of playing the levels.
        /// </summary>
        private void addTutorial()
        {
            worlds.Enqueue(new Tutorial_1(this.game));
            worlds.Enqueue(new Tutorial_2(this.game));
            worlds.Enqueue(new Tutorial_3(this.game));
            worlds.Enqueue(new Tutorial_4(this.game));
        }

        /// <summary>
        /// add here the levels of world 1. The order of adding is the order of playing the levels.
        /// </summary>
        private void addWorld1()
        {
            worlds.Enqueue(new Level1_1(this.game));
            worlds.Enqueue(new Level1_2(this.game));
            worlds.Enqueue(new Level1_3(this.game));
        }

        /// <summary>
        /// add here the levels of world 2. The order of adding is the order of playing the levels.
        /// </summary>
        private void addWorld2()
        {
            worlds.Enqueue(new Level2_5(this.game));
            worlds.Enqueue(new Level2_1(this.game));
        }

        /// <summary>
        /// add here the levels of world 3. The order of adding is the order of playing the levels.
        /// </summary>
        private void addWorld3()
        {

        }

        /// <summary>
        /// This method returns the next level
        /// </summary>
        /// <returns>the next level</returns>
        public Level getNextLevel()
        {
            Level nextLevel = null;
            if (worlds.Count > 0) {
                nextLevel = worlds.Dequeue().Level;
            }
            return nextLevel;
        }
    }
}
