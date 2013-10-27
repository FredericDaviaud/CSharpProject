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

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            arrowReceptor = Content.Load<Texture2D>("Arrows\\Receptor");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var origin = new Vector2()
            {
                X = arrowReceptor.Width / 2,
                Y = arrowReceptor.Height / 2
            };

            spriteBatch.Begin();

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
