using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FFR.Utils
{
    public class ArrowReceptor : Sprite
    {
        public Vector2 Origin { get; private set; }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            Origin = new Vector2()
            {
                X = Texture.Width / 2,
                Y = Texture.Height / 2
            };

            for (int i = 265; i < 563; i += 82)
            {
                spriteBatch.Draw(Texture, new Vector2(i, 85), null, Color.White, arrowReceptorAngle(i), Origin, 1.13f, SpriteEffects.None, 0f);
            }
        }

        private float arrowReceptorAngle(int i)
        {
            switch (i)
            {
                case (int) Row.Row1: return -MathHelper.Pi / 2;
                case (int) Row.Row2: return MathHelper.Pi;
                case (int) Row.Row3: return 0;
                case (int) Row.Row4: return  MathHelper.Pi / 2;
                default: return   MathHelper.Pi / 2;
            }
        }
    }
}
