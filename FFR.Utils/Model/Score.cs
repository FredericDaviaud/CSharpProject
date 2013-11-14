using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public void LoadContent(ContentManager content)
        {
            score = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(score, string.Format("{0}", getScoreValue()), new Vector2(140, 350), Color.White);
        }

        /// <summary>
        /// Gets the score value.
        /// </summary>
        /// <returns>The actual score</returns>
        private int getScoreValue()
        {
            return PerfectCount * 50 + GreatCount * 25 + GoodCount * 10 - MissCount * 5;
        }
    }
}
