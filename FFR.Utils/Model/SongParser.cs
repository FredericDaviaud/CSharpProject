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
using System.IO;
using FFR.Utils;
using NAudio.Wave;

namespace FFR.Parser
{
    public class SongParser
    {
        public String SongTitle { get; private set; }
        public String SongArtist { get; private set; }
        public float SongOffset { get; private set; }
        public List<Arrow> ArrowList { get; set; }
        private Mp3FileReader reader;
        TimeSpan duration;


        public List<Arrow> parse(String filePath)
        {
            string text;
            int lineNumber = 0;
            int mesureId = 1;
            int mesureSize = 0;
            List<Mesure> mesureList;

            StreamReader streamReader = new StreamReader(filePath);

            mesureList = new List<Mesure>();
            ArrowList = new List<Arrow>();

            while ((text = streamReader.ReadLine()) != null)
            {
                lineNumber++;
                if (lineNumber > 27)
                {
                    mesureSize++;
                    if (text.Substring(0, 1).Equals(",") || text.Substring(0, 1).Equals(";"))
                    {
                        mesureList.Add(new Mesure(mesureId, mesureSize - 1));
                        mesureSize = 0;
                        mesureId++;
                    }
                }
                else if (lineNumber == 1 || lineNumber == 3 || lineNumber == 13)
                {
                    switch (lineNumber)
                    {
                        case 1:
                            SongTitle = text.Substring(7, text.Length - 8);
                            break;
                        case 3:
                            SongArtist = text.Substring(8, text.Length - 9);
                            break;
                        case 13:
                            SongOffset = float.Parse((text.Substring(8, text.Length - 9)).Replace(".", ","));
                            break;
                        default:
                            break;
                    }
                }
            }

            streamReader = new StreamReader(filePath);
            mesureId = 1;
            float time = 0;
            reader = new Mp3FileReader("Songs\\Almost There.mp3");
            duration = reader.TotalTime;
            for (int i = 0; i < 27; i++) streamReader.ReadLine();
            
            do
            {
                for (int i = 0; i < mesureList[mesureId].size; i++)
                {
                    text = streamReader.ReadLine();
                    if (text.Substring(0, 1).Equals("1") || text.Substring(0, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(), Rows.Row1, time));
                    if (text.Substring(1, 1).Equals("1") || text.Substring(1, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(), Rows.Row2, time));
                    if (text.Substring(2, 1).Equals("1") || text.Substring(2, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(), Rows.Row3, time));
                    if (text.Substring(3, 1).Equals("1") || text.Substring(3, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(), Rows.Row4, time));
                    time += ((float) duration.TotalMilliseconds / 1000) / mesureList.Count / mesureList[mesureId].size;
                }
                streamReader.ReadLine();
                mesureId++;
            } while (mesureId != mesureList.Count);

            return ArrowList;
        }

        private String getArrowColor()
        {


            return ArrowColors.Blue;
        }
    }
}
