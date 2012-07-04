using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoBuddies.Model.Worlds.Tutorial;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Model.Worlds.MountainLevel;
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
        /// the level index of the current level
        /// </summary>
        public int currentLevelIndex;

        /// <summary>
        /// Creates a new worlds object, which can return the next level
        /// </summary>
        /// <param name="game">The game for the levels</param>
        public Worlds(Game game)
        {
            this.game = game;
            worlds = new Queue<WorldLevel>();
            addWorlds();
            
        }

        private void addWorlds()
        {
            addTutorial();
            addWorldMountain();
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
            worlds.Enqueue(new Tutorial_5(this.game));
            worlds.Enqueue(new Tutorial_6(this.game));
            worlds.Enqueue(new Tutorial_7(this.game));
            
        }

        /// <summary>
        /// add here the levels of world 1. The order of adding is the order of playing the levels.
        /// </summary>
        private void addWorld1()
        {
            worlds.Enqueue(new Level1_1(this.game));
            //worlds.Enqueue(new Level1_2(this.game));
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


        private void addWorldMountain()
        {
            worlds.Enqueue(new Mountain_1(this.game));
            worlds.Enqueue(new Mountain_2(this.game));
            worlds.Enqueue(new Mountain_3(this.game));
        }

        /// <summary>
        /// This method returns the next level
        /// </summary>
        /// <returns>the next level</returns>
        public Level getNextLevel()
        {
            Level nextLevel = null;
            if (++currentLevelIndex <= worlds.Count) {
                nextLevel = worlds.ElementAt<WorldLevel>(currentLevelIndex).Level;
            }
            return nextLevel;
        }

        /// <summary>
        /// set the current level to level with the specified index
        /// </summary>
        /// <param name="levelIndex">the index of the level</param>
        /// <returns>the level with the index</returns>
        public Level setLevel(int levelIndex)
        {
            Level level = null;
            currentLevelIndex = levelIndex;
            if (currentLevelIndex <= worlds.Count)
            {
                level = worlds.ElementAt<WorldLevel>(levelIndex).Level;
            }
            return level;
        }

        /// <summary>
        /// this method sets the level to a specific level of the worlds queue
        /// </summary>
        /// <param name="level">an level from the world queue</param>
        public void setLevel(Level level)
        {
            for (int levelIndex = 0; levelIndex < worlds.Count; levelIndex++ )
            {
                if (worlds.ElementAt<WorldLevel>(levelIndex).Level.LevelName == level.LevelName)
                {
                    this.currentLevelIndex = levelIndex;
                }
            }
        }

    }
}
