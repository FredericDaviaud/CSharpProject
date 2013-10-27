using System;

namespace FFR
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (FFR game = new FFR())
            {
                game.Run();
            }
        }
    }
#endif
}

