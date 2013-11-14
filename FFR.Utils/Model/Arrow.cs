using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;

namespace FFR.Utils
{
    public class Arrow : Sprite
    {
        public float Angle { get; private set; }
        public float ArrowTime { get; private set; }
        public int ArrowRow { get; set; }
        public string ArrowColor { get; private set; }
        
        public bool isVisible = true;
        public bool isArrowHit = false;
        public bool isMissed = false;
        public const float ARROW_SPEED = 0.8f; // Until Options are added...
        

        public Arrow() 
        {
            this.ArrowRow = (int) Rows.Row1;
            this.ArrowColor = ArrowColors.Blue; 
            this.ArrowTime = -1.0f; 
        }

        public Arrow(string color, Rows row, float time)
        {
            this.ArrowColor = color;
            this.ArrowRow = (int) row;
            this.ArrowTime = time;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            Position = new Vector2(ArrowRow, int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]));
            Direction = Vector2.Normalize(new Vector2(0, 1));
            Speed = ARROW_SPEED;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="assetName">Name of the asset.</param>
        public override void LoadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        /// <summary>
        /// Updates the arrow sprite and check if it is visible or not
        /// </summary>
        /// <param name="gameTime">The game time.</param>
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gamerTime">The gamer time.</param>
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

        /// <summary>
        /// Method to rotate the arrow receptor sprite depending of the row
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        protected float arrowReceptorAngle(int row)
        {
            switch (row)
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
