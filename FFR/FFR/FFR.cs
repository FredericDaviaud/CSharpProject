using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FFR
{
    public class FFR : Microsoft.Xna.Framework.Game
    {
        private Texture2D arrowReceptor;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public FFR()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 780;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            arrowReceptor = Content.Load<Texture2D>("Arrows\\Receptor");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            var origin = new Vector2()
            {
                X = arrowReceptor.Width / 2,
                Y = arrowReceptor.Height / 2
            };

            for(int i = 265; i <563 ; i+=82) 
            {
                spriteBatch.Draw(arrowReceptor, new Vector2(i, 85), null, Color.White, arrowReceptorAngle(i), origin, 1.13f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            

            base.Draw(gameTime);
        }

        private float arrowReceptorAngle(int i)
        {
            switch (i)
            {
                case 265: return -MathHelper.Pi / 2;
                case 347: return MathHelper.Pi;
                case 429: return 0;
                case 511: return MathHelper.Pi / 2;
                default:  return MathHelper.Pi / 2;
            }
        }
    }
}
