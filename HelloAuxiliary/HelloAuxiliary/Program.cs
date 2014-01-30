using System;

namespace HelloAuxiliaryNamespace
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (AuxiliaryGame game = new AuxiliaryGame())
            {
                game.Run();
            }
        }
    }
#endif
}

