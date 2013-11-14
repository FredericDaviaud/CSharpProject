using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FFR.Utils;
using System.Configuration;
using FFR.Parser;

namespace FFR
{
    /// <summary>
    /// The game engine class
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        private ArrowReceptor arrowReceptorLeft;
        private ArrowReceptor arrowReceptorDown;
        private ArrowReceptor arrowReceptorUp;
        private ArrowReceptor arrowReceptorRight;
        private Song song;
        private Judge judge;
        private KeyboardState oldState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
         
        public const Keys KEY_LEFT =  Keys.S; // Until Options are added...
        public const Keys KEY_DOWN =  Keys.D; // Until Options are added...
        public const Keys KEY_UP =    Keys.J; // Until Options are added...
        public const Keys KEY_RIGHT = Keys.K; // Until Options are added...

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferHeight = int.Parse(ConfigurationManager.AppSettings["WINDOW_HEIGHT"]);
            graphics.PreferredBackBufferWidth = int.Parse(ConfigurationManager.AppSettings["WINDOW_WIDTH"]);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Initialize the content needed for the game (sprites, songs, fonts, etc...)
        /// </summary>
        protected override void Initialize()
        {
            arrowReceptorLeft = new ArrowReceptor(Rows.Row1);
            arrowReceptorDown = new ArrowReceptor(Rows.Row2);
            arrowReceptorUp = new ArrowReceptor(Rows.Row3);
            arrowReceptorRight = new ArrowReceptor(Rows.Row4);
            song = new Song();
            judge = new Judge();

            arrowReceptorLeft.Initialize();
            arrowReceptorDown.Initialize();
            arrowReceptorUp.Initialize();
            arrowReceptorRight.Initialize();
            song.Initialize();
            judge.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Load the content needed for the game (sprites, songs, fonts, etc...)
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arrowReceptorLeft.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorDown.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorUp.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            arrowReceptorRight.LoadContent(Content, "Animation\\ArrowReceptorSheet");
            judge.LoadContent(Content, "Judge\\Judge");
            song.LoadContent(Content);
        }

        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Update the content, and check for user input
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (oldState.IsKeyUp(KEY_LEFT) && newState.IsKeyDown(KEY_LEFT))
            {
                arrowReceptorLeft.isKeyHit = true;
                judge.Update(song, Rows.Row1);
            }
            if (oldState.IsKeyUp(KEY_DOWN) && newState.IsKeyDown(KEY_DOWN))
            {
                arrowReceptorDown.isKeyHit = true;
                judge.Update(song, Rows.Row2);
            }
            if (oldState.IsKeyUp(KEY_UP) && newState.IsKeyDown(KEY_UP))
            {
                arrowReceptorUp.isKeyHit = true;
                judge.Update(song, Rows.Row3);
            }
            if (oldState.IsKeyUp(KEY_RIGHT) && newState.IsKeyDown(KEY_RIGHT))
            {
                arrowReceptorRight.isKeyHit = true;
                judge.Update(song, Rows.Row4);
            }

            arrowReceptorLeft.Update(gameTime);
            arrowReceptorDown.Update(gameTime);
            arrowReceptorUp.Update(gameTime);
            arrowReceptorRight.Update(gameTime);
            judge.UpdateMiss(song);
            judge.UpdateSpriteTimer(gameTime);
            song.Update(gameTime);

            oldState = newState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the game content (sprites, fonts, etc...)
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            arrowReceptorLeft.Draw(spriteBatch, gameTime);
            arrowReceptorDown.Draw(spriteBatch, gameTime);
            arrowReceptorUp.Draw(spriteBatch, gameTime);
            arrowReceptorRight.Draw(spriteBatch, gameTime);
            judge.Draw(spriteBatch, gameTime);
            song.Draw(spriteBatch, gameTime);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
