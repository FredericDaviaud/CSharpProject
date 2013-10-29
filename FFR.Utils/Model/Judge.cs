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


        public void Update(GameTime gameTime, Arrow nextArrow)
        {
            isVisible = true;
            try
            {
                if (65 <= (int)nextArrow.Position.Y && (int)nextArrow.Position.Y <= 105)
                {
                    ArrowAccuracy = Accuracy.Perfect;
                    nextArrow.isArrowHit = true;
                }
                else if ((106 <= (int)nextArrow.Position.Y && (int)nextArrow.Position.Y <= 125)
                    || (45 <= (int)nextArrow.Position.Y && (int)nextArrow.Position.Y <= 64))
                {
                    ArrowAccuracy = Accuracy.Great;
                    nextArrow.isArrowHit = true;
                }
                else if ((126 <= (int)nextArrow.Position.Y && (int)nextArrow.Position.Y <= 149)
                    || (21 <= (int)nextArrow.Position.Y && (int)nextArrow.Position.Y <= 44))
                {
                    ArrowAccuracy = Accuracy.Good;
                    nextArrow.isArrowHit = true;
                }
            } catch(Exception) { }
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
    }
}
