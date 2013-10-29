using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FFR.Utils
{
    public class ArrowReceptor : Arrow
    {
        private int currentFrame;
        private int lastFrame = 0;
        public bool isKeyHit { get; set; }

        public ArrowReceptor() 
        {
            currentFrame = 5;
        }

        public ArrowReceptor(Rows arrowRow) 
        {
            this.ArrowRow = (int) arrowRow;
            currentFrame = 5;
        }

        public override void Update(GameTime gameTime)
        {
            if (isKeyHit)
            {
                currentFrame--;
                if (currentFrame == lastFrame)
                {
                    currentFrame = 5;
                    isKeyHit = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * 143, 0, 143, 143);

            Origin = new Vector2()
            {
                X = sourceRectangle.Width / 2,
                Y = sourceRectangle.Height / 2
            };

            spriteBatch.Draw(Texture, new Vector2(ArrowRow, 85), sourceRectangle, Color.White, base.arrowReceptorAngle(ArrowRow), Origin, 1.13f, SpriteEffects.None, 0f);
        }
    }
}
