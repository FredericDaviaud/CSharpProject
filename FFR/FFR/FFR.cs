using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FFR.Utils;

namespace FFR
{
    public class FFR : Microsoft.Xna.Framework.Game
    {
        private ArrowReceptor arrowReceptor;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public FFR()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 780;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            arrowReceptor = new ArrowReceptor();
            arrowReceptor.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arrowReceptor.LoadContent(Content, "Arrows\\Receptor");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            arrowReceptor.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            arrowReceptor.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
