using System;

namespace RoBuddies___Editor
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RoBuddiesEditor game = new RoBuddiesEditor())
            {
                game.Run();
            }
        }
    }
#endif
}

