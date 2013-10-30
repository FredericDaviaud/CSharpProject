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
        public int GoodCount { get; set; }
        public int AverageCount { get; set; }
        public int MissCount { get; set; }
        public int Combo { get; set; }
        private SpriteFont font;

        public Score()
        {
            this.PerfectCount = 0;
            this.GoodCount = 0;
            this.AverageCount = 0;
            this.MissCount = 0;
            this.Combo = 0;
        }

        public Score(int perfectCount, int goodCount, int averageCount, int missCount)
        {
            this.PerfectCount = perfectCount;
            this.GoodCount = goodCount;
            this.AverageCount = averageCount;
            this.MissCount = missCount;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            font = content.Load<SpriteFont>("Spritefonts\\TotalArrows");
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
          //  spriteBatch.Draw(font, , Color.White);
        }

        private int getScoreValue()
        {
            return PerfectCount * 50 + GoodCount * 25 + AverageCount * 10 - MissCount * 5;
        }
    }
}
