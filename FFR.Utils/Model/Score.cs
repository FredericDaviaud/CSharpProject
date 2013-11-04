using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFR.Utils
{
    public class Score
    {
        public int PerfectCount { get; set; }
        public int GreatCount { get; set; }
        public int GoodCount { get; set; }
        public int MissCount { get; set; }
        public int Combo { get; set; }
        private SpriteFont score;

        public Score()
        {
            this.PerfectCount = 0;
            this.GreatCount = 0;
            this.GoodCount = 0;
            this.MissCount = 0;
            this.Combo = 0;
        }

        public Score(int perfectCount, int goodCount, int averageCount, int missCount)
        {
            this.PerfectCount = perfectCount;
            this.GreatCount = goodCount;
            this.GoodCount = averageCount;
            this.MissCount = missCount;
        }

        public void LoadContent(ContentManager content)
        {
            score = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(score, string.Format("{0}", getScoreValue()), new Vector2(140, 350), Color.White);
        }

        private int getScoreValue()
        {
            return PerfectCount * 50 + GreatCount * 25 + GoodCount * 10 - MissCount * 5;
        }
    }
}
