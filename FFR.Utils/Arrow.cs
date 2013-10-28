using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FFR.Utils
{
    public class Arrow : Sprite
    {
        public float Angle { get; private set; }
        public int ArrowRow { get; private set; }
        public string ArrowColor { get; private set; }

        public Arrow() { }

        public Arrow(string color, Row row)
        {
            this.ArrowColor = color;
            this.ArrowRow = (int) row;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            var Origin = new Vector2()
            {
                X = Texture.Width / 2,
                Y = Texture.Height / 2
            };
            spriteBatch.Draw(Texture, new Vector2(ArrowRow, 85), null, Color.White, arrowReceptorAngle(ArrowRow), Origin, 1.13f, SpriteEffects.None, 0f);
        }

        protected float arrowReceptorAngle(int i)
        {
            switch (i)
            {
                case (int) Row.Row1: return -MathHelper.Pi / 2;
                case (int) Row.Row2: return MathHelper.Pi;
                case (int) Row.Row3: return 0;
                case (int) Row.Row4: return MathHelper.Pi / 2;
                default: return MathHelper.Pi / 2;
            }
        }
    }
}
