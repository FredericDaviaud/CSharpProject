using FFR.Parser;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace FFR.Utils
{
    public class Song
    {
        public String Name { get; private set; }
        public String Artist { get; private set; }
        public String Music { get; set; }
        public int Length { get; private set; }
        public int Combo { get; set; }
        public Score Score { get; private set; }
        public List<Arrow> ArrowList { get; set; }
        public float Offset { get; set; }
        public Microsoft.Xna.Framework.Media.Song SongFile { get; set; }
        
        private SpriteFont totalArrows;
        private SpriteFont combo;
        private bool isSongStarted = false;

        public Song()
        {
            this.Name = "Unknown";
            this.Artist = "Unknown";
            this.Length = -1;
            this.Score = new Score();
            this.ArrowList = null;
            this.Music = "";
        }

        public Song(String name, String artist, int length, Score bestScore, List<Arrow> arrowlist, String song)
        {
            this.Name = name;
            this.Artist = artist;
            this.Length = length;
            this.Score = bestScore;
            this.ArrowList = arrowlist;
            this.Music = song;
        }

        public void Initialize()
        {
            SongParser parser = new SongParser();
            ArrowList = parser.parse("Songs\\Almost There.sm");
            Offset = parser.SongOffset;

            foreach (Arrow arrow in ArrowList)
            {
                arrow.Initialize();
            }
        }

        public void LoadContent(ContentManager content)
        {
            try
            {
                totalArrows = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
                combo = content.Load<SpriteFont>("Spritefonts\\DefaultFont");
                this.Score.LoadContent(content);
                
            }
            catch (ArgumentNullException) { }

            SongFile = content.Load<Microsoft.Xna.Framework.Media.Song>("Songs\\Almost There");
            
            foreach (Arrow arrow in ArrowList)
            {
                arrow.LoadContent(content, arrow.ArrowColor.ToString());
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!isSongStarted)
            {
                if (gameTime.TotalGameTime.Seconds * 1000
                    + gameTime.TotalGameTime.Milliseconds >= 1200)
                {
                    MediaPlayer.Play(SongFile);
                    isSongStarted = true;
                }
            }
            foreach (Arrow arrow in ArrowList)
            {
                arrow.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Arrow arrow in ArrowList)
            {
                arrow.Draw(spriteBatch, gameTime);
            }

            spriteBatch.DrawString(totalArrows, string.Format("{0}", ArrowList.Count), new Vector2(600, 420), Color.White);
            spriteBatch.DrawString(combo, string.Format("{0}", Combo), new Vector2(140, 420), Color.White);
            this.Score.Draw(spriteBatch);
        }
    }
}
