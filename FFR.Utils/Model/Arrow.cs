using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;

namespace FFR.Utils
{
    public class Arrow : Sprite
    {
        public float Angle { get; private set; }
        public int ArrowRow { get; set; }
        public string ArrowColor { get; private set; }
        public float ArrowTime { get; private set; }
        public bool isVisible = true;
        public bool isArrowHit = false;
        

        public Arrow() 
        {
            this.ArrowRow = (int) Rows.Row1; //default row
            this.ArrowColor = ArrowColors.Blue; //default color
            this.ArrowTime = -1.0f; //default time
        }

        public Arrow(string color, Rows row, float time)
        {
            this.ArrowColor = color;
            this.ArrowRow = (int) row;
            this.ArrowTime = time;
        }

        public override void Initialize()
        {
            Position = new Vector2(ArrowRow, int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]));
            Direction = Vector2.Normalize(new Vector2(0, 1));
            Speed = 0.5f;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        public override void Update(GameTime gameTime)
        {
            if (!isArrowHit)
            {
                if (ArrowTime * 1000 > gameTime.TotalGameTime.TotalMilliseconds
                    || Position.Y + Texture.Height / 2 < 0) isVisible = false;
                else
                {
                    isVisible = true;
                    Position -= Direction * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
            else isVisible = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gamerTime)
        {
            if (isVisible)
            {
                var Origin = new Vector2()
                {
                    X = Texture.Width / 2,
                    Y = Texture.Height / 2
                };
                spriteBatch.Draw(Texture, Position, null, Color.White, arrowReceptorAngle(ArrowRow), Origin, 1.13f, SpriteEffects.None, 0f);

            }
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
