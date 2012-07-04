using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RoBuddies.Utilities
{
    /// <summary>
    /// This class is an utility for saving and loading games from the save.bin file
    /// </summary>
    class SaveGameUtility
    {

        /// <summary>
        /// This method saves the new level index to the save.bin file,
        /// id the new level index is higher than the old level index in the file
        /// </summary>
        /// <param name="levelIndex">the new level index</param>
        public static void saveIfHigher(int levelIndex)
        {
            if (loadGame() < levelIndex)
            {
                saveGame(levelIndex);
            }
        }

        /// <summary>
        /// Saves the game to the save.bin file
        /// </summary>
        /// <param name="levelIndex">the level index which will be saved</param>
        public static void saveGame(int levelIndex)
        {
                BinaryWriter bw = new BinaryWriter(File.Open(".\\save.bin", FileMode.Create));
                bw.Write(levelIndex);
                bw.Flush();
                bw.Close();
        }

        /// <summary>
        /// This method loads the level index from the save.bin file
        /// </summary>
        /// <returns>the level index from the save.bin file</returns>
        public static int loadGame()
        {
            int levelIndex = 0;
            if (File.Exists(@".\\save.bin"))
            {
                BinaryReader br = new BinaryReader(File.Open(".\\save.bin", FileMode.Open));
                levelIndex = br.ReadInt32();
                br.Close();
            }
            return levelIndex;
        }
    }
}
