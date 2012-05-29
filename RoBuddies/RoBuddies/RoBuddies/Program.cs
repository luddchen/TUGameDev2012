using System;

namespace RoBuddies
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
        {
            using (RoBuddies game = new RoBuddies())
            {
                game.Run();
            }
        }
    }
#endif
}

