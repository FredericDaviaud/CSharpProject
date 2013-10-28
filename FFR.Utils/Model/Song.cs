using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFR.Utils
{
    public class Song
    {
        public String Name { get; private set; }
        public String Artist { get; private set; }
        public int Length { get; private set; }
        public Score BestScore { get; private set; }
        public List<Arrow> ArrowList { get; private set; }

        public Song(String name, String artist, int length, Score bestScore, List<Arrow> arrowlist)
        {
            this.Name = name;
            this.Artist = artist;
            this.Length = length;
            this.BestScore = bestScore;
            this.ArrowList = arrowlist;
        }
    }
}
