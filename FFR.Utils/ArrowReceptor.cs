using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FFR.Utils
{
    public class ArrowReceptor : Arrow
    {
        public ArrowReceptor() { }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            Origin = new Vector2()
            {
                X = Texture.Width / 2,
                Y = Texture.Height / 2
            };

            for (int i = 265; i < 563; i += 82)
            {
                spriteBatch.Draw(Texture, new Vector2(i, 85), null, Color.White, base.arrowReceptorAngle(i), Origin, 1.13f, SpriteEffects.None, 0f);
            }
        }
    }
}
