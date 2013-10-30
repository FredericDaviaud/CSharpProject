using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FFR.Utils;
using System.Configuration;

namespace FFR
{
    public class FFR : Microsoft.Xna.Framework.Game
    {
        private ArrowReceptor arrowReceptorLeft;
        private ArrowReceptor arrowReceptorDown;
        private ArrowReceptor arrowReceptorUp;
        private ArrowReceptor arrowReceptorRight;
        private Song arrowMadnessTest;
        private Judge judge;
        private KeyboardState oldState;
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
            arrowReceptorLeft = new ArrowReceptor(Rows.Row1);
            arrowReceptorDown = new ArrowReceptor(Rows.Row2);
            arrowReceptorUp = new ArrowReceptor(Rows.Row3);
            arrowReceptorRight = new ArrowReceptor(Rows.Row4);
            arrowMadnessTest = new Song();
            judge = new Judge();

            arrowReceptorLeft.Initialize();
            arrowReceptorDown.Initialize();
            arrowReceptorUp.Initialize();
            arrowReceptorRight.Initialize();
            arrowMadnessTest.ArrowMadnessTest();
            judge.Initialize();
            //arrowMadnessTest.Music = "Songs\\Nyan";
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Initialize();
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arrowReceptorLeft.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorDown.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorUp.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorRight.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            judge.LoadContent(Content, "Judge\\Judge");
            arrowMadnessTest.LoadContent(Content);
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.LoadContent(Content, arrow.ArrowColor.ToString());
            }
            arrowMadnessTest.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.Left) && newState.IsKeyDown(Keys.Left))
            {
                arrowReceptorLeft.isKeyHit = true;
                judge.Update(arrowMadnessTest, Rows.Row1);
            }
            if (oldState.IsKeyUp(Keys.Down) && newState.IsKeyDown(Keys.Down))
            {
                arrowReceptorDown.isKeyHit = true;
                judge.Update(arrowMadnessTest, Rows.Row2);
            }
            if (oldState.IsKeyUp(Keys.Up) && newState.IsKeyDown(Keys.Up))
            {
                arrowReceptorUp.isKeyHit = true;
                judge.Update(arrowMadnessTest, Rows.Row3);
            }
            if (oldState.IsKeyUp(Keys.Right) && newState.IsKeyDown(Keys.Right))
            {
                arrowReceptorRight.isKeyHit = true;
                judge.Update(arrowMadnessTest, Rows.Row4);
            }

            arrowReceptorLeft.Update(gameTime);
            arrowReceptorDown.Update(gameTime);
            arrowReceptorUp.Update(gameTime);
            arrowReceptorRight.Update(gameTime);
            judge.Update(arrowMadnessTest);
            judge.Update(gameTime);

            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Update(gameTime);
            }

            oldState = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            arrowReceptorLeft.Draw(spriteBatch, gameTime);
            arrowReceptorDown.Draw(spriteBatch, gameTime);
            arrowReceptorUp.Draw(spriteBatch, gameTime);
            arrowReceptorRight.Draw(spriteBatch, gameTime);
            judge.Draw(spriteBatch, gameTime);
            arrowMadnessTest.Draw(spriteBatch);
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
