using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace FFR.Utils
{
    public class Song
    {
        public String Name { get; private set; }
        public String Artist { get; private set; }
        public int Length { get; private set; }
        public Score Score { get; private set; }
        public List<Arrow> ArrowList { get; private set; }
        public int Combo { get; set; }
        public String Music { get; set; } //temp
        private SpriteFont totalArrows;
        private SpriteFont combo;

        public Song()
        {
            this.Name = "Unknown";
            this.Artist = "Unknown";
            this.Length = -1;
            this.Score = new Score();
            this.ArrowList = null;
            this.Music = "";
        }

        public Song(String name, String artist, int length, Score bestScore, List<Arrow> arrowlist, String song)
        {
            this.Name = name;
            this.Artist = artist;
            this.Length = length;
            this.Score = bestScore;
            this.ArrowList = arrowlist;
            this.Music = song;
        }

        public void LoadContent(ContentManager content)
        {
            try
            {
                totalArrows = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
                combo = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
                this.Score.LoadContent(content);
                Microsoft.Xna.Framework.Media.Song song = content.Load<Microsoft.Xna.Framework.Media.Song>(Music);
                MediaPlayer.Play(song);
            }
            catch (Exception) { }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(totalArrows, string.Format("{0}", ArrowList.Count), new Vector2(600, 420), Color.White);
            spriteBatch.DrawString(combo, string.Format("{0}", Combo), new Vector2(140, 420), Color.White);
            this.Score.Draw(spriteBatch);
        }

        public void ArrowMadnessTest() //test method
        {
            Random rand = new Random();
            ArrowList = new List<Arrow>();
            for (float i = 0; i < 500; i++)
            {
                ArrowList.Add(new Arrow(randColor(rand.Next(8) + 1), randRow(rand.Next(4) + 1), i / 4));
            }
        }

        private String randColor(int i) //test method
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

        private Rows randRow(int i) //test method
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
