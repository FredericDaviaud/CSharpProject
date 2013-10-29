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
                arrowReceptorLeft.isKeyHit = judge.IsKeyHit = true;
                Arrow nextArrow = arrowMadnessTest.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return ((arrow.ArrowRow == (int) Rows.Row1) && (arrow.isVisible == true));
                }
                );
                judge.Update(gameTime, nextArrow);
            }
            if (oldState.IsKeyUp(Keys.Down) && newState.IsKeyDown(Keys.Down))
            {
                arrowReceptorDown.isKeyHit = judge.IsKeyHit = true;
                Arrow nextArrow = arrowMadnessTest.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return ((arrow.ArrowRow == (int)Rows.Row2) && (arrow.isVisible == true));
                }
                );
                judge.Update(gameTime, nextArrow);
            }
            if (oldState.IsKeyUp(Keys.Up) && newState.IsKeyDown(Keys.Up))
            {
                arrowReceptorUp.isKeyHit = judge.IsKeyHit = true;
                Arrow nextArrow = arrowMadnessTest.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return ((arrow.ArrowRow == (int)Rows.Row3) && (arrow.isVisible == true));
                }
                );
                judge.Update(gameTime, nextArrow);
            }
            if (oldState.IsKeyUp(Keys.Right) && newState.IsKeyDown(Keys.Right))
            {
                arrowReceptorRight.isKeyHit = judge.IsKeyHit = true;
                Arrow nextArrow = arrowMadnessTest.ArrowList.Find(
                delegate(Arrow arrow)
                {
                    return ((arrow.ArrowRow == (int)Rows.Row4) && (arrow.isVisible == true));
                }
                );
                judge.Update(gameTime, nextArrow);
            }

            arrowReceptorLeft.Update(gameTime);
            arrowReceptorDown.Update(gameTime);
            arrowReceptorUp.Update(gameTime);
            arrowReceptorRight.Update(gameTime);
            judge.Update(gameTime, null);

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
            foreach (Arrow arrow in arrowMadnessTest.ArrowList)
            {
                arrow.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
