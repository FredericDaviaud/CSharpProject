using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Configuration;

namespace FFR.Utils
{
    public class Judge: Sprite
    {
        public Accuracy ArrowAccuracy { get; set; }
        public bool isVisible = false;
        private int animationTimer = 0;

        /// <summary>
        /// Updates the judge sprite.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="row">The row.</param>
        public void Update(Song song, Rows row)
        {
            Arrow nextArrow = song.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return ((arrow.ArrowRow == (int)row) 
                        && (arrow.Position.Y >= 0) 
                        && (arrow.Position.Y <= 149) 
                        && arrow.isVisible);
                }
                );

            try
            {
                if (nextArrow != null)
                {
                    checkIfArrowHit(song, nextArrow);
                    if (ArrowAccuracy != Accuracy.Miss)
                    {
                        animationTimer = 0;
                        isVisible = true;
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Check if an arrow as been missed and updates the game accordingly.
        /// </summary>
        /// <param name="song">The song.</param>
        public void UpdateMiss(Song song)
        {
            
            Arrow nextArrow = song.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return (((int) arrow.Position.Y < 0) 
                        && arrow.isMissed == false
                        && arrow.isVisible == true);
                }
                );
            try
            {
                if (nextArrow != null)
                {
                    animationTimer = 0;
                    isVisible = true;
                    ArrowAccuracy = Accuracy.Miss;
                    nextArrow.isMissed = true;
                    song.Combo = 0;
                    song.Score.MissCount += 1;
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Make the judge disapear after a fixed period of time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateSpriteTimer(GameTime gameTime)
        {
            animationTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (animationTimer >= 100)
            {
                isVisible = false;
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gamerTime">The gamer time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            if (isVisible)
            {
                Rectangle sourceRectangle = new Rectangle(0, getAccuracySprite(), 168, 28);

                Origin = new Vector2()
                {
                    X = sourceRectangle.Width / 2,
                    Y = sourceRectangle.Height / 2
                };

                spriteBatch.Draw(Texture, new Vector2(int.Parse(ConfigurationManager.AppSettings["WINDOW_WIDTH"]) / 2,
                    int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]) / 2), sourceRectangle, Color.White, 0, Origin, 1.13f, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Gets the accuracy sprite (all those accuracies are on one unique sprite that need to be cut)
        /// </summary>
        /// <returns>The Y value in pixel where the sprite needs to be cut</returns>
        private int getAccuracySprite()
        {
            switch (ArrowAccuracy)
            {
                case Accuracy.Perfect: return 28;
                case Accuracy.Great: return 56;
                case Accuracy.Good: return 84;
                case Accuracy.Miss: return 140;
                default: return 140;
            }
        }

        /// <summary>
        /// Checks if an arrow has been hit, and if it's the case, with which accuracy.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="nextArrow">The next arrow.</param>
        private void checkIfArrowHit(Song song, Arrow nextArrow)
        {
            if (45 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 125)
            {
                ArrowAccuracy = Accuracy.Perfect;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
                song.Score.PerfectCount += 1;
            }
            else if ((126 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 150)
                || (20 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 44))
            {
                ArrowAccuracy = Accuracy.Great;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
                song.Score.GreatCount += 1;
            }
            else if ((151 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 175)
                || (0 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 19))
            {
                ArrowAccuracy = Accuracy.Good;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
                song.Score.GoodCount += 1;
            }
        }
    }
}
