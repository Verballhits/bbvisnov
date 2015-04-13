using System;

namespace BBVisNov
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (VisNovGame game = new VisNovGame())
            {
                game.Run();
            }
        }
    }
#endif
}

