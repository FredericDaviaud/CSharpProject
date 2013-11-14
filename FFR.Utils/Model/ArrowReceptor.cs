using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FFR.Utils
{
    public class ArrowReceptor : Arrow
    {
        private int currentFrame = 5;
        private int lastFrame = 0;
        private int animationTimer = 0;
        public bool isKeyHit { get; set; }

        public ArrowReceptor() { }

        public ArrowReceptor(Rows arrowRow) 
        {
            this.ArrowRow = (int) arrowRow;
            currentFrame = 5;
        }

        /// <summary>
        /// Updates the arrow receptor sprite and handles the animation when key is hit
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            animationTimer += (int) gameTime.ElapsedGameTime.Ticks;
            if (isKeyHit && animationTimer >= 200000)
            {
                currentFrame--; 
                animationTimer = 0;
                if (currentFrame == lastFrame)
                {
                    currentFrame = 5;
                    isKeyHit = false;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
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
