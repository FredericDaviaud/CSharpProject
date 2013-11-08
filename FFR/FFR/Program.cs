using FFR.Parser;
using System;

namespace FFR
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (Engine game = new Engine())
            {
                game.Run();
                //SongParser parser = new SongParser();
                //parser.parse("C:\\Users\\Fred\\Documents\\Visual Studio 2012\\Projects\\FFRParser\\giga.sm");
            }
        }
    }
#endif
}

