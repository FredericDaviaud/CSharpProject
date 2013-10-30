using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FFR.Utils
{
    public class Judge: Sprite
    {
        public Accuracy ArrowAccuracy { get; set; }
        public bool isVisible = false;
        private int animationTimer = 0;

        public override void Update(GameTime gameTime)
        {
            animationTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (animationTimer >= 100)
            {
                isVisible = false;
            }
        }

        public void Update(Song song, Rows row)
        {
            animationTimer = 0;
            isVisible = true;

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
                }
            }
            catch (Exception) { }

        }

        public void Update(Song song)
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
                }
            }
            catch (Exception) { }
        }

        

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

        private void checkIfArrowHit(Song song, Arrow nextArrow)
        {
            if (65 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 105)
            {
                ArrowAccuracy = Accuracy.Perfect;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
            }
            else if ((106 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 125)
                || (31 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 64))
            {
                ArrowAccuracy = Accuracy.Great;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
            }
            else if ((126 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 149)
                || (0 <= (int) nextArrow.Position.Y && (int) nextArrow.Position.Y <= 30))
            {
                ArrowAccuracy = Accuracy.Good;
                nextArrow.isArrowHit = true;
                song.Combo += 1;
            }
        }
    }
}
