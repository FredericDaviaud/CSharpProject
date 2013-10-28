using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;

namespace FFR.Utils
{
    public class Arrow : Sprite
    {
        public float Angle { get; private set; }
        public int ArrowRow { get; private set; }
        public string ArrowColor { get; private set; }

        public Arrow() 
        {
            this.ArrowRow = (int) Rows.Row1; //default row
            this.ArrowColor = ArrowColors.Blue; //default color
        }

        public Arrow(string color, Rows row)
        {
            this.ArrowColor = color;
            this.ArrowRow = (int) row;
        }

        public override void Initialize()
        {
            Position = new Vector2(ArrowRow, int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]));
            Direction = Vector2.Normalize(new Vector2(0, 1));
            Speed = 0.2f;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        public override void Update(GameTime gameTime)
        {
            Position -= Direction * Speed * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            var Origin = new Vector2()
            {
                X = Texture.Width / 2,
                Y = Texture.Height / 2
            };
            spriteBatch.Draw(Texture, Position, null, Color.White, arrowReceptorAngle(ArrowRow), Origin, 1.13f, SpriteEffects.None, 0f);
        }

        protected float arrowReceptorAngle(int i)
        {
            switch (i)
            {
                case (int) Rows.Row1: return -MathHelper.Pi / 2;
                case (int) Rows.Row2: return MathHelper.Pi;
                case (int) Rows.Row3: return 0;
                case (int) Rows.Row4: return MathHelper.Pi / 2;
                default: return MathHelper.Pi / 2;
            }
        }
    }
}
