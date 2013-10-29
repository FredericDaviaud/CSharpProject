﻿using System;
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

        public Song()
        {
            this.Name = "Unknown";
            this.Artist = "Unknown";
            this.Length = -1;
            this.BestScore = new Score();
            this.ArrowList = null;
        }

        public Song(String name, String artist, int length, Score bestScore, List<Arrow> arrowlist)
        {
            this.Name = name;
            this.Artist = artist;
            this.Length = length;
            this.BestScore = bestScore;
            this.ArrowList = arrowlist;
        }

        public void ArrowMadnessTest()
        {
            Random rand = new Random();
            ArrowList = new List<Arrow>();
            for (float i = 0; i < 501; i++)
            {
                ArrowList.Add(new Arrow(randColor(rand.Next(8) + 1), randRow(rand.Next(4) + 1), i / 4));
            }
        }

        private String randColor(int i)
        {
            switch (i)
            {
                case 1: return ArrowColors.Blue;
                case 2: return ArrowColors.Cyan;
                case 3: return ArrowColors.Green;
                case 4: return ArrowColors.Orange;
                case 5: return ArrowColors.Pink;
                case 6: return ArrowColors.Purple;
                case 7: return ArrowColors.Red;
                case 8: return ArrowColors.Yellow;
                default: return ArrowColors.Blue;
            }
        }

        private Rows randRow(int i)
        {
            switch (i)
            {
                case 1: return Rows.Row1;
                case 2: return Rows.Row2;
                case 3: return Rows.Row3;
                case 4: return Rows.Row4;
                default: return Rows.Row1;
            }
        }
    }
}