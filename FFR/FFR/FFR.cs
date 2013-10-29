using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FFR.Utils;
using System.Configuration;

namespace FFR
{
    public class FFR : Microsoft.Xna.Framework.Game
    {
        private ArrowReceptor arrowReceptor;
        private Arrow arrowTest;
        private Song arrowMadnessTest;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public FFR()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferHeight = int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]);
            graphics.PreferredBackBufferWidth = int.Parse(ConfigurationManager.AppSettings["WINDOW_WIDTH"]);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            arrowReceptor = new ArrowReceptor();
            arrowMadnessTest = new Song();
            arrowMadnessTest.ArrowMadnessTest();
            //arrowTest = new Arrow(ArrowColors.Yellow, Rows.Row1, 1);
            arrowReceptor.Initialize();
            //arrowTest.Initialize();
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Initialize();
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arrowReceptor.LoadContent(Content, "Arrows\\Receptor");
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.LoadContent(Content, arrow.ArrowColor.ToString());
            }
            //arrowTest.LoadContent(Content, arrowTest.ArrowColor.ToString());
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Update(gameTime);
            }
                //arrowTest.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            arrowReceptor.Draw(spriteBatch, gameTime);
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Draw(spriteBatch, gameTime);
            }
            //arrowTest.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
